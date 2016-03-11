using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;

using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Config;
using Neo4jClient;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Services;

namespace federacionHemofiliaWeb.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private IFirebaseClient client;
        private GraphClient neoClient;

        public DoctorRepository(IOptions<FireOps> options)
        {
            client = new FirebaseClient(new FirebaseConfig
            {
                AuthSecret = options.Value.Secret,
                BasePath = options.Value.Url
            });

            neoClient = new GraphClient(
                new Uri(options.Value.NeoUrl),
                options.Value.NeoUser,
                options.Value.NeoPss);
        }

        public async Task<bool> Create(Medico doctor, string Id)
        {
            var doctores = new Dictionary<string, Medico>();
            doctores.Add(Id, doctor);
            var createDoctor = await client.UpdateAsync($"Doctors/", doctores);

            neoClient.Connect();
            neoClient.Cypher
                     .Create("(user:Doctor {newDoctor})")
                     .WithParam("newDoctor", Id)
                     .ExecuteWithoutResults();

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
