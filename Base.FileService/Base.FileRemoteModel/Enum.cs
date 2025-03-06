namespace Base.FileRemoteModel
{
    public enum CacheType
    {
        Private,
        Public,
        NoCache,
        NoStore
    }
    public enum FileDirStatus
    {
        启用 = 0,
        只读 = 1,
        禁用 = 2
    }
    public enum UserFileStatus
    {
        起草 = 0,
        正常 = 1,
        已删除 = 2,
        停用 = 3
    }
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        image = 0,
        video = 1,
        audio = 2,
        file = 3,
        doc = 4,
    }
    public enum FileSaveType
    {
        本地 = 0,
        OOS = 1
    }
    /// <summary>
    /// 文件权限
    /// </summary>
    public enum FilePrower
    {
        公共 = 0,
        私有 = 1,
        指定权限 = 2,
        共享 = 3
    }
    /// <summary>
    /// 物理目录状态
    /// </summary>
    public enum PhysicalDirStatus
    {
        可用 = 1,
        只读 = 0,
        警告 = 3
    }
}
