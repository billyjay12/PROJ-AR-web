<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoveragePlanNoOfAccounts.aspx.cs" Inherits="ARMS_W.Reports.Page.CoveragePlanNoOfAccounts" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1"  ScriptMode="Release" runat="server">
        </asp:ScriptManager>

          <asp:Button ID="button1" runat="server" Text="View Report" 
            onclick="btnViewReport_Click" style="height: 26px"/>
        
        <br />

     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="658px" InteractiveDeviceInfos="(Collection)" 
            ProcessingMode="Remote" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" Width="1145px" style="margin-right: 213px">
            <ServerReport ReportServerUrl="http://192.168.10.13/webreportserver" />
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
