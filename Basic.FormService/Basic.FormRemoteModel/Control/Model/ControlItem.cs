namespace Basic.FormRemoteModel.Control.Model
{
    public class ControlItem
    {
        public long Id { get; set; }

        /// <summary>
        /// 控件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 控件说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否为基础控件
        /// </summary>
        public bool IsBaseControl { get; set; }
    }
}
