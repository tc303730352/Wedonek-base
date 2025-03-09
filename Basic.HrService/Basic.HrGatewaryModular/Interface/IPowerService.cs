using Basic.HrGatewaryModular.Model.Power;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IPowerService
    {
        /// <summary>
        /// 添加目录权限
        /// </summary>
        /// <param name="datum">目录权限资料</param>
        /// <returns></returns>
        long AddPower ( PowerAdd datum );

        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PowerDataTree[] GetTrees ( PowerQuery query );
        /// <summary>
        /// 获取目录权限
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        PowerData GetPower ( long id );

        /// <summary>
        /// 获取目录权限树
        /// </summary>
        /// <param name="subSystemId">子级系统ID</param>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        PowerTree[] GetPowerTree ( long subSystemId, PowerGetParam param );

        /// <summary>
        /// 获取目录权限树含子系统
        /// </summary>
        /// <returns>目录权限子系统</returns>
        PowerSubSystem[] GetTrees ( PowerGetParam param );

        /// <summary>
        /// 查询目录权限
        /// </summary>
        /// <param name="queryParam">查询参数</param>
        /// <param name="paging">分页参数</param>
        /// <param name="count">数据总数</param>
        /// <returns>目录权限</returns>
        PowerBase[] QueryPower ( PowerQuery queryParam, IBasicPage paging, out int count );

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">目录权限ID</param>
        /// <param name="datum">目录权限资料</param>
        /// <returns></returns>
        bool SetPower ( long id, PowerSetDto datum );
        bool SetSort ( long id, int value );
    }
}
