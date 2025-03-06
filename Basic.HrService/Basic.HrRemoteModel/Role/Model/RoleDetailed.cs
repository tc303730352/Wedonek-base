namespace Basic.HrRemoteModel.Role.Model
{
    /// <summary>
    /// 角色详细
    /// </summary>
    public class RoleDetailed : RoleDatum
    {
        /// <summary>
        /// 权限
        /// </summary>
        public long[] ProwerId { get; set; }
    }
}
