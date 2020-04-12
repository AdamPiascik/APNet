using APNet.Core.ErrorHandling;
using APNet.Infrastructure.Types;
using System.Collections.Generic;
using System.Linq;

namespace APNet.Main
{
    public class Simulation
    {
        public Network Network { get; set; }

        public void CreateNetworkInfrastructure(
            string deviceDefinitionsDirectory,
            string networkMapsDirectory)
        {
            Network = new Network();

            var errors = new List<Error>();

            errors.AddRange(
                Network.TryAddDevicesToNetwork(deviceDefinitionsDirectory)
            );

            if (errors.Any())
                ErrorHandler.HandleErrors(errors);

            errors.AddRange(
                Network.TryBuildNetworkMap(networkMapsDirectory)
            );

            if (errors.Any())
                ErrorHandler.HandleErrors(errors);
        }
    }
}
