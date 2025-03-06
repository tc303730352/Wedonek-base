﻿using Basic.HrRemoteModel.EmpLogin.Model;
using Basic.HrRemoteModel.SubSystem.Model;

namespace Basic.HrGatewaryModular.Model.EmpLogin
{
    public class LoginDatum
    {
        /// <summary>
        /// 子系统
        /// </summary>
        public SubSystemItem[] SubSystem { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public Dictionary<long, ProwerRoute[]> Prower { get; set; }
        /// <summary>
        /// 人员公司
        /// </summary>
        public Dictionary<long, string> Company { get; set; }
        /// <summary>
        /// 当前选择的子系统
        /// </summary>
        public long CurSubSysId { get; set; }

        public LoginUser Datum { get; set; }
    }
}
