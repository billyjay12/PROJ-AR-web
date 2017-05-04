<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pending_orders.aspx.cs" Inherits="ARMS_W.Reports.Page.pending_reports" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="95%" 
            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
            ShowBackButton="False" ShowCredentialPrompts="False" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowParameterPrompts="False" ShowPromptAreaButton="False" 
            ShowZoomControl="False" Height="550px" SizeToReportContent="True" 
            ShowPrintButton="False">
            <LocalReport ReportPath="Reports\RDLC\pending_oreders.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" 
                        Name="rpt_pending_orders" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ARMSConnectionString3 %>" 
            SelectCommand="select 1"></asp:SqlDataSource>
        <asp:ScriptManager ID="ScriptManager1"   ScriptMode="Release" runat="server">
        </asp:ScriptManager>
        <br />
    
    </div>
    </form>
</body>
</html>
