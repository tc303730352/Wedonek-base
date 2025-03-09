using Basic.HrRemoteModel.OperatePower;
using Basic.HrRemoteModel.OperatePower.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class OperatePowerEvent : IRpcApiService
    {
        private readonly IOperatePowerService _Service;

        public OperatePowerEvent ( IOperatePowerService service )
        {
            this._Service = service;
        }
        public OperatePowerBase[] GetsEnableOpPower ( GetsEnableOpPower obj )
        {
            return this._Service.GetEnables(obj.PowerId);
        }
        public OperatePowerDto[] GetsOperatePower ( GetsOperatePower obj )
        {
            return this._Service.Gets(obj.PowerId);
        }
        public long AddOperatePower ( AddOperatePower add )
        {
            return this._Service.Add(add.Data);
        }
        public bool SetOperatePower ( SetOperatePower obj )
        {
            return this._Service.Set(obj.Id, obj.Data);
        }
        public bool SetOperatePowerIsEnable ( SetOperatePowerIsEnable obj )
        {
            return this._Service.SetIsEnable(obj.Id, obj.IsEnable);
        }
    }
}
