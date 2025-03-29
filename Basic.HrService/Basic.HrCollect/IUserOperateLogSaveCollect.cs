using Basic.HrRemoteModel.OpLog.Model;
using WeDonekRpc.Client.Attr;

namespace Basic.HrCollect
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    public interface IUserOperateLogSaveCollect
    {
        void Add ( OperateLogAdd[] adds );
    }
}