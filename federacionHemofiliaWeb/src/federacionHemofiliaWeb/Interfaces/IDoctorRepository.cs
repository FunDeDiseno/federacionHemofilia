using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using federacionHemofiliaWeb.Models;

namespace federacionHemofiliaWeb.Interfaces
{
    public interface IDoctorRepository
    {
        Task<bool> Create(Medico doctor);
    }
}
