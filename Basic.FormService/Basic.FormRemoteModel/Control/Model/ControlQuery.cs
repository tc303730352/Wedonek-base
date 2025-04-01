namespace Basic.FormRemoteModel.Control.Model
{
    public class ControlQuery
    {
        public string QueryKey { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType? ControlType { get; set; }

        /// <summary>
        /// 控件状态
        /// </summary>
        public ControlStatus[] Status { get; set; }
    }
}
