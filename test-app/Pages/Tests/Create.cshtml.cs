using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;

namespace test_app.Pages.Tests
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(string Title)
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            var test = new Test { Title = Title, UserId = user.Id };
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
