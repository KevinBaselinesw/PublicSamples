using SimpleCLI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.CliClasses
{
    internal class CLIInterpreterEngine
    {
        private List<Command> commands = new List<Command>();

        private string Description;
        public CLIInterpreterEngine(string Description) 
        { 
            this.Description = Description;
        }

        public async Task<int> ProcessCommandLine(string[] args)
        {
            string CmdString = args[0];

            // if this is top level help request
            if (Help.IsHelpOption(CmdString))
            {
                Help.DisplayHighLevelCommandHelp(this.Description, this.commands);
                return 0;
            }
            if (Help.IsVersionOption(CmdString))
            {
                Help.DisplayHighLevelVersionHelp();
                return 0;
            }

            // find the matching command. Else display error
            Command? command = FindMatchingCommand(commands, CmdString);
            if (command == null)
            {
                DisplayCommandNotFoundMessage(CmdString);
                return 1;
            }    

            // if this is a command specific help request
            if (IsCommandSpecificHelpOption(args))
            {
                DisplayCommandSpecificHelp(command);
                return 0;   
            }

            // we have a valid command with no help request!  Wait for it to finish.
            await NotifyCommand(command, args);
            return 0;
        }

  

        private Command? FindMatchingCommand(List<Command> commands, string CmdString)
        {
            foreach (var command in commands)
            {
                if (command.IsCommandMatch(CmdString))
                {
                    return command;
                }
            }
            return null;
        }

        private bool IsCommandSpecificHelpOption(string[] args)
        {
            foreach (var arg in args)
            {
                if (Help.IsHelpOption(arg))
                    return true;
            }
            return false;
        }

        private void DisplayCommandNotFoundMessage(string cmdString)
        {
            Console.WriteLine($"The entered command '{cmdString}' is not found!  Enter --help for a list of commands!");
        }

        private void DisplayCommandSpecificHelp(Command command)
        {
            Console.WriteLine($"todo: implement specific command help {command.Name}");
        }

        private async Task NotifyCommand(Command command, string[] args)
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

            if (command.CommandHandlerAsync != null) 
            {
                await command.CommandHandlerAsync(Arguments, Options);
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
