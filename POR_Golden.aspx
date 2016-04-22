<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POR_Golden.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>POR_Golden!</title>

    <link rel="stylesheet" href="..\css\styles.css" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="http://jqueryui.com/resources/demos/external/jquery.bgiframe-2.1.2.js"></script>
    <script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>

    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>    
    <script src="http://code.jquery.com/ui/1.9.0/jquery-ui.js"></script>    

    <script type="text/javascript">
    function Confirm(strMsg, strData) {
        var isOK = confirm(strMsg);
        if (isOK) {
            PageMethods.CreateJob(strData, OnSuccess, OnFail);
                
        }
    }
    function OnSuccess(receiveData, userContext, methodName) {
        //成功時，目地控制項顯示所接收結果
        if (receiveData == "") {
            window.parent.$('#dialog').dialog('close');
            window.parent.$('#cmdFilter').click();
        } else {
            alert(methodName + ": " + receiveData);
        }
    }

    function OnFail(error, userContext, methodName) {
        if (error != null) {
            alert(methodName + ": " + error.get_message());
        }

    }

    function error_msg(error_sentence) {
        alert(error_sentence);
    }


</script>


<script type="text/javascript">

    
      $(function () {
          $("[id$=TB_PKG]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("POR_Golden.aspx/GetPKG") %>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split(',')[0],
                                 val: item.split(',')[1]
                             }
                         }))
                     },
                     error: function (response) {
                         alert(response.responseText);
                     },
                     failure: function (response) {
                         alert(response.responseText);
                     }
                 });
             },         
             minLength: 1
         });
     });


</script>


 <style type="text/css">
     .style2
        {
            color: #FFFFFF;            
        }
      .div-por
        {
           
                width: 65%;
                float: inherit;
                position: absolute;
                /*right: 150px;*/
               
        }
      .div-por
        {
           
                width: 65%;
                float: inherit;
                position:static;
               
               
        }
 </style>
</head>
<body>
    
 <form id="form1"  runat="server">         
      
              
           
        <div class="div-por">
       <fieldset style="margin:auto;" class="fieldset">
    <legend class="legend" style="font-weight: 700; font-size: large;">POR Golden Condition</legend>
         <table style="width: 100%;">
              <tr>
                  <th bgcolor="#006699" Width="150px" class="style2">Production Site</th>
                  <td bgcolor="White" Width="50px"> 
                  <asp:DropDownList ID="ProSite_ddl" runat="server" Height="25px">
                            <asp:ListItem Value="0">All Site</asp:ListItem>
                        </asp:DropDownList>                                     
                  </td>
                  <th bgcolor="#006699" Width="150px" class="style2">PKG</th>
                  <td bgcolor="#fff" Width="150px"><asp:TextBox ID="TB_PKG" runat="server" 
                          Width="150px" Height="20px"></asp:TextBox></td>
                  <th bgcolor="#006699" Width="150px" class="style2">Wafer Tech.(nm)</th>
                  <td bgcolor="#FFFFFF" Width="100px"><asp:TextBox ID="TB_WT" runat="server" 
                          Width="100px" Height="20px"></asp:TextBox></td>
                  <th bgcolor="#006699" class="style2" Width="100px">FAB</th>
                  <td bgcolor="#fff" Width="100px"><%-- <asp:DropDownList ID="ddl_Fab" runat="server">
                            <asp:ListItem Value="0">All FAB</asp:ListItem>
                            <asp:ListItem Value="GF">GF</asp:ListItem>
                            <asp:ListItem Value="SMIC">SMIC</asp:ListItem>
                            <asp:ListItem Value="TSMC">TSMC</asp:ListItem>
                            <asp:ListItem Value="UMC">UMC</asp:ListItem>
                        </asp:DropDownList>--%>
                         <asp:TextBox ID="TB_FAB" runat="server" Width="100px" Height="20px"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <th bgcolor="#006699" class="style2">Wafer PSV Type/Thickness</th>     
                  <td bgcolor="#fff"><asp:TextBox ID="TB_PSV" runat="server"></asp:TextBox> </td>            
                  <%--<td bgcolor="#fff"><asp:DropDownList ID="ddl_PSV" runat="server" Height="25px">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="SiN">SiN</asp:ListItem>
                            <asp:ListItem Value="TSMC PI">TSMC PI</asp:ListItem>
                        </asp:DropDownList></td>--%>
                  <th bgcolor="#006699" class="style2">RSVI(Y/N)</th>
                  <td bgcolor="#FFFFFF"> <asp:DropDownList ID="ddl_RVSI" runat="server" Height="25px">
                            <asp:ListItem Value="0">All RSVI</asp:ListItem>
                            <asp:ListItem Value="Y">Y</asp:ListItem>
                            <asp:ListItem Value="N">N</asp:ListItem>
                        </asp:DropDownList></td>
                  <th bgcolor="#006699" class="style2">Customer</th>
                  <td bgcolor="#fff" Width="100px"><asp:TextBox ID="TB_Customer" runat="server" 
                          Width="100px" Height="20px"></asp:TextBox></td>
                  <td colspan="2" style="text-align: center">
                      <asp:Button ID="btn_Search" runat="server" Text="Search" class="blueButton button2"
                          onclick="Search_por_Click" Height="25px" />                          
                  </td>
              </tr>
          </table>        
        </fieldset>
        </div>
        <br /> 
           
    
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
         <asp:Panel ID="CPSP_Panel" runat="server" Visible="true" Width="1079px">        
      
        
     
      
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#CCCCCC" BorderStyle="None" CellPadding="3" Height="170px"
               DataKeyNames="POR_Customer,POR_Device" 
        onrowcommand="GridView1_RowCommand" 
                   AllowPaging="True" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="7" AllowSorting="True" >
                <HeaderStyle Height="50px" BackColor="#006699" ForeColor="White" ></HeaderStyle>
                <Columns>
                    <asp:ButtonField CommandName="yan" Text="Compare"  />
                    <asp:BoundField DataField="POR_17" HeaderText="*Device" />
                    <asp:BoundField DataField="POR_01" HeaderText="*Production Site" />
                    <asp:BoundField DataField="POR_02" HeaderText="*PKG " />
                    <asp:BoundField DataField="POR_03" HeaderText="*Wafer Tech.(nm)" />
                    <asp:BoundField DataField="POR_04" HeaderText="*FAB" />
                    <asp:BoundField DataField="POR_05" HeaderText="*Wafer PSV type / Thickness" />
                    <asp:BoundField DataField="POR_11" HeaderText="*RVSI(Y/N)" />
                    <asp:BoundField DataField="POR_14" HeaderText="*Customer" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

            </asp:GridView>
          
        </asp:Panel>
    
    </form>
    
</body>
</html>
