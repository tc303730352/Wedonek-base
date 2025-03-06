using Basic.HrRemoteModel.Dept.Model;

namespace Basic.HrModel.Dept
{
    public class DeptUpdateArg : DeptSet
    {
        /// <summary>
        /// 独立单位ID
        /// </summary>
        public long UnitId { get; set; }
    }
}
