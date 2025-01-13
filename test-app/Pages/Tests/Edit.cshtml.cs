using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_app.Data;
using test_app.Models;
using Microsoft.EntityFrameworkCore;

namespace test_app.Pages.Tests
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Test Test { get; set; }

        [BindProperty]
        public List<Question> Questions { get; set; } = new List<Question>();

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Cargar test y preguntas
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Test = await _context.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (Test == null)
            {
                TempData["Error"] = "Test no encontrado.";
                return NotFound();
            }

            Questions = Test.Questions.ToList();
            return Page();
        }

        // ✅ Guardar cambios con validación de campos vacíos
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Test.Title))
            {
                TempData["Error"] = "El título no puede estar vacío.";
                return Page();
            }

            if (Questions.Any(q => string.IsNullOrWhiteSpace(q.Text) || string.IsNullOrWhiteSpace(q.CorrectAnswer)))
            {
                TempData["Error"] = "Todas las preguntas y respuestas deben ser completadas.";
                return Page();
            }

            var testToUpdate = await _context.Tests
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == Test.Id);

            if (testToUpdate == null)
            {
                TempData["Error"] = "El test no fue encontrado.";
                return NotFound();
            }

            // ✅ Actualizar título del test
            testToUpdate.Title = Test.Title;

            // ✅ Actualizar preguntas y respuestas
            for (int i = 0; i < Questions.Count; i++)
            {
                var existingQuestion = testToUpdate.Questions.FirstOrDefault(q => q.Id == Questions[i].Id);
                if (existingQuestion != null)
                {
                    existingQuestion.Text = Questions[i].Text;
                    existingQuestion.CorrectAnswer = Questions[i].CorrectAnswer;
                }
                else
                {
                    testToUpdate.Questions.Add(new Question
                    {
                        Text = Questions[i].Text,
                        CorrectAnswer = Questions[i].CorrectAnswer,
                        TestId = testToUpdate.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "¡El test fue actualizado correctamente!";
            return RedirectToPage("/Index");
        }
    }
}
