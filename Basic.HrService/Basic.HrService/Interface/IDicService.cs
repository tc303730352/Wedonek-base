using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IDicService
    {
        long Add (DicAdd add);
        void Delete (long id);
        DicDto Get (long id);
        PagingResult<DicDatum> Query (DicQuery query, IBasicPage paging);
        bool SetStatus (long id, DicStatus status);
        bool Update (long id, DicSet set);
    }
}