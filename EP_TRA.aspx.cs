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



public partial class Default : System.Web.UI.Page
{

    MySql.Data.MySqlClient.MySqlConnection conn;
    public List<string> temp_Data = new List<string>();
    public List<string> por_golden_condition = new List<string>();
    public List<string> por_golden_data = new List<string>();
    public List<string> npimanual_data = new List<string>();
    public List<string> npiapp_data = new List<string>();
    public List<string> npiimport_data = new List<string>();
    public List<string> Str_Effect = new List<string>();
    public List<string> Str_Poten = new List<string>();
    public List<string> data_cap = new List<string>();
    public Boolean sign_POR = false;
    public Boolean sign_NewDevice = false;



   // [System.Web.Services.WebMethod(EnableSession = true)]
    [System.Web.Services.WebMethod]
    public static string[] GetCustomer(string prefix)
    {
        List<string> Customer = new List<string>();
        string strSQL2 = "select DISTINCT npiapp.New_Customer,npiimportdata.New_Customer,npimanual.New_Customer  from npiapp,npiimportdata,npimanual Where npiapp.New_Customer Like '" + prefix + "%' ";
        string strSQL = " select DISTINCT npiapp.New_Customer from npiapp where npiapp.New_Customer like '" + prefix + "%' union  select  npiimportdata.New_Customer from  npiimportdata where   npiimportdata.New_Customer like '" + prefix + "%' union select  npimanual.New_Customer from npimanual where npimanual.New_Customer like'" + prefix + "%'";



 

        clsMySQL db = new clsMySQL();
        foreach (DataRow dr in db.QueryDataSet(strSQL).Tables[0].Rows)
        {
            //customers.Add(string.Format("{0},{1}", dr["new_customer"], dr["new_device"]));
            Customer.Add(string.Format("{0}", dr["New_Customer"]));
        }
        return Customer.ToArray();
    }
    [System.Web.Services.WebMethod]
    public static string[] GetNewDevice(string prefix)
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
   
    
   








    protected void DBInit()
    {
        string strSQL = string.Format("SELECT * FROM npiimportdata");
        try
        {
            clsMySQL db = new clsMySQL(); //Connection MySQL
            clsMySQL.DBReply dr = db.QueryDS(strSQL);
            db.Close();
        }
        catch (Exception ex)
        {
            lblError.Text = "Exception Error Message----  " + ex.ToString() + ">>>>>>>>>>" + strSQL;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!Page.IsPostBack)
        {
            //DBInit();
            Panel1.Visible = false;
            Panel2.Visible = false;
            // HttpContext.Current.Session["value_sign2"] = sign_POR;
            //HttpContext.Current.Session["value_sign1"] = sign_NewDevice;
          
        }


        if (Customer_TB.Text.Trim() == "" && ND_TB.Text.Trim() == "")
        {
            HttpContext.Current.Session["value_sign1"] = false;
        }
        else if (Customer_TB.Text.Trim() != "" && ND_TB.Text.Trim() == "")
        {
            HttpContext.Current.Session["value_sign1"] = false;
        }
        else if (Customer_TB.Text.Trim() == "" && ND_TB.Text.Trim() != "")
        {
            HttpContext.Current.Session["value_sign1"] = false;
        }
        else
        {
            HttpContext.Current.Session["value_sign1"] = true;
        }




    }

    protected void put_effect_data()
    {

        Str_Effect.Add("Assembly");
        Str_Effect.Add("Reliability");
        Str_Effect.Add("CP");
        Str_Effect.Add("UBM<br />");
        Str_Effect.Add("PHOTO");
        Str_Effect.Add("PLAT<br />");
        Str_Effect.Add("PI1<br />");
        Str_Effect.Add("ETCH<br />");
        Str_Effect.Add("DESCUM");
        Str_Effect.Add("2RFL");
        Str_Effect.Add("PPHO");
        Str_Effect.Add("PR STRIP");
        Str_Effect.Add("FT");
        Str_Effect.Add("FV & 2D");
        Str_Effect.Add("2D");
        Str_Effect.Add("FV");
        Str_Effect.Add("3D");
        Str_Effect.Add("BH");
        Str_Effect.Add("RS");
        Str_Effect.Add("Void");
        Str_Effect.Add("Shear test");
        Str_Effect.Add("Pull test");
        Str_Effect.Add("BD");
        Str_Effect.Add("Cop");
        Str_Effect.Add("<br />");

    }

    protected void put_potential_data()
    {
        Str_Poten.Add("Bump crack");//index->0
        Str_Poten.Add("RT fail");//index->1
        Str_Poten.Add("Probe card damage");//index->2
        Str_Poten.Add("UBM poor coverage<br />");//index->3
        Str_Poten.Add("pad damage");//index->4
        Str_Poten.Add("Low k crack");//index->5
        Str_Poten.Add("BS/BP fail");//index->6
        Str_Poten.Add("RT fail");//index->7
        Str_Poten.Add("Pad damage");//index->8
        Str_Poten.Add("1.PI profile"+"<br />"+"&nbsp&nbsp"+ "non-smooth");//index->9
        Str_Poten.Add("2.Metal film"+"<br />"+ "&nbsp&nbsp" + "dis-continuity"+"<br>");//index->10
        Str_Poten.Add("PSV roughness"+"<br />");//index->11
        Str_Poten.Add("PI delam"+"<br />");//index->12

        Str_Poten.Add("poor step coverage<br />");//index->13
        Str_Poten.Add("1.Metal peeling<br />");//index->14
        Str_Poten.Add("2.BH / BC OOS<br />");//index->15
        Str_Poten.Add("Metal peeling<br />");//index->16

        Str_Poten.Add("1.PI CD OOS");//index->17
        Str_Poten.Add("2.PI residue");//index->18
        Str_Poten.Add("3.RS OOS");//index->19
        Str_Poten.Add("4.PI THK abnormal");//index->20
        Str_Poten.Add("5.PI Crack");//index->21
        Str_Poten.Add("6.Abnormal PI profile");//index->22

        Str_Poten.Add("1.BR OOS");//index->23
        Str_Poten.Add("2.UBM dis-connnection<br />&nbsp&nbsp&nbsp&nbsp/poor step coverage");//index->24
        Str_Poten.Add("3.UBM peeling");//index->25

        Str_Poten.Add("1.PI roughness OOS");//index->26
        Str_Poten.Add("2.Metal residue");//index->27
        Str_Poten.Add("3.Undercut OOS");//index->28
        Str_Poten.Add("4.BL OOS");//index->29
        Str_Poten.Add("5.Irregular bump<br />&nbsp&nbsp&nbsp&nbsp/Bump wrinkle");//index->30
        Str_Poten.Add("6.Solder burst");//index->31

        Str_Poten.Add("1.BR OOS");//index->32
        Str_Poten.Add("3.PI damage");//index->33
        Str_Poten.Add("4.PI roughness OOC");//index->34
        Str_Poten.Add("5.PI delamination");//index->35
        Str_Poten.Add("6.BL OOS");//index->36
       

        Str_Poten.Add("1.Glue residual");//index->37
        Str_Poten.Add("2.Bump crack");//index->38
        Str_Poten.Add("3.UBM crackd");//index->39
        Str_Poten.Add("4.Md/UF delam");//index->40

        //UBM type / Thickness (um)
        Str_Poten.Add("1.Metal residue");
        Str_Poten.Add("2.BL OOS");

        //Bump composition
        Str_Poten.Add("Irregular bump");
        Str_Poten.Add("1.Non-wetting");
        Str_Poten.Add("2.Bump bridge");
        //index->1

        /*REPSV PI Opening Size(um) 17*/
        Str_Poten.Add("2.RS OOS");
        Str_Poten.Add("PR bubble");
       
        Str_Poten.Add(""); 
        Str_Poten.Add("");

    }





    protected bool check_Customer_data(string mySQL)
    {
        string TableFild = "";
        int FieldCunt = 0;
        bool sign = false;
        int i;
        List<string> Customerdata = new List<string>();


        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);       
        MySqlConn.Open();


        

        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();
        
        while (SelData.Read())
        {
           

            TableFild = (String)SelData["New_Customer"];
            Customerdata.Add(TableFild);
            FieldCunt++;

        }

        SelData.Close();
        MySqlConn.Close();
       

        for (i = 0; i < FieldCunt; i++) {
            if (Customer_TB.Text == Customerdata[i])
            {
                sign = true;
                break;
            }
            else
                sign = false;
            }
        if (sign)
            return true;
        else
            return false;
        
    }

    protected bool check_NewDevice_data(string mySQL)
    {
        string TableFild = "";
        int FieldCunt = 0;
        bool sign = false;
        int i;
        List<string> NewDevicedata = new List<string>();
        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            TableFild = (String)SelData["New_Device"];
            NewDevicedata.Add(TableFild);
            FieldCunt++;

        }

        SelData.Close();
        MySqlConn.Close();
       

        for (i = 0; i < FieldCunt; i++)
        {
            if (ND_TB.Text == NewDevicedata[i])
            {
                sign = true;
                break;
            }
            else
                sign = false;
        }

        if (sign)
            return true;
        else
            return false;

    }

    protected void POR_Goledn_pickup_data(string mySQL)
    {
        int i,j;
        
         string TableFild = "";
         int FieldCunt = 0;
         

        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

         MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
         MySqlDataReader mydr = MySqlCmd.ExecuteReader();

      


        while (mydr.Read())
        {

            /*for (i = 0; i < 6; i++) {
                for (j = 0; j < 10; j++) {
                    if ((i == 5 && j == 6) || (i == 5 && j == 7) || (i == 5 && j == 8) || (i == 5 && j == 9))
                        break;
                        if (j == 0 && i==0)
                        continue;
                            TableFild = (String)mydr["POR_"+ Convert.ToString(i) + Convert.ToString(j)];
                            if(TableFild == null) {
                        por_golden_data.Add(".");
                            }
                    por_golden_data.Add(TableFild);
                   
                }
            }
            */




           
 
            por_golden_data.Add((String)mydr["POR_15"]);
            por_golden_data.Add((String)mydr["POR_14"]);
            por_golden_data.Add((String)mydr["POR_17"]);
            por_golden_data.Add((String)mydr["POR_18"]);
            por_golden_data.Add((String)mydr["POR_46"]);
            por_golden_data.Add((String)mydr["POR_24"]);
            por_golden_data.Add((String)mydr["POR_04"]);
            por_golden_data.Add((String)mydr["POR_03"]);
            por_golden_data.Add((String)mydr["POR_12"]);
            por_golden_data.Add((String)mydr["POR_20"]); 
            por_golden_data.Add((String)mydr["POR_23"]);
            por_golden_data.Add((String)mydr["POR_21"]);
            por_golden_data.Add((String)mydr["POR_25"]);
            por_golden_data.Add((String)mydr["POR_02"]);
            por_golden_data.Add((String)mydr["POR_26"]);
            por_golden_data.Add((String)mydr["POR_55"]);
            por_golden_data.Add((String)mydr["POR_16"]);
            por_golden_data.Add((String)mydr["POR_33"]);
            por_golden_data.Add((String)mydr["POR_30"]);
            por_golden_data.Add((String)mydr["POR_31"]);
            por_golden_data.Add((String)mydr["POR_32"]);
            por_golden_data.Add((String)mydr["POR_13"]);
            por_golden_data.Add((String)mydr["POR_29"]);
            por_golden_data.Add((String)mydr["POR_27"]);
            por_golden_data.Add((String)mydr["POR_28"]);
            por_golden_data.Add((String)mydr["POR_34"]);
            por_golden_data.Add((String)mydr["POR_50"]); 
            por_golden_data.Add((String)mydr["POR_44"]);
            por_golden_data.Add((String)mydr["POR_43"]);
            por_golden_data.Add((String)mydr["POR_19"]);
            por_golden_data.Add((String)mydr["POR_35"]);
            por_golden_data.Add((String)mydr["POR_45"]);
            por_golden_data.Add((String)mydr["POR_22"]);
            por_golden_data.Add((String)mydr["POR_10"]);
            por_golden_data.Add((String)mydr["POR_48"]);
            por_golden_data.Add((String)mydr["POR_49"]);
            por_golden_data.Add((String)mydr["POR_36"]);
            por_golden_data.Add((String)mydr["POR_38"]);
            por_golden_data.Add((String)mydr["POR_39"]);
            por_golden_data.Add((String)mydr["POR_40"]);
            por_golden_data.Add((String)mydr["POR_41"]);
          


        }
         mydr.Close();
         MySqlConn.Close();
         
      
       




    }
    protected void POR_Goledn_putdata()
    {
        lab_POR_15.Text = por_golden_data[0];
        lab_POR_14.Text = por_golden_data[1];
        lab_POR_17.Text = por_golden_data[2];
        lab_POR_18.Text = por_golden_data[3];
        lab_POR_46.Text = por_golden_data[4];
        lab_POR_24.Text = por_golden_data[5];
        lab_POR_04.Text = por_golden_data[6];
        lab_POR_03.Text = por_golden_data[7];
        lab_POR_12.Text = por_golden_data[8];
        lab_POR_20.Text = por_golden_data[9];
        lab_POR_23.Text = por_golden_data[10];
        lab_POR_21.Text = por_golden_data[11];
        lab_POR_25.Text = por_golden_data[12];
        lab_POR_02.Text = por_golden_data[13];
        lab_POR_26.Text = por_golden_data[14];
        lab_POR_55.Text = por_golden_data[15]+"um";
        lab_POR_16.Text = por_golden_data[16];
        lab_POR_33.Text = por_golden_data[17];
        lab_POR_30.Text = por_golden_data[18];
        lab_POR_31.Text = por_golden_data[19];
        lab_POR_32.Text = por_golden_data[20];
        lab_POR_13.Text = por_golden_data[21];
        lab_POR_29.Text = por_golden_data[22];
        lab_POR_27.Text = por_golden_data[23];
        lab_POR_28.Text = por_golden_data[24];
        lab_POR_34.Text = por_golden_data[25];
        lab_POR_50.Text = por_golden_data[26];
        lab_POR_44.Text = por_golden_data[27];
        lab_POR_43.Text = por_golden_data[28];
        lab_POR_19.Text = por_golden_data[29];
        lab_POR_35.Text = por_golden_data[30];
        lab_POR_45.Text = por_golden_data[31];
        lab_POR_22.Text = por_golden_data[32];
        lab_POR_10.Text = por_golden_data[33];
        lab_POR_48.Text = por_golden_data[34];
        lab_POR_49.Text = por_golden_data[35];
        lab_POR_36.Text = por_golden_data[36];
        lab_POR_38.Text = por_golden_data[37];
        lab_POR_39.Text = por_golden_data[38];
        lab_POR_40.Text = por_golden_data[39];
        lab_POR_41.Text = por_golden_data[40];

    }


    protected string receive_npiimportdata(string mySQL, int i)
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
            temp_Data.Add(TableFild);

            }

        }
        mydr.Close();
        MySqlConn.Close();
        return temp_Data[i];
    }

    protected void put_npiimport_data()
    {
        // string strSQL1 = " select * From npiapp where npiapp.New_Customer like '" + customer_sign +'%'+ "' and npiapp.New_Device LIKE '" + New_Device_sign + "%'";
        string sql1 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='D4' and New_Customer like '"+ Customer_TB.Text+'%'+"'and New_Device like '"+ ND_TB.Text +"%'";    
        string sql2 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='D5'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql3 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='D19'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql4 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='G19' and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql5 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D16'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql6 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D13'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql7 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D7'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql8 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D8'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql9 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D9'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql10 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='F11'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql11 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D11'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql12 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D12'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql13 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D26'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql14 = "select Im_Value from npiimportdata where SType='DRC' AND Im_Pos='F38'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql15 = "select Im_Value from npiimportdata where SType='DRC' AND Im_Pos='F39'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql16 = "select Im_Value from npiimportdata where SType='DRC' AND Im_Pos='F35'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql17 = "select Im_Value from npiimportdata where SType='DRC' AND Im_Pos='F34'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql18 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='D29'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql19 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='E26'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql20 = "select Im_Value from npiimportdata where SType='Q_R' AND Im_Pos='D30'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";
        string sql21 = "select Im_Value from npiimportdata where SType='DIF' AND Im_Pos='D30'and New_Customer like '" + Customer_TB.Text + '%' + "'and New_Device like '" + ND_TB.Text + "%'";

        npiimport_data.Add(receive_npiimportdata(sql1, 0));
        npiimport_data.Add(receive_npiimportdata(sql2, 1));
        npiimport_data.Add(receive_npiimportdata(sql3, 2));

        npiimport_data.Add(receive_npiimportdata(sql4, 3));
        npiimport_data.Add(receive_npiimportdata(sql5, 4));
        npiimport_data.Add(receive_npiimportdata(sql6, 5));    
        npiimport_data.Add(receive_npiimportdata(sql7, 6));
        npiimport_data.Add(receive_npiimportdata(sql8, 7));
        npiimport_data.Add(receive_npiimportdata(sql9, 8));
        npiimport_data.Add(receive_npiimportdata(sql10, 9));
        npiimport_data.Add(receive_npiimportdata(sql11, 10));
        npiimport_data.Add(receive_npiimportdata(sql12, 11));
        npiimport_data.Add(receive_npiimportdata(sql13, 12));
        npiimport_data.Add(receive_npiimportdata(sql14, 13));
        npiimport_data.Add(receive_npiimportdata(sql15, 14));
        npiimport_data.Add(receive_npiimportdata(sql16, 15));
        npiimport_data.Add(receive_npiimportdata(sql17, 16));
        npiimport_data.Add(receive_npiimportdata(sql18, 17));
        npiimport_data.Add(receive_npiimportdata(sql19, 18));
        npiimport_data.Add(receive_npiimportdata(sql20, 19));
        npiimport_data.Add(receive_npiimportdata(sql21, 20));

        lab_DIFD4.Text = npiimport_data[0];
        lab_DIFD5.Text = npiimport_data[1];

    
        lab_DIFD19_DIFG19.Text = string.Format("{0:F2}*{1:F2}", (Convert.ToDouble(npiimport_data[2]) / 1000),( Convert.ToDouble(npiimport_data[3]) / 1000));//取到小數第二位
       

        lab_Q_RD16.Text = npiimport_data[4];
        lab_Q_RD13.Text = npiimport_data[5];

        lab_Q_RD7.Text = npiimport_data[6];
        lab_Q_RD8.Text = npiimport_data[7];
        lab_Q_RD9.Text = npiimport_data[8];
        
        lab_DIFF11.Text = npiimport_data[9];
        lab_Q_RD11.Text = npiimport_data[10];

        lab_Q_RD12.Text = npiimport_data[11];
        lab_Q_RD26.Text = npiimport_data[12];
        lab_DRCF38.Text = npiimport_data[13];
        lab_DRCF39.Text = npiimport_data[14];
        lab_DRCF35.Text = npiimport_data[15];
        lab_DRCF34.Text = npiimport_data[16];

        char[] exp = new char[] { 'u', 'm' };
        string[] temp = npiimport_data[17].Split(exp);

        lab_DIFD29.Text = temp[0];
        lab_Q_RE26.Text = npiimport_data[18];
        lab_Q_RD30.Text = npiimport_data[19];
        lab_DIFD30.Text = npiimport_data[20];
    



    }

    protected void receive_npimanual_data()
    {
        clsMySQL db = new clsMySQL();
        string customer_sign = Customer_TB.Text;
        string New_Device_sign = ND_TB.Text;

        //string strSQL1 = " select * From npiapp,npimanual where npiapp.New_Customer = '" + customer_sign + "' and npiapp.New_Device = '" + New_Device_sign + "'and npimanual.New_Customer = '" + customer_sign + "' and npimanual.New_Device = '" + New_Device_sign + "'";
        string strSQL1 = "select * From npimanual where  npimanual.New_Customer like '" + customer_sign + "%' and npimanual.New_Device Like'" + New_Device_sign +"%'";
        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(strSQL1, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();




        while (mydr.Read())
        {
            npimanual_data.Add((String)mydr["Man_01"]);
            npimanual_data.Add((String)mydr["Man_02"]);
            npimanual_data.Add((String)mydr["Man_03"]);
            npimanual_data.Add((String)mydr["Man_04"]);
            npimanual_data.Add((String)mydr["Man_05"]);
            npimanual_data.Add((String)mydr["Man_06"]);
            npimanual_data.Add((String)mydr["Man_07"]);
            npimanual_data.Add((String)mydr["Man_08"]);
            npimanual_data.Add((String)mydr["Man_09"]);
            npimanual_data.Add((String)mydr["Man_10"]);
            npimanual_data.Add((String)mydr["Man_11"]);
            npimanual_data.Add((String)mydr["Man_12"]);
            npimanual_data.Add((String)mydr["Man_13"]);
            npimanual_data.Add((String)mydr["Man_14"]);
            npimanual_data.Add((String)mydr["Man_15"]);
            npimanual_data.Add((String)mydr["Man_16"]);
            npimanual_data.Add((String)mydr["Man_17"]);
            npimanual_data.Add((String)mydr["Man_18"]);
            npimanual_data.Add((String)mydr["Man_19"]);
            npimanual_data.Add((String)mydr["Man_20"]);
            npimanual_data.Add((String)mydr["Man_21"]);
            npimanual_data.Add((String)mydr["Man_22"]);
        }
        mydr.Close();
        MySqlConn.Close();
        lab_Man_01.Text = npimanual_data[0];
        lab_Man_02.Text = npimanual_data[1];
        lab_Man_03.Text = npimanual_data[2];
        lab_Man_04.Text = npimanual_data[3];
        lab_Man_05.Text = npimanual_data[4];
        lab_Man_06.Text = npimanual_data[5];
        lab_Man_07.Text = npimanual_data[6];

        char[] exp = new char[] { '%' };
        string[] temp = npimanual_data[7].Split(exp);

        lab_Man_08.Text = temp[0];
        lab_Man_09.Text = npimanual_data[8];
        lab_Man_10.Text = npimanual_data[9];
        lab_Man_11.Text = npimanual_data[10];
        lab_Man_12.Text = npimanual_data[11];
        lab_Man_13.Text = npimanual_data[12];
        lab_Man_14.Text = npimanual_data[13];
        lab_Man_15.Text = npimanual_data[14];
        lab_Man_16.Text = npimanual_data[15];
        lab_Man_17.Text = npimanual_data[16];
        lab_Man_18.Text = npimanual_data[17];
        lab_Man_19.Text = npimanual_data[18];
        lab_Man_20.Text = npimanual_data[19];
        lab_Man_21.Text = npimanual_data[20];
        lab_Man_22.Text = npimanual_data[21];       

    }

    protected void receive_npiapp_data()
    {
        clsMySQL db = new clsMySQL();
        string customer_sign = Customer_TB.Text;
        string New_Device_sign = ND_TB.Text;

        string strSQL1 = " select * From npiapp where npiapp.New_Customer like '" + customer_sign  + "%' and npiapp.New_Device like'" + New_Device_sign + "%'";
        //strQuerySQL = strQuerySQL + "and POR_01 like'" + ProductionSite_TextBox.Text + "%' ";

        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(strSQL1, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();




        while (mydr.Read())
        {
            npiapp_data.Add((String)mydr["APP_08"]);
            npiapp_data.Add((String)mydr["APP_21"]);
            npiapp_data.Add((String)mydr["APP_33"]);
            npiapp_data.Add((String)mydr["APP_09"]);
            npiapp_data.Add((String)mydr["APP_11"]);
            npiapp_data.Add((String)mydr["APP_10"]);
            npiapp_data.Add((String)mydr["APP_12"]);
        }
        mydr.Close();
        MySqlConn.Close();

        lab_APP_08.Text = npiapp_data[0];
        lab_APP_21.Text = npiapp_data[1];
        lab_APP_33.Text = npiapp_data[2];
        lab_APP_09.Text = npiapp_data[3];
        lab_APP_11.Text = npiapp_data[4];
        lab_APP_10.Text = npiapp_data[5];
        lab_APP_12.Text = npiapp_data[6];
        lab_APP_21_2.Text = npiapp_data[1];
        lab_APP_11_2.Text = npiapp_data[4];
        lab_APP_09_2.Text = npiapp_data[3];
        lab_APP_11_3.Text = npiapp_data[4];
        lab_APP_11_4.Text = npiapp_data[4];

        lab_APP_11_5.Text = npiapp_data[4];
        lab_APP_11_6.Text = npiapp_data[4];
        lab_APP_11_7.Text = npiapp_data[4];

        //public string tt = "oplove";


    }


    protected void receive_npicapep_data(string capsql)
    {
        int i = 4;
        List<string> data_cap_por = new List<string>(); 

        clsMySQL db = new clsMySQL();
       
        
       

          MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
          MySqlConn.Open();

          MySqlCommand MySqlCmd = new MySqlCommand(capsql, MySqlConn);
          MySqlDataReader mydr = MySqlCmd.ExecuteReader();


        while (mydr.Read())
        {
            for (i = 4; i < mydr.FieldCount; i++)
            {
                data_cap.Add(mydr.GetString(i));
              
            }
        }

       


        for (i=0;i<64;i++)
        {
            if(i==6|| i==9 || i==13 || i==16 || i==17 || i==22 || i==47 || i==49)
            {
                char[] exp = new char[] { '|' };
                string[] temp_exp = data_cap[i].Split(exp);
                data_cap[i] = "";
                for(int j=0;j<temp_exp.Length;j++)
                {
                    data_cap[i] += temp_exp[j] + "<br />";
                }

            }

              string temp = "Lab_CAP_" + Convert.ToString(i + 1);
              Label mylabel = (Label)FindControl(temp);
              mylabel.Text = data_cap[i];
        }
        int count=1;
        for(i=65;i<80;i++)
        {

            if (i == 77)
            {
                char[] exp = new char[] { '|' };
                string[] temp_exp = data_cap[i].Split(exp);
                data_cap[i] = "";
                for (int j = 0; j < temp_exp.Length; j++)
                {
                    data_cap[i] += temp_exp[j] + "<br />";
                }

            }

            

            string temp = "Lab_CAP_POR_" + Convert.ToString(count);
            Label mylabel = (Label)FindControl(temp);
            mylabel.Text = data_cap[i];
            count++;
        }




      
    }




   




    protected void POR_Butt_Click1(object sender, EventArgs e)
    {
        

      
    

    }

  
  
    protected void cmdFilter_Click(object sender, EventArgs e)
    {
      
        Lab_Device.Text = Session["value1"].ToString();
        Lab_ProSite.Text = Session["value2"].ToString();
        Lab_PKG.Text = Session["value3"].ToString();
        Lab_WaferTech.Text = Session["value4"].ToString();
        Lab_FAB.Text = Session["value5"].ToString();
        Lab_WaferThick.Text = Session["value6"].ToString();
        Lab_RVSI.Text = Session["value7"].ToString();
        Lab_Customer.Text = Session["value8"].ToString();

        

        Panel1.Visible = true;
        Panel2.Visible = false;
        //string porsql = " select '"+temp_por + "' From npipor where POR_17 = '" + Label1.Text + "' and POR_01 = '" + Label2.Text + "'and POR_02 = '" + Label3.Text + "' and POR_03 = '" + Label4.Text + "'and POR_04 = '" + Label5.Text + "'and POR_05 = '" + Label6.Text + "'and POR_11 ='" + Label7.Text + "'and POR_14 ='" + Label8.Text + "'";
        string capsql = "select * From npi_cap_ep where CAP_EP_Name='Package_All'";
        string porsql = " select * From npipor where POR_17 = '" + Lab_Device.Text + "' and POR_01 = '" + Lab_ProSite.Text + "'and POR_02 = '" + Lab_PKG.Text + "' and POR_03 = '" + Lab_WaferTech.Text + "'and POR_04 = '" + Lab_FAB.Text + "'and POR_05 = '" + Lab_WaferThick.Text + "'and POR_11 ='" + Lab_RVSI.Text + "'and POR_14 ='" + Lab_Customer.Text + "'";
        POR_Goledn_pickup_data(porsql);
        POR_Goledn_putdata();
        receive_npicapep_data(capsql);
        Session.Clear();

        for (int i = 35; i < 42; i++)
        {
                     
                HttpContext.Current.Session["value_cap_list"] += data_cap[i] + "$";                                 
        }



        for (int i = 65; i < 80; i++) { 
            if(i!=79)
            {
                HttpContext.Current.Session["value_cap_list"] += data_cap[i] + "$";
            }
            else
                HttpContext.Current.Session["value_cap_list"] += data_cap[i];          
        }


        put_effect_data();
        put_potential_data();
      
      

            lab_Eff_02.Text = "--";
            lab_Eff_03.Text = "--";

            Panel2.Visible = true;

            if (lab_POR_18.Text != lab_DIFD19_DIFG19.Text)
            {
                lab_gap4.Text = "Y";
                lab_gap4.ForeColor = System.Drawing.Color.Red;
                lab_Eff_04.Text = "<br />" + Str_Effect[0] + "<br />" + Str_Effect[1] + "<br />" + "<br />";
                lab_Pot_04.Text = "<br />" + Str_Poten[0] + "<br />" + Str_Poten[1] + "<br />" + "<br />";
            }
            else
            {
                lab_gap4.Text = "N";
                lab_gap4.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_04.Text = "--";
                lab_Pot_04.Text = "--";
            }


            if (lab_POR_46.Text != lab_Q_RD16.Text)
            {
                lab_gap5.Text = "Y";
                lab_gap5.ForeColor = System.Drawing.Color.Red;
                lab_Eff_05.Text = Str_Effect[2];
                lab_Pot_05.Text = Str_Poten[2];
            }
            else
            {
                lab_gap5.Text = "N";
                lab_gap5.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_05.Text = "--";
                lab_Pot_05.Text = "--";
            }

            if (lab_POR_24.Text != lab_Q_RD13.Text)
            {
                lab_gap6.Text = "Y";
                lab_gap6.ForeColor = System.Drawing.Color.Red;
                lab_Eff_06.Text = Str_Effect[3] + "<br />" + Str_Effect[4] + "<br />" + Str_Effect[5] + "<br />";
                lab_Pot_06.Text = Str_Poten[3] + "<br />" + Str_Poten[4] + "<br />" + Str_Poten[4] + "<br />";
            }
            else
            {

                lab_gap6.Text = "N";
                lab_gap6.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_06.Text = "--";
                lab_Pot_06.Text = "--";
            }

            if (lab_POR_04.Text != lab_Q_RD7.Text)
            {
                lab_gap7.Text = "Y";
                lab_gap7.ForeColor = System.Drawing.Color.Red;
                lab_Eff_07.Text = Str_Effect[0] + "<br />" + Str_Effect[1] + "<br />";
                lab_Pot_07.Text = Str_Poten[5] + "<br />" + Str_Poten[1] + "<br />";
            }
            else
            {
                lab_gap7.Text = "N";
                lab_gap7.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_07.Text = "--";
                lab_Pot_07.Text = "--";
            }

            if (lab_POR_03.Text != lab_Q_RD8.Text)
            {
                lab_gap8.Text = "Y";
                lab_gap8.ForeColor = System.Drawing.Color.Red;
                lab_Eff_08.Text = Str_Effect[0] + "<br />" + Str_Effect[1] + "<br />";
                lab_Pot_08.Text = Str_Poten[5] + "<br />" + Str_Poten[1] + "<br />";
            }
            else
            {
                lab_gap8.Text = "N";
                lab_gap8.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_08.Text = "--";
                lab_Pot_08.Text = "--";
            }

            if (lab_POR_12.Text != lab_Q_RD9.Text)
            {
                lab_gap9.Text = "Y";
                lab_gap9.ForeColor = System.Drawing.Color.Red;
                lab_Eff_09.Text = Str_Effect[0] + "<br />" + Str_Effect[1] + "<br />";
                lab_Pot_09.Text = Str_Poten[5] + "<br />" + Str_Poten[1] + "<br />";
            }
            else
            {
                lab_gap9.Text = "N";
                lab_gap9.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_09.Text = "--";
                lab_Pot_09.Text = "--";
            }

            if (lab_POR_20.Text != lab_DIFF11.Text)
            {
                lab_gap10.Text = "Y";
                lab_gap10.ForeColor = System.Drawing.Color.Red;
                lab_Eff_10.Text = Str_Effect[3] + Str_Effect[0] + "<br />";
                lab_Pot_10.Text = Str_Poten[6] + "<br />" + Str_Poten[1] + "<br />";
            }
            else
            {
                lab_gap10.Text = "N";
                lab_gap10.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_10.Text = "--";
                lab_Pot_10.Text = "--";
            }

            if (lab_POR_23.Text != lab_Q_RD11.Text)
            {
                lab_gap11.Text = "Y";
                lab_gap11.ForeColor = System.Drawing.Color.Red;
                lab_Eff_11.Text = Str_Effect[4] + "<br />" + Str_Effect[5] + "<br />";
                lab_Pot_11.Text = Str_Poten[4] + "<br />";
            }
            else
            {
                lab_gap11.Text = "N";
                lab_gap11.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_11.Text = "--";
                lab_Pot_11.Text = "--";

            }

            if (lab_POR_21.Text != lab_Man_01.Text)
            {
                lab_gap12.Text = "Y";
                lab_gap12.ForeColor = System.Drawing.Color.Red;
                lab_Eff_12.Text = Str_Effect[6] + "<br /><br /><br /><br />" + Str_Effect[3] + Str_Effect[24] + Str_Effect[4];
                lab_Pot_12.Text = Str_Poten[9] + "<br />" + Str_Poten[10] + "<br />" + Str_Poten[3] + "<br />" + Str_Poten[4];
            }
            else
            {
                lab_gap12.Text = "N";
                lab_gap12.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_12.Text = "--";
                lab_Pot_12.Text = "--";
            }


            if (lab_POR_25.Text != lab_Q_RD12.Text)
            {
                lab_gap13.Text = "Y";
                lab_gap13.ForeColor = System.Drawing.Color.Red;
                lab_Eff_13.Text = Str_Effect[6] + "<br />" + Str_Effect[3] + "<br />" + Str_Effect[5] + "<br />" + Str_Effect[7];
                lab_Pot_13.Text = Str_Poten[12] + "<br />" + Str_Poten[13] + "<br />" + Str_Poten[14] + Str_Poten[15] + "<br />" + Str_Poten[16];
            }
            else
            {
                lab_gap13.Text = "N";
                lab_gap13.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_13.Text = "--";
                lab_Pot_13.Text = "--";
            }


            if (lab_POR_02.Text != lab_APP_08.Text)
            {
                lab_gap14.Text = "Y";
                lab_gap14.ForeColor = System.Drawing.Color.Red;
                lab_Eff_14.Text = "--";
                lab_Pot_14.Text = "--";
            }
            else
            {
                lab_gap14.Text = "N";
                lab_gap14.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_14.Text = "--";
                lab_Pot_14.Text = "--";
            }



            if (lab_POR_26.Text != lab_Man_02.Text)
            {
                lab_gap15.Text = "Y";
                lab_gap15.ForeColor = System.Drawing.Color.Red;
                lab_Eff_15.Text = Str_Effect[6] + "<br /><br /><br /><br /><br /><br />"
                      + Str_Effect[3] + "<br /><br /><br /><br />"
                      + Str_Effect[7] + "<br /><br /><br /><br /><br /><br /><br />"
                      + Str_Effect[8] + "<br /><br /><br /><br /><br /><br /><br />"
                      + Str_Effect[0];
                lab_Pot_15.Text = Str_Poten[17] + "<br />" + Str_Poten[18] + "<br />" + Str_Poten[19] + "<br />" + Str_Poten[20] + "<br />" + Str_Poten[21] + "<br />" + Str_Poten[22] + "<br /><br />"
                            + Str_Poten[23] + "<br />" + Str_Poten[24] + "<br />" + Str_Poten[25] + "<br /><br />"
                            + Str_Poten[26] + "<br />" + Str_Poten[27] + "<br />" + Str_Poten[28] + "<br />" + Str_Poten[29] + "<br />" + Str_Poten[30] + "<br />" + Str_Poten[31] + "<br /><br />"
                            + Str_Poten[32] + "<br />" + Str_Poten[27] + "<br />" + Str_Poten[33] + "<br />" + Str_Poten[34] + "<br />" + Str_Poten[35] + "<br />" + Str_Poten[36] + "<br /><br />"
                            + Str_Poten[37] + "<br />" + Str_Poten[38] + "<br />" + Str_Poten[39] + "<br />" + Str_Poten[40];



            }
            else
            {
                lab_gap15.Text = "N";
                lab_gap15.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_15.Text = "--";
                lab_Pot_15.Text = "--";
            }

            if (lab_POR_55.Text != lab_Man_03.Text)
            {
                lab_gap16.Text = "Y";
                lab_gap16.ForeColor = System.Drawing.Color.Red;

                lab_Eff_16.Text = Str_Effect[6] + "<br /><br /><br /><br /><br /><br />"
                      + Str_Effect[3] + "<br /><br /><br /><br />"
                      + Str_Effect[10] + "<br /><br /><br /><br /><br /><br /><br />"
                      + Str_Effect[5] + "<br /><br /><br /><br /><br /><br /><br />"
                      + Str_Effect[8] + "<br /><br /><br /><br /><br /><br /><br />"
                      + Str_Effect[0];
                lab_Pot_16.Text = Str_Poten[17] + "<br />" + Str_Poten[18] + "<br />" + Str_Poten[19] + "<br />" + Str_Poten[20] + "<br />" + Str_Poten[21] + "<br />" + Str_Poten[22] + "<br /><br />"
                            + Str_Poten[23] + "<br />" + Str_Poten[24] + "<br />" + Str_Poten[25] + "<br /><br />"
                            + Str_Poten[26] + "<br />" + Str_Poten[27] + "<br />" + Str_Poten[28] + "<br />" + Str_Poten[29] + "<br />" + Str_Poten[30] + "<br />" + Str_Poten[31] + "<br /><br />"
                            + Str_Poten[32] + "<br />" + Str_Poten[27] + "<br />" + Str_Poten[33] + "<br />" + Str_Poten[34] + "<br />" + Str_Poten[35] + "<br />" + Str_Poten[36] + "<br /><br />"
                            + Str_Poten[37] + "<br />" + Str_Poten[38] + "<br />" + Str_Poten[39] + "<br />" + Str_Poten[40];


            }
            else
            {
                lab_gap16.Text = "N";
                lab_gap16.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_16.Text = "--";
                lab_Pot_16.Text = "--";
            }


            if (lab_POR_16.Text != lab_Man_04.Text)
            {
                lab_gap17.Text = "Y";
                lab_gap17.ForeColor = System.Drawing.Color.Red;
                lab_Eff_17.Text = Str_Effect[7] + "<br /><br />" + Str_Effect[0];
                lab_Pot_17.Text = Str_Poten[41] + "<br />" + Str_Poten[42] + "<br /><br />" + Str_Poten[0];
            }
            else
            {
                lab_gap17.Text = "N";
                lab_gap17.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_17.Text = "--";
                lab_Pot_17.Text = "--";
            }


            if (lab_POR_33.Text != lab_Q_RD26.Text)
            {
                lab_gap18.Text = "Y";
                lab_gap18.ForeColor = System.Drawing.Color.Red;
                lab_Eff_18.Text = Str_Effect[9] + "<br /><br />" + Str_Effect[0] + "<br /><br /><br />" + Str_Effect[1];
                lab_Pot_18.Text = Str_Poten[43] + "<br /><br />" + Str_Poten[44] + "<br />" + Str_Poten[45] + "<br /><br />" + Str_Poten[1];
            }
            else
            {
                lab_gap18.Text = "N";
                lab_gap18.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_18.Text = "--";
                lab_Pot_18.Text = "--";
            }

            if (lab_POR_30.Text != lab_APP_21.Text)
            {
                lab_gap19.Text = "Y";
                lab_gap19.ForeColor = System.Drawing.Color.Red;
                lab_Eff_19.Text = Str_Effect[6] + "<br />";
                lab_Pot_19.Text = "1.PI CD OOS<br />" + "2.RS OOS";
            }
            else
            {
                lab_gap19.Text = "N";
                lab_gap19.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_19.Text = "--";
                lab_Pot_19.Text = "--";
            }

            if ("NA" != lab_DRCF38.Text)
            {
                lab_gap20.Text = "Y";
                lab_gap20.ForeColor = System.Drawing.Color.Red;
                lab_Eff_20.Text = Str_Effect[10] + "<br />";
                lab_Pot_20.Text = "PR bubble" + "<br />";
            }
            else
            {
                lab_gap20.Text = "N";
                lab_gap20.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_20.Text = "--";
                lab_Pot_20.Text = "--";
            }

            if (lab_POR_31.Text != lab_DRCF39.Text)
            {
                lab_gap21.Text = "Y";
                lab_gap21.ForeColor = System.Drawing.Color.Red;
                lab_Eff_21.Text = Str_Effect[6] + "<br />";
                lab_Pot_21.Text = "PI delamination" + "<br />";
            }
            else
            {
                lab_gap21.Text = "N";
                lab_gap21.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_21.Text = "--";
                lab_Pot_21.Text = "--";
            }

            if (lab_POR_32.Text != lab_DRCF35.Text)
            {
                lab_gap22.Text = "Y";
                lab_gap22.ForeColor = System.Drawing.Color.Red;
                lab_Eff_22.Text = Str_Effect[6] + "<br /><br />" + Str_Effect[10] + "<br /><br />" + Str_Effect[0];
                lab_Pot_22.Text = "PI delamination<br />" + "2.PI Bubble<br /><br />" + "PR bubble<br /><br />" + "PI crack (pull out)";
            }
            else
            {
                lab_gap22.Text = "N";
                lab_gap22.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_22.Text = "--";
                lab_Pot_22.Text = "--";
            }

            if (lab_POR_13.Text != lab_Man_05.Text)
            {
                lab_gap23.Text = "Y";
                lab_gap23.ForeColor = System.Drawing.Color.Red;
                lab_Eff_23.Text = Str_Effect[10] + "<br />" + Str_Effect[11] + "<br /><br />";
                lab_Pot_23.Text = "1.PR Under develop<br />&nbsp&nbsp&nbsp&nbsp/PR Over develop<br />" + "2.PR CD out of spec<br />" +
                    "3.PR thickness OOS<br />" + "4.Poor coating<br /><br />"
                    + "1.PR residue on bump<br />" + "2.PR residue around bump<br />" + "3.Metal residue";
            }
            else
            {
                lab_gap23.Text = "N";
                lab_gap23.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_23.Text = "--";
                lab_Pot_23.Text = "--";
            }

            if (lab_POR_29.Text != lab_APP_33.Text)
            {
                lab_gap24.Text = "Y";
                lab_gap24.ForeColor = System.Drawing.Color.Red;
                lab_Eff_24.Text = Str_Effect[10] + "<br /><br />" + Str_Effect[5] + "<br /><br /><br />" + Str_Effect[0] + "<br />";
                lab_Pot_24.Text = "Under develop<br /><br />" + "1.Ni bubble(small UBM size)<br />" +
                   "2.BC / BH OOS<br />" + "3.Bump Void OOS(larger UBM size)<br /><br />"
                   + "1.Bump bridge<br />" + "2.Non - wetting<br />" + "3.Bump crack";
            }
            else
            {
                lab_gap24.Text = "N";
                lab_gap24.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_24.Text = "--";
                lab_Pot_24.Text = "--";
            }

            if (lab_POR_27.Text != lab_DRCF34.Text)
            {
                lab_gap25.Text = "Y";
                lab_gap25.ForeColor = System.Drawing.Color.Red;
                lab_Eff_25.Text = Str_Effect[7] + "<br />" + Str_Effect[0];
                lab_Pot_25.Text = "Pad damage<br /><br />" + "Bump Crack";
            }
            else
            {
                lab_gap25.Text = "N";
                lab_gap25.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_25.Text = "--";
                lab_Pot_25.Text = "--";
            }

            if (lab_POR_28.Text != lab_Man_06.Text)
            {
                lab_gap26.Text = "Y";
                lab_gap26.ForeColor = System.Drawing.Color.Red;
                lab_Eff_26.Text = Str_Effect[0];
                lab_Pot_26.Text = "Bump Crack";


            }
            else
            {
                lab_gap26.Text = "N";
                lab_gap26.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_26.Text = "--";
                lab_Pot_26.Text = "--";
            }

            if (lab_POR_34.Text != lab_Man_07.Text)
            {
                lab_gap27.Text = "Y";
                lab_gap27.ForeColor = System.Drawing.Color.Red;
                lab_Eff_27.Text = Str_Effect[10] + "<br /><br />" + Str_Effect[5] + "<br /><br /><br />" + Str_Effect[7] + "<br />" + Str_Effect[8] + "<br />";
                lab_Pot_27.Text = " Under develop<br /><br />" + "1.BC / BH OOS<br />" + "2.Composition<br />" + "3.Bump Void OOS<br /><br />"
                    + "Metal residue<br /><br />" + "1.Metal residue<br />" + "2.BL OOS<br />";


            }
            else
            {
                lab_gap27.Text = "N";
                lab_gap27.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_27.Text = "--";
                lab_Pot_27.Text = "--";
            }

            if (lab_POR_50.Text != lab_Man_08.Text)
            {


                lab_gap28.Text = "Y";
                lab_gap28.ForeColor = System.Drawing.Color.Red;
                lab_Eff_28.Text = "PPHO<br /><br />" + "PLAT<br /><br /><br /><br />" + "ETCH<br /><br />" + "DESCUM<br />";
                lab_Pot_28.Text = " Under develop<br /><br />" + "1.BC / BH OOS<br />" + "2.Composition<br />" + "3.Bump Void OOS<br /><br />"
                    + "Metal residue<br /><br />" + "1.Metal residue<br />" + "2.BL OOS<br />";

            }
            else
            {
                lab_gap28.Text = "N";
                lab_gap28.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_28.Text = "--";
                lab_Pot_28.Text = "--";
            }


            if (lab_POR_44.Text != lab_Man_09.Text)
            {
                lab_gap29.Text = "Y";
                lab_gap29.ForeColor = System.Drawing.Color.Red;
                lab_Eff_29.Text = "--";
                lab_Pot_29.Text = "--";
            }
            else
            {
                lab_gap29.Text = "N";
                lab_gap29.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_29.Text = "--";
                lab_Pot_29.Text = "--";
            }


            if (lab_POR_43.Text != lab_Man_10.Text)
            {
                lab_gap30.Text = "Y";
                lab_gap30.ForeColor = System.Drawing.Color.Red;
                lab_Eff_30.Text = Str_Effect[7] + "<br />" + Str_Effect[8];
                lab_Pot_30.Text = "Metal residue<br /><br />" + "1.Metal residue<br />2.BL OOS";
            }
            else
            {
                lab_gap30.Text = "N";
                lab_gap30.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_30.Text = "--";
                lab_Pot_30.Text = "--";
            }


            if (lab_POR_19.Text != lab_DIFD29.Text)
            {
                lab_gap31.Text = "Y";
                lab_gap31.ForeColor = System.Drawing.Color.Red;
                lab_Eff_31.Text = Str_Effect[7] + " <br />" + Str_Effect[8] + " <br /><br /><br />" + Str_Effect[0] + " <br /><br /><br />" + Str_Effect[1];
                lab_Pot_31.Text = "Metal residue<br /><br />" + "1.Metal residue<br />2.BL OOS<br /><br />" + "1.Bump bridge<br />" + "2.MD / UF Void<br /><br />" + "RT fail";
            }
            else
            {
                lab_gap31.Text = "N";
                lab_gap31.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_31.Text = "--";
                lab_Pot_31.Text = "--";
            }


            if (lab_POR_35.Text != lab_APP_09.Text)
            {
                lab_gap32.Text = "Y";
                lab_gap32.ForeColor = System.Drawing.Color.Red;
                lab_Eff_32.Text = Str_Effect[7] + " <br />" + Str_Effect[2] + "<br /><br />" + Str_Effect[0];
                lab_Pot_32.Text = "Metal residue<br /><br />" + "OS fail<br /><br />"
                    + "1.Glue residual<br />" + "2.Bump bridge<br />3.Non-wetting<br />4.MD/UF delam<br />5.MD/UF Void";
            }
            else
            {
                lab_gap32.Text = "N";
                lab_gap32.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_32.Text = "--";
                lab_Pot_32.Text = "--";
            }


            if (lab_POR_45.Text != lab_APP_11.Text)
            {
                lab_gap33.Text = "Y";
                lab_gap33.ForeColor = System.Drawing.Color.Red;
                lab_Eff_33.Text = "--";
                lab_Pot_33.Text = "--";
            }
            else
            {
                lab_gap33.Text = "N";
                lab_gap33.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_33.Text = "--";
                lab_Pot_33.Text = "--";
            }

            if (lab_POR_22.Text != lab_Man_11.Text)
            {
                lab_gap34.Text = "Y";
                lab_gap34.ForeColor = System.Drawing.Color.Red;
                lab_Eff_34.Text = Str_Effect[10] + "<br /><br />" + Str_Effect[5] + "<br /><br /><br />" + Str_Effect[11] + "<br /><br /><br /><br />" + Str_Effect[8] + "<br /><br /><br />" + Str_Effect[0] + "<br /><br /><br /><br /><br />" + Str_Effect[1] + "<br />";
                lab_Pot_34.Text = "Under develop<br /><br />" + "1.BC/BH OOS<br />2.Composition<br />3.Bump Void OOS<br /><br />"
                    + "1. PR residue on bump<br />2. PR residue around bump<br />3. Metal residue<br /><br />"
                    + "1. Metal residue<br />2. BL OOS<br /><br />"
                    + "1.Coating /Debris  residual<br />2.Flux residue<br />3.Bump crack<br />4. MD/UF void<br /><br />"
                    + "RT fail";

            }
            else
            {
                lab_gap34.Text = "N";
                lab_gap34.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_34.Text = "--";
                lab_Pot_34.Text = "--";
            }


            if ("1" != lab_Man_12.Text)
            {
                lab_gap35.Text = "Y";
                lab_gap35.ForeColor = System.Drawing.Color.Red;
                lab_Eff_35.Text = Str_Effect[5] + "<br />";
                lab_Pot_35.Text = "BC / BH OOS<br />";
            }
            else
            {
                lab_gap35.Text = "N";
                lab_gap35.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_35.Text = "--";
                lab_Pot_35.Text = "--";
            }

            if ("0.5" != lab_Q_RE26.Text)
            {
                lab_gap36.Text = "Y";
                lab_gap36.ForeColor = System.Drawing.Color.Red;
                lab_Eff_36.Text = Str_Effect[0] + "<br /><br />" + Str_Effect[12] + "<br /><br /><br />" + Str_Effect[1];
                lab_Pot_36.Text = "1.Non-wetting<br /> 2.Bump bridge<br /><br />OS fail<br /><br />RT fail";
            }
            else
            {
                lab_gap36.Text = "N";
                lab_gap36.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_36.Text = "--";
                lab_Pot_36.Text = "--";
            }

            if ("10" != lab_APP_10.Text)
            {
                lab_gap37.Text = "Y";
                lab_gap37.ForeColor = System.Drawing.Color.Red;
                lab_Eff_37.Text = Str_Effect[2] + "<br />" + Str_Effect[0] + "<br />" + Str_Effect[12] + "<br />";
                lab_Pot_37.Text = "OS fail<br /> Non Wetting<br />OS fail<br />";
            }
            else
            {
                lab_gap37.Text = "N";
                lab_gap37.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_37.Text = "--";
                lab_Pot_37.Text = "--";
            }



            if ("10" != lab_APP_12.Text)
            {
                lab_gap38.Text = "Y";
                lab_gap38.ForeColor = System.Drawing.Color.Red;
                lab_Eff_38.Text = Str_Effect[0] + "<br />" + Str_Effect[12] + "<br />";
                lab_Pot_38.Text = "Bridge<br />OS fail<br />";

            }
            else
            {
                lab_gap38.Text = "N";
                lab_gap38.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_38.Text = "--";
                lab_Pot_38.Text = "--";
            }

            if ("<20 um" != lab_Man_13.Text)
            {
                lab_gap39.Text = "Y";
                lab_gap39.ForeColor = System.Drawing.Color.Red;
                lab_Eff_39.Text = Str_Effect[2] + "<br />" + Str_Effect[0] + "<br />" + Str_Effect[12] + "<br />";
                lab_Pot_39.Text = "OS fail<br />Non Wetting<br />OS fail<br />";
            }
            else
            {
                lab_gap39.Text = "N";
                lab_gap39.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_39.Text = "--";
                lab_Pot_39.Text = "--";
            }

            if ("LF: >2.5 g/mil^2" != lab_Man_14.Text)
            {
                lab_gap40.Text = "Y";
                lab_gap40.ForeColor = System.Drawing.Color.Red;
                lab_Eff_40.Text = Str_Effect[0] + "<br />" + Str_Effect[1] + "<br />";
                lab_Pot_40.Text = "Bump crack< br />RT fail< br />";
            }
            else
            {
                lab_gap40.Text = "N";
                lab_gap40.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_40.Text = "--";
                lab_Pot_40.Text = "--";
            }

            if ("<10 %" != lab_Man_15.Text)
            {
                lab_gap41.Text = "Y";
                lab_gap41.ForeColor = System.Drawing.Color.Red;
                lab_Eff_41.Text = Str_Effect[0] + "<br />" + Str_Effect[12] + "<br />";
                lab_Pot_41.Text = "Bump crack< br />OS fail< br />";
            }
            else
            {
                lab_gap41.Text = "N";
                lab_gap41.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_41.Text = "--";
                lab_Pot_41.Text = "--";
            }

            if ("BGM3A:15~30nm" != lab_Man_16.Text)
            {
                lab_gap42.Text = "Y";
                lab_gap42.ForeColor = System.Drawing.Color.Red;
                lab_Eff_42.Text = Str_Effect[0] + "<br />" + Str_Effect[12] + "<br />";
                lab_Pot_42.Text = "1.Glue residual<br /> 2.MD / UF Delam<br />OS fail<br />";
            }
            else
            {
                lab_gap42.Text = "N";
                lab_gap42.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_42.Text = "--";
                lab_Pot_42.Text = "--";
            }


            if (lab_POR_10.Text != lab_Q_RD30.Text)
            {
                lab_gap43.Text = "Y";
                lab_gap43.ForeColor = System.Drawing.Color.Red;
                lab_Eff_43.Text = Str_Effect[2] + "<br />" + Str_Effect[12] + "<br />";
                lab_Pot_43.Text = "1.Function fail<br />2.Function fail<br />";
            }
            else
            {
                lab_gap43.Text = "N";
                lab_gap43.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_43.Text = "--";
                lab_Pot_43.Text = "--";
            }

            if (lab_Man_17.Text == "")
            {
                lab_gap44.Text = "Y";
                lab_gap44.ForeColor = System.Drawing.Color.Red;
                lab_Eff_44.Text = Str_Effect[13] + "<br />";
                lab_Pot_44.Text = "1.Alignment shift<br />";
            }
            else
            {
                if (58 <= Convert.ToDouble(lab_Man_17.Text) && Convert.ToDouble(lab_Man_17.Text) <= 25747)
                {
                    lab_gap44.Text = "N";
                    lab_gap44.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_44.Text = "--";
                    lab_Pot_44.Text = "--";
                }
                else
                {
                    lab_gap44.Text = "Y";
                    lab_gap44.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_44.Text = Str_Effect[13] + "<br />";
                    lab_Pot_44.Text = "1.Alignment shift<br />";
                }
            }

            if ("No" != lab_Man_18.Text)
            {
                lab_gap45.Text = "Y";
                lab_gap45.ForeColor = System.Drawing.Color.Red;
                lab_Eff_45.Text = Str_Effect[13] + "<br />";
                lab_Pot_45.Text = "1st reject rate too high <br />&nbsp&nbsp&nbsp&nbsp& defect escaped";
            }
            else
            {
                lab_gap45.Text = "N";
                lab_gap45.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_45.Text = "--";
                lab_Pot_45.Text = "--";
            }


            if (lab_APP_21.Text == "")
            {
                lab_gap46.Text = "Y";
                lab_gap46.ForeColor = System.Drawing.Color.Red;
                lab_Eff_46.Text = Str_Effect[14] + "<br />";
                lab_Pot_46.Text = "Defect escaped<br />";
            }
            else
            {
                if (22 <= Convert.ToDouble(lab_APP_21.Text) && Convert.ToDouble(lab_APP_21.Text) <= 240)
                {
                    lab_gap46.Text = "N";
                    lab_gap46.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_46.Text = "--";
                    lab_Pot_46.Text = "--";
                }
                else {
                    lab_gap46.Text = "Y";
                    lab_gap46.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_46.Text = Str_Effect[14] + "<br />";
                    lab_Pot_46.Text = "Defect escaped<br />";

                }
            }




            if (lab_APP_11.Text == "")
            {
                lab_gap47.Text = "Y";
                lab_gap47.ForeColor = System.Drawing.Color.Red;
                lab_Eff_47.Text = Str_Effect[15] + "<br />";
                lab_Pot_47.Text = "Defect escaped<br />";
            }
            else
            {
                if ((84 <= Convert.ToDouble(lab_APP_11.Text)) && (Convert.ToDouble(lab_APP_11.Text) <= 127))
                {
                    lab_gap47.Text = "N";
                    lab_gap47.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_47.Text = "--";
                    lab_Pot_47.Text = "--";

                }
                else {
                    lab_gap47.Text = "Y";
                    lab_gap47.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_47.Text = Str_Effect[15] + "<br />";
                    lab_Pot_47.Text = "Defect escaped<br />";
                }

            }


            if (true)
            {
                lab_gap48.Text = "Y";
                lab_gap48.ForeColor = System.Drawing.Color.Red;
                lab_Eff_48.Text = "--";
                lab_Pot_48.Text = "--";

            }
            else
            {
                lab_gap48.Text = "N";
                lab_gap48.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_48.Text = "--";
                lab_Pot_48.Text = "--";
            }


            if (true)
            {
                lab_gap49.Text = "Y";
                lab_gap49.ForeColor = System.Drawing.Color.Red;
                lab_Eff_49.Text = "--";
                lab_Pot_49.Text = "--";
            }
            else
            {
                lab_gap49.Text = "N";
                lab_gap49.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_49.Text = "--";
                lab_Pot_49.Text = "--";
            }




            if (lab_Man_19.Text == "")
            {
                lab_gap51.Text = "Y";
                lab_gap51.ForeColor = System.Drawing.Color.Red;
                lab_Eff_51.Text = Str_Effect[16] + "<br />";
                lab_Pot_51.Text = "Alignment shift<br />";
            }
            else {
                if ((58 <= Convert.ToDouble(lab_Man_19.Text)) && (Convert.ToDouble(lab_Man_19.Text) <= 25747))
                {
                    lab_gap51.Text = "N";
                    lab_gap51.ForeColor = System.Drawing.Color.Blue;
                    lab_Pot_51.Text = "--";
                    lab_Eff_51.Text = "--";
                }
                else
                {
                    lab_gap51.Text = "Y";
                    lab_gap51.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_51.Text = Str_Effect[16] + "<br />";
                    lab_Pot_51.Text = "Alignment shift<br />";
                }
            }


            if (lab_DIFD30.Text == "")
            {
                lab_gap52.Text = "Y";
                lab_gap52.ForeColor = System.Drawing.Color.Red;
                lab_Eff_52.Text = Str_Effect[16] + "<br />";
                lab_Pot_52.Text = "M/C limit<br />";
            }
            else
            {
                if ((172 <= Convert.ToDouble(lab_DIFD30.Text)) && (Convert.ToDouble(lab_DIFD30.Text) <= 18510))
                {
                    lab_gap52.Text = "N";
                    lab_gap52.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_52.Text = "--";
                    lab_Pot_52.Text = "--";
                }
                else
                {
                    lab_gap52.Text = "Y";
                    lab_gap52.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_52.Text = Str_Effect[16] + "<br />";
                    lab_Pot_52.Text = "M/C limit<br />";
                }
            }
            if (lab_APP_09.Text == "")
            {
                lab_gap53.Text = "Y";
                lab_gap53.ForeColor = System.Drawing.Color.Red;
                lab_Eff_53.Text = Str_Effect[17] + "/" + Str_Effect[23] + "<br />";
                lab_Pot_53.Text = "M/C limit<br />";
            }
            else {
                if ((65 <= Convert.ToDouble(lab_APP_09.Text)) && (Convert.ToDouble(lab_APP_09.Text) <= 108))
                {
                    lab_gap53.Text = "N";
                    lab_gap53.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_53.Text = "--";
                    lab_Pot_53.Text = "--";
                }
                else
                {
                    lab_gap53.Text = "Y";
                    lab_gap53.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_53.Text = Str_Effect[17] + "/" + Str_Effect[23] + "<br />";
                    lab_Pot_53.Text = "M/C limit<br />";

                }
            }


            if (lab_APP_11_2.Text == "")
            {
                lab_gap54.Text = "Y";
                lab_gap54.ForeColor = System.Drawing.Color.Red;
                lab_Eff_54.Text = Str_Effect[17] + "/" + Str_Effect[22] + "/" + Str_Effect[23] + "<br />";
                lab_Pot_54.Text = "Data incorrect<br />";
            }
            else {
                if ((84 <= Convert.ToDouble(lab_APP_11_2.Text)) && (Convert.ToDouble(lab_APP_11_2.Text) <= 138))
                {
                    lab_gap54.Text = "N";
                    lab_gap54.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_54.Text = "--";
                    lab_Pot_54.Text = "--";
                }


                else
                {
                    lab_gap54.Text = "Y";
                    lab_gap54.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_54.Text = Str_Effect[17] + "/" + Str_Effect[22] + "/" + Str_Effect[23] + "<br />"; ;
                    lab_Pot_54.Text = "Data incorrect<br />";
                }
            }


            if (lab_APP_11_3.Text == "")
            {
                lab_gap55.Text = "Y";
                lab_gap55.ForeColor = System.Drawing.Color.Red;
                lab_Eff_55.Text = Str_Effect[18] + "<br />";
                lab_Pot_55.Text = "Data incorrect<br />";
            }
            else {
                if (30 <= Convert.ToDouble(lab_APP_11_3.Text) && Convert.ToDouble(lab_APP_11_2.Text) <= 326)
                {
                    lab_gap55.Text = "N";
                    lab_gap55.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_55.Text = "--";
                    lab_Pot_55.Text = "--";
                }
                else
                {
                    lab_gap55.Text = "Y";
                    lab_gap55.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_55.Text = Str_Effect[18] + "<br />";
                    lab_Pot_55.Text = "Data incorrect<br />";
                }
            }


            if (lab_APP_11_4.Text == "")
            {
                lab_gap56.Text = "Y";
                lab_gap56.ForeColor = System.Drawing.Color.Red;
                lab_Eff_56.Text = Str_Effect[19] + "<br />";
                lab_Pot_56.Text = "Void escaped<br />";
            }
            else {
                if (30 <= Convert.ToDouble(lab_APP_11_4.Text) && Convert.ToDouble(lab_APP_11_4.Text) <= 326)
                {
                    lab_gap56.Text = "N";
                    lab_gap56.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_56.Text = "--";
                    lab_Pot_56.Text = "--";
                }
                else
                {
                    lab_gap56.Text = "Y";
                    lab_gap56.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_56.Text = Str_Effect[19] + "<br />";
                    lab_Pot_56.Text = "Void escaped <br />";
                }
            }


            if (lab_APP_11_5.Text == "")
            {
                lab_gap57.Text = "Y";
                lab_gap57.ForeColor = System.Drawing.Color.Red;
                lab_Eff_57.Text = Str_Effect[20] + "<br />";
                lab_Pot_57.Text = "Data incorrect <br />";
            }
            else {
                if (23 <= Convert.ToDouble(lab_APP_11_5.Text) && Convert.ToDouble(lab_APP_11_5.Text) <= 326)
                {
                    lab_gap57.Text = "N";
                    lab_gap57.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_57.Text = "--";
                    lab_Pot_57.Text = "--";

                }
                else
                {
                    lab_gap57.Text = "Y";
                    lab_gap57.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_57.Text = Str_Effect[20] + "<br />";
                    lab_Pot_57.Text = "Data incorrect <br />";
                }
            }




            if (lab_APP_11_6.Text == "")
            {
                lab_gap58.Text = "Y";
                lab_gap58.ForeColor = System.Drawing.Color.Red;
                lab_Eff_58.Text = Str_Effect[21] + "<br />";
                lab_Pot_58.Text = "Data incorrect <br />";
            }
            else
            {
                if ((200 <= Convert.ToDouble(lab_APP_11_6.Text) && Convert.ToDouble(lab_APP_11_6.Text) <= 326) ||
                (83 <= Convert.ToDouble(lab_APP_11_6.Text) && Convert.ToDouble(lab_APP_11_6.Text) <= 140))
                {
                    lab_gap58.Text = "N";
                    lab_gap58.ForeColor = System.Drawing.Color.Blue;
                    lab_Eff_58.Text = "--";
                    lab_Pot_58.Text = "--";
                }
                else
                {
                    lab_gap58.Text = "Y";
                    lab_gap58.ForeColor = System.Drawing.Color.Red;
                    lab_Eff_58.Text = Str_Effect[21] + "<br />";
                    lab_Pot_58.Text = "Data incorrect <br />";
                }


            }



            if (lab_POR_48.Text != lab_Man_20.Text)
            {
                lab_gap59.Text = "Y";
                lab_gap59.ForeColor = System.Drawing.Color.Red;
                lab_Eff_59.Text = Str_Effect[0] + "<br />";
                lab_Pot_59.Text = "Bump bridge <br />";
            }
            else
            {
                lab_gap59.Text = "N";
                lab_gap59.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_59.Text = "--";
                lab_Pot_59.Text = "--";
            }

            string temp_lab_man_21 = lab_Man_21.Text + ".00";




            if (lab_POR_49.Text != temp_lab_man_21)
            {
                lab_gap60.Text = "Y";
                lab_gap60.ForeColor = System.Drawing.Color.Red;
                lab_Eff_60.Text = "--";
                lab_Pot_60.Text = "--";
            }
            else
            {
                lab_gap60.Text = "N";
                lab_gap60.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_60.Text = "--";
                lab_Pot_60.Text = "--";
            }

            if (lab_POR_36.Text != lab_Man_22.Text)
            {
                lab_gap61.Text = "Y";
                lab_gap61.ForeColor = System.Drawing.Color.Red;
                lab_Eff_61.Text = Str_Effect[0] + "<br />";
                lab_Pot_61.Text = "Bump crack <br />";
            }
            else
            {
                lab_gap61.Text = "N";
                lab_gap61.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_61.Text = "--";
                lab_Pot_61.Text = "--";
            }

            if (lab_POR_38.Text != "NA")
            {
                lab_gap62.Text = "Y";
                lab_gap62.ForeColor = System.Drawing.Color.Red;
                lab_Eff_62.Text = "--";
                lab_Pot_62.Text = "--";
            }
            else
            {
                lab_gap62.Text = "N";
                lab_gap62.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_62.Text = "--";
                lab_Pot_62.Text = "--";
            }

            if (lab_POR_39.Text != "NA")
            {
                lab_gap63.Text = "Y";
                lab_gap63.ForeColor = System.Drawing.Color.Red;
                lab_Eff_63.Text = "--";
                lab_Pot_63.Text = "--";
            }
            else
            {
                lab_gap63.Text = "N";
                lab_gap63.ForeColor = System.Drawing.Color.Blue;
                lab_Pot_63.Text = "--";
                lab_Eff_63.Text = "--";
            }


            if (lab_POR_40.Text != "NA")
            {
                lab_gap64.Text = "Y";
                lab_gap64.ForeColor = System.Drawing.Color.Red;
                lab_Eff_64.Text = "--";
                lab_Pot_64.Text = "--";
            }
            else
            {
                lab_gap64.Text = "N";
                lab_gap64.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_64.Text = "--";
                lab_Pot_64.Text = "--";
            }

            if (lab_POR_41.Text != "NA")
            {
                lab_gap65.Text = "Y";
                lab_gap65.ForeColor = System.Drawing.Color.Red;
                lab_Eff_65.Text = "--";
                lab_Pot_65.Text = "--";
            }
            else
            {
                lab_gap65.Text = "N";
                lab_gap65.ForeColor = System.Drawing.Color.Blue;
                lab_Eff_65.Text = "--";
                lab_Pot_65.Text = "--";
            }

        
      










    }


    protected void Customer_TB_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Select_POR_Click(object sender, EventArgs e)
    {
        //日期輸入的頁面，將 TextBox 以 TextBoxId 網址參數傳給日期頁面 
        /*  sUrl = "POR_Golden.aspx?Device="+ this.por_golden_data[0] + "Production_Site=" + this.por_golden_data[1] + "PKG=" + this.por_golden_data[2] +
                      "Wafer=" + this.por_golden_data[3] + "Name_fab=" + this.por_golden_data[4] + "WaferPSV=" + this.por_golden_data[5] +
                      "RVSI=" + this.por_golden_data[6] + "Customer=" + this.por_golden_data[7];*/
        // sScript = "window.open('POR_Golden.aspx','','height=1024,width=1100,status=no,toolbar=no,menubar=no,location=no','')";
        // this.Label1.Attributes["onclick"] = sScript;



        String x, y;
        Boolean a, b;
        x = Session["value_sign1"].ToString();
        //y = Session["value_sign2"].ToString();

        a = Convert.ToBoolean(x);
        //b = Convert.ToBoolean(y);


        if (!a)
        {          
            
            string strScript = string.Format("<script language='javascript'>error_msg('Step1步驟未完成!');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

        }
        else {
            string strScript = string.Format("<script language='javascript'>AddWork();</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        }

    }



    protected void GAP_subfuntion(Label POR, Label New_Device,Label gap, Label Eff, Label Poten,Boolean sign,int[] str_array_eff,int[] str_array_poten)
    {
        int i = 0;

        if ((POR.Text != New_Device.Text) || (POR.Text == "") || (New_Device.Text == ""))
        {
            gap.Text = "Y";
            gap.ForeColor = System.Drawing.Color.Red;
            if(sign==true)
            {
                for (i = 0; i < str_array_eff.Length; i++)
                {
                    Eff.Text = Str_Effect[str_array_eff[i]] + "<br />";
                   
                }
            }
            
        }
        else
        {
            gap.Text = "N";
            gap.ForeColor = System.Drawing.Color.Blue;
            Eff.Text = "--";
            Poten.Text = "--";
        }




    }

    protected void GAP_subfuntion(Label POR, string New_Device, Label Eff, Label Poten, Boolean sign, int[] str_array)
    {
        int i = 0;

        if ((POR.Text != New_Device) || (POR.Text == "") || (New_Device == ""))
        {
            lab_gap2.Text = "Y";
            lab_gap2.ForeColor = System.Drawing.Color.Red;
            if (sign == true)
            {
                for (i = 0; i < str_array.Length; i++)
                {
                    Eff.Text = Str_Effect[str_array[i]] + "<br />";

                }
            }

        }
        else
        {
            lab_gap2.Text = "N";
            lab_gap2.ForeColor = System.Drawing.Color.Blue;
            Eff.Text = "--";
            Poten.Text = "--";
        }




    }


  



  

  


   protected Boolean jude_npiapp_SType(string strsql_npiapp)
    {
        string TableFild = "";
        int FieldCunt = 0;
        bool sign = false;
        int i;
       

        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();


        MySqlCommand MySqlCmd = new MySqlCommand(strsql_npiapp, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            TableFild = (String)SelData["SType"];
            
                                                              

        }

        SelData.Close();
        MySqlConn.Close();


        if (TableFild =="")        
            return false;
        else
            return true;



    }

    protected Boolean jude_npiimportdata_SType(string strsql_npiimportdata)
    {
        string TableFild = "";
        int FieldCunt = 0;
        bool sign = false;
        int i;


        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();


        MySqlCommand MySqlCmd = new MySqlCommand(strsql_npiimportdata, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            TableFild = (String)SelData["SType"];



        }

        SelData.Close();
        MySqlConn.Close();


        if (TableFild == "")
            return false;
        else
            return true;



    }

    protected Boolean jude_npimanual_SType(string strsql_npimanual)
    {
        string TableFild = "";
        int FieldCunt = 0;
        bool sign = false;
        int i;


        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();


        MySqlCommand MySqlCmd = new MySqlCommand(strsql_npimanual, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            TableFild = (String)SelData["SType"];



        }

        SelData.Close();
        MySqlConn.Close();


        if (TableFild == "")
            return false;
        else
            return true;



    }

    protected string jude_npiimportdata_which0ne_Stype(string jude_whichOne_Stype)
    {

        string TableFild = "";
        int count_sign = 0;
        int FieldCunt = 0;
        string stype_str = "";
        bool sign = false;
        int i;
        List<string> stype = new List<string>();

        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();


        MySqlCommand MySqlCmd = new MySqlCommand(jude_whichOne_Stype, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            TableFild = (String)SelData["SType"];
            stype.Add(TableFild);

        }

        SelData.Close();
        MySqlConn.Close();

        for(i=0;i<stype.Count;i++)
        {
            if("DRC"==stype[i])
            {
                stype_str += "DRC";
               
            }
            if("Q_R" == stype[i])
            {
                stype_str += "Q_R";                
            }
                      
            if ("DIF" == stype[i])
            {
                stype_str += "DIF";               
            }
       
        }


        if ("DRC" == stype_str)
            return "Q&R/DIF";
        else if ("Q_R" == stype_str)
            return "DIF/DRC";
        else if ("DIF" == stype_str)
            return "Q&R/DRC";
        else if ("DIFQ_R" == stype_str)
            return "DRC";
        else if ("Q_RDIF" == stype_str)
            return "DRC";
        else if ("DRCQ_R" == stype_str)
            return "DIF";
        else if ("Q_RDRC" == stype_str)
            return "DIF";
        else if ("DRCDIF" == stype_str)
            return "Q&R";
        else
            return "DRC/DIF/Q&R";


        



    }




    protected void ND_TB_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Search_Device1(object sender, EventArgs e)
    {
       // string SQL_Customer = " select DISTINCT npiapp.New_Customer from npiapp where npiapp.New_Customer like '" + prefix + "%' union  select  npiimportdata.New_Customer from  npiimportdata where   npiimportdata.New_Customer like '" + prefix + "%' union select  npimanual.New_Customer from npimanual where npimanual.New_Customer like'" + prefix + "%'";
       // string SQL_NewDevice = "SELECT DISTINCT npiimportdata.New_Device FROM npiimportdata";

        string strsql_npiapp = "select * from npiapp where New_Customer = '" + Customer_TB.Text + "'and New_Device = '" + ND_TB.Text + "'";
        string strsql_npiimportdata = "select * from npiimportdata where New_Customer = '" + Customer_TB.Text + "'and New_Device = '" + ND_TB.Text + "'";
        string strsql_npimanual = "select * from npimanual where New_Customer = '" + Customer_TB.Text + "'and New_Device = '" + ND_TB.Text + "'";
        string jude_whichOne_Stype = "select DISTINCT Stype from npiimportdata where New_Customer = '" + Customer_TB.Text + "'and New_Device = '" + ND_TB.Text + "'";
        Panel2.Visible = false;


        //jude_npiimportdata_which0ne_Stype(jude_whichOne_Stype);


if (Customer_TB.Text.Trim() != "" && ND_TB.Text.Trim() != "") { 
        if(jude_npiapp_SType(strsql_npiapp) && jude_npiimportdata_SType(strsql_npiimportdata) && jude_npimanual_SType(strsql_npimanual))
        {
                
            Panel1.Visible = true;
            receive_npiapp_data();
            put_npiimport_data();
            receive_npimanual_data();
            sign_NewDevice = true;
            HttpContext.Current.Session["value_sign1"] = sign_NewDevice;
        }
        else if((!jude_npiapp_SType(strsql_npiapp)) && jude_npiimportdata_SType(strsql_npiimportdata) && jude_npimanual_SType(strsql_npimanual))
        {
                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('" + jude_npiimportdata_which0ne_Stype(jude_whichOne_Stype) + " & Application File \\nNew_Customer:" + Customer_TB.Text + "\\nNew_Device:" + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);


        }
        else if(jude_npiapp_SType(strsql_npiapp) && (!jude_npiimportdata_SType(strsql_npiimportdata)) && jude_npimanual_SType(strsql_npimanual))
        {

                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('DIF/Q&R/DRC File \\nNew_Customer:" + Customer_TB.Text + "\\nNew_Device: " + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

         }
            else if(jude_npiapp_SType(strsql_npiapp) && jude_npiimportdata_SType(strsql_npiimportdata) && (!jude_npimanual_SType(strsql_npimanual)))
        {

                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('" + jude_npiimportdata_which0ne_Stype(jude_whichOne_Stype) + " & Mamual File \\nNew_Customer:" + Customer_TB.Text + "\\nNew_Device:" + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

        }
        else if(jude_npiapp_SType(strsql_npiapp) && (!jude_npiimportdata_SType(strsql_npiimportdata)) && (!jude_npimanual_SType(strsql_npimanual)))
        {

                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('DIF/Q&R/DRC File & Mamual File \\nNew_Customer:" + Customer_TB.Text + "\\nNew_Device:" + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);


        }
        else if((!jude_npiapp_SType(strsql_npiapp)) && (jude_npiimportdata_SType(strsql_npiimportdata)) && (!jude_npimanual_SType(strsql_npimanual)))
        {

                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('Application File & " + jude_npiimportdata_which0ne_Stype(jude_whichOne_Stype) + " File & Mamual File \\nNew_Customer:" + Customer_TB.Text + "\\nNew_Device:" + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

        }
            else if ((!jude_npiapp_SType(strsql_npiapp)) && (!jude_npiimportdata_SType(strsql_npiimportdata)) && (jude_npimanual_SType(strsql_npimanual)))
        {

                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('Application File & DIF/Q&R/DRC File  \\nNew_Customer:" + Customer_TB.Text + "\\nNew_Device:" + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

        }       
        else
        {
                HttpContext.Current.Session["value_sign1"] = false;
                string strScript = string.Format("<script language='javascript'>error_msg('Application File & " + jude_npiimportdata_which0ne_Stype(jude_whichOne_Stype) + " & Mamual File \\n New_Customer:" + Customer_TB.Text + "\\nNew_Device:" + ND_TB.Text + "\\n欄位無此資料，請重新填寫!');</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

        }
      }
    else
    {
            if (Customer_TB.Text.Trim() == "" && ND_TB.Text.Trim() == "")
            {
                HttpContext.Current.Session["value_sign1"] = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language='javascript'>alert('New_Customer與New_Device條件必須填寫');</script>");
            }
            if (Customer_TB.Text.Trim() == "" && ND_TB.Text.Trim() != "")
            {
                HttpContext.Current.Session["value_sign1"] = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language='javascript'>alert('New_Customer條件必須填寫');</script>");
            }
            if (Customer_TB.Text.Trim() != "" && ND_TB.Text.Trim() == "")
            {
                HttpContext.Current.Session["value_sign1"] = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language='javascript'>alert('New_Device條件必須填寫');</script>");
            }
     }


      
    }

  

    protected void ND_TB_TextChanged1(object sender, EventArgs e)
    {

    }




    protected Boolean jude_save_vername_null(string mySQL)
    {

        string TableFild = "";
        int sing=0;

        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();




        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            TableFild = (String)SelData["Ver_Name"];
            if (TableFild != "")
            {
                sing = 1;
            }
            else
            {
                sing = 0;
            }
        }

        SelData.Close();
        MySqlConn.Close();

        if (sing == 1)
            return true;
        else
            return false; 
        

    }


   







    protected void Change_Max_Status(int Max)
    {
        clsMySQL db = new clsMySQL();

       string strSQL_Update= "UPDATE npieptraver_main,npieptraver_gap,npieptraver_new,npieptraver_por SET npieptraver_main.Ver_Status = 'Disable',npieptraver_gap.Ver_Status = 'Disable',npieptraver_por.Ver_Status = 'Disable',npieptraver_new.Ver_Status = 'Disable' WHERE npieptraver_main.Ver_No= '" + Max +"'and npieptraver_gap.Ver_No ='"+ Max +"'and npieptraver_new.Ver_No ='"+ Max+ "'and npieptraver_por.Ver_No ='"+ Max + "'";




        try
        {
            /* if (db.QueryExecuteNonQuery(strSQL_Update))
              {
                  Response.Write("strSQL_Update");
              }
              else
              {
                  Response.Write(strSQL_Update + "<br />");
              }*/
            db.QueryExecuteNonQuery(strSQL_Update);
        }

        catch (Exception ex)
        {
            lblError.Text = ex.ToString() + "<br/>" + strSQL_Update;
        }



    }


   


    protected int search_VerNoMax_npieptraver_main()
    {

        
        string mySQL = " SELECT MAX(Ver_No) FROM npieptraver_main";
        int max=0;
        string temp_str_max="";
        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();




        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {
            max = (int)SelData["MAX(Ver_No)"];
            
        }

       

        SelData.Close();
        MySqlConn.Close();

        return max;



    }

    protected Boolean serch_MaxVerNo_Por_Golden(int Max)
    {

        int sing = 0;
        string mySQL = " SELECT * FROM npieptraver_main where Ver_No='" + Max + "'";
        string Customer = "";
        string ProductionSite = "";
        string WaferTech = "";
        string Device = "";
        string PKG = "";
        string WaferPSV = "";
        string RVSI = "";
        string FAB = "";


        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();




        MySqlCommand MySqlCmd = new MySqlCommand(mySQL, MySqlConn);
        MySqlDataReader SelData = MySqlCmd.ExecuteReader();

        while (SelData.Read())
        {


            Customer = (String)SelData["Ver_POR_Customer"];
            ProductionSite = (String)SelData["Ver_POR_Site"];
            WaferTech = (String)SelData["Ver_POR_WT"];
            Device = (String)SelData["Ver_POR_Device"];
            PKG = (String)SelData["Ver_POR_PKG"];
            RVSI = (String)SelData["Ver_POR_RVSI"];
            FAB = (String)SelData["Ver_POR_FAB"];
            WaferPSV = (String)SelData["Ver_POR_PSV"];
            if (Lab_Customer.Text == Customer && Lab_ProSite.Text == ProductionSite && Lab_WaferTech.Text == WaferTech
            && Lab_Device.Text == Device && Lab_PKG.Text == PKG && Lab_RVSI.Text == RVSI && Lab_FAB.Text == FAB)
            {
                sing = 0;
            }
            else
            {
                sing = 1;
            }

        }

       
        SelData.Close();
        MySqlConn.Close();
        
        if (sing == 1)
            return true;
        else
            return false;



    }

    protected void  save_strsql(int No,string ver_name,string username,string Status)
    {

        string temp_cap_por = "";
        temp_cap_por = Session["value_cap_list"].ToString();
        //char[] element = new char[] { '$' };
        string[] temp_cap_por_split = temp_cap_por.Split('$');
        string  temp_null = "";
       clsMySQL db = new clsMySQL();
        string   strSQL_Insert_main = string.Format("Insert into npieptraver_main" +
                                                            "(Ver_Name, Ver_No , UpdateTime , UserName, Ver_Status," +
                                                            "Ver_POR_Customer, Ver_POR_Device, Ver_POR_Site, Ver_POR_PKG, Ver_POR_WT," +
                                                            "Ver_POR_FAB,Ver_POR_PSV,Ver_POR_RVSI,Ver_New_Customer,Ver_New_Device)values " +
                                                            "('{0}','{1}', NOW(),'{2}','{3}','{4}','{5}','{6}','{7}'," +
                                                            "'{8}','{9}','{10}','{11}','{12}','{13}')"
                                                            , ver_name, No, username, Status,
                                                            Lab_Customer.Text, Lab_Device.Text, Lab_ProSite.Text, Lab_PKG.Text, Lab_WaferTech.Text,
                                                          Lab_FAB.Text, Lab_WaferThick.Text, Lab_RVSI.Text, Customer_TB.Text, ND_TB.Text);

       string  strSQL_Insert_NewDevice = string.Format("Insert into npieptraver_new" +
                                       "(Ver_Name,Ver_No ,UpdateTime, UserName, Ver_Status," +
                                       "Ver_NEW_1, Ver_NEW_2, Ver_NEW_3 , Ver_NEW_4 , Ver_NEW_5 ," +
                                       "Ver_NEW_6, Ver_NEW_7, Ver_NEW_8 , Ver_NEW_9 , Ver_NEW_10 ," +
                                       "Ver_NEW_11, Ver_NEW_12, Ver_NEW_13 , Ver_NEW_14 , Ver_NEW_15 ," +
                                       "Ver_NEW_16, Ver_NEW_17, Ver_NEW_18 , Ver_NEW_19 , Ver_NEW_20 ," +
                                       "Ver_NEW_21, Ver_NEW_22, Ver_NEW_23 , Ver_NEW_24 , Ver_NEW_25 ," +
                                       "Ver_NEW_26, Ver_NEW_27, Ver_NEW_28 , Ver_NEW_29 , Ver_NEW_30 ," +
                                       "Ver_NEW_31, Ver_NEW_32, Ver_NEW_33 , Ver_NEW_34 , Ver_NEW_35 ," +
                                       "Ver_NEW_36, Ver_NEW_37, Ver_NEW_38, Ver_NEW_39 , Ver_NEW_40 ," +
                                       "Ver_NEW_41, Ver_NEW_42, Ver_NEW_43 , Ver_NEW_44 , Ver_NEW_45 ," +
                                       "Ver_NEW_46, Ver_NEW_47, Ver_NEW_48 , Ver_NEW_49 , Ver_NEW_50 ," +
                                       "Ver_NEW_51, Ver_NEW_52, Ver_NEW_53 , Ver_NEW_54 , Ver_NEW_55 ," +
                                       "Ver_NEW_56, Ver_NEW_57, Ver_NEW_58 , Ver_NEW_59 , Ver_NEW_60 ," +
                                        "Ver_NEW_61, Ver_NEW_62, Ver_NEW_63 , Ver_NEW_64 , Ver_NEW_65)values" +
                                       "('{0}', '{1}' ,NOW(),'{2}'," +
                                       "'{3}','{4}','{5}','{6}','{7}'," +
                                       "'{8}','{9}','{10}','{11}','{12}'," +
                                       "'{13}','{14}','{15}','{16}','{17}'," +
                                       "'{18}','{19}','{20}','{21}','{22}'," +
                                       "'{23}','{24}','{25}','{26}','{27}'," +
                                       "'{28}','{29}','{30}','{31}','{32}'," +
                                       "'{33}','{34}','{35}','{36}','{37}'," +
                                       "'{38}','{39}','{40}','{41}','{42}'," +
                                       "'{43}','{44}','{45}','{46}', '{47}'," +
                                       "'{48}','{49}','{50}','{51}','{52}'," +
                                       "'{53}','{54}','{55}','{56}','{57}'," +
                                       "'{58}','{59}','{60}','{61}','{62}'," +
                                       "'{63}','{64}','{65}','{66}','{67}','{68}')"
                                     , ver_name, No, username, Status,
                                      temp_null, lab_DIFD4.Text, lab_DIFD5.Text, lab_DIFD19_DIFG19.Text, lab_Q_RD16.Text,

                                     lab_Q_RD13.Text, lab_Q_RD7.Text, lab_Q_RD8.Text, lab_Q_RD9.Text, lab_DIFF11.Text,

                                     lab_Q_RD11.Text, lab_Man_01.Text, lab_Q_RD12.Text, lab_APP_08.Text, lab_Man_02.Text,

                                      lab_Man_03.Text, lab_Man_04.Text, lab_Q_RD26.Text, lab_APP_21.Text, lab_DRCF38.Text,

                                      lab_DRCF39.Text, lab_DRCF35.Text, lab_Man_05.Text, lab_APP_33.Text, lab_DRCF34.Text, lab_Man_06.Text,

                                      lab_Man_07.Text, lab_Man_08.Text, lab_Man_09.Text, lab_Man_10.Text, lab_DIFD29.Text,

                                      lab_APP_09.Text, lab_APP_11.Text, lab_Man_11.Text, lab_Man_12.Text, lab_Q_RE26.Text,

                                     lab_APP_10.Text, lab_APP_12.Text, lab_Man_13.Text, lab_Man_14.Text, lab_Man_15.Text,

                                      lab_Man_16.Text, lab_Q_RD30.Text, lab_Man_17.Text, lab_Man_18.Text, lab_APP_21_2.Text,

                                     lab_APP_11_2.Text, "--", "--", "--", lab_Man_19.Text,

                                      lab_DIFD30.Text, lab_APP_09.Text, lab_APP_11_3.Text, lab_APP_11_4.Text, lab_APP_11_5.Text,

                                     lab_APP_11_6.Text, lab_APP_11_7.Text, lab_Man_20.Text, lab_Man_21.Text, lab_Man_22.Text,

                                       "NA", "NA", "NA", "NA");

     string   strSQL_Insert_GAP = string.Format("Insert into npieptraver_gap" +
                                                "(Ver_Name, Ver_No ,UpdateTime, UserName,LastName,Ver_Status," +
                                                "Ver_gap1, Ver_gap2, Ver_gap3 , Ver_gap4 , Ver_gap5 ," +
                                                "Ver_gap6, Ver_gap7, Ver_gap8 , Ver_gap9 , Ver_gap10 ," +
                                                "Ver_gap11, Ver_gap12, Ver_gap13 , Ver_gap14 , Ver_gap15 ," +
                                                "Ver_gap16, Ver_gap17, Ver_gap18 , Ver_gap19 ,Ver_gap20 ," +
                                                "Ver_gap21, Ver_gap22, Ver_gap23 , Ver_gap24 , Ver_gap25 ," +
                                                "Ver_gap26, Ver_gap27, Ver_gap28 , Ver_gap29 , Ver_gap30 ," +
                                                "Ver_gap31, Ver_gap32, Ver_gap33 , Ver_gap34 , Ver_gap35," +
                                                "Ver_gap36, Ver_gap37, Ver_gap38, Ver_gap39 , Ver_gap40 ," +
                                                "Ver_gap41, Ver_gap42, Ver_gap43 , Ver_gap44 , Ver_gap45," +
                                                "Ver_gap46, Ver_gap47, Ver_gap48 , Ver_gap49 , Ver_gap50 ," +
                                                "Ver_gap51 , Ver_gap52, Ver_gap53 , Ver_gap54 , Ver_gap55 ," +
                                                "Ver_gap56, Ver_gap57, Ver_gap58 , Ver_gap59 , Ver_gap60 ," +
                                                 "Ver_gap61, Ver_gap62, Ver_gap63 , Ver_gap64 , Ver_gap65)values" +
                                                "('{0}','{1}', NOW(),'{2}'," +
                                                "'{3}','{4}','{5}','{6}','{7}'," +
                                                "'{8}','{9}','{10}','{11}','{12}'," +
                                                "'{13}','{14}','{15}','{16}','{17}'," +
                                                "'{18}','{19}','{20}','{21}','{22}'," +
                                                "'{23}','{24}','{25}','{26}','{27}'," +
                                                "'{28}','{29}','{30}','{31}','{32}'," +
                                                "'{33}','{34}','{35}','{36}','{37}'," +
                                                "'{38}','{39}','{40}','{41}','{42}'," +
                                                "'{43}','{44}','{45}','{46}', '{47}'," +
                                                "'{48}','{49}','{50}','{51}','{52}'," +
                                                "'{53}','{54}','{55}','{56}','{57}'," +
                                                "'{58}','{59}','{60}','{61}','{62}'," +
                                                "'{63}','{64}','{65}','{66}','{67}','{68}','{69}')"
                                              , ver_name, No, username, username, Status,
                                               lab_gap1.Text, lab_gap2.Text, lab_gap3.Text, lab_gap4.Text, lab_gap5.Text,

                                             lab_gap6.Text, lab_gap7.Text, lab_gap8.Text, lab_gap9.Text, lab_gap10.Text,

                                               lab_gap11.Text, lab_gap12.Text, lab_gap13.Text, lab_gap14.Text, lab_gap15.Text,

                                               lab_gap16.Text, lab_gap17.Text, lab_gap18.Text, lab_gap19.Text, lab_gap20.Text,

                                                lab_gap21.Text, lab_gap22.Text, lab_gap23.Text, lab_gap24.Text, lab_gap25.Text, lab_gap26.Text,

                                               lab_gap27.Text, lab_gap28.Text, lab_gap29.Text, lab_gap30.Text, lab_gap31.Text,

                                                lab_gap32.Text, lab_gap33.Text, lab_gap34.Text, lab_gap35.Text, lab_gap36.Text,

                                              lab_gap37.Text, lab_gap38.Text, lab_gap39.Text, lab_gap40.Text, lab_gap41.Text,

                                               lab_gap42.Text, lab_gap43.Text, lab_gap44.Text, lab_gap45.Text, lab_gap46.Text,

                                               lab_gap47.Text, lab_gap48.Text, lab_gap49.Text, lab_gap50.Text, lab_gap51.Text,

                                                lab_gap52.Text, lab_gap53.Text, lab_gap54.Text, lab_gap55.Text, lab_gap56.Text,

                                               lab_gap57.Text, lab_gap58.Text, lab_gap59.Text, lab_gap60.Text, lab_gap61.Text,

                                                lab_gap62.Text, lab_gap63.Text, lab_gap64.Text, lab_gap65.Text);





        string strSQL_Insert_POR = string.Format("Insert into npieptraver_por" +
                                          "(Ver_Name,Ver_No ,UpdateTime, UserName, Ver_Status," +
                                          "Ver_POR_1, Ver_POR_2, Ver_POR_3 , Ver_POR_4 , Ver_POR_5 ," +
                                          "Ver_POR_6, Ver_POR_7, Ver_POR_8 , Ver_POR_9 , Ver_POR_10 ," +
                                          "Ver_POR_11, Ver_POR_12, Ver_POR_13 , Ver_POR_14 , Ver_POR_15 ," +
                                          "Ver_POR_16, Ver_POR_17, Ver_POR_18 , Ver_POR_19 , Ver_POR_20 ," +
                                          "Ver_POR_21, Ver_POR_22, Ver_POR_23 , Ver_POR_24 , Ver_POR_25 ," +
                                          "Ver_POR_26, Ver_POR_27, Ver_POR_28 , Ver_POR_29 , Ver_POR_30 ," +
                                          "Ver_POR_31, Ver_POR_32, Ver_POR_33 , Ver_POR_34 , Ver_POR_35 ," +
                                          "Ver_POR_36, Ver_POR_37, Ver_POR_38, Ver_POR_39 , Ver_POR_40 ," +
                                          "Ver_POR_41, Ver_POR_42, Ver_POR_43 , Ver_POR_44 , Ver_POR_45 ," +
                                          "Ver_POR_46, Ver_POR_47, Ver_POR_48 , Ver_POR_49 , Ver_POR_50 ," +
                                          "Ver_POR_51, Ver_POR_52, Ver_POR_53 , Ver_POR_54 , Ver_POR_55 ," +
                                          "Ver_POR_56, Ver_POR_57, Ver_POR_58 , Ver_POR_59 , Ver_POR_60 ," +
                                           "Ver_POR_61, Ver_POR_62, Ver_POR_63 , Ver_POR_64 , Ver_POR_65)values" +
                                          "('{0}','{1}',NOW(),'{2}'," +
                                          "'{3}','{4}','{5}','{6}','{7}'," +
                                          "'{8}','{9}','{10}','{11}','{12}'," +
                                          "'{13}','{14}','{15}','{16}','{17}'," +
                                          "'{18}','{19}','{20}','{21}','{22}'," +
                                          "'{23}','{24}','{25}','{26}','{27}'," +
                                          "'{28}','{29}','{30}','{31}','{32}'," +
                                          "'{33}','{34}','{35}','{36}','{37}'," +
                                          "'{38}','{39}','{40}','{41}','{42}'," +
                                          "'{43}','{44}','{45}','{46}', '{47}'," +
                                          "'{48}','{49}','{50}','{51}','{52}'," +
                                          "'{53}','{54}','{55}','{56}','{57}'," +
                                          "'{58}','{59}','{60}','{61}','{62}'," +
                                          "'{63}','{64}','{65}','{66}','{67}','{68}')"
                                        , ver_name, No, username, Status,
                                         lab_POR_15.Text, lab_POR_14.Text, lab_POR_17.Text, lab_POR_18.Text, lab_POR_46.Text,
                                         lab_POR_24.Text, lab_POR_04.Text, lab_POR_03.Text, lab_POR_12.Text, lab_POR_20.Text,
                                         lab_POR_23.Text, lab_POR_21.Text, lab_POR_25.Text, lab_POR_02.Text, lab_POR_26.Text,
                                        lab_POR_55.Text, lab_POR_16.Text, lab_POR_33.Text, lab_POR_30.Text, "NA",
                                        lab_POR_31.Text, lab_POR_32.Text, lab_POR_13.Text, lab_POR_29.Text, lab_POR_27.Text,
                                        lab_POR_28.Text, lab_POR_34.Text, lab_POR_50.Text, lab_POR_44.Text, lab_POR_43.Text,
                                        lab_POR_19.Text, lab_POR_35.Text, lab_POR_45.Text, lab_POR_22.Text, "1",
                                        temp_cap_por_split[0], temp_cap_por_split[1], temp_cap_por_split[2], temp_cap_por_split[3], temp_cap_por_split[4],
                                        temp_cap_por_split[5], temp_cap_por_split[6], lab_POR_10.Text, temp_cap_por_split[7], temp_cap_por_split[8],
                                        temp_cap_por_split[9], temp_cap_por_split[10], temp_cap_por_split[11], temp_cap_por_split[12], temp_cap_por_split[13],
                                        temp_cap_por_split[14], temp_cap_por_split[15], temp_cap_por_split[16], temp_cap_por_split[17], temp_cap_por_split[18],
                                         temp_cap_por_split[19], temp_cap_por_split[20], temp_cap_por_split[21], lab_POR_48.Text, lab_POR_49.Text,
                                        lab_POR_36.Text, lab_POR_38.Text, lab_POR_39.Text, lab_POR_40.Text, lab_POR_41.Text);




        try
        {

            db.QueryExecuteNonQuery(strSQL_Insert_main);

        }

        catch (Exception ex)
        {
            lblError.Text = ex.ToString() + "<br/>" + strSQL_Insert_main;
        }


        try
        {

            db.QueryExecuteNonQuery(strSQL_Insert_POR);

        }

        catch (Exception ex)
        {
            lblError.Text = ex.ToString() + "<br/>" + strSQL_Insert_POR;
        }


        try
        {

            db.QueryExecuteNonQuery(strSQL_Insert_NewDevice);

        }

        catch (Exception ex)
        {
            lblError.Text = ex.ToString() + "<br/>" + strSQL_Insert_NewDevice;
        }


        try
        {
            db.QueryExecuteNonQuery(strSQL_Insert_GAP);
        }

        catch (Exception ex)
        {
            lblError.Text = ex.ToString() + "<br/>" + strSQL_Insert_GAP;
        }



    }



    protected void SAVE_Click(object sender, EventArgs e)
    {
        DateTime dt =  DateTime.Now;
        string compaere_username = "select Ver_Name from npieptraver_main where Ver_Name LIKE'" + Customer_TB.Text + '_' +  ND_TB.Text + '%'+ "'";
      
        int No=0,i,j;
        string ver_name = Customer_TB.Text + "_" + ND_TB.Text ;
        string date = dt.ToString();
        string Status ="";
        string username = "CIM";
        int Max;
       

      


        if (jude_save_vername_null(compaere_username) == true)
        {

            Max = search_VerNoMax_npieptraver_main();
                            
              

                if (serch_MaxVerNo_Por_Golden(Max) == true)
                {

                    Change_Max_Status(Max);

                    No = Max;
                    No = No + 1;
                    Status = "Enable";
                    ver_name += "_Ver" + Convert.ToString(No);
                   
                    string strScript = string.Format("<script language='javascript'>error_msg('現在要儲存的版本為" + ver_name + "!');</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
                    save_strsql(No, ver_name, username, Status);

                }
                else
                {
                   
                    string strScript = string.Format("<script language='javascript'>error_msg('與前一版相同,重新選擇POR_Golden條件!');</script>");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
                }


                                 
        }

 
        else
        {
            No = 0;
            Status = "Enable";
            ver_name += "_Ver" + Convert.ToString(No);

         
            string strScript = string.Format("<script language='javascript'>error_msg('現在要儲存的版本為" + ver_name + "!');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);

            save_strsql(No, ver_name, username, Status);



        }











     /*   try
        {
          /*  if (db.QueryExecuteNonQuery(strSQL_Insert_main))
            {
                Response.Write("strSQL_Insert_main");
            }
            else
            {
                Response.Write(strSQL_Insert_main+"<br />");
            }*/

           /* if (db.QueryExecuteNonQuery(strSQL_Insert_POR))
            {
                Response.Write("strSQL_Insert_POR");
            }
            else
            {
                Response.Write(strSQL_Insert_POR+ "<br />");
            }*/
            /*if (db.QueryExecuteNonQuery(strSQL_Insert_NewDevice))
            {
                Response.Write("strSQL_Insert_NewDevice");
            }
            else
            {
                Response.Write(strSQL_Insert_NewDevice + "<br />");
            }
            if (db.QueryExecuteNonQuery(strSQL_Insert_GAP))
            {
                Response.Write("strSQL_Insert_GAP");
            }
            else
            {
                Response.Write(strSQL_Insert_GAP + "<br />");
            }
        }
        catch(Exception ex)
        {
            lblError.Text = ex.ToString() + "<br/>" + strSQL_Insert_POR;
        }
        */

       



    }

   
}




/*string strSQL_Insert_POR = string.Format("Insert into npieptraver_por" +
                                               "(Ver_Name ,UpdateTime, UserName, Ver_Status," +
                                               "Ver_POR_2, Ver_POR_3 , Ver_POR_4," +
                                               "Ver_POR_10 ," +
                                               " Ver_POR_12, Ver_POR_14 , Ver_POR_15 ," +
                                               " Ver_POR_17, Ver_POR_18 , Ver_POR_19 , Ver_POR_20 ," +
                                               "Ver_POR_21, Ver_POR_22, Ver_POR_23 , Ver_POR_24 , Ver_POR_25 ," +
                                               "Ver_POR_26, Ver_POR_27, Ver_POR_28 , Ver_POR_29 , Ver_POR_30 ," +
                                               "Ver_POR_31, Ver_POR_32, Ver_POR_33 , Ver_POR_34 , Ver_POR_35 ," +
                                               "Ver_POR_36, Ver_POR_38, Ver_POR_39 , Ver_POR_40 ," +
                                               "Ver_POR_41,  Ver_POR_43 , Ver_POR_44 , Ver_POR_45 ," +
                                               "Ver_POR_46, Ver_POR_48 , Ver_POR_49 , Ver_POR_50 ," +
                                               " Ver_POR_55)values" +
                                               "('{0}', NOW(),'{1}','{2}'," +
                                               "'{3}','{4}','{5}','{6}','{7}'," +
                                               "'{8}','{9}','{10}','{11}','{12}'," +
                                               "'{13}','{14}','{15}','{16}','{17}'," +
                                               "'{18}','{19}','{20}','{21}','{22}'," +
                                               "'{23}','{24}','{25}','{26}','{27}'," +
                                               "'{28}','{29}','{30}','{31}','{32}'," +
                                               "'{33}','{34}','{35}','{36}','{37}'," +
                                               "'{38}','{39}','{40}','{41}')"
                                             , ver_name,username, Status,
                                              lab_POR_02.Text, lab_POR_03.Text, lab_POR_04.Text,
                                              lab_POR_10.Text,
                                               lab_POR_12.Text, lab_POR_14.Text, lab_POR_15.Text,
                                             lab_POR_16.Text, lab_POR_17.Text, lab_POR_18.Text, lab_POR_19.Text, lab_POR_20.Text,
                                             lab_POR_21.Text, lab_POR_22.Text, lab_POR_23.Text, lab_POR_24.Text, lab_POR_25.Text,
                                             lab_POR_26.Text, lab_POR_27.Text, lab_POR_28.Text, lab_POR_29.Text, lab_POR_30.Text,
                                             lab_POR_31.Text, lab_POR_32.Text, lab_POR_33.Text, lab_POR_34.Text, lab_POR_35.Text,
                                             lab_POR_36.Text, lab_POR_38.Text, lab_POR_39.Text, lab_POR_40.Text,
                                             lab_POR_41.Text, lab_POR_43.Text, lab_POR_44.Text,
                                             lab_POR_46.Text, lab_POR_48.Text, lab_POR_49.Text, lab_POR_50.Text,
                                            lab_POR_55.Text);
                                              */
