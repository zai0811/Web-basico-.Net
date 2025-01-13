using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;
using Microsoft.EntityFrameworkCore;

namespace test_app.Pages.Tests
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Test Test { get; set; } = new Test();

        [BindProperty]
        public List<Question> Questions { get; set; } = new List<Question>();

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // No se añaden preguntas por defecto
        }

        public async Task<IActionResult> OnPostAsync()
        {
        

            if (Questions == null || !Questions.Any(q => !string.IsNullOrWhiteSpace(q.Text)))
            {
                TempData["Error"] = "Debes añadir al menos una pregunta con respuesta.";
                return Page();
            }

            var username = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["Error"] = "Usuario no autenticado. Inicia sesión nuevamente.";
                return RedirectToPage("/Login");
            }

            // Guardar el test
            Test.UserId = user.Id;
            _context.Tests.Add(Test);
            await _context.SaveChangesAsync();

            // Guardar las preguntas asociadas al test
            foreach (var question in Questions)
            {
                if (!string.IsNullOrWhiteSpace(question.Text) && !string.IsNullOrWhiteSpace(question.CorrectAnswer))
                {
                    question.TestId = Test.Id;
                    _context.Questions.Add(question);
                }
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "¡El test y las preguntas se guardaron correctamente!";
            return RedirectToPage("/Index");
        }
    }
}





