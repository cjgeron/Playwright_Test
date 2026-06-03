using Activity7.MantisPomAutomation.Fixtures;
using Activity7.MantisPomAutomation.Pages;
using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Tests;

public class DashboardTests : IClassFixture<PlaywrightFixture>
{
  private readonly PlaywrightFixture _fixture;

  public DashboardTests(PlaywrightFixture fixture)
  {
    _fixture = fixture;
  }

  // [Fact]
  // public async Task Should_LogoutSucess_When_LogoutClicked()
  // {
  //   await using var context = await _fixture.Browser.NewContextAsync();
  //   var page = await context.NewPageAsync();

  //   var pageLogin = new LoginPage(page);
  //   var pageDashboard = new DashboardPage(page);

  //   await pageLogin.HandleDialog();
  //   await pageLogin.Login("testuser@gtravel.no", "testuser1!");
  //   await pageDashboard.AssertInDashboardPage();
  //   await pageDashboard.ClickProfileButton();
  //   await pageDashboard.ClickProfileLogoutIconButton();
  //   await pageLogin.AssertInLoginPage();

  // }

  [Fact]
  public async Task Should_TueValuesVisible_When_TueClicked()
  {

    await using var context = await _fixture.Browser.NewContextAsync();
    var page = await context.NewPageAsync();

    var pageLogin = new LoginPage(page);
    var pageDashboard = new DashboardPage(page);

    await pageLogin.HandleDialog();
    await pageLogin.Login("testuser@gtravel.no", "testuser1!");
    await pageDashboard.AssertInDashboardPage();
    await pageDashboard.ClickDashboardLink();
    await pageDashboard.ClickWeekButton();
    await pageDashboard.ClickTueChart();
    await pageDashboard.AssertChartPopupVisible();

  }




}