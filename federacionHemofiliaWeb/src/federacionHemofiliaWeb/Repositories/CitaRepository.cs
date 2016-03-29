using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using System.Net.Mail;

using Neo4jClient;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using SendGrid;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Services;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Models.Neo4j;

namespace federacionHemofiliaWeb.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private IFirebaseClient client;
        private GraphClient neoClient;
        private Web emailSender;

        public CitaRepository(IOptions<FireOps> options)
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

        public Task<bool> Create(string IdDoctor, string IdPaciente, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string IdDoctor, string IdPaciente)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Get(string IdDoctor, DateTime fecha)
        {
            var pacientesPorDia = neoClient.Cypher
                                           .OptionalMatch($"(doctor:Doctor)-[{fecha}]-(paciente:Paciente)")
                                           .Where((Doctor doctor) => doctor.Id == IdDoctor)
                                           .Return((doctor, pacientes) => new
                                           {
                                               Doctor = doctor.As<Models.Neo4j.Paciente>(),
                                               Paciente = pacientes.CollectAs<Models.Neo4j.Paciente>()
                                           })
                                           .Results;

            foreach (var paciente in pacientesPorDia) { }
            {
                
            }

            throw new NotImplementedException();
        }
    }
}
