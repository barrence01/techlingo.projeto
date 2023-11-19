
using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Controllers.DTO.Aluno;
using techlingo.projeto.Models.Aluno;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class AlunoRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public AlunoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<AlunoModel> listarAlunosNT()
        {
            var lista = new List<AlunoModel>();
            lista = dataBaseContext.Alunos.AsNoTracking()
                .Include(p => p.plano)
                .ToList();
            return lista;
        }

        public IList<AlunoModel> listarAlunosCompletoNT()
        {
            var lista = new List<AlunoModel>();
            lista = dataBaseContext.Alunos.AsNoTracking()
                .Include(p => p.plano)
                .Include(c => c.cursosCursados)
                .ThenInclude(a => a.aulas_cursadas)
                .ToList();
            return lista;
        }

        public AlunoModel consultarAlunosIdNT(int id)
        {
            var aluno = dataBaseContext.Alunos.AsNoTracking()
                .Where(a => a.id_aluno == id)
                .Include(p => p.plano)
                .FirstOrDefault();

            return aluno;
        }

        public AlunoModel consultarAlunosPorEmailNT(string email)
        {
            var aluno = dataBaseContext.Alunos.AsNoTracking()
                .Where(a => a.ds_email.Contains(email))
                .FirstOrDefault();

            return aluno;
        }

        public int consultarAlunosExistentePorEmailNT(string email)
        {
            var contador = dataBaseContext.Alunos.AsNoTracking()
                .Where(a => a.ds_email.Contains(email))
                .Count();

            return contador;
        }

        public AlunoModel consultarAlunosNomeNT(string nome)
        {
            var aluno = dataBaseContext.Alunos.AsNoTracking()
                .Where(a => a.nm_aluno.Contains(nome))
                .Include(p => p.plano)
                .FirstOrDefault();

            return aluno;
        }

        public void Inserir(AlunoModel aluno)
        {
            dataBaseContext.Alunos.Add(aluno);
            dataBaseContext.SaveChanges();
        }

        public void InserirRange(List<AlunoModel> aluno)
        {
            dataBaseContext.Alunos.AddRange(aluno);
            dataBaseContext.SaveChanges();
        }

        public AlunoModel LoginAluno (string email, string senha)
        {
            var aluno = dataBaseContext.Alunos.AsNoTracking()
                .Where(a => a.ds_email.ToLower().Equals(email.ToLower()))
                .Where(a => a.ds_senha.ToLower().Equals(senha.ToLower()))
                .FirstOrDefault();

            return aluno;
        }


    }


}
