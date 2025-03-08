using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.OperatePrower;
using Basic.HrRemoteModel.OperatePrower.Model;

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
        public OperateProwerBase[] GetEnables ( long prowerId )
        {
            return new GetsEnableOpPrower
            {
                ProwerId = prowerId
            }.Send();
        }
        public bool SetIsEnable ( long id, bool isEnable )
        {
            return new SetOperateProwerIsEnable
            {
                Id = id,
                IsEnable = isEnable
            }.Send();
        }
    }
}
