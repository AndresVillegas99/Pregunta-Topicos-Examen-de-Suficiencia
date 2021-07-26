using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using WebApplication1;


namespace NorthWndData
{
    public class MetodosEmployee
    {
        private NorthWndEntities db = new NorthWndEntities();
        

        // GET: api/Employees1
        public List<Employee> GetEmployees(string nombre, string apellido)
        {
            var getEmpleados = db.Employees.ToList();
           
            var res = new List<Employee>();
            var jefes = new List<int?>();
           

            foreach (var emp in getEmpleados)
            {
                if (emp.FirstName.Equals(nombre) && emp.LastName.Equals(apellido))
                res.Add(new Employee
                {
                    
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Title = emp.Title,
                    TitleOfCourtesy = emp.TitleOfCourtesy,
                    BirthDate = emp.BirthDate,
                    HireDate = emp.HireDate,
                    Address = emp.Address,
                    City = emp.City,
                    Region = emp.Region,
                    PostalCode = emp.PostalCode,
                    Country = emp.Country,
                    HomePhone = emp.HomePhone,
                    Extension = emp.Extension,
                    Photo = emp.Photo,
                    Notes = emp.Notes,
                    ReportsTo = emp.ReportsTo,
                    EmployeeID = emp.EmployeeID,
                    PhotoPath = emp.PhotoPath

                });
            }

            
            
            return res ;
        }

        public List<Employee> GetAllEmployees()
        {
            var getEmpleados = db.Employees.ToList();

            var res = new List<Employee>();
            var jefes = new List<int?>();
            

            foreach (var emp in getEmpleados)
            {
                res.Add(new Employee
                {

                   
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Title = emp.Title,
                    TitleOfCourtesy = emp.TitleOfCourtesy,
                    BirthDate = emp.BirthDate,
                    HireDate = emp.HireDate,
                    Address = emp.Address,
                    City = emp.City,
                    Region = emp.Region,
                    PostalCode = emp.PostalCode,
                    Country = emp.Country,
                    HomePhone = emp.HomePhone,
                    Extension = emp.Extension,
                    Photo = emp.Photo,
                    Notes = emp.Notes,
                    ReportsTo = emp.ReportsTo,
                    EmployeeID = emp.EmployeeID,
                    PhotoPath = emp.PhotoPath
                });
            }



            return res;
        }

        public List<Employee> GetAllBosses()
        {
            var getEmpleados = db.Employees.ToList();
           
            var res = new List<Employee>();
            var jefes = new List<int?>();
            
                 ;

            foreach (var emp in getEmpleados)
            {
                if (!jefes.Contains(emp.ReportsTo))
                {
                    jefes.Add(emp.ReportsTo);
                }
            }
            foreach (var emp in getEmpleados)
            {
                if (jefes.Contains(emp.EmployeeID))
                {
                    res.Add(new Employee
                    {

                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Title = emp.Title,
                        TitleOfCourtesy = emp.TitleOfCourtesy,
                        BirthDate = emp.BirthDate,
                        HireDate = emp.HireDate,
                        Address = emp.Address,
                        City = emp.City,
                        Region = emp.Region,
                        PostalCode = emp.PostalCode,
                        Country = emp.Country,
                        HomePhone = emp.HomePhone,
                        Extension = emp.Extension,
                        Photo = emp.Photo,
                        Notes = emp.Notes,
                        ReportsTo = emp.ReportsTo,
                        PhotoPath = emp.PhotoPath
                    });

                }
            }
            return res;
        }

        public List<Employee> GetBosses(string nombre, string apellido)
        {
            var getEmpleados = db.Employees.ToList();

            var res = new List<Employee>();
            var jefesID = new List<int?>();
            var jefes = new List<Employee>();



            foreach (var emp in getEmpleados)
            {
                if (!jefesID.Contains(emp.ReportsTo) && emp.ReportsTo != null)
                {
                    jefesID.Add(emp.ReportsTo);
                    jefes.Add(db.Employees.Find(emp.ReportsTo));
                }
            }
            foreach (var emp in jefes)
            {
                if (emp.FirstName.Equals(nombre) && emp.LastName.Equals(apellido)) { 
                    res.Add(new Employee
                    {

                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Title = emp.Title,
                        TitleOfCourtesy = emp.TitleOfCourtesy,
                        BirthDate = emp.BirthDate,
                        HireDate = emp.HireDate,
                        Address = emp.Address,
                        City = emp.City,
                        Region = emp.Region,
                        PostalCode = emp.PostalCode,
                        Country = emp.Country,
                        HomePhone = emp.HomePhone,
                        Extension = emp.Extension,
                        Photo = emp.Photo,
                        Notes = emp.Notes,
                        ReportsTo = emp.ReportsTo,
                        PhotoPath = emp.PhotoPath
                    });

                }
            }
            return res;
        }


        public void ActualizarDetalleParcialmente(int id,
            JsonPatchDocument<Employee> patchDoc)
        {
            var Empleado = db.Employees.Find(id);

            if (Empleado == null)
                return ;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, NombresEmpleados>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);


            int IdOriginal = Empleado.EmployeeID;
            patchDoc.ApplyTo(Empleado);


            // convierta el objeto parchado a uno que pueda asignar en una línea
            var elDetalleParaActualizar = mapa.Map<NombresEmpleados>(Empleado);



            if (Empleado.EmployeeID != IdOriginal)
            {
                return ;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public void CambiarEmpleado(int id, Employee employee)
        {
            var empleado = db.Employees.Find(id);
            if (empleado == null)
            {
                return;
            }


            if (id != employee.EmployeeID)
            {
                return ;
            }
            db.Entry(employee).State = EntityState.Modified;
            //db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    
                }
                else
                {
                    throw;
                }
            }

            

        }


        public void EliminarEmpleado(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                 
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

           
        }


        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }



    }
}
