using System.Text.Json.Serialization;

namespace platapp.Domain
{
    public class AddLogRequest
    {
        public string LastActivity { get; set; }
        public DateTime LastActivityDate { get; set; }
        //[JsonIgnore]
        //public virtual Utilisateur Utilisateur { get; set; }

        //public int? UtilisateurFk { get; set; }
    }
}
