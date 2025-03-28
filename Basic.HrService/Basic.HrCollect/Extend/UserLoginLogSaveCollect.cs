﻿using Basic.HrCollect.Helper;
using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.UserLoginLog;
using Basic.HrRemoteModel.EmpLogin.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Client.Ioc;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Helper.Interface;

namespace Basic.HrCollect.Extend
{
    internal class UserLoginLogSaveCollect : IUserLoginLogSaveCollect
    {
        private readonly IDelayDataSave<LoginLogAdd> _DelaySave = new DelayDataSave<LoginLogAdd>(_save, 10, 5);

        private static void _save ( ref LoginLogAdd[] datas )
        {
            DBUserLoginLog[] logs = datas.ConvertMap<LoginLogAdd, DBUserLoginLog>();
            logs.ForEach(c =>
            {
                c.Id = IdentityHelper.CreateId();
                c.Address = _GetIpAddress(c.LoginIp);
            });
            using ( IocScope scope = RpcClient.Ioc.CreateScore() )
            {
                IUserLoginLogDAL logDAL = scope.Resolve<IUserLoginLogDAL>();
                logDAL.Adds(logs);
            }
        }
        private static string _GetIpAddress ( string ip )
        {
            try
            {
                return IpAddressHelper.GetAddress(ip);
            }
            catch ( Exception ex )
            {
                ErrorException.FormatError(ex).SaveLog("LoginLog");
                return string.Empty;
            }
        }
        public void Add ( string loginName, LoginState state )
        {
            this._DelaySave.AddData(new LoginLogAdd
            {
                SysName = state.SysName,
                IsSuccess = true,
                Browser = state.Browser,
                LoginIp = state.LoginIp,
                LoginName = loginName,
                LoginTime = DateTime.Now
            });
        }

        public void AddFailLog ( string loginName, ErrorException error, LoginState state )
        {
            this._DelaySave.AddData(new LoginLogAdd
            {
                SysName = state.SysName,
                IsSuccess = false,
                ErrorCode = error.ErrorCode,
                Browser = state.Browser,
                LoginIp = state.LoginIp,
                LoginName = loginName,
                LoginTime = DateTime.Now
            });
        }

    }
}
