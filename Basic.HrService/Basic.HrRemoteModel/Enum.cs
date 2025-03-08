namespace Basic.HrRemoteModel
{
    public enum DicStatus : int
    {
        起草 = 0,
        停用 = 2,
        启用 = 1
    }

    public enum HrEmpRepeatCheckType
    {
        手机号 = 0,
        员工编号 = 1,
        身份证号 = 2,
        Email = 3
    }

    public enum ProwerType
    {
        /// <summary>
        /// 目录
        /// </summary>
        dir = 1,
        /// <summary>
        ///菜单 
        /// </summary>
        menu = 0
    }


    public enum HrCompanyStatus
    {
        起草 = 0,
        启用 = 1,
        停用 = 2
    }
    /// <summary>
    /// 公司类型
    /// </summary>
    public enum HrCompanyType
    {
        总公司 = 0,
        子公司 = 1,
        分公司 = 2
    }
    /// <summary>
    /// 人员类型
    /// </summary>
    public enum HrUserType
    {
        未设置 = 0,
        实习 = 1,
        正式工 = 2,
        临时 = 3
    }
    /// <summary>
    /// 字典项状态
    /// </summary>
    public enum DicItemStatus
    {
        起草 = 0,
        停用 = 2,
        启用 = 1
    }
    /// <summary>
    /// 用户性别
    /// </summary>
    public enum HrUserSex
    {
        未知 = 0,
        男 = 1,
        女 = 2
    }
    /// <summary>
    /// 人员状态
    /// </summary>
    public enum HrEmpStatus
    {
        起草 = 0,
        启用 = 1,
        禁用 = 2,
    }
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountType
    {
        未开通 = 0,
        手机号 = 2,
        Email = 4,
        登陆名 = 8,
    }
    /// <summary>
    /// 人员在职状态
    /// </summary>
    public enum HrJobStatus
    {
        未填写 = 0,
        在职 = 1,
        离职 = 2,
        停职 = 3
    }
    /// <summary>
    /// 人员考勤状态
    /// </summary>
    public enum HrWorkStatus
    {
        未填写 = 0,
        在岗 = 1,
        请假 = 2,
        调休 = 3,
        出差 = 4,
        外出 = 5,
        旷工 = 6
    }
    public enum HrDeptStatus
    {
        起草 = 0,
        启用 = 1,
        停用 = 2
    }
}
