<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="busrev.aspx.cs" Inherits="ARMS_W.CS_REPORT.busrev" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
    <link href="<%=ResolveUrl("~/") %>CR13/js/crviewer/images/style.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/General.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>CR13/js/crviewer/crv.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div style="padding:5px 8px 5px 8px; background:#2982b6; color:White;">
        <b>BUSINESS REVIEW UPDATE</b>
    </div>

    <div class="simple_box">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableParameterPrompt="False" 
            ReportSourceID="CrystalReportSource1" ToolPanelView="None" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="~\CS_REPORT\rpt\summarybusinessreviews.rpt">
            </Report>
        </CR:CrystalReportSource>
        <br />
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
