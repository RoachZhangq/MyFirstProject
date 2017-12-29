using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using System.Diagnostics;
using System.IO;
namespace ZHXT_Resource_Web.Common
{
    public class ConvertToPDF
    {
        private static readonly object wordObject = new object();
        private static readonly object excelObject = new object();
        private static readonly object pptObject = new object();
        //private static ConvertToPDF singleton;
        //private static readonly object syncObject = new object();
        //private ConvertToPDF()
        //{ }
        //public static ConvertToPDF GetInstance()
        //{
        //    //这里可以保证只实例化一次
        //    //即在第一次调用时实例化
        //    //以后调用便不会再实例化
        //    //第一重 singleton == null
        //    if (singleton == null)
        //    {
        //        lock (syncObject)
        //        {
        //            //第二重 singleton == null
        //            if (singleton == null)
        //            {
        //                singleton = new ConvertToPDF();
        //            }
        //        }
        //    }
        //    return singleton;
        //}
        ///<summary>
        /// 把Word文件转换成为PDF格式文件
        ///</summary>
        ///<param name="sourcePath">源文件路径</param>
        ///<param name="targetPath">目标文件路径</param> 
        ///<returns>true=转换成功</returns>
        public bool DOCConvertToPDF(string sourcePath, string targetPath)
        {
            lock (wordObject)
            {
                bool result = false;
                Word.WdExportFormat exportFormat = Word.WdExportFormat.wdExportFormatPDF;
                object paramMissing = Type.Missing;
                Word.Application wordApplication = new Word.Application();
                Word.Document wordDocument = null;
                try
                {
                    object paramSourceDocPath = sourcePath;
                    string paramExportFilePath = targetPath;
                    Word.WdExportFormat paramExportFormat = exportFormat;
                    bool paramOpenAfterExport = false;
                    Word.WdExportOptimizeFor paramExportOptimizeFor = Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                    Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                    int paramStartPage = 0;
                    int paramEndPage = 0;
                    Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                    bool paramIncludeDocProps = true;
                    bool paramKeepIRM = true;
                    Word.WdExportCreateBookmarks paramCreateBookmarks = Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                    bool paramDocStructureTags = true;
                    bool paramBitmapMissingFonts = true;
                    bool paramUseISO19005_1 = false;
                    wordDocument = wordApplication.Documents.Open(
                    ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing);
                    if (wordDocument != null)
                        wordDocument.ExportAsFixedFormat(paramExportFilePath,
                        paramExportFormat, paramOpenAfterExport,
                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                        paramEndPage, paramExportItem, paramIncludeDocProps,
                        paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                        paramBitmapMissingFonts, paramUseISO19005_1,
                        ref paramMissing);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    if (wordDocument != null)
                    {
                        wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                        wordDocument = null;
                    }
                    if (wordApplication != null)
                    {
                        wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                        wordApplication = null;
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    KillProcesses("WINWORD");
                }
                return result;
            }

        }
        ///<summary>
        /// 把Excel文件转换成PDF格式文件
        ///</summary>
        ///<param name="sourcePath">源文件路径</param>
        ///<param name="targetPath">目标文件路径</param> 
        ///<returns>true=转换成功</returns>
        public bool XLSConvertToPDF(string sourcePath, string targetPath)
        {
            lock (excelObject)
            {
                bool result = false;
                Excel.XlFixedFormatType targetType = Excel.XlFixedFormatType.xlTypePDF;
                object missing = Type.Missing;
                Excel.Application application = null;
                Excel.Workbook workBook = null;
                try
                {
                    application = new Excel.Application();
                    object target = targetPath;
                    object type = targetType;
                    workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                            missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    workBook.ExportAsFixedFormat(targetType, target, Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    if (workBook != null)
                    {
                        workBook.Close(true, missing, missing);
                        workBook = null;
                    }
                    if (application != null)
                    {
                        application.Quit();
                        application = null;
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    KillProcesses("EXCEL");
                }
                return result;
            }
        }
        ///<summary>
        /// 把PowerPoint文件转换成PDF格式文件
        ///</summary>
        ///<param name="sourcePath">源文件路径</param>
        ///<param name="targetPath">目标文件路径</param> 
        ///<returns>true=转换成功</returns>
        public bool PPTConvertToPDF(string sourcePath, string targetPath)
        {
            lock (pptObject)
            {
                bool result;
                PowerPoint.PpSaveAsFileType targetFileType = PowerPoint.PpSaveAsFileType.ppSaveAsPDF;
                object missing = Type.Missing;
                PowerPoint.Application application = null;
                PowerPoint.Presentation persentation = null;
                try
                {
                    application = new PowerPoint.Application();
                    persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                    persentation.SaveAs(targetPath, targetFileType, Microsoft.Office.Core.MsoTriState.msoTrue);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    if (persentation != null)
                    {
                        persentation.Close();
                        persentation = null;
                    }
                    if (application != null)
                    {
                        application.Quit();
                        application = null;
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    KillProcesses("POWERPNT");
                }
                return result;
            }
        }
        private void KillProcesses(string processesName)
        {
            Process[] pp = Process.GetProcessesByName(processesName);
            foreach (Process p in pp)
            {
                p.Kill();
            }
        }
    }

    public class ConvertToSwf
    {
        //  SWFTools\pdf2swf.exe  -t E:\abc.pdf -o E:\abc3.swf -s languagedir=C:\xpdf\xpdf-chinese-simplified -s flashversion=9

        // CallCMD("C:\\SWFTools\\pdf2swf.exe", "C:\\Users\\Administrator\\Desktop\\新建文件夹\\h5.pdf" + " -o " + "C:\\Users\\Administrator\\Desktop\\新建文件夹\\h5.swf" + " -f -T 9");
        public string CallCMD(string shell, string para)
        {
            //para +=  " -f -T 9";
            para = " -t " + para;
            para += "  -s languagedir=C:\\xpdf\\xpdf-chinese-simplified   -T 9";
            string result = "";
            using (Process cmd = new Process())
            {
                cmd.StartInfo.FileName = shell;
                cmd.StartInfo.Arguments = para ?? "";
                cmd.StartInfo.WorkingDirectory = Path.GetDirectoryName(shell);
                cmd.StartInfo.RedirectStandardOutput = false;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.Start();
                cmd.WaitForExit();
                result += "1"; //cmd.StandardOutput.ReadToEnd();
                cmd.Close();
            }
            return result;
        }
    }
}