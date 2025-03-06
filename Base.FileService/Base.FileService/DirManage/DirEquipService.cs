using System.Collections.Concurrent;
using Base.FileService.DirManage.Model;
using WeDonekRpc.Helper;

namespace Base.FileService.DirManage
{
    internal class DirEquipService
    {
        private static readonly ConcurrentDictionary<string, EquipDevice> _Devices = new ConcurrentDictionary<string, EquipDevice>();
        private static Timer _refreshTimer;
        private readonly bool _IsInit = false;

        public static void Init (int interval)
        {
            _refreshTimer = new Timer(new TimerCallback(_Refresh), null, interval, interval);
        }
        private static EquipDevice _GetDevice (string name)
        {
            if (_Devices.TryGetValue(name, out EquipDevice dev))
            {
                return dev;
            }
            dev = new EquipDevice(name);
            _ = _Devices.TryAdd(name, dev);
            return dev;
        }
        private static void _Refresh (object state)
        {
            if (_Devices.Count == 0)
            {
                return;
            }
            string[] keys = _Devices.Keys.ToArray();
            keys.ForEach(c =>
            {
                if (_Devices.TryGetValue(c, out EquipDevice dev))
                {
                    dev.Refresh();
                }
            });
        }

        public static bool CheckIsUse (string name, long fileSize)
        {
            return _GetDevice(name).CheckIsUse(fileSize);
        }

        public void Dispose ()
        {
            if (this._IsInit)
            {
                _refreshTimer.Dispose();
            }
        }
    }
}
