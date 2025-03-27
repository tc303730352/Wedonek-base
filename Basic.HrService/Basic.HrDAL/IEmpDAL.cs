using System.Linq.Expressions;
using Basic.HrDAL.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IEmpDAL : IBasicDAL<DBEmpList, long>
    {
        Result[] GetEmps<Result> ( EmpGetParam getParam, Expression<Func<DBEmpList, Result>> selector );
        Result[] GetEmps<Result> ( EmpGetParam getParam ) where Result : class, new();
        EmpSelectItem[] GetSelectItems ( SelectGetParam param );
        Result[] Query<Result> ( EmpQuery query, IBasicPage paging, out int count ) where Result : class;
        void CheckEmpNo ( string empNo );
        void CheckPhone ( string phone );
        void CheckIDCard ( string idCard );
        void CheckEmail ( string email );
        void Add ( DBEmpList add, string[] title );
        string[] Set ( DBEmpList soure, EmpSetDto set, string[] title );
        void SetStatus ( long empId, HrEmpStatus status );
        void SetUserHead ( long id, string head );
        void SetPwd ( long empId, string newPwd );
        Dictionary<long, int> GetDeptEmpNum ( long[] deptId );
        void SetEmpPost ( long empId, string post );
        string[] SetEmpEntry ( DBEmpList emp, EmpEntrySet datum );
    }
}