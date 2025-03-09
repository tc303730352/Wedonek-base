using Basic.HrGatewaryModular.Model.OpPower;
using Basic.HrRemoteModel.OperatePower.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperatePowerService
    {
        OperatePower GetEnables ( long roleId, long powerId );
        long Add ( OperatePowerAdd data );
        bool Set ( long id, OperatePowerSet data );
        bool SetIsEnable ( long id, bool isEnable );
        OperatePowerDto[] Gets ( long powerId );
    }
}