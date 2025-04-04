namespace Basic.FormRemoteModel.TableGroup.Model
{
    public class TableGroupSet
    {
        /// <summary>
        /// 表单组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否固定生成
        /// </summary>
        public bool IsFixed { get; set; }
    }
}
