using BusinessLogic.DataModel;
using DataAccess.DataBase;
using InternalServices.Models.Bardo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InternalServices.Controllers
{
    [Authorize]
    public class BardosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetBardosById(int idB)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var bardo = uow.BardosRepository.GetBardoById(idB);

                    if (bardo == null)
                    {
                        return NotFound();
                    }

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(bardo);
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult AddBardo([FromBody] BardoModel bardo)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var bardoEntity = new Bardo
                    {
                        idBardo = bardo.idBardo,
                        Estado = bardo.Estado,
                        Ganador = bardo.Ganador
                    };

                    uow.BardosRepository.AddBardo(bardoEntity);

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(bardoEntity);
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateBardo([FromBody] BardoModel bardo)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var bardoEntity = uow.BardosRepository.GetBardoById(bardo.idBardo);

                    if (bardoEntity == null)
                    {
                        return NotFound();
                    }

                    bardoEntity.Estado = bardo.Estado;

                    uow.SaveChanges();
                    uow.Commit();

                    return Ok(bardoEntity);
                }
                catch(Exception ex)
                {
                    uow.Rollback();
                    return InternalServerError(ex);
                }
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteBardo(int idB)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                try
                {
                    var bardoEntity = uow.BardosRepository.GetBardoById(idB);

                    if(bardoEntity == null)
                    {
                        return NotFound();
                    }

                    uow.BardosRepository.RemoveBardo(bardoEntity);

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