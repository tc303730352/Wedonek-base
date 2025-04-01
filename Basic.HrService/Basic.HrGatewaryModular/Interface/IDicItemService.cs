using Basic.HrRemoteModel.DicItem.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IDicItemService
    {
        /// <summary>
        /// 添加字典项
        /// </summary>
        /// <param name="datum">字典项资料</param>
        /// <returns></returns>
        long AddDicItem ( DicItemAdd datum );

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="id">字典项ID</param>
        void DeleteDicItem ( long id );

        /// <summary>
        /// 启用字典项
        /// </summary>
        /// <param name="id">启用字典项ID</param>
        void EnableDicItem ( long id );
        DicItemDto GetDicItem ( long id );

        /// <summary>
        /// 获取字典项
        /// </summary>
        /// <param name="dicId">字典ID</param>
        /// <returns></returns>
        DicItem[] GetDicItems ( long dicId );

        Dictionary<string, string> GetItemName ( long dicId );
        string[] GetDicTextList ( long dicId, string[] values );

        /// <summary>
        /// 查询字典项
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        DicItemDto[] Gets ( DicItemQuery query );
        void Move ( long id, long toId );

        /// <summary>
        /// 设置字典项
        /// </summary>
        /// <param name="id">字典项ID</param>
        /// <param name="itemSet">字典项资料</param>
        /// <returns></returns>
        bool SetDicItem ( long id, DicItemSet itemSet );

        /// <summary>
        /// 停用字典项
        /// </summary>
        /// <param name="id">字典项ID</param>
        void StopDicItem ( long id );
        Dictionary<long, Dictionary<string, string>> GetItemNames ( long[] dicId );
    }
}
