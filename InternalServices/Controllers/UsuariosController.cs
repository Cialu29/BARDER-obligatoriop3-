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

        [HttpPost]
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
                            string fotoURL = ConfigurationManager.AppSettings["FOTO_FILE_PATH"] + "nombre.jpg";

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
        IHttpActionResult Autenticar([FromBody] UsuarioModel usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest("Informacion de autenticacion requerida");
                }

                //Validacion de credenciales
                SecurityService securityService = new SecurityService();
                bool credencialesValida = securityService.ValidarUsuario(usuario.Email, usuario.Contrasenia);

                if (credencialesValida)
                {
                    //Generar token
                    string token = TokenGenerator.GenerateToken(usuario.Email);
                    return Ok(token);
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