<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="System.Web.Mvc.Html5" %>
<%@ Import Namespace="ARMS_W.Class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% string pmonth = ViewData["month"].ToString();
       string pyear = ViewData["year"].ToString();
       string soId = ViewData["soId"].ToString();
    %>

    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/fullcalendar.print.css" rel="stylesheet"
        type="text/css" media="print" />
    <%--<link href="<%:ResolveUrl("~/") %>Content/theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/gcal.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/Mycalendar2.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/Mycalendar2_routechangeslog.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-buttons.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-thumbs.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.fancybox.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/autoNumeric.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

    var baseUrl = "<%=ResolveUrl("~/") %>";

        var pmonth = "<%: pmonth %>";
        var pyear = "<%: pyear %>";
        var soId = "<%: soId %>";
    </script>
    <style type="text/css">
        .fc-button.fc-button-month.fc-state-default.fc-state-active, .fc-button.fc-button-basicWeek.fc-state-default.fc-corner-right.fc-state-active
        {
            z-index: -1;
        }
        .style2
        {
            width: 77px;
        }
        .style3
        {
            border: solid 1px #c7c7c7;
            background-color: #f1efe8;
        }
        #dialog_box span
        {
            text-decoration: underline;
        }
        .invalid_data
        {
            color: Red;
        }
        .tabcolor > a
        {
            background-color: #FFD700;
        }
        input[readonly]
        {
            background-color: #ededed;
        }
        #legends
        {
            font-weight: bold;
        }
        #legends img
        {
            vertical-align: middle;
            border: 2px;
        }
        .fc-header-right
        {
            vertical-align: middle;
        }
        
        /* #calendar .fc-content
     {
         background-color:#F5F5F5
     }
    */
        #calendar .fc-event
        {
            box-shadow: 2px 2px 2px #706868;
        }
        #choose_month_table
        {
            border: 1px 1px 1px 1px;
            border-radius: 10px;
            color: rgb(20, 167, 198);
            background-color: rgb(255, 255, 255); /* border-color:rgb(20, 167, 198); */
            height: 30px;
        }
        #choose_month_table td
        {
            text-align: center;
            color: rgb(20, 167, 198);
            width: 200px;
            height: 100%;
            vertical-align: middle;
            line-height: 2em;
        }
        #choose_month_table .month:hover
        {
            background-image: url(../Images/border.jpg); /* IE10 Consumer Preview */ /*background-image: -ms-linear-gradient(bottom right, #FFFFFF 0%, rgb(20, 167, 198) 100%);

        /* Mozilla Firefox */ /*background-image: -moz-linear-gradient(bottom right, #FFFFFF 0%,rgb(20, 167, 198) 100%);

        /* Opera */ /*background-image: -o-linear-gradient(bottom right, #FFFFFF 0%, rgb(20, 167, 198) 100%);

        /* Webkit (Safari/Chrome 10) */ /*background-image: -webkit-gradient(linear, right bottom, left top, color-stop(0, #FFFFFF), color-stop(1, rgb(20, 167, 198)));

        /* Webkit (Chrome 11+) */ /*background-image: -webkit-linear-gradient(bottom right, #FFFFFF 0%, rgb(20, 167, 198) 100%);

        /* W3C Markup, IE10 Release Preview */ /*background-image: linear-gradient(to top left, #FFFFFF 0%,rgb(20, 167, 198) 100%);*/
            color: rgb(255, 255, 255);
            text-align: center;
        }
        #choose_month_table a
        {
            text-decoration: none;
            color: Black;
            font-size: 90%;
            display: block;
            width: 100%;
            height: 100%;
        }
        .selected
        {
            color: rgb(255, 255, 255);
            text-align: center;
            background-image: url(../Images/border.jpg);
        }
        .arrow
        {
            display: inline-block;
            width: 7px;
            height: 7px;
            line-height: 7px;
            border-top: 3px solid #aaa;
            border-right: 3px solid #aaa;
            -ms-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            -webkit-transform: rotate(45deg);
            transform: rotate(45deg);
            cursor: pointer;
        }
        .arrow-down
        {
            -ms-transform: rotate(135deg);
            -moz-transform: rotate(135deg);
            -webkit-transform: rotate(135deg);
            transform: rotate(135deg);
        }
        .arrow-left
        {
            -ms-transform: rotate(225deg);
            -moz-transform: rotate(225deg);
            -webkit-transform: rotate(225deg);
            transform: rotate(225deg);
        }
        .arrow-up
        {
            -ms-transform: rotate(-45deg);
            -moz-transform: rotate(-45deg);
            -webkit-transform: rotate(-45deg);
            transform: rotate(-45deg);
        }
        .arrow:hover
        {
            border-color: #444;
        }
        .disable-highlight
        {
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }
    </style>
    <% 
            _User cuurent_user = (_User)Session["Ousr"];
           
        %>
    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>My Monthly Work Plan</b>
                    </td>
                    <td align="right" valign="middle">
                        <%: Session["InputedUname"] %>
                        - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="page_header_y">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle" class="style2">
                        <a id="btn_menu" href="javascript:;">
                            <img alt="" src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border: 0;" />
                            MENU </a>
                    </td>
                    <td valign="bottom" id="upload_download_template">
                         <% if (cuurent_user != null) if (cuurent_user != null) if (cuurent_user.HasPositionOf("SIM") > -1)
                                                   { %>
                            <span style="font-size:11px;color:red"> Batch Upload Data From Excel</span><br />
                            <select id="soList"></select>&nbsp;<a href="javascript:;" id="lnk_upload_excel">&nbsp;Batch Data Upload</a>&nbsp; |
                            <a href="<%:ResolveUrl("~/") %>Template/monthly coverage template.xlsx" id="lnk_excel_template" class="cls_excel_file">Download Excel template</a>&nbsp;|
                        <% } %>
                            <a href="javascript:display_transaction_logs('calendar');" id="lnk_transaction_logs">Transaction Logs</a>
                            <button id="btnRefresh">Refresh</button>
                    </td>
                    <td align="right" valign="middle">
                        <span class="style4"><b><span id="doc_stat_msg" style="color: #da4707; font-size: medium;">
                        </span></b></span>
                    </td>
                    <%-- <td align="right">--%>
                    <%--<span><a id="sub_menu_link_2"> Save and Send </a></span>--%><%--|
                        <a id="sub_menu_link_3"> Return </a>|
                        <a id="sub_menu_link_1"> Search </a>--%>
                    <%-- </td>--%>
                </tr>
            </table>
        </div>
        <%-- <div class="post-taglist">

            <a class="post-tag" rel="tag" title="" href="/questions/tagged/google-chrome">
                January
            </a>
            <a class="post-tag" rel="tag" title="" href="/questions/tagged/geolocation">
                Cancel
            </a>

        </div>--%>
        <div class="simple_box" style="padding: 10px; font-size: 12px; overflow-y: scroll;">
            <table id="choose_month_table" class="bordered" style="width: 100%; text-align: center;"
                border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td align="left">
                        <table border="0">
                            <tr>
                                <td>
                                    <span class="arrow arrow-left disable-highlight" id="btn_prev_year"></span>
                                </td>
                                <td>
                                    <span class="arrow arrow-right disable-highlight" id="btn_next_year"></span>
                                </td>
                                <td>
                                    <h2 class="disable-highlight" style="display: inline; font-size: 24px; font-weight: 300;"
                                        id="year">
                                    </h2>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="ui-state-default ui-corner-left month" id="1">
                        <a href="javascript:gotodate('0',this);">JANUARY</a>
                    </td>
                    <td class="ui-state-default month" id="2">
                        <a href="javascript:gotodate('1',this);">FEBRUARY</a>
                    </td>
                    <td class="ui-state-default month" id="3">
                        <a href="javascript:gotodate('2',this);">MARCH</a>
                    </td>
                    <td class="ui-state-default month" id="4">
                        <a href="javascript:gotodate('3',this);">APRIL</a>
                    </td>
                    <td class="ui-state-default month" id="5">
                        <a href="javascript:gotodate('4',this);">MAY</a>
                    </td>
                    <td class="ui-state-default month" id="6">
                        <a href="javascript:gotodate('5',this);">JUNE</a>
                    </td>
                    <td class="ui-state-default month" id="7">
                        <a href="javascript:gotodate('6',this);">JULY</a>
                    </td>
                    <td class="ui-state-default month" id="8">
                        <a href="javascript:gotodate('7',this);">AUGUST</a>
                    </td>
                    <td class="ui-state-default month" id="9">
                        <a href="javascript:gotodate('8',this);">SEPTEMBER</a>
                    </td>
                    <td class="ui-state-default month" id="10">
                        <a href="javascript:gotodate('9',this);">OCTOBER</a>
                    </td>
                    <td class="ui-state-default month" id="11">
                        <a href="javascript:gotodate('10',this);">NOVEMBER</a>
                    </td>
                    <td class="ui-state-default ui-corner-right month" id="12">
                        <a href="javascript:gotodate('11',this);">DECEMBER</a>
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="3" width="100%" id="tbl_calendarholder">
                <tr>
                    <td class="hiddenTD">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <input type="hidden" id="txt_EventId" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <input type="hidden" id="txt_so_id" />
                                </td>
                                <td>
                                    <input type="hidden" id="txt_hidden_id" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="calendar">
                        </div>
                    </td>
                </tr>
                <tr class="hiddenTR">
                    <td align="center">
                        Remarks
                    </td>
                </tr>
                <tr class="hiddenTR">
                    <td align="center">
                        <textarea rows="2" style="width: 250px; height: 50px;" cols="2" id="txt_apprvrRemarks"></textarea>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="button" id="btn_SaveAndSend" value="Save and Send" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" style="float: right">
                        <table id="tbl_footer">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td style="width: 1px;">
                                </td>
                                <td align="right" style="width: 100%; text-align: right;">
                                    <span style="font-size: medium;">Call Planning Effectiveness &nbsp; &nbsp; <span
                                        id="sp_true" style="font-weight: bold;"></span>/<span id="sp_false" style="font-weight: bold;"></span>
                                        &nbsp; &nbsp; &nbsp; <span id="sp_avg"></span></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dialog_transaction_log_box" style="display: none;">
        </div>
        <div id="dialog_box" style="display: none;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr style="height: 30px">
                    <td colspan="4">
                        [ <span id="txt_date"></span>]
                    </td>
                </tr>
                <tr>
                    <td>
                        Account Code:
                    </td>
                    <td>
                        <input type="text" id="txt_acct_code" readonly="readonly" />
                    </td>
                    <td>
                        Account Name:
                    </td>
                    <td>
                        <input type="text" id="txt_acct_name" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Contact Person:
                    </td>
                    <td>
                        <input type="text" id="txt_cntct_person" readonly="readonly" />
                    </td>
                    <td>
                        Contact Person No:
                    </td>
                    <td>
                        <input type="text" id="txt_cntct_person_no" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Hotel Name:
                    </td>
                    <td>
                        <input type="text" id="txt_hotel_name" />
                    </td>
                    <td>
                        Hotel Number:
                    </td>
                    <td>
                        <input type="text" id="txt_hotel_num" />
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        Store Checking:
                    </td>
                    <td  colspan="3"> <textarea rows="2" cols="2" id="txt_store_checking" readonly="readonly"></textarea></td>
                </tr>
                <tr>
                    <td>
                        Issues and Concerns:
                    </td>
                    <td colspan="3"><textarea rows="2" cols="2" id="txt_issues_concerns"></textarea></td>
                </tr>--%>
            </table>
            <%--<table id="tbl_objectives" border="0" cellpadding="1" cellspacing="2">
                <tr>
                    <th>Objective Code</th>
                    <th>Counter Clerk</th>
                    <th>Counter Clerk No</th>
                    <th>Product Presented</th>
                    <th>Brand</th>
                    <th>Planned Amount</th>
                </tr>
                <tr class="last_row">
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                </tr>
                </table>--%>
            <br />
            <div id="tab_main">
                <ul>
                    <li id="collection"><a href="#tabs-1">Collection</a></li>
                    <li id="merchandise"><a href="#tabs-2">Merchandise</a></li>
                    <li id="sales"><a href="#tabs-3">Sales</a></li>
                    <li id="customerservice"><a href="#tabs-4">Customer Service</a></li>
                </ul>
                <div id="tabs-1" style="height: 200px; overflow: scroll">
                    <!-- TAB-1 CONTENT START -->
                    <table id="tbl_collection_dtls" cellpadding="0" cellspacing="0">
                        <tr align="center" style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x;
                            padding: 0px; font-weight: bold;">
                            <td class="hiddenTd" style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"
                                width="1px">
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Brand
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Amount
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"
                                width="10px">
                            </td>
                        </tr>
                        <tr class="last_row">
                            <td class="hiddenTd">
                            </td>
                            <td>
                                <input type="text" id="txt_collectBrand" readonly="readonly" />
                            </td>
                            <td>
                                <input type="text" id="txt_collectAmount" class="auto" />
                            </td>
                            <td>
                                <img alt="" src="<%=ResolveUrl("~/") %>Images/add.png" class="btn_add" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- TAB-1 CONTENT END -->
                <!-- TAB-2 CONTENT START -->
                <div id="tabs-2" style="height: 200px; overflow: scroll">
                    <table id="tbl_merchandise">
                        <tr>
                            <td>
                                Store Checking
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <textarea rows="2" cols="2" style="width: 335px; height: 103px" id="txt_storechecking"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table id="tbl_mse_details" cellpadding="0" cellspacing="0">
                        <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x;
                            padding: 0px; text-align: center; font-weight: bold;">
                            <td class="hiddenTd" style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Product Presented
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Counter Clerk
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Mobile No.
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;"
                                width="10px;">
                            </td>
                        </tr>
                        <tr class="last_row">
                            <td class="hiddenTd">
                            </td>
                            <td>
                                <input type="text" id="txt_mse_productpresented" />
                            </td>
                            <td>
                                <input type="text" id="txt_mse_counterclerk" />
                            </td>
                            <td>
                                <input type="text" id="txt_mse_mobileno" />
                            </td>
                            <td>
                                <img alt="" src="<%=ResolveUrl("~/")%>Images/add.png" class="btn_add" />
                            </td>
                            <td width="10px;">
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- TAB-2 CONTENT END -->
                <!-- TAB-3 CONTENT START -->
                <div id="tabs-3" style="height: 200px; overflow: scroll">
                    <table id="tbl_sales_dtls" cellpadding="0" cellspacing="0">
                        <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x;
                            padding: 0px; text-align: center; font-weight: bold;">
                            <td class="hiddenTd" style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Brand
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Amount
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                                Details
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">
                            </td>
                            <td style="background: url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"
                                width="1px;">
                            </td>
                        </tr>
                        <tr class="last_row">
                            <td class="hiddenTd">
                            </td>
                            <td>
                                <input type="text" id="txt_salesBrand" readonly="readonly" />
                            </td>
                            <td>
                                <input type="text" id="txt_salesAmount" class="auto" />
                            </td>
                            <td>
                                <input type="text" id="txt_remarks"/>
                            </td>
                            <td>
                                <img alt="" src="<%=ResolveUrl("~/") %>Images/add.png" class="btn_add" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- TAB-3 CONTENT END -->
                <!-- TAB-4 CONTENT START -->
                <div id="tabs-4" style="height: 200px; overflow: scroll">
                    <table id="tbl_custService">
                        <tr>
                            <td>
                                Issues and Concerns
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <textarea rows="2" cols="2" style="width: 335px; height: 103px" id="txt_issue_concern"></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- TAB-4 CONTENT END -->
            </div>
        </div>
        <div id="grp_upload_buttons" style="width: 100%; text-align: center; display: none;">
            <input type="button" id="btn_save_upload" value="Save upload" />
            <input type="button" id="btn_cancel_upload" value="Cancel upload" />
        </div>
    </div>
</asp:Content>
