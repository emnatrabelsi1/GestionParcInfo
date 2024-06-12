using Microsoft.AspNetCore.Mvc;

namespace WebAppli.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementController : ControllerBase
    {
        private readonly PContext _context;

        public EtablissementController(PContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("webappli")]
        public async Task<ActionResult<List<Etablissement>>> GetEtablissementes()
        {
            var heroes = await _context.Etablissement.ToListAsync();
            return Ok(heroes);
        }

       /* [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _context.Comics.ToListAsync();
            return Ok(comics);
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Etablissement>> GetSingleEtablissement(int id)
        {
            var hero = await _context.Etablissement.FindAsync(id);
            if (hero == null)
            {
                return NotFound("Sorry, no hero here. :/");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Etablissement>>> CreateEtablissement(Etablissement hero)
        {
            
            hero.Parcs = null;
            _context.Etablissement.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(hero);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Etablissement>>> UpdateEtablissement(Etablissement etab, int id)
        {
            var dbEtab = await _context.Etablissement.FindAsync(id);
            if (dbEtab == null)
                return NotFound("Sorry, but no hero for you. :/");

            dbEtab.Location = etab.Location;
            dbEtab.Nom = etab.Nom;
            

            await _context.SaveChangesAsync();

            return Ok(etab);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Etablissement>>> DeleteEtablissement(int id)
        {
            var dbHero = await _context.Etablissement.FindAsync(id);

            if (dbHero == null)
                return NotFound("Sorry, but no hero for you. :/");

            _context.Etablissement.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(dbHero);
        }

      /*  private async Task<List<Etablissement>> GetDbHeroes()
        {
            return await _context.Etablissement.Include(sh => sh.Comic).ToListAsync();
        }*/
    }
}