using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using test_app.Data;
using test_app.Models;

namespace test_app.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(string Username, string Password)
        {
            // ✅ Validar si el usuario ya existe
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "El usuario ya existe. Intenta con otro nombre de usuario.");
                return Page(); // No permite avanzar y muestra el error
            }

            // ✅ Crear nuevo usuario si no existe
            var newUser = new User { Username = Username, PasswordHash = Password };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // ✅ Redirigir al login después de registrarse
            return RedirectToPage("/Login");
        }
    }
}
