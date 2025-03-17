using Basic.HrModel.DB;
using Basic.HrModel.LoginUser;
using Basic.HrRemoteModel;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class LoginUserDAL : BasicDAL<DBLoginUser, long>, ILoginUserDAL
    {
        public LoginUserDAL ( IRepository<DBLoginUser> basicDAL ) : base(basicDAL)
        {
        }
        public void Add ( AddLoginUser add )
        {
            DateTime now = DateTime.Now;
            ISqlQueue<DBLoginUser> queue = this._BasicDAL.BeginQueue();
            queue.InsertBy(add.Emps.ConvertAll(c =>
            {
                return new DBEmpRole
                {
                    EmpId = c.EmpId,
                    RoleId = c.RoleId,
                    Id = IdentityHelper.CreateId()
                };
            }));
            if ( !add.DeptPower.IsNull() )
            {
                queue.InsertBy(add.DeptPower);
            }
            queue.Insert(add.Account);
            queue.UpdateBy<DBEmpList>(add.Emps.ConvertAll(a => new DBEmpList
            {
                EmpId = a.EmpId,
                EmpPwd = a.Pwd,
                IsOpenAccount = true
            }), "EmpPwd", "IsOpenAccount");
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.login.user.add.error");
            }

        }
        public void Set ( long empId, long[] dropId, List<LoginUser> users )
        {
            if ( dropId.IsNull() )
            {
                this._BasicDAL.Insert(users.ConvertAll(a => new DBLoginUser
                {
                    Id = IdentityHelper.CreateId(),
                    EmpId = empId,
                    LoginName = a.LoginName,
                    LoginType = a.LoginType
                }));
                return;
            }
            ISqlQueue<DBLoginUser> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => dropId.Contains(a.Id));
            queue.Insert(users.ConvertAll(a => new DBLoginUser
            {
                Id = IdentityHelper.CreateId(),
                EmpId = empId,
                LoginName = a.LoginName,
                LoginType = a.LoginType
            }));
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.login.user.add.error");
            }
        }
        public void Clear ( long empId )
        {
            ISqlQueue<DBLoginUser> queue = this._BasicDAL.BeginQueue();
            queue.Delete(a => a.EmpId == empId);
            queue.UpdateByOneColumn<DBEmpList>(a => a.IsOpenAccount == false, a => a.EmpId == empId);
            if ( queue.Submit() <= 0 )
            {
                throw new ErrorException("hr.login.user.clear.error");
            }
        }
        public long GetEmpId ( string loginName, AccountType accountType )
        {
            return this._BasicDAL.Get(a => a.LoginName == loginName && a.LoginType == accountType, a => a.EmpId);
        }
    }
}
