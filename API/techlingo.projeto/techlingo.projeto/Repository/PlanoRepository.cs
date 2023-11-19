using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Models.Planos;
using techlingo.projeto.Repository.Context;

namespace techlingo.projeto.Repository
{
    public class PlanoRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public PlanoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<PlanoModel> listarPlanosNT()
        {
            var lista = new List<PlanoModel>();
            lista = dataBaseContext.Planos.AsNoTracking()
                .ToList();
            return lista;
        }

        public IDictionary<string?, decimal?> listaDePlanosNT()
        {
            var lista = dataBaseContext.Planos.AsNoTracking()
                .Select(a => new { a.nm_plano, a.vl_plano })
                .ToDictionary(a => a.nm_plano, a => a.vl_plano);
                
            return lista;
        }

        public PlanoModel consultarPlanosIdNT(int id)
        {
            var plano = dataBaseContext.Planos.AsNoTracking()
                .Where(a => a.id_plano == id)
                .FirstOrDefault();

            return plano;
        }

        public int consultarPlanosExistentePorNomeNT(string nome)
        {
            var plano = dataBaseContext.Planos.AsNoTracking()
                .Where(a => a.nm_plano.Contains(nome))
                .Count();

            return plano;
        }

        public PlanoModel consultarPlanosNomeNT(string nome)
        {
            var plano = dataBaseContext.Planos.AsNoTracking()
                .Where(a => a.nm_plano.Contains(nome))
                .FirstOrDefault();

            return plano;
        }

        public void Inserir(PlanoModel plano)
        {
            dataBaseContext.Planos.Add(plano);
            dataBaseContext.SaveChanges();
        }
    }
}
