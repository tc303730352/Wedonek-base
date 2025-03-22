using WeDonekRpc.Modular;

namespace Basic.HrGatewaryModular
{
    internal static class LinqHelper
    {
        public static long ToEmpId ( this IUserState state )
        {
            return state.GetValue<long>("UserId");
        }
        public static long ToCompanyId ( this IUserState state )
        {
            return state.GetValue<long>("CompanyId");
        }
    }
}
