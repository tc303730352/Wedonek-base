namespace Base.FileRemoteModel.Down.Model
{
    public class DownResult
    {
        public bool IsSuccess
        {
            get;
            set;
        }
        public string Error
        {
            get;
            set;
        }
        public Uri FileUri
        {
            get;
            set;
        }
        public long FileId
        {
            get;
            set;
        }
        public string FileName { get; set; }
    }
}
