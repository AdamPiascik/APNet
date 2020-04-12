using APNet.Main.Repl;
using System;

namespace APNet.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Setting things up...");

            var simulation = new Simulation();

            //read config file

            string deviceDefinitionsDirectory = @"C:\Users\adamp\OneDrive\Desktop";
            string networkMapsDirectory = @"C:\Users\adamp\OneDrive\Desktop";

            simulation.CreateNetworkInfrastructure(
                deviceDefinitionsDirectory,
                networkMapsDirectory
            );

            Console.WriteLine("Ready to go!");

            new Loop().Init(simulation);
        }
    }
}
