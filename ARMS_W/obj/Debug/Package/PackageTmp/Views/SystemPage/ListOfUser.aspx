<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>List Of Users</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>UserProfile/AddNewUser">>> Add New User</a>
                </td>
            </tr>
        </table>
        </div>
    <div class="simple_box">
        <table cellpadding="1" cellspacing="0" border="0" width="100%" id="tbl_users">
            
            <thead>
                    <tr>
                        <th><b>Employee ID</b></th>
                        <th><b>Username</b></th>
                        <th><b>Email Address</b></th>
                        <th><b>Is Active</b></th>
                        <th>Reset</th>
                        <th>Lock/Unlock Account</th>
                    </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
     </div>


    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/systempage_user.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/complete.min.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js"></script>
    


</asp:Content>
