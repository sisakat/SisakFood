using System;
using System.Collections.Generic;
using SisakFood.Data.Dao;
using SisakFood.Data.Models;
using SisakCla.Core;
using System.Linq;
using System.IO;

namespace SisakFood.Cli
{
    class Program
    {
        const string VERSION = "1.0.0";

        [CliOption("-d", LongOption = "--debug", Description = "Show debug information")]
        public bool Debug { get; set; }

        [CliOption("-c", LongOption = "--config", Description = "Specify the config file location")]
        public string ConfigLocation { get; set; } = "config.json";

        public Config Configuration { get; set; }

        static void Main(string[] args)
        {
            var program = new Program();
            program.Configuration = Config.Load(program.ConfigLocation);

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
                if (program.Debug)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.InnerException);
                }
                Console.WriteLine("Invalid call");
            }
        }

        [CliOption("add", Description = "Adds new food or meals")]
        public void Add(string[] args)
        {
            CliCommandFactory.CreateAdd(args, Configuration).Parse();
        }

        [CliOption("remove", Description = "Remove food or meals")]
        public void Remove(string[] args)
        {
            CliCommandFactory.CreateRemove(args, Configuration).Parse();
        }

        [CliOption("list", Description = "List food or meals")]
        public void List(string[] args)
        {
            CliCommandFactory.CreateList(args, Configuration).Parse();
        }

        [CliOption("-cc", LongOption = "--create-config", Description = "Creates a default config file")]
        public void CreateConfigFile(string location = ".")
        {
            try 
            {
                new Config().Save(Path.Combine(location, "config.json"));
            } 
            catch (Exception ex) 
            {
                Console.WriteLine("Could not create default config file: " + ex.Message);
            }
        }
    }
}
