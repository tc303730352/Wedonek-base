using Basic.HrModel.DB;
using Basic.HrModel.SubSystem;

namespace Basic.HrDAL
{
    public interface ISubSystemDAL : IBasicDAL<DBSubSystem, long>
    {
        SubSystemDto[] GetEnables ();
    }
}