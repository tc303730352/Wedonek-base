using Base.FileCollect;
using Base.FileModel.BaseFile;
using Base.FileModel.DB;
using Base.FileService.Helper;
using Base.FileService.Interface;
using Base.FileService.Model.File;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.HttpApiGateway.Model;

namespace Base.FileService.lmpl
{
    internal class FileListService : IFileListService
    {
        private readonly IFileCollect _Service;

        private readonly IFileConfig _Config;

        public FileListService ( IFileCollect service, IFileConfig config )
        {
            this._Service = service;
            this._Config = config;
        }

        public PagingResult<FileUIDto> Query ( PagingParam<FileQuery> query )
        {
            DBFileList[] list = this._Service.Query<DBFileList>(query.Query, query.ToBasicPaging(), out int count);
            if ( list.IsNull() )
            {
                return new PagingResult<FileUIDto>();
            }
            FileUIDto[] files = list.ConvertMap<DBFileList, FileUIDto>();
            files.ForEach(c =>
            {
                c.FileUri = c.GetFileUri(this._Config);
            });
            return new PagingResult<FileUIDto>(files, count);
        }
    }
}
