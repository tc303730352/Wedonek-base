using Basic.HrCollect.Helper;
using WeDonekRpc.CacheClient.Interface;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Extend
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class IpAddressCache : IIpAddressCache
    {
        private readonly ILocalCacheController _LocalCache;

        public IpAddressCache ( ILocalCacheController localCache )
        {
            this._LocalCache = localCache;
        }

        public string GetIpAddress ( string ip )
        {
            string key = "IpKey_" + ip;
            if ( this._LocalCache.TryGet(key, out string addr) )
            {
                return addr;
            }
            addr = this._GetIpAddress(ip);
            if ( addr == string.Empty )
            {
                return addr;
            }
            _ = this._LocalCache.Set(key, addr, new TimeSpan(8, 0, 0));
            return addr;
        }
        private string _GetIpAddress ( string ip )
        {
            try
            {
                return IpAddressHelper.GetAddress(ip);
            }
            catch ( Exception ex )
            {
                ErrorException.FormatError(ex).SaveLog("LoginLog");
                return string.Empty;
            }
        }
    }
}
