using System.ComponentModel.DataAnnotations;

namespace Transporter.web
{
    public class LoginClass
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
