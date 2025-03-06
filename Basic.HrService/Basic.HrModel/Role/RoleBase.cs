namespace Basic.HrModel.Role
{
    public class RoleBase
    {
        public long Id
        {
            get;
            set;
        }
        public string RoleName { get; set; }
        public long CompanyId { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsEnable { get; set; }
    }
}
