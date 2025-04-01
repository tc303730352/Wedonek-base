namespace Basic.FormRemoteModel.Control.Model
{
    public class ControlDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 控件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控件说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType ControlType { get; set; }

        /// <summary>
        /// 控件最小宽度
        /// </summary>
        public int? MinWidth { get; set; }
        /// <summary>
        /// 控件状态
        /// </summary>
        public ControlStatus Status { get; set; }
    }
}
