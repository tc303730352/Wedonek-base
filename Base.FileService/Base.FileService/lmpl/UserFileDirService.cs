using Base.FileCollect;
using Base.FileModel.UserFileDir;
using Base.FileService.Interface;

namespace Base.FileService.lmpl
{
    internal class UserFileDirService : IUserFileDirService
    {
        private readonly IUserFileDirCollect _Service;

        public UserFileDirService ( IUserFileDirCollect service )
        {
            this._Service = service;
        }

        public UserFileDirBase[] GetDirs ()
        {
            return this._Service.GetAll<UserFileDirBase>();
        }
    }
}
