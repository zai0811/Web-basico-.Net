using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;

namespace test_app.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(string Username, string Password)
        {
            var user = new User { Username = Username, PasswordHash = Password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Login");
        }
    }
}
