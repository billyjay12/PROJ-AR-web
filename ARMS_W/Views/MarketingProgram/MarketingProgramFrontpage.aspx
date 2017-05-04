<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/MarketingProg.js" type="text/javascript"></script>

      <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

    </script>


    <% 
 // QUERY FOR THE POSITION OF THE USER
        string userPosition="";
        string strquery = "SELECT position FROM userHeader WHERE userName='" + Session["username"] + "'";
        OleDbDataReader greader = SqlDbHelper.getData(strquery);
        if (greader.Read())
        {
            userPosition = greader.GetValue(0).ToString();

        }
         %>


    
    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Marketing Program</b>
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
                <td align="right">
                    <% if (userPosition == "ssg" || userPosition == "csr" || userPosition == "csm" || userPosition == "ssm")
                    { %> <a href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingPog">>>Create Marketing Program</a>  <% } %>   &nbsp;&nbsp; <a href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingProgramDetails">>>List of Marketing Program</a> &nbsp;&nbsp; <a href="<%=ResolveUrl("~/") %>MarketingProgram/SearchWindow">>>Search Marketing Program</a>
                </td>
            <!--    <td>
                     <a href="<%=ResolveUrl("~/") %>MarketingProgram/MarketingPog">>>Create New E-MAT</a>
                </td>

                 <td>
                     <a href="<%=ResolveUrl("~/") %>MarketingProgram/SearchWindow">>>Search Marketing Program</a>
                </td>-->
            </tr>
        </table>
    </div>
    </div>

   
</asp:Content>
