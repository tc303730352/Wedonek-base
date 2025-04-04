﻿using System.Runtime.CompilerServices;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Company.Model
{
    public class CompanySet
    {
        /// <summary>
        /// 父公司ID
        /// </summary>
        [EntrustValidate("_Check")]
        public long ParentId { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        [NullValidate("hr.company.name.null")]
        [LenValidate("hr.company.name.len", 2, 100)]
        public string FullName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        [LenValidate("hr.company.short.name.len", 0, 50)]
        public string ShortName { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        [EnumValidate("hr.company.type.error", typeof(HrCompanyType))]
        public HrCompanyType CompanyType { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [LenValidate("hr.company.address.len", 0, 100)]
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [LenValidate("hr.company.contacts.len", 0, 50)]
        public string Contacts { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [FormatValidate("hr.company.telephone.error", ValidateFormat.联系电话)]
        public string Telephone { get; set; }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private void _Check ()
        {
            if ( this.CompanyType != HrCompanyType.总公司 && this.ParentId == 0 )
            {
                throw new ErrorException("hr.company.parent.Id.null");
            }
            else if ( this.CompanyType == HrCompanyType.总公司 && this.ParentId != 0 )
            {
                throw new ErrorException("hr.company.main.not.parent");
            }
        }

    }
}
