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
    internal class MultiplyCommand : Command
    {
        SessionData sessionData;

        public MultiplyCommand(SessionData sessionData) : base("Mult", "Command to multiply two numbers together")
        {
            this.sessionData = sessionData;

            var MultArgument1 = new Argument<double>(name: "m1", description: "");
            var MultArgument2 = new Argument<double>(name: "m2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(MultArgument1);
            Cmd.Add(MultArgument2);

            Cmd.AddAlias("mult");


            Cmd.SetHandler((MultArgument1Value, MultArgument2Value) =>
            {
                MultiplyCommandHandler(MultArgument1Value, MultArgument2Value);
            }, MultArgument1, MultArgument2);
        }

        void MultiplyCommandHandler(double p1, double p2)
        {
            double total = p1 * p2;
            Console.WriteLine(string.Format($"{p1} * {p2} = {total}"));

            Console.WriteLine(sessionData.ToString());
        }


    }
}
