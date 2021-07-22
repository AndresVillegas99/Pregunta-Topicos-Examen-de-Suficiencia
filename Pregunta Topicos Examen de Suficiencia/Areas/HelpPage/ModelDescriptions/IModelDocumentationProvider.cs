using System;
using System.Reflection;

namespace Pregunta_Topicos_Examen_de_Suficiencia.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}