using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Company;
using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class CompanyService : ICompanyService
    {
        public long AddCompany ( CompanyAdd datum )
        {
            return new AddCompany
            {
                Datum = datum,
            }.Send();
        }

        public void DeleteCompany ( long id )
        {
            new DeleteCompany
            {
                Id = id,
            }.Send();
        }

        public CompanyDto GetCompany ( long id )
        {
            return new GetCompany
            {
                Id = id,
            }.Send();
        }

        public CompanyDto[] GetCompanyList ( long? parentId, bool isAllChildren )
        {
            return new GetCompanyList
            {
                ParentId = parentId,
                IsAllChildren = isAllChildren,
            }.Send();
        }

        public CompanyTree[] GetCompanyTree ()
        {
            return new GetCompanyTree().Send();
        }

        public string GetName ( long id )
        {
            return new GetCompanyName
            {
                Id = id,
            }.Send();
        }

        public bool SetCompany ( long id, CompanySet datum )
        {
            return new SetCompany
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

    }
}
