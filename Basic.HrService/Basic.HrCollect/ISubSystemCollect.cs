using Basic.HrModel.SubSystem;

namespace Basic.HrCollect
{
    public interface ISubSystemCollect
    {
        SubSystemDto[] Gets ( long[] ids );
        SubSystemDto[] Gets ( bool? isEnable );
    }
}