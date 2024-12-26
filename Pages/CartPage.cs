using Microsoft.Playwright;

namespace project_homework.Pages;

public class CartPage(IPage page) : BasePage(page)
{
    private const string CheckoutButtonLocator = "#checkout";
    private readonly IPage _page = page;

    public async Task<int> GetCartItemCount()
    {
        var cartItems = await _page.Locator(".cart_item").CountAsync();
        return cartItems;
    }

    public async Task ClickCheckoutButton()
    {
        await _page.Locator(CheckoutButtonLocator).WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });
        await _page.Locator(CheckoutButtonLocator).ClickAsync();
    }
}