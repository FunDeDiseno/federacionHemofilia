using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;

using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

using federacionHemofiliaWeb.Interfaces;
using federacionHemofiliaWeb.Models;
using federacionHemofiliaWeb.Services;

namespace federacionHemofiliaWeb.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private IFirebaseClient client;

        public PacienteRepository(IOptions<FireOps> options)
        {
            client = new FirebaseClient(new FirebaseConfig
            {
                AuthSecret = options.Value.Secret,
                BasePath = options.Value.Url
            });
        }

        public async Task<Dictionary<string,Paciente>> get()
        {
            var response = await client.GetAsync("users/");
            var pacientes = response.ResultAs<Dictionary<string, Paciente>>();
            return pacientes;
        }

        public async Task<Paciente> get(string id)
        {
            var response = await client.GetAsync($"users/{id}");
            var user = response.ResultAs<Paciente>();
            return user;
        }

        public async Task<Dictionary<DateTime, int>> getData(string id)
        {
            var response = await client.GetAsync($"users/{id}/Aplicaciones/");
            var datos = response.ResultAs<Dictionary<DateTime, int>>();
            return datos;
        }

        public async Task<bool> update(Paciente paciente, string id)
        {
            var response = await client.UpdateAsync($"users/{id}", paciente);
            if(response.StatusCode.ToString() == "200")
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
