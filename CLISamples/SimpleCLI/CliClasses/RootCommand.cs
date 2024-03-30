using SimpleCLI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.CliClasses
{
    internal class RootCommand
    {
        private List<Command> commands = new List<Command>();
        public RootCommand(string Description) 
        { 
        }

        public async Task<int> ProcessCommandLine(string[] args)
        {
            await Task.Delay(1);

            string CmdString = args[0];

            switch (CmdString)
            {
                case "--help":
                case "-help":
                case "--?":
                case "-?":
                case "?":
                    DisplayHighLevelCommandHelp();
                    return 0;
                default:
                    break;
            }

            foreach (var command in commands)
            {
                if (command.IsCommandMatch(CmdString))
                {
                    NotifyCommand(command, args);
                    return 0;
                }
            }

            DisplayCommandNotFoundMessage(CmdString);

            return 1;
        }

   

        private void DisplayCommandNotFoundMessage(string cmdString)
        {
            Console.WriteLine($"The entered command '{cmdString} is not found");
        }

        private void DisplayHighLevelCommandHelp()
        {
            foreach (var command in commands) 
            {
                Console.WriteLine($"{command.Name}   {command.HelpSummary}");
            }

            Console.WriteLine("");
        }

        private void NotifyCommand(Command command, string[] args)
        {
            Dictionary<string,string> Arguments = new Dictionary<string,string>();
            Dictionary<string, string> Options = new Dictionary<string,string>();

            int argsOffset = 1;

            foreach (var argument in command.Arguments) 
            {
                if (argsOffset < args.Length)
                {
                    Arguments.Add(argument.Name, args[argsOffset++]);
                }
                else
                {
                    Arguments.Add(argument.Name, argument.Description);
                }
            }

            foreach (var argument in command.Options)
            {
                Options.Add(argument.Name, argument.Description);
            }

            if (command.Handler != null) 
            {
                command.Handler(Arguments, Options);
            }
            else
            {
                Console.WriteLine($"The entered command does not have a handler method configured");
            }

        }


        internal void AddCommand(Command Command)
        {
            commands.Add(Command);
        }
    }
}
