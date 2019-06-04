using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiContribuinte.Models;

namespace ApiContribuinte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CnaesController : ControllerBase
    {
        private readonly ApiContribuinteContext _context;

        public CnaesController(ApiContribuinteContext context)
        {
            _context = context;
        }

        // GET: api/Cnaes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cnae>>> GetCnae()
        {
            return await _context.Cnae.ToListAsync();
        }

        // GET: api/Cnaes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cnae>> GetCnae(int id)
        {
            var cnae = await _context.Cnae.FindAsync(id);

            if (cnae == null)
            {
                return NotFound();
            }

            return cnae;
        }

    }
}
