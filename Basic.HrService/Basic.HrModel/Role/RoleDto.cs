﻿using Basic.HrModel.DB;

namespace Basic.HrModel.Role
{
    public class RoleDto : DBRole
    {
        public long[] PowerId
        {
            get;
            set;
        }
    }
}
