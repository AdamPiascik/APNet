using System;
using System.Threading.Tasks;

namespace APNet.Main.Repl
{
    public class Loop
    {
        private Parser Parser { get; set; } = new Parser();
        private Executor Executor { get; set; } = new Executor();

        public async Task Init(
            Simulation simulation)
        {
            while (true)
            {
                string input = Console.ReadLine();
                await HandleInput(input);
            }

            async Task HandleInput(
                string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return;

                var command = Parser.ParseInput(input);

                if (command != null)
                    await Executor.ExecuteCommandAsync(command);
            }
        }
    }
}
