using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Models.Cursos;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class CursosRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public CursosRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<CursosModel> listarCursosNT()
        {
            var lista = new List<CursosModel>();
            lista = dataBaseContext.Cursos.AsNoTracking()
                .ToList();
            return lista;
        }

        public IList<CursosModel> listarCursosCompletoNT()
        {
            var lista = new List<CursosModel>();
            lista = dataBaseContext.Cursos.AsNoTracking()
                .Include(c => c.aulas)
                .ThenInclude(a => a.conteudo)
                .ThenInclude(c => c.quiz)
                .ToList();
            return lista;
        }

        public CursosModel consultarCursoPorIdNT(int id)
        {
              var curso = dataBaseContext.Cursos.AsNoTracking()
                .Where(c => c.id_curso == id)
                .Include(c => c.aulas)
                .ThenInclude(a => a.conteudo)
                .ThenInclude(c => c.quiz)
                .FirstOrDefault();

            return curso;
        }

        public int consultarCursosExistentePorNomeNT(string nome)
        {
            var contador = dataBaseContext.Cursos.AsNoTracking()
                .Where(a => a.nm_curso.Contains(nome))
                .Count();

            return contador;

        }

        public CursosModel consultarCursosPorNomeNT(string nome)
        {
            var curso = dataBaseContext.Cursos.AsNoTracking()
                .Where(a => a.nm_curso.Contains(nome))
                .FirstOrDefault();

            return curso;

        }

        public void Inserir(CursosModel curso)
        {
            dataBaseContext.Cursos.Add(curso);
            dataBaseContext.SaveChanges();
        }


    }
}
