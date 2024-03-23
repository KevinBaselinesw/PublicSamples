using Calc.Commands;
using System.CommandLine;

namespace Calc;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Sample app for System.CommandLine");

        rootCommand.AddCommand(new OptionExampleCommand());
        rootCommand.AddCommand(new AddCommand());
        rootCommand.AddCommand(new SubCommand());
        rootCommand.AddCommand(new MultiplyCommand());
        rootCommand.AddCommand(new DivideCommand());

        

        try
        {
            while (true)
            {
                Console.Write(">> ");

                string? input = Console.ReadLine();
                if (input != null)
                {
                    string[] parts = input.Split(' ');

                    await rootCommand.InvokeAsync(parts);
                }
   
            }
   
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return 0;
    }




}
