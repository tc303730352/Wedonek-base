using Basic.FormGatewaryModular.Model.EnumDic;

namespace Basic.FormGatewaryModular.Interface
{
    public interface IEnumService
    {
        EnumItem[] GetItems ( string name );

        Dictionary<string, EnumItem[]> GetItems ( string[] name );
    }
}