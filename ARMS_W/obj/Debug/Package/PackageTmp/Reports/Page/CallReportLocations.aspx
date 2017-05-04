<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallReportLocations.aspx.cs" Inherits="ARMS_W.Reports.Page.CallReportLocations" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        Date To: <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
        Date From: <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
        <asp:CalendarExtender   
        ID="CalendarExtender1"   
        TargetControlID="txtStartDate"   
        runat="server"></asp:CalendarExtender>
    <asp:CalendarExtender
        ID="CalendarExtender2"
        TargetControlID="txtEndDate"
        runat="server"></asp:CalendarExtender>
         &nbsp;
         
        <asp:Button ID="btn_view" runat="server" Text="View Report" 
            onclick="btn_view_Click" />
            <br />
&nbsp;<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="794px" 
            Height="686px">
            <ServerReport ReportPath="/arms reports/call report locations" 
                ReportServerUrl="http://reportserver/webreportserver" />
        </rsweb:ReportViewer>
    
        <%--<asp:ScriptManager ID="ScriptManager1"  ScriptManager1runat="server"></asp:ScriptManager>--%>

<asp:ToolkitScriptManager ID="tsmAdmin" runat="server" EnablePartialRendering="false" />
    </div>
    </form>
</body>
</html>
