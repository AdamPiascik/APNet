using APNet.Core.ErrorHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace APNet.Infrastructure.Types
{
    public class DeviceList
    {
        private const string DeviceFileExtension = ".netdev";

        public List<Client> Clients { get; set; } = new List<Client>();
        public List<ContentServer> ContentServers { get; set; } = new List<ContentServer>();
        public List<DnsServer> DnsServers { get; set; } = new List<DnsServer>();
        public List<Switch> Switches { get; set; } = new List<Switch>();

        public List<dynamic> All
        {
            get
            {
                var allNodes = new List<dynamic>();

                allNodes.AddRange(Clients);
                allNodes.AddRange(ContentServers);
                allNodes.AddRange(DnsServers);
                allNodes.AddRange(Switches);

                return allNodes;
            }
        }

        public static List<Error> TryAddAllDevices(
            string deviceDefinitionsDirectory,
            out DeviceList devices)
        {
            var errors = new List<Error>();
            devices = new DeviceList();

            errors.AddRange(
                devices.ReadDeviceDefinitionsFromDirectory(
                    deviceDefinitionsDirectory
                )
            );

            return errors;
        }

        private List<Error> ReadDeviceDefinitionsFromDirectory(
            string deviceDefinitionsDirectory)
        {
            var errors = new List<Error>();
            var deviceDefinitionFiles = new List<string>();

            if (Directory.Exists(deviceDefinitionsDirectory))
            {
                deviceDefinitionFiles =
                    Directory.EnumerateFiles(
                        deviceDefinitionsDirectory
                    )
                    .Where(x => x.Contains(DeviceFileExtension))
                    .ToList();
            }
            else
            {
                errors.Add(
                    Error.DirectoryNotFoundError(deviceDefinitionsDirectory)
                );
            }

            foreach (var file in deviceDefinitionFiles)
            {
                var fileContents = File.ReadAllText(file);

                var jDoc =
                    JsonDocument.Parse(fileContents);

                foreach (var item in jDoc.RootElement.EnumerateObject())
                {
                    try
                    {
                        switch (item.Name.ToLower())
                        {
                            case "clients":
                                AddNetworkNodes<Client>(item.Value);
                                break;

                            case "contentservers":
                                AddNetworkNodes<ContentServer>(item.Value);
                                break;

                            case "dnsservers":
                                AddNetworkNodes<DnsServer>(item.Value);
                                break;

                            case "switches":
                                AddNetworkNodes<Switch>(item.Value);
                                break;

                            default:
                                break;
                        };
                    }
                    catch (Exception ex)
                    {
                        errors.Add(
                            Error.GeneralException(ex)
                        );
                    }
                }
            }

            return errors;

            void AddNetworkNodes<T>(
                JsonElement array)
            {
                var settings =
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                foreach (var item in array.EnumerateArray())
                {
                    switch (typeof(T))
                    {
                        case var x when x == typeof(Client):
                            Clients.Add(
                                JsonSerializer.Deserialize<Client>(
                                    item.GetRawText(),
                                    settings
                                )
                            );
                            break;
                        case var x when x == typeof(ContentServer):
                            ContentServers.Add(
                                JsonSerializer.Deserialize<ContentServer>(
                                    item.GetRawText(),
                                    settings
                                )
                            );
                            break;
                        case var x when x == typeof(DnsServer):
                            DnsServers.Add(
                                JsonSerializer.Deserialize<DnsServer>(
                                    item.GetRawText(),
                                    settings
                                )
                            );
                            break;
                        case var x when x == typeof(Switch):
                            Switches.Add(
                                JsonSerializer.Deserialize<Switch>(
                                    item.GetRawText(),
                                    settings
                                )
                            );
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
