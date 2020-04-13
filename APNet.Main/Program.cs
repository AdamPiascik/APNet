using APNet.Main.Repl;
using System;
using System.Threading.Tasks;

namespace APNet.Main
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            Console.Write("Setting things up...");

            var simulation = new Simulation();

            //read config file          

            //simulation.CreateNetworkInfrastructure(
            //    deviceDefinitionsDirectory,
            //    networkMapsDirectory
            //);

            Console.WriteLine("Ready to go!");

            await new Loop().Init(simulation);
        }
    }
}
