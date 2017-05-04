<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <% DataTable mrktgTable;  %>
     <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>


    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

    </script>


    <% 
 // QUERY FOR THE POSITION OF THE USER
        string userPosition="";
        string strquery = "SELECT position FROM userHeader WHERE userName='" + Session["username"] + "'";
        OleDbDataReader greader = SqlDbHelper.getData(strquery);
        if (greader.Read())
        {
            userPosition = greader.GetValue(0).ToString();

        }
         %>


    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Marketing Program Details</b>
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
                   <% if (userPosition == "ssg" || userPosition == "csr" || userPosition == "csm" || userPosition == "ssm")
                    { %>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingPog">>>Create New Marketing Program</a>
                </td>
                 <% } %> 

            </tr>
        </table>
    </div>


    <div class="simple_box">
		<table id="tbl_lst_mrktPrgm" cellpadding="1" cellspacing="0" border="0" >
			<tr>
				<th style="width: 100px;">Marketing Program No.</th>
				<th style="width: 150px;">Program Name</th>
				<th style="width: 150px;">Brand</th>
             <!--   <th style="width: 150px;">Status</th>-->

                
			</tr>


            <% 
                mrktgTable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfmarketingProgram());
                if (mrktgTable.Rows.Count > 0)
                {
                    for (int i = 0; i < mrktgTable.Rows.Count; i++)
                    {
                    Response.Write("<tr>");
                    Response.Write("<td><a href=\"" + ResolveUrl("~/") + "MarketingProgram/MarketingProgramStatus?programNo=" + mrktgTable.Rows[i].ItemArray[0] + "\">" + mrktgTable.Rows[i].ItemArray[0] + "</a></td>");
                    Response.Write("<td>" + mrktgTable.Rows[i].ItemArray[1] + "</td>");
                    Response.Write("<td>" + mrktgTable.Rows[i].ItemArray[2] + "</td>");
                    //Response.Write("<td>" + mrktgTable.Rows[i].ItemArray[3] + "</td>");
                    Response.Write("</tr>");
                    }
                }
            %>

            </table>
            </div>

</asp:Content>
