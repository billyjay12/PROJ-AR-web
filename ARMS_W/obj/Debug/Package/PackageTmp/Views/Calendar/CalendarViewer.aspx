<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>

<link href="<%=ResolveUrl("~/") %>Content/fullcalendar1.css" rel="stylesheet" type="text/css" />
<link href="<%=ResolveUrl("~/") %>Content/fullcalendar1.print.css" rel="stylesheet" type="text/css" media="print" />
<link href="<%:ResolveUrl("~/") %>Content/theme.css" rel="stylesheet" type="text/css" />
<link href="<%:ResolveUrl("~/") %>Content/CalendarViewer.css" rel="stylesheet" type="text/css" />

<link href="<%=ResolveUrl("~/") %>Content/jquery.fancybox.css" rel="stylesheet" type="text/css" />

<script src="<%=ResolveUrl("~/") %>Scripts/jquery.fancybox.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/fullcalendar.min.js" type="text/javascript"></script>

<script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/") %>Scripts/calendar_calendarviewer.js" type="text/javascript"></script>

    <div class="bl_box">
            <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>Calendar Viewer</b>
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
                      <td align="left" valign="middle" class="style2">
                        <a id="btn_menu" href="javascript:;" ><img alt="" src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU </a>
                     </td>
                </tr>
            </table>
            </div>
            <div  class="simple_box">
            <div style="position:relative"  >
                <table border="0" width="100%">
                <tr>
                    <td style="width:200px; vertical-align:top;">
                        <table style="width:100%">
                            <tr>
                                <td><span class="arrow arrow-left" id="btn_prev_year"></span></td>
                                <td><span class="arrow arrow-right" id="btn_next_year"></span></td>
                                <td><span id="txt_year" class="preventhightlight"><%= DateTime.Now.Year.ToString() %></span></td>
                            </tr>
                        </table>
                        <hr />
                        <table style="width:100%;" class="choose_months" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td value="0">JAN</td>
                                    <td value="1">FEB</td>
                                    <td value="2">MAR</td>
                                </tr>
                                <tr>
                                    <td value="3">APR</td>
                                    <td value="4">MAY</td>
                                    <td value="5">JUN</td>
                                </tr>
                                <tr>
                                    <td value="6">JUL</td>
                                    <td value="7">AUG</td>
                                    <td value="8">SEP</td>
                                </tr>
                                <tr>
                                    <td value="9">OCT</td>
                                    <td value="10">NOV</td>
                                    <td value="11">DEC</td>
                                </tr>
                        </table>
                        <hr />
                        <table>
                             <tr>
                                <td>Employee ID: <span id="lbl_employeeid">(Choose Emp ID)</span> <label><img src="<%=ResolveUrl("~/") %>Images/magnifier.png" id="btn_search_empId" /></label></td>
                            </tr>
                            <tr>
                                <td>Employee Name: <span id="lbl_employeename"></span></td>
                            </tr>
                        </table>
                        <fieldset>
                            <legend>Calendar Info</legend>
                            <div>
                                <table>
                                    <tr>
                                        <td>Calendar No: <span id="lbl_calendarno"></span></td>
                                    </tr>
                                    <tr>
                                        <td>Status: <span id="lbl_status"></span></td>
                                    </tr>
                                    <tr>
                                        <td>Route Changes: <a href="javascript:getRouteChanges();">View</a></td>
                                    </tr>
                                    <tr>
                                        <td>No of Events: <span id="lbl_noOfEvents">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>No of Visits: <span id="lbl_NoOfVisit">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>No of Planned Events: <span id="lbl_noOfPlannedEvents">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>No of Unplanned Events: <span id="lbl_noOfUnplannedEvents">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>No of Edited Events: <span id="lbl_editedevents">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Call Planning Effectiveness: <span id="lbl_totalCallEffective">   </span>/<span id="lbl_totalEvents"></span>(<span id="lbl_averageCallEffective"></span>)</td>
                                    </tr>
                                </table>
                                
                            </div>
                        </fieldset>
                        <table>
                            <tr>
                                <td class="legend-header preventhightlight">Legends:</td>
                            </tr>
                            <tr>
                                <td><div class="legend legend-planned preventhightlight">Planned</div></td>
                            </tr>
                            <tr>
                                <td><div class="legend legend-unplanned preventhightlight">Unplanned</div></td>
                            </tr>
                            <tr>
                                <td><div class="legend legend-edited preventhightlight">Edited</div></td>
                            </tr>
                            <tr>
                                <td><div class="legend legend-visited preventhightlight">Visited</div></td>
                            </tr>
                            <tr>
                                <td><div class="legend legend-invtycount preventhightlight">For Invty Count</div></td>
                            </tr>
                            <tr>
                                <td><div class="legend legend-deleted preventhightlight">Deleted</div></td>
                            </tr>
                        </table>
                        <hr />
                       <%-- <fieldset>
                            <legend><b>Sales Info</b></legend>
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            Sales Update as of <label id="lbldateasof">N/A</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Gross Sales: <span id="lblGrossSales">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>CM: <span id="lblCM">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Unposted Sales: <span id="lblUnpostedSales">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Posted Sales: <span id="lblPostedSales">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pending: <span id="lblPending">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Balance Order: <span id="lblBalanceOrder">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Net Posted Sales: <span id="lblNetPostedSales">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>No. of Transacting Accts: <span id="lblNoTransactingAccts">N/A</span></td>
                                    </tr>
                                </table>
                                
                            </div>
                        </fieldset>--%>
                         <fieldset>
                            <legend><b>Sales Info</b></legend>
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="2">Sales Update as of <label id="lbldateasof">N/A</label></td>
                                    </tr>
                                    <tr>
                                        <td>Gross Sales:</td>
                                        <td><span id="lblGrossSales" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Credit Memo:</td>
                                        <td><span id="lblCM" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Unposted Sales:</td>
                                        <td><span id="lblUnpostedSales" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Posted Sales:</td>
                                        <td><span id="lblPostedSales" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Pending:</td>
                                        <td><span id="lblPending" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>Balance Order:</td>
                                        <td> <span id="lblBalanceOrder" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Net Posted Sales:</td>
                                        <td><span id="lblNetPostedSales" class="field_amount">N/A</span></td>
                                    </tr>
                                    <tr>
                                        <td>No. of Transacting Accts:</td>
                                        <td  style="text-align:right"><span id="lblNoTransactingAccts">N/A</span></td>
                                    </tr>
                                </table>
                                
                            </div>
                        </fieldset>
                    </td>
                    <td style="vertical-align:top"><div id="calendar"></div></td>
                </tr>
                </table>
            </div>
            </div>
    </div>
       <div id="dialog_transaction_log_box" style="display:none;">
            
        </div>
       
        <div id="dialog_box" style="display:none;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr style="height:30px">
                    <td colspan="4">[ <span id="txt_date"></span> ]</td>
                </tr>
                <tr>
                    <td>Account Code: </td>
                    <td><input type="text" id="txt_acct_code"  readonly="readonly"/></td>
                    <td>Account Name: </td>
                    <td><input type="text" id="txt_acct_name" readonly="readonly" /></td>
                </tr>
                <tr>
                    <td>Contact Person: </td>
                    <td><input type="text" id="txt_cntct_person" readonly="readonly"/></td>
                    <td>Contact Person No: </td>
                    <td><input type="text" id="txt_cntct_person_no" readonly="readonly" /></td>
                </tr>
                <tr>
                    <td>Hotel Name: </td>
                    <td><input type="text" id="txt_hotel_name" /></td>
                    <td>Hotel Number: </td>
                    <td><input type="text" id="txt_hotel_num" /></td>
                </tr>
                <%--<tr>
                    <td>
                        Store Checking:
                    </td>
                    <td  colspan="3"> <textarea rows="2" cols="2" id="txt_store_checking" readonly="readonly"></textarea></td>
                </tr>
                <tr>
                    <td>
                        Issues and Concerns:
                    </td>
                    <td colspan="3"><textarea rows="2" cols="2" id="txt_issues_concerns"></textarea></td>
                </tr>--%>
            </table>
            <%--<table id="tbl_objectives" border="0" cellpadding="1" cellspacing="2">
                <tr>
                    <th>Objective Code</th>
                    <th>Counter Clerk</th>
                    <th>Counter Clerk No</th>
                    <th>Product Presented</th>
                    <th>Brand</th>
                    <th>Planned Amount</th>
                </tr>
                <tr class="last_row">
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                    <td><input type="text" readonly="readonly" /></td>
                </tr>
                </table>--%>
            <br />
            <div id="tab_main">
                <ul>
		            <li id="collection"><a href="#tabs-1">Collection</a></li>
		            <li id="merchandise"><a href="#tabs-2">Merchandise</a></li>
		            <li id="sales"><a href="#tabs-3">Sales</a></li>
                    <li id="customerservice"><a href="#tabs-4">Customer Service</a></li>
	            </ul>
                    <div id="tabs-1" style="height:200px; overflow:scroll">
		            <!-- TAB-1 CONTENT START -->
                        <table id="tbl_collection_dtls" cellpadding="0" cellspacing="0">
                            <tr align="center" style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px;font-weight:bold;">
                                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;" width="1px"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Brand</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Amount</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;" width="10px"></td>
                            </tr>

                            <tr class="last_row">
                                <td class="hiddenTd"></td>
                                <td><input type="text" id="txt_collectBrand" readonly="readonly"  /></td>
                                <td><input type="text" id="txt_collectAmount" class="auto" /></td>
                                <td><img alt="" src="<%=ResolveUrl("~/") %>Images/add.png" class="btn_add" /></td>
                                <td></td>
                            </tr>
                        </table>
	                </div>
		            <!-- TAB-1 CONTENT END -->
                    <!-- TAB-2 CONTENT START -->
	                <div id="tabs-2" style="height:200px; overflow:scroll" >
                        <table id="tbl_merchandise">
                            <tr>
                                <td>Store Checking</td>
                            </tr>
                            <tr>
                                <td><textarea rows="2" cols="2" style="width: 335px; height: 103px" id="txt_storechecking"></textarea></td>
                            </tr>
                        </table> 
                        <table id="tbl_mse_details" cellpadding="0" cellspacing="0">
                            <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center; font-weight:bold;">
                                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Product Presented</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Counter Clerk</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Mobile No.</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;" width="10px;"></td>
                            </tr>
                            <tr class="last_row">
                                <td class="hiddenTd"></td>
                                <td><input type="text" id="txt_mse_productpresented"/></td>
                                <td><input type="text" id="txt_mse_counterclerk"/></td>
                                <td><input type="text" id="txt_mse_mobileno"/></td>
                                <td><img alt="" src="<%=ResolveUrl("~/")%>Images/add.png" class="btn_add" /></td>
                            <td width="10px;"></td>
                            </tr>
                        </table>
	                </div>
                    <!-- TAB-2 CONTENT END -->
                    <!-- TAB-3 CONTENT START -->
	                <div id="tabs-3" style="height:200px; overflow:scroll">
                        <table id="tbl_sales_dtls" cellpadding="0" cellspacing="0">
                            <tr  style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center; font-weight:bold;">
                                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Brand</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Amount</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;" width="1px;"></td>
                            </tr>
                            <tr class="last_row">
                                <td class="hiddenTd"></td>
                                <td><input type="text" id="txt_salesBrand" readonly="readonly" /></td>
                                <td><input type="text" id="txt_salesAmount" class="auto" /></td>
                                <td><img alt="" src="<%=ResolveUrl("~/") %>Images/add.png" class="btn_add" /></td>
                                <td></td>
                            </tr>
                        </table>
	                </div>
                    <!-- TAB-3 CONTENT END -->
                    <!-- TAB-4 CONTENT START -->
	                <div id="tabs-4" style="height:200px; overflow:scroll">
                    <table id="tbl_custService">
                        <tr>
                            <td>Issues and Concerns</td>
                        </tr>
                        <tr>
                            <td><textarea rows="2" cols="2" style="width: 335px; height: 103px" id="txt_issue_concern"></textarea></td>
                        </tr>
                    </table>     
	                </div>
                    <!-- TAB-4 CONTENT END -->
            </div>
        </div>
        <div id="grp_upload_buttons" style="width:100%; text-align:center; display:none;">
            <input type="button" id="btn_save_upload" value="Save upload" />
            <input type="button" id="btn_cancel_upload" value="Cancel upload" />
        </div>
</asp:Content>
