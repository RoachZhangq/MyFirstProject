using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ZHXT_Resource_Web.Common
{
    public static class Extensions
    {
        #region 根据一个字符串生成一个序列

        public static DateTime CurrDate = DateTime.Now;
        private static int Seq = 1;
        private const string SeqFormat = "yyMMddHHmmss";

        public static string Sequence(this string flag)
        {
            if (CurrDate.ToString(SeqFormat) != DateTime.Now.ToString(SeqFormat))
            {
                Seq = 1;
                CurrDate = DateTime.Now;
            }
            string order = string.Format("{0}{1}{2}", flag, DateTime.Now.ToString(SeqFormat), (Seq++).ToString("D2"));
            CurrDate = DateTime.Now;
            return order;
        }
        #endregion

        #region 提供标准 MD5 加密的方法
        /// <summary>
        /// 提供标准 MD5 加密的方法
        /// </summary>
        /// <param name="cort"></param>
        /// <returns></returns>
        public static string Ext_GetStandMD5(this string cort)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.GetEncoding("gbk").GetBytes(cort);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = string.Empty;
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }
        #endregion

        #region 提供 Base64 MD5 加密的方法
        /// <summary>
        /// 提供 Base64 MD5 加密的方法，Base64 MD5 加密为不可逆算法
        /// </summary>
        /// <param name="cort"> 需要加密的字符串（string） </param>
        /// <returns> 加密后的字符串（string） </returns>
        public static string Ext_GetEncryptInMD5(this string cort)
        {
            string recort = cort;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(cort.ToCharArray());   // 获得要加密的字段，并且转化为 byte[] 
            MD5 md5 = new MD5CryptoServiceProvider();   // 实例一个加密的对象
            byte[] Result = md5.ComputeHash(data);
            recort = Convert.ToBase64String(Result);    // 将加密后的数组转化为字段 

            return recort;
        }
        #endregion

        #region 加密字符串
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <returns></returns>
        public static string Ext_GetEncrypt(this string pToEncrypt)
        {
            byte[] desKey = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };
            byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                //把字符串放到byte数组中
                //原来使用的UTF8编码，我改成Unicode编码了，不行
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                //byte[] inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量
                //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
                //使得输入密码必须输入英文文本
                des.Key = desKey;       // ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = desIV;         //ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(),
                    CryptoStreamMode.Write);
                //Write the byte array into the crypto stream
                //(It will end up in the memory stream)
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //Get the data back from the memory stream, and into a string
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    //Format as hex
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch
            {
                return pToEncrypt;
            }
            finally
            {
                des = null;
            }
        }
        #endregion

        #region 解密字符串
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        public static string Ext_GetDecrypt(this string pToDecrypt)
        {
            byte[] desKey = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };
            byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                //Put the input string into the byte array
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = desKey;           //ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = desIV;             //ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                //Flush the data through the crypto stream into the memory stream
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //Get the decrypted data back from the memory stream
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();

                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return pToDecrypt;
            }
            finally
            {
                des = null;
            }
        }
        #endregion

        #region 正则过滤html标记
        /// <summary>
        /// 正则过滤html标记
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string NoHTML(this string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", string.Empty, RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", string.Empty, RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", string.Empty, RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", string.Empty, RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", string.Empty);
            Htmlstring.Replace(">", string.Empty);
            Htmlstring.Replace("\r\n", string.Empty);
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }
        #endregion

        #region 验证并获取Url字符串中的Page为正整数页码
        /// <summary>
        /// 验证并获取Url字符串中的Page为正整数页码
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int IsPageNumber(this string page)
        {
            if (page == null) return 1;

            page = page.Trim();
            if (!Regex.IsMatch(page, @"^[0-9]+$")) return 1;
            if (Convert.ToInt32(page) < 1) return 1;

            return int.Parse(page);

        }
        #endregion

         

        #region 判断是否为数字 IsNum(string s)
        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsNum(this string s)
        {
            return Regex.IsMatch(s, @"^[0-9]+$");
        }
        #endregion

        #region 去掉 NameValueCollection 中的传入键
        /// <summary>
        /// 去掉 NameValueCollection 中的传入键，注意返回值第一个字母包括&符号
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public static string RemoveQueryString(this NameValueCollection QueryString, string Keys)
        {
            string[] strs = { Keys };
            return RemoveQueryString(QueryString, strs);
        }
        /// <summary>
        /// 去掉 NameValueCollection 中的传入键，注意返回值第一个字母包括&符号
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public static string RemoveQueryString(this NameValueCollection QueryString, string[] Keys)
        {
            return RemoveQueryString(QueryString, new List<string>(Keys));
        }
        /// <summary>
        /// 去掉 NameValueCollection 中的传入键，注意返回值第一个字母包括&符号
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public static string RemoveQueryString(this NameValueCollection QueryString, List<string> Keys)
        {
            string retval = string.Empty;
            for (int i = 0; i < QueryString.Count; i++)
            {
                if (Keys.Contains(QueryString.GetKey(i))) continue;
                retval += string.Format("&{0}={1}", QueryString.GetKey(i), QueryString.Get(i));
            }
            return retval;
        }
        #endregion

        #region 过滤脚本注入
        /// <summary>
        /// 过滤脚本注入
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string NoScript(this string content)
        {
            return content.ToLower().Replace("<script", "&lt;script").Replace("</script", "&lt;/script");
        }
        #endregion

        #region 生成缩略图
        ///<summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(this string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）  
                    break;
                case "W"://指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch
            {

            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region 根据出生日期获取年龄
        /// <summary>
        /// 根据出生日期获取年龄
        /// </summary>
        /// <param name="strBirthday">传入出生日期</param>
        /// <returns>返回年龄</returns>
        public static string getAge(string strBirthday)
        {
            string _age = "";   //年龄

            int _differYear;    //是否满年

            //判断传入的出生日期是否为空
            if (!string.IsNullOrEmpty(strBirthday))
            {
                try
                {
                    //将传入的出生日期转换为日期格式
                    DateTime _Birthday = Convert.ToDateTime(strBirthday);

                    //精确判断(精确到天数)
                    if (System.DateTime.Now.Month >= _Birthday.Month)
                    {
                        if (System.DateTime.Now.Day >= _Birthday.Day)
                        {
                            _differYear = 0;
                        }
                        else
                        {
                            _differYear = 1;
                        }
                    }
                    else
                    {
                        _differYear = 1;
                    }

                    //结果
                    _age = (DateTime.Now.Year - _Birthday.Year - _differYear).ToString();

                }
                catch
                {
                    return "Error";
                }
            }
            else
            {
                _age = "参数错误";
            }

            return _age;
        }
        #endregion

        #region 发送 http/post 请求到
        /// <summary>
        /// 发送 http/post 请求到
        /// </summary>
        /// <param name="url"></param>
        /// <param name="prams">参数集合</param>
        /// <returns>返回服务端相应的字符串</returns>
        public static string SendHttpPost(this string url, NameValueCollection data)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Headers.Add("Accept-Language", "zh-cn");
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] bytReturn = wc.UploadValues(url, "post", data);
            wc.Dispose();
            return System.Text.Encoding.GetEncoding("gbk").GetString(bytReturn);
        }
        #endregion
    }
}