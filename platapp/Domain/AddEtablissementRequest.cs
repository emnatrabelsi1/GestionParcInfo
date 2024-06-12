using System.ComponentModel.DataAnnotations;

namespace platapp.Domain
{
    public class AddEtablissementRequest
    {
        public string Nom { get; set; }

        
        public string Location { get; set; }
    }
}
