using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;

namespace platapp.Controllers
{
    [ApiController]
    [Route("api/Parc")]
    public class ParcController : Controller
    {

        private readonly PContext pContext;
        private readonly ILogServiceAPI _logService;
        public ParcController(PContext pContext, ILogServiceAPI logService)
        {
            this.pContext = pContext;
            this._logService = logService;
        }
       

        [HttpGet]
        public IActionResult GetAllParc()
        {
            return Ok(pContext.Parc.ToList());
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddParc(AddParcRequest etab)
        {
            var Parc = new Parc()
            {

                 
                 EtablissementFk=null



            };

            await pContext.Parc.AddAsync(Parc);
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"Création parc {Parc.Id}");
            return Ok(Parc);
        }
        [HttpPost]
        [Route("addParcToEtab/{idetab:int}")]
        public async Task<IActionResult> AddParcEtab(Parc etab,int idetab)
        {
            var Parc = new Parc()
            {


                EtablissementFk = idetab,
                Salles=null,
                Utilisateurs=null



            };

            await pContext.Parc.AddAsync(Parc);
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"Création parc {Parc.Id} associé à l'établissement  {idetab}");
            return Ok(Parc);
        }


        [HttpPut]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteParc([FromRoute] int id)
        {
            var parc = await pContext.Parc.FindAsync(id);

            if (parc != null)
            {
                // Mettre à jour la propriété Deleted à true
                parc.Deleted = true;

                // Mettre à jour l'entité dans la base de données
                pContext.Update(parc);
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"Suppression parc {parc.Id} ");
                return Ok(parc);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("AffecterEtablissement/{parcId:int}/{etablissementId:int}")]
        public async Task<IActionResult> AffecterEtablissement(int parcId, int etablissementId)
        {
            var parc = await pContext.Parc.FirstOrDefaultAsync(p => p.Id == parcId);
            var etablissement = await pContext.Etablissement.FirstOrDefaultAsync(e => e.Id == etablissementId);

            if (parc == null || etablissement == null)
            {
                return NotFound("Le parc ou l'établissement spécifié n'existe pas.");
            }

            // Affecter le parc à l'établissement
            parc.Etablissement = etablissement;
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"affectation parc {parc.Id}  à l'établissement  {etablissementId}");
            return Ok($"Le parc {parcId} a été affecté à l'établissement {etablissementId}.");
        }

        [HttpGet]
        [Route("GetSallesDuParc/{id}")]
        public async Task<ActionResult<IEnumerable<Salle>>> GetSallesDuParc(int id)
        {
            var parc = await pContext.Parc
                .Include(p => p.Salles) // Assurez-vous que les salles sont chargées avec le parc
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parc == null)
            {
                return NotFound();
            }

            return Ok(parc.Salles);
        }

        [HttpGet]
        [Route("GetUsersDuParc/{id}")]
        public async Task<ActionResult<IEnumerable<Salle>>> GetUsersDuParc(int id)
        {
            var parc = await pContext.Parc
                .Include(p => p.Utilisateurs) // Assurez-vous que les salles sont chargées avec le parc
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parc == null)
            {
                return NotFound();
            }

            return Ok(parc.Utilisateurs);
        }
    }
}
