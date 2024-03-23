using System.CommandLine;


namespace Calc.Commands
{
    internal class OptionExampleCommand : Command
    {
        public OptionExampleCommand() : base("OptionExample", "Command to demo usage of options")
        {
            var x1option = new Option<string?>(name: "--x1", description: "x1 option.");
            x1option.AddAlias("-x1");

            var x2option = new Option<string?>(name: "--x2", description: "x2 option.");
            x2option.AddAlias("-x2");

            var x3option = new Option<string?>(name: "--x3", description: "x3 option.");
            x3option.AddAlias("-x3");

       
            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.


            Cmd.AddOption(x1option);
            Cmd.AddOption(x2option);
            Cmd.AddOption(x3option);

            Cmd.AddAlias("optionexample");


            Cmd.SetHandler((x1, x2, x3) =>
            {
                OptionExampleCommandHandler(x1!, x2!, x3!);
            }, x1option, x2option, x3option);

        }

        static void OptionExampleCommandHandler(string x1, string x2, string x3)
        {
            if (!string.IsNullOrEmpty(x1))
                Console.WriteLine("x1 = " + x1);
            else
                Console.WriteLine("x1 option not specified");

            if (!string.IsNullOrEmpty(x2))
                Console.WriteLine("x2 = " + x2);
            else
                Console.WriteLine("x2 option not specified");

            if (!string.IsNullOrEmpty(x3))
                Console.WriteLine("x3 = " + x3);
            else
                Console.WriteLine("x3 option not specified");

        }

    }
}
