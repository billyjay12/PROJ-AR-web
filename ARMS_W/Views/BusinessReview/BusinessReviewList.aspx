<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <% 
         DataTable busReviewTable; 
      
        _User Ousr = new _User(Session["username"].ToString());
        const int IS_NOT_FOUND = -1;
        
    %>

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Business Review</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>
    <% if (Ousr.HasPositionOf("vpbsm") != IS_NOT_FOUND || Ousr.HasPositionOf("ca") != IS_NOT_FOUND || Ousr.HasPositionOf("ssm") != IS_NOT_FOUND || Ousr.HasPositionOf("ssgm") != IS_NOT_FOUND || Ousr.HasPositionOf("chm") != IS_NOT_FOUND || Ousr.HasPositionOf("sim") != IS_NOT_FOUND)
       {%>
    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>BusinessReview/SetUp">>> Schedule New Business Review</a>
                </td>
            </tr>
        </table>
    </div>
     <% } %>

    <div class="simple_box">
		<table id="tbl_lst_busReview" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
			<tr>
				<th style="width: 159px">Business Review No.</th>
                <th style="width: 106px">Review Date</th>
				<th style="width: 130px">Account Code</th>
				<th style="width: 350px">Account Name</th>
                <th>Status</th>
			</tr>

            </table>
	</div>

    <div class="simple_box" style="height:650px;overflow: scroll;" >
		<table id="tbl_lst_busReview1" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">

            <%  
                _User CurrentUser = new _User(Session["username"].ToString());
                
                busReviewTable = SqlDbHelper.getDataDT(SqlQueryHelper.BusRevListFiltered( HttpContext.Current) );

                string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
                string[] AreaUsers = { "asm" };
                string[] ChannelUsers = { "chm", "cmg", "ca", "cmm" };
                string[] OtherUsers = { "vpbsm", "vptfi", "ceo", "ssm", "ssgm", "brd", "admin", "mad" };
                
                foreach (DataRow item in busReviewTable.Rows)
                {
                    bool CanViewCurrentDoc = false;

                    if (Ousr.HasPosAndRegionsOf(RegionUsers, item["region"].ToString()))
                    {
                        // REGION
                        CanViewCurrentDoc = true;
                    }
                    else if (Ousr.HasPosAndAreasOf(AreaUsers, item["area"].ToString()))
                    {
                        // AREA
                        CanViewCurrentDoc = true;
                    }
                    else if (Ousr.HasPosAndChannelsOf(ChannelUsers, item["channel"].ToString()))
                    {
                        // CHANNEL
                        CanViewCurrentDoc = true;
                    }
                    else if (Ousr.HasPositionsOf(OtherUsers))
                    {
                        // VPs
                        CanViewCurrentDoc = true;
                    }

                    if (CanViewCurrentDoc == true)
                    {
                          Response.Write("<tr>");
                          Response.Write("<td style=\"width:159px;\" align=\"left\"><a href=\"" + ResolveUrl("~/") + "BusinessReview/BusinessReviewDetails?busReviewNo=" + item["busReviewNo"].ToString() + "\">" + item["busReviewNo"].ToString() + "</a></td>");
                          Response.Write("<td style=\"width:106px;\" align=\"left\">" + item["newBusRevDate"].ToString() + "</td>");
                          Response.Write("<td style=\"width:130px;\" align=\"left\">" + item["acctCode"].ToString() + "</td>");
                          Response.Write("<td style=\"width:350px;\" align=\"left\">" + item["acctname"].ToString() + "</td>");
                          Response.Write("<td>" + item["status"].ToString() + "</td>");
                          Response.Write("</tr>");
                    }
                }
            %>
		</table>
	</div>
    </div>

</asp:Content>
