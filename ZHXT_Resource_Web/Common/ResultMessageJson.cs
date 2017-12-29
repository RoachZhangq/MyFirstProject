using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace ZHXT_Resource_Web.Common
{
    public class ResultMessageJson
    {
        public ResultMessageJson()
        {
            result = false;
            message = "";
        }
        public ResultMessageJson(bool result, string mes)
        {
            this.result = result;
            this.message = mes;
        }
        public bool result { get; set; }
        public string message { set; get; }
        public string messageEx { set; get; }
        public object list { get; set; }
    }
    public class ResultMessageJsonEx
    { 
        public bool result { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public int totalPage { get; set; }
        public object list { get; set; }
        public string message { get; set; }
    }
    public class CommonFunction
    {

        //public static string  Geta(string path)
        //{
        //    string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
        //    byte[] bytes = Encoding.Default.GetBytes(fileName);
        //    string fileNameNew = Convert.ToBase64String(bytes);
        //    fileNameNew = fileNameNew.Replace("/", "_").Replace("\\", "_").Replace("<", "_").Replace(">", "_").Replace("?", "_").Replace("|", "_").Replace("*", "_").Replace("?", "_").Replace("+", "_").Replace("-", "_");

        //}

        /// <summary>
        ///     过滤htnl标签
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetNoHtmlString(string text)
        {
            //删除脚本
            text = Regex.Replace(text, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            text = Regex.Replace(text, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"-->", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<!--.*", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            text.Replace("<", "");
            text.Replace(">", "");
            text.Replace("\r\n", "");
            text = HttpContext.Current.Server.HtmlEncode(text).Trim();
            return text;
        }
        /// <summary>
        ///     随机名字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetRandomName(string str = "")
        {
            var random = new Random();
            System.Threading.Thread.Sleep(100);
            string change = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + random.Next(100000, 999999);
            return str + change;
        }
        /// <summary>
        ///     根据值查找XMl文件对应文本
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string GetXmlTextByValue(string value, string path, HttpServerUtility server)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(server.MapPath(path));
            if (xmlDoc.DocumentElement != null)
            {
                XmlNodeList elemList = xmlDoc.DocumentElement.SelectNodes("Item");
                if (elemList != null && elemList.Count > 0)
                {
                    foreach (XmlNode item in elemList)
                    {
                        if (item.Attributes != null)
                        {
                            string attValue = item.Attributes["Text"].InnerXml;
                            if (attValue == value)
                            {
                                string str = item.Attributes["Value"].InnerXml;
                                return str;
                            }
                        }
                    }
                }
            }
            return "";
        }
        /// <summary>
        ///     根据值查找XMl文件对应文本
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string GetXmlValueByText(string value, string path, HttpServerUtility server)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(server.MapPath(path));
            if (xmlDoc.DocumentElement != null)
            {
                XmlNodeList elemList = xmlDoc.DocumentElement.SelectNodes("Item");
                if (elemList != null && elemList.Count > 0)
                {
                    foreach (XmlNode item in elemList)
                    {
                        if (item.Attributes != null)
                        {
                            string attValue = item.Attributes["Value"].InnerXml;
                            if (attValue == value)
                            {
                                string str = item.Attributes["Text"].InnerXml;
                                return str;
                            }
                        }
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }




        /// <summary>
        ///     URL路径解码
        /// </summary>
        /// <returns></returns>
        public static string UrlDecode(string strHtml)
        {
            return HttpUtility.UrlDecode(strHtml, Encoding.Default);
        }

        /// <summary>
        ///     URL路径编码
        /// </summary>
        /// <returns></returns>
        public static string UrlEncode(string strHtml)
        {
            return HttpUtility.UrlEncode(strHtml, Encoding.Default);
        }



    }
}