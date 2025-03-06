namespace Basic.HrCollect
{
    public interface ITitleDicItemCollect
    {
        string[] GetTitleName (string[] code);
        string GetTitleName (string code);
        Dictionary<string, string> GetTitleNames (string[] codes);
    }
}