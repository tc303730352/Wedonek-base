﻿namespace Basic.HrRemoteModel.Emp.Model
{
    public class EmpBasicDto : EmpBasicDatum
    {
        /// <summary>
        /// 入职的公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string UserHead { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public HrEmpStatus Status { get; set; }

        /// <summary>
        /// 是否开通的账号
        /// </summary>
        public bool IsOpenAccount { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }
    }
}
