<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="ARMS_W.Class" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MainMenus</title>
    <link href="<%=ResolveUrl("~/") %>Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Menu.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/General.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/DynamicDialogBox.css" rel="stylesheet"
        type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/reportlistindex.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/shared_site1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var DisableAllButtons = "FALSE";
        var baseUrl = "";
        var G_username = '<%: Session["username"] %>';
       // var html_menu = '<%=  ViewData["menu_builder"] %>';
         $(function () {
             baseUrl = "<%= ResolveUrl("~/") %>";
          //   $("#test").append(html_menu);
         });
    </script>
    <script src="<%=ResolveUrl("~/") %>Scripts/Menu.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/salesinfo.js" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/notification.js" type="text/javascript"></script>--%>
</head>
<body topmargin="0" rightmargin="0" leftmargin="2" bottommargin="0" style="margin: 0;
    padding-left: 5px; padding-right: 5px; padding-top: 0;">
    <div style="display: none;">
        <% 
            _User cuurent_user = (_User)Session["Ousr"];
            if (Session["username"] == "" || Session["username"] == null)
            {
                //throw new ArgumentException("Not Logged In.");
                Response.Write("<script type=\"text/javascript\"> alert(\"Not Logged In.\") </script>");
            }
            DateTime date_time = DateTime.Now;
            int month = date_time.Month - 1;
            int yr = date_time.Year;
                                    
        %>
    </div>
    <table id="TableContainer" width="100%" border="0" cellspacing="0">
        <tr>
            <td class="PContainer" valign="top">
                <table class="menu_holder" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="menu_bar" valign="top" align="left">
                            <!-- MENU START -->
                            <div>
                                <div class="menu">
                                    <div class="menu_title">
                                        Master File
                                    </div>
                                    <div class="sub_menu_holder">
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Document/Accounts" target="content_frameset">Customers</a></div>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Document/LeadAccounts" target="content_frameset">Leads</a></div>
                                    </div>
                                </div>
                                <div class="menu">
                                    <div class="menu_title">
                                        Transactions
                                    </div>
                                    <div class="sub_menu_holder">
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                Accounts
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>Document/AccountsDetailsPending" target="content_frameset">
                                                        Customer Creation Approval</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>Document/AccountsWChanges" target="content_frameset">
                                                        Customer Change Approval</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>Document/LeadAccountsDetailsPending" target="content_frameset">
                                                        Leads Creation Approval</a></div>
                                            </div>
                                        </div>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>eMAT/ListOfEMATS" target="content_frameset">E-MAT</a></div>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>LeadDb/LeadDbList" target="content_frameset">Active Leads</a></div>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Document/MMAgreements" target="content_frameset">Contracts
                                                and Agreements</a></div>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>BusinessReview/BusinessReviewList" target="content_frameset">
                                                Business Review</a></div>
                                    </div>
                                </div>
                                <% if (cuurent_user != null) if (cuurent_user.Roles.Any(o => o.Position == "SO" || o.Position == "SPRUSER" || o.Position == "CA" || o.Position == "ASM" || o.Position == "CHM" || o.Position == "VPBSM"))
                                       { %>
                                <div class="menu">
                                    <div class="menu_title">
                                        GT Inventory Tracking
                                    </div>
                                    <div class="sub_menu_holder">
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                SO Inventory Count
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <% if (cuurent_user.Roles.Any(o => o.Position == "SO" || o.Position == "ASM" || o.Position == "SPRUSER"))
                                                   { %>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Inventory/CreateNewInventoryCount" target="content_frameset">
                                                        Create New Inventory Count</a></div>
                                                <% } %>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Inventory/InventoryCountList" target="content_frameset">
                                                        List of Inventory Count</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Maintenance/OutletAssign" target="content_frameset">Inventory
                                                        Count Schedule</a></div>
                                            </div>
                                            <div class="sub_menu">
                                                <a href="<%=ResolveUrl("~/") %>ItemMasterFile/ItemMasterFile" target="content_frameset">
                                                    Item Master File</a></div>
                                            <div class="sub_menu">
                                                <a href="<%=ResolveUrl("~/") %>ItemMasterFile/ListItemsAvailable" target="content_frameset">
                                                    List of Available Inventory</a></div>
                                        </div>
                                        <% if (cuurent_user.Roles.Any(o => o.Position == "SPRUSER" || o.Position == "CA"))
                                           { %>
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                Audit Inventory Count
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Inventory/ForAuditInventoryCount" target="content_frameset">
                                                        For Audit Inventory Count</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Inventory/AuditInventoryCountList" target="content_frameset">
                                                        List of Audited Inventory Count</a></div>
                                            </div>
                                        </div>
                                        <% } %>
                                        <% if (cuurent_user.Roles.Any(o => o.Position == "SPRUSER"))
                                           { %>
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                Maintenance
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Maintenance/SalesTargetMaintenance" target="content_frameset">
                                                        Sales Target</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>MasterFile/GTAccountMasterFile" target="content_frameset">
                                                        Pareto Tagging</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Maintenance/DayCycleMaintenance" target="content_frameset">
                                                        Counting Day Cycle</a></div>
                                                <%-- <div class="sub_menu"><a href="<%=ResolveUrl("~/") %>Maintenance/OutletAssign" target="content_frameset" >Account Assignment</a></div>--%>
                                                <%-- <div class="sub_menu"><a href="<%=ResolveUrl("~/") %>Maintenance/ObjectiveMaintenance" target="content_frameset"> Objectives </a></div>--%>
                                            </div>
                                        </div>
                                        <% } %>
                                    </div>
                                </div>
                                <% } %>
                                <% if (cuurent_user != null) if (cuurent_user.Roles.Any(o => o.Position == "SO" || o.Position == "SPRUSER" || o.Position == "ASM" || o.Position == "CHM" || o.Position == "CVW" || o.Position == "VPBSM"))
                                       { %>
                                <div class="menu">
                                    <div class="menu_title">
                                        SO Monthly Coverage
                                    </div>
                                    <div class="sub_menu_holder">
                                        <% if (cuurent_user.Roles.Any(o => o.Position == "SO" || o.Position == "SPRUSER" || o.Position == "ASM" || o.Position == "CHM" || o.Position == "VPBSM"))
                                           { %>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>calendar/MyCalendar?soId=<%=cuurent_user.EmployeeIdNo %>&month=<%=month %>&year=<%=yr %>"
                                                target="content_frameset">Monthly Work Plan</a>
                                        </div>
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                Pending
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Calendar/EventListForASMapproval" target="content_frameset">
                                                        Work Plan Approval</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>calendar/ChangesForApproval" target="content_frameset">
                                                        Changes For Approval </a>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="sub_menu"><a href="<%=ResolveUrl("~/") %>Calendar/ImageUploadAttachmentView" target="content_frameset">Monthly Work Plan Image Attachment View</a></div>--%>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Calendar/EventListApproveDisapprove" target="content_frameset">
                                                Approved/Disapproved</a></div>
                                        <% } %>
                                        <% if (cuurent_user.Roles.Any(o => o.Position == "SO" || o.Position == "SPRUSER" || o.Position == "ASM" || o.Position == "CHM" || o.Position == "CVW" || o.Position == "VPBSM"))
                                           { %>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Calendar/CalendarViewer" target="content_frameset">Calendar
                                                Viewer</a></div>
                                        <% } %>
                                    </div>
                                </div>
                                <%} %>
                                <div class="menu">
                                    <div class="menu_title">
                                        Reports
                                    </div>
                                    <div class="sub_menu_holder">
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                Customer Related Reports
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/sellinbrand" target="content_frameset">Account
                                                        Management Reports</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/eMATReports" target="content_frameset">E-MAT
                                                        Reports</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/sellinreports" target="content_frameset">Sell-in
                                                        Reports</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/cm" target="content_frameset">Credit Memo</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/discount" target="content_frameset">Discount</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/salesopportunities" target="content_frameset">
                                                        Sales Opportunities</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/SalesOrderStatus" target="content_frameset">
                                                        Sales Monitoring Reports</a></div>
                                            </div>
                                        </div>
                                        <div class="menu_1">
                                            <div class="menu_title_2">
                                                <a href="<%=ResolveUrl("~/") %>ReportList/letters" target="content_frameset">Letters
                                                </a>
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu_1">
                                                    <a href="javascript:;">MTO DP Excemption Letter</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="javascript:;">Welcome Letter to Trade Partners</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="javascript:;">Quotation Template</a></div>
                                            </div>
                                        </div>
                                        <div class="menu_1">
                                            <div class="menu_title_2">
                                                <a href="<%=ResolveUrl("~/") %>ReportList/pricelist" target="content_frameset">Pricelist
                                                </a>
                                            </div>
                                        </div>
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                ARMS II Reports
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/SOcoverageplanreports" target="content_frameset">
                                                        SO Calendar Reports</a></div>
                                                <div class="sub_menu_1">
                                                    <a href="<%=ResolveUrl("~/") %>ReportList/inventoryCountreports" target="content_frameset">
                                                        Inventory Count</a></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="menu">
                                    <div class="menu_title">
                                        Frequently Asked Questions
                                    </div>
                                    <div class="sub_menu_holder">
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>UserGuide/FAQ" target="content_frameset">Volume 1</a></div>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>UserGuide/FAQ_Vol2" target="content_frameset">Volume
                                                2</a></div>
                                    </div>
                                </div>
                                <div class="menu">
                                    <div class="menu_title">
                                        Maintenance
                                    </div>
                                    <div class="sub_menu_holder">
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Config/ChangesPassword" target="content_frameset">Change
                                                Password</a></div>
                                        <% if (cuurent_user != null)
                                               if (cuurent_user.HasPositionOf("ADMIN") > -1)
                                               { %>
                                        <div class="menu_1">
                                            <div class="menu_title_1">
                                                System
                                            </div>
                                            <div class="sub_menu_holder_1">
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>UserProfile/AddNewUser" target="content_frameset">Add
                                                        User</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Config/UserRole" target="content_frameset">User Role</a></div>
                                                <div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>SystemPage/ListOfUser" target="content_frameset">List
                                                        Of User</a></div>
                                            </div>
                                        </div>
                                        <% } %>
                                    </div>
                                </div>
                                <%--	<div class="menu">
							<div class="menu_title"> Maintenance </div>
							<div class="sub_menu_holder">
								<div class="sub_menu"><a href="<%=ResolveUrl("~/") %>Config/ChangesPassword" target="content_frameset" >Change Password</a></div>
							</div>
						</div>--%>
                                <% if (cuurent_user != null) if (cuurent_user != null) if (cuurent_user.Roles.Any(o => o.Position == "ADMIN" || o.Position == "SPRUSER" || o.Position == "SIM"))//if (cuurent_user.HasPositionOf("SIM") > -1)
                                           { %>
                                <div class="menu">
                                    <div class="menu_title">
                                        Utilities
                                    </div>
                                    <div class="sub_menu_holder">
                                        
                                        <% if (cuurent_user != null) if (cuurent_user != null) if (cuurent_user.HasPositionOf("SIM") > -1)
                                                   { %>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Utilities/SalesOrderStatusUpload" target="content_frameset">
                                                Sales Order Status Uploading</a></div>
                                        <% }
                                           if (cuurent_user != null) if (cuurent_user.Roles.Any(o => o.Position == "ADMIN" || o.Position == "SPRUSER"))
                                               { %>
                                        <div class="sub_menu">
                                            <a href="<%=ResolveUrl("~/") %>Utilities/PricelistUpload" target="content_frameset">
                                                Pricelist Upload </a>
                                        </div>
                                        <%--<div class="sub_menu">
                                                    <a href="<%=ResolveUrl("~/") %>Calendar/SOMonthlyCoverageEventBatchUpload" target="content_frameset">
                                                        Monthly Coverage Batch Upload for SO </a>
                                                </div>--%>
                                        
                                        <% } %>
                                    </div>
                                </div>
                                <% } %>
                                <%--<div class="sub_menu"><a href="<%=ResolveUrl("~/") %>BusinessReview/BusinessReviewList" target="content_frameset" >Business Review</a></div>--%>
                                <%--	<% if (cuurent_user != null)
                        if (cuurent_user.HasPositionOf("ADMIN") > -1) 
                        { %>
                        <div class="menu">
							<div class="menu_title"> System Maintenance </div>
							<div class="sub_menu_holder">
                                <div class="sub_menu"><a href="<%=ResolveUrl("~/") %>UserProfile/AddNewUser" target="content_frameset" >Add User</a></div>
                                <div class="sub_menu"><a href="<%=ResolveUrl("~/") %>Config/UserRole" target="content_frameset" >User Role</a></div>
								<div class="sub_menu"><a href="<%=ResolveUrl("~/") %>SystemPage/ListOfUser" target="content_frameset" >List Of User</a></div>
							</div>
						</div>
                        <% } %>--%>
                            </div>
                            <!-- MENU END -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tblSalesInfo" style="display: none">
            <td>
                <fieldset>
                    <legend><b>My Sales Info</b></legend>
                    <div>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    Sales Update as of
                                    <label id="lbldateasof">
                                        N/A</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Gross Sales:
                                </td>
                                <td>
                                    <span id="lblGrossSales" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Credit Memo:
                                </td>
                                <td>
                                    <span id="lblCM" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Unposted Sales:
                                </td>
                                <td>
                                    <span id="lblUnpostedSales" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Posted Sales:
                                </td>
                                <td>
                                    <span id="lblPostedSales" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pending:
                                </td>
                                <td>
                                    <span id="lblPending" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Balance Order:
                                </td>
                                <td>
                                    <span id="lblBalanceOrder" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Net Posted Sales:
                                </td>
                                <td>
                                    <span id="lblNetPostedSales" class="field_amount">N/A</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. of Transacting Accts:
                                </td>
                                <td style="text-align: right">
                                    <span id="lblNoTransactingAccts">N/A</span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
</body>
</html>
