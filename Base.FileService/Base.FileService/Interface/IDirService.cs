namespace Base.FileService.Interface
{
    public interface IDirService
    {
        IDirState GetDir ( long id );
        IDirState GetTempDir ();
        IDirState GetDir ( ref long fileSize );
        void Init ( long serverId );
    }
}