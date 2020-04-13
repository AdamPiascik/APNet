using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APNet.Main.Repl
{
    public class Executor
    {
        private const BindingFlags defaultBindingFlags =
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

        public async Task ExecuteCommandAsync(
            Command command)
        {
            var method =
                 typeof(Executor).GetMethod(
                     command.Name,
                     defaultBindingFlags
                 );

            await (Task)method.Invoke(
                this,
                new object[] { command }
            );
        }

        private Task Quit(
            Command command)
        {
            Console.WriteLine("\nBye!\n");
            Environment.Exit(1);

            return Task.CompletedTask;
        }

        private Task Help(
            Command command)
        {
            if (!command.AppliedArguments.Any())
                Console.WriteLine("\nGeneral Help\n");

            else
            {
                var helpMessage = CommandDictionary.GetCommand(command.AppliedArguments.First().Value)?.Documentation
                    ?? command.AppliedArguments.First().Value;

                Console.WriteLine($"\n{helpMessage}");
            }

            return Task.CompletedTask;
        }
    }
}
