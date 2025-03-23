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

        public T[] Children { get; set; }
    }
}
