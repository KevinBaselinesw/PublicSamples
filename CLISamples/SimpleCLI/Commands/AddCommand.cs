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
    
        public async Task<int> AddHandler(string CmdLine, Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            if (argument.ContainsKey("a1") && argument.ContainsKey("a2"))
            {
                if (double.TryParse(argument["a1"], out  var a1) && double.TryParse(argument["a2"], out var a2))
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
