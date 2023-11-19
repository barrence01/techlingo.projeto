using techlingo.projeto.Repository.Context;
using techlingo.projeto.Models.Aluno;
using Microsoft.EntityFrameworkCore;

namespace techlingo.projeto.Repository
{
    public class AlunoCursosCursadosRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public AlunoCursosCursadosRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<AlunoCursosCursadosModel> listarAlunoCursosCursadosNT(int id)
        {

            var lista = dataBaseContext.AlunoCursosCursados.AsNoTracking()
                .Where(a => a.id_aluno == id)
                .ToList();
            return lista;
        }

        public AlunoCursosCursadosModel alunoCursosCursadosNT(int id_aluno, string nome_curso)
        {
            var curso = dataBaseContext.AlunoCursosCursados.AsNoTracking()
                .Include(c => c.aulas_cursadas)
                .ThenInclude(a => a.aula)
                .Where(a => a.id_aluno == id_aluno && a.curso.nm_curso.Equals(nome_curso))
                .FirstOrDefault();

            return curso;
        }


        public AlunoCursosCursadosModel alunoCursosCursadosIdNT(int id_aluno, string nome_curso)
        {
            var curso = dataBaseContext.AlunoCursosCursados
                .Where(a => a.id_aluno == id_aluno && a.curso.nm_curso.Equals(nome_curso))
                .FirstOrDefault();

            return curso;
        }

        public int alunoCursosCursadosCountNT(int id, string nome_curso)
        {
            var contador = dataBaseContext.AlunoCursosCursados.AsNoTracking()
                .Include(c => c.aulas_cursadas)
                .ThenInclude(a => a.aula)
                .Where(a => a.id_aluno == id && a.curso.nm_curso.Equals(nome_curso))
                .Count();

            return contador;
        }

        public void Inserir(AlunoCursosCursadosModel alunoCursosCursados)
        {
            dataBaseContext.AlunoCursosCursados.Add(alunoCursosCursados);
            dataBaseContext.SaveChanges();
        }

        public void Editar(AlunoCursosCursadosModel alunoCursosCursados)
        {
            dataBaseContext.AlunoCursosCursados.Update(alunoCursosCursados);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(AlunoCursosCursadosModel alunoCursosCursados)
        {
            dataBaseContext.AlunoCursosCursados.Remove(alunoCursosCursados);
            dataBaseContext.SaveChanges();
        }

    }
}
