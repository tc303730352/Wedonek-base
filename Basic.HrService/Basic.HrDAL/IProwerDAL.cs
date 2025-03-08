using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IProwerDAL : IBasicDAL<DBProwerList, long>
    {
        int GetSort ( long subSysId, long parentId );

        void SetRelation ( ProwerRelationSet[] sets );
        void Enable ( DBProwerList source );
        void Stop ( long[] ids );

        void Delete ( long[] ids );
        long[] GetAllSubId ( string levelCode, bool? isEnable );
        void Add ( DBProwerList add );
        DBProwerList[] GetEnables ();
        DBProwerList[] GetEnables ( long subSystemId );
        Result[] Query<Result> ( ProwerQuery query, IBasicPage paging, out int count ) where Result : class, new();
        string GetHomeUri ( long subSysId, long[] prowerId );
        string GetHomeUri ( long subSysId );
        T[] Gets<T> ( long[] subSysId, ProwerGetParam param ) where T : class, new();

        T[] Gets<T> ( ProwerQuery query ) where T : class, new();
    }
}