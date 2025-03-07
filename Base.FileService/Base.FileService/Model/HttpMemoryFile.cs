using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.Model
{
    internal class HttpMemoryFile : IUpFile
    {
        private readonly HttpResult _Response;
        public HttpMemoryFile ( HttpResult res, string fileName )
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

        public long FileSize => this._Response.Content.LongLength;

        public string FileType
        {
            get;
        }

        public byte FileCs => Tools.CSByByte(this._Response.Content);
        private string _Md5;
        public string FileMd5
        {
            get
            {
                this._Md5 ??= Tools.GetMD5(this._Response.Content);
                return this._Md5;
            }
        }

        public long CopyStream ( Stream stream, int offset )
        {
            byte[] buffer = this._Response.Content;
            stream.Position = offset;
            stream.Write(buffer, 0, buffer.Length);
            return buffer.Length;
        }

        public void Dispose ()
        {

        }

        public Stream GetStream ()
        {
            return new MemoryStream(this._Response.Content, false);
        }

        public byte[] ReadStream ()
        {
            return this._Response.Content;
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
            byte[] myBytes = this._Response.Content;
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
