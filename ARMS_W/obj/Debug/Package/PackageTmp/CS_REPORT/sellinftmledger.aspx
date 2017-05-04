<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sellinftmledger.aspx.cs" Inherits="ARMS_W.CS_REPORT.sellinftmledger" %><%@ Import Namespace="System.Data" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

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
                    <b>INDIVIDUAL SELL-IN - LEDGER TYPE (FTM)</b>
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
     <asp:Label ID="Label1" runat="server" Text="Select Customer:"></asp:Label>
     
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="acctname" 
                        DataValueField="acctname" Height="24px" Width="400px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ARMSConnectionString %>" 
            SelectCommand="customer_filter" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="username" SessionField="username" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
     
      <asp:Button ID="Button1" runat="server" Text="OK" Width="91px" onclick="Button1_Click" />
    </div>
    <div>
    </div>
    <div>
       <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableParameterPrompt="False" 
            ReportSourceID="CrystalReportSource1" ToolPanelView="None" 
            EnableDatabaseLogonPrompt="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="~\CS_REPORT\rpt\sell-in-ftm-ledger.rpt">
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
