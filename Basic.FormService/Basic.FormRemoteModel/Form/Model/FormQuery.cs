using WeDonekRpc.Helper.Validate;

namespace Basic.FormRemoteModel.Form.Model
{
    public class FormQuery
    {
        [NumValidate("hr.company.id.error", 1)]
        public long CompanyId
        {
            get;
            set;
        }
        public string QueryKey
        {
            get;
            set;
        }
        public string FormType
        {
            get;
            set;
        }

        public FormStatus[] Status { get; set; }
    }
}
