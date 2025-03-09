namespace Basic.HrModel.Power
{
    public class PowerRelationSet
    {
        public long Id { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 层级码
        /// </summary>
        public string LevelCode { get; set; }
    }
}
