namespace test_app.Models
{
  public class Test
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } // Correctamente definida la propiedad de navegación
}

}

