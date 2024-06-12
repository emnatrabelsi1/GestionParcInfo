using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;

namespace platapp.Controllers
{
    [ApiController]
    [Route("api/Utilisateur")]
    public class UtilisateurController : Controller
    {
        private readonly PContext pContext;
        private readonly ILogServiceAPI _logService;
        public UtilisateurController(PContext pContext,ILogServiceAPI logService)
        {
            this.pContext = pContext;
            this._logService = logService;
        }
        
        [HttpGet]
        public IActionResult GetAllUtilisateur()
        {
            return Ok(pContext.Utilisateur.ToList());
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUtilisateur(AddUtilisateurRequest etab)
        {
            var Utilisateur = new Utilisateur()
            {
                Nom = etab.Nom,
                Prenom = etab.Prenom,
                username=etab.username,
                Passwd=etab.Passwd,
                Email=etab.Email

            };

            await pContext.Utilisateur.AddAsync(Utilisateur);
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"création utilisateur {Utilisateur.Id} ");
            return Ok(Utilisateur);
        }
        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateUtilisateur([FromRoute] int id, AddUtilisateurRequest etab)
        {
            var Utilisateur = pContext.Utilisateur.Find(id);
            if (Utilisateur != null)
            {
                Utilisateur.Prenom = etab.Prenom;
                Utilisateur.Nom = etab.Nom;
                Utilisateur.Email = etab.Email;
                Utilisateur.username=etab.username;
                Utilisateur.Passwd= etab.Passwd;
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"modification utilisateur {Utilisateur.Id} ");
                return Ok(Utilisateur);
            }
            return NotFound();


        }




        [HttpGet]
        [Route("GetUtilisateurById/{id:int}")]
        public async Task<IActionResult> GetUtilisateurById(int id)
        {
             var utilisateur = await pContext.Utilisateur.FindAsync(id);
    if (utilisateur != null)
    {
                var utilisateurRequest = new AddUtilisateurRequest
                {
                    Nom = utilisateur.Nom,
                    Prenom = utilisateur.Prenom,
                    Type = utilisateur.Type
                };
                return Ok(utilisateurRequest);
            }
    return NotFound();
        }


        [HttpPut]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteUtilisateur([FromRoute] int id)
        {

            var parc = await pContext.Utilisateur.FindAsync(id);

            if (parc != null)
            {
                // Mettre à jour la propriété Deleted à true
                parc.Deleted = true;

                // Mettre à jour l'entité dans la base de données
                pContext.Update(parc);
                await _logService.CreateLog($"suppression utilisateur {parc.Id} ");
                await pContext.SaveChangesAsync();

                return Ok(parc);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("AffecterParc/{parcId:int}/{utId:int}")]
        public async Task<IActionResult> AffecterParc(int parcId, int utId)
        {
            var utilisateur = await pContext.Utilisateur.FirstOrDefaultAsync(p => p.Id == utId);
            var parc = await pContext.Parc.FirstOrDefaultAsync(e => e.Id == parcId);

            if (utilisateur == null || utilisateur.Type==true || parc == null)
            {
                return NotFound("Le parc ou l'établissement spécifié n'existe pas// Cet Utilisateur est un étudiant ou enseignant et ne peut pas avoir des parcs affectés. ");
            }
           
            utilisateur.Parcs.Add(parc);
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"affectation utilisateur {utilisateur.Id} au parc {parc.Id}");
            return Ok($"L'utilisateur {utId} a été affecté au parc {parcId}.");
        }




    }
}




