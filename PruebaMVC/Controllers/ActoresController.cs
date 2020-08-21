using AutoMapper;
using Common.Data;
using Common.Dtos;
using Common.Entities;
using Common.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaMVC.Controllers
{
    public class ActoresController : Controller
    {
        #region Campos
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly string contenedor = "actores";// La carpeta donde se guarda la foto de actores
        #endregion

        //public ApplicationDbContext context { get; set; }

        #region Constructor
        public ActoresController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.almacenadorArchivos = almacenadorArchivos;
            this.context = context;
            this.mapper = mapper;
        } 
        #endregion

        // GET: Actores
        public async Task<IActionResult> Index()
        {
            var entidades = await context.Actores.ToListAsync();
            var dtos = mapper.Map<List<ActorDto>>(entidades);
            return View(dtos);
        }

        // GET: Actores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var entidad = await context.Actores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<ActorDto>(entidad);
            return View(dto);
        }

        // GET: Actores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorCreacionDto actorCreacionDTO)
        {
            var entidad = mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    entidad.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                        actorCreacionDTO.Foto.ContentType);
                }
            }  

            if (ModelState.IsValid)
            {
                context.Add(entidad);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actorCreacionDTO);
        }

        // GET: Actores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var entidad = await context.Actores.FindAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<ActorEdicionDto>(entidad);
            return View(dto);
        }

        // POST: Actores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActorEdicionDto actorEdicionDto)
        {
            var entidad = mapper.Map<Actor>(actorEdicionDto);

            if (id != actorEdicionDto.Id)
            {
                return NotFound();
            }

            if (actorEdicionDto.FotoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorEdicionDto.FotoFile.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorEdicionDto.FotoFile.FileName);
                    entidad.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                        actorEdicionDto.FotoFile.ContentType);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(entidad);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actorEdicionDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(actorEdicionDto);
        }

        // GET: Actores/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var entidad = await context.Actores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<ActorDto>(entidad);
            return View(dto);
        }

        // POST: Actores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entidad = await context.Actores.FindAsync(id);
            context.Actores.Remove(entidad);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return context.Actores.Any(e => e.Id == id);
        }
    }
}
