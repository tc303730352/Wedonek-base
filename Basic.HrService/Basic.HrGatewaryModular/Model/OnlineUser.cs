namespace Basic.HrGatewaryModular.Model
{
    public class OnlineUser
    {
        public string AccreditId { get; set; }

        public string UserName { get; set; }

        public string DeptName { get; set; }

        public string LoginIp
        {
            get;
            set;
        }

        public string Browser
        {
            get;
            set;
        }
        public string SysName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime
        {
            get;
            set;
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expire { get; set; }
    }
}
