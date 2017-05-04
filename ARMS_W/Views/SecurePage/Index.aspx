<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="<%=ResolveUrl("~/") %>Scripts/SecurePage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var baseUrl = "<%= ResolveUrl("~/") %>";
        var G_username = '<%: Session["username"] %>';
        $(function () {
            
            // CheckActive('<%: Session["username"].ToString() %>');
        });
    </script>

    <div class="bl_box" >
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Account Resource Management System</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box">
        <table id="TableContainer" width="100%" border="0" cellspacing="0">
            <tr>
                <td class="PHeader" align="center" style="padding-left:50px; padding-bottom:10px;">
	                <!-- HEADER -->
                    <img src="<%=ResolveUrl("~/") %>Images/matimco_Watermarklogo.jpg" width="100%" height="100%" />
                </td>
            </tr>
        </table>
    </div>
    
    </div>
</asp:Content>
