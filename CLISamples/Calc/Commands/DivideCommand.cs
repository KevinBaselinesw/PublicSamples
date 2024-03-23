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
    internal class DivideCommand : Command
    {
        public DivideCommand() : base("Divide", "Command to divide two numbers together")
        {
            var DivArgument1 = new Argument<double>(name: "d1", description: "");
            var DivArgument2 = new Argument<double>(name: "d2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(DivArgument1);
            Cmd.Add(DivArgument2);

            Cmd.AddAlias("divide");


            Cmd.SetHandler((DivArgument1Value, DivArgument2Value) =>
            {
                DivideCommandHandler(DivArgument1Value, DivArgument2Value);
            }, DivArgument1, DivArgument2);
        }

        static void DivideCommandHandler(double p1, double p2)
        {
            double total = p1 / p2;
            Console.WriteLine(string.Format($"{p1} * {p2} = {total}"));
        }


    }
}
