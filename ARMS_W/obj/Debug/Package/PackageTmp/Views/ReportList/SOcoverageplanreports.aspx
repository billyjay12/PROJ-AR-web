<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%=ResolveUrl("~/") %>Content/ReportList.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlist_arms2reports.js" type="text/javascript"></script>
    <% 
    _User cuurent_user = (_User)Session["Ousr"];
    if (Session["username"] == "" || Session["username"] == null) 
    { 
        //throw new ArgumentException("Not Logged In.");
        Response.Write("<script type=\"text/javascript\"> alert(\"Not Logged In.\") </script>");
    }
                                    
        %> 
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

     <div class="bl_box">

        <div class="page_header">
             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>ARMS II reports</b>
                    </td>
                    <td align="right" valign="middle" >
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                 </tr>
              </table>
       </div>

    <div class="simple_box" >    
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('SOMTDPerformance');">SO MTD Performance</a>
        </div>
        <div class="report_menu" >
            <a href="javascript:ShowReportViewer('SalesTargetDistribution');">Sales Target Distribution</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('SOMonthlyWorkPlan');">SO Monthly Work Plan and Itinerary</a>
        </div>
         <div class="report_menu">
            <a href="javascript:ShowReportViewer('CustomerCallEfficiency');">Customer Call Efficiency</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('DetailedCallReport');">Detailed Call Report</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('ActivityLogReport');">Activity Log Report</a>
        </div>
        
        
         <% if (cuurent_user.Roles.Any(o => o.Position == "FNM" || o.Position == "SPRUSER" || o.Position == "CNC"))
            { %>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('CallReportCollection');">Collection Call Report</a>
        </div>
        <% } %>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('ARMSNewsFeedReport');">ARMS News Feed Report</a>
        </div>
         <div class="report_menu">
            <a href="javascript:ShowReportViewer('CallReportLocations');">Call Report Locations</a>
        </div>
        <div class="report_menu">
            <a href="javascript:ShowReportViewer('CoveragePlanNoOfAccounts');")>No. of Accounts</a>
        </div>
    </div>
    </div>

</asp:Content>
