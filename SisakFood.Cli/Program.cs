using System;
using System.Collections.Generic;
using SisakFood.Data.Dao;
using SisakFood.Data.Models;
using SisakCla.Core;
using System.Linq;

namespace SisakFood.Cli
{
    class Program
    {
        const string VERSION = "1.0.0";

        static void Main(string[] args)
        {
            var program = new Program();

            SisakCla.Core.Cli cli = new SisakCla.Core.Cli(args);
            cli.AddFunctionClass(program);
            cli.Description = "SisakFood.Cli";
            cli.Version = Program.VERSION;
            cli.Copyright = "(c) Sisak, 2020";

            try
            {
                cli.Parse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                cli.PrintHelp();
            }
        }

        [CliOption("add", Description = "Adds new food or meals")]
        public void Add(string[] args)
        {
            CliAdd add = new CliAdd(args ?? new string[] {});
            add.Parse();
        }

        [CliOption("remove", Description = "Remove food or meals")]
        public void Remove(string[] args)
        {
            CliRemove remove = new CliRemove(args ?? new string[] {});
            remove.Parse();
        }

        [CliOption("list", Description = "List food or meals")]
        public void List(string[] args)
        {
            CliList list = new CliList(args ?? new string[] {});
            list.Parse();
        }
    }
}
