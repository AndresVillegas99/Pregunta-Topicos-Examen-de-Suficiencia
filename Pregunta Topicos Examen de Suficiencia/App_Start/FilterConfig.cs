using System.Web;
using System.Web.Mvc;

namespace Pregunta_Topicos_Examen_de_Suficiencia
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
