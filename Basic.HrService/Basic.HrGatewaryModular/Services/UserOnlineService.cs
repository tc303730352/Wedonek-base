using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model;
using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.CacheClient.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Model;
using WeDonekRpc.Modular;
using WeDonekRpc.ModularModel.Accredit.Model;

namespace Basic.HrGatewaryModular.Services
{
    internal class UserOnlineService : IUserOnlineService
    {
        private readonly IRedisListController _RedisList;
        private readonly IAccreditService _Accredit;
        private readonly string _CacheKey = "OnlineUser";
        public UserOnlineService ( IRedisListController redisList )
        {
            this._RedisList = redisList;
        }
        public void Add ( LoginResult res, LoginState state, string accreditId )
        {
            OnlineUser t = new OnlineUser
            {
                AccreditId = accreditId,
                SysName = state.SysName,
                LoginIp = state.LoginIp,
                LoginTime = DateTime.Now,
                Browser = state.Browser,
                DeptName = res.DeptName,
                UserName = res.EmpName,
                IsOnline = true
            };
            _ = this._RedisList.InsertBefore(this._CacheKey, 0, t);
        }
        public void ClearOnlineUser ()
        {
            long count = this._RedisList.Count(this._CacheKey);
            if ( count == 0 )
            {
                return;
            }
            int skip = 0;
            int size = 50;
            List<OnlineUser> removes = new List<OnlineUser>(10);
            do
            {
                OnlineUser[] users = this._RedisList.Gets<OnlineUser>(this._CacheKey, skip, size);
                if ( users.IsNull() )
                {
                    break;
                }
                string[] ids = users.ConvertAll(a => a.AccreditId);
                List<AccreditState> state = this._Accredit.GetState(ids);
                DateTime now = DateTime.Now;
                users.ForEach(a =>
                {
                    if ( !state.IsExists(c => c.AccreditId == a.AccreditId && ( c.Expire.HasValue == false || c.Expire.Value > now )) )
                    {
                        removes.Add(a);
                    }
                });
                skip += size;
            } while ( skip < count );
            if ( removes.Count > 0 )
            {
                removes.ForEach(c =>
                {
                    _ = this._RedisList.Remove(this._CacheKey, c, 1);
                });
            }
        }

        public void KickOut ( string accreditId )
        {
            this._Accredit.CancelAccredit(accreditId);
        }

        public PagingResult<OnlineUser> Query ( IBasicPage paging )
        {
            int skip = ( paging.Index - 1 ) * paging.Size;
            int count = (int)this._RedisList.Count(this._CacheKey);
            OnlineUser[] list = this._RedisList.Gets<OnlineUser>(this._CacheKey, skip, paging.Size);
            if ( list.IsNull() )
            {
                return new PagingResult<OnlineUser>(list, count);
            }
            List<AccreditState> state = this._Accredit.GetState(list.ConvertAll(a => a.AccreditId));
            list.ForEach(c =>
            {
                AccreditState t = state.Find(a => a.AccreditId == c.AccreditId);
                if ( t != null )
                {
                    c.IsOnline = true;
                    c.Expire = t.Expire;
                }
                else
                {
                    c.IsOnline = false;
                    c.Expire = null;
                }
            });
            return new PagingResult<OnlineUser>(list, count);
        }
    }
}
