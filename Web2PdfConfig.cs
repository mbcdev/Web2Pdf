using System;

namespace Web2Pdf
{
    public class Web2PdfConfig
    {
        public string UrlString { get; set; }
        public string OutputFilePath { get; set; }
        public bool UseTimestampPrefix { get; set; } = false;

        public static Web2PdfConfig FromArgs(string[] args)
        {
            var config = new Web2PdfConfig();

            if (args.Length > 0)
            {
                config.UrlString = args[0];

                if (args.Length > 1)
                {
                    if (args[1].ToLower().Equals("/ts"))
                    {
                        config.UseTimestampPrefix = true;
                        if (args.Length == 3)
                        {
                            config.OutputFilePath = args[2];
                        }
                    }
                    else if (args.Length == 2)
                    {
                        config.OutputFilePath = args[1];
                    }
                }
            }

            return config;
        }

        public bool IsValid()
        {
            try
            {
                new Uri(UrlString);
                if (!string.IsNullOrEmpty(OutputFilePath) && OutputFilePath.ToLower().EndsWith(".pdf"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
