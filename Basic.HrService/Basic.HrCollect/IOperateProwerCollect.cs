using Basic.HrModel.DB;
using Basic.HrRemoteModel.OperatePrower.Model;

namespace Basic.HrCollect
{
    public interface IOperateProwerCollect
    {
        long Add ( OperateProwerAdd data );
        DBOperatePrower Get ( long id );
        OperateProwerDto[] Gets ( long prowerId );
        OperateProwerBase[] GetEnables ( long prowerId );
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        bool Set ( DBOperatePrower source, OperateProwerSet data );
        bool SetIsEnable ( DBOperatePrower source, bool isEnable );
    }
}