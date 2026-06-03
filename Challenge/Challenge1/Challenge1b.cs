using Microsoft.Playwright;
using Xunit;

namespace Challenge.Challenge1
{

  public class Challenge1b : IClassFixture<PlaywrightFixture>
  {
    private readonly PlaywrightFixture _fixture;

    public Challenge1b(PlaywrightFixture fixture)
    {
      _fixture = fixture;

    }

    [Fact]
    public async Task Dashboard_Should_VisibleAccountNav_When_Logout()
    {
      await using IBrowserContext context = await _fixture.Browser.NewContextAsync();
      IPage page = await context.NewPageAsync();

      await page.GotoAsync("http://localhost:3000/dashboard");

      // await Assertions.Expect(page.GetByRole(AriaRole.Navigation).GetByRole(AriaRole.Img, new() { Name = "login" })).ToBeVisibleAsync();
      // await Assertions.Expect(page.GetByRole(AriaRole.Navigation).GetByRole(AriaRole.Img, new() { Name = "profile" })).ToBeVisibleAsync();

      await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "login" })).ToBeVisibleAsync();
      await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "profile" })).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Dashboard_Should_NotVisibleAccountNav_When_Login()
    {
      await using IBrowserContext context = await _fixture.Browser.NewContextAsync();
      IPage page = await context.NewPageAsync();

      await page.GotoAsync("http://localhost:3000/login");
      await page.GetByPlaceholder("Enter email address").FillAsync("testuser@gtravel.no");
      await page.GetByPlaceholder("Enter password").FillAsync("testuser1!");
      await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();

      await page.WaitForURLAsync("**/dashboard/default");
      await page.ReloadAsync();
      // await Assertions.Expect(page.GetByRole(AriaRole.Navigation).GetByRole(AriaRole.Img, new() { Name = "login" })).ToBeHiddenAsync();
      // await Assertions.Expect(page.GetByRole(AriaRole.Navigation).GetByRole(AriaRole.Img, new() { Name = "profile" })).ToBeHiddenAsync();

      await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "login" })).ToBeHiddenAsync();
      await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "profile" })).ToBeHiddenAsync();

    }
  }
}