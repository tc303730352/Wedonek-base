using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("UserOperateLog")]
    public class DBUserOperateLog
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 模块标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 人员ID
        /// </summary>
        public long EmpId { get; set; }

        /// <summary>
        /// 人员名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusType { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 实际请求路径
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 注册参数
        /// </summary>
        public string ReqParam { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 失败说明
        /// </summary>
        public string FailShow { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public int Duration { get; set; }

        public DateTime AddTime { get; set; }
    }
}
