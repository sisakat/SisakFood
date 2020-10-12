using System;
using System.Collections.Generic;
using SisakFood.Data.Dao;
using SisakFood.Data.Models;
using SisakCla.Core;

namespace SisakFood.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            SisakCla.Core.Cli cli = new SisakCla.Core.Cli(args);
            cli.Description = "Hauptprogramm super dupa";
            cli.Version = "1.0";
            cli.Copyright = "(c) Stefan Isak, 2020";
            cli.PrintDefaultValue = false;
            cli.PrintParameters = false;
            cli.AddFunctionClass(new Program());
            cli.Parse();
        }

        [CliOption("-t", Description = "Testausgabe")]
        public void test(string eingabeParameter, int testParameter) 
        {
            Console.WriteLine("Hello");
        }

        [CliOption("-a", LongOption = "--alpha", Description = "Test aaaaa")]
        public void test2(string eingabeParameter, int testParameter=3) 
        {
            Console.WriteLine("hello2");
        }
    }
}
