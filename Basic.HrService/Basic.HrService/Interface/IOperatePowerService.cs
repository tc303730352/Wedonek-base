using Basic.HrRemoteModel.OperatePower.Model;

namespace Basic.HrService.Interface
{
    public interface IOperatePowerService
    {
        long Add ( OperatePowerAdd data );
        OperatePowerBase[] GetEnables ( long powerId );
        OperatePowerDto[] Gets ( long powerId );
        bool Set ( long id, OperatePowerSet data );
        bool SetIsEnable ( long id, bool isEnable );
    }
}