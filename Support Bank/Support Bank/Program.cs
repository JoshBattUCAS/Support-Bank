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
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            DateTime emptyDate = new DateTime();
            Transaction newtrans = new Transaction(emptyDate, "", "", "", 0);
            bool personToExist = false;
            bool personFromExist = false;
            bool nameValid = false;
            decimal amountOwed = 0;
            int i = 0;

            //log stuff
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Bootcamp\Support Bank\Support Bank\Log.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            Console.WriteLine("Please enter (1) for the 2014 Transaction list. \n" + "Please enter (2) for the 2015 Transaction List. \n");
            string go = Console.ReadLine();
            Console.WriteLine();


            string path = "";
            string path_2014 = "C:\\Bootcamp\\Support Bank\\Support Bank\\Transactions2014.csv";
            string path_2015 = "C:\\Bootcamp\\Support Bank\\Support Bank\\DodgyTransactions2015.csv";

            if (go == "1")
            {
                path = path_2014;
            }
            else if (go == "2")
            {
                path = path_2015;
            }
           

            //store each line in array
            var linesArray = File.ReadAllLines(path).Skip(1);
            List<Transaction> transList = new List<Transaction>();
            List<Person> people = new List<Person>();

            foreach (string item in linesArray)
            {
                String[] line = item.Split(',');

                
                try
                {
                    newtrans = new Transaction(DateTime.Parse(line[0]), line[1], line[2], line[3], Decimal.Parse(line[4]));
                    transList.Add(newtrans);
                }
                catch (System.FormatException)
                {
                    Logger.Error("Caught Format Exception");
                }
                
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
                    newperson.Transactions.Add(trans);
                }

                if (personFromExist == false)
                {
                    Person newperson = new Person(trans.From, 0, trans.Amount);
                    people.Add(newperson);
                    newperson.Transactions.Add(trans);
                }

                personToExist = false;
                personFromExist = false;

            }

            Console.WriteLine("1. Write 'List All' to view how much people owe. \n" + "2. Write the name of the person to view all Transactions for that particular person. \n");

            String input = Console.ReadLine();
            Console.WriteLine();

            foreach (Person person in people)
            {
                if (input == "List All")
                {
                    nameValid = true;

                    if (person.Owed > person.Owe)
                    {
                        amountOwed = person.Owed - person.Owe;
                        Console.WriteLine(person.Name + " is owed £" + amountOwed + ".");
                    }
                    if (person.Owe > person.Owed)
                    {
                        amountOwed = person.Owe - person.Owed;
                        Console.WriteLine(person.Name + " owes £" + amountOwed + ".");
                    }

                }

                if (input == person.Name)
                {
                    nameValid = true;
                    Console.WriteLine("Name: " + person.Name);
                    Console.WriteLine();

                    i = 0;

                    foreach (Transaction trans in person.Transactions)
                    {
                        i += 1;
                        String start_string = i.ToString() + ". ";

                        if (trans.From == person.Name)
                        {
                            Console.WriteLine(start_string.PadRight(5, ' ') + trans.Date.ToString("d") + " " + person.Name.PadRight(10, ' ') + " ---> " + trans.To.PadRight(10, ' ') + trans.Narrative.PadRight(34, ' ') + " £" + trans.Amount.ToString("0.00").PadRight(4, ' '));
                        }
                        else if (trans.To == person.Name)
                        {
                            Console.WriteLine(start_string.PadRight(5, ' ') + trans.Date.ToString("d") + " " + trans.From.PadRight(10, ' ') + " ---> " + person.Name.PadRight(10, ' ') + trans.Narrative.PadRight(34, ' ') + " £" + trans.Amount.ToString("0.00").PadRight(4, ' '));
                        }
                    }
                    Console.WriteLine();
                }
            }

            if (!nameValid)
            {
                Console.WriteLine("Not Valid");
            }

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
 