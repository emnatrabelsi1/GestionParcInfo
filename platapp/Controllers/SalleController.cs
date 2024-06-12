using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;

namespace platapp.Controllers
{
    [ApiController]
    [Route("api/Salle")]
    public class SalleController : Controller
    {
        private readonly PContext pContext;
        private readonly ILogServiceAPI _logService;
        public SalleController(PContext pContext,ILogServiceAPI logService)
        {
            this.pContext = pContext;
            this._logService = logService;
        }
        public SalleController(PContext pContext)
        {
            this.pContext = pContext;
        }

        [HttpGet]
        public IActionResult GetAllSalle()
        {
            return Ok(pContext.Salle.ToList());
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddSalle(AddSalleRequest etab)
        {
            var Salle = new Salle()
            {
                Type = etab.Type,
                ParcFk=null

            };

            await pContext.Salle.AddAsync(Salle);
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"création salle {Salle.Id} ");
            return Ok(Salle);
        }
        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateSalle([FromRoute] int id, AddSalleRequest etab)
        {
            var Salle = pContext.Salle.Find(id);
            if (Salle != null)
            {
                Salle.Type = etab.Type;
               
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"modification salle {Salle.Id} ");
                return Ok(Salle);
            }
            return NotFound();


        }

        [HttpPut]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteSalle([FromRoute] int id)
        {

            var parc = await pContext.Salle.FindAsync(id);

            if (parc != null)
            {
                // Mettre à jour la propriété Deleted à true
                parc.Deleted = true;

                // Mettre à jour l'entité dans la base de données
                pContext.Update(parc);
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"suppression salle {parc.Id} ");

                return Ok(parc);
            }

            return NotFound();
        }
        [HttpPut]
        [Route("AffecterParc/{SalleId:int}/{ParcId:int}")]
        public async Task<IActionResult> AffecterParc(int SalleId, int ParcId)
        {
            var salle = await pContext.Salle.FirstOrDefaultAsync(p => p.Id == SalleId);
            var parc = await pContext.Parc.FirstOrDefaultAsync(e => e.Id == ParcId);

            if (parc == null || salle == null)
            {
                return NotFound("Le parc ou l'établissement spécifié n'existe pas.");
            }

            // Affecter le parc à l'établissement
            //parc.Etablissement = etablissement;
            salle.Parc = parc;
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"affectation de la salle {salle.Id} au parc {parc.Id}");
            return Ok($"La salle {SalleId} a été affecté au parc {ParcId}.");
        }




    }
}
