﻿using SimpleCLI.CliClasses;
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

            this.AddHelpDetails("Use this command to divide two numbers together.");
            this.AddHelpDetails("It will work for integers or floating point numbers.");
            this.AddHelpDetails("The result of the two numbers will be displayed.");

            var DivArgument1 = new Argument(name: "d1", description: "this is d1 argument");
            var DivArgument2 = new Argument(name: "d2", description: "this is d2 argument");

            this.AddArgument(DivArgument1);
            this.AddArgument(DivArgument2);

            this.AddUsageMessage("Div <d1> <d2>");

            this.AddExampleMessage("Div 2 3");
            this.AddExampleMessage("Div 2.99667 -3.3345");

            this.CommandHandlerAsync = DivideHandler;

        }

        public async Task<int> DivideHandler(string CmdLine, IEnumerable<CLIParameterInfo> parameters)
        {
            if (parameters == null)
            {
                Console.WriteLine("no parameters found in the command");
                return -1;
            }

            CLIParameterInfo? d1param = parameters.FirstOrDefault(t => t.Name == "d1");
            CLIParameterInfo? d2param = parameters.FirstOrDefault(t => t.Name == "d2");

            if (d1param != null && d2param != null)
            {
                if (double.TryParse(d1param.Value, out var d1) && double.TryParse(d2param.Value, out var d2))
                {
                    double sum = d1 / d2;
                    Console.WriteLine(string.Format($"{d1} + {d2} = {sum}"));

                    await Task.Delay(TimeSpan.FromMilliseconds(1));

                }
            }


            return 0;
        }

    }
}
