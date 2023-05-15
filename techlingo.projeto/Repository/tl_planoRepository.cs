using techlingo.projeto.Models;
using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Repository.Context;
using System.Numerics;

namespace techlingo.projeto.Repository
{
    public class tl_planoRepository
    {

        private readonly DataBaseContext dataBaseContext;

        public tl_planoRepository(DataBaseContext ctx)
        {
            dataBaseContext = ctx;
        }

        public IList<tl_planoModel> Listar()
        {
            var lista = dataBaseContext.tl_plano
                        .ToList<tl_planoModel>();

            return lista;

        }

        public tl_planoModel Consultar(int id)
        {

            var plano = new tl_planoModel();

            plano = dataBaseContext.tl_plano
                        .Where(c => c.id_plano == id)
                            .FirstOrDefault();

            return plano;

        }

        public void Inserir(tl_planoModel plano)
        {
            dataBaseContext.tl_plano.Add(plano);
            dataBaseContext.SaveChanges();
        }

        public void Alterar(tl_planoModel plano)
        {
            dataBaseContext.tl_plano.Update(plano);
            dataBaseContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var plano = new tl_planoModel { id_plano = id };

            dataBaseContext.tl_plano.Remove(plano);
            dataBaseContext.SaveChanges();

        }

    }
}
