using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.Commands
{
    internal class ExitCommand : Command
    {
        private SessionData _sessionData;
        public ExitCommand(SessionData sessionData) : base("Exit", "Command to quit the applications")
        {
            this._sessionData = sessionData;

            this.AddAlias("exit");
            this.AddAlias("QUIT");
            this.AddAlias("quit");

            this.Handler = AddHandler;

        }
        public int AddHandler(Dictionary<string, string> argument, Dictionary<string,string> options)
        {
            Console.WriteLine("Do you really want to exit this application?  (Y/N)");

            string? Response = Console.ReadLine();
            if (Response != null)
            {
                if (Response == "Y" || Response == "y")
                {
                    Environment.Exit(0);
                }
            }

            return 0;
        }

    }
}
