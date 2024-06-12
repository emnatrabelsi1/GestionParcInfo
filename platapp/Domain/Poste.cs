using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace platapp.Domain
{
    public class Poste
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Version { get; set; }
      
        public bool Deleted { get; set; }
        [JsonIgnore]
        public virtual Salle Salle { get; set; }
        // [ForeignKey("Conseiller")]
        public int? SalleFk { get; set; }

        //        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Log> Logs { get; set; }

    }
}
