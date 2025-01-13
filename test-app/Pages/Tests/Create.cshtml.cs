using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;

namespace test_app.Pages.Tests
{
    [Authorize] // 🔒 Protegido
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(string Title)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null) return RedirectToPage("/Login");

            var test = new Test { Title = Title, UserId = user.Id };
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}

