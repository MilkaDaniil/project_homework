using Microsoft.Playwright;

namespace project_homework.Pages;

public abstract class BasePage(IPage page)
{
    public async Task<bool> ValidateItemInCart(string itemName, string expectedPrice)
    {
        var itemLocator = $".cart_item:has-text('{itemName}')";
        var itemPriceLocator = $"{itemLocator} .inventory_item_price";

        if (!await page.Locator(itemLocator).IsVisibleAsync())
            return false;

        var actualPrice = await page.Locator(itemPriceLocator).InnerTextAsync();
        return actualPrice == expectedPrice;
    }
}