using Basic.HrModel.DB;

namespace Basic.HrDAL
{
    public interface IEmpTitleDAL : IBasicDAL<DBEmpTitle, long>
    {
        void Add ( DBEmpTitle add );
        void Clear ( long empId );

        Dictionary<long, string[]> GetEmpTitle ( KeyValuePair<long, long>[] empAndDept );
    }
}