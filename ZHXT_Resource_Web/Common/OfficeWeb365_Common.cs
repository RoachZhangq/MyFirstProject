using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace ZHXT_Resource_Web.Common
{
    public class OfficeWeb365_Common
    {
        public static string OfficeWeb365_ID = System.Configuration.ConfigurationManager.AppSettings["OfficeWeb365_ID"];
        public static string OfficeWeb365_IV = System.Configuration.ConfigurationManager.AppSettings["OfficeWeb365_IV"];
        public static string OfficeWeb365_Key = System.Configuration.ConfigurationManager.AppSettings["OfficeWeb365_Key"];

        /// <summary>
        /// ==========URL DES加密,不支持大小等于号（英文状态下的）=============
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="IV">向量8位</param>
        /// <param name="Key">密钥8位</param>
        /// <returns></returns>
        public static String URLEncrypt(String str, string IV, string Key)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] bStr = Encoding.UTF8.GetBytes(str);
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray()).Replace('+', '_').Replace('/', '@');
            }
            catch (Exception e)
            {
                return "加密失败！" + e.Message;
            }
        }
        /// <summary>
        /// ===================== URL DES解密 ========================
        /// </summary>
        /// <param name="DecryptStr">要解密的字符串</param>
        /// <param name="IV">向量8位</param>
        /// <param name="Key">密钥8位</param>
        /// <returns></returns>
        public static String URLDecrypt(String DecryptStr, string IV, string Key)
        {
            DecryptStr = DecryptStr.Replace('_', '+').Replace('@', '/');
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Key);
                byte[] bIV = Encoding.UTF8.GetBytes(IV);
                byte[] bStr = Convert.FromBase64String(DecryptStr);
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception e)
            {
                return "解密失败！" + e.Message;
            }
        }

    }
}