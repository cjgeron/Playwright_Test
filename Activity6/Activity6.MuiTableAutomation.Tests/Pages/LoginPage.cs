using Activity6.MuiTableAutomation.Tests.Utils;
using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests.Pages;

public class LoginPage
{
  private readonly IPage _page;

  public LoginPage(IPage page)
  {
    _page = page;
  }

  public async Task GoToAsync()
  {
    await _page.GotoAsync(Constants.BASE_URL + "login");
    await AssertInPageAsync();
  }

  public async Task AssertInPageAsync()
  {
    await Assertions.Expect(_page.GetByRole(AriaRole.Heading, new() { Name = "Login" })).ToBeVisibleAsync();
  }

  public async Task LoginAsync(string username, string password)
  {
    await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).FillAsync(username);
    await _page.GetByRole(AriaRole.Textbox, new() { Name = "Enter password" }).FillAsync(password);
    await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
  }

}