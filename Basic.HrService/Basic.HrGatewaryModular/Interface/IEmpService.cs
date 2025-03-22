using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IEmpService
    {
        EmpBasicDatum[] GetBasics ( long[] empId, long companyId );
        /// <summary>
        /// 新建人员
        /// </summary>
        /// <param name="datum">人员信息</param>
        /// <returns></returns>
        long AddEmp ( EmpAdd datum );
        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool CheckIsRepeat ( DataRepeatCheck obj );
        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="id">人员ID</param>
        void DeleteEmp ( long id );

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        EmpBasic GetEmp ( long id );

        /// <summary>
        /// 获取人员详细
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns>人员详细</returns>
        EmpData GetEmpData ( long id );

        /// <summary>
        /// 获取人员选择项
        /// </summary>
        /// <param name="param">选择项资料</param>
        /// <returns>人员信息</returns>
        EmpSelectItem[] GetEmpSelectItem ( SelectGetParam param );

        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="query">人员查询参数</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        PagingResult<EmpBasicDatum> QueryEmp ( EmpQuery query, IBasicPage paging );
        /// <summary>
        /// 修改人员资料
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <param name="datum">人员资料</param>
        void SetEmp ( long id, EmpSet datum );
        EmpBasicDatum GetBasic ( long empId, long companyId );

        PagingResult<EmpBasicDto> Query ( EmpQuery query, IBasicPage basicPage );

        void SetStatus ( long id, HrEmpStatus status );
        void SetEmpHead ( long id, string headUrl, long fileId );
        void SetEmpPost ( long id, string post );
        void SetEmpEntry ( long id, EmpEntry value );
    }
}
