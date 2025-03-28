namespace Basic.HrModel.UserLoginLog
{
    public class LoginLogAdd
    {
        public string LoginName { get; set; }

        public string LoginIp { get; set; }

        public string Browser { get; set; }

        public string SysName { get; set; }


        public bool IsSuccess { get; set; }

        public string ErrorCode { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
