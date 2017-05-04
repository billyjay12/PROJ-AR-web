<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="bl_box">

       <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>Activity Logs</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>
       <div class="page_header_y" >
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle"><a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a></td>
                </tr>
            </table>
        </div>
        <div class="simple_box">
            <table id="tbl_activitylist" border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td></td>
                </tr>
              <%--  <thead>
                <tr>
                    <th>Employee Name</th>
                    <th>Date Time Stamp</th>
                    <th>Field</th>
                    <th>Activity</th>
                    <th>Customer Name</th>
                    <th>Customer Address</th>
                    <th>Location</th>
                </tr>
                </thead>
                <tbody></tbody>--%>
            </table>
        </div>

    </div>

    <script src="<%:ResolveUrl("~/") %>Scripts/systempage_activitylogs.js" type="text/javascript"></script>

</asp:Content>
