using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;
using Microsoft.EntityFrameworkCore;

namespace test_app.Pages
{
    [Authorize] // Protegido
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Test> Tests { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var username = HttpContext.User.Identity.Name;
            Tests = _context.Tests
                .Include(t => t.User)
                .Where(t => t.User.Username == username)
                .ToList();
        }
    }
}


