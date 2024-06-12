using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace platapp.Domain
{
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string username { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        //Type true student
        // type false admin
        public bool Type { get; set; }
        
        public bool Deleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<Parc> Parcs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Log> Logs { get; set; }
        [JsonIgnore]
        
        public string Passwd { get; set; }



    }
}
