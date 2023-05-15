using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using techlingo.projeto.DTO;
using techlingo.projeto.Models;
using techlingo.projeto.Repository;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Controllers
{
    [Route("api/Aluno")]
    [ApiController]
    public class tl_alunoController : ControllerBase
    {

        private readonly tl_alunoRepository alunoRepository;
        private readonly tl_alunoCursoRepository alunoCursoRepository;
        private readonly tl_cursoRepository cursoRepository;
        private readonly tl_planoRepository planoRepository;

        public tl_alunoController(DataBaseContext context)
        {
            alunoRepository = new tl_alunoRepository(context);
            alunoCursoRepository = new tl_alunoCursoRepository(context);
            cursoRepository = new tl_cursoRepository(context);
            planoRepository = new tl_planoRepository(context);
            
        }


        [HttpGet]
        public ActionResult<List<tl_alunoModel>> Get()
        {
            try
            {
                var lista = alunoRepository.Listar();

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


        [HttpGet("{id:int}")]
        public ActionResult<tl_alunoModel> Get([FromRoute] int id)
        {
            try
            {
                var alunoModel = alunoRepository.Consultar(id);

                if (alunoModel != null)
                {
                    return Ok(alunoModel);
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

        [HttpPost]
        public ActionResult<tl_alunoModel> Post([FromBody] CriarAlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alunoModel = new tl_alunoModel();
                alunoModel.nm_aluno = alunoDto.nm_aluno;
                alunoModel.nr_cpf = alunoDto.nr_cpf;
                alunoModel.ds_email = alunoDto.ds_email;
                alunoModel.dt_nascimento = alunoDto.dt_nascimento;
                alunoModel.ds_senha = alunoDto.ds_senha;

            var planoModel = planoRepository.Consultar(1);

            // Automaticamente define o plano 1 como padrão para contas de aluno criadas
            if(planoModel == null)
            {
                var plano = new tl_planoModel();
                plano.id_plano = 1;
                plano.nm_plano = "gratuito";
                plano.vl_plano = 0;

                alunoModel.plano = plano;
            } else
            {
                alunoModel.planoId = 1;
            }


                alunoRepository.Inserir(alunoModel);
                var location = new Uri(Request.GetEncodedUrl() + "/" + alunoModel.id_aluno);
                return Created(location, alunoModel);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<tl_alunoModel> Delete([FromRoute] int id)
        {
            try
            {
                Console.WriteLine("Iniciando delete");
                var alunoModel = alunoRepository.Consultar(id);

                if (alunoModel != null)
                {
                    Console.WriteLine("Representante encontrado - não nulo");
                    alunoRepository.Excluir(id);
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

        [HttpPut("{id:int}")]
        public ActionResult<tl_alunoModel> Put([FromRoute] int id, [FromBody] AlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (alunoDto.id_aluno != id)
            {
                return NotFound();
            }

            var alunoModel = alunoRepository.Consultar(alunoDto.id_aluno);
            if (alunoModel == null)
            {
                return NotFound();
            }
/*            if (alunoModel.ds_senha != alunoDto.ds_senha)
            {
                Console.WriteLine("Senha incorreta");
                return NotFound();
            }*/


                alunoModel.id_aluno = alunoDto.id_aluno;
                alunoModel.nm_aluno = alunoDto.nm_aluno;
                alunoModel.nr_cpf = alunoDto.nr_cpf;
                alunoModel.ds_email = alunoDto.ds_email;
                alunoModel.dt_nascimento = alunoDto.dt_nascimento;
               


            try
            {
                alunoRepository.Alterar(alunoModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar os dados do Aluno. Detalhes: {error.Message}" });
            }
        }

        [HttpPut("AlterarAlunoCurso/{id_aluno:int}/{id_curso:int}")]
        public ActionResult<tl_aluno_cursoModel> Put([FromBody] AlterarAlunoCursoDto cursoAlunoDto, [FromRoute] int id_aluno, [FromRoute] int id_curso)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cursoAlunoDto.id_aluno != id_aluno || cursoAlunoDto.id_curso != id_curso)
            {
                return NotFound();
            }

            var alunoCursoModel = alunoCursoRepository.Consultar(cursoAlunoDto.id_aluno, cursoAlunoDto.id_curso);

            if (alunoCursoModel == null) {
                return NotFound();
            }

            try
            {
                alunoCursoRepository.Alterar(cursoAlunoDto, alunoCursoModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar os dados do Aluno. Detalhes: {error.Message}" });
            }

        }

        [HttpGet("ConsultarAlunoCurso/{id_aluno:int}/{id_curso:int}")]
        public ActionResult<tl_aluno_cursoModel> Get([FromRoute] int id_aluno, [FromRoute] int id_curso)
        {

            var alunoCursoModel = alunoCursoRepository.Consultar(id_aluno, id_curso);

                if (alunoCursoModel != null)
                {
                    return Ok(alunoCursoModel);
                }
                else
                {
                    return NotFound();
                }
        }

        [HttpPost("AdicionarCursoAluno")]
        public ActionResult<tl_alunoModel> Post([FromBody] AdicionarCursoAlunoDto pedido)
        {

            var aluno = alunoRepository.Consultar(pedido.id_aluno);
            if (aluno == null)
            {
                return NotFound();
            }

            var curso = cursoRepository.Consultar(pedido.id_curso);
            if (curso == null)
            {
                return NotFound();
            }

            Console.WriteLine(aluno.nm_aluno);
            Console.WriteLine(curso.nm_curso);

            if (aluno.Cursando == null)
            {
                aluno.Cursando = new List<tl_aluno_cursoModel>();
            }
            tl_aluno_cursoModel aluno_curso = new tl_aluno_cursoModel();
            //aluno_curso.alunoId = pedido.id_aluno;
            aluno_curso.cursoId = curso.id_curso;

            aluno.Cursando.Clear(); // Este clear limpará a lista para não gerar duplicado na tabela
            aluno.Cursando.Add(aluno_curso);
            alunoRepository.Alterar(aluno);

            return aluno;

        }

        [HttpPut("AdicionarPlanoAluno")]
        public ActionResult<tl_alunoModel> Put([FromBody] AdicionarPlanoAlunoDto planoAluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alunoModel = alunoRepository.Consultar(planoAluno.id_aluno);

            if (alunoModel == null)
            {
                return NotFound();
            }

            var plano = planoRepository.Consultar(planoAluno.id_plano);

            if (plano == null)
            {
                return NotFound();
            }

            alunoModel.plano = plano;


            try
            {
                alunoRepository.Alterar(alunoModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar os dados do Aluno. Detalhes: {error.Message}" });
            }
        }

    }
}
