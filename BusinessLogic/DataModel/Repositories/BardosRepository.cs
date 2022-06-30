using DataAccess.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repositories
{
    public class BardosRepository
    {
        private readonly Barderbd _context;

        public BardosRepository(Barderbd context)
        {
            this._context = context;
        }

        public bool AnyBardo(int idB)
        {
            return this._context.Bardoes.Any(e => e.idBardo == idB);
        }

        public Bardo GetBardoById(int idB)
        {
            return this._context.Bardoes.FirstOrDefault(e => e.idBardo == idB);
        }

        public void AddBardo(Bardo bardo)
        {
            this._context.Bardoes.Add(bardo);
        }

        public void RemoveBardo(Bardo bardo)
        {
            this._context.Bardoes.Remove(bardo);
        }
    }
}
