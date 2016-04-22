<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fun_NPIManForm.aspx.cs" Inherits="PreNPI_Fun_NPIManForm" ClientIDMode="Predictable" EnableEventValidation = "false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<title>Add Manual Form Information</title>
<link rel="stylesheet" href="..\css\styles.css" type="text/css" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
<script src="http://code.jquery.com/jquery-1.8.3.js"></script>
<script src="../JS/jquery.bgiframe-2.1.2.js" type="text/javascript"></script>
<script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js"></script>
<script src="../JS/jquery-ui-1.9.2/jquery-1.10.0.min.js" type="text/javascript"></script>    
<script src="../JS/jquery-ui-1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="../JS/jquery-ui-1.9.2/jquery-ui.css" rel="stylesheet" type="text/css" />  
 <script type="text/javascript">
     $(function () {
         $("[id$=text_Cust]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("Fun_NPIManForm.aspx/GetNewCustomer") %>',
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

     $(function () {
         $("[id$=text_Devi]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("Fun_NPIManForm.aspx/GetNewDevice") %>',
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
  

<script language="javascript" type="text/javascript">
    function ConfirmDelManual(strMsg, strData) {
        var isOK = confirm(strMsg);
        if (isOK) {
            PageMethods.DEL_An_Manual(strData, OnSuccess, OnFail);
        }
    }
    function ConfirmUpdateManual(strMsg, strData) {
        var isOK = confirm(strMsg);
        if (isOK) {
            PageMethods.Update_An_Manual(strData, OnSuccess, OnFail);
        }
    }
    function ConfirmInsertManual(strMsg, strData) {
        var isOK = confirm(strMsg);
        if (isOK) {
            PageMethods.Insert_An_Manual(strData, OnSuccess, OnFail);
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
    table#t01 {
    border: 0px solid #d4d4d4;
    border-collapse: collapse;
}
table#t01, th {
    border: 1px solid #d4d4d4;
    padding: 1px;
    text-align: left;     
}
table#t01, td 
{    
                    
} 
table#t01 tr:nth-child(even) {
    background-color: #eee;
}
table#t01 tr:nth-child(odd) {
   background-color:#fff;
}
table#t01 th	{
    background-color:#778899;
    color: white;
    height: 30px;
}
    .style3
    {
        width: 100px; 
        border: 1px solid white; 
        text-align:left;          
    }    
    .Data-Content {
    width: 100%; /* 表單寬度 */
    line-height: 18px;    
    }   
    .style6
    {
        width: 100px;
    }   
    .style7
    {
        width: 50px;
    }

    </style>

</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
     <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
     <br/>   
    <div class="Data-Content">
    <asp:Button ID="cmdFilter" runat="server" OnClick="cmdFilter_Click" Text="Button" ClientIDMode="Static" Style="display: none;" /> 
    <asp:Panel ID="Panel1" runat="server" Visible="true">   
    <fieldset style="width:80%;margin:auto;" class="fieldset">
    <legend class="legend" style="font-weight: 700; font-size: large;">Manual Information</legend>    
    <div>
        New Cusotmer<asp:TextBox ID="text_Cust" runat="server"></asp:TextBox>
        New Device<asp:TextBox ID="text_Devi" runat="server"></asp:TextBox>
        <asp:Button ID="butSearch" runat="server" Text="Search" 
            class="blueButton button2" onclick="butSearch_Click"/>
    </div>
    <hr/>
    <div>
       <asp:GridView ID="GridView1" runat="server"  AllowPaging="True" 
            AutoGenerateColumns="False" PagerStyle-Wrap ="false"
            CellPadding="3" DataKeyNames="New_Customer,New_Device" EmptyDataText="沒有資料錄可顯示。" 
            onrowcommand="GridView1_RowCommand"              
            onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
            onrowdatabound="GridView1_RowDataBound" BackColor="White"               
              ForeColor="#333333" GridLines="None"
              onselectedindexchanged="GridView1_SelectedIndexChanged" 
              onrowediting="GridView1_RowEditing" onrowdeleting="GridView1_RowDeleting">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <HeaderTemplate>
                        <asp:Button ID="btnInsert" runat="server" CausesValidation="False" class="blueButton button2"
                            CommandName="Insert" Text="NewManual"   />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="False" 
                            CommandName="Select" Height="25px" ImageUrl="~/Images/img/View.png" 
                            Width="25px" />
                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" 
                            CommandName="Edit" Height="25px" ImageUrl="~/Images/img/Print.png" 
                            Width="25px" />
                        <asp:ImageButton ID="btnDelete" runat="server"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False" CommandName="DEL_Manual"
                             Height="25px" ImageUrl="~/Images/img/Del.png" Width="25px" />
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="New_Customer" HeaderText="New Customer" 
                    SortExpression="New_Customer" />
                <asp:BoundField DataField="New_Device" HeaderText="New Device" 
                    SortExpression="New_Device" />
                <asp:BoundField DataField="UpdateTime" HeaderText="UpdateTime" 
                    SortExpression="UpdateTime" />                
            </Columns>
           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
    </asp:GridView> 
    </div>        
     </fieldset>
    </asp:Panel>   
    
     <asp:Panel ID="Panel2" runat="server" Visible="False">        
    <asp:FormView ID="FormView1" runat="server" CellPadding="0" DataKeyNames="New_Customer,New_Device"
            DefaultMode="ReadOnly" HorizontalAlign="Left" Width="100%" EmptyDataText="No Manual Data found."  
            OnItemInserting="FormView1_ItemInserting" 
            onprerender="FormView1_PreRender" 
            onitemcommand="FormView1_ItemCommand" oniteminserted="FormView1_ItemInserted" 
            onmodechanging="FormView1_ModeChanging" 
            onitemupdated="FormView1_ItemUpdated" onitemupdating="FormView1_ItemUpdating">        
        <InsertItemTemplate>  
        <fieldset  style="width:80%;margin:auto;" class="fieldset">
        <legend class="legend" style="font-weight: 700; font-size: large;">Insert Manual Information</legend>             
    <table id="t01" align="center" class="style5" style="width:100%;">
        <tr>
            <th colspan="2">New Customer</th>
            <td class="style3"><asp:TextBox ID="text_Customer" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
          
        <tr>
            <th colspan="2">New Device</th>            
            <td class="style3"><asp:TextBox ID="text_Device" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Wafer PSV Type/Thickness</th>            
            <td class="style3"><asp:TextBox ID="text_Man01" class="textbox" runat="server" Text="SiN"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">PI Type</th>            
            <td class="style3"><asp:TextBox ID="text_Man02" class="textbox" runat="server" Text="HD4104"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">PI Thickness(um)</th>
            
            <td class="style3"><asp:TextBox ID="text_Man03" class="textbox" runat="server" Text="5um"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">UBM Type/Thickness(um)</th>  
            <td class="style3">
            <asp:DropDownList ID="DDL_Man04" runat="server" Width="185px" Height="25px">
                  <asp:ListItem Value="Ti1K/Cu5K/Ni3um">Ti1K/Cu5K/Ni3um</asp:ListItem>
                   <asp:ListItem Value="Ti1K/Cu3K/Ni3.5um">Ti1K/Cu3K/Ni3.5um</asp:ListItem>
            </asp:DropDownList>          
           </td>
        </tr>
        <tr>
            <th colspan="2">PR Thickness(um)</th>            
            <td class="style3"><asp:TextBox ID="text_Man05" class="textbox" runat="server" Text="50"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">UBM Insdie Final Metal for FOC</th>            
            <td class="style3"><asp:TextBox ID="text_Man06" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">UBM Plating Area(dm<sup>2</sup>)</th>            
            <td class="style3"><asp:TextBox ID="text_Man07" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">UBM Density(UBM Area/Die Area)</th>            
            <td class="style3"><asp:TextBox ID="text_Man08" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Mushroom CD(um)</th> 
            <td class="style3"><asp:TextBox ID="text_Man09" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Min Mushroom Space(um)</th>            
            <td class="style3"><asp:TextBox ID="text_Man10" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Bump Density(Bump Q'ty/Die Area)</th>            
            <td class="style3"><asp:TextBox ID="text_Man11" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">BH/UBM Ratio</th>            
            <td class="style3"><asp:TextBox ID="text_Man12" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Bump Coplanarity</th>            
            <td class="style3"><asp:TextBox ID="text_Man13" class="textbox" runat="server" Text="<20 um"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Bump Shear Strenght</th>            
            <td class="style3">
                <asp:DropDownList ID="DDL_Man14" runat="server"  Width="185px" Height="25px" >
                  <asp:ListItem Value="LF: >2.5 g/mil^2">LF:>2.5 g/mil^2</asp:ListItem>
                   <asp:ListItem Value="EU: >2.8 g/mil^2">EU: >2.8 g/mil^2</asp:ListItem>
                    <asp:ListItem Value="EU: >2.0 g/mil^2">EU:>2.0 g/mil^2</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th colspan="2">Bump Void</th>            
            <td class="style3"><asp:TextBox ID="text_Man15" class="textbox" runat="server" Text="<10 %"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">PI Rougness(Ra)</th>            
            <td class="style3"><asp:TextBox ID="text_Man16" class="textbox" runat="server" Text="BGM3A:15~30nm"></asp:TextBox></td>
        </tr>
        <tr>
            <th rowspan="2" class="style7">August</th>
            <th class="style6">Gross Die</th>
            <td class="style3"><asp:TextBox ID="text_Man17" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th class="style6">Expose Pad</th>
            <td class="style3"><asp:TextBox ID="text_Man18" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th class="style7" >RVSI</th>
            <th class="style6" >Gross Die</th>
            <td class="style3"><asp:TextBox ID="text_Man19" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">Bump to Bump Space(um)</th>            
            <td class="style3"><asp:TextBox ID="text_Man20" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">SMO(um)</th>            
            <td class="style3"><asp:TextBox ID="text_Man21" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th colspan="2">UBM/SMO Ratio</th>            
            <td class="style3"><asp:TextBox ID="text_Man22" class="textbox" runat="server"></asp:TextBox></td>
        </tr>
    </table>                
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="新增"><img src="../Images/img/Check_Ok.png" height="40px" width="40px" /></asp:LinkButton>
            <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="取消"><img src="../Images/img/Cancel.png" height="40px" width="40px" /></asp:LinkButton>
                </fieldset>    
        </InsertItemTemplate>
        <ItemTemplate>
        <fieldset  style="width:60%;margin:auto;" class="fieldset">
        <legend class="legend" style="font-weight: 700; font-size: large;">View Manual Information</legend>     
            <table id="t01" align="center" class="style5" style="width:100%;">
                <tr>
                    <th colspan="2">
                        New Customer</th>
                    <td class="style3"><%# Eval("New_Customer")%>                        
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        New Device</th>
                    <td class="style3"><%# Eval("New_Device")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Wafer PSV Type/Thickness</th>
                    <td class="style3"><%# Eval("Man_01")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PI Type</th>
                    <td class="style3"><%# Eval("Man_02")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PI Thickness(um)</th>
                    <td class="style3"><%# Eval("Man_03")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Type/Thickness(um)</th>
                    <td class="style3"><%# Eval("Man_04")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PR Thickness(um)</th>
                    <td class="style3"><%# Eval("Man_05")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Insdie Final Metal for FOC</th>
                    <td class="style3"><%# Eval("Man_06")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Plating Area(dm<sup>2</sup>)</th>
                    <td class="style3"><%# Eval("Man_07")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Density(UBM Area/Die Area)</th>
                    <td class="style3"><%# Eval("Man_08")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Mushroom CD(um)</th>
                    <td class="style3"><%# Eval("Man_09")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Min Mushroom Space(um)</th>
                    <td class="style3"><%# Eval("Man_10")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Density(Bump Qty/Die Area)</th>
                    <td class="style3"><%# Eval("Man_11")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        BH/UBM Ratio</th>
                    <td class="style3"><%# Eval("Man_12")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Coplanarity</th>
                    <td class="style3"><%# Eval("Man_13")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Shear Strenght</th>
                    <td class="style3"><%# Eval("Man_14")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Void</th>
                    <td class="style3"><%# Eval("Man_15")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PI Rougness(Ra)</th>
                    <td class="style3"><%# Eval("Man_16")%>
                    </td>
                </tr>
                <tr>
                    <th class="style7" rowspan="2">
                        August</th>
                    <th class="style6">
                        Gross Die</th>
                    <td class="style3"><%# Eval("Man_17")%>
                    </td>
                </tr>
                <tr>
                    <th class="style6">
                        Expose Pad</th>
                    <td class="style3"><%# Eval("Man_18")%>
                    </td>
                </tr>
                <tr>
                    <th class="style7">
                        RVSI</th>
                    <th class="style6">
                        Gross Die</th>
                    <td class="style3"><%# Eval("Man_19")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump to Bump Space(um)</th>
                    <td class="style3"><%# Eval("Man_20")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        SMO(um)</th>
                    <td class="style3"><%# Eval("Man_21")%>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM/SMO Ratio</th>
                    <td class="style3"><%# Eval("Man_22")%>
                    </td>
                </tr>
            </table>
            <br/>
             <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                class="blueButton button2" CommandName="Cancel" Text="CloseDetail"></asp:LinkButton>
                </fieldset>    
        </ItemTemplate>        
        <EditItemTemplate>
        <fieldset  style="width:60%;margin:auto;" class="fieldset">
    <legend class="legend" style="font-weight: 700; font-size: large;">Update Manual Information</legend>     
            <table id="t01" align="center" class="style5" style="width:100%;">
                <tr>
                    <th colspan="2">
                        New Customer</th>
                    <td class="style3"><%# Eval("New_Customer")%>
                        <asp:TextBox ID="text_Customer" runat="server" class="textbox" 
                            Text='<%# Eval("New_Customer")%>' Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        New Device</th>
                    <td class="style3"><%# Eval("New_Device")%>
                        <asp:TextBox ID="text_Device" runat="server" class="textbox" 
                            Text='<%# Eval("New_Device")%>' Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Wafer PSV Type/Thickness</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man01" runat="server" class="textbox" 
                            Text='<%# Eval("Man_01")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PI Type</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man02" runat="server" class="textbox" 
                            Text='<%# Eval("Man_02")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PI Thickness(um)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man03" runat="server" class="textbox" 
                            Text='<%# Eval("Man_03")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Type/Thickness(um)</th>
                    <td class="style3">
                         <asp:DropDownList ID="DDL_Man04" runat="server" Width="185px" Height="25px" AppendDataBoundItem="true" OnDataBound="DDL_Man04_DataBound1">                            
                         </asp:DropDownList>
                                              
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PR Thickness(um)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man05" runat="server" class="textbox" 
                            Text='<%# Eval("Man_05")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Insdie Final Metal for FOC</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man06" runat="server" class="textbox" 
                            Text='<%# Eval("Man_06")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Plating Area(dm<sup>2</sup>)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man07" runat="server" class="textbox" 
                            Text='<%# Eval("Man_07")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM Density(UBM Area/Die Area)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man08" runat="server" class="textbox" 
                            Text='<%# Eval("Man_08")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Mushroom CD(um)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man09" runat="server" class="textbox" 
                            Text='<%# Eval("Man_09")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Min Mushroom Space(um)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man10" runat="server" class="textbox" 
                            Text='<%# Eval("Man_10")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Density(Bump Qty/Die Area)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man11" runat="server" class="textbox" 
                            Text='<%# Eval("Man_11")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        BH/UBM Ratio</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man12" runat="server" class="textbox" 
                            Text='<%# Eval("Man_12")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Coplanarity</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man13" runat="server" class="textbox" 
                            Text='<%# Eval("Man_13")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Shear Strenght</th>
                    <td class="style3">
                       <asp:DropDownList ID="DDL_Man14" runat="server" Width="185px" Height="25px" AppendDataBoundItem="true" OnDataBound="DDL_Man14_DataBound">                            
                         </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump Void</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man15" runat="server" class="textbox" 
                            Text='<%# Eval("Man_15")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        PI Rougness(Ra)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man16" runat="server" class="textbox" 
                            Text='<%# Eval("Man_16")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="style7" rowspan="2">
                        August</th>
                    <th class="style6">
                        Gross Die</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man17" runat="server" class="textbox" 
                            Text='<%# Eval("Man_17")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="style6">
                        Expose Pad</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man18" runat="server" class="textbox" 
                            Text='<%# Eval("Man_18")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="style7">
                        RVSI</th>
                    <th class="style6">
                        Gross Die</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man19" runat="server" class="textbox" 
                            Text='<%# Eval("Man_19")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        Bump to Bump Space(um)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man20" runat="server" class="textbox" 
                            Text='<%# Eval("Man_20")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        SMO(um)</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man21" runat="server" class="textbox" 
                            Text='<%# Eval("Man_21")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        UBM/SMO Ratio</th>
                    <td class="style3">
                        <asp:TextBox ID="text_Man22" runat="server" class="textbox" 
                            Text='<%# Eval("Man_22")%>'></asp:TextBox>
                    </td>
                </tr>                                
            </table>
            <br />                        
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="新增"><img src="../Images/img/Check_Ok.png" height="40px" width="40px" /></asp:LinkButton>
            <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="取消"><img src="../Images/img/Cancel.png" height="40px" width="40px" /></asp:LinkButton>
                </fieldset>    
        </EditItemTemplate>
        <FooterTemplate>           
        </FooterTemplate>
        <HeaderStyle BackColor="#0066CC" Font-Bold="True" ForeColor="White" />
    </asp:FormView>    
    </asp:Panel>   
    </div>   
 <%--   <div id="dialog">
       <iframe height="100%" width="100%" id="dialogFrame" style="border-width: 0px; border-style: none; padding: 0px; margin: 0px;"></iframe>
    </div>--%>
    </form>
</body>
</html>
