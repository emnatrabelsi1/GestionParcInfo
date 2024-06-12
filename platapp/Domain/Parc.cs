using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace platapp.Domain
{
    public class Parc
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Deleted { get; set; }

        [JsonIgnore]
        public virtual Etablissement? Etablissement { get; set; }
        // [ForeignKey("Conseiller")]
        public int? EtablissementFk { get; set; }
        [JsonIgnore]
        public virtual ICollection<Salle>? Salles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Utilisateur>? Utilisateurs { get; set; }

    }
}
