<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quarterlyinventorycount.aspx.cs" Inherits="ARMS_W.Reports.Page.quarterlyinventorycount" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Label ID="Label1" runat="server" Text="Quarter"></asp:Label> 
            &nbsp;
            <asp:DropDownList ID="month" runat="server" 
                onselectedindexchanged="month_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
             <asp:Label ID="Label2" runat="server" Text="Account Code"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="dropdown_acctcode" runat="server">
            </asp:DropDownList>
            &nbsp;
            <asp:Button ID="Button1" runat="server" Text="Generate Report" 
                onclick="Button1_Click" />
    </div>
    <hr />
    <div>
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="550px" ShowBackButton="false" ShowCredentialPrompts="False" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowParameterPrompts="False" ShowPrintButton="False" 
            ShowPromptAreaButton="False" ShowZoomControl="False" SizeToReportContent="True" 
            Width="95%" Font-Names="Verdana" Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Reports\RDLC\quarterlyinventorycount.rdlc">
            </LocalReport>
    </rsweb:ReportViewer>
        <br />
    </div>
    <asp:ScriptManager ID="ScriptManager1"  ScriptMode="Release" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
