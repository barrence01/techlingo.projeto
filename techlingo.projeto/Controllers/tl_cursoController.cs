using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using techlingo.projeto.Models;
using techlingo.projeto.Repository;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Controllers
{
    [Route("api/Cursos")]
    [ApiController]
    public class tl_cursoController : ControllerBase
    {


        private readonly tl_cursoRepository cursoRepository;

        public tl_cursoController(DataBaseContext context)
        {
            cursoRepository = new tl_cursoRepository(context);
        }

        // GET-ALL
        [HttpGet]
        public ActionResult<List<tl_cursoModel>> Get()
        {
            try
            {
                var lista = cursoRepository.Listar();

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
        public ActionResult<tl_cursoModel> Get([FromRoute] int id)
        {
            try
            {
                var planoModel = cursoRepository.Consultar(id);

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
        public ActionResult<tl_cursoModel> Post([FromBody] tl_cursoModel cursoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                cursoRepository.Inserir(cursoModel);
                var location = new Uri(Request.GetEncodedUrl() + "/" + cursoModel.id_curso);
                return Created(location, cursoModel);
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível criar o Curso. Detalhes: {error.Message}" });
            }
        }

        // UPDATE PLANO
        [HttpPut("{id:int}")]
        public ActionResult<tl_cursoModel> Put([FromRoute] int id, [FromBody] tl_cursoModel cursoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cursoModel.id_curso != id)
            {
                return NotFound();
            }


            try
            {
                cursoRepository.Alterar(cursoModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar o Curso. Detalhes: {error.Message}" });
            }
        }

        // DELETE PLANO
        [HttpDelete("{id:int}")]
        public ActionResult<tl_cursoModel> Delete([FromRoute] int id)
        {
            try
            {
                Console.WriteLine("Iniciando delete");
                var cursoModel = cursoRepository.Consultar(id);

                if (cursoModel != null)
                {
                    Console.WriteLine("Representante encontrado - não nulo");
                    cursoRepository.Excluir(id);
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
