using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class Komputer
    {
        public int Id { get; set; }
        public string System { get; set; }
        public string Monitor { get; set; }
        public int Rozdzielczosc { get; set; }
        public bool CzyDziala { get; set; }


    }
}