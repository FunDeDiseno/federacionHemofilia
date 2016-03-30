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

        [JsonIgnore]
        public string Nombre {
            get
            {
                return PrimerNombre + " " + Apellido;
            }
        }

        [JsonIgnore]
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

        [JsonIgnore]
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
