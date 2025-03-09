namespace Basic.HrRemoteModel.OperatePower.Model
{
    public class OperatePowerBase
    {
        public long Id { get; set; }

        /// <summary>
        /// 操作权限值
        /// </summary>
        public string OperateVal { get; set; }

        /// <summary>
        /// 操作权限名
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 说明文字
        /// </summary>
        public string Show { get; set; }
    }
}
