using Basic.HrRemoteModel.EmpTitle.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IEmpTitleService
    {
        /// <summary>
        /// 添加人员职务
        /// </summary>
        /// <param name="datum">职务资料</param>
        /// <returns></returns>
        long AddEmpTitle ( EmpTitleAdd datum );

        /// <summary>
        /// 删除人员职务
        /// </summary>
        /// <param name="id">职务ID</param>
        void DeleteEmpTitle ( long id );

        /// <summary>
        /// 获取职务
        /// </summary>
        /// <param name="id">职务ID</param>
        /// <returns></returns>
        EmpTitleData GetEmpTitle ( long id );

        /// <summary>
        /// 获取职务列表
        /// </summary>
        /// <param name="empId">人员ID</param>
        /// <returns></returns>
        EmpTitleDatum[] GetEmpTitleList ( long empId, long? companyId );

    }
}
