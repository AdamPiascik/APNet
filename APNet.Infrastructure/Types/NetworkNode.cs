using System.Collections.Generic;

namespace APNet.Infrastructure.Types
{
    public class NetworkNode
    {
        public HashSet<string> SupportedApplicationLayerProtocols { get; set; }
        public HashSet<string> SupportedTransportLayerProtocols { get; set; }
        public HashSet<string> SupportedInternetLayerProtocols { get; set; }
        public HashSet<string> SupportedLinkLayerProtocols { get; set; }
        public string HostName { get; set; }
        public string IPv4Address { get; set; }
        public IList<NetworkNode> ConnectedNodes { get; set; }
    }
}
