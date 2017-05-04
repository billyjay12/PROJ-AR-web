<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ARMSNewsFeedReport.aspx.cs" Inherits="ARMS_W.Reports.Page.ARMSNewsFeedReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:640px; width:800px; margin-right: 7px;">
        <asp:ScriptManager ID="ScriptManager1"  ScriptMode="Release" runat="server">
        </asp:ScriptManager>

        <asp:Label ID="Label1" runat="server" Text="Select Region: "  Font-Names="Verdana" Font-Size="8pt" ></asp:Label>
        <asp:DropDownList ID="dropdownRegion" runat="server" style="margin-top: 0px" 
            onselectedindexchanged="dropdownRegion_SelectedIndexChanged"  
            AutoPostBack="True" Font-Names="Verdana">
        </asp:DropDownList>

        <br />

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="658px" InteractiveDeviceInfos="(Collection)" 
            ProcessingMode="Remote" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt"  Width="750px" style="margin-right: 213px">
        <ServerReport ReportServerUrl="http://192.168.10.13/webreportserver" />
        </rsweb:ReportViewer>

        <br />
    </div>
    </form>
</body>
</html>
