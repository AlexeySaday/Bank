using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{

    class AccountNonDeposit1 : Amount
    {
        public AccountNonDeposit1(int id, decimal cash, decimal procent) : base(id, cash, procent)
        {
        }
        public override void AddAmount()
        {
            cash = Math.Round(cash += cash * procent / 100 / procent, 2);
        }
    }
    class AccountDeposit1 : Amount
    {
        public AccountDeposit1(int id, decimal cash, decimal procent) : base(id, cash, procent) { }
        public override void AddAmount()
        {
            cash = Math.Round(cash += cash * procent / 12 / 100, 2);
        }
    }
}
