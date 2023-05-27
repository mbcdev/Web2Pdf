using CefSharp;
using CefSharp.OffScreen;
using System;
using System.IO;
using System.Threading;

namespace Web2Pdf
{
    public class Web2PdfWebBrowser
    {
        private Web2PdfConfig _config;
        private bool _success = false;

        private ManualResetEvent _evtCompleted = null;
        private ChromiumWebBrowser _browser = null;

        public Web2PdfWebBrowser(Web2PdfConfig config)
        {
            _config = config;
            _evtCompleted = new ManualResetEvent(false);
            var settings = new CefSettings()
            {
                //CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                //LogSeverity = LogSeverity.Disable
            };

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            _browser = new ChromiumWebBrowser(_config.UrlString);
            _browser.LoadingStateChanged += Browser_LoadingStateChanged;
        }

        private async void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                _browser.LoadingStateChanged -= Browser_LoadingStateChanged;
                Thread.Sleep(500);
                var filePath = GetEffectiveOutputFilePath();
                _success = await _browser.PrintToPdfAsync(filePath);
                if (_success)
                {
                    Console.WriteLine($"File '{filePath}' successfully created.");
                }
                else
                {

                    Console.WriteLine($"File '{filePath}' could not be created.");
                }
                _evtCompleted.Set();
            }
        }

        private string GetEffectiveOutputFilePath()
        {
            var fullFilePath = Path.GetFullPath(_config.OutputFilePath);

            if (_config.UseTimestampPrefix)
            {
                var folderName = Path.GetDirectoryName(fullFilePath);
                var fileName = Path.GetFileName(fullFilePath);
                var newFileName = $"{System.DateTime.Now.ToString("yyyyMMdd_HHmmss")}_{fileName}";
                var newPath = Path.Combine(folderName, newFileName);
                return newPath;
            }
            else
            {
                return fullFilePath;
            }
        }

        public bool WaitForCompletion()
        {
            _evtCompleted.WaitOne();
            Cef.Shutdown();
            return _success;
        }
    }
}
