using System.CommandLine;


namespace Calc.Commands
{
    internal class DivideCommand : Command
    {
        public DivideCommand() : base("Divide", "Command to divide two numbers together")
        {
            var DivArgument1 = new Argument<double>(name: "d1", description: "");
            var DivArgument2 = new Argument<double>(name: "d2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(DivArgument1);
            Cmd.Add(DivArgument2);

            Cmd.AddAlias("divide");


            Cmd.SetHandler((DivArgument1Value, DivArgument2Value) =>
            {
                DivideCommandHandler(DivArgument1Value, DivArgument2Value);
            }, DivArgument1, DivArgument2);
        }

        static void DivideCommandHandler(double p1, double p2)
        {
            double total = p1 / p2;
            Console.WriteLine(string.Format($"{p1} * {p2} = {total}"));
        }


    }
}
