using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.LoginUser;
using Basic.HrRemoteModel;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Helper.Validate;

namespace Basic.HrCollect.Impl
{
    internal class LoginUserCollect : ILoginUserCollect
    {
        private readonly ILoginUserDAL _LoginUser;

        public LoginUserCollect (ILoginUserDAL loginUser)
        {
            this._LoginUser = loginUser;
        }
        public DBLoginUser[] GetAccounts (long[] empId)
        {
            return this._LoginUser.Gets<DBLoginUser>(a => empId.Contains(a.EmpId));
        }
        public bool DeleteAccount (DBEmpList emp)
        {
            if (!emp.IsOpenAccount)
            {
                return false;
            }
            else if (emp.Status == HrEmpStatus.禁用)
            {
                throw new ErrorException("hr.emp.already.disabled");
            }
            this._LoginUser.Clear(emp.EmpId);
            return true;
        }
        public void SetAccount (DBEmpList emp)
        {

            List<LoginUser> accounts =
            [
                new LoginUser
                {
                    LoginName = emp.EmpNo,
                    LoginType = AccountType.登陆名
                },
                new LoginUser
                {
                    LoginName = emp.Phone,
                    LoginType = AccountType.手机号
                }
            ];
            if (!emp.Email.IsNull())
            {
                accounts.Add(new LoginUser
                {
                    LoginName = emp.Email,
                    LoginType = AccountType.Email
                });
            }
            LoginUserDto[] users = this._LoginUser.Gets<LoginUserDto>(a => a.EmpId == emp.EmpId);
            long[] dropId = users.Convert(a =>
            {
                LoginUser t = accounts.Find(c => c.LoginType == a.LoginType);
                if (t == null)
                {
                    return true;
                }
                return t.LoginName != a.LoginName;
            }, a => a.Id);
            _ = accounts.RemoveAll(c => users.IsExists(a => a.LoginType == c.LoginType && a.LoginName == c.LoginName));
            this._LoginUser.Set(emp.EmpId, dropId, accounts);
        }
        public void OpenAccount (OpenAccountParam open)
        {
            List<DBLoginUser> accounts = [];
            DBEmpDeptPrower[] depts = open.EmpTitle.ConvertAll(c => new DBEmpDeptPrower
            {
                DeptId = c.DeptId,
                UnitId = c.UnitId,
                EmpId = c.EmpId,
                Id = IdentityHelper.CreateId(),
                CompanyId = open.CompanyId
            });
            open.Emps.ForEach(c =>
            {
                accounts.Add(new DBLoginUser
                {
                    EmpId = c.EmpId,
                    LoginName = c.EmpNo,
                    Id = IdentityHelper.CreateId(),
                    LoginType = AccountType.登陆名
                });
                accounts.Add(new DBLoginUser
                {
                    EmpId = c.EmpId,
                    LoginName = c.Phone,
                    Id = IdentityHelper.CreateId(),
                    LoginType = AccountType.手机号
                });
                if (!c.Email.IsNull())
                {
                    accounts.Add(new DBLoginUser
                    {
                        EmpId = c.EmpId,
                        LoginName = c.Email,
                        Id = IdentityHelper.CreateId(),
                        LoginType = AccountType.Email
                    });
                }
            });
            this._LoginUser.Add(new AddLoginUser
            {
                Emps = open.Emps,
                Account = accounts,
                DeptPrower = depts
            });
        }

        public long GetEmpId (string loginName)
        {
            AccountType accountType;
            if (loginName.Validate(ValidateFormat.手机号))
            {
                accountType = AccountType.手机号;
            }
            else if (loginName.Validate(ValidateFormat.Email))
            {
                accountType = AccountType.Email;
            }
            else if (loginName.Validate(ValidateFormat.数字字母))
            {
                accountType = AccountType.登陆名;
            }
            else
            {
                throw new ErrorException("hr.login.name.error");
            }
            long empId = this._LoginUser.GetEmpId(loginName, accountType);
            if (empId == 0)
            {
                throw new ErrorException("hr.login.user.not.find");
            }
            return empId;
        }
    }
}
