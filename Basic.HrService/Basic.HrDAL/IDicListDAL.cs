using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IDicListDAL : IBasicDAL<DBDicList, long>
    {
        void Add (DBDicList db);
        void Delete (DBDicList db);
        DBDicList[] Query (DicQuery query, IBasicPage paging, out int count);
        void SetStatus (DBDicList sour, DicStatus status);
    }
}