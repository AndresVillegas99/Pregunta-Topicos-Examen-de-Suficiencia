using NorthWndData;
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
        public List<DetailsNames> GetEmployees()
        {
            MetodosEmployee ME = new MetodosEmployee();
            return ME.GetBosses();

        }
    }
}
