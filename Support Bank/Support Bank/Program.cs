using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Support_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            var linesArray = File.ReadAllLines(@"C:\Bootcamp\Support Bank\Support Bank\Transactions2014.csv").Skip(1);
            List<Transaction> transList = new List<Transaction>();
            List<Person> people = new List<Person>();

            bool personToExist = false;
            bool personFromExist = false;

            foreach (string item in linesArray)
            {
                String[] line = item.Split(',');
                Transaction newtrans = new Transaction(DateTime.Parse(line[0]),line[1], line[2], line[3], Decimal.Parse(line[4]));
                transList.Add(newtrans);

                
            }

            foreach (Transaction trans in transList)
            {
                foreach (Person person in people)
                {
                    //Console.WriteLine("To: " + trans.To);
                    //Console.WriteLine("Person: " + person.Name);


                    if (trans.To == person.Name)
                    {
                        personToExist = true;
                        person.Owed += trans.Amount;
                    }
                    else if (trans.From == person.Name)
                    {
                        personFromExist = true;
                        person.Owe += trans.Amount;
                    }

                }

                //need to check if name exists. if not then add

                if (personToExist == false)
                {
                    Person newperson = new Person(trans.To, trans.Amount, 0);
                    people.Add(newperson);
                }

                if (personFromExist == false)
                {
                    Person newperson = new Person(trans.From, 0, trans.Amount);
                    people.Add(newperson);
                }

                personToExist = false;
                personFromExist = false;

            }
            foreach (Person person in people)
            {
                if (person.Owed > person.Owe)
                Console.WriteLine("Name: " + person.Name + " Owed: " + person.Owed + " Owe: " + person.Owe);
            }

            //Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();

        }
    }
}
 