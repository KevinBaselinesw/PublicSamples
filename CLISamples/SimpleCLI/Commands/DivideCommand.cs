using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class DivideCommand : Command
    {
        private SessionData _sessionData;
        public DivideCommand(SessionData sessionData) : base("Div", "Command to divide two numbers together")
        {
            this._sessionData = sessionData;

            this.AddAlias("div");
            this.AddAlias("divide");


            var DivArgument1 = new Argument(name: "d1", description: "this is d1 argument");
            var DivArgument2 = new Argument(name: "d2", description: "this is d2 argument");

            this.AddArgument(DivArgument1);
            this.AddArgument(DivArgument2);

            this.CommandHandlerAsync = DivideHandler;

        }
        public async Task<int> DivideHandler(Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            if (argument.ContainsKey("d1") && argument.ContainsKey("d2"))
            {
                if (double.TryParse(argument["d1"], out  var a1) && double.TryParse(argument["d2"], out var a2))
                {
                    double sum = a1 / a2;
                    Console.WriteLine(string.Format($"{a1} / {a2} = {sum}"));

                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                }
            }


            return 0;
        }

    }
}
