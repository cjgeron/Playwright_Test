using Microsoft.Playwright;
using Xunit;

namespace Challenge.Challenge1
{
  public class PlaywrightFixture : IAsyncLifetime
  {
    public IPlaywright Playwright { get; private set; } = null!;
    public IBrowser Browser { get; private set; } = null!;
    public async Task InitializeAsync()
    {
      Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
      Browser = await Playwright.Chromium.LaunchAsync(
      new BrowserTypeLaunchOptions
      {
        Headless = false,
        SlowMo = 500
      });
    }
    public async Task DisposeAsync()
    {
      await Browser.CloseAsync();
      Playwright.Dispose();
    }
  }
}