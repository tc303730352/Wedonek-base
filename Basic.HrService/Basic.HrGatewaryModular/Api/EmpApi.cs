using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Emp;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Client;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class EmpApi : ApiController
    {
        private readonly IEmpService _Service;
        public EmpApi ( IEmpService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 人员借调
        /// </summary>
        /// <param name="obj"></param>
        [ApiPower("all", "hr.emp.entry")]
        public void SetEmpEntry ( LongParam<EmpEntry> obj )
        {
            this._Service.SetEmpEntry(obj.Id, obj.Value);
        }
        /// <summary>
        /// 检查数据是否重复
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CheckIsRepeat ( DataRepeatCheck obj )
        {
            return this._Service.CheckIsRepeat(obj);
        }
        /// <summary>
        /// 修改人员岗位
        /// </summary>
        /// <param name="set"></param>
        [ApiPower("all", "hr.emp.set")]
        public void SetEmpPost ( LongParam<string> set )
        {
            this._Service.SetEmpPost(set.Id, set.Value);
        }
        /// <summary>
        /// 新建人员
        /// </summary>
        /// <param name="datum">人员信息</param>
        /// <returns></returns>
        [ApiPower("all", "hr.emp.add")]
        public long Add ( [NullValidate("hr.emp.datum.null")] EmpAdd datum )
        {
            return this._Service.AddEmp(datum);
        }
        /// <summary>
        /// 保存人员头像
        /// </summary>
        /// <param name="head"></param>
        [ApiPower("all", "hr.emp.set")]
        public void SaveHead ( SetEmpHead head )
        {
            this._Service.SetEmpHead(head.Id, head.HeadUrl, head.FileId);
        }
        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="id">人员ID</param>
        [ApiPower("all", "hr.emp.delete")]
        public void Delete ( [NumValidate("hr.emp.id.error", 1)] long id )
        {
            this._Service.DeleteEmp(id);
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public EmpBasic Get ( [NumValidate("hr.emp.id.error", 1)] long id )
        {
            return this._Service.GetEmp(id);
        }

        /// <summary>
        /// 获取人员详细
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns>人员详细</returns>
        public EmpData GetData ( [NumValidate("hr.emp.id.error", 1)] long id )
        {
            return this._Service.GetEmpData(id);
        }
        /// <summary>
        /// 根据人员ID和公司ID获取人员基本信息
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public EmpBasicDatum[] GetBasics ( LongParam<long[]> arg )
        {
            return this._Service.GetBasics(arg.Value, arg.Id);
        }
        /// <summary>
        /// 获取人员基本信息
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public EmpBasicDatum GetBasic ( [NumValidate("hr.emp.id.error", 1)] long empId, [NumValidate("hr.company.id.error", 1)] long companyId )
        {
            return this._Service.GetBasic(empId, companyId);
        }
        /// <summary>
        /// 获取人员选择项
        /// </summary>
        /// <param name="param">选择项资料</param>
        /// <returns>人员信息</returns>
        public EmpSelectItem[] GetSelectItem ( [NullValidate("hr.emp.param.null")] SelectGetParam param )
        {
            base.UserState.CheckDeptPower(param.DeptId);
            return this._Service.GetEmpSelectItem(param);
        }

        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public PagingResult<EmpBasicDatum> Query ( [NullValidate("hr.emp.param.null")] UI_QueryEmp param )
        {
            param.Query.DeptId = base.UserState.PowerDeptId(param.Query.DeptId);
            return this._Service.QueryEmp(param.Query, param);
        }
        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagingResult<EmpBasicDto> QueryEmp ( PagingParam<EmpQuery> param )
        {
            param.Query.DeptId = base.UserState.PowerDeptId(param.Query.DeptId);
            return this._Service.Query(param.Query, param.ToBasicPaging());
        }
        /// <summary>
        /// 修改人员资料
        /// </summary>
        /// <param name="param">参数</param>
        [ApiPower("all", "hr.emp.set")]
        public void Set ( [NullValidate("hr.emp.param.null")] UI_SetEmp param )
        {
            this._Service.SetEmp(param.Id, param.Datum);
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="set"></param>
        [ApiPower("all", "hr.emp.set")]
        public void SetStatus ( LongParam<HrEmpStatus> set )
        {
            this._Service.SetStatus(set.Id, set.Value);
        }
    }
}
