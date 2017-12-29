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
    /// ZipFiles 的摘要说明
    /// </summary>
    public class ZipFiles : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");

            try
            {
                //string filePath = context.Request["filePath"];
                //var filePathArr = filePath.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string resourceInfoObjList = context.Request["ResourceInfoObjList"];
                var ResourceInfoObjList = JsonConvert.DeserializeObject<List<ResourceInfoObj>>(resourceInfoObjList);

                string newFileDirectory = "/Manage/BatchDownload/" + Guid.NewGuid();
                foreach (var item in ResourceInfoObjList)
                {
                    string path = context.Server.MapPath(item.FilePath);
                    string destpath = newFileDirectory + "/" + item.Name;
                    destpath = context.Server.MapPath(destpath);
                    if (!Directory.Exists(destpath))
                    {
                        Directory.CreateDirectory(destpath);
                    }
                    destpath += "/" + item.FileName;
                    File.Copy(path, destpath);
                }
                if (ResourceInfoObjList.Count > 0) {
                    //压缩文件（.gz）
                    new ZipClass().ZipFileFromDirectory(context.Server.MapPath(newFileDirectory), context.Server.MapPath(newFileDirectory) + ".gz", 6);
                    result.message = newFileDirectory + ".gz";
                    result.result = true;
                    //删除原文件夹
                    if (Directory.Exists(context.Server.MapPath(newFileDirectory)))
                    {
                        DirectoryInfo di = new DirectoryInfo(context.Server.MapPath(newFileDirectory));
                        di.Delete(true);
                    }

                    //删除 24小时前创建的文件
                    DirectoryInfo TheFolder = new DirectoryInfo(context.Server.MapPath("/Manage/BatchDownload/"));
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                       var creationTime= NextFile.CreationTime;
                        var now = DateTime.Now.AddHours(-24);
                        //var now = DateTime.Now.AddMinutes(-3);
                        if (creationTime< now)
                        {
                            //此文件超过一天了 需要删除掉
                            NextFile.Delete();
                        }
                    }
                }
             
            }
            catch (Exception ex)
            {
                result.message = "发生错误：" + ex.Message;

            }
            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class ResourceInfoObj
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}