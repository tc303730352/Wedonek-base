﻿using Basic.FormCollect;
using Basic.FormModel.DB;
using Basic.FormRemoteModel.Column.Model;
using Basic.FormService.Interface;
using WeDonekRpc.Client;

namespace Basic.FormService.lmpl
{
    internal class TableColumnService : ITableColumnService
    {
        private readonly ITableColumnCollect _Column;

        public TableColumnService ( ITableColumnCollect column )
        {
            this._Column = column;
        }

        public long Add ( TableColumnAdd add )
        {
            return this._Column.Add(add);
        }


        public void Delete ( long id )
        {
            DBTableColumn col = this._Column.Get(id);
            this._Column.Delete(col);
        }

        public TableColumnDto Get ( long id )
        {
            DBTableColumn col = this._Column.Get(id);
            return col.ConvertMap<DBTableColumn, TableColumnDto>();
        }
        public bool Set ( long id, TableColumnSet set )
        {
            DBTableColumn col = this._Column.Get(id);
            set.EditControl ??= string.Empty;
            set.ShowControl ??= string.Empty;
            return this._Column.Set(col, set);
        }

        public bool SetColSpan ( long id, int span )
        {
            DBTableColumn col = this._Column.Get(id);
            return this._Column.SetColSpan(col, span);
        }
        public void SetColSpan ( KeyValuePair<long, int>[] span )
        {
            this._Column.SetColSpan(span);
        }
        public bool SetGroupId ( long id, long groupId )
        {
            DBTableColumn col = this._Column.Get(id);
            return this._Column.SetGroupId(col, groupId);
        }

        public void SetSort ( KeyValuePair<long, int>[] sort )
        {
            this._Column.SetSort(sort);
        }
    }
}
