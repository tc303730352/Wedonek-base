namespace Basic.HrGatewaryModular.Interface
{
    public interface IEmpRoleService
    {
        /// <summary>
        /// 获取人员角色列表
        /// </summary>
        /// <param name="empId">人员ID</param>
        /// <returns></returns>
        long[] GetEmpRole ( long empId, long companyId );

        /// <summary>
        /// 修改人员角色
        /// </summary>
        /// <param name="empId">人员ID</param>
        /// <param name="roleId">人员角色列表</param>
        void SetEmpRole ( long empId, long companyId, long[] roleId );

    }
}
