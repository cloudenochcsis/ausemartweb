using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ausemartweb.Pages
{
    public class OrdersPageModel : PageModel
    {
        public bool HasOrders { get; set; } = false;

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Login");
            }

            // For demo purposes, we'll show orders if the user is logged in
            // In a real application, you would fetch orders from a database
            HasOrders = true;

            return Page();
        }
    }
} 