using System.Collections.Concurrent;
using Base.FileCollect;
using Base.FileModel.PhysicalDir;
using Base.FileService.Interface;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Client.Ioc;
using WeDonekRpc.Helper;

namespace Base.FileService.DirManage
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class DirService : IDirService
    {
        private static readonly ConcurrentDictionary<long, IDirState> _Dir = new ConcurrentDictionary<long, IDirState>();
        private readonly IIocService _Ioc;
        private readonly IFileConfig _Config;
        private IDirState[] _Dirs;
        private uint _MaxIndex;
        private uint _Index = 0;

        private long _ServerId;

        private Timer _RefreshDir;

        public DirService ( IIocService ioc, IFileConfig config )
        {
            this._Config = config;
            this._Dirs = new IDirState[0];
            this._Ioc = ioc;
        }

        public void Init ( long serverId )
        {
            this._ServerId = serverId;
            DirEquipService.Init(this._Config.EquipRefreshInterval);
            this._RefreshDir = new Timer(this._Init, null, 200, this._Config.DirRefreshInterval);
        }
        public IDirState GetDir ( long id )
        {
            if ( !_Dir.TryGetValue(id, out IDirState state) )
            {
                throw new ErrorException("file.dir.not.find");
            }
            return state;
        }
        private void _Init ( object? state )
        {
            using ( IocScope scope = this._Ioc.CreateTempScore() )
            {
                this._Load(scope);
            }
        }

        private void _Load ( IocScope scope )
        {
            IPhysicalDirCollect dir = scope.Resolve<IPhysicalDirCollect>();
            PhysicalDirDto[] dirs = dir.Gets(this._ServerId);
            if ( dirs.IsNull() )
            {
                if ( this._Dirs.Length != 0 )
                {
                    this._Dirs = new IDirState[0];
                    _Dir.Clear();
                }
                return;
            }
            if ( !_Dir.IsEmpty )
            {
                long[] keys = dirs.ConvertAll(c => c.Id);
                keys = _Dir.Keys.Where(c => !keys.Contains(c)).ToArray();
                if ( keys.Length > 0 )
                {
                    keys.ForEach(c =>
                    {
                        if ( _Dir.TryRemove(c, out IDirState state) )
                        {
                            state.Dispose();
                        }
                    });
                }
            }
            List<IDirState> ids = [];
            dirs.ForEach(c =>
            {
                if ( _Dir.TryGetValue(c.Id, out IDirState state) )
                {
                    state.Refresh(c);
                }
                else
                {
                    state = _Dir.GetOrAdd(c.Id, new DirStateMonitor(c));
                }
                if ( !state.IsOnlyRead )
                {
                    c.Weight.For(a =>
                    {
                        ids.Add(state);
                    });
                }
            });
            this._Dirs = ids.Random();
            this._MaxIndex = (uint)ids.Count;
        }
        public IDirState GetTempDir ()
        {
            uint size = this._MaxIndex;
            return this._GetDir(this._Dirs, ref size);
        }
        public IDirState GetDir ( ref long fileSize )
        {
            uint size = this._MaxIndex;
            return this._GetDir(this._Dirs, ref size, ref fileSize);
        }
        private IDirState _GetDir ( IDirState[] dirs, ref uint size )
        {
            uint index = Interlocked.Increment(ref this._Index) % size;
            IDirState dir = dirs[index];
            if ( dir.CheckIsUse() )
            {
                return dir;
            }
            foreach ( IDirState c in dirs )
            {
                if ( c.Id != dir.Id && c.CheckIsUse() )
                {
                    return c;
                }
            }
            throw new ErrorException("file.dir.no.config");
        }
        private IDirState _GetDir ( IDirState[] dirs, ref uint size, ref long fileSize )
        {
            uint index = Interlocked.Increment(ref this._Index) % size;
            IDirState dir = dirs[index];
            if ( dir.Distribution(fileSize) )
            {
                return dir;
            }
            foreach ( IDirState c in dirs )
            {
                if ( c.Id != dir.Id && c.Distribution(fileSize) )
                {
                    return c;
                }
            }
            throw new ErrorException("file.dir.no.config");
        }
    }
}
