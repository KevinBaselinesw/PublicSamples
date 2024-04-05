using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class SubCommand : Command
    {
        private SessionData _sessionData;
        public SubCommand(SessionData sessionData) : base("Sub", "Command to subtract two numbers")
        {
            this._sessionData = sessionData;

            this.AddAlias("sub");
            this.AddAlias("-");


            var SubtractArgument1 = new Argument(name: "s1", description: "this is s1 argument");
            var SubtractArgument2 = new Argument(name: "s2", description: "this is s2 argument");

            this.AddArgument(SubtractArgument1);
            this.AddArgument(SubtractArgument2);

            this.CommandHandlerAsync = SubtractHandler;

        }

        public async Task<int> SubtractHandler(string CmdLine, IEnumerable<CLIParameterInfo> parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("no parameters found in the command");
                return -1;
            }

            CLIParameterInfo? s1param = parameters.FirstOrDefault(t => t.Name == "s1" && t.ParameterType == CLIParameterType.Argument);
            CLIParameterInfo? s2param = parameters.FirstOrDefault(t => t.Name == "s2" && t.ParameterType == CLIParameterType.Argument);

            if (s1param != null && s2param != null)
            {
                if (double.TryParse(s1param.Value, out var s1) && double.TryParse(s2param.Value, out var s2))
                {
                    double sum = s1 - s2;
                    Console.WriteLine(string.Format($"{s1} - {s2} = {sum}"));

                    await Task.Delay(TimeSpan.FromMilliseconds(1));

                }
            }


            return 0;
        }
  

    }
}
