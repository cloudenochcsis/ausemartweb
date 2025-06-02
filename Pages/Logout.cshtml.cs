using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ausemartweb.Pages
{
    public class LogoutPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Get current session data for debugging
            var userId = HttpContext.Session.GetString("UserId");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userName = HttpContext.Session.GetString("UserName");
            
            // Log the current session state (in a real app, use proper logging)
            Console.WriteLine($"Logout - Current session: UserId={userId}, Email={userEmail}, Name={userName}");
            
            // Clear the session
            HttpContext.Session.Clear();
            
            // Verify session is cleared
            var userIdAfter = HttpContext.Session.GetString("UserId");
            Console.WriteLine($"Logout - Session after clear: UserId={userIdAfter}");
            
            TempData["SuccessMessage"] = "You have been successfully logged out.";
            return RedirectToPage("/Login");
        }

        public IActionResult OnPost()
        {
            return OnGet();
        }
    }
} 