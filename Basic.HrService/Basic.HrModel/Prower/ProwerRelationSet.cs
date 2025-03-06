namespace Basic.HrModel.Prower
{
    public class ProwerRelationSet
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
