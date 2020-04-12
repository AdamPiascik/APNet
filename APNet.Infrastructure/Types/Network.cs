using APNet.Core.ErrorHandling;
using System.Collections.Generic;
using System.Linq;

namespace APNet.Infrastructure.Types
{
    public class Network
    {
        public DeviceList Devices { get; set; }
        public NetworkMap NetworkMap { get; set; }        

        public List<Error> TryAddDevicesToNetwork(
            string deviceDefinitionsDirectory)
        {
            var errors =
                DeviceList.TryAddAllDevices(
                    deviceDefinitionsDirectory,
                    out DeviceList devices
                );

            if (!errors.Any())
                Devices = devices;

            return errors;
        }

        public List<Error> TryBuildNetworkMap(
            string networkMapsDirectory)
        {
            var errors =
                NetworkMap.TryBuild(
                    networkMapsDirectory,
                    out NetworkMap networkMap
                );

            errors.AddRange(
                ValidateNetworkMap()
            );

            if (!errors.Any())
                NetworkMap = networkMap;

            return errors;
        }

        private List<Error> ValidateNetworkMap()
        {
            var errors = new List<Error>();

            if (NetworkMap == null)
                errors.Add(Error.NoNetworkMapError());

            if (Devices == null)
                errors.Add(Error.NoDevicesError());

            if (errors.Any())
                return errors;

            var validIpAddresses =
                Devices.All.Select(x => (uint)x.IPv4Address);

            foreach (var ipAddress in NetworkMap.Nodes.Keys)
            {
                if (!validIpAddresses.Contains(ipAddress))
                {
                    errors.Add(
                        Error.InvalidNetworkMapIPAddressError(ipAddress)
                    );
                }
            }

            return errors;
        }
    }
}
