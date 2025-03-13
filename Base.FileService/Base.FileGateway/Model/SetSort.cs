using WeDonekRpc.Helper.Validate;
namespace Base.FileGateway.Model
{
    public class SetSort
    {
        [NullValidate("file.sort.null")]
        public Dictionary<long, int> Sort { get; set; }
    }
}
