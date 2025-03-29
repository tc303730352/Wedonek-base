namespace Basic.HrRemoteModel.OpLog.Model
{
    public class OperateLogAdd
    {
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
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusType { get; set; }

        /// <summary>
        /// 本地路径
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Referer路径
        /// </summary>
        public Uri Referer { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// 最后错误信息
        /// </summary>
        public string LastError { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        public string PostStr { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Begin { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Duration { get; set; }
    }
}
