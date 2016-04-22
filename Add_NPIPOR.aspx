<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_NPIPOR.aspx.cs" Inherits="PreNPI_Add_NPIPOR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add NPI POR</title>
    <link rel="stylesheet" href="..\css\styles.css" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://jqueryui.com/resources/demos/external/jquery.bgiframe-2.1.2.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>

    <script language="javascript" type="text/javascript">
        function ConfirmDel(strMsg, strData) {
            var isOK = confirm(strMsg);
            if (isOK) {
                PageMethods.DEL_An_POR(strData, OnSuccess, OnFail);
            }
        }
        function ConfirmUpdate(strMsg, strData) {
            var isOK = confirm(strMsg);
            if (isOK) {
                PageMethods.Update_An_POR(strData, OnSuccess, OnFail);
            }
        }
        function ConfirmInsert(strMsg, strData) {
            var isOK = confirm(strMsg);
            if (isOK) {
                PageMethods.Insert_An_POR(strData, OnSuccess, OnFail);
            }
        }
        function OnSuccess(receiveData, userContext, methodName) {
            //成功時，目地控制項顯示所接收結果   
            if (receiveData == "") {
                window.$('#cmdFilter').click();
                //document.getElementById("lblError").innerHTML = "Delete Finish!!";
                //document.getElementById('<%=cmdFilter.ClientID%>').click();               

            } else {
                alert(methodName + ": " + receiveData);
            }
        }

        function OnFail(error, userContext, methodName) {
            if (error != null) {
                alert(methodName + ": " + error.get_message());
            }
        }
        $(function () {

            $(document).ready(function () {
                $("#datepicker").datepicker();
            });

            $.datepicker.regional['zh-TW'] = {
                clearText: '清除', clearStatus: '清除已選日期',
                closeText: '關閉', closeStatus: '取消選擇',
                prevText: '<上一月', prevStatus: '顯示上個月',
                nextText: '下一月>', nextStatus: '顯示下個月',
                currentText: '今天', currentStatus: '顯示本月',
                monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
			'七月', '八月', '九月', '十月', '十一月', '十二月'],
                monthNamesShort: ['一', '二', '三', '四', '五', '六',
			'七', '八', '九', '十', '十一', '十二'],
                monthStatus: '選擇月份', yearStatus: '選擇年份',
                weekHeader: '周', weekStatus: '',
                dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
                dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
                dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
                dayStatus: '設定每周第一天', dateStatus: '選擇 m月 d日, DD',
                dateFormat: 'yy/mm/dd', firstDay: 1,
                initStatus: '請選擇日期', isRTL: false
            };
            $("#datepicker1").datepicker();
            $("#datepicker2").datepicker();
            $.datepicker.setDefaults($.datepicker.regional['zh-TW']);
        });
</script>  
    <style type="text/css">
         table, th {
    border: 1px solid white;
    border-collapse: collapse;       
    text-align:left;
}
th,td {
    padding: 5px;            
}

table#table1, th {
    border: 1px solid white;
    border-collapse: collapse;   
    text-align:left;     
}
         table#table2, th {
    border: 1px solid white;
    border-collapse: collapse;   
    text-align:left;     
}
table#table1 th,td {
    padding: 5px;        
}
table#table2 th,td {
    padding: 5px;        
}

        .style1
        {
            width: 100%;
            text-align:left;
        }
                        
         myfield {            
      padding: 20px;
      font-size: large;
  }  
        .th01
        {
            color: #FFFFFF;
            background-color:#333366;
        }
          .th02
        {
            color: #FFFFFF;
            background-color:#3399FF;
        }
    </style>   
</head>
<body>
    <form id="form1" runat="server">        
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
     <br />
     <asp:Button ID="cmdFilter" runat="server" OnClick="cmdFilter_Click" Text="Button" ClientIDMode="Static" Style="display: none;" /> 
     <asp:Panel ID="Panel1" runat="server" Visible="True">  
      <fieldset>
    <legend><myfield>Plating Solder POR Information</myfield></legend> 
          <table style="width: 100%;">
              <tr>
                  <td>Production Site</td>
                  <td> 
                  <asp:DropDownList ID="ddl_Site" runat="server">
                            <asp:ListItem Value="0">All Site</asp:ListItem>
                            <asp:ListItem Value="CH">CH</asp:ListItem>
                            <asp:ListItem Value="CS">CS</asp:ListItem>
                            <asp:ListItem Value="DF">DF</asp:ListItem>
                            <asp:ListItem Value="ZK">ZK</asp:ListItem>
                        </asp:DropDownList>                    
                  </td>
                  <td>PKG</td>
                  <td><asp:TextBox ID="text_PKG" runat="server"></asp:TextBox></td>
                  <td>Wafer Tech.(nm)</td>
                  <td><asp:TextBox ID="text_WaferTech" runat="server"></asp:TextBox></td>
                  <td>FAB</td>
                  <td> <asp:DropDownList ID="ddl_Fab" runat="server">
                            <asp:ListItem Value="0">All FAB</asp:ListItem>
                            <asp:ListItem Value="GF">GF</asp:ListItem>
                            <asp:ListItem Value="SMIC">SMIC</asp:ListItem>
                            <asp:ListItem Value="TSMC">TSMC</asp:ListItem>
                            <asp:ListItem Value="UMC">UMC</asp:ListItem>
                        </asp:DropDownList></td>
              </tr>
              <tr>
                  <td>Wafer PSV Type/Thickness</td>                  
                  <td><asp:DropDownList ID="ddl_PSV" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="SiN">SiN</asp:ListItem>
                            <asp:ListItem Value="TSMC PI">TSMC PI</asp:ListItem>
                        </asp:DropDownList></td>
                  <td>RSVI(Y/N)</td>
                  <td> <asp:DropDownList ID="ddl_RSVI" runat="server">
                            <asp:ListItem Value="0">All RSVI</asp:ListItem>
                            <asp:ListItem Value="Y">Y</asp:ListItem>
                            <asp:ListItem Value="N">N</asp:ListItem>
                        </asp:DropDownList></td>
                  <td>Customer</td>
                  <td><asp:TextBox ID="text_Cust" runat="server"></asp:TextBox></td>
                  <td colspan="2" style="text-align: center">
                      <asp:Button ID="btn_Search" runat="server" Text="Search" class="blueButton button2"
                          onclick="btn_Search_Click" />
                  </td>
              </tr>
          </table>
          <hr/>
    <asp:GridView ID="GridView1" runat="server"  AllowPaging="True" 
            AutoGenerateColumns="False" PagerStyle-Wrap ="true"
            CellPadding="3" DataKeyNames="POR_Customer,POR_Device,POR_01" EmptyDataText="沒有資料錄可顯示。" 
            onrowcommand="GridView1_RowCommand"              
            onpageindexchanging="GridView1_PageIndexChanging" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
            onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing">
            <EmptyDataTemplate>
                <asp:Button ID="btnInsert" runat="server" CausesValidation="False" 
                    CommandName="Insert" Text="InsertPOR" />
            </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField ShowHeader="False"> 
                  <HeaderTemplate>
                      <asp:Button ID="btnInsert" runat="server" CausesValidation="False" CommandName="Insert" class="blueButton button2"
                           Text="NewPOR"></asp:Button>
                  </HeaderTemplate>
                  <ItemTemplate>                     
                        <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="False" 
                        CommandName="Select" Height="25px" ImageUrl="~/Images/img/View.png" 
                        Width="25px" />
                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" 
                        CommandName="Edit" Height="25px" ImageUrl="~/Images/img/Print.png" 
                        Width="25px" />
                        <asp:ImageButton ID="btnDelete" runat="server"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False" CommandName="DEL_POR"
                        Height="25px" ImageUrl="~/Images/img/Del.png" Width="25px" />
                  </ItemTemplate>
            </asp:TemplateField>           
            <asp:BoundField DataField="Por_Customer" HeaderText="Customer" 
                SortExpression="Por_Customer" />
            <asp:BoundField DataField="Por_Device" HeaderText="Device" 
                SortExpression="Por_Device" />
            <asp:BoundField DataField="POR_01" HeaderText="PorductionSite" 
                SortExpression="POR_01" HtmlEncode="False" />
            <asp:BoundField DataField="POR_02" HeaderText="PKG" SortExpression="POR_02" />
            <asp:BoundField DataField="POR_03" HeaderText="WaferTech.(nm)" 
                SortExpression="POR_03" HtmlEncode="False" />
            <asp:BoundField DataField="POR_04" HeaderText="FAB" SortExpression="POR_04" />
            <asp:BoundField DataField="POR_05" HeaderText="WaferPSVType/Thickness" 
                SortExpression="POR_05" HtmlEncode="False" />
            <asp:BoundField DataField="POR_11" HeaderText="100%RVSI" SortExpression="POR_11" />
        </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" 
                Wrap="True" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
     </fieldset>
     </asp:Panel>  
    <asp:Panel ID="Panel2" runat="server" Visible="False">   
    <fieldset>
    <legend style="font-weight: 700; font-size: large;"><myfield>Create Plating Solder POR</myfield></legend>
    <asp:FormView ID="FormView1" runat="server" CellPadding="4" DataKeyNames="POR_Customer,POR_Device,POR_01"
             DefaultMode="ReadOnly" HorizontalAlign="Left" Width="100%" EmptyDataText="No POR Information found."
                OnItemInserting="FormView1_ItemInserting" 
                onprerender="FormView1_PreRender" 
            onitemcommand="FormView1_ItemCommand" oniteminserted="FormView1_ItemInserted" 
            onmodechanging="FormView1_ModeChanging" 
            onitemupdated="FormView1_ItemUpdated" onitemupdating="FormView1_ItemUpdating">        
        <InsertItemTemplate>
           <table class="style1" id="table2">
                	<tr>
                	<th class="th01">*01</th>
                    <th class="th02">ProductionSite</th>
                    <td bgcolor="#FFF">
                        <asp:DropDownList ID="ddPOR01" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>CH</asp:ListItem>
                            <asp:ListItem>CS</asp:ListItem>
                            <asp:ListItem>DF</asp:ListItem>
                            <asp:ListItem>ZK</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		    		<th class="th01">15</th>
                    <th class="th02">PROD</th>
                    <td bgcolor="#FFF">
                        <asp:Label ID="lab_Pord" runat="server"></asp:Label>
                        -
                        <asp:DropDownList ID="ddPord" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>9D</asp:ListItem>
                            <asp:ListItem>TG</asp:ListItem>
                            <asp:ListItem>ZK</asp:ListItem>
                            <asp:ListItem>JA</asp:ListItem>
                        </asp:DropDownList>
                        -
                        <asp:TextBox ID="text_No" runat="server" Width="42px"></asp:TextBox>
                    </td>

		    		<th class="th01">29</th>
                    <th class="th02">UBMSize</th>
                    <td bgcolor="#FFF">
                        <asp:TextBox ID="text_POR29" runat="server" />
                    </td>

		    		<th class="th01">43</th>
                    <th class="th02">Mushroon<br/>Space</th>
                    <td bgcolor="#FFF">                    
                        <asp:TextBox ID="text_POR43" runat="server" />
                    </td>
                </tr>
                <tr>                    
                    <th class="th01">*02</th>
                    <th class="th02">PKG</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR02" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddPOR02_SelectedIndexChanged">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>EP FOC-12-EU</asp:ListItem>
                            <asp:ListItem>EP FOC-12-LF</asp:ListItem>
                            <asp:ListItem>EP REPSV-12-EU</asp:ListItem>
                            <asp:ListItem>EP REPSV-12-LF</asp:ListItem>
                            <asp:ListItem>EP REPSV-8-LF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		    		<th class="th01">16</th>
                    <th class="th02">UBMType<br/>/Thickness</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR16" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu3k/Cu5um/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu3K/Ni3.5um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Cu5um/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Ni2um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Ni3.5um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti3K/Cu3K/Cu3um/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti3K/Cu3K/Cu5um/Ni2um</asp:ListItem>
                            <asp:ListItem>Ti3K/Cu3K/Ni2um</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		   			<th class="th01">30</th>
                    <th class="th02">REPSVPI<br/>OpeningSize</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR30" runat="server" />
                    </td>
 	                <th class="th01">44</th>
                    <th class="th02">MushroonCD</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR44" runat="server" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">*03</th>
                    <th class="th02">WaferTech.(nm)</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR03" runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR03" runat="server" 
                            ControlToValidate="text_POR03" Display="Static" ErrorMessage="請輸入WaferTech.!!" 
                            ForeColor="red" />
                    </td>                    
                    <th class="th01">*17</th>
                    <th class="th02">Device</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR17" runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR17" runat="server" 
                            ControlToValidate="text_POR17" Display="Static" ErrorMessage="請輸入Device" 
                            ForeColor="red" />
                    </td>                    
                    <th class="th01">31</th>
                    <th class="th02">PIviaOpening<br/>BottomEdgeTo<br/>Padpsv.Edge</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR31" runat="server" />
                    </td>                    
                    <th class="th01">45</th>
                    <th class="th02">Bump<br/>Diameter</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR45" runat="server" AutoPostBack="true" 
                            ontextchanged="text_POR45_TextChanged" />
                    </td>                   
                </tr>
                <tr>                    
                    <th class="th01">*04</th>
                    <th class="th02">FAB</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR04" runat="server">
                        <asp:ListItem>請選擇</asp:ListItem>
                        <asp:ListItem>GF</asp:ListItem>
                        <asp:ListItem>SMIC</asp:ListItem>
                        <asp:ListItem>TSMC</asp:ListItem>
                        <asp:ListItem>UMC</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="text_POR04" runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR04" runat="server" 
                            ControlToValidate="text_POR04" Display="Static" ErrorMessage="請輸入FAB" 
                            ForeColor="red" />--%>
                    </td>                    
                    <th class="th01">18</th>
                    <th class="th02">DieSize</th>
                    <td bgcolor="#eee"> 
                        <asp:Label ID="labPOR18" runat="server"></asp:Label>
                        <br/>
                        [請輸入DieSizeX與Y]
                    </td>
                    <th class="th01">32</th>
                    <th class="th02">PIEdgeInside<br/>SealRing</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR32" runat="server" />
                    </td> 		    		
 		    		<th class="th01">46</th>
                    <th class="th02">C/PProbe<br/>CardType</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR46" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Membrane</asp:ListItem>
                            <asp:ListItem>Vertical Probe</asp:ListItem>
                        </asp:DropDownList>
                    </td>                  
                </tr>
                <tr>                    
                    <th class="th01">*05</th>
                    <th class="th02">WaferPSVType<br/>/Thickness</th>
                    <td bgcolor="#fff"> 
                     <asp:DropDownList ID="ddPOR05" runat="server">
                        <asp:ListItem>請選擇</asp:ListItem>
                        <asp:ListItem>SiN</asp:ListItem>
                        <asp:ListItem>TSMC PI</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="text_POR05" runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR05" runat="server" 
                            ControlToValidate="text_POR05" Display="Static" 
                            ErrorMessage="請輸入WaferPSVType/Thickness" ForeColor="red" />--%>
                    </td>                    
                    <th class="th01">19</th>
                    <th class="th02">MinBumpPitch</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR19" runat="server" />
                    </td>                    
                    <th class="th01">33</th>
                    <th class="th02">BumpType</th>
                    <td bgcolor="#fff"> 
                        <asp:DropDownList ID="ddPOR33" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>1.8</asp:ListItem>
                            <asp:ListItem>2.3</asp:ListItem>
                            <asp:ListItem>Eu</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">47</th>
                    <th class="th02">MinFinalMetal<br/>PadToSealRing</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR47" runat="server" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">06</th>
                    <th class="th02">PRType</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR06" runat="server" /><br/>
                       <%-- <asp:RequiredFieldValidator ID="Valid_POR06" runat="server" 
                            ControlToValidate="text_POR06" Display="Static" ErrorMessage="請輸入PRType" 
                            ForeColor="red" />--%>
                    </td>                    
                    <th class="th01">20</th>
                    <th class="th02">FinalMetal<br/>PadType</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR20" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Al</asp:ListItem>
                            <asp:ListItem>Cu</asp:ListItem>
                        </asp:DropDownList>
                    </td>                     
                    <th class="th01">34</th>
                    <th class="th02">UBMPlating<br/>Area</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR34" runat="server" />
                    </td>		    
		    		<th class="th01">48</th>
                    <th class="th02">BumpTo<br/>BumpSpace</th>
                    <td bgcolor="#eee"> 
                        <asp:Label ID="labPOR48" runat="server" Text="0"></asp:Label>
                        <br/>
                        [輸入MinBumpPitch與BumpDeameter]</td>                </tr>
                <tr>                    
                    <th class="th01">07</th>
                    <th class="th02">TiEtching<br/>Chemical(T89/DHF)</th>
                    <td bgcolor="#fff"> 
                        <asp:DropDownList ID="ddPOR07" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>T89</asp:ListItem>
                            <asp:ListItem>DHF</asp:ListItem>
                            <asp:ListItem>T89+DHF</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">21</th>
                    <th class="th02">WaferPSV<br/>TypeThickness</th>
                    <td bgcolor="#fff"> 
                        <asp:DropDownList ID="ddPOR21" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>SiN</asp:ListItem>
                            <asp:ListItem>SiN/0.9um</asp:ListItem>
                            <asp:ListItem>SiN/1.1um</asp:ListItem>
                            <asp:ListItem>SiN/1.9um</asp:ListItem>
                            <asp:ListItem>SiN/2um</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">35</th>
                    <th class="th02">BumpHeight</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR35" runat="server" />
                    </td> 		     
 		     		<th class="th01">49</th>
                    <th class="th02">SMO</th>
                    <td bgcolor="#fff"> 
                        <asp:Label ID="labPOR49" runat="server" Text="0"></asp:Label>
                        <br/>
                        [請輸入UBMSize與UBM/SMORatio]</td>                    
                </tr>
                <tr>                    
                    <th class="th01">08</th>
                    <th class="th02">TinShellBake</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR08" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">22</th>
                    <th class="th02">BumpDensity</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR22" runat="server" />
                    </td>                     
                    <th class="th01">36</th>
                    <th class="th02">UBM/SMO<br/>Ratio</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR36" runat="server" AutoPostBack="true" 
                            ontextchanged="text_POR36_TextChanged" />
                    </td>                    
                    <th class="th01">50</th>
                    <th class="th02">UBMDensity</th>
                    <td bgcolor="#eee">
                        <asp:Label ID="labPOR50" runat="server" text="0"></asp:Label>
                        %<br/>[請輸入UBMSize,DieSizeX/Y與BumpCount]</td>                    
                </tr>
                <tr>                    
                    <th class="th01">09</th>
                    <th class="th02">PIrougness</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR09" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                            <asp:ListItem>BGM2:7~15nm</asp:ListItem>
                            <asp:ListItem>BGM3A:15~30nm</asp:ListItem>
                            <asp:ListItem>SAD:1~8%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="th01">23</th>
                    <th class="th02">RVHoleOnAlPad</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR23" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">37</th>
                    <th class="th02">BH/UBMRatio</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR37" runat="server" />
                    </td>                    
                    <th class="th01">51</th>
                    <th class="th02">DieSizeX</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR51" runat="server" />
                    </td>                  
                </tr>
                <tr>
                    <th class="th01">10</th>
                    <th class="th02">BumpResistance<br/>Capability</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR10" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>&lt;20mohm</asp:ListItem>
                            <asp:ListItem>&lt;30mohm</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="th01">24</th>
                    <th class="th02">ProbingOnPad</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR24" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">38</th>
                    <th class="th02">Process/Machine<br/>Ratio</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR38" runat="server" Text="NA" />
                    </td>
                    <th class="th01">52</th>
                    <th class="th02">DieSizeY</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR52" runat="server" AutoPostBack="true" 
                            ontextchanged="text_POR52_TextChanged" />
                    </td>
                </tr>
                <tr>
                    <th class="th01">*11</th>
                    <th class="th02">100%RVSI</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR11" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		    		<th class="th01">25</th>
                    <th class="th02">SelRing<br/>ProtectedBySiN</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR25" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="th01">39</th>
                    <th class="th02">Material</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR39" runat="server" Text="NA" />
                    </td>                    
                    <th class="th01">53</th>
                    <th class="th02">BumpCount</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR53" runat="server" AutoPostBack="true" 
                            ontextchanged="text_POR53_TextChanged" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">12</th>
                    <th class="th02">LowK</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR12" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>E Low K</asp:ListItem>
                            <asp:ListItem>ELK</asp:ListItem>
                            <asp:ListItem>Low K</asp:ListItem>
                            <asp:ListItem>Non-Low K</asp:ListItem>
                        </asp:DropDownList>
                    </td>		    
		    		<th class="th01">26</th>
                    <th class="th02">PassivationType</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR26" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                            <asp:ListItem>HD4104</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">40</th>
                    <th class="th02">Measurement<br/>Tool</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR40" runat="server" Text="NA" />
                    </td>                    
                    <th class="th01">54</th>
                    <th class="th02">CornerUBM<br/>Density</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR54" runat="server" />
                    </td>                   
                </tr>
                <tr>                    
                    <th class="th01">13</th>
                    <th class="th02">PRThickness</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR13" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">27</th>
                    <th class="th02">UBMOverlapPSV</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR27" runat="server" />
                    </td>                                        
                    <th class="th01">41</th>
                    <th class="th02">Raliability</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR41" runat="server" Text="NA" />
                    </td>                    
                    <th class="th01">55</th>
                    <th class="th02">Passivation<br/>Thickness</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR55" runat="server" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">*14</th>
                    <th class="th02">Customer</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR14" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>AMD</asp:ListItem>
                            <asp:ListItem>Avago</asp:ListItem>
                            <asp:ListItem>Avago-LS</asp:ListItem>
                            <asp:ListItem>ESILICON</asp:ListItem>
                            <asp:ListItem>Infineon</asp:ListItem>
                            <asp:ListItem>LSI</asp:ListItem>
                            <asp:ListItem>Magnum</asp:ListItem>
                            <asp:ListItem>Marvell</asp:ListItem>
                            <asp:ListItem>MEDIATEK</asp:ListItem>
                            <asp:ListItem>NV</asp:ListItem>
                            <asp:ListItem>Parade</asp:ListItem>
                            <asp:ListItem>PARROT</asp:ListItem>
                            <asp:ListItem>Sigma Design</asp:ListItem>
                            <asp:ListItem>SMIC</asp:ListItem>
                            <asp:ListItem>SPIL</asp:ListItem>
                            <asp:ListItem>TSMC-QCT</asp:ListItem>
                            <asp:ListItem>UMC</asp:ListItem>
                            <asp:ListItem>VeriSilicon-CN</asp:ListItem>
                            <asp:ListItem>XILINX</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">24</th>
                    <th class="th02">UBMInsdie<br/>FinalMetalForFOC</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR28" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>3(TSMC PI)</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                        </asp:DropDownList>
                    </td>                     
                    <th class="th01">42</th>
                    <th class="th02">Remark</th>                    
                    <td bgcolor="#eee" colspan="3">                        
                        <asp:TextBox ID="text_POR42" runat="server" Width="300px" TextMode="MultiLine" Height="50"></asp:TextBox>
                    </td>  
                </tr>            
            </table>   
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="新增"></asp:LinkButton>
            <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="取消"></asp:LinkButton>
        </InsertItemTemplate>
        <ItemTemplate>
            <table ID="table1" class="style1">
                <tr>
                    <td align="center" bgcolor="#0066CC" colspan="12">
                        <font color="White"><b>Plating Solder POR</b> </font>
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *01</th>
                    <th class="th02">
                        ProductionSite</th>
                    <td bgcolor="#FFF">
                        <%# Eval("POR_01")%>
                        <br />
                    </td>
                    <th class="th01">
                        15</th>
                    <th class="th02">
                        PROD</th>
                    <td bgcolor="#FFF">
                        <%# Eval("POR_15")%>
                        <br />
                    </td>
                    <th class="th01">
                        29</th>
                    <th class="th02">
                        UBMSize</th>
                    <td bgcolor="#FFF">
                        <%# Eval("POR_29")%>
                        <br />
                    </td>
                    <th class="th01">
                        43</th>
                    <th class="th02">
                        Mushroon<br />Space</th>
                    <td bgcolor="#FFF">
                        <%# Eval("POR_43")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *02</th>
                    <th class="th02">
                        PKG</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_02")%>
                        <br />
                    </td>
                    <th class="th01">
                        16</th>
                    <th class="th02">
                        UBMType<br />/Thickness</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_16")%>
                        <br />
                    </td>
                    <th class="th01">
                        30</th>
                    <th class="th02">
                        REPSVPI<br />OpeningSize</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_30")%>
                        <br />
                    </td>
                    <th class="th01">
                        44</th>
                    <th class="th02">
                        MushroonCD</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_44")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *03</th>
                    <th class="th02">
                        WaferTech.(nm)</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_03")%>
                        <br />
                    </td>
                    <th class="th01">
                        *17</th>
                    <th class="th02">
                        Device</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_17")%>
                        <br />
                    </td>
                    <th class="th01">
                        31</th>
                    <th class="th02">
                        PIviaOpening<br />BottomEdgeTo<br />Padpsv.Edge</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_31")%>
                        <br />
                    </td>
                    <th class="th01">
                        45</th>
                    <th class="th02">
                        Bump<br />Diameter</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_45")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *04</th>
                    <th class="th02">
                        FAB</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_04")%>
                        <br />
                    </td>
                    <th class="th01">
                        18</th>
                    <th class="th02">
                        DieSize</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_18")%>
                        <br />
                    </td>
                    <th class="th01">
                        32</th>
                    <th class="th02">
                        PIEdgeInside<br />SealRing</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_32")%>
                        <br />
                    </td>
                    <th class="th01">
                        46</th>
                    <th class="th02">
                        C/PProbe<br />CardType</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_46")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *05</th>
                    <th class="th02">
                        WaferPSVType<br />/Thickness</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_05")%>
                        <br />
                    </td>
                    <th class="th01">
                        19</th>
                    <th class="th02">
                        MinBumpPitch</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_19")%>
                        <br />
                    </td>
                    <th class="th01">
                        33</th>
                    <th class="th02">
                        BumpType</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_33")%>
                        <br />
                    </td>
                    <th class="th01">
                        47</th>
                    <th class="th02">
                        MinFinalMetal<br />PadToSealRing</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_47")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *06</th>
                    <th class="th02">
                        PRType</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_06")%>
                        <br />
                    </td>
                    <th class="th01">
                        20</th>
                    <th class="th02">
                        FinalMetal<br />PadType</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_20")%>
                        <br />
                    </td>
                    <th class="th01">
                        34</th>
                    <th class="th02">
                        UBMPlating<br />Area</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_34")%>
                        <br />
                    </td>
                    <th class="th01">
                        48</th>
                    <th class="th02">
                        BumpTo<br />BumpSpace</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_48")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *07</th>
                    <th class="th02">
                        TiEtching<br />Chemical(T89/DHF)</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_07")%>
                        <br />
                    </td>
                    <th class="th01">
                        21</th>
                    <th class="th02">
                        WaferPSV<br />TypeThickness</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_21")%>
                        <br />
                    </td>
                    <th class="th01">
                        35</th>
                    <th class="th02">
                        BumpHeight</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_35")%>
                        <br />
                    </td>
                    <th class="th01">
                        49</th>
                    <th class="th02">
                        SMO</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_49")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        08</th>
                    <th class="th02">
                        TinShellBake</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_08")%>
                        <br />
                    </td>
                    <th class="th01">
                        22</th>
                    <th class="th02">
                        BumpDensity</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_22")%>
                        <br />
                    </td>
                    <th class="th01">
                        36</th>
                    <th class="th02">
                        UBM/SMO<br />Ratio</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_36")%>
                        <br />
                    </td>
                    <th class="th01">
                        50</th>
                    <th class="th02">
                        UBMDensity</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_50")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        09</th>
                    <th class="th02">
                        PIrougness</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_09")%>
                        <br />
                    </td>
                    <th class="th01">
                        23</th>
                    <th class="th02">
                        RVHoleOnAlPad</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_23")%>
                        <br />
                    </td>
                    <th class="th01">
                        37</th>
                    <th class="th02">
                        BH/UBMRatio</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_37")%>
                        <br />
                    </td>
                    <th class="th01">
                        51</th>
                    <th class="th02">
                        DieSizeX</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_51")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        10</th>
                    <th class="th02">
                        BumpResistance<br />Capability</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_10")%>
                        <br />
                    </td>
                    <th class="th01">
                        24</th>
                    <th class="th02">
                        ProbingOnPad</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_24")%>
                        <br />
                    </td>
                    <th class="th01">
                        38</th>
                    <th class="th02">
                        Process/Machine<br />Ratio</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_38")%>
                        <br />
                    </td>
                    <th class="th01">
                        52</th>
                    <th class="th02">
                        DieSizeY</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_52")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        11</th>
                    <th class="th02">
                        100%RVSI</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_11")%>
                        <br />
                    </td>
                    <th class="th01">
                        25</th>
                    <th class="th02">
                        SelRing<br />ProtectedBySiN</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_25")%>
                        <br />
                    </td>
                    <th class="th01">
                        39</th>
                    <th class="th02">
                        Material</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_39")%>
                        <br />
                    </td>
                    <th class="th01">
                        53</th>
                    <th class="th02">
                        BumpCount</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_53")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        12</th>
                    <th class="th02">
                        LowK</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_12")%>
                        <br />
                    </td>
                    <th class="th01">
                        26</th>
                    <th class="th02">
                        PassivationType</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_26")%>
                        <br />
                    </td>
                    <th class="th01">
                        40</th>
                    <th class="th02">
                        Measurement<br />Tool</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_40")%>
                        <br />
                    </td>
                    <th class="th01">
                        54</th>
                    <th class="th02">
                        CornerUBM<br />Density</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_54")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        13</th>
                    <th class="th02">
                        PRThickness</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_13")%>
                        <br />
                    </td>
                    <th class="th01">
                        27</th>
                    <th class="th02">
                        UBMOverlapPSV</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_27")%>
                        <br />
                    </td>
                    <th class="th01">
                        41</th>
                    <th class="th02">
                        Raliability</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_41")%>
                        <br />
                    </td>
                    <th class="th01">
                        55</th>
                    <th class="th02">
                        Passivation<br />Thickness</th>
                    <td bgcolor="#fff">
                        <%# Eval("POR_55")%>
                        <br />
                    </td>
                </tr>
                <tr>
                    <th class="th01">
                        *14</th>
                    <th class="th02">
                        Customer</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_14")%>
                        <br />
                    </td>
                    <th class="th01">
                        28</th>
                    <th class="th02">
                        UBMInsdie<br />FinalMetalForFOC</th>
                    <td bgcolor="#eee">
                        <%# Eval("POR_24")%>
                        <br />
                    </td>
                    <th class="th01">
                        42</th>
                    <th class="th02">
                        Remark</th>
                    <td bgcolor="#eee" colspan="3">
                        <asp:TextBox ID="text_POR42" runat="server" Enabled="false" Height="50" 
                            Text='<%# Bind("POR_42")%>' TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br/>
             <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                class="blueButton button2" CommandName="Cancel" Text="CloseDetail"></asp:LinkButton>
        </ItemTemplate>
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <EditItemTemplate>
            <table class="style1" id="table2">
                	<tr>
                	<th class="th01">01</th>
                    <th class="th02">ProductionSite</th>
                    <td bgcolor="#FFF">
                        <asp:DropDownList ID="ddPOR01" runat="server" SelectedValue='<%# Eval("POR_01")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>CH</asp:ListItem>
                            <asp:ListItem>CS</asp:ListItem>
                            <asp:ListItem>DF</asp:ListItem>
                            <asp:ListItem>ZK</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		    		<th class="th01">15</th>
                    <th class="th02">PROD</th>
                    <td bgcolor="#FFF">
                        <asp:Label ID="lab_Pord" runat="server"></asp:Label>
                        -
                        <asp:DropDownList ID="ddPord" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>9D</asp:ListItem>
                            <asp:ListItem>TG</asp:ListItem>
                            <asp:ListItem>ZK</asp:ListItem>
                            <asp:ListItem>JA</asp:ListItem>
                        </asp:DropDownList>
                        -
                        <asp:TextBox ID="text_No" runat="server" Width="42px"></asp:TextBox>
                    </td>

		    		<th class="th01">29</th>
                    <th class="th02">UBMSize</th>
                    <td bgcolor="#FFF">
                        <asp:TextBox ID="text_POR29" runat="server" Text='<%# Eval("POR_29")%>' />
                    </td>

		    		<th class="th01">43</th>
                    <th class="th02">Mushroon<br/>Space</th>
                    <td bgcolor="#FFF">                    
                        <asp:TextBox ID="text_POR43" Text='<%# Eval("POR_43")%>' runat="server" />
                    </td>
                </tr>
                <tr>                    
                    <th class="th01">02</th>
                    <th class="th02">PKG</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR02" runat="server" AutoPostBack="True" SelectedValue='<%# Eval("POR_02")%>'
                            onselectedindexchanged="ddPOR02_SelectedIndexChanged">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>EP FOC-12-EU</asp:ListItem>
                            <asp:ListItem>EP FOC-12-LF</asp:ListItem>
                            <asp:ListItem>EP REPSV-12-EU</asp:ListItem>
                            <asp:ListItem>EP REPSV-12-LF</asp:ListItem>
                            <asp:ListItem>EP REPSV-8-LF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		    		<th class="th01">16</th>
                    <th class="th02">UBMType<br/>/Thickness</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR16" runat="server" SelectedValue='<%# Eval("POR_16")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu3k/Cu5um/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu3K/Ni3.5um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Cu5um/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Ni2um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Ni3.5um</asp:ListItem>
                            <asp:ListItem>Ti1K/Cu5K/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti3K/Cu3K/Cu3um/Ni3um</asp:ListItem>
                            <asp:ListItem>Ti3K/Cu3K/Cu5um/Ni2um</asp:ListItem>
                            <asp:ListItem>Ti3K/Cu3K/Ni2um</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		   			<th class="th01">30</th>
                    <th class="th02">REPSVPI<br/>OpeningSize</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR30" runat="server" Text='<%# Eval("POR_30")%>'/>
                    </td>
 	                <th class="th01">44</th>
                    <th class="th02">MushroonCD</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR44" Text='<%# Eval("POR_44")%>' runat="server" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">03</th>
                    <th class="th02">WaferTech.(nm)</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR03" runat="server" Text='<%# Eval("POR_03")%>' /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR03" runat="server" 
                            ControlToValidate="text_POR03" Display="Static" ErrorMessage="請輸入WaferTech.!!" 
                            ForeColor="red" />
                    </td>                    
                    <th class="th01">17</th>
                    <th class="th02">Device</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR17" Text='<%# Eval("POR_17")%>' runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR17" runat="server" 
                            ControlToValidate="text_POR17" Display="Static" ErrorMessage="請輸入Device" 
                            ForeColor="red" />
                    </td>                    
                    <th class="th01">31</th>
                    <th class="th02">PIviaOpening<br/>BottomEdgeTo<br/>Padpsv.Edge</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR31" Text='<%# Eval("POR_31")%>' runat="server" />
                    </td>                    
                    <th class="th01">45</th>
                    <th class="th02">Bump<br/>Diameter</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR45" runat="server" Text='<%# Eval("POR_45")%>' AutoPostBack="true" 
                            ontextchanged="text_POR45_TextChanged" />
                    </td>                   
                </tr>
                <tr>                    
                    <th class="th01">04</th>
                    <th class="th02">FAB</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR04" runat="server" SelectedValue='<%# Eval("POR_04")%>'>
                        <asp:ListItem>請選擇</asp:ListItem>
                        <asp:ListItem>GF</asp:ListItem>
                        <asp:ListItem>SMIC</asp:ListItem>
                        <asp:ListItem>TSMC</asp:ListItem>
                        <asp:ListItem>UMC</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="text_POR04" runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR04" runat="server" 
                            ControlToValidate="text_POR04" Display="Static" ErrorMessage="請輸入FAB" 
                            ForeColor="red" />--%>
                    </td>                    
                    <th class="th01">18</th>
                    <th class="th02">DieSize</th>
                    <td bgcolor="#eee"> 
                        <asp:Label ID="labPOR18" runat="server" Text='<%# Eval("POR_18")%>'></asp:Label>
                        <br/>
                        [請輸入DieSizeX與Y]
                    </td>
                    <th class="th01">32</th>
                    <th class="th02">PIEdgeInside<br/>SealRing</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR32" Text='<%# Eval("POR_32")%>' runat="server" />
                    </td> 		    		
 		    		<th class="th01">46</th>
                    <th class="th02">C/PProbe<br/>CardType</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR46" runat="server" SelectedValue='<%# Eval("POR_46")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Membrane</asp:ListItem>
                            <asp:ListItem>Vertical probe</asp:ListItem>
                        </asp:DropDownList>
                    </td>                  
                </tr>
                <tr>                    
                    <th class="th01">05</th>
                    <th class="th02">WaferPSVType<br/>/Thickness</th>
                    <td bgcolor="#fff"> 
                     <asp:DropDownList ID="ddPOR05" runat="server" SelectedValue='<%# Eval("POR_05")%>'>
                        <asp:ListItem>請選擇</asp:ListItem>
                        <asp:ListItem>SiN</asp:ListItem>
                        <asp:ListItem>TSMC PI</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="text_POR05" runat="server" /><br/>
                        <asp:RequiredFieldValidator ID="Valid_POR05" runat="server" 
                            ControlToValidate="text_POR05" Display="Static" 
                            ErrorMessage="請輸入WaferPSVType/Thickness" ForeColor="red" />--%>
                    </td>                    
                    <th class="th01">19</th>
                    <th class="th02">MinBumpPitch</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR19" Text='<%# Eval("POR_19")%>' runat="server" />
                    </td>                    
                    <th class="th01">33</th>
                    <th class="th02">BumpType</th>
                    <td bgcolor="#fff"> 
                        <asp:DropDownList ID="ddPOR33" runat="server" SelectedValue='<%# Eval("POR_33")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Plated Sn/Ag1.8</asp:ListItem>                            
                            <asp:ListItem>Plated Sn/Ag2.3</asp:ListItem>
                            <asp:ListItem>Plated 63Sn/37Pb</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">47</th>
                    <th class="th02">MinFinalMetal<br/>PadToSealRing</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR47" Text='<%# Eval("POR_47")%>' runat="server" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">06</th>
                    <th class="th02">PRType</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR06" Text='<%# Eval("POR_06")%>' runat="server" /><br/>
                       <%-- <asp:RequiredFieldValidator ID="Valid_POR06" runat="server" 
                            ControlToValidate="text_POR06" Display="Static" ErrorMessage="請輸入PRType" 
                            ForeColor="red" />--%>
                    </td>                    
                    <th class="th01">20</th>
                    <th class="th02">FinalMetal<br/>PadType</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR20" runat="server" SelectedValue='<%# Eval("POR_20")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Al</asp:ListItem>
                            <asp:ListItem>Cu</asp:ListItem>
                        </asp:DropDownList>
                    </td>                     
                    <th class="th01">34</th>
                    <th class="th02">UBMPlating<br/>Area</th>
                    <td bgcolor="#eee"> 
                        <asp:TextBox ID="text_POR34" Text='<%# Eval("POR_34")%>' runat="server" />
                    </td>		    
		    		<th class="th01">48</th>
                    <th class="th02">BumpTo<br/>BumpSpace</th>
                    <td bgcolor="#eee"> 
                        <asp:Label ID="labPOR48" runat="server" Text='<%# Eval("POR_48")%>'></asp:Label>
                        <br/>
                        [輸入MinBumpPitch與BumpDeameter]</td>                </tr>
                <tr>                    
                    <th class="th01">07</th>
                    <th class="th02">TiEtching<br/>Chemical(T89/DHF)</th>
                    <td bgcolor="#fff"> 
                        <asp:DropDownList ID="ddPOR07" runat="server" SelectedValue='<%# Eval("POR_07")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>T89</asp:ListItem>
                            <asp:ListItem>DHF</asp:ListItem>
                            <asp:ListItem>T89+DHF</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">21</th>
                    <th class="th02">WaferPSV<br/>TypeThickness</th>
                    <td bgcolor="#fff"> 
                        <asp:DropDownList ID="ddPOR21" runat="server" SelectedValue='<%# Eval("POR_21")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>SiN</asp:ListItem>
                            <asp:ListItem>SiN/0.9um</asp:ListItem>
                            <asp:ListItem>SiN/1.1um</asp:ListItem>
                            <asp:ListItem>SiN/1.9um</asp:ListItem>
                            <asp:ListItem>SiN/2um</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">35</th>
                    <th class="th02">BumpHeight</th>
                    <td bgcolor="#fff"> 
                        <asp:TextBox ID="text_POR35" Text='<%# Eval("POR_35")%>' runat="server" />
                    </td> 		     
 		     		<th class="th01">49</th>
                    <th class="th02">SMO</th>
                    <td bgcolor="#fff"> 
                        <asp:Label ID="labPOR49" runat="server" Text='<%# Eval("POR_49")%>'></asp:Label>
                        <br/>
                        [請輸入UBMSize與UBM/SMORatio]</td>                    
                </tr>
                <tr>                    
                    <th class="th01">08</th>
                    <th class="th02">TinShellBake</th>
                    <td bgcolor="#eee"> 
                        <asp:DropDownList ID="ddPOR08" runat="server" SelectedValue='<%# Eval("POR_08")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">22</th>
                    <th class="th02">BumpDensity</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR22" Text='<%# Eval("POR_22")%>' runat="server" />
                    </td>                     
                    <th class="th01">36</th>
                    <th class="th02">UBM/SMO<br/>Ratio</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR36" runat="server" Text='<%# Eval("POR_36")%>' AutoPostBack="true" 
                            ontextchanged="text_POR36_TextChanged" />
                    </td>                    
                    <th class="th01">50</th>
                    <th class="th02">UBMDensity</th>
                    <td bgcolor="#eee">
                        <asp:Label ID="labPOR50" Text='<%# Eval("POR_50")%>' runat="server"></asp:Label>
                        %<br/>[請輸入UBMSize,DieSizeX/Y與BumpCount]</td>                    
                </tr>
                <tr>                    
                    <th class="th01">09</th>
                    <th class="th02">PIrougness</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR09" runat="server" SelectedValue='<%# Eval("POR_09")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                            <asp:ListItem>BGM2:7~15nm</asp:ListItem>
                            <asp:ListItem>BGM3A:15~30nm</asp:ListItem>
                            <asp:ListItem>SAD:1~8%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="th01">23</th>
                    <th class="th02">RVHoleOnAlPad</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR23" runat="server" SelectedValue='<%# Eval("POR_23")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">37</th>
                    <th class="th02">BH/UBMRatio</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR37" Text='<%# Eval("POR_37")%>' runat="server" />
                    </td>                    
                    <th class="th01">51</th>
                    <th class="th02">DieSizeX</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR51" Text='<%# Eval("POR_51")%>' runat="server" />
                    </td>                  
                </tr>
                <tr>
                    <th class="th01">10</th>
                    <th class="th02">BumpResistance<br/>Capability</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR10" runat="server" SelectedValue='<%# Eval("POR_10")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem><20 mohm</asp:ListItem>
                            <asp:ListItem><30 mohm</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="th01">24</th>
                    <th class="th02">ProbingOnPad</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR24" runat="server" SelectedValue='<%# Eval("POR_24")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">38</th>
                    <th class="th02">Process/Machine<br/>Ratio</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR38" runat="server" Text='<%# Eval("POR_38")%>' />
                    </td>
                    <th class="th01">52</th>
                    <th class="th02">DieSizeY</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR52" runat="server" Text='<%# Eval("POR_52")%>' AutoPostBack="true" 
                            ontextchanged="text_POR52_TextChanged" />
                    </td>
                </tr>
                <tr>
                    <th class="th01">11</th>
                    <th class="th02">100%RVSI</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR11" runat="server" SelectedValue='<%# Eval("POR_11")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		    		<th class="th01">25</th>
                    <th class="th02">SelRing<br/>ProtectedBySiN</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR25" runat="server" SelectedValue='<%# Eval("POR_25")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="th01">39</th>
                    <th class="th02">Material</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR39" runat="server" Text='<%# Eval("POR_39")%>' />
                    </td>                    
                    <th class="th01">53</th>
                    <th class="th02">BumpCount</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR53" runat="server" Text='<%# Eval("POR_53")%>' AutoPostBack="true" 
                            ontextchanged="text_POR53_TextChanged" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">12</th>
                    <th class="th02">LowK</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR12" runat="server" SelectedValue='<%# Eval("POR_12")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>E Low K</asp:ListItem>
                            <asp:ListItem>ELK</asp:ListItem>
                            <asp:ListItem>Low K</asp:ListItem>
                            <asp:ListItem>Non-Low K</asp:ListItem>
                        </asp:DropDownList>
                    </td>		    
		    		<th class="th01">26</th>
                    <th class="th02">PassivationType</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR26" runat="server" SelectedValue='<%# Eval("POR_26")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                            <asp:ListItem>HD4104</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">40</th>
                    <th class="th02">Measurement<br/>Tool</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR40" runat="server" Text='<%# Eval("POR_40")%>' />
                    </td>                    
                    <th class="th01">54</th>
                    <th class="th02">CornerUBM<br/>Density</th>
                    <td bgcolor="#eee">
                        <asp:TextBox ID="text_POR54" Text='<%# Eval("POR_54")%>' runat="server" />
                    </td>                   
                </tr>
                <tr>                    
                    <th class="th01">13</th>
                    <th class="th02">PRThickness</th>
                    <td bgcolor="#fff">
                        <asp:DropDownList ID="ddPOR13" runat="server" SelectedValue='<%# Eval("POR_13")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">27</th>
                    <th class="th02">UBMOverlapPSV</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR27" Text='<%# Eval("POR_27")%>' runat="server" />
                    </td>                                        
                    <th class="th01">41</th>
                    <th class="th02">Raliability</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR41" runat="server" Text='<%# Eval("POR_41")%>' />
                    </td>                    
                    <th class="th01">55</th>
                    <th class="th02">Passivation<br/>Thickness</th>
                    <td bgcolor="#fff">
                        <asp:TextBox ID="text_POR55" Text='<%# Eval("POR_55")%>' runat="server" />
                    </td>                    
                </tr>
                <tr>                    
                    <th class="th01">14</th>
                    <th class="th02">Customer</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR14" runat="server" SelectedValue='<%# Eval("POR_14")%>'>
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>AMD</asp:ListItem>
                            <asp:ListItem>Avago</asp:ListItem>
                            <asp:ListItem>Avago-LS</asp:ListItem>
                            <asp:ListItem>ESILICON</asp:ListItem>
                            <asp:ListItem>Infineon</asp:ListItem>
                            <asp:ListItem>LSI</asp:ListItem>
                            <asp:ListItem>Magnum</asp:ListItem>
                            <asp:ListItem>Marvell</asp:ListItem>
                            <asp:ListItem>MEDIATEK</asp:ListItem>
                            <asp:ListItem>NV</asp:ListItem>
                            <asp:ListItem>Parade</asp:ListItem>
                            <asp:ListItem>PARROT</asp:ListItem>
                            <asp:ListItem>Sigma Design</asp:ListItem>
                            <asp:ListItem>SMIC</asp:ListItem>
                            <asp:ListItem>SPIL</asp:ListItem>
                            <asp:ListItem>TSMC-QCT</asp:ListItem>
                            <asp:ListItem>UMC</asp:ListItem>
                            <asp:ListItem>VeriSilicon-CN</asp:ListItem>
                            <asp:ListItem>XILINX</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <th class="th01">28</th>
                    <th class="th02">UBMInsdie<br/>FinalMetalForFOC</th>
                    <td bgcolor="#eee">
                        <asp:DropDownList ID="ddPOR28" SelectedValue='<%# Eval("POR_28")%>' runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>3(TSMC PI)</asp:ListItem>
                            <asp:ListItem>NA</asp:ListItem>
                        </asp:DropDownList>
                    </td>                     
                    <th class="th01">42</th>
                    <th class="th02">Remark</th>                    
                    <td bgcolor="#eee" colspan="3">                        
                        <asp:TextBox ID="text_POR42" runat="server" Width="300px" Text='<%# Eval("POR_01")%>' TextMode="MultiLine" Height="50"></asp:TextBox>
                    </td>  
                </tr>            
            </table>  
             <br />                        
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="新增"><img src="../Images/img/Check_Ok.png" height="40px" width="40px" /></asp:LinkButton>
            <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="取消"><img src="../Images/img/Cancel.png" height="40px" width="40px" /></asp:LinkButton>
        </EditItemTemplate>
        <HeaderStyle BackColor="#0066CC" Font-Bold="True" ForeColor="White" />
    </asp:FormView>
    </fieldset>
    </asp:Panel>               
        
       </div>
    </form>
    
</body>
</html>
