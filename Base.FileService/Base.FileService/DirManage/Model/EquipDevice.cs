namespace Base.FileService.DirManage.Model
{
    internal class EquipDevice
    {
        private readonly DriveInfo _Drive = null;
        private volatile bool _IsReady = false;
        private long _Surplus = 0;

        public bool IsReady { get => _IsReady; }

        public EquipDevice(string name)
        {
            _Drive = new DriveInfo(name);
            _IsReady = _Drive.IsReady;
            if (_Drive.IsReady)
            {
                _Surplus = _Drive.AvailableFreeSpace;
            }
        }
        public void Refresh()
        {
            _IsReady = _Drive.IsReady;
            if (_Drive.IsReady)
            {
                Interlocked.Exchange(ref _Surplus, _Drive.AvailableFreeSpace);
            }
        }
        public bool CheckIsUse(long fileSize)
        {
            long sur = Interlocked.CompareExchange(ref _Surplus, 0, 0);
            return _IsReady && fileSize <= sur;
        }
    }
}
