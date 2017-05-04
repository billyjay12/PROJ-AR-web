<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>

   <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("~/") %>Scripts/calendar_eventlistapprovedisapprove.js" type="text/javascript"></script> 
   
      <style type="text/css">
        #tbl_listofso {color:black;font-size:11px;}
        #tbl_listofso th {background:#ebf5fc;padding:3px;}
        #tbl_listofso td {padding:3px 5px 3px 5px;}
        #tbl_listofso td a {color:#da4707;font-weight:bold;text-decoration:none;}
        .page_header_y { font-size:11px; }
      </style>

      <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>Event List Approve Disapprove</b>
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
                    <td colspan="3">
                        Show
                        <select onchange="ShowEntries()" id="SelectOptionEntries">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="100">100</option>
                        </select>
                        entries
                    </td>
                    <td align="right">
                        Search <input type="text" id="txt_search" />
                    </td>
                </tr>
                
            </table>
        </div>

        <div class="simple_box" style="padding:0;">
		    <table id="tbl_listofso" width="100%"  width="100%" cellpadding="3" cellspacing="0" border="0"><%--cellpadding="2" cellspacing="0" border="0" style="color:#000000;">--%>
                <tr align="left">
                    <th align="left">Coverage Tracking No.</th>
                    <th align="left">Sales Officer ID</th>
                    <th align="left">Sales Officer Name</th>
                    <th align="left">Month</th>
                    <th align="left">Year</th>
                    <th align="left">Status</th>
                
                </tr>
	            <%--<tr class="last_row"></tr>--%>
            </table>

            <div class="div_page_navigator">
                <a href="javascript:;" id="lnk_first" ><img src="<%:ResolveUrl("~/") %>Images/resultset_first.png" alt="First Page" /></a> &nbsp;
                <a href="javascript:;" id="lnk_prev" ><img src="<%:ResolveUrl("~/") %>Images/resultset_previous.png" alt="Previous Page" /></a> &nbsp;
                
                page <span id="current_page">0</span> of <span id="total_page">0</span> &nbsp;

                <a href="javascript:;" id="lnk_next" ><img src="<%:ResolveUrl("~/") %>Images/resultset_next.png" alt="Next Page" /></a> &nbsp;
                <a href="javascript:;" id="lnk_last" ><img src="<%:ResolveUrl("~/") %>Images/resultset_last.png" alt="Last Page" /></a> &nbsp;

                / &nbsp; Record/s Found: <span id="total_rec">0</span>
            </div>

	    </div>
        </div>

</asp:Content>
