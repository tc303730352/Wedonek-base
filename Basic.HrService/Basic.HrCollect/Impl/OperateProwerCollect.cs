using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OperatePrower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class OperateProwerCollect : IOperateProwerCollect
    {
        private readonly IOperateProwerDAL _BasicDAL;

        public OperateProwerCollect ( IOperateProwerDAL basicDAL )
        {
            this._BasicDAL = basicDAL;
        }
        public OperateProwerDto[] Gets ( long prowerId )
        {
            return this._BasicDAL.Gets<OperateProwerDto>(a => a.ProwerId == prowerId);
        }
        public OperateProwerBase[] GetEnables ( long prowerId )
        {
            return this._BasicDAL.Gets<OperateProwerBase>(a => a.ProwerId == prowerId && a.IsEnable);
        }
        public long Add ( OperateProwerAdd data )
        {
            if ( this._BasicDAL.IsExists(c => c.ProwerId == data.ProwerId && c.OperateName == data.OperateName) )
            {
                throw new ErrorException("hr.operate.prower.name.repeat");
            }
            else if ( this._BasicDAL.IsExists(c => c.ProwerId == data.ProwerId && c.OperateVal == data.OperateVal) )
            {
                throw new ErrorException("hr.operate.prower.value.repeat");
            }
            DBOperatePrower add = data.ConvertMap<OperateProwerAdd, DBOperatePrower>();
            return this._BasicDAL.Add(add);
        }
        public bool SetIsEnable ( DBOperatePrower source, bool isEnable )
        {
            if ( source.IsEnable == isEnable )
            {
                return false;
            }
            this._BasicDAL.SetIsEnable(source, isEnable);
            return true;
        }

        public DBOperatePrower Get ( long id )
        {
            return this._BasicDAL.Get(id);
        }

        public bool Set ( DBOperatePrower source, OperateProwerSet data )
        {
            if ( source.IsEnable )
            {
                throw new ErrorException("hr.operate.prower.already.enable");
            }
            else if ( source.OperateName != data.OperateName && this._BasicDAL.IsExists(c => c.ProwerId == source.ProwerId && c.OperateName == data.OperateName) )
            {
                throw new ErrorException("hr.operate.prower.name.repeat");
            }
            return this._BasicDAL.Update(source, data);
        }

        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._BasicDAL.Gets<Result>(ids);
        }
    }
}
