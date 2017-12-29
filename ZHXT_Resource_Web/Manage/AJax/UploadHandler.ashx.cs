using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            UploadResult result = new UploadResult();
            HttpPostedFile file = context.Request.Files["file_upload"]; 
            

            string folder = context.Server.MapPath("~/Manage/ResourceInfo_Uploads/");
            if (file != null && file.ContentLength > 0)
            {
                string OldFileName = file.FileName;
              
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
          
                string NewFileName = CommonFunction.GetRandomName("ZHXT_") + System.IO.Path.GetExtension(OldFileName);

                result.Status = true;
                result.Message = OldFileName;
                result.Message2 = NewFileName;
                result.ContentLength = file.ContentLength; 
                file.SaveAs(Path.Combine(folder, NewFileName));
            }
            else
            {
                result.Status = false;
                result.Message = "请选择上传文件";
            }
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            context.Response.Write(json);
        }


        public class UploadResult
        {
            public bool Status;
            public string Message;
            public string Message2;
            public float ContentLength;
        }

        public bool IsReusable
        {
            get { return false; }
        }



    }
}