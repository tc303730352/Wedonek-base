using System.Runtime.CompilerServices;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Company.Model
{
    public class CompanyAdd : CompanySet
    {
        /// <summary>
        /// 父公司ID
        /// </summary>
        [EntrustValidate("_Check")]
        public long ParentId { get; set; }
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private void _Check ()
        {
            if (this.CompanyType != HrCompanyType.总公司 && this.ParentId == 0)
            {
                throw new ErrorException("hr.company.parent.Id.null");
            }
            else if (this.CompanyType == HrCompanyType.总公司 && this.ParentId != 0)
            {
                throw new ErrorException("hr.company.main.not.parent");
            }
        }
    }
}
