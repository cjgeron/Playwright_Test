using Activity7.MantisPomAutomation.Fixtures;
using Activity7.MantisPomAutomation.Pages;
using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Tests;

public class RegisterTests : IClassFixture<PlaywrightFixture>
{
  private readonly PlaywrightFixture _fixture;

  public RegisterTests(PlaywrightFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task Should_RegisterNewUser_When_ValidDetails()
  {
    await using var context = await _fixture.Browser.NewContextAsync();
    var page = await context.NewPageAsync();

    var pageRegister = new RegisterPage(page);
    var pageLogin = new LoginPage(page);
    string uniqueEmail = $"testuser{DateTime.Now:yyyyMMddHHmmss}@demo.com";

    await pageRegister.RegisterUser("John", "Doe", "Company Test", uniqueEmail, "123456");
    await pageLogin.AssertInLoginPage();

  }
}