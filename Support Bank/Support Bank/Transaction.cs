using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_Bank
{
    class Transaction
    {
        public String Date;

        public String From;

        public String To;

        public String Narrative;
        
        public decimal Amount;

        public Transaction(String d, String f, String t, String n, decimal a)
        {
            Date = d;
            From = f;
            To = t;
            Narrative = n;
            Amount = a;
        }
    }
}
