/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2024
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System.CommandLine;


namespace Calc.Commands
{
    internal class OptionExampleCommand : Command
    {
        SessionData sessionData;

        public OptionExampleCommand(SessionData sessionData) : base("OptionExample", "Command to demo usage of options")
        {
            this.sessionData = sessionData;

            var x1option = new Option<string?>(name: "--x1", description: "x1 option.");
            x1option.AddAlias("-x1");

            var x2option = new Option<string?>(name: "--x2", description: "x2 option.");
            x2option.AddAlias("-x2");

            var x3option = new Option<string?>(name: "--x3", description: "x3 option.");
            x3option.AddAlias("-x3");

       
            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.


            Cmd.AddOption(x1option);
            Cmd.AddOption(x2option);
            Cmd.AddOption(x3option);

            Cmd.AddAlias("optionexample");


            Cmd.SetHandler((x1, x2, x3) =>
            {
                OptionExampleCommandHandler(x1!, x2!, x3!);
            }, x1option, x2option, x3option);

        }

        void OptionExampleCommandHandler(string x1, string x2, string x3)
        {
            if (!string.IsNullOrEmpty(x1))
                Console.WriteLine("x1 = " + x1);
            else
                Console.WriteLine("x1 option not specified");

            if (!string.IsNullOrEmpty(x2))
                Console.WriteLine("x2 = " + x2);
            else
                Console.WriteLine("x2 option not specified");

            if (!string.IsNullOrEmpty(x3))
                Console.WriteLine("x3 = " + x3);
            else
                Console.WriteLine("x3 option not specified");

            Console.WriteLine(sessionData.ToString());

        }

    }
}
