using Microsoft.AspNetCore.Mvc;
using techlingo.projeto.Repository.Context;
using techlingo.projeto.Repository;
using techlingo.projeto.Models.Planos;
using techlingo.projeto.Controllers.DTO.Plano;
using System.Collections;
using techlingo.projeto.Controllers.DTO.Plano.Response;
using Microsoft.AspNetCore.Cors;

namespace techlingo.projeto.Controllers
{
    [Route("api/Plano")]
    [ApiController]
    [EnableCors]
    public class PlanoController : ControllerBase
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly PlanoRepository planoRepository;

        public PlanoController(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
            planoRepository = new PlanoRepository(ctx);
        }

        [HttpGet("ListarPlanos")]
        public IActionResult ListarPlanos()
        {
            return Ok(planoRepository.listarPlanosNT());
        }

        [HttpGet("ListaDePlanos")]
        public IActionResult ListaDePlanos()
        {
            var lista = planoRepository.listaDePlanosNT();

            List<string> listaResultado = new List<string>();

            foreach (var item in lista)
            {
                string plano = item.Key + " - R$" + ((int?)item.Value);
                listaResultado.Add(plano);
            }

            ListarPlanosResponseDTO listaDePlanosResponseDTO = new ListarPlanosResponseDTO();
            listaDePlanosResponseDTO.planos = listaResultado;

            return Ok(listaDePlanosResponseDTO);
        }

        [HttpPost("AdicionarPlano")]
        public IActionResult AdicionarPlano(PlanoRequestDTO novoPlanoRequest)
        {
            PlanoModel novoPlano = new PlanoModel(novoPlanoRequest);
            dataBaseContext.Planos.Add(novoPlano);
            try
            {
                dataBaseContext.SaveChanges();
                return Ok();
            } catch
            {
                return BadRequest();
            }

        }



    }
}
