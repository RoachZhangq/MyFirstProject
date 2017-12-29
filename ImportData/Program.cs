using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using System.Net;

namespace ImportData
{
    /// <summary>
    /// 课件系统、教案系统   导入数据
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string ProjectPath = Environment.CurrentDirectory.ToString();
            System.Data.DataTable dataTable = ReadExcelToDataTable(ProjectPath + "\\excel\\课件系统2017数据 - 副本.xlsx", "Sheet1", true);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    string str = dataTable.Columns[j].ColumnName.ToString();
                    string value = dataRow[j].ToString().Trim();
                    Console.WriteLine(str + ":" + value);
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static System.Data.DataTable ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            System.Data.DataTable result;
            try
            {
                bool flag = !File.Exists(fileName);
                if (flag)
                {
                    result = null;
                }
                else
                {
                    FileStream inputStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    IWorkbook workbook = WorkbookFactory.Create(inputStream);
                    bool flag2 = !string.IsNullOrEmpty(sheetName);
                    ISheet sheet;
                    if (flag2)
                    {
                        sheet = workbook.GetSheet(sheetName);
                        bool flag3 = sheet == null;
                        if (flag3)
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                    }
                    else
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                    bool flag4 = sheet != null;
                    if (flag4)
                    {
                        IRow row = sheet.GetRow(0);
                        int lastCellNum = (int)row.LastCellNum;
                        int num;
                        if (isFirstRowColumn)
                        {
                            for (int i = (int)row.FirstCellNum; i < lastCellNum; i++)
                            {
                                ICell cell = row.GetCell(i);
                                bool flag5 = cell != null;
                                if (flag5)
                                {
                                    string stringCellValue = cell.StringCellValue;
                                    bool flag6 = stringCellValue != null;
                                    if (flag6)
                                    {
                                        DataColumn column = new DataColumn(stringCellValue);
                                        dataTable.Columns.Add(column);
                                    }
                                }
                            }
                            num = sheet.FirstRowNum + 1;
                        }
                        else
                        {
                            num = sheet.FirstRowNum;
                        }
                        int lastRowNum = sheet.LastRowNum;
                        for (int j = num; j <= lastRowNum; j++)
                        {
                            IRow row2 = sheet.GetRow(j);
                            bool flag7 = row2 == null;
                            if (!flag7)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                for (int k = (int)row2.FirstCellNum; k < lastCellNum; k++)
                                {
                                    bool flag8 = row2.GetCell(k) != null;
                                    if (flag8)
                                    {
                                        dataRow[k] = row2.GetCell(k).ToString();
                                    }
                                }
                                dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                    result = dataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="saveFilePaht"></param>
        public static void DownloadFile(string filePath,string saveFilePaht)
        {
            WebClient client = new WebClient();
            client.DownloadFile(filePath, saveFilePaht);
        }
    }
}
