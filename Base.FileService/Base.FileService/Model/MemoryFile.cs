using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;
using WeDonekRpc.HttpService.Interface;

namespace Base.FileService.Model
{
    internal class MemoryFile : IUpFile
    {
        private readonly byte[] _Content;
        public MemoryFile (byte[] stream, string fileName)
        {
            this._Content = stream;
            this.FileName = fileName;
            this.FileType = "file";
        }

        public string ContentType
        {
            get => HttpHeaderHelper.GetContentType(Path.GetExtension(this.FileName));
        }

        public string FileName
        {
            get;
        }

        public long FileSize
        {
            get => this._Content.Length;
        }

        public string FileType
        {
            get;
        }

        public byte FileCs
        {
            get => Tools.CSByByte(this._Content);
        }
        private string _Md5;
        public string FileMd5
        {
            get
            {
                if (this._Md5 == null)
                {
                    this._Md5 = Tools.GetMD5(this._Content);
                }
                return this._Md5;
            }
        }

        public long CopyStream (Stream stream, int offset)
        {
            stream.Position = offset;
            stream.Write(this._Content, 0, this._Content.Length);
            return this._Content.Length;
        }

        public void Dispose ()
        {

        }

        public Stream GetStream ()
        {
            return new MemoryStream(this._Content, false);
        }

        public byte[] ReadStream ()
        {
            return this._Content;
        }

        public string SaveFile (string savePath, bool overwrite = false)
        {
            FileInfo file = new FileInfo(savePath);
            if (file.Exists)
            {
                return savePath;
            }
            else if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            using (FileStream stream = file.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                stream.Position = 0;
                stream.Write(this._Content, 0, this._Content.Length);
                stream.Flush();
            }
            return savePath;
        }

    }
}
