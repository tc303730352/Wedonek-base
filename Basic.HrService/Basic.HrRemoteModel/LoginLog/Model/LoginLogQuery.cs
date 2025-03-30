namespace Basic.HrRemoteModel.LoginLog.Model
{
    public class LoginLogQuery
    {
        public string QueryKey { get; set; }

        public bool? IsSuccess { get; set; }

        public DateTime? Begin { get; set; }

        public DateTime? End { get; set; }
    }
}
