using SisakFood.Data.Dao;

namespace SisakFood.Cli
{
    public class CliCommand
    {
        protected readonly SisakCla.Core.Cli cli;
        protected readonly IDao dao;

        public CliCommand(string[] args) 
        {
            dao = new JsonFileDao(".");
            cli = new SisakCla.Core.Cli(args);
        }

        public void Parse() 
        {
            cli.Parse();
        }
    }
}