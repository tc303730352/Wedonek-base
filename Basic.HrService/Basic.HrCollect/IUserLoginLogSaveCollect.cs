using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Helper;

namespace Basic.HrCollect
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    public interface IUserLoginLogSaveCollect
    {
        void Add ( string loginName, LoginState state );
        void AddFailLog ( string loginName, ErrorException error, LoginState state );
    }
}