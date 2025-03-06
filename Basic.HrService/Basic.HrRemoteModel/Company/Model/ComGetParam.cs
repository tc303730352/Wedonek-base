namespace Basic.HrRemoteModel.Company.Model
{
    public class ComGetParam
    {
        public long? ParentId
        {
            get;
            set;
        }
        public bool IsAllChildren { get; set; }

        public HrCompanyStatus[] Status { get; set; }

        public HrCompanyType[] CompanyType { get; set; }
    }
}
