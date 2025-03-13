using Base.FileDAL.Model;
using Base.FileModel.DB;
using Base.FileModel.UserFile;
using Base.FileRemoteModel;
using SqlSugar;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Base.FileDAL.Repository
{
    internal class UserFileDAL : BasicDAL<DBUserFileList, long>, IUserFileDAL
    {
        public UserFileDAL ( IRepository<DBUserFileList> basicDAL ) : base(basicDAL)
        {
        }
        public UserFileBasic[] GetFiles ( long dirId, long linkBizPk, string tag )
        {
            if ( tag.IsNull() )
            {
                return this._BasicDAL.Gets<UserFileBasic>(a => a.UserDirId == dirId && a.LinkBizPk == linkBizPk && a.FileStatus == UserFileStatus.正常);
            }
            return this._BasicDAL.Gets<UserFileBasic>(a => a.UserDirId == dirId && a.LinkBizPk == linkBizPk && a.Tag == tag && a.FileStatus == UserFileStatus.正常);
        }
        public void Add ( DBUserFileList add )
        {
            add.Id = IdentityHelper.CreateId();
            add.AddTime = DateTime.Now;
            this._BasicDAL.Insert(add);
        }
        public void SaveFile ( Dictionary<long, int> sorts )
        {
            ISqlQueue<DBUserFileList> queue = this._BasicDAL.BeginQueue();
            sorts.ForEach(( id, sort ) =>
            {
                queue.UpdateOneColumn(a => a.Sort == sort, a => a.Id == id);
            });
            _ = queue.Submit();
        }
        public void SaveFile ( long fileId, long linkBizPk, long[] dropId )
        {
            if ( dropId.IsNull() )
            {
                this._SaveFile(fileId, linkBizPk);
                return;
            }
            ISqlQueue<DBUserFileList> queue = this._BasicDAL.BeginQueue();
            queue.Update(a => new DBUserFileList
            {
                FileStatus = UserFileStatus.正常,
                LinkBizPk = linkBizPk
            }, a => a.FileId == fileId);
            queue.UpdateOneColumn(a => a.FileStatus == UserFileStatus.已删除, c => dropId.Contains(c.Id));
            _ = queue.Submit();
        }
        private void _SaveFile ( long fileId, long linkBizPk )
        {
            if ( !this._BasicDAL.Update(a => new DBUserFileList
            {
                FileStatus = UserFileStatus.正常,
                LinkBizPk = linkBizPk
            }, a => a.Id == fileId) )
            {
                throw new ErrorException("file.update.fail");
            }
        }
        public void SaveFile ( long[] fileId, long linkBizPk, long[] dropId )
        {
            if ( dropId.IsNull() )
            {
                this._SaveFile(fileId, linkBizPk);
                return;
            }
            ISqlQueue<DBUserFileList> queue = this._BasicDAL.BeginQueue();
            queue.Update(a => new DBUserFileList
            {
                FileId = a.FileId,
                FileStatus = UserFileStatus.正常,
                LinkBizPk = linkBizPk
            }, a => fileId.Contains(a.Id) && a.FileStatus == UserFileStatus.起草);
            queue.UpdateOneColumn(a => a.FileStatus == UserFileStatus.已删除, c => dropId.Contains(c.Id));
            _ = queue.Submit();
        }
        private void _SaveFile ( long[] fileId, long linkBizPk )
        {
            if ( !this._BasicDAL.Update(a => new DBUserFileList
            {
                FileStatus = UserFileStatus.正常,
                LinkBizPk = linkBizPk
            }, a => fileId.Contains(a.Id) && a.FileStatus == UserFileStatus.起草) )
            {
                throw new ErrorException("file.update.fail");
            }
        }
        public UserFileDto GetFile ( long id )
        {
            TUserFileDto dto = this._BasicDAL.JoinGet<DBFileList, TUserFileDto>(( a, b ) => b.Id == a.FileId && a.Id == id, ( a, b ) => new TUserFileDto
            {
                Id = a.Id,
                FileStatus = a.FileStatus,
                FileName = b.FileName,
                Power = a.Power,
                Extension = b.Extension,
                PowerCode = a.PowerCode,
                UserId = a.UserId,
                DirPath = b.DirPath,
                LocalPath = b.LocalPath,
                ETag = b.Etag,
                SaveTime = b.SaveTime
            });
            if ( dto == null )
            {
                return null;
            }
            UserFileDto file = dto.ConvertMap<TUserFileDto, UserFileDto>();
            file.FilePath = Path.Combine(dto.DirPath, dto.LocalPath);
            return file;
        }

        public void Drop ( long[] ids )
        {
            if ( !this._BasicDAL.Update(a => a.FileStatus == UserFileStatus.已删除, a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("file.drop.fail");
            }
        }
        public void Drop ( long id )
        {
            if ( !this._BasicDAL.Update(a => a.FileStatus == UserFileStatus.已删除, a => a.Id == id) )
            {
                throw new ErrorException("file.drop.fail");
            }
        }
        public void Delete ( long id )
        {
            if ( !this._BasicDAL.Delete(a => a.Id == id) )
            {
                throw new ErrorException("file.delete.fail");
            }
        }
        public long[] GetIds ( long[] dirId, long[] linkBizPk )
        {
            return this._BasicDAL.Gets(a => dirId.Contains(a.UserDirId) && linkBizPk.Contains(a.LinkBizPk.Value) && a.FileStatus == UserFileStatus.正常, c => c.Id);
        }
        public long[] GetIds ( long dirId, long linkBizPk, string tag )
        {
            if ( tag.IsNull() )
            {
                return this._BasicDAL.Gets(a => a.UserDirId == dirId && a.LinkBizPk == linkBizPk && a.FileStatus == UserFileStatus.正常, c => c.Id);
            }
            return this._BasicDAL.Gets(a => a.UserDirId == dirId && a.LinkBizPk == linkBizPk && a.Tag == tag && a.FileStatus == UserFileStatus.正常, c => c.Id);
        }

        public Dictionary<long, int> GetFileNum ( long[] fileId )
        {
            return this._BasicDAL.GroupBy(a => fileId.Contains(a.FileId), a => a.FileId, a => new
            {
                a.FileId,
                num = SqlFunc.AggregateCount(a.FileId)
            }).ToDictionary(a => a.FileId, a => a.num);
        }
    }
}
