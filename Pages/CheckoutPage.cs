using System.Globalization;
using Microsoft.Playwright;
using NUnit.Framework;

namespace project_homework.Pages;

public class CheckoutPage(IPage page) : BasePage(page)
{
    private const string FirstNameField = "#first-name";
    private const string LastNameField = "#last-name";
    private const string PostalCodeField = "#postal-code";
    private const string ContinueButton = "#continue";
    public static string FinishButton => "#finish";
    private readonly IPage _page = page;

    public async Task FillCheckoutForm(string firstName, string lastName, string postalCode)
    {
        await _page.FillAsync(FirstNameField, firstName);
        await _page.FillAsync(LastNameField, lastName);
        await _page.FillAsync(PostalCodeField, postalCode);
        await _page.ClickAsync(ContinueButton);
    }

    public async Task<decimal> CalculateItemTotal()
    {
        var itemTotal = await _page.InnerTextAsync(".summary_total_label");
        var numericValue = itemTotal.Replace("Total: $", string.Empty).Trim();

        if (decimal.TryParse(numericValue, NumberStyles.Currency, CultureInfo.InvariantCulture, out var price))
        {
            return price;
        }

        throw new Exception($"Unable to parse price from item total: {itemTotal}");
    }

    public async Task VerifyOrderCompletion()
    {
        await _page.WaitForSelectorAsync(".complete-header");

        var headerText = await _page.InnerTextAsync(".complete-header");
        var completionMessage = await _page.InnerTextAsync(".complete-text");

        // For Soft assertions in case if both messages are incorrect
        Assert.Multiple(() =>
        {
            Assert.That(headerText.Contains("Thank you for your order"),
                Is.True, "Order completion message is not displayed correctly");

            Assert.That(completionMessage.Contains("Your order has been dispatched"),
                Is.True, "Order completion details are not displayed correctly");
        });
    }
}