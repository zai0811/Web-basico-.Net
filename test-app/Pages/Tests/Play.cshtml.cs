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

        public Test Test { get; set; }
        public List<Question> Questions { get; set; }

        public PlayModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Test = await _context.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (Test == null || Test.Questions.Count == 0)
            {
                TempData["Message"] = "El test no tiene preguntas o no fue encontrado.";
                return RedirectToPage("/Index");
            }

            Questions = Test.Questions;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int[] QuestionIds, string[] UserAnswers)
        {
            int score = 0;
            List<string> resultados = new List<string>();

            for (int i = 0; i < QuestionIds.Length; i++)
            {
                var question = await _context.Questions.FindAsync(QuestionIds[i]);

                if (question != null && question.CorrectAnswer.Trim().ToLower() == UserAnswers[i].Trim().ToLower())
                {
                    score++;
                    resultados.Add($"✅ Pregunta {i + 1}: Correcta.");
                }
                else
                {
                    resultados.Add($"❌ Pregunta {i + 1}: Incorrecta. Respuesta Correcta: {question.CorrectAnswer}");
                }
            }

            var username = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Guardar el resultado en la base de datos
            var result = new Result
            {
                UserId = user.Id,
                TestId = QuestionIds[0], 
                Score = score
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            // ✅ Mostrar los resultados del test y la puntuación
            TempData["Message"] = $"Tu puntuación es {score}/{QuestionIds.Length}." + string.Join("", resultados);
            return RedirectToPage("/Tests/Play", new { id = QuestionIds[0] });
        }
    }
}


