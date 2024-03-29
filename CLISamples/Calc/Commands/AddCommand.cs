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
    internal class AddCommand : Command
    {
        SessionData sessionData;
        public AddCommand(SessionData sessionData) : base("Add", "Command to add two numbers together")
        {
            this.sessionData = sessionData;

            var AddArgument1 = new Argument<double>(name: "a1", description: "");
            var AddArgument2 = new Argument<double>(name: "a2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(AddArgument1);
            Cmd.Add(AddArgument2);

            Cmd.AddAlias("add");

            Cmd.SetHandler((AddArgument1Value, AddArgument2Value) =>
            {
                AddCommandHandler(AddArgument1Value, AddArgument2Value);
            },  AddArgument1, AddArgument2);
            this.sessionData = sessionData;
        }

        void AddCommandHandler(double p1, double p2)
        {
            double sum = p1 + p2;
            Console.WriteLine(string.Format($"{p1} + {p2} = {sum}"));

            Console.WriteLine(sessionData.ToString());
        }


    }
}
