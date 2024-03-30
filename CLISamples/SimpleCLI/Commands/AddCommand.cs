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


            var AddArgument1 = new Argument(name: "a1", description: "this is a1 argument");
            var AddArgument2 = new Argument(name: "a2", description: "this is a2 argument");

            this.AddArgument(AddArgument1);
            this.AddArgument(AddArgument2);

            this.CommandHandlerAsync = AddHandler;
        }
    
        public async Task<int> AddHandler(Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            if (argument.ContainsKey("a1") && argument.ContainsKey("a2"))
            {
                if (double.TryParse(argument["a1"], out  var a1) && double.TryParse(argument["a2"], out var a2))
                {
                    double sum = a1 + a2;
                    Console.WriteLine(string.Format($"{a1} + {a2} = {sum}"));

                    await Task.Delay(TimeSpan.FromSeconds(Math.Max(sum,10)));
                }
            }


            return 0;
        }

    }
}
