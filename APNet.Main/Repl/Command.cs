using System.Collections.Generic;

namespace APNet.Main.Repl
{
    public class Command
    {
        public string Name { get; set; }
        public int MinimumNumberOfArguments { get; set; }
        public int MaximumNumberOfArguments { get; set; }
        public string Documentation { get; set; }

        public List<Argument> ValidArguments { get; set; }
         = new List<Argument>();

        public Dictionary<string, string> AppliedArguments { get; set; }
            = new Dictionary<string, string>();
    }
}
