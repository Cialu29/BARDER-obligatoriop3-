using BusinessLogic.DataModel;
using BusinessLogic.Service.Seguridad;
using DataAccess.DataBase;
using InternalServices.Models.Usuario;
using InternalServices.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InternalServices.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
       [HttpGet]
       [Route("api/Uusario/GetUsuarioById")]
       public IHttpActionResult GetUsuarioById(int idU)
       {
            using(var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var usuario = uow.UsuariosRepository.GetUsuarioById(idU);

                    if (usuario == null)
                    {
                        return NotFound();
                    }

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(usuario);
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
       }

        [HttpGet]
        [Route("api/Usuario/ExistUsuarioByEmail")]
        public IHttpActionResult ExistUsuarioByEmail(string email)
        {

        }

        [HttpGet]
        [Route("api/Usuario/GetUsuarios")]
        public IHttpActionResult GetUsuarios()
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    var usuarios = uow.UsuariosRepository.GetUsuarios();

                    if (usuarios == null)
                    {
                        return NotFound();
                    }

                    return Ok(usuarios);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/Usuario/AddUsuario")]
        public IHttpActionResult AddUsuario([FromBody] UsuarioModel usuario)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    SecurityService security = new SecurityService();

                    //Generar Salt
                    string salt = security.GenerarSalt(10);

                    //Hashear Contrasenia
                    string hashedC = security.GenerarHashSHA256(usuario.Contrasenia, salt);

                    //Crear usuario
                    var usuarioEntity = new Usuario
                    {
                        idUsuario = usuario.idUsuario,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Ciudad = usuario.Ciudad,
                        Pais = usuario.Pais,
                        Email = usuario.Email,
                        Resumen = usuario.Resumen,
                        Contrasenia = hashedC,
                        Salt = salt
                    };

                    uow.UsuariosRepository.AddUsuario(usuarioEntity);

                    uow.SaveChanges();

                    //Fotos manejo
                    if(usuario.Fotoes != null)
                    {
                        foreach(var foto in usuario.Fotoes)
                        {
                            byte[] fotoBytes = Convert.FromBase64String(foto);
                            string fotoURL = ConfigurationManager.AppSettings["FOTO_FILE_URL"] + "nombre.jpg";

                            File.WriteAllBytes(fotoURL, fotoBytes);

                            //Guardar foto BD
                            Foto fotoEntity = new Foto()
                            {
                                idUsuario = usuarioEntity.idUsuario,
                                URL = fotoURL
                            };

                            uow.FotosRepository.AddFoto(fotoEntity);
                        }
                    }

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok();
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPost]
        [Route("api/Usuario/Autenticar")]
        public IHttpActionResult Autenticar([FromBody] UsuarioModel usuario)
        {
            try
            {
                SecurityService securityService = new SecurityService();
                var usuarioValido = securityService.ValidarUsuario(usuario.Email, usuario.Contrasenia);

                if (usuarioValido != null)
                {
                    //Generar token
                    string token = TokenGenerator.GenerateToken(usuario.Email);
                    usuarioValido.Token = token;

                    return Ok(usuarioValido);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/Usuario/UpdateUsuario")]
        public IHttpActionResult UpdateUsuario([FromBody] UsuarioModel usuario)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var usuarioEntity = uow.UsuariosRepository.GetUsuarioById(usuario.idUsuario);

                    if (usuarioEntity == null)
                    {
                        return NotFound();
                    }

                    usuarioEntity.Fotoes = (ICollection<Foto>)usuario.Fotoes;

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(usuarioEntity);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpDelete]
        [Route("api/Usuario/RemoveUsuario")]
        public IHttpActionResult RemoveUsuario(int idU)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var usuarioEntity = uow.UsuariosRepository.GetUsuarioById(idU);

                    if(usuarioEntity == null)
                    {
                        return NotFound();
                    }

                    uow.UsuariosRepository.RemoveUsuario(usuarioEntity);

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok();
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }
    }
}