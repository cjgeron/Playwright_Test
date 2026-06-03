using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Pages;

public class DashboardPage
{
  private readonly IPage _page;

  public DashboardPage(IPage page)
  {
    _page = page;
  }

  public async Task ClickDashboardLink()
  {
    await _page.GetByRole(AriaRole.Link, new() { Name = "Dashboard" }).ClickAsync();
  }

  public async Task ClickWeekButton()
  {
    await _page.GetByRole(AriaRole.Button, new() { Name = "Week" }).ClickAsync();
  }

  public async Task ClickTueChart()
  {
    await _page.Locator("#root").GetByText("Tue").ClickAsync();
  }
  public async Task ClickTuesdayOnChartAsync()
  {
    var tuesdayLabel = _page
        .Locator("svg")
        .GetByText("Tue")
        .First;

    await Assertions.Expect(tuesdayLabel)
        .ToBeVisibleAsync();

    await tuesdayLabel.ClickAsync();
  }

  public async Task ClickProfileButton()
  {
    await _page.GetByRole(AriaRole.Button, new() { Name = "open profile" })
       .Filter(new() { Has = _page.GetByRole(AriaRole.Img, new() { Name = "profile" }) })
       .ClickAsync();
  }

  public async Task ClickProfileLogoutIconButton()
  {
    await _page.Locator(".MuiCardContent-root")
      .Filter(new() { Has = _page.GetByRole(AriaRole.Button, new() { Name = "Logout" }) })
      .GetByRole(AriaRole.Button, new() { Name = "Logout" })
      .ClickAsync();
  }

  public async Task AssertInDashboardPage()
  {
    await Assertions.Expect(_page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" })).ToBeVisibleAsync();
  }

  public async Task AssertChartPopupVisible()
  {
    await Assertions.Expect(_page.GetByText("Page views", new() { Exact = true })).ToBeVisibleAsync();
    await Assertions.Expect(_page.GetByText("Sessions", new() { Exact = true })).ToBeVisibleAsync();
  }

}
