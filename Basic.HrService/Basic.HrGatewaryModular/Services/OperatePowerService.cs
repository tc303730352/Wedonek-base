using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.OpPower;
using Basic.HrRemoteModel.OperatePower;
using Basic.HrRemoteModel.OperatePower.Model;
using Basic.HrRemoteModel.RolePower;
using WeDonekRpc.Helper;

namespace Basic.HrGatewaryModular.Services
{
    internal class OperatePowerService : IOperatePowerService
    {
        public long Add ( OperatePowerAdd data )
        {
            return new AddOperatePower
            {
                Data = data
            }.Send();
        }
        public bool Set ( long id, OperatePowerSet data )
        {
            return new SetOperatePower
            {
                Id = id,
                Data = data
            }.Send();
        }
        public OperatePower GetEnables ( long roleId, long powerId )
        {
            OperatePowerBase[] list = new GetsEnableOpPower
            {
                PowerId = powerId
            }.Send();
            if ( list.IsNull() )
            {
                return null;
            }
            return new OperatePower
            {
                Powers = list,
                Selected = new GetRoleOperateId
                {
                    RoleId = roleId,
                    PowerId = powerId
                }.Send()
            };
        }
        public bool SetIsEnable ( long id, bool isEnable )
        {
            return new SetOperatePowerIsEnable
            {
                Id = id,
                IsEnable = isEnable
            }.Send();
        }

        public OperatePowerDto[] Gets ( long powerId )
        {
            return new GetsOperatePower
            {
                PowerId = powerId
            }.Send();
        }
    }
}
