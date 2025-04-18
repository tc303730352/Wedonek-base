﻿using Base.FileRemoteModel;
using SqlSugar;

namespace Base.FileModel.UserFileDir
{
    public class UserFileDirDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 目录状态
        /// </summary>
        public FileDirStatus DirStatus { get; set; }

        /// <summary>
        /// 允许上传的文件大小
        /// </summary>
        public long? AllowUpFileSize { get; set; }

        /// <summary>
        /// 允许扩展名
        /// </summary>
        [SugarColumn(IsJson = true)]
        public string[] AllowExtension { get; set; }

        /// <summary>
        /// 上传说明
        /// </summary>
        public string UpShow { get; set; }

        /// <summary>
        /// 是否需要登陆
        /// </summary>
        public bool IsAccredit { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        [SugarColumn(IsJson = true)]
        public string[] UpPower { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        public FilePower Power { get; set; }

        [SugarColumn(IsJson = true)]
        public string[] ReadPower { get; set; }

        /// <summary>
        /// 上传图片的设置
        /// </summary>
        [SugarColumn(IsJson = true)]
        public UpImgSet UpImgSet { get; set; }
    }
}
