using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.Models
{
    public class Paciente
    {
        public List<Applicacion> Applicaciones { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNac { get; set; }
        public float Estatura { get; set; }
        public float Peso { get; set; }
        public string FotoUrl { get; set; }
        public string Severidad { get; set; }
        public string Tipo { get; set; }
        public int edad
        {
            get
            {
                int age = DateTime.Today.Year - FechaNac.Year;
                if (FechaNac > DateTime.Today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
    }
}
