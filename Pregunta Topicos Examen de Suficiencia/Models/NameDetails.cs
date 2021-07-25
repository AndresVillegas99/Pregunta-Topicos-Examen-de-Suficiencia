using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Pregunta_Topicos_1;

namespace WebApplication1.Models
{
    public class NameDetails 
    {
        
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Title { get; set; }

        public string TitleOfCourtesy { get; set; }
    }
}