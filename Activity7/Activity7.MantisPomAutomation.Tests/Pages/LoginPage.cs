using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Pages;

public class LoginPage
{
  private readonly IPage _page;

  public LoginPage(IPage page)
  {
    _page = page;
  }

  public async Task Login(string username, string password)
  {
    await _page.GotoAsync("http://localhost:3000/login");
    await _page.GetByPlaceholder("Enter email address").FillAsync(username);
    await _page.GetByPlaceholder("Enter password").FillAsync(password);
    await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
  }

  public async Task HandleDialog()
  {
    _page.Dialog += async (_, dialog) =>
            {
              await dialog.AcceptAsync();
            };
  }

  public async Task AssertInLoginPage()
  {
    await Assertions.Expect(_page.GetByRole(AriaRole.Heading, new() { Name = "Login" })).ToBeVisibleAsync();
  }

}
