<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%: ResolveUrl("~/") %>Content/ReportList.css" rel="stylesheet" type="text/css" />
    <script src="<%: ResolveUrl("~/") %>Scripts/reportlist_salesorderstatus.js" type="text/javascript"></script>

    <% 
        _User current_user = (_User)Session["Ousr"];
    %>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Sales Order </b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right"> &nbsp;&nbsp;&nbsp; </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:0;" >
        <div class="report_menu">
        <% if (current_user.HasPositionsOf(new string[] { "CSR", "CSM", "SO", "ASM", "SIM", "CHM", "CA" })) { %>
        <a href="javascript:;" onclick="javascript:ShowReportViewer('salesorderstatus');"  >Sales Order Status</a>
        <% } %>
        </div>
    </div>
    </div>
    <input type="hidden" id="uploadDateTime" />
</asp:Content>
