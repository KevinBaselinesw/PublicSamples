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

        public async Task<int> ProcessCommandLine(string CommandLine, string[] args)
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
                Help.DisplayCommandNotFoundMessage(CmdString);
                return 1;
            }    

            // if this is a command specific help request
            if (IsCommandSpecificHelpOption(args))
            {
                Help.DisplayCommandSpecificHelp(command);
                return 0;   
            }

            // we have a valid command with no help request!  Wait for it to finish.
            await NotifyCommand(command, CommandLine, args);
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

        private async Task NotifyCommand(Command command, string CmdLine, string[] args)
        {
            Dictionary<string,string> Arguments = new Dictionary<string,string>();
            Dictionary<string, string> Options = new Dictionary<string,string>();

            // skip the first arg as that is the command
            for (int i = 1; i < args.Length; i++) 
            {
                bool ArgIsOption = false;
                foreach (var option in command.Options)
                {
                    if (option.IsOptionMatch(args[i])) 
                    {
                        i = AddOptions(Options, option, args, i);
                        ArgIsOption = true;
                        break;
                    }
                }

                // if the arg is not an option for this command, it must be an argument
                if (ArgIsOption == false)
                {
                    if (Arguments.Count < command.Arguments.Count)
                    {
                        var NextArgument = command.Arguments[Arguments.Count];
                        Arguments.Add(NextArgument.Name, args[i]);
                    }
                }
            }



            if (command.CommandHandlerAsync != null) 
            {
                await command.CommandHandlerAsync(CmdLine, Arguments, Options);
            }
            else
            {
                Console.WriteLine($"The entered command does not have a handler method configured");
            }

        }

        private int AddOptions(Dictionary<string, string> options, Option option, string[] args, int i)
        {
            if (option.ExpectedNumberOfArguments == 0)
            {
                options.Add(option.Name, string.Empty);
                return i;
            }

            if (option.ExpectedNumberOfArguments == 1)
            {
                if (args.Length > i+1)
                {
                    options.Add(option.Name, args[i + 1]);
                    return i + 1;
                }
  
            }

            if (option.ExpectedNumberOfArguments == 2)
            {
                if (args.Length > i + 2)
                {
                    options.Add(option.Name, string.Format($"{args[i+1]}::{args[i+2]}"));
                    return i + 2;
                }

            }

            if (option.ExpectedNumberOfArguments == 3)
            {
                if (args.Length > i + 3)
                {
                    options.Add(option.Name, string.Format($"{args[i + 1]}::{args[i + 2]}::{args[i + 3]}"));
                    return i + 3;
                }

            }

            return i;
        }

        internal void AddCommand(Command Command)
        {
            commands.Add(Command);
        }

    }
}
