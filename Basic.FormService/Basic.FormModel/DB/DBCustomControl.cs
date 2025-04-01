using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Control.Model;
using SqlSugar;

namespace Basic.FormModel.DB
{
    [SugarTable("CustomControl")]
    public class DBCustomControl
    {
        [SugarColumn(IsPrimaryKey = true)]
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
        /// 编辑控件
        /// </summary>
        public string EditControl { get; set; }

        /// <summary>
        /// 显示控件
        /// </summary>
        public string ShowControl { get; set; }
        /// <summary>
        /// 控件配置
        /// </summary>
        public ControlConfig[] Config { get; set; }

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
