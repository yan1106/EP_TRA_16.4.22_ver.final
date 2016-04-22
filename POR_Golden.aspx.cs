using System;
using System.Data.SqlClient;
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

public partial class _Default : System.Web.UI.Page
{
    public string srcCtl = "";
    public string srcForm = "";
    public List<string> por_golden_data = new List<string>();
    public string strQuerySQL = "";
    public string sql1 = "select DISTINCT POR_01 from npipor";
    /*
    public void ReturnOpener()
    {
        string jsStr = "";
        jsStr += "window.opener." + srcForm + "." + srcCtl + ".value = ";
        jsStr += "window.opener." + srcForm + "." + srcCtl + ".value = form1." + por_golden_data[1] + ".value; ";
        jsStr += "window.close();";
        jsStr += "self.opener." + srcForm + ".submit();";
        ClientScript.RegisterStartupScript(this.GetType(), "ret", jsStr, true);

    }
    */

    [System.Web.Services.WebMethod]
    public static string[] GetPKG(string prefix)
    {
        List<string> PKG = new List<string>();
       
        string strSQL = " select DISTINCT POR_02 from npipor where POR_02 like '" + prefix + "%'"; 


        clsMySQL db = new clsMySQL();
        foreach (DataRow dr in db.QueryDataSet(strSQL).Tables[0].Rows)
        {
            //customers.Add(string.Format("{0},{1}", dr["new_customer"], dr["new_device"]));
            PKG.Add(string.Format("{0}", dr["POR_02"]));
        }
        return PKG.ToArray();
    }
  
   /* public static string[] GetNewDevice(string prefix)
    {
        List<string> New_Device = new List<string>();
        string strSQL = " select DISTINCT npiapp.New_Device from npiapp where npiapp.New_Device like '" + prefix + "%' union  select  npiimportdata.New_Device from  npiimportdata where   npiimportdata.New_Device like '" + prefix + "%' union select  npimanual.New_Device from npimanual where npimanual.New_Device like'" + prefix + "%'";
        clsMySQL db = new clsMySQL();
        foreach (DataRow dr in db.QueryDataSet(strSQL).Tables[0].Rows)
        {
            //customers.Add(string.Format("{0},{1}", dr["new_customer"], dr["new_device"]));
            New_Device.Add(string.Format("{0}", dr["New_Device"]));
        }
        return New_Device.ToArray();
    }

        */






    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            put_prosite_data(sql1);


        }
    }

    
  

    protected void GridView1_RowCommand(object sender,GridViewCommandEventArgs e)
    {
        int index;
        int i = 0;
        index = Convert.ToInt32(e.CommandArgument);
        GridViewRow selecteRow = GridView1.Rows[index];
        TableCell productName_Device = selecteRow.Cells[1];
        TableCell productName_Production_Site = selecteRow.Cells[2];
        TableCell productName_PKG = selecteRow.Cells[3];
        TableCell productName_Wafer = selecteRow.Cells[4];
        TableCell productName_fab = selecteRow.Cells[5];
        TableCell productWaferPSV = selecteRow.Cells[6];
        TableCell productRVSI = selecteRow.Cells[7];
        TableCell productCustomer = selecteRow.Cells[8];
       


        if (e.CommandName == "yan")
        {

            
             /*por_golden_data.Add(productName_Device.Text);
             por_golden_data.Add(productName_Production_Site.Text);
             por_golden_data.Add(productName_PKG.Text);
             por_golden_data.Add(productName_Wafer.Text);
             por_golden_data.Add(productName_fab.Text);
             por_golden_data.Add(productWaferPSV.Text);
             por_golden_data.Add(productRVSI.Text);
             por_golden_data.Add(productCustomer.Text);
             */

            /* string Device = "Device=" + Server.HtmlEncode(productName_Device.Text);
              string Production_Site = "Production_Site=" + Server.HtmlEncode(productName_Production_Site.Text);
              string PKG = "PKG=" + Server.HtmlEncode(productName_PKG.Text);
              string Wafer = "Wafer=" + Server.HtmlEncode(productName_Wafer.Text);
              string fab = "fab=" + Server.HtmlEncode(productName_fab.Text);
              string WaferPSV = "WaferPSV=" + Server.HtmlEncode(productWaferPSV.Text);
              string RVSI = "RVSI=" + Server.HtmlEncode(productRVSI.Text);
              string Customer = "Customer=" + Server.HtmlEncode(productCustomer.Text);

              string connn = Device + "&" + Production_Site + "&" + PKG + "&" + Wafer + "&" + fab + "&" + WaferPSV + "&" + RVSI + "&" + Customer;

              Server.Transfer("EP_TRA.aspx?" + connn);
              */

            string strScript = "";
            string arydata = "";

            string pass_message = "'確定將以下八個條件帶入EP_TRA中?\\nDevice:" + productName_Device.Text + "\\nProduction_Site:" + productName_Production_Site.Text + 
                                "\\nPKG:" + productName_PKG.Text  + "\\nWafer Tech.(nm) :" + productName_Wafer.Text  + "\\nFAB:" + productName_fab.Text
                                + "\\nWafer PSV type / Thickness:" + productWaferPSV.Text + "\\nRVSI:" + productRVSI.Text + "\\nCustomer:" + productCustomer.Text
                                + "\\nWafer PSV type / Thickness:" + productWaferPSV.Text  + "\\nRVSI:" + productRVSI.Text + "\\nCustomer:" + productCustomer.Text+"','";



            arydata = productName_Device.Text + "|" + productName_Production_Site.Text + "|" + productName_PKG.Text + "|" + productName_Wafer.Text
                +"|"+ productName_fab.Text +"|" + productWaferPSV.Text +"|" + productRVSI.Text +"|" + productCustomer.Text;
            strScript = "<script language='javascript'>Confirm("+ pass_message + arydata + "');</script>";
            //strScript = "<script language='javascript'>Confirm('確定？','" + arydata + "');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);


            /*string strScript = string.Format("<script language='javascript'>error_msg('查無此資料，請再重新選擇POR Golden 條件!');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);*/



            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('"+ "'Device:" + productName_Device.Text +"')", true);


            /* for (i=0;i<8;i++)
             {
                 Response.Write(por_golden_data[i]+"<br />");
             }*/
            // Response.Write("<script>window.open('POR_Golden.aspx','POR_Golden_Condition',config='height=1024,width=1100');</script>");


            /*
             Response.Redirect("~/EP_TRA.aspx?Device="+this.por_golden_data[0] +"Production_Site=" + this.por_golden_data[1] + "PKG=" + this.por_golden_data[2] +
                  "Wafer=" + this.por_golden_data[3] + "Name_fab=" + this.por_golden_data[4] + "WaferPSV=" + this.por_golden_data[5] +
                  "RVSI=" + this.por_golden_data[6] + "Customer=" + this.por_golden_data[7]);
             */



            /*Response.Redirect("~/EP_TRA.aspx?" + this.por_golden_data[1]  + this.por_golden_data[2] +
                 this.por_golden_data[3] +  this.por_golden_data[4] +  this.por_golden_data[5] +
                 this.por_golden_data[6]  + this.por_golden_data[7]);*/





        }
    }

    [System.Web.Services.WebMethod(EnableSession = true)]

    public static string CreateJob(string strConjData)
    {
        string strError = "";
        string[] strData = new string[1];
        strData = strConjData.Split('|');
        HttpContext.Current.Session["value1"] = strData[0];
        HttpContext.Current.Session["value2"] = strData[1];
        HttpContext.Current.Session["value3"] = strData[2];
        HttpContext.Current.Session["value4"] = strData[3];
        HttpContext.Current.Session["value5"] = strData[4];
        HttpContext.Current.Session["value6"] = strData[5];
        HttpContext.Current.Session["value7"] = strData[6];
        HttpContext.Current.Session["value8"] = strData[7];
        return strError;
    }


    protected void Search_Por_Sql(string porsql)
    {

        clsMySQL db = new clsMySQL(); //Connection MySQL
        clsMySQL.DBReply dr = db.QueryDS(porsql);
        GridView1.DataSource = dr.dsDataSet.Tables[0].DefaultView;
        GridView1.DataBind();
        db.Close();



    }


    protected void put_prosite_data(string mySQL)
    {
        string TableFild = "";
        int FieldCunt = 0;


        // clsMySQL db = new clsMySQL();

        //MySqlDataReader mydr = db.QueryDataReader(mySQL);
        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();

        while (mydr.Read())
        {
            for (FieldCunt = 0; FieldCunt <= 0; FieldCunt++)
            {

                TableFild = mydr.GetString(0);
                //temp_Data.Add(TableFild);
                ProSite_ddl.Items.Add(TableFild);
                //FieldCunt++;

            }

        }
        mydr.Close();
        MySqlConn.Close();
    }

    protected Boolean jude_pordata(string mySQL)
    {
        string TableFild = "";
        int FieldCunt = 0;


        // clsMySQL db = new clsMySQL();

        //MySqlDataReader mydr = db.QueryDataReader(mySQL);
        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();

        while (mydr.Read())
        {
            for (FieldCunt = 0; FieldCunt <= 0; FieldCunt++)
            {

                TableFild = mydr.GetString(0);
                //temp_Data.Add(TableFild);            
                //FieldCunt++;

            }

        }
        mydr.Close();
        MySqlConn.Close();

        if (TableFild == "")
            return false;
        else
            return true;



    }

    protected void put_POR_Data(string sql)
    {
        clsMySQL db = new clsMySQL();

        clsMySQL.DBReply dr = db.QueryDS(sql);
        GridView1.DataSource = dr.dsDataSet.Tables[0].DefaultView;
        GridView1.DataBind();
        db.Close();
   
    }




    protected void Search_por_Click(object sender, EventArgs e)
    {
      
        GridView1.Visible = true;
       



       
        Boolean bisWhereExist = false;
        
        string jude_Sql = "";
       
 
        if (TB_PKG.Text.Trim() != "" || TB_WT.Text.Trim() != "" ||
             TB_FAB.Text.Trim() != "" || TB_PSV.Text.Trim() != "" || ddl_RVSI.SelectedIndex!= 0 ||
             TB_Customer.Text.Trim() != "" || ProSite_ddl.SelectedIndex != 0)
        {
            strQuerySQL = "Select * from npipor Where Stype='POR' ";
            jude_Sql= "Select POR_Customer from npipor Where Stype='POR' ";
            bisWhereExist = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('請輸入POR Golden 條件!')", true);
        }
        if (TB_PKG.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_02 like'" + TB_PKG.Text + "%' ";
                jude_Sql = jude_Sql + "and POR_02 like'" + TB_PKG.Text + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_01 like'" + TB_PKG.Text + "%' ";
                jude_Sql = jude_Sql + "Where POR_01 like'" + TB_PKG.Text + "%' ";
            }         
        }
       
        if (TB_WT.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_03 like'" + TB_WT.Text + "%' ";
                jude_Sql = jude_Sql + "and POR_03 like'" + TB_WT.Text + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_03 like'" + TB_WT.Text + "%' ";
                jude_Sql = jude_Sql + "Where POR_03 like'" + TB_WT.Text + "%' ";
            }
        }
        if (TB_FAB.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_04 like'" + TB_FAB.Text + "%' ";
                jude_Sql = jude_Sql + "and POR_04 like'" + TB_FAB.Text + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_04 like'" + TB_FAB.Text + "%' ";
                jude_Sql = jude_Sql + "Where POR_04 like'" + TB_FAB.Text + "%' ";
            }
        }
        if (TB_PSV.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_05 like'" + TB_PSV.Text + "%' ";
                jude_Sql = jude_Sql + "and POR_05 like'" + TB_PSV.Text + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_04 like'" + TB_PSV.Text + "%' ";
                jude_Sql = jude_Sql + "Where POR_04 like'" + TB_PSV.Text + "%' ";
            }
        }
        if (ddl_RVSI.SelectedIndex != 0)
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_11 like'" + ddl_RVSI.SelectedValue + "%' ";
                jude_Sql = jude_Sql + "and POR_11 like'" + ddl_RVSI.SelectedValue + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_11 like'" + ddl_RVSI.SelectedValue + "%'";
                jude_Sql = jude_Sql + "Where POR_11 like'" + ddl_RVSI.SelectedValue + "%'";
            }
        }
        if (TB_Customer.Text.Trim() != "")
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_14 like'" + TB_Customer.Text + "%' ";
                jude_Sql = strQuerySQL + "and POR_14 like'" + TB_Customer.Text + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_14 like'" + TB_Customer.Text + "%' ";
                jude_Sql = jude_Sql + "Where POR_14 like'" + TB_Customer.Text + "%' ";
            }
        }
        if (ProSite_ddl.SelectedIndex != 0)
        {
            if (bisWhereExist)
            {
                strQuerySQL = strQuerySQL + "and POR_01 like'" + ProSite_ddl.SelectedValue + "%' ";
                jude_Sql = strQuerySQL + "and POR_01 like'" + ProSite_ddl.SelectedValue + "%' ";
            }
            else
            {
                strQuerySQL = strQuerySQL + "Where POR_01 like'" + ProSite_ddl.SelectedValue + "%' ";
                jude_Sql = jude_Sql + "Where POR_01 like'" + ProSite_ddl.SelectedValue + "%' ";
            }
        }
        //lblError.Text = strQuerySQL;
        try
        {
            

            if (jude_pordata(jude_Sql))
            {
                put_POR_Data(strQuerySQL);
                HttpContext.Current.Session["value_sign_sql"] = strQuerySQL;
            }
            else
            {

                string strScript = string.Format("<script language='javascript'>error_msg('查無此資料，請再重新選擇POR Golden 條件!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);              
                GridView1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //lblError.Text = "[NPIPOR Function-Error Message, btn_Search_Click] : " + ex.ToString();
        }

    }

    
  





  

  
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string temp = Session["value_sign_sql"].ToString();
        GridView1.PageIndex = e.NewPageIndex;
        put_POR_Data(temp);
    }

    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        string temp = Session["value_sign_sql"].ToString();
        put_POR_Data(temp);
    }

   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}