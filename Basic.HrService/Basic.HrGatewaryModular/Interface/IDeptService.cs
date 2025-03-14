using Basic.HrRemoteModel.Dept.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IDeptService
    {
        DeptTallyTree[] GetTallyTrees ( DeptGetArg param );
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="datum">部门资料</param>
        /// <returns></returns>
        long AddDept ( DeptAdd datum );

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        void DeleteDept ( long id );

        /// <summary>
        /// 启用部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        bool EnableDept ( long[] deptId );

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门资料</returns>
        DeptDto GetDept ( long id );

        /// <summary>
        /// 获取部门选项
        /// </summary>
        /// <param name="getParam">查询参数</param>
        /// <returns></returns>
        DeptSelect[] GetDeptSelect ( DeptGetArg getParam );

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        /// <param name="param">部门查询参数</param>
        /// <returns>部门树</returns>
        DeptTree[] GetDeptTree ( DeptGetArg param );

        /// <summary>
        /// 修改部门资料
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="dept">部门资料</param>
        void SetDept ( long id, DeptSet dept );

        /// <summary>
        /// 停用部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        bool StopDept ( long deptId );

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        DeptFullTree[] Gets ( DeptQueryParam obj );
        /// <summary>
        /// 修改负载人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="leaderId"></param>
        void SetLeader ( long id, long? leaderId );
    }
}
