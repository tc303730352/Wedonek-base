using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IDicService
    {
        /// <summary>
        /// 新建字典
        /// </summary>
        /// <param name="datum">字典资料</param>
        /// <returns></returns>
        long AddDic (DicAdd datum);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id">字典ID</param>
        void DeleteDic (long id);

        /// <summary>
        /// 启用字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        bool EnableDic (long id);

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        DicDto GetDic (long id);

        /// <summary>
        /// 查询字典
        /// </summary>
        /// <param name="query">字典查询参数</param>
        /// <param name="paging">分页参数</param>
        /// <param name="count">数据总数</param>
        /// <returns></returns>
        DicDatum[] QueryDic (DicQuery query, IBasicPage paging, out int count);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <param name="datum">字典资料</param>
        /// <returns></returns>
        bool SetDic (long id, DicSet datum);

        /// <summary>
        /// 停用字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        bool StopDic (long id);

    }
}
