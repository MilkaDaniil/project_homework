using Microsoft.Playwright;

namespace project_homework.Pages;

public class InventoryPage(IPage page)
{
    public async Task AddItemToCartByName(string itemName)
    {
        var productSelector = $"div.inventory_item:has-text('{itemName}') >> button:has-text('Add to cart')";
        await page.Locator(productSelector).WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
        });

        await page.ClickAsync(productSelector);
    }
}