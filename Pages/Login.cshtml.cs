using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ausemartweb.Models;
using ausemartweb.Services;

namespace ausemartweb.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginPageModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginModel Input { get; set; } = new();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public void OnGet()
        {
            // Check if there's a success message from registration
            if (TempData.ContainsKey("SuccessMessage"))
            {
                SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _authService.ValidateUserAsync(Input);

            if (user != null)
            {
                // Store user info in session (simplified approach)
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

                TempData["SuccessMessage"] = $"Welcome back, {user.FirstName}!";
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid email or password. Please try again.";
                return Page();
            }
        }
    }
} 