namespace Basic.FormRemoteModel
{
    public enum FormStatus
    {
        起草 = 0,
        启用 = 1,
        停用 = 2
    }
    public enum ControlStatus
    {
        起草 = 0,
        启用 = 1,
        停用 = 2
    }
    public enum ValidateRuleType
    {
        数字范围 = 0,
        正则表达式 = 1,
        字符长度 = 2
    }
    public enum ControlValueRange
    {
        自定义 = 0,
        树形字典 = 1,
        普通字典 = 2,
        开关 = 3,
        下拉选项 = 4,
        自定义选项 = 5
    }
    public enum ControlType
    {
        Input = 0,
        Number = 1,
        Text = 2,
        Label = 3,
        Date = 4,
        Null = 5,
        Emp = 6,
        Dept = 7,
        Company = 8,
        Time = 9,
        Switch = 10,
        Customize = 11
    }
    public enum FormTableType
    {
        单一表单 = 0,
        多行列表 = 1,
    }
}
