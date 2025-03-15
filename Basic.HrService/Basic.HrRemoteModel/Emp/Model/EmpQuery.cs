using WeDonekRpc.Helper.Validate;
namespace Basic.HrRemoteModel.Emp.Model
{
    public class EmpQuery
    {
        /// <summary>
        /// 所在公司ID
        /// </summary>
        [NumValidate("hr.company.Id.error", 1)]
        public long CompanyId { get; set; }
        /// <summary>
        /// 是否入职  IsEntry=true 返回在部门中拥有职务的人员 IsEntry=false 返回部门中的人员
        /// </summary>
        public bool IsEntry { get; set; }
        /// <summary>
        /// 查询Key
        /// </summary>
        public string QueryKey { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        public long[] DeptId { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long? UnitId { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public HrEmpStatus[] Status { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public HrUserType[] UserType { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string[] Post { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string[] Title { get; set; }
        /// <summary>
        /// 是否未开通账号
        /// </summary>
        public bool? IsNoOpen { get; set; }
        /// <summary>
        /// 拥有的角色
        /// </summary>
        public long[] RoleId { get; set; }
    }
}
