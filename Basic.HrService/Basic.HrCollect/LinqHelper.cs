using WeDonekRpc.Helper;

namespace Basic.HrCollect
{
    internal static class LinqHelper
    {
        private static readonly char _val = '|';
        public static long[] ToLongArray ( this string sour )
        {
            if ( sour.IsNull() )
            {
                return Array.Empty<long>();
            }
            return sour.SplitToLong(_val);
        }
    }
}
