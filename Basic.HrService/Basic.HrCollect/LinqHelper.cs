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
            sour = sour.Substring(1, sour.Length - 2);
            return sour.SplitToLong(_val);
        }
        public static string[] ToStringArray ( this string sour )
        {
            if ( sour.IsNull() )
            {
                return Array.Empty<string>();
            }
            sour = sour.Substring(1, sour.Length - 2);
            return sour.Split(_val);
        }
    }
}
