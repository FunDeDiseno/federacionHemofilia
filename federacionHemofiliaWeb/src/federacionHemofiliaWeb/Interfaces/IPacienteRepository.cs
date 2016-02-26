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

        Task<Paciente> get(string id);

        Task<Dictionary<DateTime, int>> getData(string id);

        Task<bool> update(Paciente paciente, string id);

    }
}
