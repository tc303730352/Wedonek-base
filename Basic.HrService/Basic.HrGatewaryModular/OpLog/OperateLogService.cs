using System.Collections.Concurrent;
using Basic.HrGatewaryModular.Config;
using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model;
using Basic.HrRemoteModel.OpLog;
using Basic.HrRemoteModel.OpLog.Model;
using Basic.HrRemoteModel.OpMenu;
using Basic.HrRemoteModel.OpMenu.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Interface;
using WeDonekRpc.HttpApiGateway.Interface;
using WeDonekRpc.Modular;

namespace Basic.HrGatewaryModular.OpLog
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    [IocName("OperateLog")]
    internal class OperateLogService : IApiRouteFilter
    {
        private static readonly ConcurrentDictionary<string, OpMenu> _Menus = new ConcurrentDictionary<string, OpMenu>();
        private static readonly IDelayDataSave<OperateLogAdd> _LogList = new DelayDataSave<OperateLogAdd>(_SaveLog, new TimeSpan(0, 0, 5));

        private static Timer _SyncPower;

        [ThreadStatic]
        private static OperateLogAdd _Log;

        private static IOperateLogConfig _Config;
        public static void Init ()
        {
            _Config = RpcClient.Ioc.Resolve<IOperateLogConfig>();
            _SyncPower = new Timer(_Refresh, null, 500, 60000);
        }
        private static void _SaveLog ( ref OperateLogAdd[] datas )
        {
            new SaveOperateLog
            {
                Logs = datas
            }.Send();
        }
        private static void _Refresh ( object? state )
        {
            OperateMenu[] menus = new GetOperateMenus().Send();
            if ( menus.IsNull() )
            {
                return;
            }
            menus.ForEach(a =>
            {
                if ( !_Menus.ContainsKey(a.RoutePath) )
                {
                    _ = _Menus.TryAdd(a.RoutePath, new OpMenu
                    {
                        BusType = a.BusType,
                        Title = a.Title
                    });
                }
            });
        }

        public void BeginRequest ( IApiService service, IRoute route )
        {
            if ( _Config.IsEnable && route.IsAccredit && !route.Prower.IsNull() )
            {
                if ( !_Menus.TryGetValue(route.ApiUri, out OpMenu menu) )
                {
                    _Log = null;
                    return;
                }
                IUserState state = service.UserState;
                _Log = new OperateLogAdd
                {
                    DeptName = state.ToDeptName(),
                    EmpName = state.ToEmpName(),
                    EmpId = state.ToEmpId(),
                    BusType = menu.BusType,
                    UserType = state.ToUserType(),
                    Uri = service.Request.Url,
                    Referer = service.Referer,
                    Title = menu.Title,
                    Ip = service.Request.ClientIp,
                    Begin = DateTime.Now
                };
                if ( service.Request.HttpMethod == "POST" && service.Request.IsPostFile == false && ( LogRecordRange.请求参数 & _Config.Range ) == LogRecordRange.请求参数 )
                {
                    _Log.PostStr = service.Request.PostString;
                }
            }
            else
            {
                _Log = null;
            }
        }

        public void EndRequest ( IApiService service, IRoute route )
        {
            if ( _Log != null )
            {
                _Log.Duration = ( DateTime.Now - _Log.Begin ).Microseconds;
                _Log.StatusCode = service.Response.StatusCode;
                if ( service.IsError )
                {
                    _Log.IsError = service.IsError;
                    _Log.LastError = service.LastError;
                }
                else if ( ( LogRecordRange.响应结果 & _Config.Range ) == LogRecordRange.响应结果 )
                {
                    _Log.Result = service.Response.ResponseTxt;
                }
                _LogList.AddData(_Log);
                _Log = null;
            }
        }
    }
}
