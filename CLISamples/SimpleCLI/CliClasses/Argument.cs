using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.CliClasses
{
    internal class Argument
    {
        List<Command> parents = new List<Command>();
        private string name;
        private string description; 

        public Argument(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public void AddParent(Command parent)
        {
            (parents ??= new()).Add(parent);
        }

    }
}
