namespace Basic.FormRemoteModel.TableGroup.Model
{
    public class TableGroupDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 表单组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 是否隐藏标题
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 是否固定生成
        /// </summary>
        public bool IsFixed { get; set; }
    }
}
