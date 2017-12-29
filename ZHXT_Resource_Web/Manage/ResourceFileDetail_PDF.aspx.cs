using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZHXT_Resource_Web.Bll;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage
{
    public partial class ResourceFileDetail_PDF : System.Web.UI.Page
    {
        public string FileUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["id"];
            var model = new ResourceInfoBll().GetResourceInfoById(Convert.ToInt32(id));

            string fileName = Path.GetFileNameWithoutExtension(model.FileNamePath);
            //byte[] bytes = Encoding.Default.GetBytes(fileName);
            //string fileNameNew = Convert.ToBase64String(bytes);
            //fileNameNew = fileNameNew.Replace("/", "_").Replace("\\", "_").Replace("<", "_").Replace(">", "_").Replace("?", "_").Replace("|", "_").Replace("*", "_").Replace("?", "_").Replace("+", "_").Replace("-", "_");

            FileUrl =EncryptUtil.Des( model.FileNamePath, OfficeWeb365_Common.OfficeWeb365_IV, OfficeWeb365_Common.OfficeWeb365_Key);
        }
    }
}