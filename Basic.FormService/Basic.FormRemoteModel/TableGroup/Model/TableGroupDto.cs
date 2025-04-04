namespace Basic.FormRemoteModel.TableGroup.Model
{
    public class TableGroupDto
    {
        public long Id { get; set; }

        public long ParentId { get; set; }
        /// <summary>
        /// 表单组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 是否固定生成
        /// </summary>
        public bool IsFixed { get; set; }
    }
}
