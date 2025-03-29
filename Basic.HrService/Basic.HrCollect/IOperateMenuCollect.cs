using Basic.HrRemoteModel.OpMenu.Model;

namespace Basic.HrCollect
{
    public interface IOperateMenuCollect
    {
        long Add ( OpMenuAdd add );
        OperateMenu[] GetMenus ();
    }
}