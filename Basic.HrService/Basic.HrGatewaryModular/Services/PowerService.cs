using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Power;
using Basic.HrRemoteModel.Power.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class PowerService : IPowerService
    {
        public long AddPower ( PowerAdd datum )
        {
            return new AddPower
            {
                Datum = datum,
            }.Send();
        }

        public PowerData GetPower ( long id )
        {
            return new GetPower
            {
                Id = id,
            }.Send();
        }
        public PowerDataTree[] GetTrees ( PowerQuery query )
        {
            return new GetPowerTrees
            {
                Query = query
            }.Send();
        }
        public PowerTree[] GetPowerTree ( long subSystemId, PowerGetParam param )
        {
            return new GetPowerTree
            {
                SubSystemId = subSystemId,
                Param = param
            }.Send();
        }

        public PowerSubSystem[] GetTrees ( PowerGetParam param )
        {
            return new GetPowerTreeBySystem
            {
                Param = param
            }.Send();
        }

        public PowerBase[] QueryPower ( PowerQuery queryParam, IBasicPage paging, out int count )
        {
            return new QueryPower
            {
                QueryParam = queryParam,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out count);
        }

        public bool SetPower ( long id, PowerSet datum )
        {
            return new SetPower
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public bool SetSort ( long id, int sort )
        {
            return new SetPowerSort
            {
                Id = id,
                Sort = sort
            }.Send();
        }
    }
}
