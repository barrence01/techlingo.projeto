using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Models;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class tl_alunoRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public tl_alunoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<tl_alunoModel> Listar()
        {
            var lista = new List<tl_alunoModel>();
            lista = dataBaseContext.tl_aluno
                .Include(c => c.plano)
                .Include(c => c.Cursando)
                    .ThenInclude(x => x.curso) //inclui a table mencionada no aluno_cursos
                .ToList<tl_alunoModel>();
            return lista;
        }

        public IList<tl_alunoModel> ListarOrdenadoPorNome()
        {
            var lista = new List<tl_alunoModel>();

            lista = dataBaseContext
                .tl_aluno
                .OrderBy(r => r.nm_aluno).ToList<tl_alunoModel>();

            return lista;
        }

        public IList<tl_alunoModel> ListarOrdenadoPorNomeDescendente()
        {
            var lista = new List<tl_alunoModel>();

            lista = dataBaseContext
                .tl_aluno
                .OrderByDescending(r => r.nm_aluno).ToList<tl_alunoModel>();

            return lista;
        }

        public tl_alunoModel ConsultarPorCpf(String cpf)
        {
            var aluno = dataBaseContext.tl_aluno.
                    Where(r => r.nr_cpf == cpf).
                        FirstOrDefault<tl_alunoModel>();

            return aluno;
        }

        public tl_alunoModel ConsultarPorParteNome(String nomeParcial)
        {
            var aluno = dataBaseContext.tl_aluno.
                    Where(r => r.nm_aluno.Contains(nomeParcial)).
                        FirstOrDefault<tl_alunoModel>();

            return aluno;
        }

        public tl_alunoModel Consultar(int id)
        {
            var aluno = dataBaseContext.tl_aluno
                .Where(c => c.id_aluno == id)
                .Include(c => c.Cursando)
                    .ThenInclude(x => x.curso) //inclui a table mencionada no aluno_cursos
                .Include(c => c.plano).First();

            return aluno;
        }

        public void Inserir(tl_alunoModel aluno)
        {
            dataBaseContext.tl_aluno.Add(aluno);
            dataBaseContext.SaveChanges();
        }
        public void Alterar(tl_alunoModel aluno)
        {
            dataBaseContext.tl_aluno.Update(aluno);
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
