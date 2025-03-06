using Base.FileModel.PhysicalDir;
using Base.FileRemoteModel;
using Base.FileService.Interface;
namespace Base.FileService.DirManage
{
    internal class DirStateMonitor : IDirState
    {
        private readonly DirectoryInfo _Dir;
        private readonly string _RootName;
        public DirStateMonitor (PhysicalDirDto dir)
        {
            this._Dir = new DirectoryInfo(dir.DirFullPath);
            this.IsOnlyRead = dir.Status == PhysicalDirStatus.只读;
            this._RootName = this._Dir.Root.Name;
        }
        public long Id { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsOnlyRead { get; private set; }

        public string DirPath => this._Dir.FullName;

        public void Dispose ()
        {

        }

        public void Refresh (PhysicalDirDto state)
        {
            if (!this.IsOnlyRead && state.Status == PhysicalDirStatus.只读)
            {
                this.IsOnlyRead = true;
            }
        }

        public string CombinePath (params string[] path)
        {
            return Path.Combine(this._Dir.FullName, Path.Combine(path));
        }
        public string GetFullPath (string basePath)
        {
            return Path.Combine(this._Dir.FullName, basePath);
        }

        public bool Distribution (long fileSize)
        {
            if (this.IsOnlyRead)
            {
                return false;
            }
            return DirEquipService.CheckIsUse(this._RootName, fileSize);
        }

    }
}
