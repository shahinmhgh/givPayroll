using Microsoft.Playwright;

namespace givPayroll.Services;

public sealed class PlaywrightPdfService : IPdfService
{
    private readonly IWebHostEnvironment _environment;

    public PlaywrightPdfService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<byte[]> GeneratePdfFromUrlAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        //using var playwright = await Playwright.CreateAsync();

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            ExecutablePath = @"C:\Users\shahin\source\repos\givPayroll\givPayroll\Playwright\chromium-1228\chrome.exe",
            Headless = true
        });


        //await using var browser =
        //    await playwright.Chromium.LaunchAsync(
        //        new BrowserTypeLaunchOptions
        //        {
        //            Headless = true
        //        });

        var page = await browser.NewPageAsync(
            new BrowserNewPageOptions
            {
                Locale = "fa-IR"
            });

        await page.GotoAsync(
            url,
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle,
                Timeout = 60000
            });

        // Make sure web fonts/images have finished loading.
        await page.EvaluateAsync("""
            async () => {
                if (document.fonts) {
                    await document.fonts.ready;
                }

                const images = Array.from(document.images);

                await Promise.all(
                    images.map(img => {
                        if (img.complete)
                            return Promise.resolve();

                        return new Promise(resolve => {
                            img.addEventListener("load", resolve);
                            img.addEventListener("error", resolve);
                        });
                    })
                );
            }
            """);

        return await page.PdfAsync(
            new PagePdfOptions
            {
                Format = "A4",

                PrintBackground = true,

                PreferCSSPageSize = true,

                DisplayHeaderFooter = false,

                Margin = new Margin
                {
                    Top = "10mm",
                    Bottom = "10mm",
                    Left = "10mm",
                    Right = "10mm"
                }
            });
    }
}