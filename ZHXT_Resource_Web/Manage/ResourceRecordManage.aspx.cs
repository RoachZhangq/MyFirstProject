using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class ResourceRecordManage : System.Web.UI.Page
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ID = Request["id"];
            Type = Request["type"];
            TypeName = Type == "select" ? "查看" : "下载";
        }
    }
}