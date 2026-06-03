using Microsoft.Playwright;


namespace Challenge.Challenge1
{
  public class Challenge1c : IClassFixture<PlaywrightFixture>
  {
    private readonly PlaywrightFixture _fixture;

    public Challenge1c(PlaywrightFixture fixture)
    {
      _fixture = fixture;
    }

    [Fact]
    public async Task LogoutButton_Should_RedirectToLoginPage_When_Clicked()
    {
      await using IBrowserContext context = await _fixture.Browser.NewContextAsync();
      IPage page = await context.NewPageAsync();

      //Login page
      await page.GotoAsync("http://localhost:3000/login");
      await page.GetByPlaceholder("Enter email address").FillAsync("testuser@gtravel.no");
      await page.GetByPlaceholder("Enter password").FillAsync("testuser1!");
      await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();

      //Dashboard page 
      await page.WaitForURLAsync("**/dashboard/default");

      await page.GetByRole(AriaRole.Button, new() { Name = "open profile" })
         .Filter(new() { Has = page.GetByRole(AriaRole.Img, new() { Name = "profile" }) })
         .ClickAsync();

      await page.Locator(".MuiCardContent-root")
        .Filter(new() { Has = page.GetByRole(AriaRole.Button, new() { Name = "Logout" }) })
        .GetByRole(AriaRole.Button, new() { Name = "Logout" })
        .ClickAsync();

      // Login page
      await page.WaitForURLAsync("**/login");



    }
  }
}