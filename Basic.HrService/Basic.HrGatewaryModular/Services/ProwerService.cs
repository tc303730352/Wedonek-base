using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Prower;
using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class ProwerService : IProwerService
    {
        public long AddPrower ( ProwerAdd datum )
        {
            return new AddPrower
            {
                Datum = datum,
            }.Send();
        }

        public ProwerData GetPrower ( long id )
        {
            return new GetPrower
            {
                Id = id,
            }.Send();
        }

        public ProwerTree[] GetProwerTree ( long subSystemId, bool? isEnable )
        {
            return new GetProwerTree
            {
                SubSystemId = subSystemId,
                IsEnable = isEnable,
            }.Send();
        }

        public ProwerSubSystem[] GetTrees ( ProwerGetParam param )
        {
            return new GetProwerTreeBySystem
            {
                Param = param
            }.Send();
        }

        public ProwerBase[] QueryPrower ( ProwerQuery queryParam, IBasicPage paging, out int count )
        {
            return new QueryPrower
            {
                QueryParam = queryParam,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out count);
        }

        public bool SetPrower ( long id, ProwerSet datum )
        {
            return new SetPrower
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

    }
}
