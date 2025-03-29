namespace Basic.HrRemoteModel.OpLog.Model
{
    public class OpLogQueryParam
    {
        public string QueryKey
        {
            get;
            set;
        }
        /// <summary>
        /// 人员ID
        /// </summary>
        public long? EmpId
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusType { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? Begin { get; set; }

        /// <summary>
        /// 开始结束
        /// </summary>
        public DateTime? End { get; set; }
    }
}
