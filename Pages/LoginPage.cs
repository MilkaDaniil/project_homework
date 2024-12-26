using Microsoft.Playwright;

namespace project_homework.Pages;

public class LoginPage(IPage page)
{
    private const string UsernameField = "#user-name";
    private const string PasswordField = "#password";
    private const string LoginButton = "#login-button";

    public async Task Authorize(string username, string password)
    {
        await page.FillAsync(UsernameField, username);
        await page.FillAsync(PasswordField, password);
        await page.ClickAsync(LoginButton);
    }
}