using Basic.FormCollect;
using Basic.FormModel.Column;
using Basic.FormModel.DB;
using Basic.FormModel.Group;
using Basic.FormModel.Table;
using Basic.FormRemoteModel;
using Basic.FormRemoteModel.Form.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Helper;

namespace Basic.FormService.Extend
{
    internal class FormBodyService : IFormBodyService
    {
        private readonly ICustomFormCollect _Form;
        private readonly ICustomTableCollect _Table;
        private readonly ITableGroupCollect _TableGroup;
        private readonly ITableColumnCollect _TableColumn;

        public FormBodyService ( ICustomFormCollect form,
            ICustomTableCollect table,
            ITableGroupCollect tableGroup,
            ITableColumnCollect tableColumn )
        {
            this._Form = form;
            this._Table = table;
            this._TableGroup = tableGroup;
            this._TableColumn = tableColumn;
        }
        private FormTableColumn _Format ( TableColumnBase col )
        {
            return new FormTableColumn
            {
                ColId = col.Id,
                Sort = col.Sort,
                ShowControl = col.ShowControl,
                ColSpan = col.ColSpan,
                ColType = col.ColType,
                EditControl = col.EditControl,
                DefaultVal = col.DefaultVal,
                Description = col.Description,
                IsNotNull = col.IsNotNull,
                ColTitle = col.ColTitle,
                ColName = col.ColAliasName.GetValueOrDefault(col.ColName),
                ControlSet = col.ControlSet,
                MaxLen = col.MaxLen,
                Width = col.Width
            };
        }
        private void _InitColumn ( TableGroupBase prt, TableGroupBase[] groups, TableColumnBase[] cols, List<FormTableColumn> list )
        {
            TableColumnBase[] gcols = cols.FindAll(c => c.GroupId == prt.Id);
            TableGroupBase[] children = groups.FindAll(c => c.ParentId == prt.Id);
            if ( gcols.IsNull() && children.IsNull() )
            {
                return;
            }
            if ( prt.IsHidden )
            {
                gcols.ForEach(c =>
                 {
                     list.Add(this._Format(c));
                 });
                children.ForEach(a =>
                {
                    this._InitColumn(a, groups, cols, list);
                });
            }
            else
            {
                FormTableColumn col = new FormTableColumn
                {
                    ColName = prt.GroupName,
                    Children = new List<FormTableColumn>()
                };
                if ( gcols.Length > 0 )
                {
                    gcols.ForEach(c =>
                    {
                        col.Children.Add(this._Format(c));
                    });
                }
                children.ForEach(a =>
                {
                    this._InitColumn(a, groups, cols, col.Children);
                });
                list.Add(col);
            }
        }
        public FormBody Get ( long id )
        {
            DBCustomForm form = this._Form.Get(id);
            FormBody body = new FormBody
            {
                FormName = form.FormName,
                Ver = form.Ver,
                FormStatus = form.FormStatus,
                FormShow = form.FormShow,
                FormType = form.FormType
            };
            TableBase[] tables = this._Table.GetsByFormId<TableBase>(id);
            if ( tables.IsNull() )
            {
                return body;
            }
            body.Tables = tables.OrderBy(a => a.Sort).Select(a => new FormTableBody
            {
                Id = a.Id,
                ColNum = a.ColNum,
                LabelWidth = a.LabelWidth,
                IsHidden = a.IsHidden,
                TableType = a.TableType,
                Title = a.Title
            }).ToArray();
            TableColumnBase[] columns = this._TableColumn.GetsByFormId<TableColumnBase>(id);
            if ( columns.IsNull() )
            {
                return body;
            }
            TableGroupBase[] groups = null;
            if ( tables.IsExists(c => c.TableType == FormTableType.多行列表) )
            {
                groups = this._TableGroup.GetsByFormId<TableGroupBase>(id);
            }
            body.Tables.ForEach(a =>
            {
                TableColumnBase[] cols = columns.Where(c => c.TableId == a.Id).OrderBy(c => c.Sort).ToArray();
                if ( cols.Length == 0 )
                {
                    return;
                }
                else if ( a.TableType == FormTableType.多行列表 )
                {
                    TableGroupBase[] gList = groups.Where(c => c.TableId == a.Id).OrderBy(c => c.Sort).ToArray();
                    List<FormTableColumn> tList = new List<FormTableColumn>();
                    gList.ForEach(a =>
                    {
                        this._InitColumn(a, gList, cols, tList);
                    });
                    a.Columns = tList.ToArray();
                }
                else
                {
                    a.Columns = cols.ConvertAll(c => this._Format(c));
                }

            });
            return body;
        }
    }
}
