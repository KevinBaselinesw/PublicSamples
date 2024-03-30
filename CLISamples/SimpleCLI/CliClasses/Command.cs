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
        private string _commandName;
        private List<string>? _commandAliases;

  
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

        public string Name {  get { return this._commandName; } }
        public string HelpSummary { get { return this._description != null ? this._description : ""; } }

   
        public IReadOnlyList<Argument> Arguments => _arguments is not null ? _arguments : Array.Empty<Argument>();

        internal bool HasArguments => _arguments is not null;

 
        public IReadOnlyList<Option> Options => _options is not null ? _options : Array.Empty<Option>();
            

     
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

  
        public void AddGlobalOption(Option option)
        {
            option.IsGlobal = true;
            AddOption(option);
        }

        public void Add(Option option) => AddOption(option);

        public void Add(Argument argument) => AddArgument(argument);

    
        /// <summary>
        /// 
        /// </summary>
        public Func<Dictionary<string, string>, Dictionary<string, string>, int>? Handler { get; set; }

        
    }
}
