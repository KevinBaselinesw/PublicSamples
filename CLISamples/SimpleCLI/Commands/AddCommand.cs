using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class AddCommand : Command
    {
        private SessionData _sessionData;
        public AddCommand(SessionData sessionData) : base("Add", "Command to add two numbers together")
        {
            this._sessionData = sessionData;

            this.AddAlias("add");
            this.AddAlias("+");

            this.AddHelpDetails("Use this command to add two numbers together.");
            this.AddHelpDetails("It will work for integers or floating point numbers.");
            this.AddHelpDetails("The sum of the two numbers will be displayed.");


            var AddArgument1 = new Argument(name: "a1", description: "this is a1 argument");
            var AddArgument2 = new Argument(name: "a2", description: "this is a2 argument");

            this.AddArgument(AddArgument1);
            this.AddArgument(AddArgument2);

            this.AddUsageMessage("Add <a1> <a2>");

            this.AddExampleMessage("Add 2 3");
            this.AddExampleMessage("Add 2.99667 -3.3345");

            this.CommandHandlerAsync = AddHandler;
        }
    
        public async Task<int> AddHandler(string CmdLine, IEnumerable<CLIParameterInfo> parameters)
        {
            if (parameters == null) 
            {
                Console.WriteLine("no parameters found in the command");
                return -1;
            }

            CLIParameterInfo? a1param = parameters.FirstOrDefault(t => t.Name == "a1" && t.ParameterType == CLIParameterType.Argument);
            CLIParameterInfo? a2param = parameters.FirstOrDefault(t => t.Name == "a2" && t.ParameterType == CLIParameterType.Argument);

            if (a1param != null && a2param != null)
            {
                if (double.TryParse(a1param.Value, out  var a1) && double.TryParse(a2param.Value, out var a2))
                {
                    double sum = a1 + a2;
                    Console.WriteLine(string.Format($"{a1} + {a2} = {sum}"));

                    await Task.Delay(TimeSpan.FromMilliseconds(1));

                }
            }


            return 0;
        }

    }
}
