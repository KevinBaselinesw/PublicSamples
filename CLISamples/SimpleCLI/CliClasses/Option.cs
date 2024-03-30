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


        public Option(string name, string description)
        {
            this.name = name;
            this.description = description;
            _optionAliases = null;
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
