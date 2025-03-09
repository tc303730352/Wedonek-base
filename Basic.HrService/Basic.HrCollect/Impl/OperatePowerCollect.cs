using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrRemoteModel.OperatePower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class OperatePowerCollect : IOperatePowerCollect
    {
        private readonly IOperatePowerDAL _BasicDAL;

        public OperatePowerCollect ( IOperatePowerDAL basicDAL )
        {
            this._BasicDAL = basicDAL;
        }
        public OperatePowerDto[] Gets ( long powerId )
        {
            return this._BasicDAL.Gets<OperatePowerDto>(a => a.PowerId == powerId);
        }
        public OperatePowerBase[] GetEnables ( long powerId )
        {
            return this._BasicDAL.Gets<OperatePowerBase>(a => a.PowerId == powerId && a.IsEnable);
        }
        public long Add ( OperatePowerAdd data )
        {
            if ( this._BasicDAL.IsExists(c => c.PowerId == data.PowerId && c.OperateName == data.OperateName) )
            {
                throw new ErrorException("hr.operate.power.name.repeat");
            }
            else if ( this._BasicDAL.IsExists(c => c.PowerId == data.PowerId && c.OperateVal == data.OperateVal) )
            {
                throw new ErrorException("hr.operate.power.value.repeat");
            }
            DBOperatePower add = data.ConvertMap<OperatePowerAdd, DBOperatePower>();
            return this._BasicDAL.Add(add);
        }
        public void Delete ( DBOperatePower source )
        {
            if ( source.IsEnable )
            {
                throw new ErrorException("hr.operate.power.already.enable");
            }
            this._BasicDAL.Delete(source.Id);
        }
        public bool SetIsEnable ( DBOperatePower source, bool isEnable )
        {
            if ( source.IsEnable == isEnable )
            {
                return false;
            }
            this._BasicDAL.SetIsEnable(source, isEnable);
            return true;
        }

        public DBOperatePower Get ( long id )
        {
            return this._BasicDAL.Get(id);
        }

        public bool Set ( DBOperatePower source, OperatePowerSet data )
        {
            if ( source.IsEnable )
            {
                throw new ErrorException("hr.operate.power.already.enable");
            }
            else if ( source.OperateName != data.OperateName && this._BasicDAL.IsExists(c => c.PowerId == source.PowerId && c.OperateName == data.OperateName) )
            {
                throw new ErrorException("hr.operate.power.name.repeat");
            }
            return this._BasicDAL.Update(source, data);
        }

        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._BasicDAL.Gets<Result>(ids);
        }

        public void Clear ( long powerId )
        {
            long[] ids = this._BasicDAL.Gets(a => a.PowerId == powerId, a => a.Id);
            if ( ids.IsNull() )
            {
                return;
            }
            this._BasicDAL.Delete(ids);
        }
    }
}
