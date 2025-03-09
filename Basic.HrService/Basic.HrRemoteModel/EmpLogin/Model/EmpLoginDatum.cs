using Basic.HrRemoteModel.SubSystem.Model;

namespace Basic.HrRemoteModel.EmpLogin.Model
{
    public class EmpLoginDatum
    {
        public string EmpName
        {
            get;
            set;
        }
        public string UserHead { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public SubSystemItem[] SubSystem { get; set; }


        /// <summary>
        /// 菜单列表
        /// </summary>
        public Dictionary<long, PowerRoute[]> Power { get; set; }
        /// <summary>
        /// 人员公司
        /// </summary>
        public Dictionary<long, string> Company { get; set; }
        /// <summary>
        /// 当前选择的子系统
        /// </summary>
        public long CurSubSysId { get; set; }
    }
}
