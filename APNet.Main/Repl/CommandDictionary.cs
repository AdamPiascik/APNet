using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace APNet.Main.Repl
{
    public static class CommandDictionary
    {
        private const string commandsResourceName = "APNet.Core.Config.ValidCommands.json";

        public static List<Command> ValidCommands { get; set; }

        static CommandDictionary()
        {
            var commandsJson =
                Assembly.Load("APNet.Core")
                .GetManifestResourceStream(commandsResourceName);

            var settings =
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

            var reader = new StreamReader(commandsJson);

            ValidCommands =
                JsonSerializer.Deserialize<List<Command>>(
                    reader.ReadToEnd(), settings
                );
        }

        public static Command GetCommand(
            string commandName)
        {
            var command =
                ValidCommands.Where(
                    x => x.Name.ToLower() == commandName.ToLower()
                ).FirstOrDefault();

            if (command == null)
                return null;

            return new Command()
            {
                Name = command.Name,
                ValidArguments = command.ValidArguments,
                MinimumNumberOfArguments = command.MinimumNumberOfArguments,
                MaximumNumberOfArguments = command.MaximumNumberOfArguments
            };
        }
    }
}
