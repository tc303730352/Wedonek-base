using Basic.HrModel.DB;
using Basic.HrModel.Power;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IPowerCollect
    {
        Result[] Query<Result> ( PowerQuery query, IBasicPage paging, out int count ) where Result : class, new();
        PowerRouteDto[] GetRoutes ( long subSysId );
        bool Set ( DBPowerList sour, PowerSet set );
        long[] Filters ( long[] ids, PowerType powerType );
        long Add ( PowerAdd add );
        PowerDto[] GetDtos ( long[] subSystemId, PowerGetParam getParam );
        PowerDto[] GetDtos ( long subSystemId, PowerGetParam param );
        PowerDto[] GetDtos ( long subSystemId );
        PowerBasic[] Gets ( long[] ids );
        PowerRouteDto[] GetEnables ( long[] ids );
        DBPowerList Get ( long id );
        PowerBasic[] GetFulls ( long[] ids );
        string GetHomeUri ( long subSysId );
        T[] Gets<T> ( PowerQuery query ) where T : class, new();
        bool SetSort ( DBPowerList db, int sort );
        void Delete ( DBPowerList db );
        bool SetIsEnable ( DBPowerList db, bool isEnable );
    }
}