﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailedCallReport.aspx.cs" Inherits="ARMS_W.Reports.Page.DetailedCallReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
       <div style="min-width:800px; min-height: 640px;">
        <asp:ScriptManager ID="ScriptManager1"  ScriptMode="Release" runat="server">
        </asp:ScriptManager>

        <br />
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
            <ServerReport ReportPath="/arms reports/detailcallreport" 
                ReportServerUrl="http://reportserver/webreportserver" />
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
