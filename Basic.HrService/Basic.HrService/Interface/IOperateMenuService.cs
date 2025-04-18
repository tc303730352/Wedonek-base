﻿using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IOperateMenuService
    {
        long Add ( OpMenuAdd add );
        void Delete ( long id );
        OperateMenuDto Get ( long id );
        OperateMenu[] GetMenus ();
        PagingResult<OperateMenuDto> Query ( OpMenuQuery query, IBasicPage basicPage );
        bool SetIsEnable ( long id, bool isEnable );
    }
}