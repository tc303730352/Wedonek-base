using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control.Model;

namespace Basic.FormModel.Control
{
    public class CustomControl
    {
        public long Id { get; set; }

        /// <summary>
        /// 控件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否为基础控件
        /// </summary>
        public bool IsBaseControl { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType ControlType { get; set; }

        /// <summary>
        /// 编辑控件
        /// </summary>
        public string EditControl { get; set; }

        /// <summary>
        /// 显示控件
        /// </summary>
        public string ShowControl { get; set; }

        /// <summary>
        /// 控件最小宽度
        /// </summary>
        public int? MinWidth { get; set; }

        /// <summary>
        /// 控件配置
        /// </summary>
        [SqlSugar.SugarColumn(IsJson = true)]
        public ControlConfig[] Config { get; set; }
    }
}
