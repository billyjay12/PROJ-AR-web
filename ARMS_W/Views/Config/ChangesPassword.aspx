<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ConfigChangePassword.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Change Password</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:15px; font-size:12px;">
        
        <table border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td>New Password</td>
                <td><input type="password" id="txt_password_first" /></td>
            </tr>
            <tr>
                <td>Confirm Password</td>
                <td><input type="password" id="txt_password_second" /></td>
            </tr>
            <tr>
                <td colspan="2" align="right" >
                    <input type="button" value="Save" onclick="javascript:SavePassword();" />
                </td>
            </tr>
        </table>

    </div>
    </div>

</asp:Content>
