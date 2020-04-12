using APNet.Core.ErrorHandling;
using APNet.Core.Graphs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static APNet.Core.Helpers.IPAddressHelper;

namespace APNet.Infrastructure.Types
{
    public class NetworkMap : Graph<uint>
    {
        private const char MapFileDelimiter = '-';
        private const string MapFileExtension = ".netmap";

        public static List<Error> TryBuild(
            string networkMapsDirectory,
            out NetworkMap networkMap)
        {
            var errors = new List<Error>();
            networkMap = new NetworkMap();

            errors.AddRange(
                networkMap.ReadNetworkMapsFromDirectory(
                    networkMapsDirectory
                )
            );

            return errors;
        }

        public List<Error> ReadNetworkMapsFromDirectory(
            string networkMapsDirectory)
        {
            var errors = new List<Error>();

            var networkMapFiles = new List<string>();

            if (Directory.Exists(networkMapsDirectory))
            {
                networkMapFiles =
                    Directory.GetFiles(
                        networkMapsDirectory,
                        MapFileExtension
                    )
                    .ToList();
            }
            else
            {
                errors.Add(
                    Error.DirectoryNotFoundError(networkMapsDirectory)
                );
            }

            foreach (var file in networkMapFiles)
            {
                string line;
                using var streamReader = new StreamReader(file);

                int lineNumber = 1;
                while ((line = streamReader.ReadLine()) != null)
                {
                    errors.AddRange(ParseMapFileLine(line, lineNumber));
                    ++lineNumber;
                }
            }

            return errors;
        }

        public List<Error> ParseMapFileLine(
            string line,
            int lineNumber)
        {
            var errors = new List<Error>();

            string[] splitLine = line.Split(MapFileDelimiter);

            if (splitLine.Length != 3)
            {
                errors.Add(
                    Error.NetworkMapFileParseError(lineNumber)
                );

                return errors;
            }

            uint address1 = 0;
            uint address2 = 0;
            uint cost = 0;

            if (!TryParseIPv4Address(splitLine[0].Trim(), out address1)
                || !TryParseIPv4Address(splitLine[1].Trim(), out address2)
                || !uint.TryParse(splitLine[2], out cost))
            {
                errors.Add(
                    Error.NetworkMapFileParseError(lineNumber)
                );
            }

            if (!errors.Any())
                AddUndirectedEdge(address1, address2, cost);

            return errors;
        }
    }
}
