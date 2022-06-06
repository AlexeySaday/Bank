using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Repository
    {
        public int Id { get; set; }
        public List<Client> Clients;
        public Repository()
        {
            Clients = new List<Client>();
        }
        public void AddClient(string surname, string name, int age)
        {
            var client = new Client(Id, Clients.Count + 1, surname, name, age);
            client.idValue += a => Id = a;
            Clients.Add(client);
        }
    }
}

