using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.CliClasses
{
    internal class Command 
    {
        private List<Argument>? _arguments;
        private List<Option>? _options;

        private string? _description;
        private List<string>? _descriptionDetails;
        private string _commandName;
        private List<string>? _commandAliases;
        private List<string>? _usageMessages;
        private List<string>? _exampleMessages;

        public Command(string name, string? description = null)
        {
            this._commandName = name;
            this._description = description;

        }

        public bool IsCommandMatch(string commandName)
        {
            if (this._commandName == commandName)
                return true;

            if (this._commandAliases != null)
            {
                foreach (var alias in this._commandAliases)
                {
                    if (alias == commandName)
                        return true;
                }
            }

            return false;
        }

        internal string Name {  get { return this._commandName; } }
        internal string HelpSummary { get { return this._description != null ? this._description : ""; } }

        internal IReadOnlyList<string> HelpDetails => _descriptionDetails is not null ? _descriptionDetails : Array.Empty<string>();

        internal bool HasHelpDetails => _descriptionDetails is not null;

        internal IReadOnlyList<Argument> Arguments => _arguments is not null ? _arguments : Array.Empty<Argument>();

        internal bool HasArguments => _arguments is not null;

        internal IReadOnlyList<string> Aliases => _commandAliases is not null ? _commandAliases : Array.Empty<string>();

        internal bool HasAliases => _commandAliases is not null;

        internal IReadOnlyList<string> UsageMessages => _usageMessages is not null ? _usageMessages : Array.Empty<string>();

        internal bool HasUsageMessages => _usageMessages is not null;

        internal IReadOnlyList<string> ExampleMessages => _exampleMessages is not null ? _exampleMessages : Array.Empty<string>();

        internal bool HasExampleMessages => _exampleMessages is not null;

        internal IReadOnlyList<Option> Options => _options is not null ? _options : Array.Empty<Option>();

        internal bool HasOptions => _options is not null;


        public void AddHelpDetails(string HelpDetail)
        {
            (_descriptionDetails ??= new()).Add(HelpDetail);
        }

        public void AddArgument(Argument argument)
        {
            argument.AddParent(this);
            (_arguments ??= new()).Add(argument);
        }
   
        public void AddOption(Option option)
        {
            option.AddParent(this);
            (_options ??= new()).Add(option);
        }

        public void AddAlias(string alias)
        {
            (_commandAliases ??= new()).Add(alias);
        }

        public void AddUsageMessage(string usageMessage)
        {
            (_usageMessages ??= new()).Add(usageMessage);
        }

        public void AddExampleMessage(string exampleMessage)
        {
            (_exampleMessages ??= new()).Add(exampleMessage);
        }


        public void AddGlobalOption(Option option)
        {
            option.IsGlobal = true;
            AddOption(option);
        }

       
        /// <summary>
        /// 
        /// </summary>
        public Func<string, Dictionary<string, string>, Dictionary<string, string>, Task<int>>? CommandHandlerAsync { get; set; }

        
    }
}
