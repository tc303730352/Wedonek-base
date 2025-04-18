﻿using Basic.HrRemoteModel;

namespace Basic.HrModel.Emp
{
    public class EmpGetParam
    {
        public long CompanyId { get; set; }
        public long[] DeptId { get; set; }

        public HrEmpStatus[] Status { get; set; }

        /// <summary>
        ///   是否入职  IsEntry=true 返回在部门中拥有职务的人员 IsEntry=false 返回部门中的人员
        /// </summary>
        public bool IsEntry { get; set; }
    }
}
