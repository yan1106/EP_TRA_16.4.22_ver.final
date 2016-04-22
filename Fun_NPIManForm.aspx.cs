using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Web.Services;
using System.Drawing;
using System.IO;
using System.Reflection;


public partial class PreNPI_Fun_NPIManForm : System.Web.UI.Page
{
    public struct FillingData
    {
        public string strData;
        public string strError;
    }
    public string editflag = "Yes";
    public string username = "CIM";
    public List<string> maual_data = new List<string>();
    public string temp_sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBInit();
        }
        
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    protected void DBInit()
    {
        clsMySQL db = new clsMySQL();
        string strQuerySQL = "Select * from npiManual ";
        if (text_Cust.Text != "")
        {
            strQuerySQL = strQuerySQL + "Where New_Customer like'%" + text_Cust.Text.Trim() + "%'";
            if (text_Devi.Text != "")
            {
                strQuerySQL = strQuerySQL + " and  New_Device like '%" + text_Devi.Text.Trim() + "%'";
            }
        }
        else
        {
            if (text_Devi.Text != "")
            {
                strQuerySQL = strQuerySQL + "Where  New_Device like '%" + text_Devi.Text.Trim() + "%'";
            }
        }
        try
        {
            
            clsMySQL.DBReply dr = db.QueryDS(strQuerySQL);
            GridView1.DataSource = dr.dsDataSet.Tables[0].DefaultView;
            GridView1.DataBind();
            db.Close();
        }
        catch (Exception ex)
        {
            lblError.Text = "[Error Message, ButSearch] : " + ex.ToString();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //----分頁 Start----
        GridView1.PageIndex = e.NewPageIndex;
        DBInit();
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        //----分頁 End----
        DBInit();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Button tButton = new Button();
        ImageButton tButton = new ImageButton();                //View Button
        ImageButton eButton = new ImageButton();                //Edit Button
        ImageButton dButton = new ImageButton();                //Del Button
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //tButton = (Button)e.Row.Cells[0].FindControl("btnSelect");
            tButton = (ImageButton)e.Row.Cells[0].FindControl("btnSelect");
            eButton = (ImageButton)e.Row.Cells[0].FindControl("btnEdit");
            dButton = (ImageButton)e.Row.Cells[0].FindControl("btnDelete");
            //按下Select按鈕時，CommandArgument為該筆資料的index
            tButton.CommandArgument = e.Row.RowIndex.ToString();
            eButton.CommandArgument = e.Row.RowIndex.ToString();
        }
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow gvrow = GridView1.Rows[e.NewSelectedIndex];

        clsMySQL db = new clsMySQL();        
        FormView1.Visible = true;
        Panel2.Visible = true;        
        string strSQL = "select * from npiManual where New_Customer = '" + gvrow.Cells[1].Text + "' and New_Device = '" + gvrow.Cells[2].Text + "'";
        HttpContext.Current.Session["value_temp_sql"] = strSQL;        
        
        FormView1.DataSource = db.QueryDataSet(strSQL);
        FormView1.DataBind();        
    }

    private int getNowIndex(int pRowIndex)
    {
        int tEditIndex = 0;

        if (this.GridView1.AllowPaging)
        {
            //GridView有分頁時，index=頁次*分頁大小+頁面的index
            tEditIndex = (this.GridView1.PageIndex) * this.GridView1.PageSize + pRowIndex;
        }
        else
        {
            //GridView無分頁時，直接使用pRowIndex
            tEditIndex = pRowIndex;
        }

        return tEditIndex;
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strScript = "";

        /*Button btn = (Button)GridView1.FindControl("btnInsert");

        if (btn != null)

        {

          int  btn.CommandArgument 

        }
        */

       

        if (editflag == "Yes")
        {
            int iEditIndex = 0;            
            if (!e.CommandName.Equals("Page"))
            {
                


                switch (e.CommandName)
                {
                    


                    case "Insert":
                        FormView1.ChangeMode(FormViewMode.Insert);
                        GridView1.Visible = false;
                        Panel1.Visible = false;
                        FormView1.Visible = true;
                        Panel2.Visible = true;
                        break;
                    case "Select":
                        //根據有沒有分頁來取得目前實際的index，避免換頁後取錯資料
                        iEditIndex = getNowIndex(Convert.ToInt32(e.CommandArgument));
                        FormView1.Visible = true;
                        Panel2.Visible = true;
                        GridView1.Visible = false;
                        Panel1.Visible = false;
                        break;
                    case "Edit":
                        //根據有沒有分頁來取得目前實際的index，避免換頁後取錯資料
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow selecteRow = GridView1.Rows[index];

                        TableCell New_Customer = selecteRow.Cells[1];
                        TableCell New_Device = selecteRow.Cells[2];

                        string str_sql = "select * from npimanual where New_Customer='" + New_Customer.Text + "'and New_Device='" + New_Device.Text + "'";
                        maual_data.Clear();
                        temp_receive_manual_data(str_sql);
                        iEditIndex = getNowIndex(Convert.ToInt32(e.CommandArgument));                        
                        FormView1.Visible = true;
                        Panel2.Visible = true;
                        FormView1.ChangeMode(FormViewMode.Edit);
                        //DropDownList myMan04 = (DropDownList)FormView1.FindControl("DDL_Man04");
                      
                        GridView1.Visible = false;
                        Panel1.Visible = false;
                        break;
                    case "DEL_Manual":                             
                        iEditIndex = Convert.ToInt32(e.CommandArgument);//getNowIndex(Convert.ToInt32(e.CommandArgument));
                        //lblError.Text = iEditIndex.ToString();
                        GridViewRow row = GridView1.Rows[iEditIndex];                        
                        string strCustomer = row.Cells[1].Text;
                        string strDevice = row.Cells[2].Text;   
                        string strData = strCustomer.Trim() + "|" + strDevice.Trim() + "|" + username.Trim();
                        strScript = string.Format("<script language='javascript'>ConfirmDelManual('確定刪除？','" + strData + "');</script>", strCustomer);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
                        break;                  
                }
            }            
        }
        else
        {
            switch (e.CommandName)
            {
                case "Insert":
                    lblError.Text = "您無新增權限!!";
                    break;
            }
        }
    }

    private void ChangeViewMode()
    {
        FormView1.Visible = false;
        Panel2.Visible = false;
        GridView1.Visible = true;
        Panel1.Visible = true;
        GridView1.EditIndex = -1;
        ClearTextBox();        
        FormView1.ChangeMode(FormViewMode.ReadOnly);
        DBInit();
    }
    private void ClearControls(ArrayList controls)
    {
        foreach (Control control in controls)
        {
            if (control is TextBox)
            {
                (control as TextBox).Text = "";
            }
            else if (control is Label)
            {
                (control as Label).Text = "";
            }
            else if (control is CheckBox)
            {
                (control as CheckBox).Checked = false;
            }
            else if (control is DropDownList)
            {
               
            }
        }
    }   
    private void ClearTextBox()
    {
        ArrayList arycontrl = new ArrayList();
        TextBox myNew_Customer = (TextBox)FormView1.FindControl("text_Customer");       //Customer        
        TextBox myNew_Device = (TextBox)FormView1.FindControl("text_Device");           //Device        
        TextBox myMan_01 = (TextBox)FormView1.FindControl("text_Man01");                //Wafer PSV Type/Thickness
        TextBox myMan_02 = (TextBox)FormView1.FindControl("text_Man02");                //PI Type

        TextBox myMan_03 = (TextBox)FormView1.FindControl("text_Man03");                //PI Thickness
        TextBox myMan_04 = (TextBox)FormView1.FindControl("text_Man04");                //UBM Type/Thickness
        TextBox myMan_05 = (TextBox)FormView1.FindControl("text_Man05");                //PR Thickness
        TextBox myMan_06 = (TextBox)FormView1.FindControl("text_Man06");                //UBM Insdie Final Maetal for FOC

        TextBox myMan_07 = (TextBox)FormView1.FindControl("text_Man07");                //UBM Plating Area(dm2)
        TextBox myMan_08 = (TextBox)FormView1.FindControl("text_Man08");                //UBM Density(UBM Area/Die Area)

        TextBox myMan_09 = (TextBox)FormView1.FindControl("text_Man09");                //Mushroom CD
        TextBox myMan_10 = (TextBox)FormView1.FindControl("text_Man10");                //Min Mushroom Space
        TextBox myMan_11 = (TextBox)FormView1.FindControl("text_Man11");                //Bump Density(Bump Q'ty/Die Area)
        TextBox myMan_12 = (TextBox)FormView1.FindControl("text_Man12");                //BM/UBM Ratio
        TextBox myMan_13 = (TextBox)FormView1.FindControl("text_Man13");                //Bump Coplanarity 
        TextBox myMan_14 = (TextBox)FormView1.FindControl("text_Man14");                //Bump Shear Strenght

        TextBox myMan_15 = (TextBox)FormView1.FindControl("text_Man15");                //Bump Void
        TextBox myMan_16 = (TextBox)FormView1.FindControl("text_Man16");                //PI Rougness(Ra)

        TextBox myMan_17 = (TextBox)FormView1.FindControl("text_Man17");                //August - Gross Die
        TextBox myMan_18 = (TextBox)FormView1.FindControl("text_Man18");                //August - Expose Pad
        TextBox myMan_19 = (TextBox)FormView1.FindControl("text_Man19");                //RVSI - Gross Die
        TextBox myMan_20 = (TextBox)FormView1.FindControl("text_Man20");                //Bump To Bump Space
        TextBox myMan_21 = (TextBox)FormView1.FindControl("text_Man21");                //SMO
        TextBox myMan_22 = (TextBox)FormView1.FindControl("text_Man22");                //UBM/SMO Ratio

        arycontrl.Add(myMan_01); arycontrl.Add(myMan_02); arycontrl.Add(myMan_03); arycontrl.Add(myMan_04);
        arycontrl.Add(myMan_05); arycontrl.Add(myMan_06); arycontrl.Add(myMan_07); arycontrl.Add(myMan_08);
        arycontrl.Add(myMan_09); arycontrl.Add(myMan_10); arycontrl.Add(myMan_11); arycontrl.Add(myMan_12);
        arycontrl.Add(myMan_13); arycontrl.Add(myMan_14); arycontrl.Add(myMan_15); arycontrl.Add(myMan_16);
        arycontrl.Add(myMan_17); arycontrl.Add(myMan_18); arycontrl.Add(myMan_19); arycontrl.Add(myMan_20);
        arycontrl.Add(myMan_21); arycontrl.Add(myMan_22); arycontrl.Add(myNew_Customer); arycontrl.Add(myNew_Device);
       
        ClearControls(arycontrl);
        lblError.Text = "";
    }
    protected void FormView1_PreRender(object sender, EventArgs e)
    {
        FormView oFormView = default(FormView);
        LinkButton oLinkButton = default(LinkButton);
        //TextBox oTextBox = default(TextBox);

        oFormView = (FormView)sender;
        if (!oFormView.Visible)
            return;        
        switch (oFormView.CurrentMode)
        {
            case FormViewMode.Insert:
                // 顯示新增Button
                oLinkButton = (LinkButton)(oFormView.FindControl("InsertButton"));
                oLinkButton.Visible = true;          
                break;
            case FormViewMode.Edit:
                //顯示更新Button
                 oLinkButton = (LinkButton)(oFormView.FindControl("UpdateButton"));
                 oLinkButton.Visible = true;                
                break;
            case FormViewMode.ReadOnly:                
                break;
        }
    }
    protected void FormView1_ItemCommand(object sender, System.Web.UI.WebControls.FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            //取消後，切換為瀏覽模式                            
            ChangeViewMode();            
        }        
    }
    protected void FormView1_ItemUpdated(object sender, System.Web.UI.WebControls.FormViewUpdatedEventArgs e)
    {
        //更新後，切換為瀏覽模式
        ChangeViewMode();        
    }
    protected void FormView1_ItemInserted(object sender, System.Web.UI.WebControls.FormViewInsertedEventArgs e)
    {
        //新增後，切換為瀏覽模式
        ChangeViewMode();        
    }
    protected void FormView1_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        ClearTextBox();
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        
        FillingData fd;
        fd.strData = "";
        fd.strError = "";
        fd = FillDataCheck();
        string strScript = "";
        TextBox myNew_Customer = (TextBox)FormView1.FindControl("text_Customer");       //Customer        
        TextBox myNew_Device = (TextBox)FormView1.FindControl("text_Device");           //Device        
        TextBox myMan_01 = (TextBox)FormView1.FindControl("text_Man01");                //Wafer PSV Type/Thickness
        TextBox myMan_02 = (TextBox)FormView1.FindControl("text_Man02");                //PI Type

        TextBox myMan_03  = (TextBox) FormView1.FindControl("text_Man03");              //PI Thickness
        DropDownList myMan_04 = (DropDownList)FormView1.FindControl("DDL_Man04");                //UBM Type/Thickness
        TextBox myMan_05 = (TextBox)FormView1.FindControl("text_Man05");                //PR Thickness
        TextBox myMan_06 = (TextBox)FormView1.FindControl("text_Man06");                //UBM Insdie Final Maetal for FOC

        TextBox myMan_07 = (TextBox)FormView1.FindControl("text_Man07");                //UBM Plating Area(dm2)
        TextBox myMan_08 = (TextBox)FormView1.FindControl("text_Man08");                //UBM Density(UBM Area/Die Area)

        TextBox myMan_09 = (TextBox)FormView1.FindControl("text_Man09");                //Mushroom CD
        TextBox myMan_10 = (TextBox)FormView1.FindControl("text_Man10");                //Min Mushroom Space
        TextBox myMan_11 = (TextBox)FormView1.FindControl("text_Man11");                //Bump Density(Bump Q'ty/Die Area)
        TextBox myMan_12 = (TextBox)FormView1.FindControl("text_Man12");                //BM/UBM Ratio
        TextBox myMan_13 = (TextBox)FormView1.FindControl("text_Man13");                //Bump Coplanarity 
        DropDownList myMan_14 = (DropDownList)FormView1.FindControl("DDL_Man14");                 //Bump Shear Strenght

        TextBox myMan_15 = (TextBox)FormView1.FindControl("text_Man15");                //Bump Void
        TextBox myMan_16 = (TextBox)FormView1.FindControl("text_Man16");                //PI Rougness(Ra)

        TextBox myMan_17 = (TextBox)FormView1.FindControl("text_Man17");                //August - Gross Die
        TextBox myMan_18 = (TextBox)FormView1.FindControl("text_Man18");                //August - Expose Pad
        TextBox myMan_19 = (TextBox)FormView1.FindControl("text_Man19");                //RVSI - Gross Die
        TextBox myMan_20 = (TextBox)FormView1.FindControl("text_Man20");                //Bump To Bump Space
        TextBox myMan_21 = (TextBox)FormView1.FindControl("text_Man21");                //SMO
        TextBox myMan_22 = (TextBox)FormView1.FindControl("text_Man22");                //UBM/SMO Ratio
        
        if (!fd.strError.Equals(""))
        {
            strScript = "<script language='javascript'>alert('" + fd.strError + "？');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        }
        else
        {

          
            string strData = myNew_Customer.Text.Trim() + "|" + myNew_Device.Text.Trim() + "|" + myMan_01.Text.Trim() + "|" + myMan_02.Text.Trim()+ "|" + myMan_03.Text.Trim() + "|" +
                                    myMan_04.SelectedValue + "|" + myMan_05.Text.Trim() + "|" + myMan_06.Text.Trim() + "|" + myMan_07.Text.Trim() + "|" + myMan_08.Text.Trim() + "|" +
                                    myMan_09.Text.Trim() + "|" + myMan_10.Text.Trim() + "|" + myMan_11.Text.Trim() + "|" + myMan_12.Text.Trim() + "|" + myMan_13.Text.Trim() + "|" +
                                    myMan_14.SelectedValue + "|" + myMan_15.Text.Trim() + "|" + myMan_16.Text.Trim() + "|" + myMan_17.Text.Trim() + "|" + myMan_18.Text.Trim() + "|" +
                                    myMan_19.Text.Trim() + "|" + myMan_20.Text.Trim() + "|" + myMan_21.Text.Trim() + "|" + myMan_22.Text.Trim() + "|" + username.Trim();

            strScript = string.Format("<script language='javascript'>ConfirmInsertManual('確定新增？','" + strData + "');</script>", myNew_Customer);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        }      



        //clsMySQL db = new clsMySQL();
        //DataSet ds;
        //if (myNew_Customer.Text == "")
        //{
        //    RegisterStartupScript("訊息通知", "<script> alert('必須填寫New Customer!!');</script>");
        //}
        //else
        //{
        //    if (myNew_Device.Text == "")
        //    {
        //        RegisterStartupScript("訊息通知", "<script> alert('必須填寫New Device!!');</script>");
        //    }
        //    else
        //    {
        //        string strSQL_Query = "select * from npiManual where New_Customer = '" + myNew_Customer.Text + "' and New_Device = '" + myNew_Device.Text + "'";
        //        ds = db.QueryDataSet(strSQL_Query);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            RegisterStartupScript("訊息通知", "<script> alert('Customer = [" + myNew_Customer.Text + "]/ Device = [" + myNew_Device.Text + "]的資料已存在!!');</script>");
        //        }
        //        else
        //        {
        //            try
        //            {
        //string strSQL_Insert = string.Format("Insert into npiManual " +
        //                                    "(New_Customer, New_Device, Stype, UpdateTime, npiUser, Man_Status," +
        //                                    "Man_01, Man_02, Man_03, Man_04, Man_05, Man_06, Man_07, Man_08, Man_09, Man_10," +
        //                                    "Man_11, Man_12, Man_13, Man_14, Man_15, Man_16, Man_17, Man_18, Man_19, Man_20, Man_21, Man_22" +
        //                                    ") values " +
        //                                    "('{0}','{1}','Man',NOW(),'{24}','Y','{2}','{3}','{4}'," +
        //                                    "'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}'," +
        //                                    "'{22}','{23}')",
        //                                    myNew_Customer.Text.Trim(), myNew_Device.Text.Trim(), myMan_01.Text.Trim(), myMan_02.Text.Trim(), myMan_03.Text.Trim(),
        //                                    myMan_04.Text.Trim(), myMan_05.Text.Trim(), myMan_06.Text.Trim(), myMan_07.Text.Trim(), myMan_08.Text.Trim(),
        //                                    myMan_09.Text.Trim(), myMan_10.Text.Trim(), myMan_11.Text.Trim(), myMan_12.Text.Trim(), myMan_13.Text.Trim(),
        //                                    myMan_14.Text.Trim(), myMan_15.Text.Trim(), myMan_16.Text.Trim(), myMan_17.Text.Trim(), myMan_18.Text.Trim(),
        //                                    myMan_19.Text.Trim(), myMan_20.Text.Trim(), myMan_21.Text.Trim(), myMan_22.Text.Trim(), username.Trim());
                        //strSQL_Insert = strSQL_Insert.Replace("'", "''");
                        //string strHisSQL = string.Format("Insert into npiHistory (His_Time, npiUser, npiFun, His_SQL) values (NOW(),'npiMan_Add','{0}','{1}') ", username.Trim() , strSQL_Insert);

        //lblError.Text = strSQL_Insert;
        //                if (db.QueryExecuteNonQuery(strSQL_Insert))
        //                {
        //                    RegisterStartupScript("訊息通知", "<script> alert('資料新增，成功！！');</script>");                            
        //                    ChangeViewMode();
        //                }
        //                else
        //                {
        //                    //lblError.Text = strSQL_Insert;
        //                    RegisterStartupScript("訊息通知", "<script> alert('資料新增，失敗！！');</script>");
        //                }
        //            }
        //            catch (Exception ex)
        //            {                        
        //                lblError.Text = "[Error Message::NPI_Manual Form Insert Function]: " + ex.ToString();
        //            }
        //        }
        //    }
        //}
    }
    protected string jude_ddl_Man04(string syn)
    {
        if (syn == "Ti1K/Cu5K/Ni3um")
            return "Ti1K/Cu3K/Ni3.5um";
        else
            return "Ti1K/Cu5K/Ni3um";

    }

    protected string jude_ddl_Man14(string syn,int i)
    {
        if (syn == "LF: >2.5 g/mil^2" && i == 1)
            return "EU: >2.8 g/mil^2";
        else if (syn == "LF: >2.5 g/mil^2" && i == 2)
            return "EU: > 2.0 g / mil ^ 2";
        else if (syn == "EU: >2.8 g/mil^2" && i == 1)
            return "LF: >2.5 g/mil^2";
        else if (syn == "EU: >2.8 g/mil^2" && i == 2)
            return "EU: > 2.0 g / mil ^ 2";
        else if (syn == "EU: >2.0 g/mil^2" && i == 1)
            return "LF: >2.5 g/mil^2";
        else if (syn == "EU: >2.0 g/mil^2" && i == 2)
            return "EU: >2.8 g/mil^2";
        else
            return "error!!";

    }


    protected void temp_receive_manual_data(string mySQL)
    {
      
        // clsMySQL db = new clsMySQL();

        //MySqlDataReader mydr = db.QueryDataReader(mySQL);
        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();

        while (mydr.Read())
        {
            maual_data.Add( (string)mydr["Man_04"]);
            maual_data.Add((string)mydr["Man_14"]);
            break;
        }
        mydr.Close();
        MySqlConn.Close();
      
    }




    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        FillingData fd;
        fd.strData = "";
        fd.strError = "";
        fd = FillDataCheck();
        string strScript = "";
        lblError.Text = "";
        


        TextBox myNew_Customer = (TextBox)FormView1.FindControl("text_Customer");       //Customer        
        TextBox myNew_Device = (TextBox)FormView1.FindControl("text_Device");           //Device        
        TextBox myMan_01 = (TextBox)FormView1.FindControl("text_Man01");                //Wafer PSV Type/Thickness
        TextBox myMan_02 = (TextBox)FormView1.FindControl("text_Man02");                //PI Type

        TextBox myMan_03 = (TextBox)FormView1.FindControl("text_Man03");              //PI Thickness
        DropDownList myMan_04 = (DropDownList)FormView1.FindControl("DDL_Man04");  //UBM Type/Thickness
        
        TextBox myMan_05 = (TextBox)FormView1.FindControl("text_Man05");                //PR Thickness
        TextBox myMan_06 = (TextBox)FormView1.FindControl("text_Man06");                //UBM Insdie Final Maetal for FOC

        TextBox myMan_07 = (TextBox)FormView1.FindControl("text_Man07");                //UBM Plating Area(dm2)
        TextBox myMan_08 = (TextBox)FormView1.FindControl("text_Man08");                //UBM Density(UBM Area/Die Area)

        TextBox myMan_09 = (TextBox)FormView1.FindControl("text_Man09");                //Mushroom CD
        TextBox myMan_10 = (TextBox)FormView1.FindControl("text_Man10");                //Min Mushroom Space
        TextBox myMan_11 = (TextBox)FormView1.FindControl("text_Man11");                //Bump Density(Bump Q'ty/Die Area)
        TextBox myMan_12 = (TextBox)FormView1.FindControl("text_Man12");                //BM/UBM Ratio
        TextBox myMan_13 = (TextBox)FormView1.FindControl("text_Man13");                //Bump Coplanarity 
        DropDownList myMan_14 = (DropDownList)FormView1.FindControl("DDL_Man14");               //Bump Shear Strenght

        TextBox myMan_15 = (TextBox)FormView1.FindControl("text_Man15");                //Bump Void
        TextBox myMan_16 = (TextBox)FormView1.FindControl("text_Man16");                // PI Rougness(Ra)

        TextBox myMan_17 = (TextBox)FormView1.FindControl("text_Man17");                //August - Gross Die
        TextBox myMan_18 = (TextBox)FormView1.FindControl("text_Man18");                //August - Expose Pad
        TextBox myMan_19 = (TextBox)FormView1.FindControl("text_Man19");                //RVSI - Gross Die
        TextBox myMan_20 = (TextBox)FormView1.FindControl("text_Man20");                //Bump To Bump Space
        TextBox myMan_21 = (TextBox)FormView1.FindControl("text_Man21");                //SMO
        TextBox myMan_22 = (TextBox)FormView1.FindControl("text_Man22");                //UBM/SMO Ratio

        //myMan_04.Items.Add(new ListItem(maual_data[0], maual_data[0]));
        //myMan_04.Items.Add(new ListItem(jude_ddl_Man04(maual_data[0]), jude_ddl_Man04(maual_data[0])));

        if (!fd.strError.Equals(""))
        {
            strScript = "<script language='javascript'>alert('" + fd.strError + "？');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        }
        else
        {
            string strData = myNew_Customer.Text.Trim() + "|" + myNew_Device.Text.Trim() + "|" + myMan_01.Text.Trim() + "|" + myMan_02.Text.Trim() + "|" + myMan_03.Text.Trim() + "|" +
                                   myMan_04.SelectedValue + "|" + myMan_05.Text.Trim() + "|" + myMan_06.Text.Trim() + "|" + myMan_07.Text.Trim() + "|" + myMan_08.Text.Trim() + "|" +
                                   myMan_09.Text.Trim() + "|" + myMan_10.Text.Trim() + "|" + myMan_11.Text.Trim() + "|" + myMan_12.Text.Trim() + "|" + myMan_13.Text.Trim() + "|" +
                                   myMan_14.SelectedValue + "|" + myMan_15.Text.Trim() + "|" + myMan_16.Text.Trim() + "|" + myMan_17.Text.Trim() + "|" + myMan_18.Text.Trim() + "|" +
                                   myMan_19.Text.Trim() + "|" + myMan_20.Text.Trim() + "|" + myMan_21.Text.Trim() + "|" + myMan_22.Text.Trim() + "|" + username.Trim();

            strScript = string.Format("<script language='javascript'>ConfirmUpdateManual('確定修改？','" + strData + "');</script>", myNew_Customer);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        }      

        //if (!fd.strError.Equals(""))
        //{
        //    strScript = "<script language='javascript'>alert('" + fd.strError + "？');</script>";
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        //}
        //else
        //{
        //    clsMySQL db = new clsMySQL();
        //    try
        //    {
        //        string strSQL_Update = string.Format("Update npiManual set New_Customer='{0}', New_Device='{1}',Stype='Man',UpdateTime=NOW(),Man_Status='Y',"+
        //                                            " Man_01='{2}',Man_02='{3}',Man_03='{4}',Man_04='{5}',Man_05='{6}',Man_06='{7}',Man_07='{8}',Man_08='{9}',"+
        //                                            " Man_09='{10}',Man_10='{11}',Man_11='{12}',Man_12='{13}',Man_13='{14}',Man_15='{16}',Man_16='{17}',Man_17='{18}',"+
        //                                            " Man_18='{19}',Man_19='{20}',Man_20='{21}',Man_21='{22}',Man_22='{23}' " +
        //                                            "where New_Customer='{0}' and New_Device='{1}' and SType='Man' and Man_Status='Y'", 
        //                                            myNew_Customer.Text.Trim(), myNew_Device.Text.Trim(), myMan_01.Text.Trim(), myMan_02.Text.Trim(), myMan_03.Text.Trim(),
        //                                            myMan_04.Text.Trim(), myMan_05.Text.Trim(), myMan_06.Text.Trim(), myMan_07.Text.Trim(), myMan_08.Text.Trim(),
        //                                            myMan_09.Text.Trim(), myMan_10.Text.Trim(), myMan_11.Text.Trim(), myMan_12.Text.Trim(), myMan_13.Text.Trim(),
        //                                            myMan_14.Text.Trim(), myMan_15.Text.Trim(), myMan_16.Text.Trim(), myMan_17.Text.Trim(), myMan_18.Text.Trim(),
        //                                            myMan_19.Text.Trim(), myMan_20.Text.Trim(), myMan_21.Text.Trim(), myMan_22.Text.Trim());

               
        //        //lblError.Text = strSQL_Update;
        //        if (db.QueryExecuteNonQuery(strSQL_Update))
        //        {
        //            RegisterStartupScript("訊息通知", "<script> alert('資料更新，成功！！');</script>");                    
        //            ChangeViewMode();
        //        }
        //        else
        //        {
        //            //lblError.Text = strSQL_Update;
        //            RegisterStartupScript("訊息通知", "<script> alert('資料更新，失敗！！');</script>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {                
        //        lblError.Text = "[Error Message::NPI Manual Form Update Function]: " + ex.ToString();
        //    }
        //}


    }

    private FillingData FillDataCheck()
    {   //--------Check New Customer and New Device 是否沒有輸入資料
        FillingData fd;
        fd.strData = "";
        fd.strError = "";
        TextBox myNew_Customer = (TextBox)FormView1.FindControl("text_Customer");
        TextBox myNew_Device = (TextBox)FormView1.FindControl("text_Device");

        if (myNew_Customer.Text == "")
        {
            fd.strError = "請填寫New Customer!!";
        }
        else
        {
            if (myNew_Device.Text == "") {

                fd.strError = "請填寫New Device!!";
            }
        
        }

        return fd;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        //----編輯模式----
        GridView1.EditIndex = e.NewEditIndex;
        GridViewRow gvrow = GridView1.Rows[e.NewEditIndex];
        clsMySQL db = new clsMySQL();
        FormView1.Visible = true;
        Panel2.Visible = true;        
        
        if (! FormView1.Visible)
            return;

        switch (FormView1.CurrentMode)
        {
            case FormViewMode.ReadOnly:
                FormView1.ChangeMode(FormViewMode.ReadOnly);
                break;
            case FormViewMode.Edit:
                FormView1.ChangeMode(FormViewMode.Edit);
            break;
        }

        string strSQL = "select * from npiManual where New_Customer = '" + gvrow.Cells[1].Text + "' and New_Device = '" + gvrow.Cells[2].Text + "'";
        FormView1.DataSource = db.QueryDataSet(strSQL);
        FormView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //-----View Mode----
        GridViewRow gvrow = GridView1.SelectedRow;
        clsMySQL db = new clsMySQL();
        FormView1.Visible = true;
        Panel2.Visible = true;        
        string strSQL = "select * from npiManual where New_Customer = '" + gvrow.Cells[1].Text + "' and New_Device = '" + gvrow.Cells[2].Text + "'";
        FormView1.DataSource = db.QueryDataSet(strSQL);
        FormView1.DataBind();
        
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //lblError.Text = e.Keys["C_Code"].ToString() + "////" + e.Values.ToString();        
        clsMySQL db = new clsMySQL();
        try
        {   
            GridViewRow gvrow = GridView1.Rows[e.RowIndex];

            lblError.Text = gvrow.Cells[1].Text + "////" + gvrow.Cells[2].Text;
            string strSQL_Delete = string.Format("Delete from npiManual where New_Customer='{0}' and  New_Device='{1}'",
                                                gvrow.Cells[1].Text.Trim(), gvrow.Cells[2].Text.Trim());

            if (db.QueryExecuteNonQuery(strSQL_Delete))
            {
                RegisterStartupScript("訊息通知", "<script> alert('資料已刪除，成功！！');</script>");                
                ChangeViewMode();
            }
            else
            {
                //lblError.Text = strSQL_Delete;
                RegisterStartupScript("訊息通知", "<script> alert('資料刪除，失敗！！');</script>");
            }
        }
        catch (FormatException ex)
        {
            lblError.Text = "[Error Message::NPI Manual Form Delete Function]: " + ex.ToString();
        }        
    }
    protected void cmdFilter_Click(object sender, EventArgs e)
    {
        ChangeViewMode();         
    }
    [System.Web.Services.WebMethod]
    public static string DEL_An_Manual(string strConjData)
    {
        string strError = "";
        string[] strData = new string[2];
        strData = strConjData.Split('|');

        ArrayList arSQL = new ArrayList();
        string strSQL_Del = string.Format("Delete from npiManual where New_Customer='{0}' and New_Device = '{1}'", strData[0], strData[1]);
        arSQL.Add(strSQL_Del);
        strSQL_Del = strSQL_Del.Replace("'", "''");
        string strHisSQL = string.Format("Insert into npiHistory (His_Time, npiUser, npiFun, His_SQL) values (NOW(),'{0}','npiMan_Del','{1}') ", strData[2], strSQL_Del);        
        arSQL.Add(strHisSQL);                        
        clsMySQL db = new clsMySQL();
        //if (!db.QueryExecuteNonQuery(strSQL_Del))
        if (!db.myBatchNonQuery(arSQL))
        {
            strError = "Error Message:[NPI ManualForm] Delete Fail!!";            
        }        
        return strError;
    }
    [System.Web.Services.WebMethod]
    public static string Update_An_Manual(string strConjData)
    {
        string strError = "";
        string[] strData = new string[25];
        strData = strConjData.Split('|');
        ArrayList arSQL = new ArrayList();
        clsMySQL db = new clsMySQL();
        try
        { 
            string strSQL_Update = string.Format("Update npiManual set New_Customer='{0}', New_Device='{1}',Stype='Man',UpdateTime=NOW(),npiUser='{24}',Man_Status='Y'," +
                                                    " Man_01='{2}',Man_02='{3}',Man_03='{4}',Man_04='{5}',Man_05='{6}',Man_06='{7}',Man_07='{8}',Man_08='{9}'," +
                                                    " Man_09='{10}',Man_10='{11}',Man_11='{12}',Man_12='{13}',Man_13='{14}',Man_14='{15}',Man_15='{16}',Man_16='{17}',Man_17='{18}'," +
                                                    " Man_18='{19}',Man_19='{20}',Man_20='{21}',Man_21='{22}',Man_22='{23}' " +
                                                    "where New_Customer='{0}' and New_Device='{1}' and SType='Man' and Man_Status='Y'",
                                                    strData[0], strData[1], strData[2], strData[3], strData[4],
                                                    strData[5], strData[6], strData[7], strData[8], strData[9],
                                                    strData[10], strData[11], strData[12], strData[13], strData[14],
                                                    strData[15], strData[16], strData[17], strData[18], strData[19],
                                                    strData[20], strData[21], strData[22].Trim(), strData[23],strData[24]);
            arSQL.Add(strSQL_Update);
            strSQL_Update = strSQL_Update.Replace("'", "''");
            string strHisSQL = string.Format("Insert into npiHistory (His_Time, npiUser, npiFun, His_SQL) values (NOW(),'{0}','npiMan_Edit','{1}') ", strData[24], strSQL_Update);            
            arSQL.Add(strHisSQL);
            //lblError.Text = strSQL_Update;
            //if (!db.QueryExecuteNonQuery(strSQL_Update))
            if (!db.myBatchNonQuery(arSQL))
            {
                strError = "Error Message: Update Fail !!! ";
            }               
        }
        catch (Exception ex)
        {                
            //lblError.Text = "[Error Message::NPI Manual Form Update Function]: " + ex.ToString();
            strError = "[Update Error Message:]" + ex.ToString();
        }
        return strError;
    }
    [System.Web.Services.WebMethod]
    public static string Insert_An_Manual(string strConjData)
    {
        string strError = "";
        string[] strData = new string[25];
        strData = strConjData.Split('|');

        ArrayList arSQL = new ArrayList();
        arSQL.Clear();
        clsMySQL db = new clsMySQL();
        DataSet ds;
        string strSQL_Query = "select * from npiManual where New_Customer = '" + strData[0] + "' and New_Device = '" + strData[1] + "'";
        ds = db.QueryDataSet(strSQL_Query);
        if (ds.Tables[0].Rows.Count > 0)
        {            
            strError = "Insert Error Message: New_Customer = [" + strData[0] + "]/ New_Device = [" + strData[1] + "]的資料已存在!!');";
        }
        else
        {
            try
            {   
                string strSQL_Insert = string.Format("Insert into npiManual " +
                                                "(New_Customer, New_Device, Stype, UpdateTime, npiUser, Man_Status," +
                                                "Man_01, Man_02, Man_03, Man_04, Man_05, Man_06, Man_07, Man_08, Man_09, Man_10," +
                                                "Man_11, Man_12, Man_13, Man_14, Man_15, Man_16, Man_17, Man_18, Man_19, Man_20, Man_21, Man_22" +
                                                ") values " +
                                                "('{0}','{1}','Man',NOW(),'{24}','Y','{2}','{3}','{4}'," +
                                                "'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}'," +
                                                "'{22}','{23}')",
                                                strData[0], strData[1], strData[2], strData[3], strData[4],
                                                strData[5], strData[6], strData[7], strData[8], strData[9],
                                                strData[10], strData[11], strData[12], strData[13], strData[14],
                                                strData[15], strData[16], strData[17], strData[18], strData[19],
                                                strData[20], strData[21], strData[22].Trim(), strData[23], strData[24]);
                arSQL.Add(strSQL_Insert);
                strSQL_Insert = strSQL_Insert.Replace("'", "''");
                string strHisSQL = string.Format("Insert into npiHistory (His_Time, npiUser, npiFun, His_SQL) values (NOW(),'{0}','npiMan_Add','{1}') ", strData[24], strSQL_Insert);
                arSQL.Add(strHisSQL);
                
                //if (!db.QueryExecuteNonQuery(strSQL_Insert))
                if (!db.myBatchNonQuery(arSQL))
                {
                    strError = "Error Message:[NPI ManualForm] Insert Fail !!! ";
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "[Error Message::NPI Manual Form Update Function]: " + ex.ToString();
                strError = "Insert Error Message:[NPI ManualForm] " + ex.ToString();
            }         
        }       
        return strError;
    }
    protected void butSearch_Click(object sender, EventArgs e)
    {
        clsMySQL db = new clsMySQL();
        string strQuerySQL = "Select * from npiManual ";
        if(text_Cust.Text != "")
        {
            strQuerySQL = strQuerySQL + "Where New_Customer like'%" + text_Cust.Text.Trim() +"%'";
            if (text_Devi.Text != "")
            {
                strQuerySQL = strQuerySQL + " and  New_Device like '%" + text_Devi.Text.Trim() + "%'";
            }
        }
        else 
        {
            if (text_Devi.Text != "")
            {
                strQuerySQL = strQuerySQL + "Where  New_Device like '%" + text_Devi.Text.Trim() + "%'";
            }            
        }
        try 
        {
            clsMySQL.DBReply dr = db.QueryDS(strQuerySQL);
            GridView1.DataSource = dr.dsDataSet.Tables[0].DefaultView;
            GridView1.DataBind();
            db.Close();
        }
        catch(Exception ex)
        {
            lblError.Text = "[Error Message, ButSearch] : " + ex.ToString();
        }

    }
    [WebMethod]
    public static string[] GetNewCustomer(string prefix)
    {
        List<string> customers = new List<string>();
        string strSQL = "select DISTINCT new_customer from npimanual Where new_customer Like '" + prefix + "%'";
        clsMySQL db = new clsMySQL();      
        foreach (DataRow dr in db.QueryDataSet(strSQL).Tables[0].Rows)
        {
            //customers.Add(string.Format("{0},{1}", dr["new_customer"], dr["new_device"]));
            customers.Add(string.Format("{0}", dr["new_customer"]));
        }      
        return customers.ToArray();
    }
    [WebMethod]
    public static string[] GetNewDevice(string prefix)
    {
        List<string> devices = new List<string>();
        string strSQL = "select DISTINCT new_device from npimanual Where new_device Like '" + prefix + "%'";
        clsMySQL db = new clsMySQL();
        try
        {
            foreach (DataRow dr in db.QueryDataSet(strSQL).Tables[0].Rows)
            {
                //customers.Add(string.Format("{0},{1}", dr["new_customer"], dr["new_device"]));
                devices.Add(string.Format("{0}", dr["new_device"]));
            }
            //return customers.ToArray();
        }
        catch (Exception ex)
        {
        }
        return devices.ToArray();
    }


    protected void DDL_Man04_DataBound1(object sender, EventArgs e)
    {
        DropDownList myMan04 = (DropDownList)FormView1.FindControl("DDL_Man04");
        myMan04.Items.Add(new ListItem(maual_data[0], maual_data[0]));
        myMan04.Items.Add(new ListItem(jude_ddl_Man04(maual_data[0]), jude_ddl_Man04(maual_data[0])));
    }


    protected void DDL_Man14_DataBound(object sender, EventArgs e)
    {
        DropDownList myMan14 = (DropDownList)FormView1.FindControl("DDL_Man14");
        myMan14.Items.Add(new ListItem(maual_data[1], maual_data[1]));
        myMan14.Items.Add(new ListItem(jude_ddl_Man14(maual_data[1], 1), jude_ddl_Man14(maual_data[1], 1)));
        myMan14.Items.Add(new ListItem(jude_ddl_Man14(maual_data[1], 2), jude_ddl_Man14(maual_data[1], 2)));
        // myMan14.Items.Add(new ListIteammaual_data[1]);
        //myMan14.Items.Add(jude_ddl_Man14(maual_data[1],1));
        //myMan14.Items.Add(jude_ddl_Man14(maual_data[1],2));
        
    }

   
}