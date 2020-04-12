namespace APNet.Core.ErrorHandling
{
    public static class ErrorMessages
    {
        public static string DirectoryNotFoundError(string directory) =>
            $"Could not find the requested directory:{directory}";

        public static string NetworkMapFileParseError(int lineNumber) =>
            $"There was an error parsing line {lineNumber} of the network map file. Please refer to README.md for the expected syntax";

        public static string InvalidNetworkMapIPAddressError(string ipAddress) =>
            $"A node with IP address {ipAddress} cannot be specified in the network map, as it isn't contained in the list of available nodes.";

        public const string NoNetworkMapError =
            "There is no network map loaded";

        public const string NoDevicesError =
            "There are no devices loadedo";
    }
}
