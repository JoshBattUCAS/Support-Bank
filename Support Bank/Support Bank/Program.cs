using NLog;
using NLog.Config;
using NLog.Targets;
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
            //log stuff
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            //store each line in array
            var linesArray = File.ReadAllLines(@"C:\Bootcamp\Support Bank\Support Bank\Transactions2014.csv").Skip(1);
            List<Transaction> transList = new List<Transaction>();
            List<Person> people = new List<Person>();
            

            bool personToExist = false;
            bool personFromExist = false;
            decimal amountOwed = 0;
            int i = 0;

            foreach (string item in linesArray)
            {
                String[] line = item.Split(',');
                Transaction newtrans = new Transaction(line[0],line[1], line[2], line[3], Decimal.Parse(line[4]));
                transList.Add(newtrans);
            }

            foreach (Transaction trans in transList)
            {
                foreach (Person person in people)
                {
                    if (trans.To == person.Name)
                    {
                        personToExist = true;
                        person.Owed += trans.Amount;
                        person.Transactions.Add(trans);
                    }
                    else if (trans.From == person.Name)
                    {
                        personFromExist = true;
                        person.Owe += trans.Amount;
                        person.Transactions.Add(trans);
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

            String input = Console.ReadLine();

            foreach (Person person in people)
            {

                if (input == "Name")
                {
                    if (person.Owed > person.Owe)
                    {
                        amountOwed = person.Owed - person.Owe;
                        Console.WriteLine(person.Name + " is owed £" + amountOwed);
                    }
                    if (person.Owe > person.Owed)
                    {
                        amountOwed = person.Owe - person.Owed;
                        Console.WriteLine(person.Name + " owes £" + amountOwed);
                    }

                }

                if (input == "List All")
                {
                    Console.WriteLine("Name: " + person.Name);
                    Console.WriteLine();

                    i = 0; 

                    foreach (Transaction trans in person.Transactions)
                    {
                        i += 1;
                        
                        if (trans.From == person.Name)
                        {
                            Console.WriteLine(i + ". " + person.Name + " ---> " + trans.To + ", Date: " + trans.Date + ", Narrative: " + trans.Narrative + ", Amount: £" + trans.Amount);
                        }
                        else if (trans.To == person.Name)
                        {
                            Console.WriteLine(i + ". " + trans.From + " ---> " + person.Name + ", Date: " + trans.Date + ", Narrative: " + trans.Narrative + ", Amount: £" + trans.Amount);
                        }
                       

                    }
                    Console.WriteLine();
                }
                    

             
            
                
            }
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
           

        }
    }
}
 