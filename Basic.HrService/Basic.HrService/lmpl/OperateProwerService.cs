using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OperatePrower.Model;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class OperateProwerService : IOperateProwerService
    {
        private readonly IOperateProwerCollect _Service;

        public OperateProwerService ( IOperateProwerCollect service )
        {
            this._Service = service;
        }

        public long Add ( OperateProwerAdd data )
        {
            return this._Service.Add(data);
        }

        public OperateProwerBase[] GetEnables ( long prowerId )
        {
            return this._Service.GetEnables(prowerId);
        }

        public OperateProwerDto[] Gets ( long prowerId )
        {
            return this._Service.Gets(prowerId);
        }

        public bool Set ( long id, OperateProwerSet data )
        {
            DBOperatePrower source = this._Service.Get(id);
            return this._Service.Set(source, data);
        }

        public bool SetIsEnable ( long id, bool isEnable )
        {
            DBOperatePrower source = this._Service.Get(id);
            return this._Service.SetIsEnable(source, isEnable);
        }
    }
}
