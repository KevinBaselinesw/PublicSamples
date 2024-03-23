using System.CommandLine;


namespace Calc.Commands
{
    internal class AddCommand : Command
    {
        public AddCommand() : base("Add", "Command to add two numbers together")
        {
            var AddArgument1 = new Argument<double>(name: "a1", description: "");
            var AddArgument2 = new Argument<double>(name: "a2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(AddArgument1);
            Cmd.Add(AddArgument2);

            Cmd.AddAlias("add");

            Cmd.SetHandler((AddArgument1Value, AddArgument2Value) =>
            {
                AddCommandHandler(AddArgument1Value, AddArgument2Value);
            }, AddArgument1, AddArgument2);
        }

        static void AddCommandHandler(double p1, double p2)
        {
            double sum = p1 + p2;
            Console.WriteLine(string.Format($"{p1} + {p2} = {sum}"));
        }


    }
}
