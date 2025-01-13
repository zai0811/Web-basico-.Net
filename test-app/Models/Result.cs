namespace test_app.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public Test Test { get; set; }
    }
}
