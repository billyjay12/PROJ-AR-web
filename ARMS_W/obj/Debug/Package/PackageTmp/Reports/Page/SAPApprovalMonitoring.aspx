<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SAPApprovalMonitoring.aspx.cs" Inherits="ARMS_W.Reports.Page.SapApprovalMonitoring" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"  Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <div>
        <form id="form1" runat="server">
            <div>
            
                <div class="rTable">
                    <div class="rTableRow">
                        <div class="rTableCell">
                            Year <asp:TextBox ID="txtYear" runat="server"></asp:TextBox> &nbsp;
                            <asp:Button
                                ID="btn_view" runat="server" Text="View Report" onclick="btn_view_Click" />
                         </div>
                    </div>
                    <div class="rTableRow">
                        <div class="rTableCell">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                                Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
                                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="794px" 
                                Height="686px">
                                <ServerReport ReportPath="/arms reports/SAP Approval Monitoring" ReportServerUrl="http://reportserver/webreportserver" />
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                </div>

                <asp:ToolkitScriptManager ID="tsmAdmin" runat="server" EnablePartialRendering="false" />
            </div>
        </form>
    </div>

</body>
</html>
