﻿using Basic.HrRemoteModel;
using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("PowerList")]
    public class DBPowerList
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 子系统ID
        /// </summary>
        public long SubSystemId { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 层级码
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public PowerType PowerType { get; set; }
        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int LevelNum { get; set; }
        /// <summary>
        /// 页面路由路径
        /// </summary>
        public string RoutePath { get; set; }

        /// <summary>
        /// 页面路由名
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 页面路径
        /// </summary>
        public string PagePath { get; set; }

        /// <summary>
        /// 布局
        /// </summary>
        public string Layout { get; set; }

        [SugarColumn(IsJson = true)]
        public Dictionary<string, string> PageParam { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

    }
}
