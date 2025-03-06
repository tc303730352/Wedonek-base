using Base.FileRemoteModel.UpFile;
using Basic.HrLocalEvent.Model;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrLocalEvent.Emp
{
    [LocalEventName("Delete")]
    internal class ClearEmpHeadFile : IEventHandler<EmpLocalEvent>
    {
        public ClearEmpHeadFile ()
        {
        }

        public void HandleEvent (EmpLocalEvent data, string eventName)
        {
            if (data.Emp.UserHead.IsNotNull())
            {
                new DropFile
                {
                    DirKey = "EmpHead",
                    LinkBizPk = data.Emp.EmpId
                }.Send();
            }
        }
    }
}
