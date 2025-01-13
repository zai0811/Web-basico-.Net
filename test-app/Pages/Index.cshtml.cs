using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace test_app.Pages
{
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
            // Verificar si el usuario está autenticado antes de acceder a su nombre
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var username = HttpContext.User.Identity.Name;
                
                // Asegúrate de que User esté incluido correctamente en el contexto
                Tests = _context.Tests
                    .Include(t => t.User)
                    .Where(t => t.User != null && t.User.Username == username)
                    .ToList();
            }
            else
            {
                Tests = new List<Test>(); // Devolver una lista vacía si no está autenticado
            }
        }
    }
}
