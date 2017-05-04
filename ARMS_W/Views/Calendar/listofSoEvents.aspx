﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>

   <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("~/") %>Scripts/ListofSOEvents.js" type="text/javascript"></script> 

  

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>List of Sales Officer</b>
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
                <td colspan="2" align="right">
             
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:0;">
		<table id="tbl_listofso" width="100%" cellpadding="2" cellspacing="0" border="0" style="color:#000000;">
            <tr align="left">
                <th align="left">Coverage Tracking No.</th>
                <th align="left">Sales Officer ID</th>
                <th align="left">Sales Officer Name</th>
                <th align="left">Month</th>
                <th align="left">Year</th>
                <th align="left">Status</th>
                
            </tr>

	        <tr class="last_row"></tr>


        </table>

        <div class="div_page_navigator">
            <%--<a href="<%: str_first %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_first.png" alt="First Page" /></a> &nbsp;
            <a href="<%: str_prev %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_previous.png" alt="Previous Page" /></a> &nbsp;
            <a href="<%: str_next %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_next.png" alt="Next Page" /></a> &nbsp;
            <a href="<%: str_last %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_last.png" alt="Last Page" /></a> &nbsp;

            / &nbsp; Record/s Found: <%:TotalDocs %>--%>
        </div>

	</div>
    </div>

</asp:Content>
