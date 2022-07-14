using DataAccess.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel.Repositories
{
    public class UsuariosRepository
    {
        private readonly Barderbd _context;

        public UsuariosRepository(Barderbd context)
        {
            this._context = context;
        }

        public bool AnyUsuario(int idU)
        {
            return this._context.Usuarios.Any(e => e.idUsuario == idU);
        }

        public Usuario GetUsuarioById(int idU)
        {
            return this._context.Usuarios.FirstOrDefault(e => e.idUsuario == idU);
        }

        public Usuario GetUsuarios()
        {
            return this._context.Usuarios.Find();
        }

        public Usuario GetUsuarioByMail(string mail)
        {
            return this._context.Usuarios.FirstOrDefault(e => e.Email.ToLower() == mail.ToLower());
        }

        public void AddUsuario(Usuario usuario)
        {
            this._context.Usuarios.Add(usuario);
        }

        public void RemoveUsuario(Usuario usuario)
        {
            this._context.Usuarios.Remove(usuario);
        }
    }
}
