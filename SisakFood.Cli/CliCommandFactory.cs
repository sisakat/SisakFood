namespace SisakFood.Cli
{
    public static class CliCommandFactory
    {
        public static CliCommand CreateAdd(string[] args, Config config)
        {
            return new CliAdd(args, config.Location);
        }

        public static CliCommand CreateList(string[] args, Config config)
        {
            return new CliList(args, config.Location);
        }

        public static CliCommand CreateRemove(string[] args, Config config)
        {
            return new CliRemove(args, config.Location);
        }
    }
}