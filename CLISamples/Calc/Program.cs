using Calc.Commands;
using System.CommandLine;

namespace Calc;

class Program
{
    /// <summary>
    /// Demo application for this MS library https://learn.microsoft.com/en-us/dotnet/standard/commandline/
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Sample app for System.CommandLine");

        rootCommand.AddCommand(new OptionExampleCommand());
        rootCommand.AddCommand(new AddCommand());
        rootCommand.AddCommand(new SubCommand());
        rootCommand.AddCommand(new MultiplyCommand());
        rootCommand.AddCommand(new DivideCommand());
        rootCommand.AddCommand(new ExitCommand());

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
