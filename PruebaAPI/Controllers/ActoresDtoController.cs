using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Data;
using Common.Entities;
using AutoMapper;
using Common.Servicios;
using Common.Dtos;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresDtoController : ControllerBase
    {
        #region Campos
        private IAlmacenadorArchivos almacenadorArchivos;
        private IMapper mapper;
        private readonly ApplicationDbContext _context;
        private readonly string contenedor = "actores";// La carpeta donde se guarda la foto de actores
        #endregion

        #region Propiedades
        public IMapper Mapper
        {
            get { return mapper; }
            set { mapper = value; }
        }
        public IAlmacenadorArchivos AlmacenadorArchivos
        {
            get { return almacenadorArchivos; }
            set { almacenadorArchivos = value; }
        }
        #endregion
        

        public ActoresDtoController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.almacenadorArchivos = almacenadorArchivos;
            this._context = context;
            this.mapper = mapper;
        }

        // GET: api/ActoresDto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActores()
        {
            var entidades = await _context.Actores.ToListAsync();
            var dtos = mapper.Map<List<ActorDto>>(entidades);
            return dtos;
        }

        // GET: api/ActoresDto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _context.Actores.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<ActorDto>(actor);

            return dto;
        }

        // PUT: api/ActoresDto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ActoresDto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            _context.Actores.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/ActoresDto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Actor>> DeleteActor(int id)
        {
            var actor = await _context.Actores.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actores.Remove(actor);
            await _context.SaveChangesAsync();

            return actor;
        }

        private bool ActorExists(int id)
        {
            return _context.Actores.Any(e => e.Id == id);
        }
    }
}
