using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapiautores.Entities;

namespace webapiautores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController:ControllerBase

    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext applicationDbContext)
        {
            this.context=applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Libro>>>Get()
        {
            // return new List<Autor>(){
            //     new Autor(){
            //         Id=1,Nombre="juan"
            //     },
            //     new Autor(){
            //         Id=2,Nombre="maria"
            //     }
            // };
            return await context.Libros.ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>>GetlIBROS(int id)
        {
            // return new List<Autor>(){
            //     new Autor(){
            //         Id=1,Nombre="juan"
            //     },
            //     new Autor(){
            //         Id=2,Nombre="maria"
            //     }
            // };
            return await context.Libros.Include(x=>x.Autor).FirstOrDefaultAsync(x=>x.Id==id);

        }
        
        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor=await context.Autores.AnyAsync(x=> x.Id== libro.AutorId);
            if (!existeAutor)
            {
                return BadRequest($"No existe el autor con ese Id{libro.AutorId}");
            }
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor,int id)
        {
             autor.Id = id;


            context.Entry(autor).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!autorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existe=await context.Autores.AnyAsync(x => x.Id==id);

            if (!existe)
            {
                return NotFound("El id del autor no coincide");
            }
            context.Remove(new Autor() {Id=id});
            await context.SaveChangesAsync();
            return Ok();
        }


         private bool autorExists(int id)
        {
            return context.Autores.Any(e => e.Id == id);
        }
    }
}