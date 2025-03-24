using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.EmpTitle.Model;
using Basic.HrService.Interface;
using LinqKit;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class EmpTitleService : IEmpTitleService
    {
        private readonly IEmpTitleCollect _Service;
        private readonly IDeptCollect _Dept;
        private readonly ITitleDicItemCollect _TitleDic;
        private readonly ICompanyCollect _Company;
        public EmpTitleService ( IEmpTitleCollect service,
            IDeptCollect dept,
            ICompanyCollect company,
            ITitleDicItemCollect titleDic )
        {
            this._Company = company;
            this._TitleDic = titleDic;
            this._Dept = dept;
            this._Service = service;
        }

        public long Add ( EmpTitleAdd add )
        {
            add.UnitId = this._Dept.GetUnitId(add.DeptId);
            return this._Service.Add(add);
        }

        public void Delete ( long id )
        {
            DBEmpTitle title = this._Service.Get(id);
            this._Service.Delete(title);
        }

        public EmpTitleData Get ( long id )
        {
            DBEmpTitle title = this._Service.Get(id);
            EmpTitleData dto = title.ConvertMap<DBEmpTitle, EmpTitleData>();
            dto.Title = this._TitleDic.GetTitleName(title.TitleCode);
            return dto;
        }
        public EmpTitleDatum[] Gets ( long empId, long? companyId )
        {
            DBEmpTitle[] titles = this._Service.GetEmpTitle<DBEmpTitle>(empId, companyId);
            if ( titles.IsNull() )
            {
                return null;
            }
            EmpTitleDatum[] dtos = titles.ConvertMap<DBEmpTitle, EmpTitleDatum>();
            long[] deptId = dtos.ConvertAll(c => c.DeptId);
            deptId = deptId.Add(dtos.ConvertAll(c => c.UnitId));
            Dictionary<long, string> comNames = this._Company.GetNames(dtos.Distinct(c => c.CompanyId));
            Dictionary<long, string> deptName = this._Dept.GetDeptName(deptId);
            Dictionary<string, string> titleName = this._TitleDic.GetTitleNames(dtos.ConvertAll(c => c.TitleCode));
            dtos.ForEach(c =>
            {
                c.CompanyName = comNames.GetValueOrDefault(c.CompanyId);
                c.DeptName = deptName.GetValueOrDefault(c.DeptId);
                c.UnitName = deptName.GetValueOrDefault(c.UnitId);
                c.Title = titleName.GetValueOrDefault(c.TitleCode);
            });
            return dtos;
        }

    }
}
