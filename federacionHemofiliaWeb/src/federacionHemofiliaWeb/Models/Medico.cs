using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.Models
{
    public class Medico
    {
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Especialidad { get; set; }
        public Dictionary<DateTime, string> Citas { get; set; }
    }
}
