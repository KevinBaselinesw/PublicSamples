using System.CommandLine;


namespace Calc.Commands
{
    internal class MultiplyCommand : Command
    {
        public MultiplyCommand() : base("Mult", "Command to multiply two numbers together")
        {
            var MultArgument1 = new Argument<double>(name: "m1", description: "");
            var MultArgument2 = new Argument<double>(name: "m2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(MultArgument1);
            Cmd.Add(MultArgument2);

            Cmd.AddAlias("mult");


            Cmd.SetHandler((MultArgument1Value, MultArgument2Value) =>
            {
                MultiplyCommandHandler(MultArgument1Value, MultArgument2Value);
            }, MultArgument1, MultArgument2);
        }

        static void MultiplyCommandHandler(double p1, double p2)
        {
            double total = p1 * p2;
            Console.WriteLine(string.Format($"{p1} * {p2} = {total}"));
        }


    }
}
