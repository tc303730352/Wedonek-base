using Basic.HrDAL;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Extend
{
    internal class PostDicItemCollect : IPostDicItemCollect
    {
        private readonly ITreeDicItemDAL _TreeItem;
        private readonly IHrConfig _Config;
        public PostDicItemCollect (ITreeDicItemDAL treeDic, IHrConfig config)
        {
            this._Config = config;
            this._TreeItem = treeDic;
        }

        public Dictionary<string, string> GetPostName (string[] codes)
        {
            if (codes.IsNull())
            {
                return null;
            }
            return this._TreeItem.Gets(a => a.DicId == this._Config.PostDicId && codes.Contains(a.DicValue), a => new
            {
                a.DicValue,
                a.DicText
            }).ToDictionary(a => a.DicValue, a => a.DicText);
        }

        public string GetPostName (string code)
        {
            return this._TreeItem.Get(a => a.DicId == this._Config.PostDicId && a.DicValue == code, a => a.DicText);
        }

        public string[] GetPostNameList (string[] codes)
        {
            return this._TreeItem.Gets(a => a.DicId == this._Config.PostDicId && codes.Contains(a.DicValue), a => a.DicText);
        }
    }
}
