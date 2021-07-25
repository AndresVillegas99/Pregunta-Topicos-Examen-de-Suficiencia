using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JsonPatch.Formatting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using NorthWndData;
using WebApplication1;
using AutoMapper;
using WebApplication1.Models;

namespace Pregunta_Topicos_Examen_de_Suficiencia.Controllers
{
    public class EmployeesController : ApiController
    {
        private NorthWndEntities db = new NorthWndEntities();


        public static void ConfigureApis(HttpConfiguration config)
        {
            config.Formatters.Add(new JsonPatchFormatter());
        }
        // GET: api/Employees1
        public List<Employee> GetEmployees()
        {
            var getEmpleados = db.Employees.ToList();
            var res = new List<Employee>();
            foreach (var emp in getEmpleados)
            {
                res.Add(new Employee()
                {
                    EmployeeID = emp.EmployeeID,
                    FirstName = emp.FirstName,
                    LastName= emp.LastName
                });
            }
            return res;
        }

        // GET: api/Employees1/5
        [ResponseType(typeof(Employee))]
        public List<Employee> GetEmployee(int id)
        {
            var getEmpleados = db.Employees.ToList();
            Employee employee = db.Employees.Find(id);
            var res = new List<Employee>();
            foreach (var emp in getEmpleados)
            {
                if(id == emp.EmployeeID)
                {
                     employee = emp;
                    res.Add( new Employee(){
                        EmployeeID = emp.EmployeeID,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName
                }); 
                }
            }
            
           

            return res;
        }

        // PUT: api/Employees1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            /*catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }*/
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees1
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employees1/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }


        private IHttpActionResult ActualizarDetalleParcialmente(Employee laOrden,
            JsonPatchDocument<Employee> patchDoc)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDetailsForUpdate>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);
            

            int IdOriginal = laOrden.EmployeeID;
            patchDoc.ApplyTo(laOrden);


            // convierta el objeto parchado a uno que pueda asignar en una línea
            var elDetalleParaActualizar = mapa.Map<EmployeeDetailsForUpdate>(laOrden);



            if (laOrden.EmployeeID != IdOriginal)
            {
                return Content(HttpStatusCode.BadRequest, "No es permitido cambiar el ID");
            }
            
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        //PATCH
        [ResponseType(typeof(Employee))]
        [HttpPatch]
        public IHttpActionResult PartiallyUpdateOrderDetail(int id,
            [FromBody] JsonPatchDocument<Employee> patchedOrderDetail)
        {
            var Empleado = db.Employees.Find(id);
           
            if (Empleado == null)
                return NotFound();

           
            return   ActualizarDetalleParcialmente(Empleado, 
                patchedOrderDetail);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}