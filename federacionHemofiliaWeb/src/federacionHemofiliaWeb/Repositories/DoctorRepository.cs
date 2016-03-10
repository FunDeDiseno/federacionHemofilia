using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;

using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Config;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Services;

namespace federacionHemofiliaWeb.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private IFirebaseClient client;

        public DoctorRepository(IOptions<FireOps> options)
        {
            client = new FirebaseClient(new FirebaseConfig
            {
                AuthSecret = options.Value.Secret,
                BasePath = options.Value.Url
            });
        }

        public async Task<bool> Create(Medico doctor, string Id)
        {
            var doctores = new Dictionary<string, Medico>();
            doctores.Add(Id, doctor);
            var createDoctor = await client.UpdateAsync($"Doctors/", doctores);
            var result = createDoctor.StatusCode;
            if (result.ToString() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
