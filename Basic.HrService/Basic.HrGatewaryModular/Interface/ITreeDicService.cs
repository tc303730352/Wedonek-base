using Basic.HrRemoteModel.TreeDic.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface ITreeDicService
    {
        /// <summary>
        /// 添加树形字典项
        /// </summary>
        /// <param name="datum">树形字典资料</param>
        /// <returns></returns>
        long AddTreeDicItem (TreeDicItemAdd datum);

        /// <summary>
        /// 删除树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        void DeleteTreeDicItem (long id);

        /// <summary>
        /// 启用树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        /// <returns></returns>
        bool EnableTreeDicItem (long id);

        /// <summary>
        /// 获取树形字典项
        /// </summary>
        /// <param name="dicId">字典ID</param>
        /// <returns>树形字典</returns>
        TreeItemBase[] GetDicTree (long dicId);
        TreeFullItem[] GetTree (TreeItemQuery query);

        /// <summary>
        /// 获取树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        /// <returns></returns>
        TreeDicItemDto GetTreeDicItem (long id);

        /// <summary>
        /// 移动树形字典
        /// </summary>
        /// <param name="fromId">来源</param>
        /// <param name="toId">目的地</param>
        void MoveTreeDicItem (long fromId, long toId);

        /// <summary>
        /// 查询字典项
        /// </summary>
        /// <param name="query">查询项</param>
        /// <param name="paging">分页参数</param>
        /// <param name="count">数据总数</param>
        /// <returns></returns>
        TreeDicItemDto[] QueryTreeDicItem (TreeItemQuery query, IBasicPage paging, out int count);

        /// <summary>
        /// 修改树形字典项
        /// </summary>
        /// <param name="id">树形字典项ID</param>
        /// <param name="datum">树形字典项资料</param>
        /// <returns></returns>
        bool SetTreeDicItem (long id, TreeDicItemSet datum);

        /// <summary>
        /// 停用树形字典项
        /// </summary>
        /// <param name="id">树形字典ID</param>
        /// <returns></returns>
        bool StopTreeDicItem (long id);

    }
}
