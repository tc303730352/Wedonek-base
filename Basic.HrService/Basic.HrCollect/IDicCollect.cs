using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IDicCollect
    {
        long Add (DicAdd add);
        void Delete (DBDicList db);
        DBDicList Get (long id);
        DBDicList[] Query (DicQuery query, IBasicPage paging, out int count);
        bool SetStatus (DBDicList sour, DicStatus status);
        bool Update (DBDicList source, DicSet set);
    }
}