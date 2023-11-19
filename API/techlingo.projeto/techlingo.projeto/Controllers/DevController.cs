using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using techlingo.projeto.Models.Aluno;
using techlingo.projeto.Models.Cursos;
using techlingo.projeto.Models.Planos;
using techlingo.projeto.Repository;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Controllers
{
    [Route("api/Dev")]
    [ApiController]
    [EnableCors]
    public class DevController : ControllerBase
    {

        private readonly DataBaseContext dataBaseContext;
        private readonly PlanoRepository planoRepository;
        private readonly AlunoRepository alunoRepository;
        private readonly CursosRepository cursosRepository;
        private readonly AulasRepository aulasRepository;
        private readonly AulaConteudoRepository aulaConteudoRepository;

        public DevController(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
            planoRepository = new PlanoRepository(ctx);
            alunoRepository = new AlunoRepository(ctx);
            cursosRepository = new CursosRepository(ctx);
            aulasRepository = new AulasRepository(ctx);
            aulaConteudoRepository = new AulaConteudoRepository(ctx);
        }

        [HttpGet("1-PopularTabelas")]
        public IActionResult PopularAlunos()
        {
            try
            {
                // Criar lista de planos
                var listaPlanos = new List<PlanoModel>();
                PlanoModel plano = new PlanoModel();
                plano.nm_plano = "Basic";
                plano.vl_plano = 119;
                PlanoModel plano2 = new PlanoModel();
                plano2.nm_plano = "Intermediate";
                plano2.vl_plano = 159;
                PlanoModel plano3 = new PlanoModel();
                plano3.nm_plano = "Gold";
                plano3.vl_plano = 199;
                PlanoModel plano4 = new PlanoModel();
                plano4.nm_plano = "Platinum";
                plano4.vl_plano = 239;
                PlanoModel plano5 = new PlanoModel();
                plano5.nm_plano = "Scholar";
                plano5.vl_plano = 329;
                listaPlanos.Add(plano);
                listaPlanos.Add(plano2);
                listaPlanos.Add(plano3);
                listaPlanos.Add(plano4);
                listaPlanos.Add(plano5);

                foreach (var item in listaPlanos)
                {
                    var contador = planoRepository.consultarPlanosExistentePorNomeNT(item.nm_plano);

                    if (contador == 0)
                    {
                        dataBaseContext.Planos.Add(item);

                    }

                }

                dataBaseContext.SaveChanges();

                var planoBasic = planoRepository.consultarPlanosNomeNT("Basic");
                var planoIntermediate = planoRepository.consultarPlanosNomeNT("Intermediate");
                var planoGold = planoRepository.consultarPlanosNomeNT("Gold");
                var planoPlatinum = planoRepository.consultarPlanosNomeNT("Platinum");
                var planoScholar = planoRepository.consultarPlanosNomeNT("Scholar");

                // Crie lista de alunos
                var listaAlunos = new List<AlunoModel>();
                AlunoModel alunoModel = new AlunoModel();
                alunoModel.criarExemplo("William", "email@email.com", "123", planoBasic.id_plano);
                AlunoModel alunoModel2 = new AlunoModel();
                alunoModel2.criarExemplo("João", "email2@email.com", "123", planoIntermediate.id_plano);
                AlunoModel alunoModel3 = new AlunoModel();
                alunoModel3.criarExemplo("Maria", "email3@email.com", "123", planoGold.id_plano);
                AlunoModel alunoModel4 = new AlunoModel();
                alunoModel4.criarExemplo("José", "email4@email.com", "123", planoPlatinum.id_plano);
                AlunoModel alunoModel5 = new AlunoModel();
                alunoModel5.criarExemplo("Ana", "email5@email.com", "123", planoScholar.id_plano);
                listaAlunos.Add(alunoModel);
                listaAlunos.Add(alunoModel2);
                listaAlunos.Add(alunoModel3);
                listaAlunos.Add(alunoModel4);
                listaAlunos.Add(alunoModel5);

                foreach (var item in listaAlunos)
                {
                    var contador = alunoRepository.consultarAlunosExistentePorEmailNT(item.ds_email);

                    if (contador == 0)
                    {
                        dataBaseContext.Alunos.Add(item);

                    }

                }

                dataBaseContext.SaveChanges();



                return popularCursos();
            }
            catch (System.Exception ex)
            {
                return BadRequest("Já foi populado ou tabela não existe");
            }
        }

        [HttpGet("2-popularCursos")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult popularCursos()
        {
            //Cria lista de cursos
            var listaCursos = new List<CursosModel>();
            CursosModel cursoModel = new CursosModel();
            cursoModel.nm_curso = "Back-End";
            cursoModel.ds_curso = "Curso de Back-End";
            cursoModel.dt_criacao = DateTime.Now;
            CursosModel cursoModel2 = new CursosModel();
            cursoModel2.nm_curso = "Front-End";
            cursoModel2.ds_curso = "Curso de Front-End";
            cursoModel2.dt_criacao = DateTime.Now;
            CursosModel cursoModel3 = new CursosModel();
            cursoModel3.nm_curso = "Database";
            cursoModel3.ds_curso = "Curso de Database";
            cursoModel3.dt_criacao = DateTime.Now;
            CursosModel cursoModel4 = new CursosModel();
            cursoModel4.nm_curso = "Lógica de Programação";
            cursoModel4.ds_curso = "Curso de Lógica de Programação";
            cursoModel4.dt_criacao = DateTime.Now;
            CursosModel cursoModel5 = new CursosModel();
            cursoModel5.nm_curso = "Cloud Computing";
            cursoModel5.ds_curso = "Curso de Cloud Computing";
            cursoModel5.dt_criacao = DateTime.Now;

            listaCursos.Add(cursoModel);
            listaCursos.Add(cursoModel2);
            listaCursos.Add(cursoModel3);
            listaCursos.Add(cursoModel4);
            listaCursos.Add(cursoModel5);

            foreach (var item in listaCursos)
            {
                var contador = cursosRepository.consultarCursosExistentePorNomeNT(item.nm_curso);

                if (contador == 0)
                {
                    dataBaseContext.Cursos.Add(item);

                }

            }

            dataBaseContext.SaveChanges();

            var cursoBackEnd = cursosRepository.consultarCursosPorNomeNT("Back-End");
            var cursoBackEndId = cursoBackEnd.id_curso;
            var cursoFrontEnd = cursosRepository.consultarCursosPorNomeNT("Front-End");
            var cursoFrontEndId = cursoFrontEnd.id_curso;
            var cursoDatabase = cursosRepository.consultarCursosPorNomeNT("Database");
            var cursoDatabaseId = cursoDatabase.id_curso;
            var cursoLogica = cursosRepository.consultarCursosPorNomeNT("Lógica de Programação");
            var cursoLogicaId = cursoLogica.id_curso;
            var cursoCloud = cursosRepository.consultarCursosPorNomeNT("Cloud Computing");
            var cursoCloudId = cursoCloud.id_curso;

            //Criar Lista de Aulas
            var listaAulas = new List<AulasModel>();
            // Popular backend com 3 aulas
            AulasModel aulasModel = new AulasModel();
            aulasModel.nr_aula = 1;
            aulasModel.nm_aula = "Aula 01";
            aulasModel.dt_criacao = DateTime.Now;
            aulasModel.id_curso = cursoBackEnd.id_curso;
            AulasModel aulasModel2 = new AulasModel();
            aulasModel2.nr_aula = 2;
            aulasModel2.nm_aula = "Aula 02";
            aulasModel2.dt_criacao = DateTime.Now;
            aulasModel2.id_curso = cursoBackEnd.id_curso;
            AulasModel aulasModel3 = new AulasModel();
            aulasModel3.nr_aula = 3;
            aulasModel3.nm_aula = "Aula 03";
            aulasModel3.dt_criacao = DateTime.Now;
            aulasModel3.id_curso = cursoBackEnd.id_curso;
            // Popular FrontEnd com 3 aulas
            AulasModel aulasModel4 = new AulasModel();
            aulasModel4.nr_aula = 1;
            aulasModel4.nm_aula = "Aula 01";
            aulasModel4.dt_criacao = DateTime.Now;
            aulasModel4.id_curso = cursoFrontEnd.id_curso;
            AulasModel aulasModel5 = new AulasModel();
            aulasModel5.nr_aula = 2;
            aulasModel5.nm_aula = "Aula 02";
            aulasModel5.dt_criacao = DateTime.Now;
            aulasModel5.id_curso = cursoFrontEnd.id_curso;
            AulasModel aulasModel6 = new AulasModel();
            aulasModel6.nr_aula = 3;
            aulasModel6.nm_aula = "Aula 03";
            aulasModel6.dt_criacao = DateTime.Now;
            aulasModel6.id_curso = cursoFrontEnd.id_curso;
            // Popular Database com 3 aulas
            AulasModel aulasModel7 = new AulasModel();
            aulasModel7.nr_aula = 1;
            aulasModel7.nm_aula = "Aula 01";
            aulasModel7.dt_criacao = DateTime.Now;
            aulasModel7.id_curso = cursoDatabase.id_curso;
            AulasModel aulasModel8 = new AulasModel();
            aulasModel8.nr_aula = 2;
            aulasModel8.nm_aula = "Aula 02";
            aulasModel8.dt_criacao = DateTime.Now;
            aulasModel8.id_curso = cursoDatabase.id_curso;
            AulasModel aulasModel9 = new AulasModel();
            aulasModel9.nr_aula = 3;
            aulasModel9.nm_aula = "Aula 03";
            aulasModel9.dt_criacao = DateTime.Now;
            aulasModel9.id_curso = cursoDatabase.id_curso;
            // Popular Lógica de Programação com 3 aulas
            AulasModel aulasModel10 = new AulasModel();
            aulasModel10.nr_aula = 1;
            aulasModel10.nm_aula = "Aula 01";
            aulasModel10.dt_criacao = DateTime.Now;
            aulasModel10.id_curso = cursoLogica.id_curso;
            AulasModel aulasModel11 = new AulasModel();
            aulasModel11.nr_aula = 2;
            aulasModel11.nm_aula = "Aula 02";
            aulasModel11.dt_criacao = DateTime.Now;
            aulasModel11.id_curso = cursoLogica.id_curso;
            AulasModel aulasModel12 = new AulasModel();
            aulasModel12.nr_aula = 3;
            aulasModel12.nm_aula = "Aula 03";
            aulasModel12.dt_criacao = DateTime.Now;
            aulasModel12.id_curso = cursoLogica.id_curso;
            // Popular Cloud Computing com 3 aulas
            AulasModel aulasModel13 = new AulasModel();
            aulasModel13.nr_aula = 1;
            aulasModel13.nm_aula = "Aula 01";
            aulasModel13.dt_criacao = DateTime.Now;
            aulasModel13.id_curso = cursoCloud.id_curso;
            AulasModel aulasModel14 = new AulasModel();
            aulasModel14.nr_aula = 2;
            aulasModel14.nm_aula = "Aula 02";
            aulasModel14.dt_criacao = DateTime.Now;
            aulasModel14.id_curso = cursoCloud.id_curso;
            AulasModel aulasModel15 = new AulasModel();
            aulasModel15.nr_aula = 3;
            aulasModel15.nm_aula = "Aula 03";
            aulasModel15.dt_criacao = DateTime.Now;
            aulasModel15.id_curso = cursoCloud.id_curso;

            listaAulas.Add(aulasModel);
            listaAulas.Add(aulasModel2);
            listaAulas.Add(aulasModel3);
            listaAulas.Add(aulasModel4);
            listaAulas.Add(aulasModel5);
            listaAulas.Add(aulasModel6);
            listaAulas.Add(aulasModel7);
            listaAulas.Add(aulasModel8);
            listaAulas.Add(aulasModel9);
            listaAulas.Add(aulasModel10);
            listaAulas.Add(aulasModel11); 
            listaAulas.Add(aulasModel12);
            listaAulas.Add(aulasModel13);
            listaAulas.Add(aulasModel14);
            listaAulas.Add(aulasModel15);

            foreach (var item in listaAulas)
            {
                var contador = aulasRepository.consultarAulasExistentePorNomeIdNT(item.nm_aula, item.id_curso);

                if (contador == 0)
                {
                    dataBaseContext.Aulas.Add(item);

                }

            }

            dataBaseContext.SaveChanges();


            var BackEndAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoBackEndId);
            var BackEndAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoBackEndId);
            var BackEndAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoBackEndId);
            var FrontEndAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoFrontEndId);
            var FrontEndAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoFrontEndId);
            var FrontEndAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoFrontEndId);
            var DatabaseAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoDatabaseId);
            var DatabaseAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoDatabaseId);
            var DatabaseAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoDatabaseId);
            var LogicaAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoLogicaId);
            var LogicaAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoLogicaId);
            var LogicaAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoLogicaId);
            var CloudAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoCloudId);
            var CloudAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoCloudId);
            var CloudAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoCloudId);


            //Criar Lista de Conteudo
            var listaAulasConteudo = new List<AulaConteudoModel>();
            // popular cada aula com 3 conteudo
            AulaConteudoModel aulaConteudoModel = new AulaConteudoModel();
            aulaConteudoModel.ds_titulo = "Titulo da aula 1 backend";
            aulaConteudoModel.ds_link_video = @"https://youtu.be/q1sF6D7vKPE?si=VxXydCJWzlkicM2p";
            aulaConteudoModel.ds_descricao = "Descricao da aula 1 backend";
            aulaConteudoModel.id_aula = BackEndAulaId;
            AulaConteudoModel aulaConteudoModel2 = new AulaConteudoModel();
            aulaConteudoModel2.ds_titulo = "Titulo da aula 2 backend";
            aulaConteudoModel2.ds_link_video = @"https://youtu.be/uGZJICFG-P4?si=9HDRKId0wJgeoW0J";
            aulaConteudoModel2.ds_descricao = "Descricao da aula 2 backend";
            aulaConteudoModel2.id_aula = BackEndAulaId2;
            AulaConteudoModel aulaConteudoModel3 = new AulaConteudoModel();
            aulaConteudoModel3.ds_titulo = "Titulo da aula 3 backend";
            aulaConteudoModel3.ds_link_video = @"https://youtu.be/am1HD3B2sjU?si=VIP-_3U3cZhV-lUc";
            aulaConteudoModel3.ds_descricao = "Descricao da aula 3 backend";
            aulaConteudoModel3.id_aula = BackEndAulaId3;
            AulaConteudoModel aulaConteudoModel4 = new AulaConteudoModel();
            aulaConteudoModel4.ds_titulo = "Titulo da aula 1 frontend";
            aulaConteudoModel4.ds_link_video = @"https://youtu.be/FXqX7oof0I4?si=rdManCgevhhXGive";
            aulaConteudoModel4.ds_descricao = "Descricao da aula 1 frontend";
            aulaConteudoModel4.id_aula = FrontEndAulaId;
            AulaConteudoModel aulaConteudoModel5 = new AulaConteudoModel();
            aulaConteudoModel5.ds_titulo = "Titulo da aula 2 frontend";
            aulaConteudoModel5.ds_link_video = @"https://youtu.be/Jg6JaEjovJk?si=gDB2leA9w551W-Sz";
            aulaConteudoModel5.ds_descricao = "Descricao da aula 2 frontend";
            aulaConteudoModel5.id_aula = FrontEndAulaId2;
            AulaConteudoModel aulaConteudoModel6 = new AulaConteudoModel();
            aulaConteudoModel6.ds_titulo = "Titulo da aula 3 frontend";
            aulaConteudoModel6.ds_link_video = @"https://youtu.be/9iKNxnFJY_Q?si=-shyhw6JO_KpD09U";
            aulaConteudoModel6.ds_descricao = "Descricao da aula 3 frontend";
            aulaConteudoModel6.id_aula = FrontEndAulaId3;
            AulaConteudoModel aulaConteudoModel7 = new AulaConteudoModel();
            aulaConteudoModel7.ds_titulo = "Titulo da aula 1 database";
            aulaConteudoModel7.ds_link_video = @"https://youtu.be/Ofktsne-utM?si=hrxC1p5-Mm7ThsgF";
            aulaConteudoModel7.ds_descricao = "Descricao da aula 1 database";
            aulaConteudoModel7.id_aula = DatabaseAulaId;
            AulaConteudoModel aulaConteudoModel8 = new AulaConteudoModel();
            aulaConteudoModel8.ds_titulo = "Titulo da aula 2 database";
            aulaConteudoModel8.ds_link_video = @"https://youtu.be/5JbAOWJbgIA?si=ilaSlY3R-ceAHV6H";
            aulaConteudoModel8.ds_descricao = "Descricao da aula 2 database";
            aulaConteudoModel8.id_aula = DatabaseAulaId2;
            AulaConteudoModel aulaConteudoModel9 = new AulaConteudoModel();
            aulaConteudoModel9.ds_titulo = "Titulo da aula 3 database";
            aulaConteudoModel9.ds_link_video = @"https://youtu.be/m9YPlX0fcJk?si=978DUIzZ_tDTkr-Z";
            aulaConteudoModel9.ds_descricao = "Descricao da aula 3 database";
            aulaConteudoModel9.id_aula = DatabaseAulaId3;
            AulaConteudoModel aulaConteudoModel10 = new AulaConteudoModel();
            aulaConteudoModel10.ds_titulo = "Titulo da aula 1 logica";
            aulaConteudoModel10.ds_link_video = @"https://youtu.be/8mei6uVttho?si=OAX0wLCkwoKxjg-N";
            aulaConteudoModel10.ds_descricao = "Descricao da aula 1 logica";
            aulaConteudoModel10.id_aula = LogicaAulaId;
            AulaConteudoModel aulaConteudoModel11 = new AulaConteudoModel();
            aulaConteudoModel11.ds_titulo = "Titulo da aula 2 logica";
            aulaConteudoModel11.ds_link_video = @"https://youtu.be/M2Af7gkbbro?si=D1X4Fq6s4YVcCPtz";
            aulaConteudoModel11.ds_descricao = "Descricao da aula 2 logica";
            aulaConteudoModel11.id_aula = LogicaAulaId2;
            AulaConteudoModel aulaConteudoModel12 = new AulaConteudoModel();
            aulaConteudoModel12.ds_titulo = "Titulo da aula 3 logica";
            aulaConteudoModel12.ds_link_video = @"https://youtu.be/RDrfZ-7WE8c?si=aogMMsBVt3bATnck";
            aulaConteudoModel12.ds_descricao = "Descricao da aula 3 logica";
            aulaConteudoModel12.id_aula = LogicaAulaId3;
            AulaConteudoModel aulaConteudoModel13 = new AulaConteudoModel();
            aulaConteudoModel13.ds_titulo = "Titulo da aula 1 cloud";
            aulaConteudoModel13.ds_link_video = @"https://youtu.be/zaj0IX8dQwA?si=wIzgLoZiA0s8L7pp";
            aulaConteudoModel13.ds_descricao = "Descricao da aula 1 cloud";
            aulaConteudoModel13.id_aula = CloudAulaId;
            AulaConteudoModel aulaConteudoModel14 = new AulaConteudoModel();
            aulaConteudoModel14.ds_titulo = "Titulo da aula 2 cloud";
            aulaConteudoModel14.ds_link_video = @"https://youtu.be/t-pECrshNZQ?si=e-jZtI972Z3TPeAb";
            aulaConteudoModel14.ds_descricao = "Descricao da aula 2 cloud";
            aulaConteudoModel14.id_aula = CloudAulaId2;
            AulaConteudoModel aulaConteudoModel15 = new AulaConteudoModel();
            aulaConteudoModel15.ds_titulo = "Titulo da aula 3 cloud";
            aulaConteudoModel15.ds_link_video = @"https://youtu.be/9Y3ekuEBFdg?si=aPXu104ILBnkX4e0";
            aulaConteudoModel15.ds_descricao = "Descricao da aula 3 cloud";
            aulaConteudoModel15.id_aula = CloudAulaId3;

            listaAulasConteudo.Add(aulaConteudoModel);
            listaAulasConteudo.Add(aulaConteudoModel2);
            listaAulasConteudo.Add(aulaConteudoModel3);
            listaAulasConteudo.Add(aulaConteudoModel4);
            listaAulasConteudo.Add(aulaConteudoModel5);
            listaAulasConteudo.Add(aulaConteudoModel6);
            listaAulasConteudo.Add(aulaConteudoModel7);
            listaAulasConteudo.Add(aulaConteudoModel8);
            listaAulasConteudo.Add(aulaConteudoModel9);
            listaAulasConteudo.Add(aulaConteudoModel10);
            listaAulasConteudo.Add(aulaConteudoModel11);
            listaAulasConteudo.Add(aulaConteudoModel12);
            listaAulasConteudo.Add(aulaConteudoModel13);
            listaAulasConteudo.Add(aulaConteudoModel14);
            listaAulasConteudo.Add(aulaConteudoModel15);

            foreach (var item in listaAulasConteudo)
            {
                var contador = aulaConteudoRepository.consultarAulasConteudoExistentePorIdsNT(item.id_aula);

                if (contador == 0)
                {
                    dataBaseContext.AulaConteudo.Add(item);

                }

            }

            dataBaseContext.SaveChanges();

            // Criar lista de quiz
            var listaQuiz = new List<AulaQuizModel>();
            // Popular cada conteudo de aula com 1 quiz
            AulaQuizModel aulaQuizModel = new AulaQuizModel();
            aulaQuizModel.ds_titulo = "Titulo quiz aula 1 backend";
            aulaQuizModel.ds_descricao = "Qual é a principal responsabilidade do back end em uma aplicação web?";
            aulaQuizModel.ds_pergunta = "Gerenciar a interface do usuário e a experiência do usuário.";
            aulaQuizModel.ds_pergunta2 = "Lidar com o armazenamento de dados e a lógica de negócios.";
            aulaQuizModel.ds_pergunta3 = "Fornecer design gráfico e recursos visuais.";
            aulaQuizModel.ds_resposta = "Lidar com o armazenamento de dados e a lógica de negócios.";
            aulaQuizModel.id_aula_conteudo = aulaConteudoModel.id_aula_conteudo;
            AulaQuizModel aulaQuizModel2 = new AulaQuizModel();
            aulaQuizModel2.ds_titulo = "Titulo quiz aula 2 backend";
            aulaQuizModel2.ds_descricao = "Qual linguagem de programação é frequentemente usada no desenvolvimento do back-end de uma aplicação web?";
            aulaQuizModel2.ds_pergunta = "HTML";
            aulaQuizModel2.ds_pergunta2 = "Python";
            aulaQuizModel2.ds_pergunta3 = "CSS";
            aulaQuizModel2.ds_resposta = "Python";
            aulaQuizModel2.id_aula_conteudo = aulaConteudoModel2.id_aula_conteudo;
            AulaQuizModel aulaQuizModel3 = new AulaQuizModel();
            aulaQuizModel3.ds_titulo = "Titulo quiz aula 3 backend";
            aulaQuizModel3.ds_descricao = "O que é uma API (Interface de Programação de Aplicativos) no contexto do back-end?";
            aulaQuizModel3.ds_pergunta = "Um idioma de marcação usado para criar a interface do usuário.";
            aulaQuizModel3.ds_pergunta2 = "Um componente de hardware que armazena dados de forma segura.";
            aulaQuizModel3.ds_pergunta3 = "Um conjunto de regras e protocolos que permitem que aplicativos se comuniquem entre si.";
            aulaQuizModel3.ds_resposta = "Um conjunto de regras e protocolos que permitem que aplicativos se comuniquem entre si.";
            aulaQuizModel3.id_aula_conteudo = aulaConteudoModel3.id_aula_conteudo;
            AulaQuizModel aulaQuizModel4 = new AulaQuizModel();
            aulaQuizModel4.ds_titulo = "Titulo quiz 1 1 aula 1 frontend";
            aulaQuizModel4.ds_descricao = "Qual é o principal objetivo do front end em uma aplicação web?";
            aulaQuizModel4.ds_pergunta = "Gerenciar o servidor e a lógica de negócios.";
            aulaQuizModel4.ds_pergunta2 = "Criar a interface do usuário e melhorar a experiência do usuário.";
            aulaQuizModel4.ds_pergunta3 = "Armazenar dados e autenticar usuários.";
            aulaQuizModel4.ds_resposta = "Criar a interface do usuário e melhorar a experiência do usuário.";
            aulaQuizModel4.id_aula_conteudo = aulaConteudoModel4.id_aula_conteudo;
            AulaQuizModel aulaQuizModel5 = new AulaQuizModel();
            aulaQuizModel5.ds_titulo = "Titulo quiz aula 2 frontend";
            aulaQuizModel5.ds_descricao = "Qual linguagem de estilo é comumente usada no desenvolvimento front-end para aplicar estilos visuais a elementos HTML?";
            aulaQuizModel5.ds_pergunta = "Java";
            aulaQuizModel5.ds_pergunta2 = "CSS (Cascading Style Sheets)";
            aulaQuizModel5.ds_pergunta3 = "JavaScript";
            aulaQuizModel5.ds_resposta = "CSS (Cascading Style Sheets)";
            aulaQuizModel5.id_aula_conteudo = aulaConteudoModel5.id_aula_conteudo;
            AulaQuizModel aulaQuizModel6 = new AulaQuizModel();
            aulaQuizModel6.ds_titulo = "Titulo quiz aula 3 frontend";
            aulaQuizModel6.ds_descricao = "O que é responsabilidade do JavaScript no desenvolvimento front-end de uma página web?";
            aulaQuizModel6.ds_pergunta = "Definir a estrutura e o conteúdo da página.";
            aulaQuizModel6.ds_pergunta2 = "Aplicar estilos visuais aos elementos HTML.";
            aulaQuizModel6.ds_pergunta3 = "Adicionar interatividade e lógica à página.";
            aulaQuizModel6.ds_resposta = "Adicionar interatividade e lógica à página.";
            aulaQuizModel6.id_aula_conteudo = aulaConteudoModel6.id_aula_conteudo;
            AulaQuizModel aulaQuizModel7 = new AulaQuizModel();
            aulaQuizModel7.ds_titulo = "Titulo quiz 1 aula 1 database";
            aulaQuizModel7.ds_descricao = "Qual é o principal objetivo de um banco de dados em um sistema de informação?";
            aulaQuizModel7.ds_pergunta = "Armazenar e organizar dados de forma eficiente.";
            aulaQuizModel7.ds_pergunta2 = "Projetar interfaces gráficas atraentes para os usuários.";
            aulaQuizModel7.ds_pergunta3 = "Gerenciar a interface do usuário e a experiência do usuário.";
            aulaQuizModel7.ds_resposta = "Armazenar e organizar dados de forma eficiente.";
            aulaQuizModel7.id_aula_conteudo = aulaConteudoModel7.id_aula_conteudo;
            AulaQuizModel aulaQuizModel8 = new AulaQuizModel();
            aulaQuizModel8.ds_titulo = "Titulo quiz aula 2 database";
            aulaQuizModel8.ds_descricao = "O que é um SGBD (Sistema de Gerenciamento de Banco de Dados)?";
            aulaQuizModel8.ds_pergunta = "Um programa de edição de texto.";
            aulaQuizModel8.ds_pergunta2 = "Um dispositivo de armazenamento físico para dados.";
            aulaQuizModel8.ds_pergunta3 = "Um software que permite criar, modificar e gerenciar bancos de dados.";
            aulaQuizModel8.ds_resposta = "Um software que permite criar, modificar e gerenciar bancos de dados.";
            aulaQuizModel8.id_aula_conteudo = aulaConteudoModel8.id_aula_conteudo;
            AulaQuizModel aulaQuizModel9 = new AulaQuizModel();
            aulaQuizModel9.ds_titulo = "Titulo quiz aula 3 database";
            aulaQuizModel9.ds_descricao = "O que é uma chave primária em um banco de dados relacional?";
            aulaQuizModel9.ds_pergunta = "Um campo que armazena valores duplicados.";
            aulaQuizModel9.ds_pergunta2 = "Um campo que identifica de forma exclusiva cada registro em uma tabela.";
            aulaQuizModel9.ds_pergunta3 = "Um campo que armazena dados sensíveis.";
            aulaQuizModel9.ds_resposta = "Um campo que identifica de forma exclusiva cada registro em uma tabela.";
            aulaQuizModel9.id_aula_conteudo = aulaConteudoModel9.id_aula_conteudo;
            AulaQuizModel aulaQuizModel10 = new AulaQuizModel();
            aulaQuizModel10.ds_titulo = "Titulo quiz 1 aula 1 logica";
            aulaQuizModel10.ds_descricao = "Para printar uma frase no console em python devemos ultilizar qual comando?";
            aulaQuizModel10.ds_pergunta = "printf(Hello World);";
            aulaQuizModel10.ds_pergunta2 = "Log.print(\"hello world\")";
            aulaQuizModel10.ds_pergunta3 = "print(\"Hello World\")";
            aulaQuizModel10.ds_resposta = "print(\"Hello World\")";
            aulaQuizModel10.id_aula_conteudo = aulaConteudoModel10.id_aula_conteudo;
            AulaQuizModel aulaQuizModel11 = new AulaQuizModel();
            aulaQuizModel11.ds_titulo = "Titulo quiz aula 2 logica";
            aulaQuizModel11.ds_descricao = "Qual estrutura de controle é usada para repetir uma ação até que uma condição seja atendida?";
            aulaQuizModel11.ds_pergunta = "Estrutura condicional.";
            aulaQuizModel11.ds_pergunta2 = "Estrutura de repetição (loop).";
            aulaQuizModel11.ds_pergunta3 = "Função.";
            aulaQuizModel11.ds_resposta = "Estrutura de repetição (loop).";
            aulaQuizModel11.id_aula_conteudo = aulaConteudoModel11.id_aula_conteudo;
            AulaQuizModel aulaQuizModel12 = new AulaQuizModel();
            aulaQuizModel12.ds_titulo = "Titulo quiz aula 3 logica";
            aulaQuizModel12.ds_descricao = "Qual é o resultado da seguinte expressão lógica em Python?\n \"5 > 3 and 2 < 4\":";
            aulaQuizModel12.ds_pergunta = "True";
            aulaQuizModel12.ds_pergunta2 = "False";
            aulaQuizModel12.ds_pergunta3 = "10";
            aulaQuizModel12.ds_resposta = "True";
            aulaQuizModel12.id_aula_conteudo = aulaConteudoModel12.id_aula_conteudo;
            AulaQuizModel aulaQuizModel13 = new AulaQuizModel();
            aulaQuizModel13.ds_titulo = "Titulo quiz 1 aula 1 cloud";
            aulaQuizModel13.ds_descricao = "O que é Cloud Computing?";
            aulaQuizModel13.ds_pergunta = "Um método para armazenar dados em servidores físicos locais.";
            aulaQuizModel13.ds_pergunta2 = "Uma técnica para enviar e-mails de forma mais segura.";
            aulaQuizModel13.ds_pergunta3 = "Um modelo de fornecimento de serviços de computação pela Internet.";
            aulaQuizModel13.ds_resposta = "Um modelo de fornecimento de serviços de computação pela Internet.";
            aulaQuizModel13.id_aula_conteudo = aulaConteudoModel13.id_aula_conteudo;
            AulaQuizModel aulaQuizModel14 = new AulaQuizModel();
            aulaQuizModel14.ds_titulo = "Titulo quiz aula 2 cloud";
            aulaQuizModel14.ds_descricao = "O que é um benefício comum do uso de serviços de Cloud Computing?";
            aulaQuizModel14.ds_pergunta = "Maior controle sobre a infraestrutura física.";
            aulaQuizModel14.ds_pergunta2 = "Redução de custos operacionais.";
            aulaQuizModel14.ds_pergunta3 = "Limitação na escalabilidade de recursos.";
            aulaQuizModel14.ds_resposta = "Redução de custos operacionais.";
            aulaQuizModel14.id_aula_conteudo = aulaConteudoModel14.id_aula_conteudo;
            AulaQuizModel aulaQuizModel15 = new AulaQuizModel();
            aulaQuizModel15.ds_titulo = "Titulo quiz aula 3 cloud";
            aulaQuizModel15.ds_descricao = "Quais dos seguintes são exemplos de serviços de Cloud Computing?";
            aulaQuizModel15.ds_pergunta = "Processadores de texto e planilhas.";
            aulaQuizModel15.ds_pergunta2 = "Servidores físicos e roteadores.";
            aulaQuizModel15.ds_pergunta3 = "Armazenamento em nuvem e máquinas virtuais.";
            aulaQuizModel15.ds_resposta = "Armazenamento em nuvem e máquinas virtuais.";
            aulaQuizModel15.id_aula_conteudo = aulaConteudoModel15.id_aula_conteudo;

            listaQuiz.Add(aulaQuizModel);
            listaQuiz.Add(aulaQuizModel2);
            listaQuiz.Add(aulaQuizModel3);
            listaQuiz.Add(aulaQuizModel4);
            listaQuiz.Add(aulaQuizModel5);
            listaQuiz.Add(aulaQuizModel6);
            listaQuiz.Add(aulaQuizModel7);
            listaQuiz.Add(aulaQuizModel8);
            listaQuiz.Add(aulaQuizModel9);
            listaQuiz.Add(aulaQuizModel10);
            listaQuiz.Add(aulaQuizModel11);
            listaQuiz.Add(aulaQuizModel12);
            listaQuiz.Add(aulaQuizModel13);
            listaQuiz.Add(aulaQuizModel14);
            listaQuiz.Add(aulaQuizModel15);

            dataBaseContext.AulaQuiz.AddRange(listaQuiz);


            dataBaseContext.SaveChanges();


            return PopularRelacoes();
        }

        
        [HttpGet("3-popularRelacoes")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult PopularRelacoes()
        {
            var aluno1 = alunoRepository.consultarAlunosPorEmailNT("email@email.com");
            var aluno2 = alunoRepository.consultarAlunosPorEmailNT("email2@email.com");
            var aluno3 = alunoRepository.consultarAlunosPorEmailNT("email3@email.com");
            var aluno4 = alunoRepository.consultarAlunosPorEmailNT("email4@email.com");
            var aluno5 = alunoRepository.consultarAlunosPorEmailNT("email5@email.com");

            var cursoBackEnd = cursosRepository.consultarCursosPorNomeNT("Back-End");
            var cursoFrontEnd = cursosRepository.consultarCursosPorNomeNT("Front-End");
            var cursoDatabase = cursosRepository.consultarCursosPorNomeNT("Database");
            var cursoLogica = cursosRepository.consultarCursosPorNomeNT("Lógica de Programação");
            var cursoCloud = cursosRepository.consultarCursosPorNomeNT("Cloud Computing");

            var BackEndAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoBackEnd.id_curso);
            var BackEndAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoBackEnd.id_curso);
            var BackEndAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoBackEnd.id_curso);
            var FrontEndAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoFrontEnd.id_curso);
            var FrontEndAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoFrontEnd.id_curso);
            var FrontEndAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoFrontEnd.id_curso);
            var DatabaseAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoDatabase.id_curso);
            var DatabaseAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoDatabase.id_curso);
            var DatabaseAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoDatabase.id_curso);
            var LogicaAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoLogica.id_curso);
            var LogicaAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoLogica.id_curso);
            var LogicaAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoLogica.id_curso);
            var CloudAulaId = aulasRepository.consultarAulasPorNumeroIdNT(1, cursoCloud.id_curso);
            var CloudAulaId2 = aulasRepository.consultarAulasPorNumeroIdNT(2, cursoCloud.id_curso);
            var CloudAulaId3 = aulasRepository.consultarAulasPorNumeroIdNT(3, cursoCloud.id_curso);

            // Criar lista de cursos cursado por alunos
            var listaAlunoCursosCursados = new List<AlunoCursosCursadosModel>();

            AlunoCursosCursadosModel alunoCursosCursados = new AlunoCursosCursadosModel();
            alunoCursosCursados.st_status = "Em andamento";
            alunoCursosCursados.dt_entrada = DateTime.Now;
            alunoCursosCursados.id_aluno = aluno1.id_aluno;
            alunoCursosCursados.id_curso = cursoBackEnd.id_curso;
            AlunoCursosCursadosModel alunoCursosCursados2 = new AlunoCursosCursadosModel();
            alunoCursosCursados2.st_status = "Em andamento";
            alunoCursosCursados2.dt_entrada = DateTime.Now;
            alunoCursosCursados2.id_aluno = aluno1.id_aluno;
            alunoCursosCursados2.id_curso = cursoFrontEnd.id_curso;
            AlunoCursosCursadosModel alunoCursosCursados3 = new AlunoCursosCursadosModel();
            alunoCursosCursados3.st_status = "Concluido";
            alunoCursosCursados3.dt_entrada = DateTime.Now;
            alunoCursosCursados3.id_aluno = aluno2.id_aluno;
            alunoCursosCursados3.id_curso = cursoDatabase.id_curso;
            AlunoCursosCursadosModel alunoCursosCursados4 = new AlunoCursosCursadosModel();
            alunoCursosCursados4.st_status = "Em andamento";
            alunoCursosCursados4.dt_entrada = DateTime.Now;
            alunoCursosCursados4.id_aluno = aluno2.id_aluno;
            alunoCursosCursados4.id_curso = cursoLogica.id_curso;
            AlunoCursosCursadosModel alunoCursosCursados5 = new AlunoCursosCursadosModel();
            alunoCursosCursados5.st_status = "Em andamento";
            alunoCursosCursados5.dt_entrada = DateTime.Now;
            alunoCursosCursados5.id_aluno = aluno3.id_aluno;
            alunoCursosCursados5.id_curso = cursoCloud.id_curso;
            AlunoCursosCursadosModel alunoCursosCursados6 = new AlunoCursosCursadosModel();
            alunoCursosCursados6.st_status = "Em andamento";
            alunoCursosCursados6.dt_entrada = DateTime.Now;
            alunoCursosCursados6.id_aluno = aluno4.id_aluno;
            alunoCursosCursados6.id_curso = cursoBackEnd.id_curso;
            AlunoCursosCursadosModel alunoCursosCursados7 = new AlunoCursosCursadosModel();
            alunoCursosCursados7.st_status = "Em andamento";
            alunoCursosCursados7.dt_entrada = DateTime.Now;
            alunoCursosCursados7.id_aluno = aluno5.id_aluno;
            alunoCursosCursados7.id_curso = cursoFrontEnd.id_curso;

            listaAlunoCursosCursados.Add(alunoCursosCursados);
            listaAlunoCursosCursados.Add(alunoCursosCursados2);
            listaAlunoCursosCursados.Add(alunoCursosCursados3);
            listaAlunoCursosCursados.Add(alunoCursosCursados4);
            listaAlunoCursosCursados.Add(alunoCursosCursados5);
            listaAlunoCursosCursados.Add(alunoCursosCursados6);
            listaAlunoCursosCursados.Add(alunoCursosCursados7);

            
            dataBaseContext.AlunoCursosCursados.AddRange(listaAlunoCursosCursados);

            dataBaseContext.SaveChanges();

            // Criar lista de aulas cursadas por alunos
            var listaAlunoAulasCursadas = new List<AlunoAulasCursadasModel>();
            // Popular com 2 aulas cursadas para cada aluno cursante
            AlunoAulasCursadasModel alunoAulasCursadas = new AlunoAulasCursadasModel();
            alunoAulasCursadas.st_status = "Concluido";
            alunoAulasCursadas.id_aluno_cursante = alunoCursosCursados.id_aluno_cursante;
            alunoAulasCursadas.id_aula = BackEndAulaId;
            AlunoAulasCursadasModel alunoAulasCursadas2 = new AlunoAulasCursadasModel();
            alunoAulasCursadas2.st_status = "Não concluido";
            alunoAulasCursadas2.id_aluno_cursante = alunoCursosCursados.id_aluno_cursante;
            alunoAulasCursadas2.id_aula = BackEndAulaId2;
            AlunoAulasCursadasModel alunoAulasCursadas11 = new AlunoAulasCursadasModel();
            alunoAulasCursadas11.st_status = "Não iniciado";
            alunoAulasCursadas11.id_aluno_cursante = alunoCursosCursados.id_aluno_cursante;
            alunoAulasCursadas11.id_aula = BackEndAulaId3;

            AlunoAulasCursadasModel alunoAulasCursadas3 = new AlunoAulasCursadasModel();
            alunoAulasCursadas3.st_status = "Não concluido";
            alunoAulasCursadas3.id_aluno_cursante = alunoCursosCursados2.id_aluno_cursante;
            alunoAulasCursadas3.id_aula = FrontEndAulaId;
            AlunoAulasCursadasModel alunoAulasCursadas4 = new AlunoAulasCursadasModel();
            alunoAulasCursadas4.st_status = "Não concluido";
            alunoAulasCursadas4.id_aluno_cursante = alunoCursosCursados2.id_aluno_cursante;
            alunoAulasCursadas4.id_aula = FrontEndAulaId2;
            AlunoAulasCursadasModel alunoAulasCursadas12 = new AlunoAulasCursadasModel();
            alunoAulasCursadas12.st_status = "Não concluido";
            alunoAulasCursadas12.id_aluno_cursante = alunoCursosCursados2.id_aluno_cursante;
            alunoAulasCursadas12.id_aula = FrontEndAulaId3;

            AlunoAulasCursadasModel alunoAulasCursadas5 = new AlunoAulasCursadasModel();
            alunoAulasCursadas5.st_status = "Não concluido";
            alunoAulasCursadas5.id_aluno_cursante = alunoCursosCursados3.id_aluno_cursante;
            alunoAulasCursadas5.id_aula = DatabaseAulaId;
            AlunoAulasCursadasModel alunoAulasCursadas6 = new AlunoAulasCursadasModel();
            alunoAulasCursadas6.st_status = "Não concluido";
            alunoAulasCursadas6.id_aluno_cursante = alunoCursosCursados3.id_aluno_cursante;
            alunoAulasCursadas6.id_aula = DatabaseAulaId2;
            AlunoAulasCursadasModel alunoAulasCursadas13 = new AlunoAulasCursadasModel();
            alunoAulasCursadas13.st_status = "Não concluido";
            alunoAulasCursadas13.id_aluno_cursante = alunoCursosCursados3.id_aluno_cursante;
            alunoAulasCursadas13.id_aula = DatabaseAulaId3;

            AlunoAulasCursadasModel alunoAulasCursadas7 = new AlunoAulasCursadasModel();
            alunoAulasCursadas7.st_status = "Não concluido";
            alunoAulasCursadas7.id_aluno_cursante = alunoCursosCursados4.id_aluno_cursante;
            alunoAulasCursadas7.id_aula = LogicaAulaId;
            AlunoAulasCursadasModel alunoAulasCursadas8 = new AlunoAulasCursadasModel();
            alunoAulasCursadas8.st_status = "Não concluido";
            alunoAulasCursadas8.id_aluno_cursante = alunoCursosCursados4.id_aluno_cursante;
            alunoAulasCursadas8.id_aula = LogicaAulaId2;
            AlunoAulasCursadasModel alunoAulasCursadas14 = new AlunoAulasCursadasModel();
            alunoAulasCursadas14.st_status = "Não concluido";
            alunoAulasCursadas14.id_aluno_cursante = alunoCursosCursados4.id_aluno_cursante;
            alunoAulasCursadas14.id_aula = LogicaAulaId3;

            AlunoAulasCursadasModel alunoAulasCursadas9 = new AlunoAulasCursadasModel();
            alunoAulasCursadas9.st_status = "Não concluido";
            alunoAulasCursadas9.id_aluno_cursante = alunoCursosCursados5.id_aluno_cursante;
            alunoAulasCursadas9.id_aula = CloudAulaId;
            AlunoAulasCursadasModel alunoAulasCursadas10 = new AlunoAulasCursadasModel();
            alunoAulasCursadas10.st_status = "Não concluido";
            alunoAulasCursadas10.id_aluno_cursante = alunoCursosCursados5.id_aluno_cursante;
            alunoAulasCursadas10.id_aula = CloudAulaId2;
            AlunoAulasCursadasModel alunoAulasCursadas15 = new AlunoAulasCursadasModel();
            alunoAulasCursadas15.st_status = "Não concluido";
            alunoAulasCursadas15.id_aluno_cursante = alunoCursosCursados5.id_aluno_cursante;
            alunoAulasCursadas15.id_aula = CloudAulaId3;


                
            listaAlunoAulasCursadas.Add(alunoAulasCursadas);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas2);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas3);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas4);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas5);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas6);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas7);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas8);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas9);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas10);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas11);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas12);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas13);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas14);
            listaAlunoAulasCursadas.Add(alunoAulasCursadas15);

            dataBaseContext.AlunoAulasCursadas.AddRange(listaAlunoAulasCursadas);

            dataBaseContext.SaveChanges();

            return Ok("Dados populado com sucesso");
        }

        [HttpDelete("4-deletarPopulados")]
        public IActionResult DeletarRelacoesPopulados()
        {
            try
            {
                var aluno1 = alunoRepository.consultarAlunosPorEmailNT("email@email.com");
                var aluno2 = alunoRepository.consultarAlunosPorEmailNT("email2@email.com");
                var aluno3 = alunoRepository.consultarAlunosPorEmailNT("email3@email.com");
                var aluno4 = alunoRepository.consultarAlunosPorEmailNT("email4@email.com");
                var aluno5 = alunoRepository.consultarAlunosPorEmailNT("email5@email.com");

                var listaAlunoCursosCursados = dataBaseContext.AlunoCursosCursados
                    .Include(a => a.aulas_cursadas)
                    .Where(a => a.id_aluno == aluno1.id_aluno || a.id_aluno == aluno2.id_aluno || a.id_aluno == aluno3.id_aluno || a.id_aluno == aluno4.id_aluno || a.id_aluno == aluno5.id_aluno)
                    .ToList();

                dataBaseContext.AlunoCursosCursados.RemoveRange(listaAlunoCursosCursados);

                dataBaseContext.SaveChanges();


                return DeletarCursos();
            }
            catch (Exception e)
            {
                return BadRequest("Não foi possível deletar. Tabela pode não existir");
            }
        }

        [HttpDelete("5-deletarCursosPopulados")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult DeletarCursos()
        {
            var listaCursos = dataBaseContext.Cursos
                .Include(a => a.aulas)
                .ThenInclude(c => c.conteudo)
                .ThenInclude(q => q.quiz)
                .Where(dataBaseContext => dataBaseContext.nm_curso == "Back-End" || dataBaseContext.nm_curso == "Front-End" || dataBaseContext.nm_curso == "Database" || dataBaseContext.nm_curso == "Lógica de Programação" || dataBaseContext.nm_curso == "Cloud Computing")
                .ToList();

            dataBaseContext.Cursos.RemoveRange(listaCursos);


            dataBaseContext.SaveChanges();

            return DeletarAlunosPopulados();
        }

        [HttpDelete("6-deletarAlunosPopulados")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult DeletarAlunosPopulados()
        {
            var aluno1 = alunoRepository.consultarAlunosPorEmailNT("email@email.com");
            var aluno2 = alunoRepository.consultarAlunosPorEmailNT("email2@email.com");
            var aluno3 = alunoRepository.consultarAlunosPorEmailNT("email3@email.com");
            var aluno4 = alunoRepository.consultarAlunosPorEmailNT("email4@email.com");
            var aluno5 = alunoRepository.consultarAlunosPorEmailNT("email5@email.com");

            var listaAlunos = dataBaseContext.Alunos
                .Where(a => a.id_aluno == aluno1.id_aluno || a.id_aluno == aluno2.id_aluno || a.id_aluno == aluno3.id_aluno || a.id_aluno == aluno4.id_aluno || a.id_aluno == aluno5.id_aluno)
                .ToList();

            dataBaseContext.Alunos.RemoveRange(listaAlunos);

            dataBaseContext.SaveChanges();

            return Ok("Dados populados deletados com sucesso");
        }

    }
}
