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

        public decimal Owed;

        public decimal Owe;

        public Person(String n, decimal od, decimal o)
        {
            Name = n;
            Owed = od;
            Owe = o;

        }
    }
}
