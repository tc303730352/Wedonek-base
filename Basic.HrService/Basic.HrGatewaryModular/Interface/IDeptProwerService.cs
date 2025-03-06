namespace Basic.HrGatewaryModular.Interface
{
    public interface IDeptProwerService
    {
        /// <summary>
        /// 获取员工部门数据权限
        /// </summary>
        /// <param name="empId">员工ID</param>
        /// <returns></returns>
        long[] GetDeptPrower (long empId, long companyId);

        /// <summary>
        /// 设置员工部门权限
        /// </summary>
        /// <param name="empId">员工ID</param>
        /// <param name="deptId">拥有的部门ID</param>
        void SetDeptPrower (long empId, long companyId, long[] deptId);

    }
}
