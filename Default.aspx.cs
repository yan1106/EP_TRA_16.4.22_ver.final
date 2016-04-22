using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;



public partial class _Default : System.Web.UI.Page
{

    Microsoft.Office.Interop.Excel.Application xlApp = null;
    Workbook wb = null;
    Worksheet ws = null;
    Range aRange = null;

    string upload_excel_Dir = @"D:\brunohuang\myWeb_excel";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string excel_filePath = "";
        try
        {
            excel_filePath = SaveFileAndReturnPath();//先上傳EXCEL檔案給Server

            if (this.xlApp == null)
            {
                this.xlApp = new Microsoft.Office.Interop.Excel.Application();
            }
            //打開Server上的Excel檔案
            this.xlApp.Workbooks.Open(excel_filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            this.wb = xlApp.Workbooks[1];//第一個Workbook
            this.wb.Save();

            //從第一個Worksheet讀資料
            SaveOrInsertSheet(excel_filePath, (Worksheet)xlApp.Worksheets[1]);



            ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.Page), "匯入完成", "alert('匯入完成');", true);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            xlApp.Workbooks.Close();
            xlApp.Quit();
            try
            {
                //刪除 Windows工作管理員中的Excel.exe 處理緒.
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.aRange);
            }
            catch { }
            this.xlApp = null;
            this.wb = null;
            this.ws = null;
            this.aRange = null;


            //是否刪除Server上的Excel檔
            bool isDeleteFileFromServer = true;
            if (isDeleteFileFromServer)
            {
                System.IO.File.Delete(excel_filePath);
            }


            GC.Collect();
        }
    }



    private string SaveFileAndReturnPath()
    {
        string return_file_path = "";//上傳的Excel檔在Server上的位置
        if (FileUpload1.FileName != "")
        {
            return_file_path = System.IO.Path.Combine(this.upload_excel_Dir, Guid.NewGuid().ToString() + ".xls");

            FileUpload1.SaveAs(return_file_path);
        }
        return return_file_path;
    }



    private void SaveOrInsertSheet(string excel_filename, Worksheet ws)
    {

        //要開始讀取的起始列(微軟Worksheet是從1開始算)
        int rowIndex = 1;

        //取得一列的範圍
        this.aRange = ws.get_Range("A" + rowIndex.ToString(), "C" + rowIndex.ToString());

        //判斷Row範圍裡第1格有值的話，迴圈就往下跑
        while (((object[,])this.aRange.Value2)[1, 1] != null)//用this.aRange.Cells[1, 1]來取值的方式似乎會造成無窮迴圈？
        {
            //範圍裡第1格的值
            string cell1 = ((object[,])this.aRange.Value2)[1, 1] != null ? ((object[,])this.aRange.Value2)[1, 1].ToString() : "";

            //範圍裡第2格的值
            string cell2 = ((object[,])this.aRange.Value2)[1, 2] != null ? ((object[,])this.aRange.Value2)[1, 2].ToString() : "";

            //範圍裡第3格的值
            string cell3 = ((object[,])this.aRange.Value2)[1, 3] != null ? ((object[,])this.aRange.Value2)[1, 3].ToString() : "";

            //再對各Cell處理完商業邏輯後，Insert into Table...(略






            //往下抓一列Excel範圍
            rowIndex++;
            this.aRange = ws.get_Range("A" + rowIndex.ToString(), "C" + rowIndex.ToString());
        }


    }














}