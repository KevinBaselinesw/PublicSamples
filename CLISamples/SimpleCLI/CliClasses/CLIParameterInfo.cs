using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCLI.CliClasses
{
    enum CLIParameterType
    {
        Argument = 1,
        Option = 2,
    }

    internal class CLIParameterInfo
    {
        public CLIParameterType ParameterType { get; set; } = CLIParameterType.Argument;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;   
    }
}
