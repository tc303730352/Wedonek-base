using Basic.HrCollect;
using Basic.HrRemoteModel.OpLog;
using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class UserOperateLogEvent : IRpcApiService
    {
        private readonly IUserOperateLogSaveCollect _Service;
        private readonly IOperateLogCollect _OpLog;
        public UserOperateLogEvent ( IUserOperateLogSaveCollect service, IOperateLogCollect opLog )
        {
            this._OpLog = opLog;
            this._Service = service;
        }
        public PagingResult<OperateLogDto> QueryOperateLog ( QueryOperateLog obj )
        {
            OperateLogDto[] logs = this._OpLog.Query<OperateLogDto>(obj.Query, obj.ToBasicPage(), out int count);
            return new PagingResult<OperateLogDto>(logs, count);
        }
        public void SaveOperateLog ( SaveOperateLog obj )
        {
            this._Service.Add(obj.Logs);
        }
    }
}
