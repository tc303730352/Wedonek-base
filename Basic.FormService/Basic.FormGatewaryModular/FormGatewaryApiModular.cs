using Basic.FormGatewaryModular.ExtendService;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Interface;

namespace Basic.FormGatewaryModular
{
    public class FormGatewaryApiModular : BasicApiModular
    {
        public FormGatewaryApiModular () : base("FormApi_Gateway")
        {
        }
        protected override void Load ( IHttpGatewayOption option, IModularConfig config )
        {
            config.ApiRouteFormat = "/form/{controller}/{name}";
            option.IocBuffer.SetDefLifetimeType(( body ) =>
            {
                if ( body.To.FullName.StartsWith("Basic.FormGatewaryModular.Interface.") )
                {
                    body.SetLifetimeType(ClassLifetimeType.SingleInstance);
                }
            });
        }
        protected override void Init ()
        {
            base.Init();
            EnumService.Load();
        }
    }
}
