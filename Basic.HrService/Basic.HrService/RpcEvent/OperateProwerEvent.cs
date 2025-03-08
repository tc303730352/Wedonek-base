using Basic.HrRemoteModel.OperatePrower;
using Basic.HrRemoteModel.OperatePrower.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class OperateProwerEvent : IRpcApiService
    {
        private readonly IOperateProwerService _Service;

        public OperateProwerEvent ( IOperateProwerService service )
        {
            this._Service = service;
        }
        public OperateProwerBase[] GetsEnableOpPrower ( GetsEnableOpPrower obj )
        {
            return this._Service.GetEnables(obj.ProwerId);
        }
        public OperateProwerDto[] GetsOperatePrower ( GetsOperatePrower obj )
        {
            return this._Service.Gets(obj.ProwerId);
        }
        public long AddOperatePrower ( AddOperatePrower add )
        {
            return this._Service.Add(add.Data);
        }
        public bool SetOperatePrower ( SetOperatePrower obj )
        {
            return this._Service.Set(obj.Id, obj.Data);
        }
        public bool SetOperateProwerIsEnable ( SetOperateProwerIsEnable obj )
        {
            return this._Service.SetIsEnable(obj.Id, obj.IsEnable);
        }
    }
}
