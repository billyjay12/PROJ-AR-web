<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesorderstatus.aspx.cs" Inherits="ARMS_W.Reports.Page.salesorderstatus" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="550px" 
            ShowBackButton="False" ShowCredentialPrompts="False" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowParameterPrompts="False" ShowPrintButton="False" 
            ShowPromptAreaButton="False" ShowZoomControl="False" SizeToReportContent="True" 
            Width="95%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Reports\RDLC\salesorderreport.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
        <br />
    
    </div>
    <asp:ScriptManager ID="ScriptManager1"   ScriptMode="Release" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
