<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>
<script runat="server">


</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
     <% DataTable ListOfUsers;  %>
    
    
    
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>List Of Users</b>
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
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>UserProfile/AddNewUser">>> Add New User</a>
                </td>
            </tr>
        </table>
    </div>
     
    <div class="simple_box">
		<table id="tbl_lst_Users" cellpadding="1" cellspacing="0" border="0">
			<tr>
				<th  align="left" style="width: 140px">Employee ID No.</th>
                <th  align="left" style="width: 300px">Name</th>
				<th  align="left" style="width: 160px">Position</th>
                <th  align="left" style="width: 130px">Status</th>
				</tr>

                </table>
	            </div>

         <div class="simple_box" style="height:650px;overflow:scroll;">
		<table id="tbl_lst_Users1" cellpadding="1" cellspacing="0" border="0">


            <% 
                ListOfUsers = SqlDbHelper.getDataDT(SqlQueryHelper.Profiles());
                if (ListOfUsers.Rows.Count > 0)
                {
                    for (int i = 0; i < ListOfUsers.Rows.Count; i++)
                    {
                    Response.Write("<tr>");
                    Response.Write("<td  style=\"width:140px;\" align=\"left\"><a href=\"" + ResolveUrl("~/") + "UserProfile/Profile?empIdNo=" + ListOfUsers.Rows[i].ItemArray[0] + "\">" + ListOfUsers.Rows[i].ItemArray[0] + "</a></td>");
                    Response.Write("<td  style=\"width:300px;\" align=\"left\">" + ListOfUsers.Rows[i].ItemArray[1] + "</td>");
                    Response.Write("<td  style=\"width:160px;\" align=\"left\">" + ListOfUsers.Rows[i].ItemArray[2] + "</td>");
                    Response.Write("<td  style=\"width:130px;\" align=\"left\">" + ListOfUsers.Rows[i].ItemArray[3] + "</td>");
                    Response.Write("</tr>");
                    }
                }
            %>
		</table>
	</div>
    </div>
</asp:Content>
