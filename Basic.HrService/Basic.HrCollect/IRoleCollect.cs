﻿using Basic.HrModel.DB;
using Basic.HrModel.Role;
using Basic.HrModel.RolePower;
using Basic.HrRemoteModel.Role.Model;
using WeDonekRpc.Model;

namespace Basic.HrCollect
{
    public interface IRoleCollect
    {
        bool SetIsAdmin ( DBRole role, bool isAdmin );
        void CheckIsEnable ( long[] ids );

        RoleDto GetRole ( long roleId );

        long Add ( long companyId, RoleSetDatum add, RolePower[] powers );
        void AddDefRole ( long companyId );
        void Delete ( DBRole role );

        DBRole Get ( long roleId );

        long GetDefRoleId ( long companyId );

        RoleBase[] GetBases ( long[] ids );


        bool Set ( DBRole role, RoleSetDatum set, RolePower[] powers );

        bool SetIsEnable ( DBRole role, bool enable );

        bool CheckIsAdmin ( long companyId, long[] roleId );
        RoleSelectItem[] GetSelect ( long companyId );
        Result[] Query<Result> ( RoleGetParam param, IBasicPage paging, out int count ) where Result : class, new();
        void SetIsDef ( DBRole role, long defId );
        void Clear ( long companyId );
    }
}