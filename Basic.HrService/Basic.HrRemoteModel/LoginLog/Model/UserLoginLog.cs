namespace Basic.HrRemoteModel.LoginLog.Model
{
    public class UserLoginLog
    {
        public long Id { get; set; }

        public long? EmpId { get; set; }

        public string LoginName { get; set; }

        public AccountType LoginType { get; set; }

        public string LoginIp { get; set; }

        public string Browser { get; set; }

        public string SysName { get; set; }

        public bool IsSuccess { get; set; }

        public string FailShow { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
