using Basic.FormDAL;
using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using System.Linq.Expressions;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.FormCollect.lmpl
{
    internal class CustomFormCollect : ICustomFormCollect
    {
        private ICustomFormDAL _FormDAL;

        public CustomFormCollect(ICustomFormDAL formDAL)
        {
            this._FormDAL = formDAL;
        }

        public Result[] Gets<Result>(long[] ids) where Result : class
        {
            return this._FormDAL.Gets<Result>(ids);
        }
        public Result[] Gets<Result>(long[] ids, Expression<Func<DBCustomForm, Result>> selector)
        {
            return this._FormDAL.Gets<Result>(ids, selector);
        }
        public long Add(FormAdd data)
        {
            if (this._FormDAL.IsExists(a => a.FormType == data.FormType && a.FormName == data.FormName))
            {
                throw new ErrorException("form.name.repeat");
            }
            return this._FormDAL.Add(data);
        }
        public Result[] Query<Result>(FormQuery query, IBasicPage paging, out int count) where Result : class
        {
            return this._FormDAL.Query<Result>(query, paging, out count);
        }
        public DBCustomForm Get(long id)
        {
            return this._FormDAL.Get(id);
        }
        public bool Set(DBCustomForm source, FormSet set)
        {
            if ((set.FormName != source.FormName || set.FormType != source.FormType) && this._FormDAL.IsExists(a => a.FormType == set.FormType && a.FormName == set.FormName))
            {
                throw new ErrorException("form.control.name.repeat");
            }
            return this._FormDAL.Update(source, set);
        }
        public void Delete(DBCustomForm source)
        {
            if (source.FormStatus != FormStatus.起草)
            {
                throw new ErrorException("form.not.allow.delete");
            }
            this._FormDAL.Delete(source.Id);
        }

        public bool SetStatus(DBCustomForm source, FormStatus status)
        {
            if (source.FormStatus == status)
            {
                return false;
            }
            this._FormDAL.SetStatus(source, status);
            return true;
        }
    }
}
