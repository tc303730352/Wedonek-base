using Basic.FormGatewaryModular.Interface;
using Basic.FormGatewaryModular.Model.EnumDic;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
namespace Basic.FormGatewaryModular.Api
{
    internal class EnumApi : ApiController
    {
        private readonly IEnumService _Service;

        public EnumApi ( IEnumService service )
        {
            this._Service = service;
        }

        /// <summary>
        /// 获取枚举项
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EnumItem[] GetItems ( [NullValidate("hr.enum.name.null")] string name )
        {
            return this._Service.GetItems(name);
        }

        public Dictionary<string, EnumItem[]> Gets ( [NullValidate("hr.enum.name.null")] string[] keys )
        {
            return this._Service.GetItems(keys);
        }
    }
}
