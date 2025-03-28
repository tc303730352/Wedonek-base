﻿using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("RoleList")]
    public class DBRole
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }

        public string RoleName { get; set; }

        public string Remark { get; set; }

        public bool IsDefRole { get; set; }

        public bool IsEnable { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime AddTime { get; set; }
    }
}
