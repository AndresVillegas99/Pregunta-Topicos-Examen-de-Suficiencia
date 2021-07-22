using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace NorthWndData
{
    public class MetodosEmployee
    {
        private NorthWndEntities1 db = new NorthWndEntities1();

        // GET: api/Employees1
        public List<DetailsNames> GetEmployees()
        {
            var getEmpleados = db.Employees.ToList();
            var res = new List<DetailsNames>();
            var jefes = new List<int?>();
            
                
            
             
            foreach (var emp in getEmpleados)
            {
                res.Add(new DetailsNames 
                {
                    
                    FirstName = emp.FirstName,
                    LastName = emp.LastName
                });
            }
            
            return res;
        }

        public List<DetailsNames> GetBosses()
        {
            var getEmpleados = db.Employees.ToList();
            var res = new List<DetailsNames>();
            var jefes = new List<int?>();

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
                    res.Add(new DetailsNames
                    {

                        FirstName = emp.FirstName,
                        LastName = emp.LastName
                    });

                }
            }
            return res;
        }
    }
}
