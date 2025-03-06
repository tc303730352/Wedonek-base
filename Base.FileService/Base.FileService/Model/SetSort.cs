using WeDonekRpc.Helper.Validate;
namespace Base.FileService.Model
{
    public class SetSort
    {
        [NullValidate("file.sort.null")]
        public Dictionary<long, int> Sort { get; set; }
    }
}
