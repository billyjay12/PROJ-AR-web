<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bounce_checks.aspx.cs" Inherits="ARMS_W.Reports.Page.bounce_checks" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="550px" InteractiveDeviceInfos="(Collection)" 
            ShowBackButton="False" ShowCredentialPrompts="False" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowParameterPrompts="False" ShowPrintButton="False" 
            ShowPromptAreaButton="False" ShowZoomControl="False" SizeToReportContent="True" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="95%">
            <LocalReport ReportPath="Reports\RDLC\bounce_checks.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" 
                        Name="rpt_bounce_checks" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1"  ScriptMode="Release" runat="server">
        </asp:ScriptManager>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ARMSConnectionString %>" 
            SelectCommand="select 1"></asp:SqlDataSource>
        <br />
    
    </div>
    </form>
</body>
</html>
