using System.ComponentModel.DataAnnotations;

namespace platapp.Domain
{
    public class AddUtilisateurRequest
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public bool Type { get; set; }
        public string username { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Passwd { get; set; }
    }
}
