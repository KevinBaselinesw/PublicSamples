using System.CommandLine;


namespace Calc.Commands
{
    internal class SubCommand : Command
    {
        public SubCommand() : base("Sub", "Command to subtract two numbers from each other")
        {
            var SubArgument1 = new Argument<double>(name: "s1", description: "");
            var SubArgument2 = new Argument<double>(name: "s2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(SubArgument1);
            Cmd.Add(SubArgument2);

            Cmd.AddAlias("sub");


            Cmd.SetHandler((SubArgument1Value, SubArgument2Value) =>
            {
                SubCommandHandler(SubArgument1Value, SubArgument2Value);
            }, SubArgument1, SubArgument2);
        }

        static void SubCommandHandler(double p1, double p2)
        {
            double sum = p1 - p2;
            Console.WriteLine(string.Format($"{p1} - {p2} = {sum}"));
        }


    }
}
