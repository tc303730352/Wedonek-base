using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.DeptChange.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
namespace Basic.HrGatewaryModular.Api
{
    public class DeptChangeApi : ApiController
    {
        private readonly IDeptChangeService _Service;

        public DeptChangeApi ( IDeptChangeService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取变动的部门列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public ChangeDeptTree GetDept ( [NumValidate("hr.dept.id.error", 1)] long deptId, bool? isUnit )
        {
            return this._Service.GetDept(deptId, isUnit);
        }
        /// <summary>
        /// 获取解散部门影响的人员
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DisbandedDeptEmp[] GetDisbandedEmps ( DeptDisbandedArg obj )
        {
            return this._Service.GetDisbandedEmps(obj);
        }
        /// <summary>
        /// 获取合并部门影响的人员
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public MergeEmp GetMergeEmp ( DeptMergeArg arg )
        {
            return this._Service.GetMergeEmp(arg);
        }
        /// <summary>
        /// 合并部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="toDeptId"></param>
        [ApiPower("all", "hr.dept.merge")]
        public void Merge ( [NumValidate("hr.dept.id.error", 1)] long deptId, [NumValidate("hr.to.dept.id.error", 1)] long toDeptId )
        {
            this._Service.Merge(deptId, toDeptId);
        }

        /// <summary>
        /// 解散部门
        /// </summary>
        /// <param name="deptId"></param>
        [ApiPower("all", "hr.dept.merge")]
        public void ToVoid ( [NumValidate("hr.dept.id.error", 1)] long deptId )
        {
            this._Service.ToVoid(deptId);
        }
    }
}
