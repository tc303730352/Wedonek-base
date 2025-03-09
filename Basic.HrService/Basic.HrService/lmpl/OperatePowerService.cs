using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OperatePower.Model;
using Basic.HrService.Interface;

namespace Basic.HrService.lmpl
{
    internal class OperatePowerService : IOperatePowerService
    {
        private readonly IOperatePowerCollect _Service;

        public OperatePowerService ( IOperatePowerCollect service )
        {
            this._Service = service;
        }

        public long Add ( OperatePowerAdd data )
        {
            return this._Service.Add(data);
        }

        public OperatePowerBase[] GetEnables ( long powerId )
        {
            return this._Service.GetEnables(powerId);
        }

        public OperatePowerDto[] Gets ( long powerId )
        {
            return this._Service.Gets(powerId);
        }

        public bool Set ( long id, OperatePowerSet data )
        {
            DBOperatePower source = this._Service.Get(id);
            return this._Service.Set(source, data);
        }

        public bool SetIsEnable ( long id, bool isEnable )
        {
            DBOperatePower source = this._Service.Get(id);
            return this._Service.SetIsEnable(source, isEnable);
        }
    }
}
