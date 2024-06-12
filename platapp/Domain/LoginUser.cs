using System.ComponentModel.DataAnnotations;

namespace platapp.Domain
{
    public class LoginUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
        public string username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]

        public string Passwd { get; set; }
    }
}
