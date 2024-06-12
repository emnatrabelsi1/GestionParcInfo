using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;
using static platapp.Controllers.LogController;

namespace platapp.Controllers
{
    [ApiController]
    [Route("api/etablissement")]
    public class EtablissementController : Controller
    {
        private readonly PContext pContext;
        private readonly ILogServiceAPI _logService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EtablissementController(PContext pContext, ILogServiceAPI logService, IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            this.pContext = pContext;
            this._logService = logService;
            _authService = authService; 
            _httpContextAccessor = httpContextAccessor;
        }
            [HttpGet]
        public IActionResult GetAllEtablissement()
        {
            return Ok(pContext.Etablissement.ToList());
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEtablissement(AddEtablissementRequest etab)
        {
            var user = await _authService.GetAuthenticatedUser();
            var etablissement = new Etablissement()
            {
                Nom = etab.Nom,
                Location = etab.Location,
                Parcs = null
                
                
            };

            await pContext.Etablissement.AddAsync(etablissement);
            await pContext.SaveChangesAsync();
            await _logService.CreateLog($"Création établissement {etablissement.Id}");
            return Ok(etablissement);
        }
        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateEtablissement([FromRoute] int id, AddEtablissementRequest etab)
        {

            var user = await _authService.GetAuthenticatedUser();
            var etablissement=  pContext.Etablissement.Find(id);
            if(etablissement!=null)
            {
            etablissement.Location= etab.Location;
                etablissement.Nom= etab.Nom;
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"Modification établissement {etablissement.Id}");
                return Ok(etablissement);
            }
            return NotFound();


        }

        [HttpPut]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult>  DeleteEtablissement([FromRoute] int id)
        {

           // var user = await _authService.GetAuthenticatedUser();
            var parc = await pContext.Etablissement.FindAsync(id);

            if (parc != null)
            {
                // Mettre à jour la propriété Deleted à true
                parc.Deleted = true;

                // Mettre à jour l'entité dans la base de données
                pContext.Update(parc);
                await pContext.SaveChangesAsync();
                await _logService.CreateLog($"Suppression établissement {parc.Id}");

                return Ok(parc);
            }

            return NotFound();
        }
        [HttpGet]
        [Route("GetParcsDeEtab/{id}")]
        public async Task<ActionResult<IEnumerable<Parc>>> GetParcsDeEtab(int id)
        {
            var parc = await pContext.Etablissement
                .Include(p => p.Parcs) // Assurez-vous que les salles sont chargées avec le parc
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parc == null)
            {
                return NotFound();
            }

            return Ok(parc.Parcs);
        }




    } }
            



