using Base.HrExtendService;
using Basic.HrCollect;
using Basic.HrDAL.Model;
using Basic.HrLocalEvent.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Emp;
using Basic.HrModel.EmpTitle;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;

namespace Basic.HrService.lmpl
{
    internal class EmpService : IEmpService
    {
        private readonly IEmpCollect _Service;
        private readonly IEmpTitleCollect _Title;
        private readonly IPostDicItemCollect _Post;
        private readonly ICompanyCollect _Company;
        private readonly IDeptCollect _Dept;
        private readonly ITitleDicItemCollect _Titles;
        private readonly IFileService _FileService;
        public EmpService ( IEmpCollect service,
            IEmpTitleCollect title,
            IPostDicItemCollect post,
            ITitleDicItemCollect titleDic,
            ICompanyCollect company,
            IFileService file,
            IDeptCollect dept )
        {
            this._FileService = file;
            this._Titles = titleDic;
            this._Service = service;
            this._Title = title;
            this._Post = post;
            this._Company = company;
            this._Dept = dept;
        }
        public void SetEmpHead ( long empId, string head, long fileId )
        {
            var emp = this._Service.Get(empId, a => new
            {
                a.EmpId,
                a.UserHead
            });
            this._Service.SetUserHead(empId, head);
            this._FileService.Save(fileId, empId, emp.UserHead);
        }
        public void SetEmpPost ( long empId, string post )
        {
            this._Service.SetEmpPost(empId, post);
        }
        public EmpSelectItem[] GetSelectItems ( SelectGetParam param )
        {
            return this._Service.GetSelectItems(param);
        }

        public EmpBasicDatum[] GetBasics ( long[] empId, long companyId )
        {
            EmpDto[] emps = this._Service.Gets<EmpDto>(empId);
            if ( emps.IsNull() )
            {
                return Array.Empty<EmpBasicDatum>();
            }
            return this._Format(emps, companyId);
        }
        private EmpBasicDatum[] _Format ( EmpDto[] emps, long companyId )
        {
            EmpBasicDatum[] list = emps.ConvertMap<EmpDto, EmpBasicDatum>();
            List<long> deptId = new List<long>(list.Length * 2);
            List<string> post = new List<string>(list.Length);
            long[] empId = new long[list.Length];
            emps.ForEach(( c, i ) =>
            {
                deptId.Add(c.DeptId);
                deptId.Add(c.UnitId);
                if ( c.PostCode.IsNotNull() )
                {
                    post.Add(c.PostCode);
                }
                empId[i] = c.EmpId;
            });
            EmpTitleDto[] titles = this._Title.GetEmpTitle(empId, companyId);
            Dictionary<string, string> titleName = null;
            long[] ids;
            if ( !titles.IsNull() )
            {
                titleName = this._Titles.GetTitleNames(titles.Distinct(a => a.TitleCode));
                titles.ForEach(c =>
                {
                    deptId.Add(c.DeptId);
                    deptId.Add(c.UnitId);
                });
                ids = deptId.Distinct().ToArray();
            }
            else
            {
                ids = deptId.Distinct().ToArray();
            }
            Dictionary<long, string> depts = this._Dept.GetDeptName(ids);
            Dictionary<string, string> posts = null;
            if ( post.Count > 0 )
            {
                posts = this._Post.GetPostName(post.Distinct().ToArray());
            }
            list.ForEach(c =>
            {
                if ( !c.PostCode.IsNull() )
                {
                    c.Post = posts.GetValueOrDefault(c.PostCode);
                }
                if ( titles != null )
                {
                    EmpTitleDto[] title = titles.FindAll(a => a.EmpId == c.EmpId && a.DeptId == c.DeptId);
                    if ( !title.IsNull() )
                    {
                        c.DeptTitleId = title.ConvertAll(a => a.TitleCode);
                        c.DeptTitle = title.Join(",", a => titleName.GetValueOrDefault(a.TitleCode));
                    }
                    c.Title = titles.Convert(a => a.EmpId == a.EmpId && a.DeptId != a.DeptId, a => new EmpTitle
                    {
                        DeptId = a.DeptId,
                        TitleCode = a.TitleCode,
                        UnitId = a.UnitId,
                        Show = string.Format("{0}({1}) {2}", depts.GetValueOrDefault(a.DeptId), depts.GetValueOrDefault(a.UnitId), titleName.GetValueOrDefault(a.TitleCode))
                    });
                }
                c.Dept = depts.GetValueOrDefault(c.DeptId) + "(" + depts.GetValueOrDefault(c.UnitId) + ")";
            });
            return list;
        }
        public PagingResult<EmpBasicDto> QueryEmp ( EmpQuery query, IBasicPage paging )
        {
            EmpDatumDto[] emps = this._Service.Query<EmpDatumDto>(query, paging, out int count);
            if ( emps.IsNull() )
            {
                return new PagingResult<EmpBasicDto>();
            }
            EmpBasicDto[] dtos = this._Format(emps, query.CompanyId);
            return new PagingResult<EmpBasicDto>(dtos, count);
        }
        private EmpBasicDto[] _Format ( EmpDatumDto[] emps, long companyId )
        {
            EmpBasicDto[] list = emps.ConvertMap<EmpDatumDto, EmpBasicDto>();
            List<long> deptId = new List<long>(list.Length * 2);
            List<string> post = new List<string>(list.Length);
            long[] empId = new long[list.Length];
            emps.ForEach(( c, i ) =>
            {
                deptId.Add(c.DeptId);
                deptId.Add(c.UnitId);
                if ( c.PostCode.IsNotNull() )
                {
                    post.Add(c.PostCode);
                }
                empId[i] = c.EmpId;
            });
            EmpTitleDto[] titles = this._Title.GetEmpTitle(empId, companyId);
            Dictionary<string, string> titleName = null;
            long[] ids;
            if ( titles.Length > 0 )
            {
                titleName = this._Titles.GetTitleNames(titles.Distinct(a => a.TitleCode));
                titles.ForEach(c =>
                {
                    deptId.Add(c.DeptId);
                    deptId.Add(c.UnitId);
                });
                ids = deptId.Distinct().ToArray();
            }
            else
            {
                ids = deptId.Distinct().ToArray();
            }
            Dictionary<long, string> depts = this._Dept.GetDeptName(ids);
            Dictionary<string, string> posts = null;
            if ( post.Count > 0 )
            {
                posts = this._Post.GetPostName(post.Distinct().ToArray());
            }
            list.ForEach(c =>
            {
                if ( !c.PostCode.IsNull() )
                {
                    c.Post = posts.GetValueOrDefault(c.PostCode);
                }
                if ( titles != null )
                {
                    EmpTitleDto[] title = titles.FindAll(a => a.EmpId == c.EmpId && a.DeptId == c.DeptId);
                    if ( !title.IsNull() )
                    {
                        c.DeptTitleId = title.ConvertAll(a => a.TitleCode);
                        c.DeptTitle = title.Join(",", a => titleName.GetValueOrDefault(a.TitleCode));
                    }
                    c.Title = titles.Convert(a => a.EmpId == a.EmpId && a.DeptId != a.DeptId, a => new EmpTitle
                    {
                        DeptId = a.DeptId,
                        TitleCode = a.TitleCode,
                        UnitId = a.UnitId,
                        Show = string.Format("{1} {0} {2}", depts.GetValueOrDefault(a.DeptId), depts.GetValueOrDefault(a.UnitId), titleName.GetValueOrDefault(a.TitleCode))
                    });
                }
                c.Dept = depts.GetValueOrDefault(c.UnitId) + " " + depts.GetValueOrDefault(c.DeptId);
            });
            return list;
        }
        public PagingResult<EmpBasicDatum> Query ( EmpQuery query, IBasicPage paging )
        {
            EmpDto[] emps = this._Service.Query<EmpDto>(query, paging, out int count);
            if ( emps.IsNull() )
            {
                return new PagingResult<EmpBasicDatum>(count);
            }
            EmpBasicDatum[] list = this._Format(emps, query.CompanyId);
            return new PagingResult<EmpBasicDatum>(list, count);
        }
        public void Delete ( long id )
        {
            DBEmpList emp = this._Service.Get<DBEmpList>(id);
            this._Service.Delete(emp);
            new EmpLocalEvent(emp).Send("Delete");
        }
        public long Add ( EmpAdd add )
        {
            add.UnitId = this._Dept.GetUnitId(add.DeptId);
            long empId = this._Service.Add(add);
            if ( add.FileId.HasValue )
            {
                this._FileService.Save(add.FileId.Value, empId);
            }
            return empId;
        }
        public bool CheckIsRepeat ( DataRepeatCheck check )
        {
            return this._Service.CheckIsRepeat(check);
        }
        public void Set ( long id, EmpSet set )
        {
            DBEmpList emp = this._Service.Get<DBEmpList>(id);
            EmpSetDto dto = set.ConvertMap<EmpSet, EmpSetDto>();
            if ( emp.DeptId != dto.DeptId )
            {
                dto.UnitId = this._Dept.GetUnitId(set.DeptId);
            }
            else
            {
                dto.UnitId = emp.UnitId;
                string[] codes = this._Title.GetTitle(id, emp.DeptId);
                if ( codes.IsEquals(set.Title) )
                {
                    set.Title = null;
                }
            }
            string head = emp.UserHead;
            string[] cols = this._Service.Set(emp, dto, set.Title);
            if ( !cols.IsNull() )
            {
                new EmpLocalEvent(emp, cols).AsyncSend("Update");
                if ( cols.Contains("UserHead") )
                {
                    this._FileService.Sync(set.FileId, id, head);
                }
            }

        }

        public EmpBasic GetBasic ( long id )
        {
            DBEmpList emp = this._Service.Get<DBEmpList>(id);
            EmpBasic basic = emp.ConvertMap<DBEmpList, EmpBasic>();
            string[] title = this._Title.GetTitle(id, emp.DeptId);
            if ( !title.IsNull() )
            {
                basic.TitleCode = title;
                basic.Title = string.Join(',', this._Titles.GetTitleNames(title));
            }
            return basic;
        }
        public EmpData Get ( long id )
        {
            DBEmpList emp = this._Service.Get<DBEmpList>(id);
            EmpData data = emp.ConvertMap<DBEmpList, EmpData>();
            if ( emp.PostCode.IsNotNull() )
            {
                data.Post = this._Post.GetPostName(emp.PostCode);
            }
            data.Dept = this._Dept.GetDeptName(emp.UnitId) + " " + this._Dept.GetDeptName(emp.DeptId);
            data.Company = this._Company.GetName(data.CompanyId);
            string[] title = this._Title.GetTitle(id, emp.DeptId);
            if ( title != null )
            {
                data.TitleCode = title;
                data.Title = string.Join(',', this._Titles.GetTitleNames(title));
            }
            return data;
        }
        public string GetEmpName ( long id )
        {
            return this._Service.GetName(id);
        }
        public EmpBasicDatum GetEmpBasic ( long id, long companyId )
        {
            EmpDto emp = this._Service.Get<EmpDto>(id);
            EmpTitleDto[] titles = this._Title.GetEmpTitle<EmpTitleDto>(emp.EmpId, companyId);
            EmpBasicDatum datum = new EmpBasicDatum
            {
                Dept = this._Dept.GetDeptName(emp.DeptId),
                DeptId = emp.DeptId,
                EmpId = emp.EmpId,
                EmpName = emp.EmpName,
                EmpNo = emp.EmpNo
            };
            if ( !datum.PostCode.IsNull() )
            {
                datum.Post = this._Post.GetPostName(emp.PostCode);
            }
            if ( !titles.IsNull() )
            {
                Dictionary<string, string> titleName = this._Titles.GetTitleNames(titles.ConvertAll(a => a.TitleCode));
                Dictionary<long, string> deptName = this._Dept.GetDeptName(titles.ConvertAll(a => a.DeptId));
                EmpTitleDto[] title = titles.FindAll(a => a.EmpId == id && a.DeptId == emp.DeptId);
                if ( !title.IsNull() )
                {
                    datum.DeptTitleId = title.ConvertAll(a => a.TitleCode);
                    datum.DeptTitle = title.Join(",", a => titleName.GetValueOrDefault(a.TitleCode));
                }
                datum.Title = titles.Convert(a => a.EmpId == emp.EmpId && a.DeptId != emp.DeptId, a => new EmpTitle
                {
                    DeptId = a.DeptId,
                    TitleCode = a.TitleCode,
                    Show = deptName.GetValueOrDefault(a.DeptId) + " " + titleName.GetValueOrDefault(a.TitleCode)
                });
            }
            return datum;
        }

        public void SetEmpStatus ( long empId, HrEmpStatus status )
        {
            if ( this._Service.SetStatus(empId, status) && status == HrEmpStatus.禁用 )
            {
                new UserChangeEvent(empId).AsyncSend("EmpDisable");
            }
        }

        public void SetEmpEntry ( long id, EmpEntry datum )
        {
            DBEmpList emp = this._Service.Get<DBEmpList>(id);
            EmpEntrySet set = new EmpEntrySet
            {
                CompanyId = datum.CompanyId,
                DeptId = datum.DeptId,
                IsRetainTitle = datum.IsRetainTitle,
                PostCode = datum.PostCode,
                Title = datum.Title
            };
            set.UnitId = this._Dept.GetUnitId(datum.DeptId);
            var titles = this._Title.GetEmpDeptTitle(id, datum.DeptId, a => new
            {
                a.Id,
                a.TitleCode
            });
            if ( !titles.IsNull() )
            {
                set.DropTitleId = titles.Convert(c => !datum.Title.Contains(c.TitleCode), c => c.Id);
            }
            long companyId = emp.CompanyId;
            this._Service.SetEmpEntry(emp, set);
            new EmpEntryEvent(emp, companyId).AsyncSend();
        }

        public Dictionary<long, string> GetEmpName ( long[] ids )
        {
            return this._Service.GetName(ids);
        }
    }
}
