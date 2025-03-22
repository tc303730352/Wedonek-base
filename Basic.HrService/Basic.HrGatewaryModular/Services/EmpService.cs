using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp;
using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;
using WeDonekRpc.Modular;

namespace Basic.HrGatewaryModular.Services
{
    internal class EmpService : IEmpService
    {
        public EmpService ( IUserState state )
        {
        }
        public long AddEmp ( EmpAdd datum )
        {
            return new AddEmp
            {
                Datum = datum,
            }.Send();
        }
        public EmpBasicDatum[] GetBasics ( long[] empId, long companyId )
        {
            return new GetEmpBasics
            {
                EmpId = empId,
                CompanyId = companyId
            }.Send();
        }
        public bool CheckIsRepeat ( DataRepeatCheck obj )
        {
            return new CheckEmpRepeat
            {
                Check = obj
            }.Send();
        }
        public void DeleteEmp ( long id )
        {
            new DeleteEmp
            {
                Id = id,
            }.Send();
        }

        public EmpBasic GetEmp ( long id )
        {
            return new GetEmp
            {
                Id = id,
            }.Send();
        }

        public EmpData GetEmpData ( long id )
        {
            return new GetEmpData
            {
                Id = id,
            }.Send();
        }

        public EmpSelectItem[] GetEmpSelectItem ( SelectGetParam param )
        {
            return new GetEmpSelectItem
            {
                Param = param,
            }.Send();
        }

        public PagingResult<EmpBasicDatum> QueryEmp ( EmpQuery query, IBasicPage paging )
        {
            if ( query.DeptId != null && query.DeptId.Length == 0 )
            {
                return new PagingResult<EmpBasicDatum>();
            }
            EmpBasicDatum[] list = new QueryEmp
            {
                Query = query,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out int count);
            return new PagingResult<EmpBasicDatum>(list, count);
        }

        public void SetEmp ( long id, EmpSet datum )
        {
            new SetEmp
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public EmpBasicDatum GetBasic ( long empId, long companyId )
        {
            return new GetEmpBasic
            {
                Id = empId,
                CompanyId = companyId
            }.Send();
        }
        public void SetStatus ( long id, HrEmpStatus status )
        {
            new SetEmpStatus
            {
                Status = status,
                Id = id,
            }.Send();
        }
        public PagingResult<EmpBasicDto> Query ( EmpQuery query, IBasicPage paging )
        {
            if ( query.DeptId != null && query.DeptId.Length == 0 )
            {
                return new PagingResult<EmpBasicDto>();
            }
            return new QueryEmpList
            {
                Query = query,
                IsDesc = paging.IsDesc,
                Size = paging.Size,
                SortName = paging.SortName,
                Index = paging.Index,
                NextId = paging.NextId
            }.Send();
        }

        public void SetEmpHead ( long id, string headUrl, long fileId )
        {
            new SaveEmpHead
            {
                EmpId = id,
                HeadUri = headUrl,
                FileId = fileId
            }.Send();
        }

        public void SetEmpPost ( long id, string post )
        {
            new SetEmpPost
            {
                EmpId = id,
                Post = post
            }.Send();
        }

        public void SetEmpEntry ( long id, EmpEntry value )
        {
            new SetEmpEntry
            {
                Id = id,
                Datum = value
            }.Send();
        }
    }
}
