using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ausemartweb.Models;
using ausemartweb.Services;

namespace ausemartweb.Pages
{
    public class RegisterPageModel : PageModel
    {
        private readonly AuthService _authService;

        public RegisterPageModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterModel Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registrationResult = await _authService.RegisterUserAsync(Input);

            if (registrationResult)
            {
                TempData["SuccessMessage"] = "Registration successful! Please sign in with your new account.";
                return RedirectToPage("/Login");
            }
            else
            {
                ErrorMessage = "A user with this email address already exists. Please use a different email or try signing in.";
                return Page();
            }
        }
    }
} 