using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.FormDAL.Repository
{
    internal class CustomFormDAL : BasicDAL<DBCustomForm, long>, ICustomFormDAL
    {
        public CustomFormDAL(IRepository<DBCustomForm> basicDAL) : base(basicDAL)
        {
        }
        public long Add(FormAdd form)
        {
            DBCustomForm db = form.ConvertMap<FormAdd, DBCustomForm>();
            db.Id = IdentityHelper.CreateId();
            db.CreateTime = DateTime.Now;
            db.FormStatus = FormStatus.起草;
            base._BasicDAL.Insert(db);
            return db.Id;
        }
        public Result[] Query<Result>(FormQuery query, IBasicPage paging, out int count) where Result : class
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(), paging, out count);
        }
        public void SetStatus(DBCustomForm form, FormStatus status)
        {
            if (!_BasicDAL.Update(a => a.FormStatus == status, a => a.Id == form.Id))
            {
                throw new ErrorException("form.status.set.fail");
            }
        }
    }
}
