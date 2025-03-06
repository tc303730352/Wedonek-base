
using System.Collections.Frozen;

namespace Basic.HrCollect
{
    public interface IPostDicItemCollect
    {
        string GetPostName (string code);
        Dictionary<string, string> GetPostName (string[] codes);
        string[] GetPostNameList (string[] codes);
    }
}