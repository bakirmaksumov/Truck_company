using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Truck_company.Models;
using Truck_company.Services;

namespace Truck_company.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly GoogleRecaptchaVerifier _recaptcha;
    private readonly GoogleRecaptchaOptions _recaptchaOptions;

    public LoginModel(
        SignInManager<ApplicationUser> signInManager,
        ILogger<LoginModel> logger,
        GoogleRecaptchaVerifier recaptcha,
        IOptions<GoogleRecaptchaOptions> recaptchaOptions)
    {
        _signInManager = signInManager;
        _logger = logger;
        _recaptcha = recaptcha;
        _recaptchaOptions = recaptchaOptions.Value;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public string SiteKey => _recaptchaOptions.SiteKey;

    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl ??= returnUrl;
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl ??= returnUrl;
        var redirectUrl = string.IsNullOrWhiteSpace(ReturnUrl) ? Url.Content("~/Admin") : ReturnUrl;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var captchaValid = await _recaptcha.VerifyAsync(
            Input.RecaptchaToken,
            HttpContext.Connection.RemoteIpAddress?.ToString(),
            HttpContext.RequestAborted);

        if (!captchaValid)
        {
            _logger.LogWarning("Login rejected because reCAPTCHA verification failed for {Email}.", Input.Email);
            ModelState.AddModelError(string.Empty, "Please complete the CAPTCHA verification.");
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(
            Input.Email,
            Input.Password,
            Input.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");
            return LocalRedirect(redirectUrl);
        }

        if (result.RequiresTwoFactor)
        {
            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = redirectUrl, RememberMe = Input.RememberMe });
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return RedirectToPage("./Lockout");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        _logger.LogWarning("Login rejected by Identity for {Email}. Result: Failed.", Input.Email);
        return Page();
    }

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string RecaptchaToken { get; set; } = string.Empty;
    }
}
