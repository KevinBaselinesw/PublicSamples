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

            this.AddUsageMessage("OptionExample <--x1> <--x2> <--x3>");

            this.AddExampleMessage("OptionExample --x1 hello --x3 goodbye");
            this.AddExampleMessage("OptionExample --x1 hello --x3 goodbye  -x2 usingAlias");

            this.CommandHandlerAsync = OptionHandler;

        }
        public async Task<int> OptionHandler(string CmdLine, Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));

            foreach (var kv in options)
            {
                Console.WriteLine(string.Format($"Key:{kv.Key}, Val:{kv.Value}"));
            }


            return 0;
        }

    }
}
