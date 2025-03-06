using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.Dic;
using Basic.HrRemoteModel.Dic.Model;
using WeDonekRpc.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class DicService : IDicService
    {
        public long AddDic (DicAdd datum)
        {
            return new AddDic
            {
                Datum = datum,
            }.Send();
        }

        public void DeleteDic (long id)
        {
            new DeleteDic
            {
                Id = id,
            }.Send();
        }

        public bool EnableDic (long id)
        {
            return new EnableDic
            {
                Id = id,
            }.Send();
        }

        public DicDto GetDic (long id)
        {
            return new GetDic
            {
                Id = id,
            }.Send();
        }

        public DicDatum[] QueryDic (DicQuery query, IBasicPage paging, out int count)
        {
            return new QueryDic
            {
                Query = query,
                Index = paging.Index,
                Size = paging.Size,
                NextId = paging.NextId,
                SortName = paging.SortName,
                IsDesc = paging.IsDesc
            }.Send(out count);
        }

        public bool SetDic (long id, DicSet datum)
        {
            return new SetDic
            {
                Id = id,
                Datum = datum,
            }.Send();
        }

        public bool StopDic (long id)
        {
            return new StopDic
            {
                Id = id,
            }.Send();
        }

    }
}
