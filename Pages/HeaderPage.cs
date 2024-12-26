using Microsoft.Playwright;

namespace project_homework.Pages;

public class HeaderPage(IPage page)
{
    private const string CartIconSelector = "#shopping_cart_container";

    public async Task OpenCartPage()
    {
        await page.Locator(CartIconSelector).ClickAsync();
    }
}