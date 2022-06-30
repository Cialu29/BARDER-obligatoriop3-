using BusinessLogic.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Seguridad
{
    public class SecurityService
    {
        public string GenerarSalt(int tamano)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buffer = new byte[tamano];

            rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public string GenerarHashSHA256(string plainString, string salt)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoder = Encoding.UTF8;
                Byte[] bytes = hash.ComputeHash(encoder.GetBytes(plainString));

                foreach (Byte bite in bytes)
                {
                    stringBuilder.Append(bite.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public bool ValidarUsuario(string mail, string contrasenia)
        {
            bool usuarioEsValido = false;

            using (UnitOfWork uow = new UnitOfWork())
            {
                //Obtener email
                var email = uow.UsuariosRepository.GetUsuarioByMail(mail);

                if (email != null)
                {
                    //Hashear contrasenia
                    string hashedContrasenia = this.GenerarHashSHA256(contrasenia, email.Contrasenia);

                    //Comparar contrasenia
                    if (email.Contrasenia == hashedContrasenia)
                    {
                        usuarioEsValido = true;
                    }
                }
            }

            return usuarioEsValido;
        }
    }
}
