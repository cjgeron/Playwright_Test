using System.Text.RegularExpressions;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace Challenge.Challenge2
{
  public class Challenge2a
  {
    private const string DialogsUrl = "https://mantisdashboard.com/components-overview/dialogs";

    public Challenge2a()
    {

    }

    [Fact]
    public async Task Should_Automate_Common_Mui_Dialogs()
    {
      using IPlaywright playwright = await Playwright.CreateAsync();
      await using IBrowser browser = await playwright.Chromium.LaunchAsync(
      new BrowserTypeLaunchOptions
      {
        Headless = false,
        SlowMo = 500
      });
      IPage page = await browser.NewPageAsync();
      await page.GotoAsync(DialogsUrl);

      await HandleGreenDialog(page);
      await TestBasic(page);
      await TestAlert(page);
      await TestForm(page);
      await TestTransition(page);
      await TestCustomized(page);
      await TestFullscreen(page);
      await TestSizes(page);
      await TestResponsive(page);
      await TestDraggable(page);
      await TestScrollingPaper(page);
      await TestScrollingBody(page);
      await TestConfirmation(page);

    }

    private static async Task HandleGreenDialog(IPage page)
    {
      var greenDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Build faster with ready-to-use prompts" });

      // await greenDialog.WaitForAsync();
      await Expect(greenDialog).ToBeVisibleAsync();
      await greenDialog.GetByRole(AriaRole.Button)
        .ClickAsync();
    }

    private static async Task OpenDialogAsync(IPage page, string buttonName)
    {
      await page.GetByRole(AriaRole.Button, new() { Name = buttonName }
      ).ClickAsync();
      // await Expect(page.GetByRole(AriaRole.Dialog)).ToBeVisibleAsync();
    }

    private static async Task ClickDialogButtonAsync(ILocator dialog, string
    buttonName)
    {
      ILocator button = dialog.GetByRole(
      AriaRole.Button,
      new()
      {
        Name = buttonName,
        Exact = true
      });
      await button.ClickAsync();
      await Expect(dialog).ToBeHiddenAsync();
    }

    private static async Task CloseDialogAsync(IPage page)
    {
      ILocator dialog = page.GetByRole(AriaRole.Dialog);
      ILocator closeButton = dialog.GetByRole(
      AriaRole.Button,
      new()
      {
        NameRegex = new Regex("close", RegexOptions.IgnoreCase)
      });
      await closeButton.ClickAsync();
      await Expect(dialog).ToBeHiddenAsync();
    }

    private static async Task TestBasic(IPage page)
    {
      await OpenDialogAsync(page, "Open Simple Dialog");
      var basicDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Set backup account" });
      await Expect(basicDialog).ToBeVisibleAsync();
      await CloseDialogAsync(page);
      await Expect(basicDialog).ToBeHiddenAsync();

    }

    private static async Task TestAlert(IPage page)
    {
      await OpenDialogAsync(page, "Open Alert Dialog");
      var alertDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Use Google's location service?" });
      await Expect(alertDialog).ToBeVisibleAsync();
      await ClickDialogButtonAsync(alertDialog, "Agree");
      await Expect(alertDialog).ToBeHiddenAsync();
    }

    private static async Task TestForm(IPage page)
    {
      await OpenDialogAsync(page, "Open Form Dialog");
      ILocator formDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Subscribe" });
      await Expect(formDialog).ToBeVisibleAsync();
      await formDialog.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).FillAsync("testemail@test.com");
      await ClickDialogButtonAsync(formDialog, "Subscribe");
      await Expect(formDialog).ToBeHiddenAsync();
    }

    private static async Task TestTransition(IPage page)
    {
      await OpenDialogAsync(page, "Slide in Dialog");
      var transitionDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Use Google'ss location service?" });
      await Expect(transitionDialog).ToBeVisibleAsync();
      await ClickDialogButtonAsync(transitionDialog, "Agree");
      await Expect(transitionDialog).ToBeHiddenAsync();

    }

    private static async Task TestCustomized(IPage page)
    {
      await OpenDialogAsync(page, "Open Dialog");
      var customizedDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Modal Title" });
      await Expect(customizedDialog).ToBeVisibleAsync();
      await ClickDialogButtonAsync(customizedDialog, "Save changes");
      await Expect(customizedDialog).ToBeHiddenAsync();
    }

    private static async Task TestFullscreen(IPage page)
    {

      await OpenDialogAsync(page, "Open Full-screen Dialog");
      await Expect(page.GetByText("Set backup account")).ToBeVisibleAsync();
      await page.GetByLabel("Close").First.ClickAsync();
      await Expect(page.GetByText("Set backup account")).ToBeHiddenAsync();

    }

    private static async Task TestSizes(IPage page)
    {
      await OpenDialogAsync(page, "Open max-width dialog");
      var sizeDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Optional sizes" });
      await Expect(sizeDialog).ToBeVisibleAsync();
      var cmbSize = sizeDialog.GetByRole(AriaRole.Combobox);
      await cmbSize.ClickAsync();
      await page.GetByRole(AriaRole.Option, new() { Name = "xl" }).ClickAsync();
      await cmbSize.ClickAsync();
      await page.GetByRole(AriaRole.Option, new() { Name = "xs" }).ClickAsync();
      await sizeDialog.GetByRole(AriaRole.Checkbox).UncheckAsync();
      await sizeDialog.GetByRole(AriaRole.Checkbox).CheckAsync();
      await ClickDialogButtonAsync(sizeDialog, "Close");
      await Expect(sizeDialog).ToBeHiddenAsync();
    }

    private static async Task TestResponsive(IPage page)
    {
      await OpenDialogAsync(page, "Open responsive dialog");
      var responsiveDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Use Google's location service?" });
      await Expect(responsiveDialog).ToBeVisibleAsync();
      await ClickDialogButtonAsync(responsiveDialog, "Agree");
      await Expect(responsiveDialog).ToBeHiddenAsync();
    }

    private static async Task TestDraggable(IPage page)
    {
      await OpenDialogAsync(page, "Open Draggable Dialog");
      var draggableDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Subscribe" });
      await Expect(draggableDialog).ToBeVisibleAsync();
      await draggableDialog.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).FillAsync("testemail@test.com");
      await ClickDialogButtonAsync(draggableDialog, "Subscribe");
      await Expect(draggableDialog).ToBeHiddenAsync();
    }

    private static async Task TestScrollingPaper(IPage page)
    {
      await OpenDialogAsync(page, "Scroll=Paper");
      var scrollPaperDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Subscribe" });
      await Expect(scrollPaperDialog).ToBeVisibleAsync();
      await ClickDialogButtonAsync(scrollPaperDialog, "Subscribe");
      await Expect(scrollPaperDialog).ToBeHiddenAsync();
    }

    private static async Task TestScrollingBody(IPage page)
    {
      await OpenDialogAsync(page, "Scroll=Body");
      var scrollBodyDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Subscribe" });
      await Expect(scrollBodyDialog).ToBeVisibleAsync();
      await ClickDialogButtonAsync(scrollBodyDialog, "Subscribe");
      await Expect(scrollBodyDialog).ToBeHiddenAsync();
    }

    private static async Task TestConfirmation(IPage page)
    {
      await OpenDialogAsync(page, "Phone Ringtone");
      var confirmationDialog = page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Phone Ringtone" });
      await Expect(confirmationDialog).ToBeVisibleAsync();
      await confirmationDialog.GetByLabel("Callisto").ClickAsync();
      await confirmationDialog.GetByLabel("Oberon").ClickAsync();
      await ClickDialogButtonAsync(confirmationDialog, "Done");
      await Expect(confirmationDialog).ToBeHiddenAsync();
    }

  }
}