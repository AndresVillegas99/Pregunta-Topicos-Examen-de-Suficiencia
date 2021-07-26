using AutoMapper;
using NorthWndData;
using Pregunta_Topicos_Examen_de_Suficiencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace Pregunta_Topicos_Examen_de_Suficiencia.Controllers
{
    public class ConsultaJefaturaController : ApiController
    {

        //Este metodo GET trae un solo jefe en una lista, lo busca por nombre y apellido.
        //Solo puede traer empleados cuyo EmployeeID se encuentre en el ReportTo de otro empleado.
        [HttpGet]
        public List<BossDetails> GetEmployees(string nombre, string apellido)
        {
            MetodosEmployee ME = new MetodosEmployee();
            var lista = ME.GetBosses(nombre, apellido);
            var res = new List<BossDetails>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, BossDetails>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);
           
          

            List<NombreCompleto> nombresCompletos = new List<NombreCompleto>();

            List<BossDetails> listNombre = new List<BossDetails>();
            foreach (var empleado in lista)
            {
                var nuevoNombre = mapa.Map<BossDetails>(empleado);
                
                nuevoNombre.NombreCompleto = NameFormat(empleado);
                listNombre.Add(nuevoNombre);
            };

            return listNombre;
        }
       /* 
        * Este mtodo GET trae una lista con todos los jefes que tienen un campo en ReportTo de los empleados
        * [HttpGet]
        public List<NombreCompleto> GetEmployees()
        {
            MetodosEmployee ME = new MetodosEmployee();
            var lista = ME.GetAllBosses();
            var res = new List<BossDetails>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, BossDetails>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);



            List<NombreCompleto> nombresCompletos = new List<NombreCompleto>();

            List<BossDetails> listNombre = new List<BossDetails>();
            foreach (var empleado in lista)
            {
                var nuevoNombre = mapa.Map<BossDetails>(empleado);
              
                nuevoNombre.NombreCompleto = NameFormat(empleado);
                listNombre.Add(nuevoNombre);
            };

            return nombresCompletos;
        }*/
        private NombreCompleto NameFormat(NameDetails Detalles)
        {
            NombreCompleto Nombre = new NombreCompleto();
            string todo = Detalles.TitleOfCourtesy + Detalles.FirstName + " " + Detalles.LastName + "(" + Detalles.Title + ")";
            Nombre.NombresCompletos = todo;
            return Nombre;
        }
        private string NameFormat(Employee Detalles)
        {

            string todo = Detalles.TitleOfCourtesy + Detalles.FirstName + " " + Detalles.LastName + "(" + Detalles.Title + ")";

            return todo;
        }
    }
}
