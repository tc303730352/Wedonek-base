using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.DicItem;
using Basic.HrRemoteModel.DicItem.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class DicItemApi : ApiController
    {
        private readonly IDicItemService _Service;
        public DicItemApi ( IDicItemService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 添加字典项
        /// </summary>
        /// <param name="datum">字典项资料</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public long Add ( [NullValidate("hr.dicitem.datum.null")] DicItemAdd datum )
        {
            return this._Service.AddDicItem(datum);
        }

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="id">字典项ID</param>
        [ApiPower("all", "hr.dic.delete")]
        public void Delete ( [NumValidate("hr.dicitem.id.error", 1)] long id )
        {
            this._Service.DeleteDicItem(id);
        }


        public DicItemDto Get ( [NumValidate("hr.dicitem.id.error", 1)] long id )
        {
            return this._Service.GetDicItem(id);
        }

        /// <summary>
        /// 启用字典项
        /// </summary>
        /// <param name="id">启用字典项ID</param>
        [ApiPower("all", "hr.dic.set")]
        public void Enable ( [NumValidate("hr.dicitem.id.error", 1)] long id )
        {
            this._Service.EnableDicItem(id);
        }
        /// <summary>
        /// 获取字典名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string[] GetDicTextList ( LongParam<string[]> param )
        {
            return this._Service.GetDicTextList(param.Id, param.Value);
        }
        /// <summary>
        /// 获取字典项
        /// </summary>
        /// <param name="dicId">字典ID</param>
        /// <returns></returns>
        public DicItem[] Gets ( [NumValidate("hr.dicitem.dicId.error", 1)] long dicId )
        {
            return this._Service.GetDicItems(dicId);
        }
        public Dictionary<string, string> GetItemName ( [NumValidate("hr.dicitem.dicId.error", 1)] long dicId )
        {
            return this._Service.GetItemName(dicId);
        }
        public Dictionary<long, Dictionary<string, string>> GetItemNames ( [NullValidate("hr.dicitem.dicId.null")] long[] dicId )
        {
            return this._Service.GetItemNames(dicId);
        }
        /// <summary>
        /// 查询字典项
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        public DicItemDto[] GetItems ( DicItemQuery query )
        {
            return this._Service.Gets(query).OrderBy(a => a.Sort).ToArray();
        }

        /// <summary>
        /// 设置字典项
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [ApiPower("all", "hr.dic.set")]
        public bool Set ( [NullValidate("hr.dicitem.param.null")] UI_SetDicItem param )
        {
            return this._Service.SetDicItem(param.Id, param.ItemSet);
        }

        /// <summary>
        /// 停用字典项
        /// </summary>
        /// <param name="id">字典项ID</param>
        [ApiPower("all", "hr.dic.set")]
        public void Stop ( [NumValidate("hr.dicitem.id.error", 1)] long id )
        {
            this._Service.StopDicItem(id);
        }
        /// <summary>
        /// 设置排序位
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toId"></param>
        [ApiPower("all", "hr.dic.set")]
        public void Move ( [NumValidate("hr.dicitem.id.error", 1)] long id, [NumValidate("hr.dicitem.to.id.error", 1)] long toId )
        {
            this._Service.Move(id, toId);
        }
    }
}
