using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;
using Microsoft.EntityFrameworkCore;

namespace test_app.Pages.Tests
{
    public class PlayModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Test Test { get; set; }

        [BindProperty]
        public List<Question> Questions { get; set; }

        [BindProperty]
        public List<string> UserAnswers { get; set; } = new List<string>();

        [BindProperty]
        public int Score { get; set; }

        [BindProperty]
        public bool ShowResults { get; set; } = false;

        public PlayModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Cargar Test y Preguntas
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Test = await _context.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (Test == null)
            {
                TempData["Error"] = "El test no existe o fue eliminado.";
                return RedirectToPage("/Index");
            }

            Questions = Test.Questions.ToList();
            return Page();
        }

        // ✅ Mostrar resultados después de enviar respuestas
        public async Task<IActionResult> OnPostAsync(int testId)
        {
            Test = await _context.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == testId);

            if (Test == null)
            {
                TempData["Error"] = "El test no fue encontrado.";
                return RedirectToPage("/Index");
            }

            Questions = Test.Questions.ToList();
            Score = 0;

            // ✅ Calcular el puntaje
            for (int i = 0; i < Questions.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(UserAnswers[i]) &&
                    Questions[i].CorrectAnswer.Equals(UserAnswers[i], StringComparison.OrdinalIgnoreCase))
                {
                    Score++;
                }
            }

            // ✅ Guardar resultado en la base de datos
            var result = new Result
            {
                UserId = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name)?.Id ?? 0,
                TestId = testId,
                Score = Score
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            // ✅ Mostrar resultados en la misma página
            ShowResults = true;
            return Page();
        }
    }
}
