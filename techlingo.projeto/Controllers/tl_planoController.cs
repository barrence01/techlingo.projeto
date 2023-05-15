using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using techlingo.projeto.Models;
using techlingo.projeto.Repository;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Controllers
{
    [Route("api/Planos")]
    [ApiController]
    public class tl_planoController : ControllerBase
    {

        private readonly tl_planoRepository planoRepository;

        public tl_planoController(DataBaseContext context)
        {
            planoRepository = new tl_planoRepository(context);
        }

        // GET-ALL
        [HttpGet]
        public ActionResult<List<tl_planoModel>> Get()
        {
            try
            {
                var lista = planoRepository.Listar();

                if (lista != null)
                {
                    return Ok(lista);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET BY ID
        [HttpGet("{id:int}")]
        public ActionResult<tl_planoModel> Get([FromRoute] int id)
        {
            try
            {
                var planoModel = planoRepository.Consultar(id);

                if (planoModel != null)
                {
                    return Ok(planoModel);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // ADD PLANO
        [HttpPost]
        public ActionResult<tl_alunoModel> Post([FromBody] tl_planoModel planoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                planoRepository.Inserir(planoModel);
                var location = new Uri(Request.GetEncodedUrl() + "/" + planoModel.id_plano);
                return Created(location, planoModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível criar o Plano. Detalhes: {error.Message}" });
            }
        }

        // UPDATE PLANO
        [HttpPut("{id:int}")]
        public ActionResult<tl_planoModel> Put([FromRoute] int id, [FromBody] tl_planoModel planoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (planoModel.id_plano != id)
            {
                return NotFound();
            }


            try
            {
                planoRepository.Alterar(planoModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar Representante. Detalhes: {error.Message}" });
            }
        }

        // DELETE PLANO
        [HttpDelete("{id:int}")]
        public ActionResult<tl_planoModel> Delete([FromRoute] int id)
        {
            try
            {
                Console.WriteLine("Iniciando delete");
                var planoModel = planoRepository.Consultar(id);

                if (planoModel != null)
                {
                    Console.WriteLine("Representante encontrado - não nulo");
                    planoRepository.Excluir(id);
                    // Retorno Sucesso.
                    // Efetuou a exclusão, porém sem necessidade de informar os dados.
                    return NoContent();
                }
                else
                {
                    Console.WriteLine("Representante não encontrado - nulo");
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível deletar, possível problemas comuns: A instancia está monitorando este objeto - use UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) em Program.cs");
                return BadRequest();
            }
        }

    }
}
