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
    internal class SubCommand : Command
    {
        SessionData sessionData;

        public SubCommand(SessionData sessionData) : base("Sub", "Command to subtract two numbers from each other")
        {
            this.sessionData = sessionData;

            var SubArgument1 = new Argument<double>(name: "s1", description: "");
            var SubArgument2 = new Argument<double>(name: "s2", description: "");

            Command? Cmd = this;  // necessary to cast to nullable to Command so .SetHandler is found.

            Cmd.Add(SubArgument1);
            Cmd.Add(SubArgument2);

            Cmd.AddAlias("sub");


            Cmd.SetHandler((SubArgument1Value, SubArgument2Value) =>
            {
                SubCommandHandler(SubArgument1Value, SubArgument2Value);
            }, SubArgument1, SubArgument2);
        }

        void SubCommandHandler(double p1, double p2)
        {
            double sum = p1 - p2;
            Console.WriteLine(string.Format($"{p1} - {p2} = {sum}"));

            Console.WriteLine(sessionData.ToString());
        }


    }
}
