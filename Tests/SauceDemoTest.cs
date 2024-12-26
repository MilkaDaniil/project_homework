using Bogus;
using Microsoft.Playwright;
using NUnit.Framework;
using project_homework.Pages;
using Constants = project_homework.Utilities.Constants;

namespace project_homework.Tests;

[TestFixture]
public class PlaywrightTests
{
    private IPage _page;
    private IBrowser _browser;
    private IPlaywright _playwright;
    private LoginPage _loginPage;
    private InventoryPage _inventoryPage;
    private HeaderPage _headerPage;
    private CartPage _cartPage;
    private CheckoutPage _checkoutPage;

    [SetUp]
    public async Task SetUp()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        _page = await _browser.NewPageAsync();

        // Pages
        _loginPage = new LoginPage(_page);
        _inventoryPage = new InventoryPage(_page);
        _headerPage = new HeaderPage(_page);
        _cartPage = new CartPage(_page);
        _checkoutPage = new CheckoutPage(_page);

        await _page.GotoAsync(Constants.BaseUrl);
    }

    [Test]
    [Description("This test validates the happy path of order functionality on saucedemo app")]
    public async Task TestSauceDemoOrderFunctionality()
    {
        const string itemName = "Sauce Labs Bolt T-Shirt";
        const string itemPrice = "$15.99";
        
        TestContext.WriteLine("Step 1 - User authorization");
        await _loginPage.Authorize("standard_user", "secret_sauce");
        
        TestContext.WriteLine("Step 2 - Adding item to the cart by name: " + itemName);
        await _inventoryPage.AddItemToCartByName(itemName);

        TestContext.WriteLine("Step 3 - Opening cart page");
        await _headerPage.OpenCartPage();
        
        TestContext.WriteLine("Step 4 - Validate item cart count and added items");
        Assert.That(await _cartPage.GetCartItemCount(), Is.EqualTo(1), "Cart should contain exactly one item");
        Assert.That(await _cartPage.ValidateItemInCart(itemName, itemPrice), Is.True,
            "Cannot find item with provided price");

        TestContext.WriteLine("Step 5 - Proceed to checkout");
        await _cartPage.ClickCheckoutButton();

        TestContext.WriteLine("Step 6 - Fill the checkout form");
        var fakePerson = new Person();
        await _checkoutPage.FillCheckoutForm(fakePerson.FirstName, fakePerson.LastName, fakePerson.Address.ZipCode);
        
        TestContext.WriteLine("Step 7 - Validate items added to the cart");
        Assert.That(await _checkoutPage.ValidateItemInCart(itemName, itemPrice), Is.True,
            "Cannot find item with provided price");

        TestContext.WriteLine("Step 8 - Validate item total price including taxes");
        var totalPrice = await _checkoutPage.CalculateItemTotal();
        Assert.That(17.27, Is.EqualTo(totalPrice));

        TestContext.WriteLine("Step 9 - Complete the order");
        await _page.ClickAsync(CheckoutPage.FinishButton);
        
        TestContext.WriteLine("Step 10 - Verify order is completed");
        await _checkoutPage.VerifyOrderCompletion();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}