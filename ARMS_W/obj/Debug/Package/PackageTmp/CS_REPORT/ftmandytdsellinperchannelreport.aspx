﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ftmandytdsellinperchannelreport.aspx.cs" Inherits="ARMS_W.CS_REPORT.ftmandytdsellinperchannelreport" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="<%=ResolveUrl("~/") %>CR13/js/crviewer/images/style.css" rel="stylesheet" type="text/css" />

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/General.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/BusinessReviewDetails.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>CR13/js/crviewer/crv.js" type="text/javascript"></script>
    <title></title>

</head>
<body>
    <form id="form1" runat="server">


    <div class="bl_box">
    <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>SELL-IN PER CHANNEL</b>
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
        <asp:Label ID="Label2" runat="server" Text="Select Region:"></asp:Label>
        <asp:DropDownList ID="txt_region" runat="server" Width="150px">
        <asp:ListItem Value="C">All</asp:ListItem>
        <asp:ListItem Value="Luzon">Luzon</asp:ListItem>
        <asp:ListItem Value="Vismin">Vismin</asp:ListItem>
    </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="OK" Width="91px" 
            onclick="Button1_Click" style="height: 26px" />
        
    </div>
    <div> <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="626px" 
            ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" 
            ToolPanelWidth="200px" Width="350px" ToolPanelView="None" 
            EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="~\CS_REPORT\rpt\ftmandytdsellinperchannelreport.rpt">
            </Report>
        </CR:CrystalReportSource></div>
       
    </div>
    </div>
    </form>
</body>
</html>
