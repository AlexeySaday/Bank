using System;
using System.Collections; 
using System.Windows.Controls; 

namespace MyInterface
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public IEnumerable Data
        {
            get => loglist.ItemsSource;
            set => loglist.ItemsSource = value;
        }
        public UserControl1()
        {
            InitializeComponent();

            loglist.ItemsSource = Data;
        }
    }
}