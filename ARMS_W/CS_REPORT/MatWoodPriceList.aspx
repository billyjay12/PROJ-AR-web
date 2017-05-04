<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatWoodPriceList.aspx.cs" Inherits="ARMS_W.CS_REPORT.MatWoodPriceList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

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
                    <b>MATWOOD PRICELIST</b>
                </td>
                <td align="right" valign="middle" >
                   
                </td>
             </tr>
          </table>
      </div>

      
   <div class="page_header_y">
       
      </div>
    <div class="simple_box">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="50px" 
            ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" 
            ToolPanelView="None" ToolPanelWidth="200px" Width="350px" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="~\CS_REPORT\rpt\MatwoodPricelist.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
    </div>
    </form>
</body>
</html>
