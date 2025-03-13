using Base.FileCollect;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using Base.FileService.Interface;
using Base.FileStoreGatewayModular.Helper;
using Base.FileStoreGatewayModular.Interface;
using Base.FileStoreGatewayModular.Model.File;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileStoreGatewayModular.Logic
{
    internal class FileListService : IFileListService
    {
        private readonly IFileCollect _Service;
        private readonly IUserFileCollect _UserFile;
        private readonly IFileConfig _Config;

        public FileListService ( IFileCollect service,
            IUserFileCollect userFile,
            IFileConfig config )
        {
            this._Service = service;
            this._UserFile = userFile;
            this._Config = config;
        }

        public PagingResult<FileUIDto> Query ( PagingParam<FileQuery> query )
        {
            DBFileList[] list = this._Service.Query<DBFileList>(query.Query, query.ToBasicPaging(), out int count);
            if ( list.IsNull() )
            {
                return new PagingResult<FileUIDto>();
            }
            Dictionary<long, int> fileNum = this._UserFile.GetFileNum(list.ConvertAll(c => c.Id));
            FileUIDto[] files = list.ConvertMap<DBFileList, FileUIDto>();
            files.ForEach(c =>
            {
                c.FileNum = fileNum.GetValueOrDefault(c.Id);
                c.FileUri = c.GetFileUri(this._Config);
            });
            return new PagingResult<FileUIDto>(files, count);
        }
    }
}
