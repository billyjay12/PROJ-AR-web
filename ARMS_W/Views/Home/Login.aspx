<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site2.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="<%=ResolveUrl("~/") %>Scripts/Login.js" type="text/javascript"></script>
    
	<link href="<%=ResolveUrl("~/") %>Content/DynamicDialogBox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
    <style type="text/css">
        .required_fields { background-color:#fff7dd; border:1px solid #4e4e4e; }
    </style>

    <div id="login_container" style="text-align:left;">
	    <table class="blinker" cellpadding="2" cellspacing="0" border="0">
		    <tr>
			    <td>Username</td>
			    <td><input id="txtUsername" type="text" class="required_fields" /></td>
		    </tr>
		    <tr>
			    <td>Password</td>
			    <td><input id="txtPassword" type="password" class="required_fields" /></td>
		    </tr>
            <tr>
                <td colspan="2"><p id="p_acctlocked" style=" font-size:12px; display:none;">Account is locked. Request to unlock? Click <a href="javascript:requestUnlockAccount();" style=" text-decoration:none;"> here</a></p></td>
            </tr>
		    <tr>
			    <td colspan="2" align="right">
				    <input id="btnLogin" type="button" value="Login"  />
                    <input type="hidden" id="txt_ccanum" value="<% if(Request.QueryString["ccanum"] != ""){ Response.Write(Request.QueryString["ccanum"]); } %>" />
                    <input type="hidden" id="txt_requestid" value="<% if(Request.QueryString["requestid"] != ""){ Response.Write(Request.QueryString["requestid"]); } %>" />
                    <input type="hidden" id="txt_ematno" value="<% if(Request.QueryString["eMATno"] != ""){ Response.Write(Request.QueryString["eMATno"]); } %>" />
                    <input type="hidden" id="txt_busReviewNo" value="<% if(Request.QueryString["busReviewNo"] != ""){ Response.Write(Request.QueryString["busReviewNo"]); } %>" />

                    <input type="hidden" id="txt_doctype" value="<% if(Request.QueryString["doctype"] != ""){ Response.Write(Request.QueryString["doctype"]); } %>" />
                    <input type="hidden" id="txt_docid" value="<% if(Request.QueryString["id"] != ""){ Response.Write(Request.QueryString["id"]); } %>" />
                    <input type="hidden" id="txt_month" value="<% if(Request.QueryString["month"] != ""){ Response.Write(Request.QueryString["month"]); } %>" />
                    <input type="hidden" id="txt_year" value="<% if(Request.QueryString["year"] != ""){ Response.Write(Request.QueryString["year"]); } %>" />
                    <input type="hidden" id="txt_soId" value="<% if(Request.QueryString["soId"] != ""){ Response.Write(Request.QueryString["soId"]); } %>" />
			    </td>
		    </tr>
	    </table>
    </div>
    <div id="redirect_page" style="display:none">
        Redirecting..
    </div>
</asp:Content>
