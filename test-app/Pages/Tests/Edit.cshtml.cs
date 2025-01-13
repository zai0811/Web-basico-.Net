using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;

namespace test_app.Pages.Tests
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Test Test { get; set; }

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            Test = _context.Tests.Find(id);
        }

        public async Task<IActionResult> OnPostAsync(int id, string Title)
        {
            var test = _context.Tests.Find(id);
            test.Title = Title;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
