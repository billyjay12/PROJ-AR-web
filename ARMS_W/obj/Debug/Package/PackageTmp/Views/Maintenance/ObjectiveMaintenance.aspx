<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=ResolveUrl("~/") %>Scripts/ObjectiveMaintenance.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/json2.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Content/jquery-ui-1.8.12.custom.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript" ></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.checkboxtree.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Content/jquery.checkboxtree.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .required { background-color:#fff7dd; border:1px solid #4e4e4e; }
    </style>


    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>Objective Maintenance</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>

        <br />
        <div id="tab_main">
            <ul>
                <li><a href="#tabs-1">Objectives</a></li>
                <li><a href="<%:ResolveUrl("~/") %>Maintenance/getObjectiveLogChanges">Log Changes</a></li>
            </ul>
            <div id="tabs-1">
                <div id="doc_status"><h2>Enable/Disable Fields</h2></div>
                <div id="div_main1">    
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
