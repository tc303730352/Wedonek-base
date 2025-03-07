namespace Base.FileRemoteModel.Down.Model
{
    public enum RequestDataType
    {
        None = 0,
        Form = 1,
        JSON = 2,
        XML = 3,
        Text = 4,
        Binary = 5,
    }
    public class DownFileParam
    {
        /// <summary>
        /// 方案名
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 关联的PK
        /// </summary>
        public long? LinkBizPk { get; set; }

        /// <summary>
        /// 目录Key
        /// </summary>
        public string DirKey { get; set; }

        /// <summary>
        /// 文件名带后缀
        /// </summary>
        public string FileName
        {
            get;
            set;
        }
        /// <summary>
        /// Post请求数据
        /// </summary>
        public string PostData
        {
            get;
            set;
        }
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求数据类型
        /// </summary>
        public RequestDataType DataType { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 请求配置(为空采用默认规则)
        /// </summary>
        public HttpRequestArg RequestSet { get; set; }
    }
}
