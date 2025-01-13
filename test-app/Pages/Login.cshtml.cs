using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using test_app.Data;
using test_app.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace test_app.Pages
{
    [AllowAnonymous] // Permitir el acceso sin autenticación
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(string Username, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);
            if (user != null && user.PasswordHash == Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError("", "Credenciales incorrectas");
            return Page();
        }
    }
}


