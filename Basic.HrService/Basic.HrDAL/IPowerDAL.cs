using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Model;

namespace Basic.HrDAL
{
    public interface IPowerDAL : IBasicDAL<DBPowerList, long>
    {
        int GetSort ( long subSysId, long parentId );

        void SetRelation ( PowerRelationSet[] sets );
        void Enable ( DBPowerList source );
        void Stop ( long[] ids );

        void Delete ( long[] ids );
        long[] GetAllSubId ( string levelCode, bool? isEnable );
        void Add ( DBPowerList add );
        DBPowerList[] GetEnables ();
        DBPowerList[] GetEnables ( long subSystemId );
        Result[] Query<Result> ( PowerQuery query, IBasicPage paging, out int count ) where Result : class, new();
        string GetHomeUri ( long subSysId, long[] powerId );
        string GetHomeUri ( long subSysId );
        T[] Gets<T> ( long[] subSysId, PowerGetParam param ) where T : class, new();

        T[] Gets<T> ( PowerQuery query ) where T : class, new();
        void SetSort ( DBPowerList db, int sort );
    }
}