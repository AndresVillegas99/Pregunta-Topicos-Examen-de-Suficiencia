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

using System.Web.UI.WebControls;
using AutoMapper;
using WebApplication1.Models;
using Pregunta_Topicos_Examen_de_Suficiencia.WebApplication1.Models;
using Pregunta_Topicos_Examen_de_Suficiencia.Models;

namespace Pregunta_Topicos_Examen_de_Suficiencia.Controllers
{
    public class EmployeesController : ApiController
    {
        private NorthWndEntities db = new NorthWndEntities();
        private readonly NorthWndContext _context = new NorthWndContext();
        private MetodosEmployee ME = new MetodosEmployee();

        /*public EmployeesController(NorthWndContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }*/

        public static void ConfigureApis(HttpConfiguration config)
        {
            config.Formatters.Add(new JsonPatchFormatter());
        }


        [HttpGet]
        public List<EmployeeDetails> GetEmployee(string nombre, string apellido)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDetails>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);

            var lista = ME.GetEmployees(nombre, apellido);


            List<EmployeeDetails> listNombre = new List<EmployeeDetails>();
            foreach (var empleado in lista)
            {
                var DetallesNombre = mapa.Map<EmployeeDetails>(empleado);
                DetallesNombre.NombreCompleto = NameFormat(empleado);
                listNombre.Add(DetallesNombre);

            };



            return listNombre;

        }
        // * Este metodo GET trae una lista de todos los empleados
        /*   [HttpGet]
          public List<EmployeeDetails> GetEmployees()
          {
              var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDetails>());
              IMapper iMapper = config.CreateMapper();
              var mapa = new Mapper(config);
              var lista = ME.GetAllEmployees();
              List<NombreCompleto> nombresCompletos = new List<NombreCompleto>();

              List<EmployeeDetails> listNombre = new List<EmployeeDetails>();
              foreach (var empleado in lista)
              {
                  var DetallesNombre = mapa.Map<EmployeeDetails>(empleado);
                  DetallesNombre.NombreCompleto = empleado.TitleOfCourtesy + empleado.FirstName + " " + empleado.LastName + "(" + empleado.Title + ")";
                  listNombre.Add(DetallesNombre);

              };



              return listNombre;

          }*/

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {

            CambiarEmpleado(id, employee);
            return Ok();

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
        }
        private IHttpActionResult CambiarEmpleado(int id, Employee employee)
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



        

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            
            ME.EliminarEmpleado(id);
            return Ok();
        }

        /*public IHttpActionResult EliminarEmpleado(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }*/


        /*public IHttpActionResult ActualizarDetalleParcialmente(int id,
            JsonPatchDocument<Employee> patchDoc)
        {
            var Empleado = db.Employees.Find(id);

            if (Empleado == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDetailsForUpdate>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);
            

            int IdOriginal = Empleado.EmployeeID;
            patchDoc.ApplyTo(Empleado);


            // convierta el objeto parchado a uno que pueda asignar en una línea
            var elDetalleParaActualizar = mapa.Map<EmployeeDetailsForUpdate>(Empleado);



            if (Empleado.EmployeeID != IdOriginal)
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
        }*/

        //PATCH
        [ResponseType(typeof(Employee))]
        [HttpPatch]
        public IHttpActionResult PartiallyUpdateOrderDetail(int id,
            [FromBody] JsonPatchDocument<Employee> patchedOrderDetail)
        {
            ME.ActualizarDetalleParcialmente(id,
                patchedOrderDetail);
            return Ok();
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

        private string NameFormat(Employee Detalles)
        {
            string todo = Detalles.TitleOfCourtesy + Detalles.FirstName + " " + Detalles.LastName + "(" + Detalles.Title + ")";
            return todo;
        }

    }
}