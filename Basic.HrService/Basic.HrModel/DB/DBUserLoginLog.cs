using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("UserLoginLog")]
    public class DBUserLoginLog
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        public string LoginName { get; set; }

        public string LoginIp { get; set; }

        public string Browser { get; set; }

        public string SysName { get; set; }

        public string Address { get; set; }

        public bool IsSuccess { get; set; }

        public string FailShow { get; set; }

        public DateTime LoginTime { get; set; }
    }
}
