using BusinessLogic.DataModel;
using DataAccess.DataBase;
using InternalServices.Models.UsuarioBardo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InternalServices.Controllers
{
    [Authorize]
    public class UsuariosBardosController : ApiController
    {
       [HttpGet]
       [Route("api/UsuariosBardos/GetUBById")]
       public IHttpActionResult GetUBById(int idB)
       {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var ub = uow.UsuariosBardosRepository.GetUBById(idB);

                    if (ub == null)
                    {
                        return NotFound();
                    }

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(ub);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
       }

        [HttpPost]
        [Route("api/UsuariosBardos/AddUB")]
        public IHttpActionResult AddUB([FromBody] UsuarioBardoModel usuarioBardo)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var UbEntity = new UsuarioBardo
                    {
                        idBardo = usuarioBardo.idBardo,
                        idU1 = usuarioBardo.idU1,
                        idU2 = usuarioBardo.idU2
                    };

                    uow.UsuariosBardosRepository.AddUB(UbEntity);

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(UbEntity);
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPut]
        [Route("api/UsuariosBardos/UpdateUB")]
        public IHttpActionResult UpdateUB([FromBody] UsuarioBardoModel usuarioBardo)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var ubEntity = uow.UsuariosBardosRepository.GetUBById(usuarioBardo.idBardo);

                    if(ubEntity == null)
                    {
                        return NotFound();
                    }

                    ubEntity.idU1 = usuarioBardo.idU1;

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(ubEntity);
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }

            }
        }

        [HttpDelete]
        [Route("api/UsuariosBardos/RemoveUB")]
        public IHttpActionResult RemoveUB(int idB)
        {
            using(var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var ubEntity = uow.UsuariosBardosRepository.GetUBById(idB);

                    if(ubEntity == null)
                    {
                        return NotFound();
                    }

                    uow.UsuariosBardosRepository.RemoveUB(ubEntity);

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok();
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }
    }
}