using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.OpPrower;
using Basic.HrRemoteModel.OperatePrower;
using Basic.HrRemoteModel.OperatePrower.Model;
using Basic.HrRemoteModel.RolePrower;
using WeDonekRpc.Client;
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
        public OperatePrower[] GetEnables ( long roleId, long prowerId )
        {
            OperateProwerBase[] list = new GetsEnableOpPrower
            {
                ProwerId = prowerId
            }.Send();
            if ( list.IsNull() )
            {
                return Array.Empty<OperatePrower>();
            }
            OperatePrower[] dtos = list.ConvertMap<OperateProwerBase, OperatePrower>();
            long[] ids = new GetRoleOperateId
            {
                RoleId = roleId,
                ProwerId = prowerId
            }.Send();
            dtos.ForEach(c =>
            {
                c.IsSelected = ids.Contains(c.Id);
            });
            return dtos;
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
