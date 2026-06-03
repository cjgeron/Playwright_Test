using Microsoft.Playwright;
using Xunit;

namespace Challenge.Challenge1
{
  public class Challenge1a
  {
    public Challenge1a()
    {

    }

    [Fact]
    public async Task ClosePopUp()
    {

      using IPlaywright playwright = await Playwright.CreateAsync();
      await using IBrowser browser = await playwright.Chromium.LaunchAsync(
      new BrowserTypeLaunchOptions
      {
        Headless = false,
        SlowMo = 500
      });
      IPage page = await browser.NewPageAsync();

      await page.GotoAsync("https://mantisdashboard.com/login");

      await page.GetByPlaceholder("Enter email address").FillAsync("info@codedthemes.com");
      await page.GetByPlaceholder("Enter password").FillAsync("123456");
      await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();

      // var txtPopUp = await page.GetByText("Build faster with ready-to-use prompts");
      await page.GetByRole(AriaRole.Button, new() { Name = "×" }).ClickAsync();

      // var btnPopUp = await txtPopUp.Locator("..").GetByRole(AriaRole.Button);

    }

  }
}