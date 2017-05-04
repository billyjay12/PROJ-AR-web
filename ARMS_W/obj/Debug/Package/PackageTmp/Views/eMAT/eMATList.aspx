<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% DataTable ematTable;  %>

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>

    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>E-MAT</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>eMAT/Create">>>Create New E-MAT</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="width: 200px; height: 200px; overflow: scroll; border: 5px dashed black; background-color: #ccc;">
		<table id="tbl_lst_eMAT" cellpadding="1" cellspacing="0" border="0">
			<tr>
				<th>E-MAT Document No.</th>
				<th>Buyer's Name</th>
				<th>Encoded By</th>
                <th>Status</th>
			</tr>
            <% 
                ematTable = SqlDbHelper.getDataDT(SqlQueryHelper.EMATListFiltered(HttpContext.Current));
                if (ematTable.Rows.Count > 0) {
                    for (int i = 0; i < ematTable.Rows.Count; i++) {
                    Response.Write("<tr>");
                    Response.Write("<td><a href=\"" + ResolveUrl("~/") + "Document/AccountsDetails?ccanum=" + ematTable.Rows[i].ItemArray[0] + "\">" + ematTable.Rows[i].ItemArray[0] + "</a></td>");
                    Response.Write("<td>" + ematTable.Rows[i].ItemArray[1] + "</td>");
                    Response.Write("<td>" + ematTable.Rows[i].ItemArray[2] + "</td>");
                    Response.Write("<td>" + ematTable.Rows[i].ItemArray[3] + "</td>");
                    Response.Write("</tr>");
                    }
                }
            %>
		</table>
	</div>

</asp:Content>
