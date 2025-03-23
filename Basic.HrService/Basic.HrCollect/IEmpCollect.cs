using System.Linq.Expressions;
using Basic.HrDAL.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IEmpCollect
    {
        void SetUserHead ( long id, string head );
        EmpSelectItem[] GetSelectItems ( SelectGetParam param );
        long Add ( EmpAdd add );
        void Delete ( DBEmpList emp );
        T Get<T> ( long id ) where T : class;
        Result Get<Result> ( long id, Expression<Func<DBEmpList, Result>> selector );
        string GetName ( long id );
        Dictionary<long, string> GetName ( long[] ids );
        Result[] Query<Result> ( EmpQuery query, IBasicPage paging, out int count ) where Result : class;
        string[] Set ( DBEmpList emp, EmpSetDto set, string[] title );
        Dictionary<long, EmpBase> GetBases ( long[] ids );
        T[] Gets<T> ( long[] empId ) where T : class;
        bool SetStatus ( long empId, HrEmpStatus status );
        bool CheckIsRepeat ( DataRepeatCheck check );
        void SetEmpPwd ( long empId, string newPwd );
        Result[] GetEmps<Result> ( EmpGetParam getParam ) where Result : class, new();
        Result[] GetEmps<Result> ( EmpGetParam getParam, Expression<Func<DBEmpList, Result>> selector );
        Dictionary<long, int> GetDeptEmpNum ( long[] deptId );
        bool CheckDeptIsExists ( long[] deptId );
        void SetEmpPost ( long empId, string post );
        string[] SetEmpEntry ( DBEmpList emp, EmpEntry datum );
        bool CheckIsNull ( long id );
    }
}