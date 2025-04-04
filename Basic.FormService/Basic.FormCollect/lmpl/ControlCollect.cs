using System.Linq.Expressions;
using Basic.FormDAL;
using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.FormCollect.lmpl
{
    internal class ControlCollect : IControlCollect
    {
        private readonly ICustomControlDAL _ControlDAL;

        public ControlCollect ( ICustomControlDAL controlDAL )
        {
            this._ControlDAL = controlDAL;
        }
        public Result[] GetEnables<Result> () where Result : class, new()
        {
            return this._ControlDAL.Gets<Result>(a => a.Status == ControlStatus.启用);
        }
        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._ControlDAL.Gets<Result>(ids);
        }
        public Result[] Gets<Result> ( long[] ids, Expression<Func<DBCustomControl, Result>> selector )
        {
            return this._ControlDAL.Gets<Result>(ids, selector);
        }
        public long Add ( ControlAdd data )
        {
            if ( this._ControlDAL.IsExists(a => a.Name == data.Name) )
            {
                throw new ErrorException("form.control.name.repeat");
            }
            return this._ControlDAL.Add(data);
        }
        public Result[] Query<Result> ( ControlQuery query, IBasicPage paging, out int count ) where Result : class
        {
            return this._ControlDAL.Query<Result>(query, paging, out count);
        }
        public DBCustomControl Get ( long id )
        {
            return this._ControlDAL.Get(id);
        }
        public bool Set ( DBCustomControl control, ControlSet set )
        {
            if ( set.Name != control.Name && this._ControlDAL.IsExists(a => a.Name == set.Name) )
            {
                throw new ErrorException("form.control.name.repeat");
            }
            return this._ControlDAL.Update(control, set);
        }
        public void Delete ( DBCustomControl control )
        {
            if ( control.Status != ControlStatus.起草 )
            {
                throw new ErrorException("form.control.not.allow.delete");
            }
            this._ControlDAL.Delete(control.Id);
        }

        public bool SetStatus ( DBCustomControl control, ControlStatus status )
        {
            if ( control.Status == status )
            {
                return false;
            }
            this._ControlDAL.SetStatus(control.Id, status);
            return true;
        }
    }
}
