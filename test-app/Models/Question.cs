namespace test_app.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        // ✅ Agregada la propiedad CorrectAnswer
        public string CorrectAnswer { get; set; }

        // ✅ Relación con Test
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
