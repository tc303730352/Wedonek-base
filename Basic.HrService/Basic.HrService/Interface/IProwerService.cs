using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IProwerService
    {
        ProwerTree[] GetProwerTree (long subSysId, bool? isEnable);
        long Add (ProwerAdd add);
        ProwerData Get (long id);
        ProwerSubSystem[] GetTrees ( ProwerGetParam param );
        PagingResult<ProwerBase> Query (ProwerQuery queryParam, IBasicPage basicPage);
        bool Set (long id, ProwerSet datum);
    }
}