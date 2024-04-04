/*
*BSD 3 - Clause License
*
* Copyright(c) 2024
* All rights reserved.
 *
 *Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *1.Redistributions of source code must retain the above copyright notice,
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

using SimpleCLI.CliClasses;
using SimpleCLI.Commands;
using System.ComponentModel;

namespace SimpleCLI;

class Program
{
    /// <summary>
    /// Demo application for this command line application
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    static async Task<int> Main(string[] args)
    {
        // TODO: Get this information from a login screen
        string session = "SessionDataString";
        SessionData sessionData = new SessionData("Shemp", "Howard", session);

        var interpreterEngine = new CLIInterpreterEngine("Sample app for command line testing");

        interpreterEngine.AddCommand(new OptionExampleCommand(sessionData));
        interpreterEngine.AddCommand(new AddCommand(sessionData));
        interpreterEngine.AddCommand(new SubCommand(sessionData));
        interpreterEngine.AddCommand(new MultiplyCommand(sessionData));
        interpreterEngine.AddCommand(new DivideCommand(sessionData));
        interpreterEngine.AddCommand(new WaitSecondsCommand(sessionData));
        interpreterEngine.AddCommand(new ExitCommand(sessionData));

        try
        {
            while (true)
            {
                Console.Write(">> ");

                string? input = Console.ReadLine();
                if (input != null)
                {
                    string[] parts = CreateTokensFromCommandLineInput(input);
                    if (parts != null && parts.Length > 0) 
                    {
                        await interpreterEngine.ProcessCommandLine(parts);
                    }
                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return 0;
    }


    static string[] CreateTokensFromCommandLineInput(string input)
    {
        List<string> tokens = new List<string>();   

        int inputLen = input.Length;

        for (int i = 0; i < inputLen; i++) 
        {
            if (char.IsWhiteSpace(input[i]))
                continue;

            if (input[i] == '"')
            {
                int StartQuoteIndex = i;
                int EndQuoteIndex = input.IndexOf('"', StartQuoteIndex + 1);
                if (EndQuoteIndex == -1) 
                {
                    Console.WriteLine("Mismatched Quote within the command line at offset {i}");
                    return new string[] { };
                }
                else 
                {
                    tokens.Add(input.Substring(StartQuoteIndex+1, (EndQuoteIndex-StartQuoteIndex-1)));
                    i = EndQuoteIndex+1;
                    continue;
                }
            }

            // we have a non white space/non quote section of code

            int StartTokenIndex = i;
            while (i < inputLen) 
            {
                if (char.IsWhiteSpace(input[i]))
                    break;

                i++;
            }
            int EndTokenIndex = i;
            tokens.Add(input.Substring(StartTokenIndex, EndTokenIndex-StartTokenIndex));
        }

        return tokens.ToArray();
    }




}
