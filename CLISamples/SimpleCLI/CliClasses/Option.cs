using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.CliClasses
{
    internal class Option
    {
        public bool IsGlobal { get; set; }

        private string name;
        private string description;
        private List<string>? _optionAliases;

        public int MinimumNumberOfArguments { get; set; }
        public int MaximumNumberOfArguments { get; set; }

        public Option(string name, string description)
        {
            this.name = name;
            this.description = description;
            _optionAliases = null;

            MinimumNumberOfArguments = 0;
            MaximumNumberOfArguments = 0;
        }

        public bool IsOptionMatch(string optionName)
        {
            if (this.Name == optionName)
                return true;

            if (this._optionAliases != null)
            {
                foreach (var alias in this._optionAliases)
                {
                    if (alias == optionName)
                        return true;
                }
            }

            return false;
        }


        public string Name { get { return name; } }
        public string Description { get { return description; } }

        List<Command> parents = new List<Command>();
        public void AddParent(Command parent)
        {
            (parents ??= new()).Add(parent);
        }

        public void AddAlias(string alias)
        {
            (_optionAliases ??= new()).Add(alias);
        }
    }
}
