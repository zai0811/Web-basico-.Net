namespace test_app.Models
{
  public class Test
  {
    public int Id { get; set; }
    public string Title { get; set; }

    // ✅ Relación con User (Clave Foránea)
    public int UserId { get; set; }
    public User User { get; set; }

    // ✅ Nueva Relación con Questions (Lista de Preguntas)
    // ✅ Relación con Preguntas y Resultados
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<Result> Results { get; set; } = new List<Result>();
  }

}

