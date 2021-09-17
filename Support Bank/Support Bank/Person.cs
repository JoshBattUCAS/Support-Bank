using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_Bank
{
    class Person
    {
        public String Name;

        public Decimal Owed;

        public Decimal Owe;

        public List<Transaction> Transactions { get; set; }

        public Person(String n, Decimal od, Decimal o)
        {
            Name = n;
            Owed = od;
            Owe = o;
            Transactions = new List<Transaction>();

        }

    }
}
