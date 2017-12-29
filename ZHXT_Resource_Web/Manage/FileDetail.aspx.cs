using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage
{
    public partial class FileDetail : System.Web.UI.Page
    {
        ConvertToPDF toPdf = new ConvertToPDF();
        ConvertToSwf toSwf = new ConvertToSwf();
        public string AttachmentPath { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
             AttachmentPath = @"/Manage/ResourceInfo_Uploads/销售报表.xlsx";
            ConvertToPDF toPdf = new ConvertToPDF();
            ConvertToSwf toSwf = new ConvertToSwf();
            string path = Request.MapPath("~/Manage/ResourceInfo_Uploads/");
            string exe = "C:\\SWFTools\\pdf2swf.exe";
            string fileName = Path.GetFileNameWithoutExtension(AttachmentPath);
            byte[] bytes = Encoding.Default.GetBytes(fileName);
            string fileNameNew = Convert.ToBase64String(bytes);
            fileNameNew = fileNameNew.Replace("/", "_").Replace("\\", "_").Replace("<", "_").Replace(">", "_").Replace("?", "_").Replace("|", "_").Replace("*", "_").Replace("?", "_").Replace("+", "_").Replace("-", "_");
            string extensionName = Path.GetExtension(AttachmentPath).ToLower();
            switch (extensionName)
            {
                case ".avi":
                case ".mp4":
                case ".mov":
                    
                    break;
                case ".doc": 
                case ".docx":
                    #region
                   
                    if (System.IO.File.Exists(path + fileNameNew + ".swf"))
                    {
                        // courseRow.VideoType = VideoTypeEnum.Swf;
                        AttachmentPath = fileNameNew + ".swf";
                    }
                    else
                    {
                        bool result = toPdf.DOCConvertToPDF(path + fileName + extensionName, path + fileName + ".pdf");
                        if (result)
                        {
                            //   courseRow.VideoType = VideoTypeEnum.PDF;
                            toSwf.CallCMD(exe, path + fileName + ".pdf" + " -o " + path + fileNameNew + ".swf");
                            //courseRow.VideoType = VideoTypeEnum.Swf;
                            AttachmentPath = fileNameNew + ".swf";
                        }
                    }
                    #endregion
                    break;
                case ".ppt":
                case ".pptx":
                    #region
                 
                    if (System.IO.File.Exists(path + fileNameNew + ".swf"))
                    {
                        // courseRow.VideoType = VideoTypeEnum.Swf;
                        AttachmentPath = fileNameNew + ".swf";
                    }
                    else
                    {
                        bool result = toPdf.PPTConvertToPDF(path + fileName + extensionName, path + fileName + ".pdf");
                        if (result)
                        {
                            //courseRow.VideoType = VideoTypeEnum.PDF;
                            toSwf.CallCMD(exe, path + fileName + ".pdf" + " -o " + path + fileNameNew + ".swf");
                            // courseRow.VideoType = VideoTypeEnum.Swf;
                            AttachmentPath = fileNameNew + ".swf";
                        }
                    }
                    #endregion

                    break;
                case ".xls":
                case ".xlsx":
                    #region
                  
                    if (System.IO.File.Exists(path + fileNameNew + ".swf"))
                    { 
                        AttachmentPath = fileNameNew + ".swf";
                    }
                    else
                    {
                        bool result = toPdf.XLSConvertToPDF(path + fileName + extensionName, path + fileName + ".pdf");
                        if (result)
                        {
                            toSwf.CallCMD(exe, path + fileName + ".pdf" + " -o " + path + fileNameNew + ".swf");
                            AttachmentPath = fileNameNew + ".swf";
                        }
                    }
                    #endregion
                    break;
                default:
                   
                    break;
            }
        }
    }
}