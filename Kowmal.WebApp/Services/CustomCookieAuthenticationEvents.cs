using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kowmal.WebApp.Services;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.Redirect("/account/login?ReturnUrl=%2Fposts");
        return Task.CompletedTask;
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.Redirect("/account/login?ReturnUrl=%2Fposts");
        return Task.CompletedTask;
    }
}