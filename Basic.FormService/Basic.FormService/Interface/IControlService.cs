﻿using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormService.Interface
{
    public interface IControlService
    {
        long Add ( ControlAdd data );
        void Delete ( long id );
        ControlDetailed Get ( long id );
        PagingResult<ControlDto> Query ( ControlQuery query, IBasicPage paging );
        bool Set ( long id, ControlSet set );
    }
}