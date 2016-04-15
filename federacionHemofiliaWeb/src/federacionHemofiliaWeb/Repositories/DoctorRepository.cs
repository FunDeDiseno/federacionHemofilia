using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using System.Net.Mail;
using System.Security.Cryptography;

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

        public async Task<Medico> GetDoctor(string Id)
        {
            var response = await client.GetAsync($"Doctors/{Id}");
            var doctor = response.ResultAs<Medico>();
            return doctor;
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

        public async void SendMail(string nameDoctor, string mailReceiver)
        {
            var hash = GetHash(mailReceiver);
            var newMessage = new SendGridMessage();
            newMessage.AddTo(mailReceiver);
            newMessage.From = new MailAddress("hello@federacion.com");
            newMessage.Subject = $"Invitacion de {nameDoctor} para Proyecto Ultra";
            newMessage.Html = $@"
                                  <html>
                                        <body><p>El doctor {nameDoctor} te invitó a participar del proyecto Ultra!</p>
                                              <p>ve a la siguiente liga <a href='http://localhost:5000/User/Registro/{hash}'>link</a></p>
                                        </body>
                                  </html>";

            await emailSender.DeliverAsync(newMessage);
        }

        public string GetHash(string mail)
        {
            var provider = new SHA1CryptoServiceProvider();
            byte[] byteMail = new byte[1000];
            byteMail = BitConverter.GetBytes(true);
            byteMail.Concat(System.Text.Encoding.UTF8.GetBytes(mail));
            byte[] valueHash = provider.ComputeHash(byteMail);
            string hash = null;
            foreach (var byteHash in valueHash)
            {
                hash += byteHash.ToString();
            }
            return hash;
        }
    }
}
