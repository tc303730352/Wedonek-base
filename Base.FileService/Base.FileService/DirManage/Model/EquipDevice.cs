namespace Base.FileService.DirManage.Model
{
    internal class EquipDevice
    {
        private readonly DriveInfo _Drive = null;
        private volatile bool _IsReady = false;
        private long _Surplus = 0;

        public bool IsReady => this._IsReady;

        public EquipDevice ( string name )
        {
            this._Drive = new DriveInfo(name);
            this._IsReady = this._Drive.IsReady;
            if ( this._Drive.IsReady )
            {
                this._Surplus = this._Drive.AvailableFreeSpace;
            }
        }
        public void Refresh ()
        {
            this._IsReady = this._Drive.IsReady;
            if ( this._Drive.IsReady )
            {
                _ = Interlocked.Exchange(ref this._Surplus, this._Drive.AvailableFreeSpace);
            }
        }
        public bool CheckIsUse ( long fileSize )
        {
            long sur = Interlocked.CompareExchange(ref this._Surplus, 0, 0);
            return this._IsReady && fileSize <= sur;
        }
        public bool CheckIsUse ()
        {
            return this._IsReady && Interlocked.Read(ref this._Surplus) > 0;
        }
    }
}
