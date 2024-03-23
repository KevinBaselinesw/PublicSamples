using System.CommandLine;


namespace Calc.Commands
{
    internal class ExitCommand : Command
    {
        public ExitCommand() : base("Exit", "Command to quit the application")
        {
            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.AddAlias("exit");
            Cmd.AddAlias("QUIT");
            Cmd.AddAlias("quit");


            Cmd.SetHandler(() =>
            {
                ExitCommandHandler();
            });
        }

        static void ExitCommandHandler()
        {
            Console.WriteLine("Do you really want to exit this application?  (Y/N)");

            string? Response = Console.ReadLine();
            if (Response != null) 
            { 
                if (Response == "Y" ||  Response == "y") 
                {
                    Environment.Exit(0);
                }
            }

        }


    }
}
