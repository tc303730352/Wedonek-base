namespace Basic.HrCollect.Model
{
    internal class DeptTemp
    {
        public long Id { get; set; }
        public string DeptName { get; set; }

        public string ShortName { get; set; }

        public string LevelCode { get; set; }

        public long[] Pid { get; set; }
    }
}
