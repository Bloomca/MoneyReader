using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyReader.Classes
{
    internal class Statement
    {
        public DateTime Date { get; }

        public string Name { get; } = "";

        public decimal Transaction { get; }

        public bool IsSpending { get; }

        public decimal Balance { get; }

        public Statement(DateTime date, string name, decimal transaction, bool isSpending, decimal balance)
        {
            Date = date;
            Name = name;
            Transaction = transaction;
            IsSpending = isSpending;
            Balance = balance;
        }
    }
}
