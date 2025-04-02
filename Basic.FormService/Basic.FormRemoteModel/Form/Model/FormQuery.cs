namespace Basic.FormRemoteModel.Form.Model
{
    public class FormQuery
    {
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
