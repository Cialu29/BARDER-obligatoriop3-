using DataAccess.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repositories
{
    public class FotoRepository
    {
        private readonly Barderbd _context;

        public FotoRepository(Barderbd context)
        {
            this._context = context;
        }

        public bool AnyFoto(int idF)
        {
            return this._context.Fotoes.Any(e => e.idFoto == idF);
        }

        public Foto GetFotoById(int idF)
        {
            return this._context.Fotoes.FirstOrDefault(e => e.idFoto == idF);
        }

        public void AddFoto(Foto foto)
        {
            this._context.Fotoes.Add(foto);
        }

        public void RemoveFoto(Foto foto)
        {
            this._context.Fotoes.Remove(foto);
        }
    }
}
