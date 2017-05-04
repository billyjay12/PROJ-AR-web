<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListOfCreatedCustomer.aspx.cs" Inherits="ARMS_W.CS_REPORT.ListOfCreatedCustomer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=ResolveUrl("~/") %>CR13/js/crviewer/images/style.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>CR13/js/crviewer/crv.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="bl_box">

    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    PCA</td>
                <td align="right" valign="middle" >
                   
                </td>
             </tr>
        </table>
    </div>

    <div class="page_header_y"> &nbsp; </div>

    <div class="simple_box">    
        <div>
            <asp:Label ID="Label1" runat="server" Text="Date:"></asp:Label> &nbsp; <asp:TextBox ID="txt_date" runat="server" Width="150px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="OK" Width="91px" onclick="Button1_Click" />
        </div>
        <div></div>
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" ReportSourceID="CrystalReportSource1" 
                ToolPanelView="None" EnableParameterPrompt="False" 
                EnableDatabaseLogonPrompt="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="~\CS_REPORT\rpt\List of Customers.rpt">
                </Report>
            </CR:CrystalReportSource> 
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
