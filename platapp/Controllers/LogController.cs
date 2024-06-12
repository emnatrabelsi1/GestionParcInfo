using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;

namespace platapp.Controllers
{
    [ApiController]
    [Route("api/Log")]
    public class LogController : Controller
    {
        private readonly PContext pContext;
        private readonly ILogServiceAPI _logService;


        public LogController(PContext pContext, ILogServiceAPI logService)
        {
            this.pContext = pContext;
            this._logService = logService;

        }

        [HttpGet]
        public IActionResult GetAllLog()
        {
            try
            {
                // Récupérer tous les logs de la base de données
                var logs = pContext.Log.ToList();

                // Vérifier les valeurs NULL et les remplacer par des valeurs par défaut si nécessaire
                foreach (var log in logs)
                {
                    if (log.LastActivity == null)
                    {
                        log.LastActivity = "N/A"; // Remplacer par une valeur par défaut
                    }

                    if (log.LastActivityDate == null)
                    {
                        log.LastActivityDate = DateTime.MinValue; // Remplacer par une valeur par défaut
                    }
                }

                return Ok(logs);
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici
                Console.WriteLine($"Une exception s'est produite lors de la récupération des logs : {ex.Message}");
                return StatusCode(500, "Une erreur s'est produite lors de la récupération des logs.");
            }
        }




        [HttpPost]
        [Route("CreateLog")]
        public async Task<IActionResult> AddLog([FromBody] AddLogRequest request)
        {
            await _logService.CreateLog(request.LastActivity);
            return Ok();
        }

       

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateLog([FromRoute] int id, AddLogRequest etab)
        {
            var Log = pContext.Log.Find(id);
            if (Log != null)
            {
                Log.LastActivity = etab.LastActivity;
                Log.LastActivityDate = etab.LastActivityDate;
                await pContext.SaveChangesAsync();
                return Ok(Log);
            }
            return NotFound();


        }

        [HttpPut]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteLog([FromRoute] int id)
        {

            var parc = await pContext.Log.FindAsync(id);

            if (parc != null)
            {
                // Mettre à jour la propriété Deleted à true
                parc.Deleted = true;

                // Mettre à jour l'entité dans la base de données
                pContext.Update(parc);
                await pContext.SaveChangesAsync();

                return Ok(parc);
            }

            return NotFound();

        }

        [HttpPut]
        [Route("AffecterUtilisateur/{logId:int}/{utilisateurId:int}")]
        public async Task<IActionResult> AffecterUtilisateur(int logId, int utilisateurId)
        {
            var log = await pContext.Log.FirstOrDefaultAsync(p => p.Id == logId);
            var utilisateur = await pContext.Utilisateur.FirstOrDefaultAsync(e => e.Id == utilisateurId);

            if (log == null || utilisateur == null)
            {
                return NotFound("Le parc ou l'établissement spécifié n'existe pas.");
            }
            // Affecter le parc à l'établissement
            log.Utilisateur = utilisateur;
            await pContext.SaveChangesAsync();

            return Ok($"Le Log {logId} a été affecté à l'utilisateur {utilisateurId}.");
        }
        [HttpPut]
        [Route("AffecterPoste/{logId:int}/{posteId:int}")]
        public async Task<IActionResult> AffecterPoste(int logId, int posteId)
        {
            var log = await pContext.Log.FirstOrDefaultAsync(p => p.Id == logId);
            var poste = await pContext.Poste.FirstOrDefaultAsync(e => e.Id == posteId);

            if (log == null || poste == null)
            {
                return NotFound("Le parc ou l'établissement spécifié n'existe pas.");
            }
            // Affecter le parc à l'établissement
            log.Poste = poste;
            await pContext.SaveChangesAsync();

            return Ok($"Le Log {logId} a été affecté au poste {posteId}.");
        }




    }
}




