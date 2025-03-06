using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.Model
{
    internal class HttpMemoryFile : IUpFile
    {
        private readonly HttpResponseRes _Response;
        public HttpMemoryFile ( HttpResponseRes res, string fileName )
        {
            this._Response = res;
            this.FileName = fileName;
            this.FileType = "downFile";
        }

        public string ContentType => this._Response.ContentType;

        public string FileName
        {
            get;
        }

        public long FileSize => this._Response.SourceBytes.LongLength;

        public string FileType
        {
            get;
        }

        public byte FileCs => Tools.CSByByte(this._Response.SourceBytes);
        private string _Md5;
        public string FileMd5
        {
            get
            {
                this._Md5 ??= Tools.GetMD5(this._Response.SourceBytes);
                return this._Md5;
            }
        }

        public long CopyStream ( Stream stream, int offset )
        {
            byte[] buffer = this._Response.SourceBytes;
            stream.Position = offset;
            stream.Write(buffer, 0, buffer.Length);
            return buffer.Length;
        }

        public void Dispose ()
        {

        }

        public Stream GetStream ()
        {
            return new MemoryStream(this._Response.SourceBytes, false);
        }

        public byte[] ReadStream ()
        {
            return this._Response.SourceBytes;
        }

        public string SaveFile ( string savePath, bool overwrite = false )
        {
            FileInfo file = new FileInfo(savePath);
            if ( file.Exists )
            {
                return savePath;
            }
            else if ( !file.Directory.Exists )
            {
                file.Directory.Create();
            }
            byte[] myBytes = this._Response.SourceBytes;
            using ( FileStream stream = file.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read) )
            {
                stream.Position = 0;
                stream.Write(myBytes, 0, myBytes.Length);
                stream.Flush();
            }
            return savePath;
        }

    }
}
