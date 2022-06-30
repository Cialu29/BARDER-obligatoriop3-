using DataAccess.DataBase;
using BusinessLogic.DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel
{
    public class UnitOfWork : IDisposable
    {
        public void Dispose()
        {
            if(this.Transction != null)
            {
                this.Transction.Dispose();
            }
            this._context.Dispose();
        }

        protected readonly Barderbd _context;
        protected DbContextTransaction Transction;

        public BardosRepository BardosRepository { get; set; }
        public FotoRepository FotosRepository { get; set; }
        public UsuariosRepository UsuariosRepository { get; set; }
        public UsuariosBardosRepository UsuariosBardosRepository { get; set; }

        public UnitOfWork()
        {
            this._context = new Barderbd();

            this.BardosRepository = new BardosRepository(this._context);
            this.FotosRepository = new FotoRepository(this._context);
            this.UsuariosRepository = new UsuariosRepository(this._context);
            this.UsuariosBardosRepository = new UsuariosBardosRepository(this._context);
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public void BeginTransaction()
        {
            this.Transction = this._context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            if (this.Transction != null)
            {
                this.Transction.Commit();
            }
        }

        public void Rollback()
        {
            if (this.Transction != null)
            {
                this.Transction.Rollback();
            }
        }
    }
}
