﻿using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Prower
{
    /// <summary>
    /// 获取目录权限树 UI参数实体
    /// </summary>
    internal class UI_GetProwerTree
    {
        /// <summary>
        /// 子级系统ID
        /// </summary>
        [NumValidate("hr.sub.system.id.error", 1)]
        public long SubSystemId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

    }
}
