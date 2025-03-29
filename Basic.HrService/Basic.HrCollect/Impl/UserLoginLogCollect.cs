using Basic.HrDAL;
using Basic.HrRemoteModel.LoginLog.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect.Impl
{
    internal class UserLoginLogCollect : IUserLoginLogCollect
    {
        private readonly IUserLoginLogDAL _LogDAL;

        public UserLoginLogCollect ( IUserLoginLogDAL logDAL )
        {
            this._LogDAL = logDAL;
        }

        public Result[] Query<Result> ( LoginLogQuery query, IBasicPage paging, out int count ) where Result : class
        {
            return this._LogDAL.Query<Result>(query, paging, out count);
        }
    }
}
