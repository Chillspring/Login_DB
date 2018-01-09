using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_DB.Models
{
    public class Player
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        [Index(IsUnique = true)]
        [StringLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
    }
}