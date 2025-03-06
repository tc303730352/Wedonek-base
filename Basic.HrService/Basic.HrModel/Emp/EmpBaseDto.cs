using Basic.HrRemoteModel;

namespace Basic.HrModel.Emp
{
    public class EmpBaseDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public HrUserSex Sex { get; set; }


        /// <summary>
        /// 头像地址
        /// </summary>
        public string UserHead { get; set; }


    }
}
