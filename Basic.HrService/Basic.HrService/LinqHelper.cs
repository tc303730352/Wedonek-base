using Basic.HrModel.TreeDic;
using Basic.HrRemoteModel.TreeDic.Model;
using Basic.HrService.Model;
using WeDonekRpc.Helper;

namespace Basic.HrService
{
    internal static class LinqHelper
    {
        public static string[] ToSplit ( this string str )
        {
            if ( str.IsNull() )
            {
                return null;
            }
            return str.Substring(1, str.Length - 2).Split('|');
        }
        public static TreeItemBase[] ToTree ( this TreeItem[] items, TreeItem parent )
        {
            return items.Convert(c => c.ParentId == parent.Id, c => new TreeItemBase
            {
                Id = c.Id,
                DicText = c.DicText,
                DicValue = c.DicValue,
                Children = items.ToTree(c)
            });
        }
        public static TreeFullItem[] ToTree ( this TreeItemTemp[] items, TreeItemTemp parent )
        {
            return items.Convert(c => c.ParentId == parent.Id, c => new TreeFullItem
            {
                DicText = c.DicText,
                DicValue = c.DicValue,
                IsEnd = c.IsEnd,
                Sort = c.Sort,
                DicStatus = c.DicStatus,
                Id = c.Id,
                Children = items.ToTree(c)
            });
        }
    }
}
