﻿namespace Basic.HrRemoteModel.OpLog.Model
{
    public class OperateLogDto
    {
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
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

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
