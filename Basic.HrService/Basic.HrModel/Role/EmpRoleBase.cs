namespace Basic.HrModel.Role
{
    public class EmpRoleBase
    {
        public long RoleId { get; set; }

        public long CompanyId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
