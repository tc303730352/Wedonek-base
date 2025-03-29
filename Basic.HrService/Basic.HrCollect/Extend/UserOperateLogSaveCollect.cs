using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Ioc;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Error;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Helper.Interface;

namespace Basic.HrCollect.Extend
{
    internal class UserOperateLogSaveCollect : IUserOperateLogSaveCollect
    {
        private readonly IDelayDataSave<OperateLogAdd> _DelaySave;
        private readonly IIpAddressCache _Cache;
        public UserOperateLogSaveCollect ( IIpAddressCache cache )
        {
            this._Cache = cache;
            this._DelaySave = new DelayDataSave<OperateLogAdd>(this._save, 10, 5);
        }
        private void _save ( ref OperateLogAdd[] datas )
        {
            DBUserOperateLog[] logs = datas.ConvertAll(a =>
            {
                DBUserOperateLog log = new DBUserOperateLog
                {
                    AddTime = a.Begin,
                    BusType = a.BusType,
                    IsSuccess = a.IsError == false,
                    EmpId = a.EmpId,
                    EmpName = a.EmpName,
                    DeptName = a.DeptName,
                    ErrorCode = a.LastError,
                    Id = IdentityHelper.CreateId(),
                    Ip = a.Ip,
                    Referer = a.Referer.AbsoluteUri,
                    Result = a.Result,
                    ReqParam = a.PostStr,
                    Title = a.Title,
                    Uri = a.Uri.AbsolutePath,
                    UserType = a.UserType,
                    Duration = a.Duration
                };
                if ( !a.IsError && LocalErrorManage.GetErrorMsg(a.LastError, out ErrorMsg msg) )
                {
                    log.FailShow = msg.Text;
                }
                log.Address = this._Cache.GetIpAddress(a.Ip);
                return log;
            });
            using ( IocScope scope = RpcClient.Ioc.CreateScore() )
            {
                IUserOperateLogDAL logDAL = scope.Resolve<IUserOperateLogDAL>();
                logDAL.Adds(logs);
            }
        }

        public void Add ( OperateLogAdd[] adds )
        {
            this._DelaySave.AddData(adds);
        }
    }
}
