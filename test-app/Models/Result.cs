using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test_app.Models
{
    public class Result
    {
        public int Id { get; set; }
          [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Test")]
        public int TestId { get; set; }

        [Required]
        public int Score { get; set; }
        public User User { get; set; }
        public Test Test { get; set; }
    }
}
