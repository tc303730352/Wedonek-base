using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.EmpLogin;
using Basic.HrRemoteModel.EmpLogin;
using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Modular;
using WeDonekRpc.Modular.Model;
using WeDonekRpc.ModularModel.Accredit.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class EmpLoginService : IEmpLoginService
    {
        private readonly IAccreditService _Accredit;

        public EmpLoginService ( IAccreditService accredit )
        {
            this._Accredit = accredit;
        }
        public LoginDatum GetLoginDatum ( IUserState state )
        {
            long empId = state.ToEmpId();
            EmpLoginDatum datum = new GetEmpLoginDatum
            {
                EmpId = empId,
                CompanyId = state.GetValue<long>("CompanyId"),
                Company = state.GetValue<long[]>("Company")
            }.Send();
            return new LoginDatum
            {
                CompanyId = state.ToCompanyId(),
                Company = datum.Company,
                CurSubSysId = datum.CurSubSysId,
                Power = datum.Power,
                SubSystem = datum.SubSystem,
                Datum = new LoginUser
                {
                    EmpId = empId,
                    EmpName = datum.Name,
                    HeadUri = datum.Head
                }
            };
        }
        public LoginDatum Switch ( IUserState state, long companyId )
        {
            long[] comId = state.GetValue<long[]>("Company");
            if ( comId.Contains(companyId) )
            {
                throw new ErrorException("hr.user.no.power");
            }
            long curId = state.GetValue<long>("CompanyId");
            if ( curId == companyId )
            {
                return this.GetLoginDatum(state);
            }
            ComSwitchResult res = new EmpSwitchCompany
            {
                CompanyId = companyId,
                EmpId = state.ToEmpId()
            }.Send();
            state.SetPower(res.Power);
            state["IsAdmin"] = res.IsAdmin;
            if ( !state.SaveState() )
            {
                throw new ErrorException("hr.user.state.save.fail");
            }
            return this.GetLoginDatum(state);
        }
        private EmpLoginRes _GetLoginResult ( LoginResult result )
        {
            string applyId = "Hr_" + result.EmpId;
            UserState state = new UserState
            {
                Power = result.Power,
                Param = new Dictionary<string, StateParam>
                {
                    {"UserId",new StateParam(result.EmpId) },
                    {"UserType",new StateParam("emp") },
                    {"CompanyId",new StateParam(result.CompanyId)},
                    {"IsAdmin",new StateParam(result.IsAdmin)},
                    {"Company",new StateParam(result.Company) }
                }
            };
            string accreditId = this._Accredit.AddAccredit(applyId, state);
            return new EmpLoginRes
            {
                AccreditId = accreditId
            };
        }
        public EmpLoginRes EmpPwdLogin ( string loginName, string password )
        {
            LoginResult result = new EmpPwdLogin
            {
                LoginName = loginName,
                Password = password,
            }.Send();
            return this._GetLoginResult(result);
        }
    }
}
