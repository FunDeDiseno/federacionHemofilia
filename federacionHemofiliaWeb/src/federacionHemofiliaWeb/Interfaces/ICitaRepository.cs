using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace federacionHemofiliaWeb.Interfaces
{
    public interface ICitaRepository
    {
        Task<bool> Create(string IdDoctor, string IdPaciente, DateTime fecha);

        Task<bool> Delete(string IdDoctor, string IdPaciente);

        Task<List<string>> Get(string IdDoctor, DateTime fecha);
    }
}
