using Basic.FormGatewaryModular.Interface;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormGatewaryModular.Services
{
    internal class ControlService : IControlService
    {
        public long Add ( ControlAdd data )
        {
            return new AddControl
            {
                Datum = data
            }.Send();
        }
        public ControlItem[] GetItems ()
        {
            return new GetControlItems().Send();
        }
        public void Delete ( long id )
        {
            new DeleteControl
            {
                Id = id
            }.Send();
        }

        public bool Disable ( long id )
        {
            return new SetControlStatus
            {
                Id = id,
                Status = ControlStatus.停用
            }.Send();
        }

        public bool Enable ( long id )
        {
            return new SetControlStatus
            {
                Id = id,
                Status = ControlStatus.启用
            }.Send(); throw new NotImplementedException();
        }

        public ControlDetailed Get ( long id )
        {
            return new GetControl
            {
                Id = id
            }.Send();
        }
        public PagingResult<ControlDto> Query ( ControlQuery query, IBasicPage paging )
        {
            return new QueryControl
            {
                Index = paging.Index,
                SortName = paging.SortName,
                Size = paging.Size,
                IsDesc = paging.IsDesc,
                NextId = paging.NextId,
                Query = query
            }.Send();
        }

        public bool Set ( long id, ControlSet set )
        {
            return new SetControl
            {
                Datum = set,
                Id = id
            }.Send();
        }
    }
}
