﻿namespace Basic.HrModel.DeptPower
{
    public class DeptPowerAdd
    {
        public long CompanyId { get; set; }

        public long EmpId { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
    }
}
