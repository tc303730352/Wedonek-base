namespace Base.FileService.Interface
{
    public interface IDirService
    {
        IDirState GetDir (ref long fileSize);
        void Init (long serverId);
    }
}