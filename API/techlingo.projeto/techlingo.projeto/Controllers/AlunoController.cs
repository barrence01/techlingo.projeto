using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Controllers.DTO.Aluno;
using techlingo.projeto.Models.Aluno;
using techlingo.projeto.Repository;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Controllers
{
    [Route("api/Aluno")]
    [ApiController]
    [EnableCors]
    public class AlunoController : ControllerBase
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly AlunoRepository alunoRepository;
        private readonly AlunoCursosCursadosRepository alunoCursosCursadosRepository;
        private readonly PlanoRepository planoRepository;

        public AlunoController(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
            alunoRepository = new AlunoRepository(ctx);
            alunoCursosCursadosRepository = new AlunoCursosCursadosRepository(ctx);
            planoRepository = new PlanoRepository(ctx);
        }

        [HttpGet("ListarAlunos")]
        public async Task<IActionResult> ListarAlunos()
        {
            return Ok(alunoRepository.listarAlunosNT());
        }

        [HttpGet("ListarAlunosCompleto")]
        public async Task<IActionResult> ListarAlunosCompleto()
        {
            return Ok(alunoRepository.listarAlunosCompletoNT());
        }


        [HttpPost("CadastrarAluno")]
        public async Task<IActionResult> CadastrarAluno(AlunoRequestDTO novoAlunoRequest)
        {
            string planoNovo;
            switch (novoAlunoRequest.plano)
            {
                case "Basic":
                    planoNovo = "Basic";
                    break;
                case "Intermediate":
                    planoNovo = "Intermediate";
                    break;
                case "Gold":
                    planoNovo = "Gold";
                    break;
                case "Platinum":
                    planoNovo = "Platinum";
                    break;
                case "Scholar":
                    planoNovo = "Scholar";
                    break;
                default:
                    planoNovo = "Basic";
                    break;
            }

            var plano = planoRepository.consultarPlanosNomeNT(planoNovo);
            
            if (plano == null)
            {
                return BadRequest("Plano não existe.");
            }

            var emailExiste = dataBaseContext.Alunos.AsNoTracking()
                .Where(a => a.ds_email.ToLower().Equals(novoAlunoRequest.ds_email.ToLower()))
                .Count();

            if (emailExiste > 0)
            {
                return BadRequest("Email já cadastrado.");
            }

            try
            {
                AlunoModel novoAluno = new AlunoModel(novoAlunoRequest, plano.id_plano);

                dataBaseContext.Alunos.Add(novoAluno);
                try
                {
                    dataBaseContext.SaveChanges();
                    return Ok();
                }
                catch
                {
                    return BadRequest("Não foi possível cadastrar o usuário.");
                }


            } catch
            {
                return BadRequest("Data de nascimento inválida");
            }


        }

        [HttpGet("ConsultarAlunoId/{id:int}")]
        public async Task<IActionResult> ConsultarAlunoId(int id)
        {
            return Ok(alunoRepository.consultarAlunosIdNT(id));
        }

        [HttpGet("ConsultarCursos/{id:int}")]
        public async Task<IActionResult> ConsultarCursos(int id)
        {
            var lista = alunoCursosCursadosRepository.listarAlunoCursosCursadosNT(id);
            return Ok(lista);
        }

        [HttpPost("AdicionarCurso")]
        public async Task<IActionResult> AdicionarCurso(AlunoCursosCursadosRequestDTO cursoRequest)
        {
            AlunoCursosCursadosModel novoCurso = new AlunoCursosCursadosModel(cursoRequest);

            dataBaseContext.AlunoCursosCursados.Add(novoCurso);
            try
            {
                dataBaseContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost("LoginAluno")]
        public async Task<IActionResult> LoginAluno(LoginAlunoRequestDTO loginAlunoRequest)
        {
            if (loginAlunoRequest.email.Length == 0 || loginAlunoRequest.senha.Length == 0)
                return Unauthorized();

            var aluno = alunoRepository.LoginAluno(loginAlunoRequest.email,loginAlunoRequest.senha);
            if (aluno == null)
            {
                return Unauthorized();
            }

            return Ok();
        }

    }
}
