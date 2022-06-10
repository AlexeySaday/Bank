using System;
using System.Collections.Generic; 
using System.Windows;
using System.Windows.Controls; 
using Newtonsoft.Json;
using System.IO;
 

namespace Bank
{
    public partial class MainWindow : Window
    {
        Repository repository = new Repository();
        string path = "data.json";
        public MainWindow()
        {
            try
            { 
                repository = JsonConvert.DeserializeObject<Repository>(File.ReadAllText(path), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch(FileNotFoundException)
            {
                RefreshData();
            }
            InitializeComponent();
            loglist.ItemsSource = repository.Clients;
        }
        private void Account_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Account.ItemsSource = (loglist.SelectedItem as Client).amount;
        }
        private void Right_Click(object sender, EventArgs e)
        {
            (loglist.SelectedItem as Client).DeleteAccount((Account.SelectedItem as Amount));
            Account.Items.Refresh();
            RefreshData();
        }
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                repository.AddClient((string)Surname.Text, (string)Name.Text, Convert.ToInt32(Age.Text));
                Remind.Text = $"Добавлен ползователь {(string)Surname.Text} {(string)Name.Text}";
                loglist.Items.Refresh();
                RefreshData();
                Surname.Clear();
                Name.Clear();
                Age.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите полные данные клиента");
            }
        }
        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            (loglist.SelectedItem as Client).AddAccountD(Convert.ToDecimal(Amount.Text), Convert.ToDecimal(Procent.Text));
            loglist.Items.Refresh();
            RefreshData();
            Amount.Clear();
            Procent.Clear();
        }
        private void NonDeposit_Click(object sender, RoutedEventArgs e)
        {
            (loglist.SelectedItem as Client).AddAccountN(Convert.ToDecimal(Amount.Text), Convert.ToDecimal(Procent.Text));
            loglist.Items.Refresh();
            RefreshData();
            Amount.Clear();
            Procent.Clear();
        }
        private void SendMoney_Click(object sender, RoutedEventArgs e)
        {

            Find(repository.Clients);
            Account.Items.Refresh();
            loglist.Items.Refresh();
            RefreshData();
            CASH.Clear();
            AccountID.Clear();
        }
        private void Find(List<Client> client)
        {
            try
            {
                bool f = true;
                for (int i = 0; i < client.Count; i++)
                    for (int j = 0; j < client[i].amount.Count; j++)
                        if (client[i].amount[j].ID == Convert.ToInt32(AccountID.Text))
                        {
                            f = false;
                            (Account.SelectedItem as Amount).NameEvent += PostValue1;
                            (Account.SelectedItem as Amount).SendMoney(client[i].amount[j], Convert.ToDecimal(CASH.Text));
                            break;

                        }
                if (f) throw new LightException(f, "Не найден номер карты");
            }
            catch (LightException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выберите счет");
            }
        }
        public void PostValue1(object sender)
        {
            var amount = (sender as Amount);
            Remind.Text = $"Со счета {(Account.SelectedItem as Amount).ID} был перевод на счет {amount.ID}";
        }
        private void RefreshData()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, repository, typeof(Repository));
            }
        }
    }
}
