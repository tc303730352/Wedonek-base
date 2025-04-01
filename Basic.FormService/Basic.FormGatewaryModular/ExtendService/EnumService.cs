using System.Collections.Frozen;
using System.Reflection;
using Basic.FormGatewaryModular.Interface;
using Basic.FormGatewaryModular.Model.EnumDic;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Config;

namespace Basic.FormGatewaryModular.ExtendService
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class EnumService : IEnumService
    {
        private static FrozenDictionary<string, EnumItem[]> _EnumDic = FrozenDictionary<string, EnumItem[]>.Empty;

        public static void Load ()
        {
            IConfigSection section = RpcClient.Config.GetSection("dic");
            string[] names = section.GetValue("enum", new string[] { "Basic.HrRemoteModel", "Base.FileRemoteModel" });
            if ( names.IsNull() )
            {
                return;
            }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().FindAll(c => names.Contains(c.GetName().Name));
            if ( assemblies.IsNull() )
            {
                return;
            }
            Dictionary<string, EnumItem[]> enumDic = [];
            assemblies.ForEach(c =>
            {
                Type[] types = c.GetTypes().FindAll(a => a.IsEnum);
                if ( types.IsNull() )
                {
                    return;
                }
                string key = c.GetName().Name;
                types.ForEach(a =>
                {
                    EnumItem[] items = Enum.GetValues(a).ConvertAll(c => new EnumItem
                    {
                        Text = c.ToString(),
                        Value = (int)c
                    });
                    if ( !enumDic.ContainsKey(a.Name) )
                    {
                        enumDic.Add(a.Name, items);
                    }
                });
            });
            _EnumDic = enumDic.ToFrozenDictionary();
        }
        public EnumItem[] GetItems ( string name )
        {
            return _EnumDic.GetValueOrDefault(name);
        }

        public Dictionary<string, EnumItem[]> GetItems ( string[] name )
        {
            return name.Where(_EnumDic.ContainsKey).ToDictionary(a => a, a => _EnumDic[a]);
        }
    }
}
