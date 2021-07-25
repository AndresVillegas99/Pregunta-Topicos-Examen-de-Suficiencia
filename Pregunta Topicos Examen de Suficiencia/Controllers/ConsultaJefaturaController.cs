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
        public List<NombreCompleto> GetEmployees()
        {
            MetodosEmployee ME = new MetodosEmployee();
            var lista = ME.GetBosses();
            var res = new List<Employee>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, NameDetails>());
            IMapper iMapper = config.CreateMapper();
            var mapa = new Mapper(config);
           
          

            List<NombreCompleto> nombresCompletos = new List<NombreCompleto>();

            List<NameDetails> listNombre = new List<NameDetails>();
            foreach (var empleado in lista)
            {
                var nuevoNombre = mapa.Map<NameDetails>(empleado);
                NombreCompleto test = new NombreCompleto();
                string todo = nuevoNombre.TitleOfCourtesy + nuevoNombre.FirstName +" " + nuevoNombre.LastName + "(" + nuevoNombre.Title + ")";
                test.NombresCompletos = todo;
                nombresCompletos.Add(test);
            };

            return nombresCompletos;
        }
    }
}
