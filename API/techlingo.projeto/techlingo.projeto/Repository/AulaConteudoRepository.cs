using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Models.Cursos;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class AulaConteudoRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public AulaConteudoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<AulaConteudoModel> listarAulasConteudoNT()
        {
            var lista = new List<AulaConteudoModel>();
            lista = dataBaseContext.AulaConteudo.AsNoTracking()
                .Include(a => a.quiz)
                .ToList();
            return lista;
        }

        public AulaConteudoModel consultarAulasConteudoPorIdNT(int id)
        {
            var aulaConteudo = dataBaseContext.AulaConteudo.AsNoTracking()
                .Where(a => a.id_aula_conteudo == id)
                .FirstOrDefault();

            return aulaConteudo;
        }

        public int consultarAulasConteudoExistentePorIdsNT(int id_aula)
        {
            var contador = dataBaseContext.AulaConteudo.AsNoTracking()
                .Where(a => a.id_aula == id_aula)
                .Count();

            return contador;

        }

        public void Inserir(AulaConteudoModel aulaConteudo)
        {
            dataBaseContext.AulaConteudo.Add(aulaConteudo);
            dataBaseContext.SaveChanges();
        }

    }
}
