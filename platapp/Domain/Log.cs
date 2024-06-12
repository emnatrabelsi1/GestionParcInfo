using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace platapp.Domain
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
            public bool Deleted { get; set; }
        public string LastActivity { get; set; }
        public DateTime LastActivityDate { get; set; }
        [JsonIgnore]
        public virtual Utilisateur Utilisateur { get; set; }
        
        public int? UtilisateurFk { get; set; }

        [JsonIgnore]
        public virtual Poste? Poste { get; set; }
        
        public int? PosteFk { get; set; }

    }
}
