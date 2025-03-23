using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class UnitApi : ApiController
    {
        private readonly IUnitService _Service;
        public UnitApi ( IUnitService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取独立单位
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public DeptSelect[] GetDeptSelect ( [NullValidate("hr.unit.param.null")] DeptSelectGetArg param )
        {
            param.DeptId = base.UserState.PowerDeptId(param.DeptId);
            return this._Service.GetUnitDeptSelect(param);
        }
        /// <summary>
        /// 获取部门或单位名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] GetNameList ( [NullValidate("hr.dept.id.null")] long[] id )
        {
            return this._Service.GetName(id);
        }
        /// <summary>
        /// 获取独立机构树
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns>部门树</returns>
        public DeptTree[] GetDeptTree ( [NullValidate("hr.unit.param.null")] UnitGetArg param )
        {
            param.DeptId = base.UserState.PowerDeptId(param.DeptId);
            return this._Service.GetUnitDeptTree(param);
        }

        /// <summary>
        /// 获取单位选项数据
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DeptSelect[] GetSelect ( [NullValidate("hr.unit.param.null")] UnitSelectGetParam param )
        {
            param.DeptId = base.UserState.PowerDeptId(param.DeptId);
            return this._Service.GetUnitSelect(param);
        }

        /// <summary>
        /// 选项单位树形
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns>部门树</returns>
        public CompanyTree<UnitTree> GetTree ( [NullValidate("hr.unit.param.null")] UnitQueryParam param )
        {
            if ( param.IsSubCompany && param.ParentId.HasValue )
            {
                param.IsSubCompany = false;
            }
            param.DeptId = base.UserState.PowerDeptId(param.DeptId);
            return this._Service.GetUnitTree(param);
        }

    }
}
