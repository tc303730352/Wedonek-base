using Base.FileRemoteModel.UpFile;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Helper;

namespace Base.HrExtendService.lmpl
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class FileService : IFileService
    {
        public void Save (long fileId, long linkBizPk, params long[] dropId)
        {
            new SaveFile
            {
                FileId = fileId,
                LinkBizPk = linkBizPk,
                DropFileId = dropId
            }.SyncSend();
        }
        public void Save (long fileId, long linkBizPk, string fileUri)
        {
            if (fileUri.IsNull())
            {
                this.Save(fileId, linkBizPk);
            }
            else
            {
                long id = long.Parse(Path.GetFileNameWithoutExtension(fileUri));
                this.Save(fileId, linkBizPk, id);
            }
        }
        public void Save (long[] fileId, long linkBizPk, params long[] dropId)
        {
            new SaveFileList
            {
                FileId = fileId,
                LinkBizPk = linkBizPk,
                DropFileId = dropId
            }.SyncSend();
        }
        public void Drop (string dirKey, long linkBizPk)
        {
            new DropFile
            {
                DirKey = dirKey,
                LinkBizPk = linkBizPk
            }.SyncSend();
        }
        public void Drop (string dirKey, long linkBizPk, string tag)
        {
            new DropFile
            {
                DirKey = dirKey,
                LinkBizPk = linkBizPk,
                Tag = tag
            }.SyncSend();
        }
        public void Drop (long fileId)
        {
            new DropFileById
            {
                FileId = fileId
            }.SyncSend();
        }
        public void Drop (string fileUri)
        {
            long id = long.Parse(Path.GetFileNameWithoutExtension(fileUri));
            new DropFileById
            {
                FileId = id
            }.SyncSend();
        }
        public void Sync (long? fileId, long linkBizPk, string fileUri)
        {
            if (fileId.HasValue == false && fileUri.IsNull())
            {
                return;
            }
            else if (fileId.HasValue == false)
            {
                this.Drop(fileUri);
            }
            else if (fileUri.IsNull())
            {
                this.Save(fileId.Value, linkBizPk);
            }
            else
            {
                long id = long.Parse(Path.GetFileNameWithoutExtension(fileUri));
                this.Save(fileId.Value, linkBizPk, id);
            }
        }

        public void Drop (string dirKey, long[] linkBizPk)
        {
            new BatchDropFile
            {
                DirKey = new string[] { dirKey },
                LinkBizPk = linkBizPk
            }.SyncSend();
        }
    }
}
