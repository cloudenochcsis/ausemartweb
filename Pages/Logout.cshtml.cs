using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ausemartweb.Pages
{
    public class LogoutPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear the session
            HttpContext.Session.Clear();
            
            TempData["SuccessMessage"] = "You have been successfully logged out.";
            return RedirectToPage("/Login");
        }

        public IActionResult OnPost()
        {
            return OnGet();
        }
    }
} 