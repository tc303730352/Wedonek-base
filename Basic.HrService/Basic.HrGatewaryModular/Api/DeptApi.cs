using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Dept;
using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class DeptApi : ApiController
    {
        private readonly IDeptService _Service;
        public DeptApi ( IDeptService service )
        {
            this._Service = service;
        }
        public DeptTallyTree[] GetTallyTrees ( DeptGetArg param )
        {
            return this._Service.GetTallyTrees(param);
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="datum">部门资料</param>
        /// <returns></returns>
        public long Add ( [NullValidate("hr.dept.datum.null")] DeptAdd datum )
        {
            return this._Service.AddDept(datum);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        public void Delete ( [NumValidate("hr.dept.id.error", 1)] long id )
        {
            this._Service.DeleteDept(id);
        }

        /// <summary>
        /// 启用部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        public bool Enable ( [NullValidate("hr.dept.deptId.null")] long[] deptId )
        {
            return this._Service.EnableDept(deptId);
        }
        /// <summary>
        /// 查询单位部门
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DeptFullTree[] Gets ( DeptQueryParam obj )
        {
            return this._Service.Gets(obj);
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门资料</returns>
        public DeptDto Get ( [NumValidate("hr.dept.id.error", 1)] long id )
        {
            return this._Service.GetDept(id);
        }

        /// <summary>
        /// 获取部门选项
        /// </summary>
        /// <param name="getParam">查询参数</param>
        /// <returns></returns>
        public DeptSelect[] GetSelect ( [NullValidate("hr.dept.getParam.null")] DeptGetArg getParam )
        {
            return this._Service.GetDeptSelect(getParam);
        }

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        /// <param name="param">部门查询参数</param>
        /// <returns>部门树</returns>
        public DeptTree[] GetTree ( [NullValidate("hr.dept.param.null")] DeptGetArg param )
        {
            return this._Service.GetDeptTree(param);
        }

        /// <summary>
        /// 修改部门资料
        /// </summary>
        /// <param name="param">参数</param>
        public void Set ( [NullValidate("hr.dept.param.null")] UI_SetDept param )
        {
            this._Service.SetDept(param.Id, param.Dept);
        }
        /// <summary>
        /// 设置负载人
        /// </summary>
        /// <param name="leader"></param>
        public void SetLeader ( LongNullParam<long?> leader )
        {
            this._Service.SetLeader(leader.Id, leader.Value);
        }

        /// <summary>
        /// 停用部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        public bool Stop ( [NumValidate("hr.dept.deptId.error", 1)] long deptId )
        {
            return this._Service.StopDept(deptId);
        }

    }
}
