<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SOMTDPerformance.aspx.cs" Inherits="ARMS_W.Reports.Page.SOMTDPerformance" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 714px">
    <form id="form1" runat="server">
    <div style="min-width:800px; min-height: 640px;">
   
        <br />
        Sales Officer: <asp:DropDownList ID="comboSO" runat="server">
        </asp:DropDownList>
        Month:&nbsp;
        <asp:DropDownList ID="comboMonth" runat="server">
        </asp:DropDownList>
&nbsp;Year:
        <asp:DropDownList ID="comboYear" runat="server">
        </asp:DropDownList>

        <asp:Button ID="btnView" runat="server" Text="View Report" 
            onclick="btnView_Click" />
&nbsp;<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="794px" 
            Height="686px">
            <ServerReport ReportPath="/arms reports/salesofficermtdperformance" 
                ReportServerUrl="http://reportserver/webreportserver" />
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" ScriptMode="Release" runat="server">
        </asp:ScriptManager>

    </div>
    </form>
</body>
</html>
