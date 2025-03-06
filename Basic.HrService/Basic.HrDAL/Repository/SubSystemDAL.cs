using Basic.HrModel.DB;
using Basic.HrModel.SubSystem;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class SubSystemDAL : BasicDAL<DBSubSystem, long>, ISubSystemDAL
    {
        public SubSystemDAL (IRepository<DBSubSystem> basicDAL) : base(basicDAL)
        {
        }

        public SubSystemDto[] GetEnables ()
        {
            return this._BasicDAL.Gets<SubSystemDto>(a => a.IsEnable);
        }
    }
}
