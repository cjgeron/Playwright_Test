using Activity6.MuiTableAutomation.Tests.Utils;
using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests.Pages;

public class TableFilteringPage
{
  private readonly IPage _page;

  public TableFilteringPage(IPage page)
  {
    _page = page;
  }

  public async Task GoToAsync()
  {
    await _page.GotoAsync(Constants.BASE_URL + "tables/react-table/filtering");
    await AssertInPageAsync();
  }

  public async Task AssertInPageAsync()
  {
    await Assertions.Expect(_page.Locator("h2").Filter(new() { HasText = "Filtering" })).ToBeVisibleAsync();
  }

  public async Task<ILocator> GetFirstDataRowLocator()
  {
    return _page.Locator("tbody tr").First.Locator("td");
  }

  public async Task FillGlobalFilterAsync(string search)
  {
    await _page.GetByRole(AriaRole.Textbox, new() { Name = "Search records" }).FillAsync(search);
  }

  public async Task FilterAgeAsync(int minAge, int maxAge)
  {
    var ageColumnHead = _page.Locator("thead tr").Nth(1).GetByRole(AriaRole.Columnheader).Nth(3);
    await Assertions.Expect(ageColumnHead).ToBeVisibleAsync();

    await ageColumnHead.GetByPlaceholder("Min").FillAsync(minAge.ToString());
    await ageColumnHead.GetByPlaceholder("Max").FillAsync(maxAge.ToString());
  }

  public async Task AssertAllRecordInAgeRangeAsync(int minAge, int maxAge)
  {
    var dataRows = _page.Locator("tbody tr");
    var countRows = await dataRows.CountAsync();
    for (int i = 0; i < countRows; i++)
    {

      var cellAge = dataRows.Nth(i).GetByRole(AriaRole.Cell).Nth(3);
      var ageValue = Convert.ToInt32(await cellAge.InnerTextAsync());
      await _page.PauseAsync();
      Assert.InRange(ageValue, minAge, maxAge);
    }
  }

}