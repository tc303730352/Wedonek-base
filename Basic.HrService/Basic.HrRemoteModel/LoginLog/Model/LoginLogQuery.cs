namespace Basic.HrRemoteModel.LoginLog.Model
{
    public class LoginLogQuery
    {
        public string LoginName { get; set; }

        public string LoginIp { get; set; }

        public bool? IsSuccess { get; set; }

        public DateTime? Begin { get; set; }

        public DateTime? End { get; set; }
    }
}
