<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/DayCycleMaintenance.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/json2.js" type="text/javascript"></script>
    <style type="text/css">
        .required { background-color:#fff7dd; border:1px solid #4e4e4e; }
    </style>


    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>Day Cycle Maintenance</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="page_header_y">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align"left" valign="middle">
                        <a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                    </td>
               
                </tr>
            </table>
        </div>
        <div class="simple_box" id="tab_main">
            <ul>
                <li><a href="#tabs-1">Day Cycle</a></li>
                <li><a href="<%:ResolveUrl("~/") %>Maintenance/getDayCycleLogChanges">Log Changes</a></li>
            </ul>
            <div id="tabs-1">
	            <div id="main_div">
                    <table>
                        <tr>
                            <td>Counting Day Cycle</td>
                            <td><input type="text" id="txt_daycycle" maxlength="2" style="text-align:right" /></td>
                        </tr>
                        <tr>
                            <td>Range day cycle</td>
                            <td><input type="text" id="txt_rangedaycecle" maxlength="2" style="text-align:right"/></td>
                        </tr>
                        <tr style="display:none">
                            <td>Day of the Month Start Counting Sched</td>
                            <td><input type="text" id="txt_startDayOfTheMonth" maxlength="2" style="text-align:right" /></td>
                        </tr>
                    </table>
                </div>
                <div class="div_blinker">
                    <input type="button" id="btn_save" value="Save" />
                    <input type="button" id="btn_cancel" value="Cancel" />
                </div>
	        </div>
            <div id="tabs-2"></div>
        </div>
    </div>
</asp:Content>
