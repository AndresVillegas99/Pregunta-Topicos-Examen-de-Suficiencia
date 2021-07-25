using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var nombres = new List<NombresEmpleados>();

            foreach (var emp in getEmpleados)
            {
                res.Add(new Employee
                {
                    
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Title = emp.Title,
                    TitleOfCourtesy = emp.TitleOfCourtesy
                });
            }

            
            
            return res ;
        }

        

        public List<Employee> GetBosses()
        {
            var getEmpleados = db.Employees.ToList();
           
            var res = new List<Employee>();
            var jefes = new List<int?>();
            var nombres = new List<NombresEmpleados>()
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
                        TitleOfCourtesy = emp.TitleOfCourtesy
                    });

                }
            }
            return res;
        }
    }
}
