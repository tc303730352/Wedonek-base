namespace Basic.HrRemoteModel.Dept.Model
{
    public class ComTallyTree
    {
        public long Id
        {
            get;
            set;
        }
        public string Name { get; set; }

        public ComTallyTree[] Children { get; set; }

        public DeptTallyTree[] Dept { get; set; }
    }
}
