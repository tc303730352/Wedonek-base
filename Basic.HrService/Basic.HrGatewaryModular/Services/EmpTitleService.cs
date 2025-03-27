using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.EmpTitle;
using Basic.HrRemoteModel.EmpTitle.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class EmpTitleService : IEmpTitleService
    {
        public long AddEmpTitle ( EmpTitleAdd datum )
        {
            return new AddEmpTitle
            {
                Datum = datum,
            }.Send();
        }

        public void DeleteEmpTitle ( long id )
        {
            new DeleteEmpTitle
            {
                Id = id,
            }.Send();
        }

        public EmpTitleData GetEmpTitle ( long id )
        {
            return new GetEmpTitle
            {
                Id = id,
            }.Send();
        }

        public EmpTitleDatum[] GetEmpTitleList ( long empId, long companyId )
        {
            return new GetEmpTitleList
            {
                EmpId = empId,
                CompanyId = companyId
            }.Send();
        }

    }
}
