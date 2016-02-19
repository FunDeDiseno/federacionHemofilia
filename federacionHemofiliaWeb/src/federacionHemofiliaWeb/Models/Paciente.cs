using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace federacionHemofiliaWeb.Models
{
    public class Paciente
    {
        public Dictionary<DateTime,int> Aplicaciones { get; set; }
        public DateTime FechaNac { get; set; }
        public float Estatura { get; set; }
        public float Peso { get; set; }
        public string FotoUrl { get; set; }
        public string Severidad { get; set; }
        public string Tipo { get; set; }
        public string PrimerNombre { get; set; }
        public string Apellido { get; set; }

        public string Nombre {
            get
            {
                return PrimerNombre + " " + Apellido;
            }
        }

        public int Edad
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

        public string getData
        {
            get
            {
                string json = "";
                Parallel.ForEach(Aplicaciones, datos =>
                {
                    json = JsonConvert.SerializeObject(Aplicaciones);
                });
                return json;
            }
        }
    }
}
