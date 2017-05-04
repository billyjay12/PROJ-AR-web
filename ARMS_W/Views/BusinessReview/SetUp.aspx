<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        _User CurrentUser = new _User(Session["username"].ToString());
        const int IS_NOT_FOUND = -1;
        
        string roles = "";
        if (CurrentUser.HasPositionOf("vpbsm") != IS_NOT_FOUND || CurrentUser.HasPositionOf("ssm") != IS_NOT_FOUND || CurrentUser.HasPositionOf("ca") != IS_NOT_FOUND || CurrentUser.HasPositionOf("chm") != IS_NOT_FOUND || CurrentUser.HasPositionOf("ssgm") != IS_NOT_FOUND)
        {
            roles = "vpbsm";
        }
    %>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/") %>Scripts/SetUpBusinessReview.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
   

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";
    
        $(function () {
	        $("#txt_br_date").datepicker();
            $("#txt_encoded_by").attr('value', '<%: Session["InputedUname"] %>');
            $("#txt_acctCode").addClass("required_fields");
		    $("#txt_br_date").addClass("required_fields");
            $("#txtchannel").addClass("required_fields");
        });

    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Business Review</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %>- <a href="<%=ResolveUrl("~/") %>Home/Logout">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="page_header_y">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="right">
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>BusinessReview/BusinessReviewList">>> List of Business Review</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:10px; font-size:12px; border: 0px; width: 619px;"> 
        <table align="right" style="width: 0px" >
            <tr>
                <td colspan="5" align="right" valign="top">
                    <span id=""></span>
                </td>
            </tr>
         </table>
     </div>

    <div class="simple_box" style="padding:10px; font-size:12px; border: 0px; width: 619px;">
		<table cellpadding="1" id="tbl_br1" cellspacing="1" border="0" style="color:#000000;" >
           <tr>
                 <td style="width: 170px;"> Business Review Date </td>
                 <td>
		            <input type="text" id="txt_br_date" readonly="readonly" style="width: 300px" />
                 </td>
            </tr>
            <tr> 
                <td colspan="2"><hr /></td>
            </tr>
            <tr>
                <td style="width:172px;"> Channel </td>
                <td style="width:100px;"><input type="text" id="txtchannel" size="26" onclick="javascript:LookUpData('txtchannel','ListOfChannel');"  style="width: 300px"/></td>
            </tr>
            <tr>
                <td style="width:172px;"> Account Code</td>
                <td style="width:100px;"><input type="text" id="txt_acctCode" size="26" onclick="javascript:LookUpData('txt_acctCode','ListOfDirAcccod');"  style="width: 300px"/></td>
            </tr>
            <tr>
                <td style="width: 172px;"> Account Name </td>
                <td>
		            <input type="text" id="txt_acctName" readonly="readonly" style="width: 300px"/>
                </td>
            </tr>
            <tr> 
                <td colspan="2"><hr /></td>
            </tr>
            <tr>
                <td style="width: 170px;"> Account Officer</td>
                <td>
                    <input type="text" id="txt_acctOfficer" readonly="readonly" style="width: 300px" />
                </td>
            </tr>
            <tr>
                <td style="width: 170px;"> Area Sales Manager </td>
                <td>
		            <input type="text" id="txt_salesManager" readonly="readonly" style="width: 300px"/>
                </td>
            </tr> 
            <tr>
                <td style="width: 170px;"> Channel Manager </td>
                <td>
		            <input type="text" id="txt_channelManager" readonly="readonly" style="width: 300px"/>
                </td>
            </tr> 
          </table>      
    </div>

    <div>
        <div class="simple_box" id="Div1" style="border-style: none; border-color: inherit; border-width: 0px; padding: 10px; font-size:12px; width: 619px;">
            <table cellpadding="1" id="Table2" cellspacing="1" border="0" style="color:#000000;" >
                <tr>
                    <td style="width: 170px;"> Scheduled By:</td>
                    <td><input type="text" id="txt_encoded_by" readonly="readonly" style="width: 300px" /></td>
                </tr>
            </table>
        </div>
            
            <% if (roles == "vpbsm") { %>
            <div class="simple_box" style="margin-left:10px; margin-bottom:10px; margin-right:10px;"><%--style="padding:10px; font-size:12px; width: 619px; height:20px; "--%>
                <center>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" value="Save" onclick="javascript:SetUpBusRevDoc();" />
                    <input type="button" value="Cancel" onclick="javascript:Cancel();" />
                </center>
            </div>
            <% } %>
    </div>
    </div>
</asp:Content>
