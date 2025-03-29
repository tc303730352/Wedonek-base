using Basic.HrRemoteModel.OpMenu.Model;

namespace Basic.HrService.Interface
{
    public interface IOperateMenuService
    {
        long Add ( OpMenuAdd add );
        OperateMenu[] GetMenus ();
    }
}