using Bank;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyInter
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class BarChart : UserControl
    {
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
        public IEnumerable Data
        {
            get => loglist.ItemsSource;
            set => loglist.ItemsSource = value;
        }
        public IEnumerable Data1
        {
            get => (IEnumerable)loglist.SelectedItem; 
        }
        public IEnumerable Items1
        {
            get => Account.ItemsSource;
            set => Account.ItemsSource = value;
        }
        public IEnumerable Items2
        {
            get => (IEnumerable)Account.SelectedItem;
        }
        public void AccountRefresh()
        {
            Account.Items.Refresh();
        }
        public void loglistRefresh()
        {
            loglist.Items.Refresh();
        }
        private void Account_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public BarChart()
        {
            InitializeComponent();
            loglist.ItemsSource = Data; 
            Account.ItemsSource = Items1;
        }
    }
}
