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

            List<CLIParameterInfo> notificationInfos = new List<CLIParameterInfo>();

            // skip the first arg as that is the command
            for (int i = 1; i < args.Length; i++) 
            {
                bool ArgIsOption = false;
                foreach (var option in command.Options)
                {
                    if (option.IsOptionMatch(args[i])) 
                    {
                        string? optionValue;

                        (i, optionValue) = GenerateOptionValue(option.ExpectedNumberOfArguments, args, i);

                        // if found a matching option value, add it to the list
                        if (optionValue != null)
                        {
                            CLIParameterInfo notificationInfo = new CLIParameterInfo();
                            notificationInfo.ParameterType = CLIParameterType.Option;
                            notificationInfo.Name = option.Name;
                            notificationInfo.Value = optionValue;

                            notificationInfos.Add(notificationInfo);
                        }

                        ArgIsOption = true;
                        break;
                    }
                }

                // if the arg is not an option for this command, it must be an argument
                if (ArgIsOption == false)
                {
                    int ArgumentsCount = notificationInfos.Count(t => t.ParameterType == CLIParameterType.Argument);
                    if (ArgumentsCount < command.Arguments.Count)
                    {
                        var NextArgument = command.Arguments[ArgumentsCount];

                        CLIParameterInfo notificationInfo = new CLIParameterInfo();
                        notificationInfo.ParameterType = CLIParameterType.Argument;
                        notificationInfo.Name = NextArgument.Name;
                        notificationInfo.Value = args[i];

                        notificationInfos.Add(notificationInfo);    
                    }
                }
            }



            if (command.CommandHandlerAsync != null) 
            {
                await command.CommandHandlerAsync(CmdLine, notificationInfos);
            }
            else
            {
                Console.WriteLine($"The entered command does not have a handler method configured");
            }

        }

        private (int, string?) GenerateOptionValue(int ExpectedNumberOfArguments, string[] args, int arg_index)
        {
            int return_arg_index = arg_index;
            string? return_value = null;

            if (ExpectedNumberOfArguments == 0)
            {
                return_value = string.Empty;
                return_arg_index = arg_index;
            }

            if (ExpectedNumberOfArguments == 1)
            {
                if (args.Length > arg_index+1)
                {
                    return_value = args[arg_index + 1];
                    return_arg_index = arg_index + 1;
                }
  
            }

            if (ExpectedNumberOfArguments == 2)
            {
                if (args.Length > arg_index + 2)
                {
                    return_value = string.Format($"{args[arg_index + 1]}::{args[arg_index + 2]}");
                    return_arg_index = arg_index + 2;
                }

            }

            if (ExpectedNumberOfArguments == 3)
            {
                if (args.Length > arg_index + 3)
                {
                    return_value = string.Format($"{args[arg_index + 1]}::{args[arg_index + 2]}::{args[arg_index + 3]}");
                    return_arg_index = arg_index + 3;
                }

            }

            return (return_arg_index, return_value);
        }

        internal void AddCommand(Command Command)
        {
            commands.Add(Command);
        }

    }
}
