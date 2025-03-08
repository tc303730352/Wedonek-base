using Basic.HrModel.DB;
using Basic.HrModel.Prower;
using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IProwerCollect
    {
        Result[] Query<Result> ( ProwerQuery query, IBasicPage paging, out int count ) where Result : class, new();
        ProwerRouteDto[] GetRoutes ( long subSysId );
        bool Set ( DBProwerList sour, ProwerSet set );
        long Add ( ProwerAdd add );
        ProwerDto[] GetDtos ( long[] subSystemId, ProwerGetParam getParam );
        ProwerDto[] GetDtos ( long subSystemId, bool? isEnable );
        ProwerDto[] GetDtos ( long subSystemId );
        ProwerBasic[] Gets ( long[] ids );
        ProwerRouteDto[] GetEnables ();
        DBProwerList Get ( long id );
        ProwerBasic[] GetFulls ( long[] ids );
        string GetHomeUri ( long subSysId );
    }
}