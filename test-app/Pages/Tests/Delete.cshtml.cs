using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace test_app.Pages.Tests
{
    [Authorize] // 🔒 Protegido

    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            // Validar si el test no existe
            if (test == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el test
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}

