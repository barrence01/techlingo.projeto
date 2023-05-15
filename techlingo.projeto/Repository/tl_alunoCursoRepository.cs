using Microsoft.EntityFrameworkCore;
using techlingo.projeto.DTO;
using techlingo.projeto.Models;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class tl_alunoCursoRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public tl_alunoCursoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }



        public tl_aluno_cursoModel Consultar(int id_aluno, int id_curso)
        {
            var alunoCurso = dataBaseContext.tl_aluno_cursoModel
                .Where(c => c.alunoId == id_aluno && c.cursoId == id_curso)
                .Include(c => c.tl_alunoModel)
                .Include(c => c.curso)
                .FirstOrDefault();

            return alunoCurso;
        }

        public void Alterar(AlterarAlunoCursoDto alunoCursoUpdate, tl_aluno_cursoModel alunoCursoModel)
        {

            alunoCursoModel.nr_nota = alunoCursoUpdate.nr_nota;
            alunoCursoModel.st_status = alunoCursoUpdate.st_status;
            alunoCursoModel.dt_conclusao = alunoCursoUpdate.dt_conclusao;


            dataBaseContext.tl_aluno_cursoModel.Update(alunoCursoModel);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var aluno = new tl_alunoModel(id, "", "", "");

            dataBaseContext.tl_aluno.Remove(aluno);
            dataBaseContext.SaveChanges();
        }

    }
}
