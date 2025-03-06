namespace Basic.HrCollect
{
    public interface IHrConfig
    {
        long TitleDicId { get; }

        long PostDicId { get; }

        string PwdSign { get; }

        string EncryptionPwd (string pwd, long empId);
        string EncryptionFullPwd (string pwd, long empId);
        string InitPwd { get; }
    }
}