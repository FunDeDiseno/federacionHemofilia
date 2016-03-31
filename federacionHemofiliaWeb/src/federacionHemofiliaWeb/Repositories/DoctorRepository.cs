using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using System.Net.Mail;


using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using Neo4jClient;
using SendGrid;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Models.Neo4j;
using federacionHemofiliaWeb.Services;  

namespace federacionHemofiliaWeb.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private IFirebaseClient client;
        private GraphClient neoClient;
        private Web emailSender;

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

            emailSender = new Web(options.Value.SendGrid);
        }

        public async Task<bool> Create(Medico doctor, string Id)
        {
            var doctores = new Dictionary<string, Medico>();
            doctores.Add(Id, doctor);
            var createDoctor = await client.UpdateAsync($"Doctors/", doctores);

            var newDoctor = new Doctor
            {
                Id = Id
            };

            neoClient.Connect();
            neoClient.Cypher
                     .Create("(user:Doctor {newDoctor})")
                     .WithParam("newDoctor", newDoctor)
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
