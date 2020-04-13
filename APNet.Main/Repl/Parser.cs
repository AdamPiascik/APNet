using APNet.Core.ErrorHandling;
using System.Collections.Generic;
using System.Linq;

namespace APNet.Main.Repl
{
    public class Parser
    {
        public Command ParseInput(
            string input)
        {
            var inputParts = input.Split(' ',
                System.StringSplitOptions.RemoveEmptyEntries
            ).ToList();

            inputParts.ForEach(x => x.Trim());

            var command =
                CommandDictionary.GetCommand(inputParts[0]);

            if (command == null)
                return null;

            TryParseArguments(
                command,
                inputParts
            );

            return command;

            void TryParseArguments(
                Command command,
                List<string> inputParts)
            {
                for (int i = 1; i < inputParts.Count; ++i)
                {
                    string thisPart = inputParts[i];
                    Argument argument;

                    if (thisPart.StartsWith('-'))
                    {
                        string argumentName =
                            thisPart.Substring(1)
                            .ToLower();

                        if (argumentName.Length == 1)
                        {
                            argument = command.ValidArguments.Where(
                                x => x.Alias.ToLower() == argumentName
                            ).FirstOrDefault();
                        }
                        else
                        {
                            argument = command.ValidArguments.Where(
                                x => x.FullName.ToLower() == argumentName
                            ).FirstOrDefault();
                        }

                        if (argument == null)
                        {
                            ErrorHandler.HandleReplError(Error.InvalidArgumentError(
                                    command.Name,
                                    thisPart
                                )
                            );
                        }

                        command.AppliedArguments.Add(
                            argument.FullName,
                            inputParts[i + 1]
                        );
                    }
                    else
                    {
                        argument = command.ValidArguments.Where(
                                x => x.Position == i
                            ).FirstOrDefault();

                        if (argument == null)
                        {
                            ErrorHandler.HandleReplError(Error.InvalidArgumentError(
                                    command.Name,
                                    thisPart
                                )
                            );
                        }

                        command.AppliedArguments.Add(
                            argument.FullName,
                            thisPart
                        );
                    }
                }
            }
        }
    }
}
