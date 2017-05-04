<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesTargetDistribution.aspx.cs" Inherits="ARMS_W.Reports.Page.SalesTargetDistribution" %>

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
        SO Name: 
        <asp:DropDownList ID="comboSO" runat="server">
        </asp:DropDownList>
    &nbsp;Year:
        <asp:DropDownList ID="comboYear" runat="server">
        </asp:DropDownList>
    &nbsp;Month:
        <asp:DropDownList ID="comboMonth" runat="server">
        </asp:DropDownList>
    &nbsp;<asp:Button ID="btnViewReport" runat="server" Text="View Report"
            onclick="btnViewReport_Click" />

        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="462px" InteractiveDeviceInfos="(Collection)" 
            ProcessingMode="Remote" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" Width="869px">
            <ServerReport ReportPath="/arms reports/salestargetdistribution" 
                ReportServerUrl="http://reportserver/webreportserver" />
        </rsweb:ReportViewer>

    </div>
    </form>
</body>
</html>
