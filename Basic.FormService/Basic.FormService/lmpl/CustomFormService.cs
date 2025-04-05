using Basic.FormCollect;
using Basic.FormLocalEvent.Model;
using Basic.FormModel.DB;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.FormService.lmpl
{
    internal class CustomFormService : ICustomFormService
    {
        private readonly ICustomFormCollect _Form;

        public CustomFormService ( ICustomFormCollect form )
        {
            this._Form = form;
        }

        public long Add ( FormAdd data )
        {
            return this._Form.Add(data);
        }
        public FormDto Get ( long id )
        {
            DBCustomForm form = this._Form.Get(id);
            return form.ConvertMap<DBCustomForm, FormDto>();
        }

        public void Delete ( long id )
        {
            DBCustomForm form = this._Form.Get(id);
            this._Form.Delete(form);
            new FormEvent(form).AsyncSend("Delete");
        }

        public PagingResult<FormDto> Query ( FormQuery query, IBasicPage paging )
        {
            FormDto[] dtos = this._Form.Query<FormDto>(query, paging, out int count);
            return new PagingResult<FormDto>(dtos, count);
        }

        public bool Set ( long id, FormSet set )
        {
            DBCustomForm form = this._Form.Get(id);
            return this._Form.Set(form, set);
        }

        public bool SetStatus ( long id, FormStatus status )
        {
            DBCustomForm form = this._Form.Get(id);
            return this._Form.SetStatus(form, status);
        }
    }
}
