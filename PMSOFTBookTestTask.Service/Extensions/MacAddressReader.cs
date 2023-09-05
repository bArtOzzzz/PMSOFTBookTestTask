using System.Net.NetworkInformation;

namespace PMSOFTBookTestTask.Service.Extensions
{
    public static class MacAddressReader
    {
        public static Task<string> GetMACAsync()
        {
            string macAddress = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddress = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return Task.FromResult(macAddress);
        }
    }
}
