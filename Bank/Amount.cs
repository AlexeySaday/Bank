using System;
using System.Windows.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bank
{

    abstract class Amount : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<object> NameEvent;
        private DispatcherTimer timer;
        public int ID { get; set; }
        private decimal Cash;
        public decimal cash
        {
            get { return Cash; }
            set
            {
                Cash = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.cash)));
            }
        }
        public void SendMoney(Amount Value, decimal cash)
        {
            Value.cash += cash;
            this.cash -= cash;
            NameEvent?.Invoke(Value);
        }
        public decimal procent;
        public virtual void AddAmount() { }
        public Amount(int id, decimal cash, decimal procent)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(1, 0, 0, 0, 0);
            timer.Tick += (sender, e) => AddAmount();
            timer.Start();

            ID = id;
            this.cash = cash;
            this.procent = procent;
            Time = DateTime.Now;
        }
        private DateTime Time;
    }
}
