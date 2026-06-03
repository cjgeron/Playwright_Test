using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests.Pages;

public class DashboardPage
{
  private readonly IPage _page;

  public DashboardPage(IPage page)
  {
    _page = page;
  }

}