namespace Basic.HrService.Interface
{
    public interface IAccountService
    {
        void ResetPwd (long empId);
        void UpdatePwd (long empId, string newPwd, string oldPwd);
    }
}