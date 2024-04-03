using SimpleCLI.CliClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI
{
    internal class Help
    {
        internal static bool IsHelpOption(string option)
        {
            string[] HelpOptions = new string[] { "--help", "-help", "--?", "-?", "?" };

            option = option.ToLower();

            foreach (string helpOption in HelpOptions)
            {
                if (helpOption == option)
                    return true;
            }

            return false;

        }

        internal static bool IsVersionOption(string option)
        {
            string[] VersionOptions = new string[] { "--version", "-version", "-vers" };

            option = option.ToLower();

            foreach (string versionOption in VersionOptions)
            {
                if (versionOption == option)
                    return true;
            }

            return false;

        }


        private static string IndentSpacer = "   ";
        internal static void DisplayHighLevelCommandHelp(string Description, IEnumerable<Command> commands)
        {
            // description section
            Console.WriteLine(GetDescriptionText());
            Console.WriteLine(IndentSpacer + Description);
            Console.WriteLine("");

            // usage section
            Console.WriteLine(GetUsageText());
            Console.WriteLine(IndentSpacer + GetUsageDescriptionText());
            Console.WriteLine("");

            // options section
            Console.WriteLine(GetOptionsText());

            List<TwoColumnHelpMessage> OptionMessages = new List<TwoColumnHelpMessage>();
            OptionMessages.Add(GetVersionOptionText());
            OptionMessages.Add(GetHelpOptionText());
            //OptionMessages.Add(GetCommandHelpTipText());
            WriteTwoColumnMessages(OptionMessages, 5);
            Console.WriteLine("");

            // commands section
            Console.WriteLine(GetCommandsText());

            List<TwoColumnHelpMessage> CommandMessages = new List<TwoColumnHelpMessage>();
            foreach (var command in commands)
            {
                CommandMessages.Add(GetCommandHelpMessage(command));
            }
            WriteTwoColumnMessages(CommandMessages, 5);
            Console.WriteLine("");
        }

        internal static void DisplayHighLevelVersionHelp()
        {
            Console.WriteLine("1.0.demo");
        }


        private static void WriteTwoColumnMessages(List<TwoColumnHelpMessage> optionMessages, int columnSpacing)
        {
            int longestColumn1 = 0;
            foreach (var option in optionMessages)
            {
                if (option.Column1.Length > longestColumn1)
                    longestColumn1 = option.Column1.Length;
            }

            foreach (var option in optionMessages)
            {
                string spacing = BuildColumnSpacer(option.Column1.Length, columnSpacing, longestColumn1);

                Console.WriteLine(string.Format($"{IndentSpacer}{option.Column1}{spacing}{option.Column2}"));
            }


        }

        private static string BuildColumnSpacer(int Column1Length, int columnSpacing, int longestColumn1)
        {
            string columnSpacer = string.Empty;

            int neededPaddingCount = (longestColumn1 + columnSpacing) - Column1Length;
            if (neededPaddingCount > 0)
            {
                for (int i = 0; i < neededPaddingCount; i++)
                {
                    columnSpacer += " ";
                }
            }

            return columnSpacer;
        }

        private static string GetDescriptionText()
        {
            return "Description:";
        }
        private static string GetUsageText()
        {
            return "Usage:";
        }
        private static string GetUsageDescriptionText()
        {
            return "[command] <arguments> <options>";
        }

        private static string GetOptionsText()
        {
            return "Options:";
        }

        private static string GetCommandsText()
        {
            return "Commands:";
        }
        private static TwoColumnHelpMessage GetVersionOptionText()
        {
            var VersionHelp = new TwoColumnHelpMessage();
            VersionHelp.Column1 = "--version";
            VersionHelp.Column2 = "Show version information";

            return VersionHelp;
        }

        private static TwoColumnHelpMessage GetHelpOptionText()
        {
            var HelpOptions = new TwoColumnHelpMessage();
            HelpOptions.Column1 = "--?, -?, --help";
            HelpOptions.Column2 = "Show help and usage information";

            return HelpOptions;
        }

        private static TwoColumnHelpMessage GetCommandHelpTipText()
        {
            var HelpOptions = new TwoColumnHelpMessage();
            HelpOptions.Column1 = "[command] <--help>";
            HelpOptions.Column2 = "Show detailed help information for the specified command";

            return HelpOptions;
        }

        private static TwoColumnHelpMessage GetCommandHelpMessage(Command command)
        {
            var CommandMessage = new TwoColumnHelpMessage();
            CommandMessage.Column1 = command.Name;
            CommandMessage.Column2 = command.HelpSummary;

            return CommandMessage;
        }


    }
}
