using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Models.Cursos;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class AulasRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public AulasRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<AulasModel> listarAulasNT()
        {
            var lista = new List<AulasModel>();
            lista = dataBaseContext.Aulas.AsNoTracking()
                .ToList();
            return lista;
        }

        public AulasModel consultarAulaPorIdNT(int id)
        {
            var aula = dataBaseContext.Aulas.AsNoTracking()
                .Where(a => a.id_aula == id)
                .Include(a => a.conteudo)
                .FirstOrDefault();

            return aula;
        }

        public int consultarAulasExistentePorNomeIdNT(string nome, int id_curso)
        {
            var contador = dataBaseContext.Aulas.AsNoTracking()
                .Where(a => a.nm_aula.Contains(nome) && a.id_curso == id_curso)
                .Count();

            return contador;

        }

        public AulasModel consultarAulasPorIdCurso(int nr_aula, int id_curso)
        {
            var aula = dataBaseContext.Aulas
                .Include(a => a.conteudo)
                .ThenInclude(c => c.quiz)
                .Where(a => a.id_curso == id_curso && a.nr_aula == nr_aula)
                .OrderBy(a => a.nr_aula)
                .FirstOrDefault();

            return aula;

        }

        public int consultarAulasPorNumeroIdNT(int nr_aula, int id_curso)
        {
            var aula = dataBaseContext.Aulas.AsNoTracking()
                .Where(a => a.nr_aula == nr_aula && a.id_curso == id_curso)
                .FirstOrDefault();

            return aula.id_aula;

        }

        public void Inserir(AulasModel aula)
        {
            dataBaseContext.Aulas.Add(aula);
            dataBaseContext.SaveChanges();
        }
    }
}
