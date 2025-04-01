namespace Basic.FormRemoteModel.Column.Model
{
    public class ColumnValidateRule
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        public ValidateRuleType RuleType { get; set; }
        /// <summary>
        /// 最小数字是否不等
        /// </summary>
        public bool MinIsEqual { get; set; }
        /// <summary>
        /// 最小数字
        /// </summary>
        public decimal? Min { get; set; }

        /// <summary>
        /// 最小数字是否不等
        /// </summary>
        public bool MaxIsEqual { get; set; }
        /// <summary>
        /// 最大数字
        /// </summary>
        public decimal? Max { get; set; }

        /// <summary>
        /// 最小长度
        /// </summary>
        public int? MinLen { get; set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否是警告
        /// </summary>
        public bool IsWarn { get; set; }
    }
}
