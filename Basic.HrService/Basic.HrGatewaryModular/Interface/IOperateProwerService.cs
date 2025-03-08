using Basic.HrRemoteModel.OperatePrower.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperateProwerService
    {
        OperateProwerBase[] GetEnables ( long prowerId );
        long Add ( OperateProwerAdd data );
        bool Set ( long id, OperateProwerSet data );
        bool SetIsEnable ( long id, bool isEnable );
    }
}