using SqlSugar;

namespace Basic.HrModel.DB
{
    [SugarTable("OperateMenu")]
    public class DBOperateMenu
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public string RoutePath
        {
            get;
            set;
        }
        public string BusType
        {
            get;
            set;
        }
        public bool IsEnable
        {
            get;
            set;
        }
    }
}
