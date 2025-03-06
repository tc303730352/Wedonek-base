using Base.FileService.Model;
using WeDonekRpc.Modular;

namespace Base.FileService.Interface
{
    public interface IUserFileService
    {
        void SaveSort (Dictionary<long, int> sort);
        void Drop (long[] fileId);
        void Drop (long fileId);
        void Drop (string[] dirKey, long[] linkBizPk);
        void Drop (string dirKey, long linkBizPk, string tag);
        void SaveFile (long[] fileId, long linkBizPk, long[] dropId);
        void SaveFile (long fileId, long linkBizPk, long[] dropId);
        DirUpConfig GetUploadConfig (GetConfigArg arg);
        void Delete (long fileId, IUserState state);
    }
}