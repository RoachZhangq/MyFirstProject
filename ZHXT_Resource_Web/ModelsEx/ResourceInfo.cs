using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace Models
{
    public partial class ResourceInfo
    {
        public string SubjectName { get; set; }
        public string CourseTypeName { get; set; }
        public string GradeName { get; set; }
        public string UserName { get; set; }

        public string CreationDateStr { get { return CreationDate.ToString(); } }

        public string PrintUrl
        {
            get
            {
                return "http://ow365.cn/?i="+ OfficeWeb365_Common.OfficeWeb365_ID + "&info=3&furl="+OfficeWeb365_Common.URLEncrypt(ZHXT_Resource_Web.Global.SiteUrl + this.FileNamePath, OfficeWeb365_Common.OfficeWeb365_IV, OfficeWeb365_Common.OfficeWeb365_Key);
            }
        }
    }
}