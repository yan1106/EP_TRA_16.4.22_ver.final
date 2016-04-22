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


public partial class PreNPI_Add_NPIPOR : System.Web.UI.Page
{
    public struct FillingData
    {
        public string strData;
        public string strError;
    }
    public string editflag = "Yes";    
    public string username = "CIM";
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            DBInit();
        }
    }

    protected void DBInit()
    {
        string strSQL = string.Format("SELECT * FROM npipor");
        try
        {
            clsMySQL db = new clsMySQL(); //Connection MySQL
            clsMySQL.DBReply dr = db.QueryDS(strSQL);            
            GridView1.DataSource = dr.dsDataSet.Tables[0].DefaultView;
            GridView1.DataBind();
            db.Close();        
        }
        catch (Exception ex)
        {
            lblError.Text = "Exception Error Message----  " + ex.ToString() + ">>>>>>>>>>" + strSQL;
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strScript = "";
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
                        GridView1.Visible = false;
                        Panel2.Visible = true;
                        Panel1.Visible = false;
                        break;
                    case "Edit":
                        //根據有沒有分頁來取得目前實際的index，避免換頁後取錯資料
                        iEditIndex = getNowIndex(Convert.ToInt32(e.CommandArgument));
                        FormView1.Visible = true;
                        Panel2.Visible = true;
                        FormView1.ChangeMode(FormViewMode.Edit);
                        GridView1.Visible = false;
                        Panel1.Visible = false;
                        break;
                    case "DEL_POR":
                        iEditIndex = Convert.ToInt32(e.CommandArgument);//getNowIndex(Convert.ToInt32(e.CommandArgument));
                        //lblError.Text = iEditIndex.ToString();
                        GridViewRow row = GridView1.Rows[iEditIndex];
                        string strCustomer = row.Cells[1].Text;
                        string strDevice = row.Cells[2].Text;
                        string strSite = row.Cells[3].Text;
                        string strPKG = row.Cells[4].Text;
                        string strWafer = row.Cells[5].Text;
                        string strFab = row.Cells[6].Text;
                        string strPSV = row.Cells[7].Text;
                        string strRVSI = row.Cells[8].Text;
                        string strData = strCustomer.Trim() + "|" + strDevice.Trim() + "|" + strSite.Trim() + "|" + strPKG.Trim() + "|" + strWafer.Trim() + "|" +
                                         strFab.Trim() + "|" + strPSV.Trim() + "|" + strRVSI.Trim() + "|" + username.Trim();
                        strScript = string.Format("<script language='javascript'>ConfirmDel('確定刪除？','" + strData + "');</script>", strCustomer);
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
                //顯示新增鈕
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
    protected void FormView1_ItemInserted(object sender, System.Web.UI.WebControls.FormViewInsertedEventArgs e)
    {
        //新增後，切換為瀏覽模式
        ChangeViewMode();
    }
    private void ChangeViewMode()
    {        
        FormView1.Visible = false;
        Panel2.Visible = false;
        GridView1.Visible = true;
        Panel1.Visible = true;
        GridView1.EditIndex = -1;
        //ClearTextBox();
        FormView1.ChangeMode(FormViewMode.ReadOnly);
        DBInit();
    }
    protected void FormView1_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        //ClearTextBox();        
    }
    private void ClearTextBox()
    {
        ArrayList arycontrl = new ArrayList();
        DropDownList myddPOR_01 = (DropDownList)FormView1.FindControl("ddPOR01");
        myddPOR_01.SelectedIndex = 0;
        DropDownList myddPOR_02 = (DropDownList)FormView1.FindControl("ddPOR02");
        myddPOR_02.SelectedIndex = 0;
        TextBox myPOR_03 = (TextBox)FormView1.FindControl("text_POR03");
        myPOR_03.Text = "";
        //TextBox myPOR_04 = (TextBox)FormView1.FindControl("text_POR04");
        //myPOR_04.Text = "";
        DropDownList myddPOR_04 = (DropDownList)FormView1.FindControl("ddPOR04");
        myddPOR_04.SelectedIndex = 0;
        //TextBox myPOR_05 = (TextBox)FormView1.FindControl("text_POR05");
        //myPOR_05.Text = "";
        DropDownList myddPOR_05 = (DropDownList)FormView1.FindControl("ddPOR05");
        myddPOR_05.SelectedIndex = 0;        
        TextBox myPOR_06 = (TextBox)FormView1.FindControl("text_POR06");
        myPOR_06.Text = "";
        DropDownList myddPOR_07 = (DropDownList)FormView1.FindControl("ddPOR07");
        myddPOR_07.SelectedIndex = 0;
        DropDownList myddPOR_08 = (DropDownList)FormView1.FindControl("ddPOR08");
        myddPOR_08.SelectedIndex = 0;
        DropDownList myddPOR_09 = (DropDownList)FormView1.FindControl("ddPOR09");
        myddPOR_09.SelectedIndex = 0;
        DropDownList myddPOR_10 = (DropDownList)FormView1.FindControl("ddPOR10");
        myddPOR_10.SelectedIndex = 0;
        DropDownList myddPOR_11 = (DropDownList)FormView1.FindControl("ddPOR11");
        myddPOR_11.SelectedIndex = 0;
        DropDownList myddPOR_12 = (DropDownList)FormView1.FindControl("ddPOR12");
        myddPOR_12.SelectedIndex = 0;
        DropDownList myddPOR_13 = (DropDownList)FormView1.FindControl("ddPOR13");
        myddPOR_13.SelectedIndex = 0;
        DropDownList myddPOR_14 = (DropDownList)FormView1.FindControl("ddPOR14");
        myddPOR_14.SelectedIndex = 0;
        //------------POR_15 For Pord---------------------------
        Label mylab_Pord = (Label)FormView1.FindControl("lab_Pord");
        mylab_Pord.Text = "";      
        DropDownList myddPord = (DropDownList)FormView1.FindControl("ddPord");
        myddPord.SelectedIndex = 0;
        TextBox mytext_No = (TextBox)FormView1.FindControl("text_No");
        mytext_No.Text = "";
        //------------For Pord End-----------------------
        DropDownList myddPOR_16 = (DropDownList)FormView1.FindControl("ddPOR16");
        myddPOR_16.SelectedIndex = 0;
        TextBox myPOR_17 = (TextBox)FormView1.FindControl("text_POR17");
        myPOR_17.Text = "";
        Label myPOR_18 = (Label)FormView1.FindControl("labPOR18");
        myPOR_18.Text = "";
        TextBox myPOR_19 = (TextBox)FormView1.FindControl("text_POR19");
        myPOR_19.Text = "0";
        DropDownList myddPOR_20 = (DropDownList)FormView1.FindControl("ddPOR20");
        myddPOR_20.SelectedIndex = 0;
        DropDownList myddPOR_21 = (DropDownList)FormView1.FindControl("ddPOR21");
        myddPOR_21.SelectedIndex = 0;
        TextBox myPOR_22 = (TextBox)FormView1.FindControl("text_POR22");
        myPOR_22.Text = "";
        DropDownList myddPOR_23 = (DropDownList)FormView1.FindControl("ddPOR23");
        myddPOR_23.SelectedIndex = 0;
        DropDownList myddPOR_24 = (DropDownList)FormView1.FindControl("ddPOR24");
        myddPOR_24.SelectedIndex = 0;
        DropDownList myddPOR_25 = (DropDownList)FormView1.FindControl("ddPOR25");
        myddPOR_25.SelectedIndex = 0;
        DropDownList myddPOR_26 = (DropDownList)FormView1.FindControl("ddPOR26");
        myddPOR_26.SelectedIndex = 0;
        TextBox myPOR_27 = (TextBox)FormView1.FindControl("text_POR27");
        myPOR_27.Text = "";
        DropDownList myddPOR_28 = (DropDownList)FormView1.FindControl("ddPOR28");
        myddPOR_28.SelectedIndex = 0;
        TextBox myPOR_29 = (TextBox)FormView1.FindControl("text_POR29");
        myPOR_29.Text = "0";
        TextBox myPOR_30 = (TextBox)FormView1.FindControl("text_POR30");
        myPOR_30.Text = "";
        TextBox myPOR_31 = (TextBox)FormView1.FindControl("text_POR31");
        myPOR_31.Text = "";
        TextBox myPOR_32 = (TextBox)FormView1.FindControl("text_POR32");
        myPOR_32.Text = "";
        DropDownList myddPOR_33 = (DropDownList)FormView1.FindControl("ddPOR33");
        myddPOR_33.SelectedIndex = 0;
        TextBox myPOR_34 = (TextBox)FormView1.FindControl("text_POR34");
        myPOR_34.Text = "";
        TextBox myPOR_35 = (TextBox)FormView1.FindControl("text_POR35");
        myPOR_35.Text = "";
        TextBox myPOR_36 = (TextBox)FormView1.FindControl("text_POR36");
        myPOR_36.Text = "0";
        TextBox myPOR_37 = (TextBox)FormView1.FindControl("text_POR37");
        myPOR_37.Text = "";
        //Default Text = "NA"-----------------------------------------------
        TextBox myPOR_38 = (TextBox)FormView1.FindControl("text_POR38");
        myPOR_38.Text = "NA";
        TextBox myPOR_39 = (TextBox)FormView1.FindControl("text_POR39");
        myPOR_39.Text = "NA";
        TextBox myPOR_40 = (TextBox)FormView1.FindControl("text_POR40");
        myPOR_40.Text = "NA";
        TextBox myPOR_41 = (TextBox)FormView1.FindControl("text_POR41");
        myPOR_41.Text = "NA";
        //------------------------------------------------------------------
        TextBox myPOR_42 = (TextBox)FormView1.FindControl("text_POR42");
        myPOR_42.Text = "";
        TextBox myPOR_43 = (TextBox)FormView1.FindControl("text_POR43");
        myPOR_43.Text = "";
        TextBox myPOR_44 = (TextBox)FormView1.FindControl("text_POR44");
        myPOR_44.Text = "";
        TextBox myPOR_45 = (TextBox)FormView1.FindControl("text_POR45");
        myPOR_45.Text = "0";
        DropDownList myddPOR_46 = (DropDownList)FormView1.FindControl("ddPOR46");
        myddPOR_46.SelectedIndex = 0;
        TextBox myPOR_47 = (TextBox)FormView1.FindControl("text_POR47");
        myPOR_47.Text = "";
        Label myPOR_48 = (Label)FormView1.FindControl("labPOR48");  //套公式: POR19-POR45
        myPOR_48.Text = "0";
        Label myPOR_49 = (Label)FormView1.FindControl("labPOR49");  //套公式: POR29/POR36
        myPOR_49.Text = "0";
        Label myPOR_50 = (Label)FormView1.FindControl("labPOR50");  //套公式: (Math.PI * (POR29 * POR29/4) * POR53) / (POR51 * POR52) * 1000000
        myPOR_50.Text = "0";
        TextBox myPOR_51 = (TextBox)FormView1.FindControl("text_POR51");
        myPOR_51.Text = "0";
        TextBox myPOR_52 = (TextBox)FormView1.FindControl("text_POR52");
        myPOR_52.Text = "0";
        TextBox myPOR_53 = (TextBox)FormView1.FindControl("text_POR53");
        myPOR_53.Text = "0";
        TextBox myPOR_54 = (TextBox)FormView1.FindControl("text_POR54");
        myPOR_54.Text = "";
        TextBox myPOR_55 = (TextBox)FormView1.FindControl("text_POR55");
        myPOR_55.Text = "";
      
        lblError.Text = "";        
    }
   
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

        DropDownList myddPOR_01 = (DropDownList)FormView1.FindControl("ddPOR01");    //Porduction Site
        DropDownList myddPOR_02 = (DropDownList)FormView1.FindControl("ddPOR02");    //PKG
        TextBox myPOR_03 = (TextBox)FormView1.FindControl("text_POR03");            //WaferTech
        //TextBox myPOR_04 = (TextBox)FormView1.FindControl("text_POR04");       
        DropDownList myddPOR_04 = (DropDownList)FormView1.FindControl("ddPOR04");   //FAB
        //TextBox myPOR_05 = (TextBox)FormView1.FindControl("text_POR05");            
        DropDownList myddPOR_05 = (DropDownList)FormView1.FindControl("ddPOR05");   //WaferPSVTypeThickness
        TextBox myPOR_06 = (TextBox)FormView1.FindControl("text_POR06");            //PRType
        DropDownList myddPOR_07 = (DropDownList)FormView1.FindControl("ddPOR07");    //TietchingChemical
        DropDownList myddPOR_08 = (DropDownList)FormView1.FindControl("ddPOR08");    //TinShellBake
        DropDownList myddPOR_09 = (DropDownList)FormView1.FindControl("ddPOR09");    //PIRougness
        DropDownList myddPOR_10 = (DropDownList)FormView1.FindControl("ddPOR10");    //BumpResistanceCapability
        DropDownList myddPOR_11 = (DropDownList)FormView1.FindControl("ddPOR11");    //推大球
        DropDownList myddPOR_12 = (DropDownList)FormView1.FindControl("ddPOR12");    //LowK
        DropDownList myddPOR_13 = (DropDownList)FormView1.FindControl("ddPOR13");    //PRThickness
        DropDownList myddPOR_14 = (DropDownList)FormView1.FindControl("ddPOR14");    //Customer
        //------------POR_15 For Pord---------------------------
        Label mylab_Pord = (Label)FormView1.FindControl("lab_Pord");       
        DropDownList myddPord = (DropDownList)FormView1.FindControl("ddPord");
        TextBox mytext_No = (TextBox)FormView1.FindControl("text_No");        
        string myPOR_15 = mylab_Pord.Text + "-" + myddPord.SelectedValue.ToString() + "-" + mytext_No.Text;        
        //------------For Pord End-----------------------
        DropDownList myddPOR_16 = (DropDownList)FormView1.FindControl("ddPOR16");        //UBMTypeThickness
        TextBox myPOR_17 = (TextBox)FormView1.FindControl("text_POR17");                //Device                       
        Label myPOR_18 = (Label)FormView1.FindControl("labPOR18");        
        TextBox myPOR_19 = (TextBox)FormView1.FindControl("text_POR19");        
        DropDownList myddPOR_20 = (DropDownList)FormView1.FindControl("ddPOR20");        
        DropDownList myddPOR_21 = (DropDownList)FormView1.FindControl("ddPOR21");        
        TextBox myPOR_22 = (TextBox)FormView1.FindControl("text_POR22");        
        DropDownList myddPOR_23 = (DropDownList)FormView1.FindControl("ddPOR23");        
        DropDownList myddPOR_24 = (DropDownList)FormView1.FindControl("ddPOR24");        
        DropDownList myddPOR_25 = (DropDownList)FormView1.FindControl("ddPOR25");        
        DropDownList myddPOR_26 = (DropDownList)FormView1.FindControl("ddPOR26");        
        TextBox myPOR_27 = (TextBox)FormView1.FindControl("text_POR27");        
        DropDownList myddPOR_28 = (DropDownList)FormView1.FindControl("ddPOR28");        
        TextBox myPOR_29 = (TextBox)FormView1.FindControl("text_POR29");        
        TextBox myPOR_30 = (TextBox)FormView1.FindControl("text_POR30");
        TextBox myPOR_31 = (TextBox)FormView1.FindControl("text_POR31");
        TextBox myPOR_32 = (TextBox)FormView1.FindControl("text_POR32");
        DropDownList myddPOR_33 = (DropDownList)FormView1.FindControl("ddPOR33");
        TextBox myPOR_34 = (TextBox)FormView1.FindControl("text_POR34");
        TextBox myPOR_35 = (TextBox)FormView1.FindControl("text_POR35");
        TextBox myPOR_36 = (TextBox)FormView1.FindControl("text_POR36");
        TextBox myPOR_37 = (TextBox)FormView1.FindControl("text_POR37");
        //POR38,POR39,POR40,POR41 Default Text = "NA"-----------------------------------------------
        TextBox myPOR_38 = (TextBox)FormView1.FindControl("text_POR38");
        myPOR_38.Text = "NA";
        TextBox myPOR_39 = (TextBox)FormView1.FindControl("text_POR39");
        myPOR_39.Text = "NA";
        TextBox myPOR_40 = (TextBox)FormView1.FindControl("text_POR40");
        myPOR_40.Text = "NA";
        TextBox myPOR_41 = (TextBox)FormView1.FindControl("text_POR41");
        myPOR_41.Text = "NA";
        //------------------------------------------------------------------
        TextBox myPOR_42 = (TextBox)FormView1.FindControl("text_POR42");
        TextBox myPOR_43 = (TextBox)FormView1.FindControl("text_POR43");
        TextBox myPOR_44 = (TextBox)FormView1.FindControl("text_POR44");
        TextBox myPOR_45 = (TextBox)FormView1.FindControl("text_POR45");
        DropDownList myddPOR_46 = (DropDownList)FormView1.FindControl("ddPOR46");
        TextBox myPOR_47 = (TextBox)FormView1.FindControl("text_POR47");
        Label myPOR_48 = (Label)FormView1.FindControl("labPOR48");  //套公式: POR19-POR45
        Label myPOR_49 = (Label)FormView1.FindControl("labPOR49");  //套公式: POR29/POR36
        Label myPOR_50 = (Label)FormView1.FindControl("labPOR50");  //套公式: (Math.PI * (POR29 * POR29/4) * POR53) / (POR51 * POR52) * 1000000
        TextBox myPOR_51 = (TextBox)FormView1.FindControl("text_POR51");
        TextBox myPOR_52 = (TextBox)FormView1.FindControl("text_POR52");
        TextBox myPOR_53 = (TextBox)FormView1.FindControl("text_POR53");
        TextBox myPOR_54 = (TextBox)FormView1.FindControl("text_POR54");
        TextBox myPOR_55 = (TextBox)FormView1.FindControl("text_POR55");

        clsMySQL db = new clsMySQL();
        DataSet ds;
        if (myddPOR_14.SelectedValue.ToString() == "")
        {
            RegisterStartupScript("訊息通知", "<script> alert('必須填寫POR Customer!!');</script>");
        }
        else
        {
            if (myPOR_17.Text == "")
            {
                RegisterStartupScript("訊息通知", "<script> alert('必須填寫POR Device!!');</script>");
            }
            else
            {
                if (myddPOR_01.SelectedValue.ToString() == "")
                {
                    RegisterStartupScript("訊息通知", "<script> alert('必須填寫Production Site!!');</script>");
                }
                else
                {
                    string strSQL_Query = "select * from npipor where POR_Customer = '" + myddPOR_14.SelectedValue.ToString() + "' and POR_Device = '" + myPOR_17.Text + "' and POR_01 = '" + myddPOR_01.SelectedValue.ToString() + "'";
                    ds = db.QueryDataSet(strSQL_Query);
                    try
                    {
                        string strSQL_Insert = string.Format("Insert into npiPor " +
                                                            "(POR_Customer, POR_Device, Stype, UpdateTime, npiUser, POR_Status," +
                                                            "POR_01, POR_02, POR_03, POR_04, POR_05, POR_06, POR_07, POR_08, POR_09, POR_10," +
                                                            "POR_11, POR_12, POR_13, POR_14, POR_15, POR_16, POR_17, POR_18, POR_19, POR_20, POR_21, POR_22," +
                                                            "POR_23, POR_24, POR_25, POR_26, POR_27, POR_28, POR_29, POR_30, POR_31, POR_32, POR_33, POR_34," +
                                                            "POR_35, POR_36, POR_37, POR_38, POR_39, POR_40, POR_41, POR_42, POR_43, POR_44, POR_45, POR_46," +
                                                            "POR_47, POR_48, POR_49, POR_50, POR_51, POR_52, POR_53, POR_54, POR_55)values " +
                                                            "('{13}','{16}','POR',NOW(),'','Y','{0}','{1}','{2}','{3}','{4}'," +
                                                            "'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}'," +
                                                            "'{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}'," +
                                                            "'{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}'," +
                                                            "'{54}')",
                                                            myddPOR_01.SelectedValue.ToString().Trim(), myddPOR_02.SelectedValue.ToString().Trim(), myPOR_03.Text.Trim(), myddPOR_04.SelectedValue.ToString().Trim(),
                                                            myddPOR_05.SelectedValue.ToString().Trim(), myPOR_06.Text.Trim(), myddPOR_07.SelectedValue.ToString().Trim(), myddPOR_08.SelectedValue.ToString().Trim(),
                                                            myddPOR_09.SelectedValue.ToString().Trim(), myddPOR_10.SelectedValue.ToString().Trim(), myddPOR_11.SelectedValue.ToString().Trim(), myddPOR_12.SelectedValue.ToString().Trim(),
                                                            myddPOR_13.SelectedValue.ToString().Trim(), myddPOR_14.SelectedValue.ToString().Trim(), myPOR_15.Trim(), myddPOR_16.SelectedValue.ToString().Trim(), myPOR_17.Text.Trim(),
                                                            myPOR_18.Text.Trim(), myPOR_19.Text.Trim(), myddPOR_20.SelectedValue.ToString().Trim(), myddPOR_21.SelectedValue.ToString().Trim(), myPOR_22.Text.Trim(),
                                                            myddPOR_23.SelectedValue.ToString().Trim(), myddPOR_24.SelectedValue.ToString().Trim(), myddPOR_25.SelectedValue.ToString().Trim(), myddPOR_26.SelectedValue.ToString().Trim(),
                                                            myPOR_27.Text.Trim(), myddPOR_28.SelectedValue.ToString().Trim(), myPOR_29.Text.Trim(), myPOR_30.Text.Trim(), myPOR_31.Text.Trim(), myPOR_32.Text.Trim(),
                                                            myddPOR_33.SelectedValue.ToString().Trim(), myPOR_34.Text.Trim(), myPOR_35.Text.Trim(), myPOR_36.Text.Trim(), myPOR_37.Text.Trim(), myPOR_38.Text.Trim(),
                                                            myPOR_39.Text.Trim(), myPOR_40.Text.Trim(), myPOR_41.Text.Trim(), myPOR_42.Text.Trim(), myPOR_43.Text.Trim(), myPOR_44.Text.Trim(), myPOR_45.Text.Trim(),
                                                            myddPOR_46.SelectedValue.ToString().Trim(), myPOR_47.Text.Trim(), myPOR_48.Text.Trim(), myPOR_49.Text.Trim(), myPOR_50.Text.Trim(), myPOR_51.Text.Trim(),
                                                            myPOR_52.Text.Trim(), myPOR_53.Text.Trim(), myPOR_54.Text.Trim(), myPOR_55.Text.Trim());

                        //lblError.Text = strSQL_Insert;
                        if (db.QueryExecuteNonQuery(strSQL_Insert))
                        {
                            RegisterStartupScript("訊息通知", "<script> alert('POR資料新增，成功！！');</script>");
                            ClearTextBox();
                            ChangeViewMode();
                        }
                        else
                        {
                            //lblError.Text = strSQL_Insert;
                            RegisterStartupScript("訊息通知", "<script> alert('POR資料新增，失敗！！');</script>");
                        }

                    }
                    catch (Exception ex)
                    {
                        ClearTextBox();
                        lblError.Text = "[POR Insert Error Message]:" + ex.ToString();
                    }
                }                
            }
        }
    }
    protected void ddPOR02_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label mylab_Pord = (Label)FormView1.FindControl("lab_Pord");
        DropDownList myddPOR_02 = (DropDownList)FormView1.FindControl("ddPOR02");    //PKG
        if (myddPOR_02.SelectedIndex != 0)
        {
            string[] str1 = myddPOR_02.SelectedValue.ToString().Split('-');
            string[] str2 = str1[0].Split(' ');
            switch (str2[1])
            {
                case "REPSV":
                    mylab_Pord.Text = str1[1] + "-BP-PSV-" + str2[0] + "-" + str1[2] + "-PI-TCN";
                    break;
                case "FOC":
                    mylab_Pord.Text = str1[1] + "-BP-FOC-" + str2[0] + "-" + str1[2] + "-TCN";
                    break;
                default:
                    break;
            }
        }
        else 
        {
            mylab_Pord.Text = "";
        }
    }  
   
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {        
        GridViewRow gvrow = GridView1.Rows[e.NewSelectedIndex];
        clsMySQL db = new clsMySQL();        
        //string strSQL = "select * from npipor where POR_Customer = '" + gvrow.Cells[1].Text + "' and POR_Device = '" + gvrow.Cells[2].Text + "'" + " and POR_01 = '" + gvrow.Cells[3].Text + "'";
        string strSQL = string.Format("select * from npipor where POR_Customer='{0}' and POR_Device='{1}' and POR_01='{2}' and POR_02='{3}' and POR_03='{4}' and POR_04='{5}' " +
                                      "and POR_05='{6}' and POR_11='{7}'",
                                      gvrow.Cells[1].Text, gvrow.Cells[2].Text, gvrow.Cells[3].Text, gvrow.Cells[4].Text, gvrow.Cells[5].Text, gvrow.Cells[6].Text,
                                      gvrow.Cells[7].Text, gvrow.Cells[8].Text);
        FormView1.Visible = true;
        Panel2.Visible = true;        
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

    protected void text_POR45_TextChanged(object sender, EventArgs e)
    {
        TextBox tmpPOR19 = (TextBox)FormView1.FindControl("text_POR19");
        TextBox tmpPOR45 = (TextBox)FormView1.FindControl("text_POR45");
        Label tmpPOR48 = (Label)FormView1.FindControl("labPOR48");

        try 
        {
            double d_POR19 = Convert.ToDouble(tmpPOR19.Text.Trim());
            double d_POR45 = Convert.ToDouble(tmpPOR45.Text.Trim());
            tmpPOR48.Text = Math.Round((d_POR19 - d_POR45), 2).ToString();
        }
        catch (Exception ex) 
        {
            lblError.Text = "[Error Message-text_POR45_TextChanged]:您輸入MinBumpPitch= " + tmpPOR19.Text + "或是 BumpDiameter= " + tmpPOR45.Text + "不是正確的數字資料"; // + ex.ToString();
        }
        
    }
    protected void text_POR36_TextChanged(object sender, EventArgs e)
    {
        TextBox tmpPOR29 = (TextBox)FormView1.FindControl("text_POR29");
        TextBox tmpPOR36 = (TextBox)FormView1.FindControl("text_POR36");
        Label tmpPOR49 = (Label)FormView1.FindControl("labPOR49");
        try 
        {
            double d_POR29 = Convert.ToDouble(tmpPOR29.Text.Trim());
            double d_POR36 = Convert.ToDouble(tmpPOR36.Text.Trim());
            if (d_POR29 != 0 && d_POR36 != 0)
            {
                tmpPOR49.Text = Math.Round((d_POR29 / d_POR36), 0).ToString();
            }
            else 
            {
                tmpPOR49.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "[Error Message-text_POR49_TextChanged]:您輸入UBMSize= " + tmpPOR29.Text + "或是UBM/SMORatio = " + tmpPOR36.Text + "不是正確的資料"; // + ex.ToString();
        }

    }
    protected void text_POR53_TextChanged(object sender, EventArgs e)
    {
        TextBox tmpPOR29 = (TextBox)FormView1.FindControl("text_POR29");
        TextBox tmpPOR51 = (TextBox)FormView1.FindControl("text_POR51");
        TextBox tmpPOR52 = (TextBox)FormView1.FindControl("text_POR52");
        TextBox tmpPOR53 = (TextBox)FormView1.FindControl("text_POR53");
        Label tmpPOR50 = (Label)FormView1.FindControl("labPOR50");

        try
        {
            double d_POR29 = Convert.ToDouble(tmpPOR29.Text.Trim());
            double d_POR51 = Convert.ToDouble(tmpPOR51.Text.Trim());
            double d_POR52 = Convert.ToDouble(tmpPOR52.Text.Trim());
            double d_POR53 = Convert.ToDouble(tmpPOR53.Text.Trim());
            if (d_POR29 != 0 && d_POR51 != 0 && d_POR52 != 0 && d_POR53 != 0)
            {
                tmpPOR50.Text = Math.Round(((Math.PI * (d_POR29 * d_POR29 / 4) * d_POR53) / (d_POR51 * d_POR52 * 1000000))*100, 2).ToString();
            }
            else 
            {
                tmpPOR50.Text = "0" ;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "[Error Message-text_POR50_TextChanged]:您輸入UBMSize= " + tmpPOR29.Text + "或是UBM/SMORatio = " + tmpPOR51.Text + "不是正確的資料"; // + ex.ToString();
        }
        
    }
    protected void text_POR52_TextChanged(object sender, EventArgs e)
    {
        TextBox tmpPOR51 = (TextBox)FormView1.FindControl("text_POR51");
        TextBox tmpPOR52 = (TextBox)FormView1.FindControl("text_POR52");
        Label tmpPOR18 = (Label)FormView1.FindControl("labPOR18");

        tmpPOR18.Text = tmpPOR51.Text.Trim() + "*" + tmpPOR52.Text.Trim();
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        clsMySQL db = new clsMySQL();
        Boolean bisWhereExist = false;
        string strQuerySQL = "";
        lblError.Text = "";
        if (ddl_Site.SelectedValue != "0" || ddl_PSV.SelectedValue != "0" || ddl_RSVI.SelectedValue != "0" || text_WaferTech.Text.Trim() != "" || text_Cust.Text.Trim() != "" || ddl_Fab.SelectedValue != "0" || text_PKG.Text.Trim() != "")
        {
            strQuerySQL = "Select * from npiPOR Where Stype='POR' ";
            bisWhereExist = true;
        }
        else
        {
            strQuerySQL = "Select * from npiPOR ";
        }
        if (ddl_Site.SelectedValue != "0")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_01 like'" + ddl_Site.SelectedValue + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_01 like'" + ddl_Site.SelectedValue + "%' ";
            }         
        }
       
        if (text_PKG.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_02 like'" + text_PKG.Text.Trim() + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_02 like'" + text_PKG.Text.Trim() + "%' ";
            }
        }
        if (text_WaferTech.Text.Trim() !="")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_03 like'" + text_WaferTech.Text.Trim() + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_03 like'" + text_WaferTech.Text.Trim() + "%' ";
            }
        }
        if (ddl_Fab.SelectedValue != "0")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_04 like'" + ddl_Fab.SelectedValue + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_04 like'" + ddl_Fab.SelectedValue + "%' ";
            }
        }
        if (ddl_PSV.SelectedValue != "0")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_05 like'" + ddl_PSV.SelectedValue + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_05 like'" + ddl_PSV.SelectedValue + "%'";
            }
        }
        if (ddl_RSVI.SelectedValue != "0")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_11 like'" + ddl_RSVI.SelectedValue + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_11 like'" + ddl_RSVI.SelectedValue + "%' ";
            }
        }
        if (text_Cust.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_14 like'" + text_Cust.Text.Trim() + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_14 like'" + text_Cust.Text.Trim() + "%' ";
            }
        }
        //lblError.Text = strQuerySQL;
        try
        {
            clsMySQL.DBReply dr = db.QueryDS(strQuerySQL);
            GridView1.DataSource = dr.dsDataSet.Tables[0].DefaultView;
            GridView1.DataBind();
            db.Close();
        }
        catch (Exception ex)
        {
            lblError.Text = "[NPIPOR Function-Error Message, btn_Search_Click] : " + ex.ToString();
        }
    }
    protected void cmdFilter_Click(object sender, EventArgs e)
    {
        ChangeViewMode();
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        //更新後，切換為瀏覽模式
        ChangeViewMode();  
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //----編輯模式----
        GridView1.EditIndex = e.NewEditIndex;
        GridViewRow gvrow = GridView1.Rows[e.NewEditIndex];
        clsMySQL db = new clsMySQL();
        FormView1.Visible = true;
        Panel2.Visible = true;

        if (!FormView1.Visible)
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

        //string strSQL = "select * from npiPOR where POR_Customer = '" + gvrow.Cells[1].Text + "' and POR_Device = '" + gvrow.Cells[2].Text + "'";
        string strSQL = string.Format("select * from npipor where POR_Customer='{0}' and POR_Device='{1}' and POR_01='{2}' and POR_02='{3}' and POR_03='{4}' and POR_04='{5}' " +
                                     "and POR_05='{6}' and POR_11='{7}'",
                                     gvrow.Cells[1].Text, gvrow.Cells[2].Text, gvrow.Cells[3].Text, gvrow.Cells[4].Text, gvrow.Cells[5].Text, gvrow.Cells[6].Text,
                                     gvrow.Cells[7].Text, gvrow.Cells[8].Text);
        FormView1.DataSource = db.QueryDataSet(strSQL);
        FormView1.DataBind();
    }
}