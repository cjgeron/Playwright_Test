using Activity6.MuiTableAutomation.Tests.Fixtures;
using Activity6.MuiTableAutomation.Tests.Pages;
using Activity6.MuiTableAutomation.Tests.Utils;
using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests.Tests;

public class TableFilteringTests : IClassFixture<PlaywrightFixture>
{
  private readonly PlaywrightFixture _fixture;

  public TableFilteringTests(PlaywrightFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task Should_Filter_When_GlobalFilter()
  {
    await using var context = await _fixture.Browser.NewContextAsync();
    var page = await context.NewPageAsync();

    var pageLogin = new LoginPage(page);
    var pageTableFiltering = new TableFilteringPage(page);

    await pageLogin.GoToAsync();
    await pageLogin.LoginAsync(Constants.LOGIN_USERNAME, Constants.LOGIN_PASSWORD);

    await pageTableFiltering.GoToAsync();

    var firstDataRow = await pageTableFiltering.GetFirstDataRowLocator();
    var fullName = await firstDataRow.Nth(0).InnerTextAsync();

    await pageTableFiltering.FillGlobalFilterAsync(fullName);

    await Assertions.Expect(page.Locator("td").GetByText(fullName)).ToBeVisibleAsync();
  }

  [Fact]
  public async Task Should_Filter_Age_When_RangeInputted()
  {
    await using var context = await _fixture.Browser.NewContextAsync();
    var page = await context.NewPageAsync();

    var pageLogin = new LoginPage(page);
    var pageTableFiltering = new TableFilteringPage(page);

    await pageLogin.GoToAsync();
    await pageLogin.LoginAsync(Constants.LOGIN_USERNAME, Constants.LOGIN_PASSWORD);

    await pageTableFiltering.GoToAsync();
    await pageTableFiltering.FilterAgeAsync(20, 30);
    await pageTableFiltering.AssertAllRecordInAgeRangeAsync(20, 30);
  }

}