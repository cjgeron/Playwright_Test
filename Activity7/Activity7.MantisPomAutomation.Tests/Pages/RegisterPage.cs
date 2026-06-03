using Activity7.MantisPomAutomation.Fixtures;
using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Pages;

public class RegisterPage
{
  private readonly IPage _page;

  public RegisterPage(IPage page)
  {
    _page = page;
  }

  public async Task RegisterUser(string firstname, string lastname, string company, string email, string password)
  {
    await _page.GotoAsync("http://localhost:3000/register");

    await _page.GetByPlaceholder("John").FillAsync(firstname);
    await _page.GetByPlaceholder("Doe").FillAsync(lastname);
    await _page.GetByPlaceholder("Demo Inc.").FillAsync(company);
    await _page.GetByPlaceholder("demo@company.com").FillAsync(email);
    await _page.GetByPlaceholder("******").FillAsync(password);
    await _page.GetByRole(AriaRole.Button, new() { Name = "Create Account" }).ClickAsync();
  }

}