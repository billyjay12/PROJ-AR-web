<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>
<script runat="server">


</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    // Request.QueryString["User Profile"]
    DataTable UserProfile;
    UserProfile = SqlDbHelper.getDataDT("SELECT empIdNo, (lastName + ', ' + firstName) AS fullname, position, emailAdd, userName, userPass, status, area, territory, region  FROM userHeader WHERE empIdNo ='" + Request.QueryString["empIdNo"].ToString() + "'");

    string userName = "";
    string strquery = "SELECT userName FROM userHeader WHERE empIdNo ='" + Request.QueryString["empIdNo"].ToString() + "'";
    OleDbDataReader _greader = SqlDbHelper.getData(strquery);
    if (_greader.Read())
    {
        userName = _greader.GetValue(0).ToString();

    }

    string user = "";
    string strquery2 = "SELECT userName FROM userHeader WHERE userName='" + Session["username"] + "'";
    OleDbDataReader greader = SqlDbHelper.getData(strquery2);
    if (greader.Read())
    {
        user = greader.GetValue(0).ToString();

    }
%>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/Profile.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
            $("#tabs").tabs();
            $("#sub_tab").tabs();
             
            <% if (userName != user){%>
                  changeType();
            <% } %>
            Loadprofile();
        });

        function Loadprofile(){
            var empIdNo;
            var name;
            var position;
            var emailAdd;
            var status;
            var area;
            var territory;
            var region;
            var userName;
            var userPass;
            var passEncrypted;

            <%  foreach (DataRow row in UserProfile.Rows) { %>
                empIdNo = '<% Response.Write(row["empIdNo"].ToString().Trim()); %>';
                name = '<% Response.Write(row["fullname"].ToString().Trim()); %>';
                position = '<% Response.Write(row["position"].ToString().Trim()); %>';
                emailAdd = '<% Response.Write(row["emailAdd"].ToString().Trim()); %>';
                userName = '<% Response.Write(row["userName"].ToString().Trim()); %>';
                userPass = '<% Response.Write(row["userPass"].ToString().Trim()); %>';
                passEncrypted = '<% Response.Write(row["userPass"].ToString().Trim()); %>';
                status = '<% Response.Write(row["status"].ToString().Trim()); %>';
                area = '<% Response.Write(row["area"].ToString().Trim()); %>';
                territory = '<% Response.Write(row["territory"].ToString().Trim()); %>';
                region = '<% Response.Write(row["region"].ToString().Trim()); %>';
            <%  } %>

            $("#txt_idNo").attr('value', empIdNo);
            $("#txt_name").attr('value', name);
            $("#txt_position").attr('value', position);
            $("#txt_email").attr('value', emailAdd);
            $("#txt_username").attr('value', userName);
            $("#txt_password").attr('value', userPass);
            $("#txt_status").attr('value', status);
            $("#txt_area").attr('value', area);
            $("#txt_territory").attr('value', territory);
            $("#txt_region").attr('value', region);
            $("#txt_password_encrypted").attr('value', passEncrypted);

   
            $("#td_update").hide();
            if (position != "SUPER USER 2") {
                $("#td_update").show();
            }
        }

    </script>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                        <b>Profile</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
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

        <div class="simple_box" style="padding:15px; font-size:12px; border: 0px; width: 619px;">
            <table cellpadding="1" id="tbl_br1" cellspacing="1" border="0" style="color:#000000;" >
                <tr>
                    <td style="width: 170px;"> ID Number</td>
                    <td>
                        <input type="text" id="txt_idNo" readonly="readonly" style="width: 250px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px;"> Name </td>
                    <td>
                        <input type="text" id="txt_name" readonly="readonly" style="width: 250px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px;"> Position </td>
                    <td>
                        <input type="text" id="txt_position" readonly="readonly" style="width: 250px" />
                    </td>
                </tr> 
                <tr> 
                    <td colspan="2"><hr /></td>
                </tr>
                <tr>
                    <td style="width: 170px;"> Email Address</td>
                    <td>
                        <input type="text" id="txt_email" readonly="readonly" style="width: 250px" />
                    </td>
                </tr>
                <tr>
                    <td style="width:172px;"> Username</td>
                    <td style="width:100px;">
                        <input type="text" id="txt_username" readonly="readonly" size="26" style="width: 250px"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 172px;"> Password </td>
                    <td>
                        <input style="display:none;" type="text" id="txt_password_encrypted" readonly="readonly" style="width: 250px"/>
                        <input type="password" id="txt_password" readonly="readonly" style="width: 250px"/>
                    </td>
                    <td ><a href="#" id="decrypt_pass">decrypt</a><a href="#" id="encrypt_pass">encrypt</a></td>
                </tr>
                <tr> 
                    <td colspan="2"><hr /></td>
                </tr> 
                <tr>
                    <td style="width: 170px;"> Status </td>
                    <td id="td_status">
                        <input type="text" id="txt_status" readonly="readonly" style="width: 250px" />
                    </td>
                </tr> 
                <tr>
                    <td style="width: 170px;"> Area </td>
                    <td>
                        <input type="text" id="txt_area" readonly="readonly" style="width: 250px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px;"> Territory </td>
                    <td>
                        <input type="text" id="txt_territory" readonly="readonly" style="width: 250px" />
                    </td>
                </tr> 
                <tr>
                    <td style="width: 170px;"> Region </td>
                    <td>
                        <input type="text" id="txt_region" readonly="readonly" style="width: 250px" />
                    </td>
                </tr> 
                <tfoot>

                        <tr>
                            <td id="td_update" colspan="2" style="text-align:right"><button id="edit_button">Edit</button><button id="update_button">Update</button><button id="cancel_button">Cancel</button><button id="refresh_button">Refresh</button></td>
                        </tr>

                </tfoot>
            </table>      
        </div>
</asp:Content>
