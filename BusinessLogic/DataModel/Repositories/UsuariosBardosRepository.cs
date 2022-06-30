using DataAccess.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repositories
{
    public class UsuariosBardosRepository
    {
        private readonly Barderbd _contex;

        public UsuariosBardosRepository(Barderbd context)
        {
            this._contex = context;
        }

        public bool AnyUB(int idB)
        {
            return this._contex.UsuarioBardoes.Any(e => e.idBardo == idB);
        }

        public UsuarioBardo GetUBById(int idB)
        {
            return this._contex.UsuarioBardoes.FirstOrDefault(e => e.idBardo == idB);
        }

        public void AddUB(UsuarioBardo usuarioBardo)
        {
            this._contex.UsuarioBardoes.Add(usuarioBardo);
        }

        public void RemoveUB(UsuarioBardo usuarioBardo)
        {
            this._contex.UsuarioBardoes.Remove(usuarioBardo);
        }
    }
}
