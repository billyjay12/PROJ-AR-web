<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %>
<%@ Import Namespace="ARMS_W.Class" %>
<%@ Import Namespace="System.Web.Mvc.Html5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%
        string pEventId = ViewData["EventId"].ToString();
        string pmonth = ViewData["month"].ToString();
        string pyear = ViewData["year"].ToString();
        string soId = ViewData["soId"].ToString();
     %>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/fullcalendar.print.css" rel="stylesheet" type="text/css"  media="print"  />
    <%--<link href="<%=ResolveUrl("~/") %>Content/theme.css" rel="stylesheet" type="text/css" />--%>
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-buttons.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox-thumbs.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/gcal.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/calendarAsmview.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/Mycalendar2_routechangeslog.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.fancybox.js" type="text/javascript"></script>
    
    <style type="text/css">
        .fc-button.fc-button-month.fc-state-default.fc-state-active,
        .fc-button.fc-button-basicWeek.fc-state-default.fc-corner-right.fc-state-active{  z-index:-1; }
        .style2 { width: 77px; }
        .style3 { border: solid 1px #c7c7c7; background-color:#f1efe8; }
        .fc-header-right{ vertical-align:middle; }
        #calendar .fc-event{ box-shadow: 2px 2px 2px #706868; }
        #choose_month_table
        {
            border:1px 1px 1px 1px;
            border-radius: 10px;
            color: rgb(20, 167, 198);
            background-color: rgb(255, 255, 255);
            height:30px;
        }
        #choose_month_table td
        {
            text-align:center;
            color:rgb(20, 167, 198);
            width: 200px;
            height: 100%;
            vertical-align:middle;
            line-height: 2em;
        }
         #choose_month_table .month:hover
        {
            background-image:url(../Images/border.jpg);  
            /* IE10 Consumer Preview */ 
            /*background-image: -ms-linear-gradient(bottom right, #FFFFFF 0%, rgb(20, 167, 198) 100%);

            /* Mozilla Firefox */ 
            /*background-image: -moz-linear-gradient(bottom right, #FFFFFF 0%,rgb(20, 167, 198) 100%);

            /* Opera */ 
            /*background-image: -o-linear-gradient(bottom right, #FFFFFF 0%, rgb(20, 167, 198) 100%);

            /* Webkit (Safari/Chrome 10) */ 
            /*background-image: -webkit-gradient(linear, right bottom, left top, color-stop(0, #FFFFFF), color-stop(1, rgb(20, 167, 198)));

            /* Webkit (Chrome 11+) */ 
            /*background-image: -webkit-linear-gradient(bottom right, #FFFFFF 0%, rgb(20, 167, 198) 100%);

            /* W3C Markup, IE10 Release Preview */ 
            /*background-image: linear-gradient(to top left, #FFFFFF 0%,rgb(20, 167, 198) 100%);*/
            color: rgb(255, 255, 255);
            text-align:center;
        }
        #choose_month_table a
        {
            text-decoration:none;
            color:Black;
            font-size: 90%;
            display: block;
            width: 100%;
            height:100%;
        }
        .selected 
        {
            color: rgb(255, 255, 255);
            text-align:center;
            background-image:url(../Images/border.jpg);  
       }
       .arrow {
            display:inline-block;
            width:7px;
            height:7px;
            line-height:7px;
            border-top:3px solid #aaa;
            border-right:3px solid #aaa;
            -ms-transform:rotate(45deg);
            -moz-transform:rotate(45deg);
            -webkit-transform:rotate(45deg);
            transform:rotate(45deg);
            cursor: pointer; 
        }
        .arrow-down {
            -ms-transform:rotate(135deg);
            -moz-transform:rotate(135deg);
            -webkit-transform:rotate(135deg);
            transform:rotate(135deg);
        }
        .arrow-left {
            -ms-transform:rotate(225deg);
            -moz-transform:rotate(225deg);
            -webkit-transform:rotate(225deg);
            transform:rotate(225deg);
        }
        .arrow-up{
            -ms-transform:rotate(-45deg);
            -moz-transform:rotate(-45deg);
            -webkit-transform:rotate(-45deg);
            transform:rotate(-45deg);
        }
        .arrow:hover {
        border-color:#444;
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

    <script type="text/javascript" language="javascript">
        var baseUrl = "<%=ResolveUrl("~/") %>";
        var pEventId = "<%: pEventId %>";
        var pmonth = "<%: pmonth %>";
        var pyear = "<%: pyear %>";
        var soId = "<%: soId %>";
    </script>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>Yearly Calendar</b>
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
                <td align="left" valign="middle" class="style2">
                    <a id="btn_menu" href="javascript:;" ><img alt="" src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                </td>
                <td valign="bottom" id="upload_download_template" >
                   | <a href="javascript:display_transaction_logs('vw_calendar');" id="lnk_transaction_logs">Transaction Logs</a>
                </td>
                <td align="right" valign="middle">
                    <span>
                        <b><span id="doc_stat2_msg" style="color:#da4707; font-size:medium;"></span></b>
                    </span>
                </td>
            </tr>
            </table>
        </div>

        <div class="simple_box" style="padding:10px; font-size:12px;">
            <table id="choose_month_table" class="bordered" style="width:100%; text-align:center;" border="0" cellpadding="0" cellspacing="0" align="center">
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
                                <td><h2 class="disable-highlight" style="display: inline; font-size: 24px; font-weight:300;" id="year"></h2></td>
                            </tr>
                        </table>
                    </td>
                    <td class="ui-state-default ui-corner-left month"  id="1"><a href="javascript:gotodate('0',this);">JANUARY</a></td>
                    <td class="ui-state-default month"  id="2"><a href="javascript:gotodate('1',this);" >FEBRUARY</a></td>
                    <td class="ui-state-default month"  id="3"><a href="javascript:gotodate('2',this);">MARCH</a></td>
                    <td class="ui-state-default month"  id="4"><a href="javascript:gotodate('3',this);">APRIL</a></td>
                    <td class="ui-state-default month"  id="5"><a href="javascript:gotodate('4',this);">MAY</a></td>
                    <td class="ui-state-default month"  id="6"><a href="javascript:gotodate('5',this);" >JUNE</a></td>
                    <td class="ui-state-default month"  id="7"><a href="javascript:gotodate('6',this);">JULY</a></td>
                    <td class="ui-state-default month"  id="8"><a href="javascript:gotodate('7',this);">AUGUST</a></td>
                    <td class="ui-state-default month"  id="9"><a href="javascript:gotodate('8',this);">SEPTEMBER</a></td>
                    <td class="ui-state-default month"  id="10"><a href="javascript:gotodate('9',this);">OCTOBER</a></td>
                    <td class="ui-state-default month"  id="11"><a href="javascript:gotodate('10',this);">NOVEMBER</a></td>
                    <td class="ui-state-default ui-corner-right month"  id="12"><a href="javascript:gotodate('11',this);">DECEMBER</a></td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="3" width="100%" id="tbl_calendar_viewholder">
                <tr> 
                    <td class="hiddenTD">
                            <table>
                                <tr>
                                    <td></td>
                                    <td><input type="hidden" id="txt_vw_EventId" /></td>
                                    <td></td>
                                    <td></td>
                                    <td><input type="hidden" id="txt_vw_so_id" /></td>
                                    <td><input type="hidden" id="txt_vw_hidden_id" /></td>
                                </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td><div id="vw_calendar"></div></td>
                </tr>
                <tr>
                    <td align="center">Remarks</td>
                </tr>
                <tr>
                    <td align="center"><textarea rows="2" cols="2" style="width:250px; height:50px;" id="txt_apprvrRemarks"></textarea></td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="button" id="btn_approvedCoverage" value="Approve" /> 
                        <input type="button" id="btn_returntosender" value="Return to Sender" />
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
        </div>
    </div>
    <div id="dialog_transaction_log_box" style="display:none;"></div>

</asp:Content>
