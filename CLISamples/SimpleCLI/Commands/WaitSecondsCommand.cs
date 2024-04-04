using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class WaitSecondsCommand : Command
    {
        private SessionData _sessionData;
        public WaitSecondsCommand(SessionData sessionData) : base("WaitSeconds", "Command to test waiting specified seconds")
        {
            this._sessionData = sessionData;

            this.AddAlias("ws");

            var WSArgument1 = new Argument(name: "w1", description: "number of seconds to wait");

            this.AddArgument(WSArgument1);

            this.CommandHandlerAsync = WaitSecondsHandler;
        }
    
        public async Task<int> WaitSecondsHandler(string CmdLine, Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            if (argument.ContainsKey("w1"))
            {
                if (double.TryParse(argument["w1"], out  var waitSeconds))
                {
                    if (waitSeconds > 30 || waitSeconds < 1)
                    {
                        Console.WriteLine("You have entered a wait time greater than 30 seconds or less than 1 second.  Defaulting to 30 seconds");
                        waitSeconds = 30;
                    }

                    for (int i = 0; i < waitSeconds; i++) 
                    {
                        Console.WriteLine($"seconds remaining: {waitSeconds-i}");
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }

                }
            }


            return 0;
        }

    }
}
