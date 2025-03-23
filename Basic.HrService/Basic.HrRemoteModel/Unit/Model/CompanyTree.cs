namespace Basic.HrRemoteModel.Unit.Model
{
    public class CompanyTree<T>
    {
        public long Id
        {
            get;
            set;
        }
        public string Name { get; set; }

        public CompanyTree<T>[] Children { get; set; }

        public T[] Dept { get; set; }
    }
}
