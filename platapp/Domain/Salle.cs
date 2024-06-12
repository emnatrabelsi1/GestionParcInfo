using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace platapp.Domain
{
    public class Salle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //type 1 salle tp
        //type 2 salle examen
        public bool Type { get; set; }
        public bool Deleted { get; set; }
        [JsonIgnore]
        public virtual Parc Parc { get; set; }
        // [ForeignKey("Conseiller")]
        public int? ParcFk { get; set; }
        [JsonIgnore]
        public virtual ICollection<Poste> Postes { get; set; }


    }
}
