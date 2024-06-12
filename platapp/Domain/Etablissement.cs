using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace platapp.Domain
{
    public class Etablissement
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Location { get; set; }
        
        public bool Deleted { get; set; }
        public virtual ICollection<Parc> Parcs{ get; set; }
    }
}
