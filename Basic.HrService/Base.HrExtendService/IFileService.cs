namespace Base.HrExtendService
{
    public interface IFileService
    {
        void Drop (long fileId);
        void Drop (string fileUri);
        void Drop (string dirKey, long linkBizPk);
        void Drop (string dirKey, long linkBizPk, string tag);
        void Drop (string dirKey, long[] linkBizPk);
        void Save (long fileId, long linkBizPk, string fileUri);
        void Save (long fileId, long linkBizPk, params long[] dropId);
        void Save (long[] fileId, long linkBizPk, params long[] dropId);
        void Sync (long? fileId, long linkBizPk, string fileUri);
    }
}