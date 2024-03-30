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
        private List<Command>? _subcommands;

        private string? _description;
        private string _commandName;
        private List<string>? _commandAliases;

        /// <summary>
        /// Initializes a new instance of the Command class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of the command, shown in help.</param>
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

        /// <summary>
        /// Represents all of the arguments for the command.
        /// </summary>
        public IReadOnlyList<Argument> Arguments => _arguments is not null ? _arguments : Array.Empty<Argument>();

        internal bool HasArguments => _arguments is not null;

        /// <summary>
        /// Represents all of the options for the command, including global options that have been applied to any of the command's ancestors.
        /// </summary>
        public IReadOnlyList<Option> Options => _options is not null ? _options : Array.Empty<Option>();
            

        /// <summary>
        /// Adds an <see cref="Argument"/> to the command.
        /// </summary>
        /// <param name="argument">The argument to add to the command.</param>
        public void AddArgument(Argument argument)
        {
            argument.AddParent(this);
            (_arguments ??= new()).Add(argument);
        }

     
        /// <summary>
        /// Adds an <see cref="Option"/> to the command.
        /// </summary>
        /// <param name="option">The option to add to the command.</param>
        public void AddOption(Option option)
        {
            option.AddParent(this);
            (_options ??= new()).Add(option);
        }

        public void AddAlias(string alias)
        {
            (_commandAliases ??= new()).Add(alias);
        }

        /// <summary>
        /// Adds a global <see cref="Option"/> to the command.
        /// </summary>
        /// <param name="option">The global option to add to the command.</param>
        /// <remarks>Global options are applied to the command and recursively to subcommands. They do not apply to
        /// parent commands.</remarks>
        public void AddGlobalOption(Option option)
        {
            option.IsGlobal = true;
            AddOption(option);
        }
        /// <summary>
        /// Adds an <see cref="Option"/> to the command.
        /// </summary>
        /// <param name="option">The option to add to the command.</param>
        public void Add(Option option) => AddOption(option);

        /// <summary>
        /// Adds an <see cref="Argument"/> to the command.
        /// </summary>
        /// <param name="argument">The argument to add to the command.</param>
        public void Add(Argument argument) => AddArgument(argument);

    

        /// <summary>
        /// Gets or sets the <see cref="ICommandHandler"/> for the command. The handler represents the action
        /// that will be performed when the command is invoked.
        /// </summary>
        /// <remarks>
        /// <para>Use one of the <see cref="Handler.SetHandler(Command, Action)" /> overloads to construct a handler.</para>
        /// <para>If the handler is not specified, parser errors will be generated for command line input that
        /// invokes this command.</para></remarks>
        public Func<Dictionary<string, string>, Dictionary<string, string>, int>? Handler { get; set; }

        
    }
}
