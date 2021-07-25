using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using NorthWndData;
using Pregunta_Topicos_Examen_de_Suficiencia.Models;
using AutoMapper;

namespace Pregunta_Topicos_Examen_de_Suficiencia.Controllers
{
    public class ConsultaEmpleadosController : ApiController
    {
        public List<NombreCompleto> GetEmployees(string nombre, string apellido)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, NameDetails>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);
            MetodosEmployee ME = new MetodosEmployee();
            var lista = ME.GetEmployees(nombre, apellido);
            
            List<NombreCompleto> nombresCompletos = new List<NombreCompleto>();

            List<NameDetails> listNombre = new List<NameDetails>();
            foreach (var empleado in lista)
            {
                var DetallesNombre = mapa.Map<NameDetails>(empleado);
                NombreCompleto nombreConFormato = NameFormat(DetallesNombre);
                nombresCompletos.Add(nombreConFormato);
            } ;

          

            return nombresCompletos;

        }

        private NombreCompleto NameFormat(NameDetails Detalles)
        {
            NombreCompleto Nombre = new NombreCompleto();
            string todo = Detalles.TitleOfCourtesy + Detalles.FirstName + " " + Detalles.LastName + "(" + Detalles.Title + ")";
            Nombre.NombresCompletos = todo;
            return Nombre;
        }

    }
}
