using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class DownloadFilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Request["path"] ?? "";
            string name = HttpUtility.UrlDecode ( Request["name"] ?? "");
            string ids = HttpUtility.UrlDecode(Request["ids"]);
            var arr= ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var user = Session[Global.Session_ManagerUser] as User;
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(name)&&user != null)
            {
                foreach (var item in arr)
                {
                    //添加下载数
                    ResourceRecord resourceRecord = new ResourceRecord();
                    resourceRecord.Type = (int)Common.ResourceRecordTypeEnum.Download;
                    resourceRecord.ResourceInfoID = Convert.ToInt32(item);
                    resourceRecord.DisplayIndex = 0;
                    resourceRecord.Disabled = false;
                    resourceRecord.CreationDate = DateTime.Now;
                    resourceRecord.OwnerID = user.ID;
                    resourceRecord.Remark = resourceRecord.CreationDate + ",用户(" + user.Name + ")下载了资源";
                    new Bll.ResourceRecordBll().AddResourceRecord(resourceRecord);
                }
                name = System.Web.HttpUtility.UrlDecode(name);
                DownloadFileFunc(Server.MapPath(path), name);
            }
        }

        public void DownloadFileFunc(string path, string name)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(name));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/ms-excel";
                // 把文件流发送到客户端
                Response.WriteFile(file.FullName);
                // 停止页面的执行
                //Response.End();     
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('系统出现以下错误://n" + ex.Message + "!//n请尽快与管理员联系.')</script>");
            }
        }
    }
}