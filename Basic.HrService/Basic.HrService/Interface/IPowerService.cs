using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrService.Interface
{
    public interface IPowerService
    {
        bool SetSort ( long id, int sort );
        PowerDataTree[] GetTrees ( PowerQuery query );
        PowerTree[] GetPowerTree ( long subSysId, PowerGetParam param );
        long Add ( PowerAdd add );
        PowerData Get ( long id );
        PowerSubSystem[] GetTrees ( PowerGetParam param );
        PagingResult<PowerBase> Query ( PowerQuery queryParam, IBasicPage basicPage );
        bool Set ( long id, PowerSet datum );
        void Delete ( long id );
        bool SetIsEnable ( long id, bool isEnable );
    }
}