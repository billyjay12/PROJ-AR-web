<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<h2>Changes Details</h2>--%>

   <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>

   <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("~/") %>Scripts/ChangesDtls.js" type="text/javascript"></script>

   <% 
        string EventId = ViewData["EventId"].ToString();
        string Eventmonth = ViewData["month"].ToString();
        string Eventyear = ViewData["year"].ToString();
        string soId = ViewData["soId"].ToString();
        string SoName = ViewData["SoName"].ToString();
    %>


    <script type="text/javascript" language="javascript">

        var Eventmonth = "<%: Eventmonth %>";
        var Eventyear = "<%: Eventyear %>";
        var soId = "<%: soId %>";
        var EventID = "<%: EventId %>";
        var SoName ="<%:SoName %>";

        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

     <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                 <tr>
                    <td align="left" valign="middle">
                        <b>Changes Details</b>
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
                  <%--  <% 
                        if ( Ousr.HasPositionOf("csr") != -1 ) { 
                    %>
                        <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>eMAT/Create">>> Create New E-MAT</a>
                    <% 
                        } 
                    %> --%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="simple_box" style="padding:0;">

            <table>
                <tr>
                    <td>Sales Officer ID</td>
                    <td><input type="text" id="txt_sales_id" readonly="readonly"/></td>
                    <td>Sales Name</td>
                    <td><input type="text" id="txt_sales_name" readonly="readonly"/></td>
                </tr>
               <tr>
                    <td>Tracking No.</td>
                    <td><input type="text" id="txt_coveragetrackingno" readonly="readonly"/></td>
                    <td>Month</td>
                    <td><input type="text" id="txt_month" readonly="readonly"/><input type="hidden" id="txt_hidden_month" readonly="readonly"/></td>
               </tr>
               <tr>
                    <td>Year</td>
                    <td><input type="text" id="txt_year" readonly="readonly"/></td>
               </tr>
            </table>

            <br />

            <table id="tbl_changesDtls"  cellpadding="0" cellspacing="1" border="0" style="color:#000000; width: 265px;">
                <tr style="height:30px; background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px;">
                    <td style="width:40px;background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"><input type="checkbox" id="chk_selectall" /></td>
                    <td style="width: 80px;background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"><b>Day</b></td>
                    <td style="width:150px;background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"><b>Account Code</b></td>
                    <td style="width:150px;background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"><b>Remarks</b></td>
                    <%--<td style="width:10px; background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"></td>--%>
     
                </tr>
                <tr class="last_row">
                    <td><br /></td>
                </tr>
                <tr>
                    <td><br /></td>
                </tr>
                <tr class="hideTR" align="center">
                    <td colspan="4"><input type="button" value="Approve" id="btn_approve" /> / 
                    <input type="button" value="Disapprove" id="btn_disapprove"/></td>
                </tr>
                <tr class="hideTR">
                    <td></td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
