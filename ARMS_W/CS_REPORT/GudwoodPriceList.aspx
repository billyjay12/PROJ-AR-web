<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GudwoodPriceList.aspx.cs" Inherits="ARMS_W.CS_REPORT.WebForm2" %>

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
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
          
    <div style="padding:5px 8px 5px 8px; background:#2982b6; color:White;">
        <b>GUDWOOD PRICELIST</b>
    </div>

    <div class="simple_box">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="50px" 
            ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" 
            ToolPanelView="None" ToolPanelWidth="200px" Width="350px" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="~\CS_REPORT\rpt\GudWoodPricelist.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
    </form>
</body>
</html>
