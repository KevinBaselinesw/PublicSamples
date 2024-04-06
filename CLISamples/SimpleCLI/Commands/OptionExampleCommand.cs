using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class OptionExampleCommand : Command
    {
        private SessionData _sessionData;
        public OptionExampleCommand(SessionData sessionData) : base("OptionExample", "Command to demo usage of options")
        {
            this._sessionData = sessionData;

            this.AddHelpDetails("This command demos the use of options which start with -- typically.");

            var Argument1 = new Argument(name: "arg1", description: "this is arg1 argument");
            this.AddArgument(Argument1);

            var Argument2 = new Argument(name: "arg2", description: "this is arg2 argument");
            this.AddArgument(Argument2);


            var x1option = new Option(name: "--x1", description: "x1 option.");
            x1option.ExpectedNumberOfArguments = 1;
            x1option.AddAlias("-x1");

            var x2option = new Option(name: "--x2", description: "x2 option.");
            x2option.ExpectedNumberOfArguments = 2;
            x2option.AddAlias("-x2");

            var x3option = new Option(name: "--x3", description: "x3 option.");
            x3option.ExpectedNumberOfArguments = 3;
            x3option.AddAlias("-x3");

            this.AddOption(x1option);
            this.AddOption(x2option);
            this.AddOption(x3option);

            this.AddUsageMessage("OptionExample <argument> <--x1> <--x2> <--x3> <Argument2>");

            this.AddExampleMessage("OptionExample argument --x1 hello --x3 A B C");
            this.AddExampleMessage("OptionExample argument --x1 hello --x3 A B C  -x2 E F");
            this.AddExampleMessage("OptionExample argument1 argument2 --x1 hello --x3 A B C  -x2 E F");
            this.AddExampleMessage("OptionExample argument1 --x1 hello --x3 A B C  -x2 E F argument2");

            this.CommandHandlerAsync = OptionHandler;

        }

        public async Task<int> OptionHandler(string CmdLine, IEnumerable<CLIParameterInfo> parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("no parameters found in the command");
                return -1;
            }

            foreach(var parameter in parameters) 
            { 
                Console.WriteLine(string.Format($"Name:{parameter.Name}, Value:{parameter.Value}, Type:{parameter.ParameterType}"));
            }

            await Task.Delay(TimeSpan.FromMilliseconds(1));

            return 0;
        }

    
    }
}
