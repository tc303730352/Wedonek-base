using Basic.HrRemoteModel.OperatePrower.Model;

namespace Basic.HrService.Interface
{
    public interface IOperateProwerService
    {
        long Add ( OperateProwerAdd data );
        OperateProwerBase[] GetEnables ( long prowerId );
        OperateProwerDto[] Gets ( long prowerId );
        bool Set ( long id, OperateProwerSet data );
        bool SetIsEnable ( long id, bool isEnable );
    }
}