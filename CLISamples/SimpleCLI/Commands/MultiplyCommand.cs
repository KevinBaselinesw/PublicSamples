using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class MultiplyCommand : Command
    {
        private SessionData _sessionData;
        public MultiplyCommand(SessionData sessionData) : base("Mult", "Command to multiply two numbers together")
        {
            this._sessionData = sessionData;

            this.AddAlias("mult");
            this.AddAlias("*");


            var MultArgument1 = new Argument(name: "m1", description: "this is m1 argument");
            var MultArgument2 = new Argument(name: "m2", description: "this is m2 argument");

            this.AddArgument(MultArgument1);
            this.AddArgument(MultArgument2);

            this.AddUsageMessage("Mult <m1> <m2>");

            this.AddExampleMessage("Mult 2 3");
            this.AddExampleMessage("Mult 2.99667 -3.3345");

            this.CommandHandlerAsync = MultiplyHandler;

        }

        public async Task<int> MultiplyHandler(string CmdLine, IEnumerable<CLIParameterInfo> parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("no parameters found in the command");
                return -1;
            }

            CLIParameterInfo? m1param = parameters.FirstOrDefault(t => t.Name == "m1" && t.ParameterType == CLIParameterType.Argument);
            CLIParameterInfo? m2param = parameters.FirstOrDefault(t => t.Name == "m2" && t.ParameterType == CLIParameterType.Argument);

            if (m1param != null && m2param != null)
            {
                if (double.TryParse(m1param.Value, out var m1) && double.TryParse(m2param.Value, out var m2))
                {
                    double sum = m1 * m2;
                    Console.WriteLine(string.Format($"{m1} * {m2} = {sum}"));

                    await Task.Delay(TimeSpan.FromMilliseconds(1));

                }
            }


            return 0;
        }
 

    }
}
