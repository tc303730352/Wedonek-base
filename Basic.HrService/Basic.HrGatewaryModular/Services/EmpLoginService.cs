using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.EmpLogin;
using Basic.HrRemoteModel.EmpLogin;
using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Modular;
using WeDonekRpc.Modular.Model;
using WeDonekRpc.ModularModel.Accredit.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class EmpLoginService : IEmpLoginService
    {
        private readonly IAccreditService _Accredit;

        public EmpLoginService (IAccreditService accredit)
        {
            this._Accredit = accredit;
        }
        public LoginDatum GetLoginDatum (IUserState state)
        {
            long empId = state.ToEmpId();
            EmpLoginDatum datum = new GetEmpLoginDatum
            {
                EmpId = empId
            }.Send();
            return new LoginDatum
            {
                Company = datum.Company,
                CurSubSysId = datum.CurSubSysId,
                Prower = datum.Prower,
                SubSystem = datum.SubSystem,
                Datum = new LoginUser
                {
                    CompanyId = state.GetValue<long>("CompanyId"),
                    UnitId = state.GetValue<long>("UnitId"),
                    DeptId = state.GetValue<long>("DeptId"),
                    EmpId = empId,
                    EmpName = datum.EmpName,
                    HeadUri = datum.UserHead
                }
            };
        }
        private EmpLoginRes _GetLoginResult (LoginResult result)
        {
            string applyId = "Hr_" + result.EmpId;
            UserState state = new UserState
            {
                Prower = result.Prower,
                Param = new Dictionary<string, StateParam>
                {
                    {"UserId",new StateParam(result.EmpId) },
                    {"UserType",new StateParam("emp") },
                    {"UnitId",new StateParam(result.UnitId) },
                    {"CompanyId",new StateParam(result.CompanyId)},
                    {"DeptId",new StateParam(result.DeptId) },
                    {"Title",new StateParam(result.Title) },
                    {"Post",new StateParam(result.Post) }
                }
            };
            string accreditId = this._Accredit.AddAccredit(applyId, state);
            return new EmpLoginRes
            {
                AccreditId = accreditId
            };
        }
        public EmpLoginRes EmpPwdLogin (string loginName, string password)
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
