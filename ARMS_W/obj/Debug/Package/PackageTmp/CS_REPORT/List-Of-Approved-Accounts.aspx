﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List-Of-Approved-Accounts.aspx.cs" Inherits="ARMS_W.CS_REPORT.List_Of_Approved_Accounts" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="<%=ResolveUrl("~/") %>CR13/js/crviewer/images/style.css" rel="stylesheet" type="text/css" />

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/General.css" rel="stylesheet" type="text/css" />
     <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
   
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>CR13/js/crviewer/crv.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        $(function () {
	        $("#date_to").datepicker();
            $("#date_from").datepicker();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
   <div class="bl_box">

    <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>LIST OF APPROVED ACCOUNTS</b>
                </td>
                <td align="right" valign="middle" >
                   
                </td>
             </tr>
          </table>
      </div>

      
   <div class="page_header_y">
       
      </div>
      <div class="simple_box">
    <div>
     <asp:Label ID="Label1" runat="server" Text="Select Region:"></asp:Label>
    <asp:DropDownList ID="TextBox1" runat="server" Width="150px">
        <asp:ListItem Value="C">All</asp:ListItem>
        <asp:ListItem Value="Luzon">Luzon</asp:ListItem>
        <asp:ListItem Value="Vismin">Vismin</asp:ListItem>
    </asp:DropDownList>
        &nbsp;&nbsp;&nbsp; 
        <asp:Label ID="Label3" runat="server" Text="From:"></asp:Label>
        <asp:TextBox ID="date_from" runat="server" Width="150px"></asp:TextBox>
       &nbsp;&nbsp;&nbsp; 
        <asp:Label ID="Label4" runat="server" Text="To:"></asp:Label>
        <asp:TextBox ID="date_to" runat="server" Width="150px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp; 
        <asp:Button ID="Button1" runat="server" Text="OK" Width="91px" 
            onclick="Button1_Click" />
          
    </div>
    <div>
    </div>
    <div>
       <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableParameterPrompt="False" 
            ReportSourceID="CrystalReportSource1" ToolPanelView="None" 
            EnableDatabaseLogonPrompt="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="~\CS_REPORT\rpt\List-Of-Approved-Accounts.rpt">
            </Report>
        </CR:CrystalReportSource>
        <br />
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    </div>
    </div>
    </div>
    </form>
</body>
</html>
