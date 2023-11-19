using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using techlingo.projeto.Controllers.DTO.Cursos;
using techlingo.projeto.Controllers.DTO.Cursos.Response;
using techlingo.projeto.Models.Aluno;
using techlingo.projeto.Models.Cursos;
using techlingo.projeto.Repository;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Controllers
{
    [Route("api/Cursos")]
    [ApiController]
    [EnableCors]
    public class CursosController : ControllerBase
    {

        private readonly DataBaseContext dataBaseContext;
        private readonly CursosRepository cursosRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly AlunoCursosCursadosRepository alunoCursosCursadosRepository;
        private readonly AulasRepository aulasRepository;

        public CursosController(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
            cursosRepository = new CursosRepository(ctx);
            alunoRepository = new AlunoRepository(ctx);
            alunoCursosCursadosRepository = new AlunoCursosCursadosRepository(ctx);
            aulasRepository = new AulasRepository(ctx);
        }

        [HttpGet("ListarCursos")]
        public IActionResult ListarCursos()
        {
            return Ok(cursosRepository.listarCursosNT());
        }

        [HttpGet("ListarCursosCompleto")]
        public IActionResult ListarCursosCompleto()
        {
            return Ok(cursosRepository.listarCursosCompletoNT());
        }


        [HttpGet("ConsultarCurso/{id:int}")]
        public IActionResult ConsultarCurso(int id)
        {
            return Ok(cursosRepository.consultarCursoPorIdNT(id));
        }

        [HttpPost("AdicionarCurso")]
        public IActionResult AdicionarCurso(CursosRequestDTO novoCursoRequest)
        {

            CursosModel novoCurso = new CursosModel(novoCursoRequest);


            dataBaseContext.Cursos.Add(novoCurso);
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

        [HttpPost("AdicionarAula")]
        public IActionResult AdicionarAula(AulasResquestDTO aulaResquest)
        {
            AulasModel novaAula = new AulasModel(aulaResquest);

            dataBaseContext.Aulas.Add(novaAula);
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

        [HttpPost("AdicionarConteudoAula")]
        public IActionResult AdicionarConteudoAula(ConteudoAulaRequestDTO aulaConteudoResquest)
        {
            AulaConteudoModel novaAulaConteudo = new AulaConteudoModel(aulaConteudoResquest);

            dataBaseContext.AulaConteudo.Add(novaAulaConteudo);
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

        [HttpPost("AdicionarConteudoQuiz")]
        public IActionResult AdicionarConteudoQuiz(ConteudoQuizRequestDTO quizResquest)
        {
            AulaQuizModel novaAulaQuiz = new AulaQuizModel(quizResquest);

            dataBaseContext.AulaQuiz.Add(novaAulaQuiz);
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



        [HttpPost("abrirCurso")]
        public IActionResult abrirCurso(AbrirCursoRequestDTO abrirCursoRequest)
        {
            var aluno = alunoRepository.LoginAluno(abrirCursoRequest.email, abrirCursoRequest.senha);
            if (aluno == null)
            {
                return BadRequest();
            }

            var curso = cursosRepository.consultarCursosPorNomeNT(abrirCursoRequest.nm_curso);
            if (curso == null)
            {
                return BadRequest();
            }

            var contador = alunoCursosCursadosRepository.alunoCursosCursadosCountNT(aluno.id_aluno, abrirCursoRequest.nm_curso);
            if (contador == 0)
            {
                AlunoCursosCursadosModel novoCurso = new AlunoCursosCursadosModel(aluno.id_aluno, curso.id_curso);


                
                dataBaseContext.AlunoCursosCursados.Add(novoCurso);
                try
                {
                    dataBaseContext.SaveChanges();

                    var aulasCurso = dataBaseContext.Aulas
                        .Where(a => a.id_curso == curso.id_curso)
                        .ToList();

                    aulasCurso.ForEach(aula =>
                    {
                        AlunoAulasCursadasModel novaAulaCursada = new AlunoAulasCursadasModel();
                        novaAulaCursada.st_status = "Não iniciado";
                        novaAulaCursada.id_aluno_cursante = novoCurso.id_aluno_cursante;
                        novaAulaCursada.id_aula = aula.id_aula;

                        dataBaseContext.AlunoAulasCursadas.Add(novaAulaCursada);
                    });

                    dataBaseContext.SaveChanges();


                }
                catch
                {
                    return BadRequest();
                }
            }

            var cursos_cursados = alunoCursosCursadosRepository.alunoCursosCursadosNT(aluno.id_aluno, abrirCursoRequest.nm_curso);

            CursoResponseDTO cursoResponseDTO = new CursoResponseDTO();

            cursoResponseDTO.dt_entrada = cursos_cursados.dt_entrada?.ToString("d/MM/yyyy");
            cursoResponseDTO.st_status = cursos_cursados.st_status;
            Dictionary<string, string> aulas = new Dictionary<string, string>();
            cursos_cursados.aulas_cursadas.ToList().ForEach(aula =>
            {
                aulas.Add(aula.aula.nm_aula, aula.st_status);
            });

            cursoResponseDTO.aulas = aulas;


            var qtd_aulas = dataBaseContext.Aulas.AsNoTracking()
                .Where(a => a.id_curso == curso.id_curso)
                .Count();
            cursoResponseDTO.qtd_aulas = qtd_aulas;


            return Ok(cursoResponseDTO);
        }


        [HttpPost("abrirAula")]
        public IActionResult abrirAula(AbrirAulaRequestDTO abrirConteudoAulaRequest)
        {

            var aluno = alunoRepository.LoginAluno(abrirConteudoAulaRequest.email, abrirConteudoAulaRequest.senha);
            if (aluno == null)
            {
                return BadRequest();
            }

            var curso = cursosRepository.consultarCursosPorNomeNT(abrirConteudoAulaRequest.nm_curso);
            if (curso == null)
            {
                return BadRequest();
            }

            var curso_cursado = alunoCursosCursadosRepository.alunoCursosCursadosNT(aluno.id_aluno, abrirConteudoAulaRequest.nm_curso);

            




            var aula = aulasRepository.consultarAulasPorIdCurso(abrirConteudoAulaRequest.nr_aula, curso.id_curso);
            if (aula == null)
            {
                return BadRequest();
            }

            var aula_cursada = dataBaseContext.AlunoAulasCursadas.AsNoTracking()
                .Include(a => a.aula)
                .Where(a => a.id_aluno_cursante == curso_cursado.id_aluno_cursante && a.aula.nr_aula == abrirConteudoAulaRequest.nr_aula)
                .Count();
            if (aula_cursada == 0)
            {
                AlunoAulasCursadasModel novaAulaCursada = new AlunoAulasCursadasModel();
                novaAulaCursada.st_status = "Não concluido";
                novaAulaCursada.id_aluno_cursante = curso_cursado.id_aluno_cursante;
                novaAulaCursada.id_aula = aula.id_aula;

                dataBaseContext.AlunoAulasCursadas.Add(novaAulaCursada);

                dataBaseContext.SaveChanges();

            }

            AbrirAulaConteudoResponseDTO abrirAulaConteudoResponseDTO = new AbrirAulaConteudoResponseDTO
            {
                ds_titulo = aula.conteudo.ds_titulo,
                ds_link_video = aula.conteudo.ds_link_video,
                ds_descricao = aula.conteudo.ds_descricao,
                ds_descricao_quiz = aula.conteudo.quiz.ds_descricao,
                ds_pergunta1 = aula.conteudo.quiz.ds_pergunta,
                ds_pergunta2 = aula.conteudo.quiz.ds_pergunta2,
                ds_pergunta3 = aula.conteudo.quiz.ds_pergunta3,
                ds_resposta = aula.conteudo.quiz.ds_resposta
            };


            return Ok(abrirAulaConteudoResponseDTO);
        }

        [HttpPost("concluirAula")]
        public IActionResult concluirAula(AulaConcluirAulaRequestDTO aulaConcluirAulaRequest)
        {
            var aluno = alunoRepository.LoginAluno(aulaConcluirAulaRequest.email, aulaConcluirAulaRequest.senha);
            if (aluno == null)
            {
                return BadRequest();
            }

            var curso = cursosRepository.consultarCursosPorNomeNT(aulaConcluirAulaRequest.nm_curso);
            if (curso == null)
            {
                return BadRequest();
            }

            var curso_cursado = alunoCursosCursadosRepository.alunoCursosCursadosIdNT(aluno.id_aluno, aulaConcluirAulaRequest.nm_curso);


            var aula = aulasRepository.consultarAulasPorIdCurso(aulaConcluirAulaRequest.nr_aula, curso.id_curso);
            if (aula == null)
            {
                return BadRequest();
            }

            var aula_cursada = dataBaseContext.AlunoAulasCursadas
                .Include(a => a.aula)
                .Where(a => a.id_aluno_cursante == curso_cursado.id_aluno_cursante && a.aula.nr_aula == aulaConcluirAulaRequest.nr_aula)
                .FirstOrDefault();
            if (aula_cursada == null)
            {
                return BadRequest();
            }

            aula_cursada.EditInfo(true);
            dataBaseContext.AlunoAulasCursadas.Update(aula_cursada);

            dataBaseContext.SaveChanges();


            return Ok();
        }

        [HttpPost("concluirCurso")]
        public IActionResult concluirCurso(ConcluirCursoRequestDTO concluirCursoRequest)
        {
            ConcluirCursoResponseDTO concluirCursoResponse = new ConcluirCursoResponseDTO();

            var aluno = alunoRepository.LoginAluno(concluirCursoRequest.email, concluirCursoRequest.senha);
            if (aluno == null)
            {
                return BadRequest();
            }

            var curso = cursosRepository.consultarCursosPorNomeNT(concluirCursoRequest.nm_curso);
            if (curso == null)
            {
                return BadRequest();
            }

            var curso_cursado = alunoCursosCursadosRepository.alunoCursosCursadosIdNT(aluno.id_aluno, concluirCursoRequest.nm_curso);

            int aulasCursadas = dataBaseContext.AlunoAulasCursadas.AsNoTracking()
                .Where(a => a.id_aluno_cursante == curso_cursado.id_aluno_cursante && a.st_status.Equals("Concluido"))
                .Count();

            int total_aulas = dataBaseContext.Aulas.AsNoTracking()
                .Where(a => a.id_curso == curso.id_curso)
                .Count();

            if (aulasCursadas != total_aulas)
            {
                concluirCursoResponse.resultado = "Não foi possível concluir o curso, pois ainda existem aulas não concluidas";
                return Ok(concluirCursoResponse);
            }

            curso_cursado.EditInfo(true);
            dataBaseContext.AlunoCursosCursados.Update(curso_cursado);

            dataBaseContext.SaveChanges();

            concluirCursoResponse.resultado = "Curso concluido com sucesso";
            return Ok(concluirCursoResponse);
        }
    }
}
