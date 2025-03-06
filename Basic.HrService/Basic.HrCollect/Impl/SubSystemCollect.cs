using Basic.HrDAL;
using Basic.HrModel.SubSystem;

namespace Basic.HrCollect.Impl
{
    internal class SubSystemCollect : ISubSystemCollect
    {
        private readonly ISubSystemDAL _SubSystemDAL;

        public SubSystemCollect (ISubSystemDAL subSystemDAL)
        {
            this._SubSystemDAL = subSystemDAL;
        }
        public SubSystemDto[] Gets (long[] ids)
        {
            return this._SubSystemDAL.Gets<SubSystemDto>(a => ids.Contains(a.Id) && a.IsEnable);
        }
        public SubSystemDto[] GetEnables ()
        {
            return this._SubSystemDAL.GetEnables();
        }

    }
}
