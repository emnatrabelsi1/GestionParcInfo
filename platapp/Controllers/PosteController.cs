using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;

namespace platapp.Controllers
{
   
        [ApiController]
        [Route("api/Poste")]
        public class PosteController : Controller
        {
            private readonly PContext pContext;
        private readonly ILogServiceAPI _logService;
        public PosteController(PContext pContext,ILogServiceAPI logService)
        {
            this.pContext = pContext;
            this._logService = logService;
        }


        [HttpGet]
            public IActionResult GetAllPoste()
            {
                return Ok(pContext.Poste.ToList());
            }


            [HttpPost]
            [Route("add")]
            public async Task<IActionResult> AddPoste(AddPosteRequest etab)
            {
                var Poste = new Poste()
                {
                   
                    Version = etab.Version,
                    SalleFk=null
                     

                };

                await pContext.Poste.AddAsync(Poste);
                await pContext.SaveChangesAsync();
            await _logService.CreateLog($"Création poste {Poste.Id}");
            return Ok(Poste);
            }
            [HttpPut]
            [Route("Update/{id:int}")]
            public async Task<IActionResult> UpdatePoste([FromRoute] int id, AddPosteRequest etab)
            {
                var Poste = pContext.Poste.Find(id);
                if (Poste != null)
                {
                    
                    await pContext.SaveChangesAsync();
                await _logService.CreateLog($"modification poste {Poste.Id}");
                return Ok(Poste);
                }
                return NotFound();


            }

            [HttpPut]
            [Route("Delete/{id:int}")]
            public async Task<IActionResult> DeletePoste([FromRoute] int id)
            {

            var parc = await pContext.Poste.FindAsync(id);

            if (parc != null)
            {
                // Mettre à jour la propriété Deleted à true
                parc.Deleted = true;

                // Mettre à jour l'entité dans la base de données
                pContext.Update(parc);
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"Suppression poste {parc.Id}");

                return Ok(parc);
            }

            return NotFound();
        }
        [HttpPut]
        [Route("AffecterSalle/{posteId:int}/{salleId:int}")]
        public async Task<IActionResult> AffecterPoste(int posteId, int salleId)
        {
            var poste = await pContext.Poste.FirstOrDefaultAsync(p => p.Id == posteId);
            var salle = await pContext.Salle.FirstOrDefaultAsync(e => e.Id == salleId);

            if (poste == null || salle == null)
            {
                return NotFound("Le parc ou l'établissement spécifié n'existe pas.");
            }
            // Affecter le parc à l'établissement
            poste.Salle = salle;
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"affectation poste {poste.Id} à la salle {salle.Id}");
            return Ok($"Le poste {posteId} a été affecté a la salle {salleId}.");
        }

      
       
    }
    }





