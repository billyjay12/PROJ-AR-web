<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/UserRole.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/UserRole.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

    <% 
        string str_searchfilter = "";
        string username_f = "", rolename_f = "";
        
        if (Request.QueryString["username"] != null && Request.QueryString["username"].ToString() != "")
            username_f = Request.QueryString["username"].ToString();

        if (Request.QueryString["rolename"] != null && Request.QueryString["rolename"].ToString() != "")
            rolename_f = Request.QueryString["rolename"].ToString();

        if (username_f != "") 
        {
            str_searchfilter = " and b.username like '%" + username_f + "%' ";
        }

        if (rolename_f != "")
        {
            // str_searchfilter = " and b.username like '%" + username_f + "%' ";
        }
        
    %>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>User Roles</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:15px; font-size:12px;">
        
        <div id="div_add_role" style="position:absolute; display:none; padding:5px; background:#cdcdcd;">
        <table cellpadding="3" cellspacing="1" border="0" style="background:#ffffff;" >
            <tr>
                <td>Role ID</td>
                <td>
                    <input type="text" id="txt_roleid" onclick="javascript:LookUpData('txt_roleid', 'ListOfRoleId_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>User Name</td>
                <td>
                    <input type="text" id="txt_username" onclick="javascript:LookUpData('txt_username', 'ListOfUserHeader_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <input type="text" id="txt_email" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>For SO and ASM [SplCode]</td>
                <td>
                    <input type="text" id="txt_slpcode" onclick="javascript:LookUpData('txt_slpcode', 'ListOfSlpcode_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Brand</td>
                <td>
                    <input type="text" id="txt_brand" onclick="javascript:LookUpData('txt_brand', 'ListOfBrand_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Region</td>
                <td>
                    <input type="text" id="txt_region" onclick="javascript:LookUpData('txt_region', 'ListOfRegion_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Channel</td>
                <td>
                    <input type="text" id="txt_channel" onclick="javascript:LookUpData('txt_channel', 'ListOfChannels_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>Area</td>
                <td>
                    <input type="text" id="txt_area" onclick="javascript:LookUpData('txt_area', 'ListOfArea_MOD');" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="button" value="Cancel" onclick="javascript:HideAddDialog();" />
                    &nbsp;&nbsp;/&nbsp;&nbsp;
                    <input type="button" value="Add" onclick="javascript:AddNeWuserRole();" />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
        </table>
        </div>
        <!--
            List of AppvrDesig
        -->
        <% 
            string strQuery = @"
                select a.roleid, (select rolename from apprvrRole where roleid=a.roleid) as 'rolename', b.username, a.email, 
                case when a.branch = 'LZ' then 'LUZON' when a.branch = 'VM' then 'VISMIN' else '' end as 'region', 
                a.brand, a.channel, left(a.area,5) as 'area', a.desigID from apprvrDesig a, userHeader b 
                where a.counterid=b.counterid " + str_searchfilter + @"
                order by b.username, a.channel, a.area ";
                
            DataTable appvrDesig = SqlDbHelper.getDataDT(strQuery);
        %>
        <table cellpadding="5" cellspacing="0" border="0" style="font-size:11px;" >
            <tr>
                <td colspan="8" align="right" >
                    Username: <input type="text" id="txt_search_uname" value="<%: username_f %>" />
                    <input type="button" value="Search" onclick="javascript:SearchUsername();" />
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <input type="button" value="Add New Role" onclick="javascript:ShowAddDialog();" />
                </td>
            </tr>
            <tr>
                <td style="background:#cdcdcd;" align="center"></td>
	            <td style="background:#cdcdcd;" align="center"><b>User Name</b></td>
                <td style="background:#cdcdcd;" align="center"><b>Role Name</b></td>
	            <td style="background:#cdcdcd;" align="center"><b>Region</b></td>
	            <td style="background:#cdcdcd;" align="center"><b>Brand</b></td>
	            <td style="background:#cdcdcd;" align="center"><b>Channel</b></td>
	            <td style="background:#cdcdcd;" align="center"><b>Area</b></td>
            </tr>
            <% 
                int icounter = 1;
                string str_style = "";
                string str_data_style = "";
                foreach (DataRow itm in appvrDesig.Rows) 
                {
                    if (icounter % 2 == 0) str_style = "style=\"background:#f4f2f2;\"";
                    else str_style = "";

                    Response.Write("<tr " + str_style + ">");
                    Response.Write("<td " + str_data_style + "><a href=\"javascript:DeleteUserRole(" + itm["desigID"].ToString() + ");\">delete</a></td>");
                    Response.Write("<td " + str_data_style + " align=\"left\">" + itm["username"].ToString() + "</td>");
                    Response.Write("<td " + str_data_style + " align=\"left\">" + itm["rolename"].ToString() + "</td>");
                    Response.Write("<td " + str_data_style + " align=\"left\">" + itm["region"].ToString() + "</td>");
                    Response.Write("<td " + str_data_style + " align=\"left\">" + itm["brand"].ToString() + "</td>");
                    Response.Write("<td " + str_data_style + " align=\"left\">" + itm["channel"].ToString() + "</td>");
                    Response.Write("<td " + str_data_style + " align=\"left\">" + itm["area"].ToString() + "</td>");
                    Response.Write("</tr>");

                    icounter = icounter + 1;
                }
            %>
            </table>
            </div>
    </div>
    </div>

</asp:Content>
