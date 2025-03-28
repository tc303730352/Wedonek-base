﻿using System.Collections.Specialized;
using Base.FileGateway.Helper;
using Base.FileModel.UserFile;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Img;
using WeDonekRpc.Modular;

namespace Base.FileGateway.Helper
{
    internal static class HelperLinq
    {
        public static ImgOperate GetImgOperate ( this NameValueCollection query )
        {
            ImgOperate op = new ImgOperate
            {
                Cut = new CutImg(),
            };
            foreach ( string i in query )
            {
                string val = query[i];
                if ( val.IsNull() )
                {
                    continue;
                }
                else if ( i == "width" )
                {
                    op.Width = int.Parse(query[i]);
                }
                else if ( i == "height" )
                {
                    op.Height = int.Parse(query[i]);
                }
                else if ( i == "rotate" )
                {
                    op.Rotate = int.Parse(query[i]);
                }
                else if ( i == "rotate" )
                {
                    op.Rotate = int.Parse(query[i]);
                }
                else if ( i == "cutx" )
                {
                    op.Cut.X = int.Parse(query[i]);
                }
                else if ( i == "cuty" )
                {
                    op.Cut.Y = int.Parse(query[i]);
                }
                else if ( i == "cutw" )
                {
                    op.Cut.Width = int.Parse(query[i]);
                }
                else if ( i == "cuth" )
                {
                    op.Cut.Height = int.Parse(query[i]);
                }
            }
            return op;
        }
        public static bool CheckIsNull ( this ImgOperate operate )
        {
            if ( operate.Rotate.HasValue )
            {
                return false;
            }
            else if ( operate.Cut != null && !operate.Cut.IsNull() )
            {
                return false;
            }
            return !operate.Width.HasValue && !operate.Height.HasValue;
        }
        public static string GetNewFileName ( this ImgOperate operate, UserFileDto file )
        {
            return string.Join("_", file.Id, operate.Rotate.GetValueOrDefault(),
                operate.Width.GetValueOrDefault(),
                operate.Height.GetValueOrDefault(),
                operate.Cut.Width,
                operate.Cut.Height,
                operate.Cut.X,
                operate.Cut.Y) + file.Extension;
        }
        public static bool IsNull ( this CutImg cut )
        {
            return cut.X == 0 && cut.Y == 0 && cut.Width == 0 && cut.Height == 0;
        }

        public static long ToUserId ( this IUserState state )
        {
            return state.GetValue<long>("UserId");
        }
        public static string ToUserType ( this IUserState state )
        {
            return state.GetValue<string>("UserType");
        }
    }
}
