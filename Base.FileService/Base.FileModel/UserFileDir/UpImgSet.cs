using WeDonekRpc.Helper.Validate;

namespace Base.FileModel.UserFileDir
{
    public class UpImgSet
    {
        /// <summary>
        /// 高宽比例
        /// </summary>
        [NumValidate("file.up.image.ratio.error", 1)]
        [LenValidate("file.up.image.ratio.len", 2, 2)]
        public int[] Ratio { get; set; }

        /// <summary>
        /// 最小宽度
        /// </summary>
        [NumValidate("file.up.image.min.width.error", 1)]
        public int? MinWidth { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        [NumValidate("file.up.image.max.width.error", 1)]
        public int? MaxWidth { get; set; }

        /// <summary>
        /// 最小高度
        /// </summary>
        [NumValidate("file.up.image.min.height.error", 1)]
        public int? MinHeight { get; set; }

        /// <summary>
        /// 最大高度    
        /// </summary>
        [NumValidate("file.up.image.max.height.error", 1)]
        public int? MaxHeight { get; set; }
    }
}
