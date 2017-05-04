<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CredInvestigation.aspx.cs" Inherits="ARMS_W.CS_REPORT.CredInvestigation" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="<%=ResolveUrl("~/") %>CR13/js/crviewer/images/style.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>CR13/js/crviewer/crv.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlistindex.js" type="text/javascript"></script>
   
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server"> 
        
        <div style="padding:5px 8px 5px 8px; background:#2982b6; color:White;">
            <b>Credit Investigation Report</b>
        </div>

        <div class="simple_box">    
            <div style="margin:2px 0px 2px 0px; padding:5px 10px 5px 10px;">
                <asp:Label ID="Label1" runat="server" Text="Select Customer:"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="acctname" 
                    DataValueField="acctcode" Height="24px" Width="400px">
                </asp:DropDownList>
                <asp:Button ID="Button1" runat="server" Text="OK" Width="91px" onclick="Button1_Click" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ARMSConnectionString %>" 
                    SelectCommand="customer_filter" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="username" SessionField="username" Type="String" 
                            DefaultValue="" ConvertEmptyStringToNull="False" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>

            <div>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </div>
            <div>
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" ReportSourceID="CrystalReportSource1" 
                ToolPanelView="None" EnableParameterPrompt="False" 
                EnableDatabaseLogonPrompt="False" HasCrystalLogo="False" />
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="~\CS_REPORT\rpt\Credit Investigation Tab Info.rpt">
                </Report>
                </CR:CrystalReportSource> 
                <br />
            </div>
        </div>

    </form>
</body>
</html>
