using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ausemartweb.Models;
using ausemartweb.Services;
using System.ComponentModel.DataAnnotations;

namespace ausemartweb.Pages
{
    public class ProfilePageModel : PageModel
    {
        private readonly AuthService _authService;

        public ProfilePageModel(AuthService authService)
        {
            _authService = authService;
        }

        public User? UserInfo { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public UpdateProfileModel UpdateProfile { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Login");
            }

            if (int.TryParse(userIdString, out int userId))
            {
                var users = await _authService.GetAllUsersAsync();
                UserInfo = users.FirstOrDefault(u => u.Id == userId);
                
                if (UserInfo == null)
                {
                    return RedirectToPage("/Login");
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Reload user info
                return Page();
            }

            try
            {
                // In a real application, you would update the user in the database
                // For now, we'll just show a success message
                SuccessMessage = "Profile updated successfully!";
                
                // Update session with new name if changed
                HttpContext.Session.SetString("UserName", $"{UpdateProfile.FirstName} {UpdateProfile.LastName}");
                
                await OnGetAsync(); // Reload user info
                return Page();
            }
            catch (Exception)
            {
                ErrorMessage = "An error occurred while updating your profile. Please try again.";
                await OnGetAsync(); // Reload user info
                return Page();
            }
        }
    }

    public class UpdateProfileModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }
} 