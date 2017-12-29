using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script;
using System.Web.SessionState;
using ZHXT_Resource_Web.Common;
using Models;
using Newtonsoft.Json;
using SqlSugar;
using ZHXT_Resource_Web.ModelsEx;
using ZHXT_Resource_Web.Dao;
using ZHXT_Resource_Web.Bll;
using System.Text;
using System.IO;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// Pretreatment 的摘要说明
    /// </summary>
    public class Pretreatment : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(true, "");
            string id = context.Request["id"];
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                if (!string.IsNullOrEmpty(id))
                    result.message = Check_Handler(id, user, context);
            }
            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        private string Check_Handler(string id, User user, HttpContext context)
        {
            string url = "";
            var model = new ResourceInfoBll().GetResourceInfoById(Convert.ToInt32(id));
            string extensionName = Path.GetExtension(model.FileNamePath).ToLower();
            switch (extensionName)
            {
                case ".pdf":
                case ".doc":
                case ".docx":
                case ".ppt":
                case ".pptx":
                case ".xls":
                case ".xlsx":
                case ".zip ":
                case ".rar":
                case ".7z":
                case ".txt":
                    url = "/Manage/OfficeWeb365?id=" + id;
                    break;
                case ".mp4":
                case ".flv":
                case ".ogg":
                case ".mp3":
                case ".swf":
                    url = "/Manage/VideoPage?id=" + id;
                    break;
                //case ".swf":
                //    url = "/Manage/FlexPaperView/Index?id=" + id;
                //    break;
                default:
                    url = "javascript:alert('此类型占时不能查看！')";
                    break;
            }
            //添加访问数
            ResourceRecord resourceRecord = new ResourceRecord();
            resourceRecord.Type = (int)ResourceRecordTypeEnum.Select ;
            resourceRecord.ResourceInfoID = Convert.ToInt32(id);
            resourceRecord.DisplayIndex = 0;
            resourceRecord.Disabled = false;
            resourceRecord.CreationDate = DateTime.Now;
            resourceRecord.OwnerID = user.ID;
            resourceRecord.Remark = resourceRecord.CreationDate+",用户(" +user.Name+")查看了资源《"+model.Name+"》";
            new Bll.ResourceRecordBll().AddResourceRecord(resourceRecord);
            return url;
        }
        #region ********不用这种方式了***********
        /// <summary>
        /// 检查文件类型 生成pdf ********不用这种方式了***********
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private ReturnModel Check(string id, HttpContext context)
        {
            ReturnModel returnModel = new ReturnModel();
            var model = new ResourceInfoBll().GetResourceInfoById(Convert.ToInt32(id));
            string  resourceFilePath = model.FileNamePath;
            ConvertToPDF toPdf = new ConvertToPDF();
            ConvertToSwf toSwf = new ConvertToSwf();
            string path = context.Request.MapPath("~/Manage/ResourceInfo_Uploads/");
            //string exe = "C:\\SWFTools\\pdf2swf.exe";
            string fileName = Path.GetFileNameWithoutExtension(resourceFilePath);
            byte[] bytes = Encoding.Default.GetBytes(fileName);
            string fileNameNew = Convert.ToBase64String(bytes);
            fileNameNew = fileNameNew.Replace("/", "_").Replace("\\", "_").Replace("<", "_").Replace(">", "_").Replace("?", "_").Replace("|", "_").Replace("*", "_").Replace("?", "_").Replace("+", "_").Replace("-", "_");
            string extensionName = Path.GetExtension(resourceFilePath).ToLower();

            string resourceInfo_Uploads = "/Manage/ResourceInfo_Uploads/";
            string resourceFileDetail_PDF = "/Manage/ResourceFileDetail_PDF";
            switch (extensionName)
            {
                case ".avi":
                case ".mp4":
                case ".mov":

                    break;
                case ".doc":
                case ".docx":
                    #region

                    if (!System.IO.File.Exists(path + fileNameNew + ".pdf"))
                    { 
                        bool result = toPdf.DOCConvertToPDF(path + fileName + extensionName, path + fileName + ".pdf");
                    }
                    resourceFilePath = resourceInfo_Uploads + fileNameNew + ".pdf";
                    returnModel.pagePath = resourceFileDetail_PDF+"?id=" + id;
                    //returnModel.filePath = resourceFilePath;
                    #endregion
                    break;
                case ".ppt":
                case ".pptx":
                    #region
                    if (!System.IO.File.Exists(path + fileNameNew + ".pdf"))
                    {
                        bool result = toPdf.PPTConvertToPDF(path + fileName + extensionName, path + fileName + ".pdf");
                    }
                    resourceFilePath = resourceInfo_Uploads + fileNameNew + ".pdf";
                    returnModel.pagePath = resourceFileDetail_PDF + "?id=" + id;
                   // returnModel.filePath = resourceFilePath;
                    #endregion

                    break;
                case ".xls":
                case ".xlsx":
                    #region
                    if (!System.IO.File.Exists(path + fileNameNew + ".pdf"))
                    {
                        bool result = toPdf.XLSConvertToPDF(path + fileName + extensionName, path + fileName + ".pdf");
                    }
                    resourceFilePath = resourceInfo_Uploads + fileNameNew + ".pdf";
                    returnModel.pagePath = resourceFileDetail_PDF + "?id=" + id;
                    //returnModel.filePath = resourceFilePath;
                    #endregion
                    break;
                case ".pdf":
                    #region
                  
                    returnModel.pagePath = resourceFileDetail_PDF + "?id=" + id;
                    //returnModel.filePath = resourceFilePath;
                    #endregion
                    break;
                default: 
                    break;
            }


            return returnModel;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class ReturnModel
    {
        //public string filePath { get; set; }
        public string pagePath { get; set; }
    }
}