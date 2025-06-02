using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ausemartweb.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactForm ContactInfo { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // Initialize page
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please fill in all required fields correctly.";
                return Page();
            }

            try
            {
                // Here you would typically:
                // 1. Send email notification
                // 2. Save to database
                // 3. Integrate with CRM system
                
                // For now, we'll just simulate success
                await SimulateEmailSending();
                
                SuccessMessage = $"Thank you {ContactInfo.FirstName}! Your message has been sent successfully. We'll get back to you within 24 hours.";
                
                // Clear the form after successful submission
                ContactInfo = new ContactForm();
                
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Sorry, there was an error sending your message. Please try again or contact us directly.";
                // Log the exception in a real application
                return Page();
            }
        }

        private async Task SimulateEmailSending()
        {
            // Simulate email sending delay
            await Task.Delay(500);
        }
    }

    public class ContactForm
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please select a subject")]
        public string Subject { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Order number cannot exceed 20 characters")]
        public string? OrderNumber { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        [MinLength(10, ErrorMessage = "Message must be at least 10 characters")]
        public string Message { get; set; } = string.Empty;

        public bool Newsletter { get; set; } = false;
    }
} 