using Basic.HrModel.DB;
using Basic.HrRemoteModel.OperatePower.Model;

namespace Basic.HrCollect
{
    public interface IOperatePowerCollect
    {
        long Add ( OperatePowerAdd data );
        DBOperatePower Get ( long id );
        OperatePowerDto[] Gets ( long powerId );
        OperatePowerBase[] GetEnables ( long powerId );
        Result[] Gets<Result> ( long[] ids ) where Result : class;
        bool Set ( DBOperatePower source, OperatePowerSet data );
        bool SetIsEnable ( DBOperatePower source, bool isEnable );
        void Delete ( DBOperatePower source );
        void Clear ( long powerId );
    }
}