using System.Net;

namespace PMSOFTBookTestTask.Service.Extensions
{
    public static class GetIPAdress
    {
        public static async Task<string> GetIPAsync()
        {
            return await Task.Run(() =>
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList
                          .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                          .ToString()!;
            });
        }
    }
}
