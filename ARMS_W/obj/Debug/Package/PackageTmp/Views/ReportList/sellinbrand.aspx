<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        _User Ousr = new _User(Session["username"].ToString());
        const int IS_NOT_FOUND = -1;
    %>
    <link href="<%=ResolveUrl("~/") %>Content/ReportList.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlistindex.js" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlist_arms2reports.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>ACCOUNT MANAGEMENT REPORTS</b>
                    </td>
                    <td align="right" valign="middle" >
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>

        <div class="page_header_y">
            &nbsp;
        </div>

        <div class="simple_box">    
            <!--
            <div class="report_menu">
                <a>Customer Information Sheet</a>
            </div> -->
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/acctclassfcation.aspx');">Account Classification</a>
            </div>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/ListOfCustomerAccounts.aspx');">List Of Customers</a>
            </div>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/busrev.aspx');">Business Review Update</a>
            </div>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/meetagree.aspx');">Meeting & Agreement</a>
            </div>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/ListOfPendingTrans.aspx');">List of Pending Account Transactions for Approval</a>
            </div>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/List-Of-Approved-Accounts.aspx');">List of Approved Accounts</a>
            </div>
            
            <div class="report_menu">
                <a href="javascript:ShowReportViewer('CustomerCreditLine');")>Customer Credit Line</a>
            </div>
            
            <div class="report_menu">
                <a href="javascript:ShowReportViewer('CustomerCreationLeadTime');">ARMS Code Creation Lead Time</a>
            </div>
            
            <div class="report_menu">
                <a href="javascript:ShowReportViewer('SapApprovalMonitoring');">SAP Approval Monitoring</a>
            </div>
            <div class="report_menu">
                <a href="javascript:ShowReportViewer('ArmsApprovalMonitoring');">ARMS SAP Approval Monitoring</a>
            </div>
            
            
            <!-- TEST -->
            <!--
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/acctclassfcation.aspx');">TESTING</a>
            </div>
            -->
            <!-- TEST -->
            <% if (Ousr.HasPositionOf("cnc") != IS_NOT_FOUND || Ousr.HasPositionOf("fnm") != IS_NOT_FOUND)
               {%>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/CredInvestigation.aspx');">Credit Investigation Report</a>
            </div>
            <div class="report_menu">
                <a href="javascript:LoadReport('', 'CS_REPORT/ListOfCreatedCustomer.aspx');">PCA Report</a>
            </div>
            <% } %>
        </div>
    </div>
</asp:Content>
