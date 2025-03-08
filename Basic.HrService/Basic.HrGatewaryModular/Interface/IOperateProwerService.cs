using Basic.HrGatewaryModular.Model.OpPrower;
using Basic.HrRemoteModel.OperatePrower.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperateProwerService
    {
        OperatePrower[] GetEnables ( long roleId, long prowerId );
        long Add ( OperateProwerAdd data );
        bool Set ( long id, OperateProwerSet data );
        bool SetIsEnable ( long id, bool isEnable );
        OperateProwerDto[] Gets ( long prowerId );
    }
}