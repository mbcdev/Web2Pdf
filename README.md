# Web2Pdf
A Windows command-line utility to save websites as a PDF file.

## Description
This utility renders a given website by using the [Chromium Embedded Framework][CEF] (CEF) and prints the web content to a PDF file.  
Optionally, a timestamp prefix can be added to the output file name which e.g. makes it easier to store multiple versions of a website.

**Why not using Microsoft Edge/WebView2?**  
I needed to create snapshots of websites unattended by using Windows Task Scheduler, but unfortunately, WebView2 [cannot run][WV2Issue] in the context of the SYSTEM user account and does not work if no user is logged on in Windows.

[CEF]: https://github.com/chromiumembedded
[WV2Issue]: https://github.com/MicrosoftEdge/WebView2Feedback/issues/1907

## Getting Started

### Compilation
```
msbuild Web2Pdf.sln
```
Alternatively, you can simply open the Solution File in Visual Studio and compile from there.

### Usage Examples

Create PDF file from a given URL:
```
Web2Pdf.exe http://<URL> website.pdf
Web2Pdf.exe https://<URL> /ts C:\Folder\website.pdf
```

## Version History

* 0.1
    * Initial Release

## License
This project is licensed under the MIT License.
