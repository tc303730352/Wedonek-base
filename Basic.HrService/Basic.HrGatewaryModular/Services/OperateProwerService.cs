using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.OpPrower;
using Basic.HrRemoteModel.OperatePrower;
using Basic.HrRemoteModel.OperatePrower.Model;
using Basic.HrRemoteModel.RolePrower;
using WeDonekRpc.Helper;

namespace Basic.HrGatewaryModular.Services
{
    internal class OperateProwerService : IOperateProwerService
    {
        public long Add ( OperateProwerAdd data )
        {
            return new AddOperatePrower
            {
                Data = data
            }.Send();
        }
        public bool Set ( long id, OperateProwerSet data )
        {
            return new SetOperatePrower
            {
                Id = id,
                Data = data
            }.Send();
        }
        public OperatePrower GetEnables ( long roleId, long prowerId )
        {
            OperateProwerBase[] list = new GetsEnableOpPrower
            {
                ProwerId = prowerId
            }.Send();
            if ( list.IsNull() )
            {
                return null;
            }
            return new OperatePrower
            {
                Prowers = list,
                Selected = new GetRoleOperateId
                {
                    RoleId = roleId,
                    ProwerId = prowerId
                }.Send()
            };
        }
        public bool SetIsEnable ( long id, bool isEnable )
        {
            return new SetOperateProwerIsEnable
            {
                Id = id,
                IsEnable = isEnable
            }.Send();
        }

        public OperateProwerDto[] Gets ( long prowerId )
        {
            return new GetsOperatePrower
            {
                ProwerId = prowerId
            }.Send();
        }
    }
}
