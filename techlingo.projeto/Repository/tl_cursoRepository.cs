using System.Linq;
using techlingo.projeto.Models;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class tl_cursoRepository
    {

        private readonly DataBaseContext dataBaseContext;

        public tl_cursoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<tl_cursoModel> Listar()
        {
            var lista = dataBaseContext.tl_curso
                        .ToList<tl_cursoModel>();

            return lista;

        }

        public tl_cursoModel Consultar(int id)
        {

            var curso = new tl_cursoModel();

            curso = dataBaseContext.tl_curso
                        .Where(c => c.id_curso == id)
                            .FirstOrDefault();

            return curso;

        }

        public void Inserir(tl_cursoModel curso)
        {
            dataBaseContext.tl_curso.Add(curso);
            dataBaseContext.SaveChanges();
        }

        public void Alterar(tl_cursoModel curso)
        {
            dataBaseContext.tl_curso.Update(curso);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var curso = new tl_cursoModel { id_curso = id };

            dataBaseContext.tl_curso.Remove(curso);
            dataBaseContext.SaveChanges();

        }
    }
}
