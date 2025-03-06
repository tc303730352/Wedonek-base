using Basic.HrDAL;

namespace Basic.HrCollect.Extend
{
    internal class TitleDicItemCollect : ITitleDicItemCollect
    {
        private readonly IDicItemDAL _DictItem;
        private readonly IHrConfig _Config;
        public TitleDicItemCollect (IDicItemDAL dictItem, IHrConfig config)
        {
            this._Config = config;
            this._DictItem = dictItem;
        }

        public Dictionary<string, string> GetTitleNames (string[] codes)
        {
            return this._DictItem.Gets(a => a.DicId == this._Config.TitleDicId && codes.Contains(a.DicValue), a => new
            {
                a.DicValue,
                a.DicText
            }).ToDictionary(a => a.DicValue, a => a.DicText);
        }

        public string GetTitleName (string code)
        {
            return this._DictItem.Get(a => a.DicId == this._Config.TitleDicId && a.DicValue == code, a => a.DicText);
        }

        public string[] GetTitleName (string[] code)
        {
            return this._DictItem.Gets(a => a.DicId == this._Config.TitleDicId && code.Contains(a.DicValue), a => a.DicText);
        }
    }
}
