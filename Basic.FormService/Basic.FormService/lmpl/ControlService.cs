using Basic.FormCollect;
using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormService.lmpl
{
    internal class ControlService : IControlService
    {
        private readonly IControlCollect _Control;

        public ControlService ( IControlCollect control )
        {
            this._Control = control;
        }
        public bool SetStatus ( long id, ControlStatus status )
        {
            DBCustomControl db = this._Control.Get(id);
            return this._Control.SetStatus(db, status);
        }
        public long Add ( ControlAdd data )
        {
            return this._Control.Add(data);
        }

        public void Delete ( long id )
        {
            DBCustomControl db = this._Control.Get(id);
            this._Control.Delete(db);
        }

        public ControlDetailed Get ( long id )
        {
            DBCustomControl db = this._Control.Get(id);
            return db.ConvertMap<DBCustomControl, ControlDetailed>();
        }
        public PagingResult<ControlDto> Query ( ControlQuery query, IBasicPage paging )
        {
            ControlDto[] list = this._Control.Query<ControlDto>(query, paging, out int count);
            return new PagingResult<ControlDto>(count, list);
        }

        public bool Set ( long id, ControlSet set )
        {
            DBCustomControl db = this._Control.Get(id);
            return this._Control.Set(db, set);
        }

        public ControlItem[] GetItems ()
        {
            return this._Control.GetEnables<ControlItem>();
        }
    }
}
