using Basic.HrCollect;
using Basic.HrRemoteModel.OpLog;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class UserOperateLogEvent : IRpcApiService
    {
        private readonly IUserOperateLogSaveCollect _Service;

        public UserOperateLogEvent ( IUserOperateLogSaveCollect service )
        {
            this._Service = service;
        }

        public void SaveOperateLog ( SaveOperateLog obj )
        {
            this._Service.Add(obj.Logs);
        }
    }
}
