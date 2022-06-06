using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    interface IClient<in T> where T : Amount
    {
        int Id { set; }
        string Surname { set; }
        string Name { set; }
        int Age { set; }
        void AddAccount(T value);
        void DeleteAccount(T value);
    }

    class Client
    {
        public event Action<int> idValue;
        static int id;
        static int NextID()
        {
            id++;
            return id;
        }
        public List<Amount> amount;
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Client(int id, int idName, string surname, string name, int age)
        {
            Client.id = id;
            Id = idName;
            Surname = surname;
            Name = name;
            Age = age;
            amount = new List<Amount>();
        }
        public void AddAccountD(decimal cash, decimal procent)
        {
            amount.Add(new AccountDeposit1(NextID(), cash, procent));
            idValue?.Invoke(id);
        }
        public void AddAccountN(decimal cash, decimal procent)
        {
            amount.Add(new AccountNonDeposit1(NextID(), cash, procent));
            idValue?.Invoke(id);
        }
        public void DeleteAccount(Amount am)
        {
            amount.Remove(am);
        }
    }
}
