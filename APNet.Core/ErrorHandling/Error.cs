using System;
using static APNet.Core.Helpers.IPAddressHelper;

namespace APNet.Core.ErrorHandling
{
    public class Error
    {
        public ErrorType Type { get; private set; }
        public ErrorSeverity Severity { get; private set; }
        public string Message { get; private set; }

        public Error(
            ErrorType type,
            ErrorSeverity severity,
            string message)
        {
            Type = type;
            Message = message;
            Severity = severity;
        }

        public static Error FileNotFoundError(
            string file)
        {
            return
                new Error(
                    ErrorType.ItemNotFoundError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.FileNotFoundError(file)
                );
        }

        public static Error DirectoryNotFoundError(
            string directory)
        {
            return
                new Error(
                    ErrorType.ItemNotFoundError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.DirectoryNotFoundError(directory)
                );
        }

        public static Error NetworkMapFileParseError(
            int lineNumber)
        {
            return
                new Error(
                    ErrorType.NetworkMapError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.NetworkMapFileParseError(lineNumber)
                );
        }

        public static Error InvalidNetworkMapIPAddressError(
            uint ipv4Address)
        {
            string friendlyIpv4Address =
                GetFriendlyIPv4Address(ipv4Address);

            return
                new Error(
                    ErrorType.NetworkMapError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.InvalidNetworkMapIPAddressError(friendlyIpv4Address)
                );
        }

        public static Error GeneralException(
            Exception exception)
        {
            return
                new Error(
                    ErrorType.GeneralException,
                    ErrorSeverity.Fatal,
                    exception.Message
                );
        }

        public static Error NoNetworkMapError()
        {
            return
                new Error(
                    ErrorType.NetworkMapError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.NoNetworkMapError
                );
        }

        public static Error NoDevicesError()
        {
            return
                new Error(
                    ErrorType.DeviceListError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.NoDevicesError
                );
        }

        public static Error InvalidArgumentError(
            string commandName,
            string argument)
        {
            return
                new Error(
                    ErrorType.InvalidArgumentError,
                    ErrorSeverity.Fatal,
                    ErrorMessages.InvalidArgumentError(commandName, argument)
                );
        }
    }
}
