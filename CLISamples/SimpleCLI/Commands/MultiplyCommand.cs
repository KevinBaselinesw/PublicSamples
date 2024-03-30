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

            this.Handler = MultiplyHandler;

        }
        public int MultiplyHandler(Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            if (argument.ContainsKey("m1") && argument.ContainsKey("m2"))
            {
                if (double.TryParse(argument["m1"], out  var a1) && double.TryParse(argument["m2"], out var a2))
                {
                    double sum = a1 * a2;
                    Console.WriteLine(string.Format($"{a1} * {a2} = {sum}"));
                }
            }


            return 0;
        }

    }
}
