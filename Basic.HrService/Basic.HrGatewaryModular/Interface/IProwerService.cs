using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IProwerService
    {
        /// <summary>
        /// 添加目录权限
        /// </summary>
        /// <param name="datum">目录权限资料</param>
        /// <returns></returns>
        long AddPrower (ProwerAdd datum);

        /// <summary>
        /// 获取目录权限
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        ProwerData GetPrower (long id);

        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="subSystemId">子级系统ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        ProwerTree[] GetProwerTree (long subSystemId, bool? isEnable);

        /// <summary>
        /// 获取目录权限树含子系统
        /// </summary>
        /// <returns>目录权限子系统</returns>
        ProwerSubSystem[] GetProwerTreeBySystem ();

        /// <summary>
        /// 查询目录权限
        /// </summary>
        /// <param name="queryParam">查询参数</param>
        /// <param name="paging">分页参数</param>
        /// <param name="count">数据总数</param>
        /// <returns>目录权限</returns>
        ProwerBase[] QueryPrower (ProwerQuery queryParam, IBasicPage paging, out int count);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">目录权限ID</param>
        /// <param name="datum">目录权限资料</param>
        /// <returns></returns>
        bool SetPrower (long id, ProwerSet datum);

    }
}
