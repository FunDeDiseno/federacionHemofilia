using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.ViewModels;

namespace federacionHemofiliaWeb.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Dictionary<string, Paciente>> get();

        Task<Dictionary<DateTime, int>> getData(string id);
    }
}
