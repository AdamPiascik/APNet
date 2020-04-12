using System;

namespace APNet.Core.Helpers
{
    public static class IPAddressHelper
    {
        public static bool TryParseIPv4Address(
            string strAddress,
            out uint parsedAddress)
        {
            parsedAddress = 0;

            string[] addressParts = strAddress.Split('.');

            if (addressParts.Length != 4)
                return false;

            byte[] addressBytes = new byte[4];

            for (byte i = 0; i < 4; ++i)
            {
                if (!byte.TryParse(addressParts[i], out byte parsedPart))
                    return false;

                addressBytes[i] = parsedPart;
            }

            parsedAddress = BitConverter.ToUInt32(addressBytes);

            return true;
        }

        public static string GetFriendlyIPv4Address(
            uint ipAddress)
        {
            byte[] addressBytes = BitConverter.GetBytes(ipAddress);

            return
                addressBytes[0].ToString()
                + "."
                + addressBytes[1].ToString()
                + "."
                + addressBytes[2].ToString()
                + "."
                + addressBytes[3].ToString();
        }
    }
}
