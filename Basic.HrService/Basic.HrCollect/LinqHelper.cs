using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel;
using WeDonekRpc.Helper;

namespace Basic.HrCollect
{
    internal static class LinqHelper
    {
        public static string[] ToSplit (this string str)
        {
            return str.Remove(str.Length - 1, 1).Remove(0, 1).Split('|');
        }
        public static void InitItem (this TreeItemSet[] items, TreeItemSet prt, int len)
        {
            items.ForEach(c =>
            {
                c.LevelCode = prt.LevelCode + c.LevelCode.Remove(0, len);
                if (c.ParentId == prt.Id)
                {
                    _InitItemLvl(items, c, len);
                    if (c.DicStatus == DicItemStatus.启用)
                    {
                    }
                }
            });
        }
        private static void _InitItemLvl (TreeItemSet[] items, TreeItemSet prt, int len)
        {
            items.ForEach(c =>
            {
                if (c.ParentId == prt.Id)
                {
                    c.DicLvl = prt.DicLvl + 1;
                    _InitItemLvl(items, c, len);
                }
            });
        }
    }
}
