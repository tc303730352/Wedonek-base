using System.Collections.Frozen;
using Basic.HrGatewaryModular.Model.EnumDic;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IEnumService
    {
        EnumItem[] GetItems (string name);

        Dictionary<string, EnumItem[]> GetItems (string[] name);
    }
}