using System.Net;
using System.Text;
using WeDonekRpc.Helper.Http;

namespace Basic.HrCollect.Helper
{
    internal static class IpAddressHelper
    {
        private static readonly string _IpUri = "http://whois.pconline.com.cn/ipJson.jsp";
        private static readonly RequestSet _RequestSet = new RequestSet
        {
            ResponseEncoding = Encoding.GetEncoding("GBK"),
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36 Edg/134.0.0.0"
        };
        private static readonly string _LocalIp = "127.0.0.1";
        // 10.x.x.x/8
        private static readonly byte SECTION_1 = 0x0A;
        // 172.16.x.x/12
        private static readonly byte SECTION_2 = 0xAC;
        private static readonly byte SECTION_3 = 0x10;
        private static readonly byte SECTION_4 = 0x1F;
        // 192.168.x.x/16
        private static readonly byte SECTION_5 = 0xC0;
        private static readonly byte SECTION_6 = 0xA8;
        static IpAddressHelper ()
        {

        }

        public static string GetAddress ( string ip )
        {
            if ( InternalIp(ip) )
            {
                return "内网IP";
            }
            string uri = string.Format("{0}?ip={1}&json=true", _IpUri, ip);
            HttpResult result = HttpClientFactory.Create().SendGet(new Uri(uri), _RequestSet);
            dynamic obj = result.GetObject<object>();
            return obj.addr;
        }
        public static bool InternalIp ( string ip )
        {
            if ( ip == _LocalIp )
            {
                return true;
            }
            byte[] addr = IPAddress.Parse(ip).GetAddressBytes();
            return _InternalIp(addr);
        }
        private static bool _InternalIp ( byte[] addr )
        {
            if ( addr.Length < 2 )
            {
                return true;
            }
            byte v = addr[0];
            if ( v == SECTION_1 )
            {
                return true;
            }
            else if ( v == SECTION_2 )
            {
                return addr[1] >= SECTION_3 && addr[1] <= SECTION_4;
            }
            else if ( v == SECTION_5 )
            {
                return addr[1] == SECTION_6;
            }
            return false;
        }
    }
}
