using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapiautores.Entities;
using webapiautores.services;

namespace webapiautores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController:ControllerBase

    {
        private readonly ApplicationDbContext context;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ServicioScope servicioScope;
        private readonly IService servicio;
        private readonly ServicioTransient servicioTransient;

        private readonly ILogger<AutoresController>logger;

        public AutoresController(ApplicationDbContext applicationDbContext,ServicioTransient servicioTransient,
        ServicioScope servicioScope,ServicioSingleton servicioSingleton,IService servicio,ILogger<AutoresController>logger)
        {
            this.context=applicationDbContext;
            this.servicio=servicio;
            this.servicioTransient=servicioTransient;
            this.servicioScope=servicioScope;
            this.servicioSingleton=servicioSingleton;
            this.logger=logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>>Get()
        {
            logger.LogInformation("Estamos obteniendo los autores");
            // return new List<Autor>(){
            //     new Autor(){
            //         Id=1,Nombre="juan"
            //     },
            //     new Autor(){
            //         Id=2,Nombre="maria"
            //     }
            // };
            return await context.Autores.Include(x=>x.Libros).ToListAsync();

        }
        [HttpGet("guid")]
        public ActionResult obtenerGuid()
        {
            return Ok(new {
                AutoresControllerTRansient=servicioTransient.Guid,
                AutoresControllersCOPED=servicioScope.Guid,
                AutoresControllerSingleton=servicioSingleton.Guid,
                ServicioA_Transient=servicio.ObtenerTransient(),
                ServicioA_Scopet=servicio.ObtenerScope(),
                ServicioA_sINGLETON=servicio.ObtenerSingleton(),

            });
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Autor autor)

        {
            var yaExisteAutor=await context.Autores.AnyAsync(x=>x.Nombre==autor.Nombre);
            if(yaExisteAutor){
                return BadRequest($" Ya existe un autor con el nombre {autor.Nombre}");
            }
            context.Add(autor);
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