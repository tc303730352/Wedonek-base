using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Helper.UAParser;
using WeDonekRpc.HttpService;
using WeDonekRpc.HttpService.Interface;
using WeDonekRpc.Modular;

namespace Basic.HrGatewaryModular
{
    internal static class LinqHelper
    {
        private static readonly Parser _UAgentParser = null;

        static LinqHelper ()
        {
            _UAgentParser = Parser.GetDefault();
        }

        public static LoginState ToLoginState ( this IHttpRequest request )
        {
            string agent = request.UAgent();
            ClientInfo client = _UAgentParser.Parse(agent);
            return new LoginState
            {
                LoginIp = request.ClientIp,
                Browser = client.UA.ToString(),
                SysName = client.OS.ToString(),
            };
        }
        public static long ToEmpId ( this IUserState state )
        {
            return state.GetValue<long>("UserId");
        }
        public static long ToCompanyId ( this IUserState state )
        {
            return state.GetValue<long>("CompanyId");
        }
        public static bool IsAdmin ( this IUserState state )
        {
            return state.GetValue<bool>("IsAdmin");
        }
        public static void CheckDeptPower ( this IUserState state, long deptId )
        {
            if ( state.GetValue<bool>("IsAdmin") )
            {
                return;
            }
            long[] ids = state.GetValue<long[]>("DeptId");
            if ( ids == null || !ids.Contains(deptId) )
            {
                throw new Exception("hr.emp.no.power");
            }
        }
        public static long[] PowerDeptId ( this IUserState state, long[] deptId )
        {
            if ( state.GetValue<bool>("IsAdmin") )
            {
                return deptId;
            }
            long[] ids = state.GetValue<long[]>("DeptId");
            if ( ids == null )
            {
                return Array.Empty<long>();
            }
            else if ( deptId == null )
            {
                return ids;
            }
            return ids.Intersect(deptId).ToArray();
        }
        public static long[] PowerDeptId ( this IUserState state )
        {
            if ( state.GetValue<bool>("IsAdmin") )
            {
                return null;
            }
            return state.GetValue<long[]>("DeptId", Array.Empty<long>());
        }
        public static T GetValue<T> ( this IUserState state, string name, T def ) where T : class
        {
            T res = state.GetValue<T>(name);
            if ( res == null )
            {
                return def;
            }
            return res;
        }
    }
}
