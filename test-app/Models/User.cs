namespace test_app.Models
{
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    // ✅ Relación con Tests y Resultados
        public List<Test> Tests { get; set; } = new List<Test>();
        public List<Result> Results { get; set; } = new List<Result>();
}

}

