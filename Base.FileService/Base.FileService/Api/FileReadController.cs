﻿using Base.FileModel.BaseFile;
using Base.FileService.VisitEvent;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.HttpApiGateway.Response;

namespace Base.FileService.Api
{
    [ApiEventAttr(typeof(FileVisitEvent))]
    internal class FileReadController : ApiController
    {
        [ApiPower(false)]
        [ApiRouteName("/file/read/")]
        public IResponse ReadFile ( bool isDown )
        {
            FileBase file = base.RequestState.Get<FileBase>("file");
            string path = Path.Combine(file.DirPath, file.LocalPath);
            FileInfo fileInfo = new FileInfo(path);
            if ( isDown )
            {
                return new StreamResponse(fileInfo, file.FileName) { IsBinary = true };
            }
            return new StreamResponse(fileInfo);
        }
    }
}
