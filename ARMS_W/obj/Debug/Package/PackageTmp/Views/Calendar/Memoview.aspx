<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- <h2>Memoview</h2>--%>

   <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
   <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />
   <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("~/") %>Scripts/memoview.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("~/") %>Scripts/1.10.3/ui/jquery-ui.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("~/") %>Scripts/ui/timekeepingjavascript.js" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>

   <script src="<%=ResolveUrl("~/") %>Scripts/autoNumeric.js" type="text/javascript"></script>


       <% 
        string Eventdate=ViewData["date"].ToString();
        string Eventday = ViewData["day"].ToString();
        string Eventmonth = ViewData["month"].ToString();
        string Eventyear = ViewData["year"].ToString();
        string soId = ViewData["soId"].ToString();
        string acctCode = ViewData["acctCode"].ToString();
        
    %>
    

   <script type="text/javascript" language="javascript">
        var Eventdate = "<%: Eventdate %>";
        var Eventday = "<%: Eventday %>";
        var Eventmonth = "<%: Eventmonth %>";
        var Eventyear = "<%: Eventyear %>";
        var soId = "<%: soId %>";
        var qrystring_acctCode = "<%: acctCode %>";
       
        var baseUrl = "<%= ResolveUrl("~/") %>";
        
        $(function () {
			$("#vwcoveragetabs").tabs();
            $("#headers_tab").tabs();
            $("#vwcallreporttab").tabs();
            $("#tabs_assign").tabs();
			$("#sub_tab").tabs({
				select: function (event, ui) {
				}
			});
        });
   </script>


    <style type="text/css">
        input[readonly] { background-color:#F0F0F0;  color:#303030 ; }
    
        .tracker {border:2px solid #0094ff; border-top:20px;}
        .tracker h2 {background:#0094ff; color:white; padding:10px;  vertical-align:top;}
        .tracker p {color:#333;padding:10px;}
        .tracker {
            -moz-border-radius-topright:5px;
            -moz-border-radius-topleft:5px;
            -webkit-border-top-right-radius:5px;
            -webkit-border-top-left-radius:5px;
        }
        .tabcolor > a {   background-color:#FFD700; }
         .margin {  margin-right:20px; }
    </style>    





   <div class="bl_box" style="height:inherit; width:100%;">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Coverage Plan</b>
                </td>
            </tr>
        </table>
    </div>

    <div id="headers_tab" style="width:99%; <%--height:100%;--%> <%--overflow:scroll;--%>">

    <ul>
		<li><a href="#vwcoveragetabs">Coverage</a></li>
		<li><a href="#vwcallreporttab">Call Report</a></li>
	</ul>
     
    <input type="hidden" id="txt_Eventday" /> &nbsp;&nbsp;&nbsp;<input type="hidden" id="txt_Eventmonth" />&nbsp;&nbsp;&nbsp;<input type="hidden" id="txt_Eventyear" />

	<div id="vwcoveragetabs" style="width:98%">
     <!--ADDED--->
     <div>
         <table id="tbl_header_info">
            <tr>
                <td>Account Code</td>
                <td ><input type="text" id="txt_vw_acctcode" style="width:184px;" readonly="readonly" /></td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;Contact Person</td>
                <td><input type="text" id="txt_vw_contactPerson" style="width:184px;" /><input type="hidden" id="txt_hidden_eventId"/></td>
            </tr>
            <tr>
                <td>Account Class</td>
                <td ><input type="text" id="txt_vw_accountClass" style="width:184px;"   readonly="readonly"/></td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;Contact Number</td>
                <td><input type="text" id="txt_vw_ContactNumber" style="width:184px;"   /><input type="hidden" id="txt_hidden_linenum"/></td>
            </tr>
            <tr>
                <td>Account Name</td>
                <td ><input type="text" id="txt_vw_accountName" style="width:184px;"  readonly="readonly"/></td>
                <td></td>
                <td style="white-space:nowrap;">&nbsp;&nbsp;&nbsp;Hotel Name</td>
                <td><input type="text" id="txt_vw_hotelname" style="width:184px;"   /></td>
            </tr>
             <tr>
                 <td valign="top">Account Address</td>
                 <td colspan="2"><textarea  cols="2" rows="2" id="txt_vw_accountAddress" style="width: 183px; height: 46px" readonly="readonly"></textarea></td>
                 <td valign="top" style="white-space:nowrap;">&nbsp;&nbsp;&nbsp;Hotel Contact Number</td>
                 <td valign="top"><input type="text" id="txt_vw_hotel_contact" style="width:184px;"   /></td>
             </tr>
         </table>
     </div>

     <!--END ADDED-->
	<ul>
		<li id="vwcollection"><a href="#vwcoveragetabs-1">Collection</a></li>
		<li id="vwmsde"><a href="#vwcoveragetabs-2">Merchandise</a></li>
		<li id="vwsales"><a href="#vwcoveragetabs-3">Sales</a></li>
        <li id="vwcs"><a href="#vwcoveragetabs-4">Customer Service</a></li>
        <li id="vwinventory"><a href="#vwcoveragetabs-5">Inventory</a></li>
	</ul>
	<div id="vwcoveragetabs-1" class="margin">
	<!-- TAB-1 CONTENT START -->
           <table id="tbl_collection_dtls" cellpadding="0" cellspacing="0">
                <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center;">
                    <th style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"><span style="white-space:nowrap">TRF Number</span></th>
                    <th style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"><span style="white-space:nowrap">Actual Date Delivery</span></th>
                    <th style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"><span style="white-space:nowrap">Due Date</span></th>
                    <th style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"><span style="white-space:nowrap">Total Amount for Collection</span></th>
                </tr>
                <tr class="last_row">
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td align="right">Total: </td>
                    <td><input type="text" readonly="readonly" id="txt_totalcollection" class="fld_amount"/></td>
                </tr>
            </table>
	<!-- TAB-1 CONTENT END -->
	</div>
    <!-- TAB-2 CONTENT START -->
	<div id="vwcoveragetabs-2" class="margin">
        <table id="tbl_merchandise">
            <tr>
                <td>Store Checking</td>
            </tr>
            <tr>
                <td><textarea rows="2" cols="2" style="width: 335px; height: 103px" id="txt_vw_storechecking"></textarea></td>
            </tr>
        </table>
        <table id="tbl_vwmse_details" cellpadding="0" cellspacing="0">
            <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center;">
                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Product Presented</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Counter Clerk</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Mobile No.</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;" width="1px;"></td>
            </tr>
            <tr class="last_row">
                <td class="hiddenTd"></td>
                <td><input type="text" id="txt_vw_mse_productpresented"/></td>
                <td><input type="text" id="txt_vw_mse_counterclerk"/></td>
                <td><input type="text" id="txt_vw_mse_mobileno"/></td>
                <td width="1px;"></td>
            </tr>
        </table>
	</div>
    <!-- TAB-2 CONTENT END -->

    <!-- TAB-3 CONTENT START -->
	<div id="vwcoveragetabs-3" class="margin">
        <table id="tbl_vwsales_dtls" cellpadding="0" cellspacing="0">
            <tr align="center" style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center; font-weight:bold;">
                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Brand</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Amount</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Details</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;" width="1px;"></td>
            </tr>
            <tr class="last_row">
                <td class="hiddenTd"></td>
                <td><input type="text" id="txt_vw_salesBrand" /></td>
                <td><input type="text" id="txt_vw_salesAmount" /></td>
                <td><input type="text" id="txt_vw_details" /></td>
                <td></td>
            </tr>
    
            <tr style="height:1px;">
                <td></td>
            </tr>
            <tr>
                <td class="hiddenTd"></td>
                <td align="right"><b>Total:</b></td>
                <td><input type="text" id="txt_vw_sales_total" value="0" readonly="readonly" /></td>
            </tr>
        </table>
	</div>
    <!-- TAB-3 CONTENT END -->

    <!-- TAB-4 CONTENT START -->
	<div id="vwcoveragetabs-4" class="margin">
        <table id="tbl_custService">
            <tr>
                <td>Issues and Concerns</td>
            </tr>
            <tr>
                <td><textarea style="width: 335px; height: 103px" id="txt_wv_issue_concern"></textarea></td>
            </tr>
        </table>    
	</div>
    <!-- TAB-4 CONTENT END -->

    <!-- TAB-5 CONTENT START -->
	<div id="vwcoveragetabs-5" style="height:505px;">
        <p>
        <span id="create_inventory_count_link"></span>
        <br />
        <span id="view_inventory_count_link"></span>
        </p>
        
	</div>
    <!-- TAB-5 CONTENT END -->

	 &nbsp;</div>



    <div id="vwcallreporttab" style="width:98%;">
    <table>
        <tr>
            <td>Unplanned</td>
            <td><input type="checkbox" id="chk_isunPlanned" /></td>
        </tr>
    </table>

    <table id="tbl_headerCallrep" >
        <tr>
            <td>Account Code</td>
            <td ><input type="text" id="txt_vwcr_accountCode" style="width:184px;" readonly="readonly" /></td>
      
            <td>&nbsp;&nbsp;&nbsp;Contact Person</td>
            <td>&nbsp;&nbsp;&nbsp;<input type="text" id="txt_vwcr_contactperson" style="width:184px;"   /><input type="hidden" id="txt_hidden_cr_soId" /><input type="hidden" id="txt_hiden_cr_eventId" /></td>
        </tr>
        <tr>
            <td>Account Class</td>
            <td ><input type="text" id="txt_vwcr_accountclass" style="width:184px;"   readonly="readonly"/></td>
      
            <td>&nbsp;&nbsp;&nbsp;Contact Number</td>
            <td>&nbsp;&nbsp;&nbsp;<input type="text" id="txt_vwcr_contactpersonNo" style="width:184px;"   /><input type="hidden" id="txt_hidden_cr_month" /><input type="hidden" id="txt_hidden_cr_linenum" /></td>
        </tr>
        <tr>
            <td>Account Name</td>
            <td ><input type="text" id="txt_vwcr_accountname" style="width:184px;"  readonly="readonly"/></td>
      
            <td style="white-space:nowrap;">&nbsp;&nbsp;&nbsp;Hotel Name</td>
            <td>&nbsp;&nbsp;&nbsp;<input type="text" id="txt_vwcr_hotelname" style="width:184px;"   /><input type="hidden" id="txt_hidden_cr_day" /><input type="hidden" id="txt_hidden_cr_year" /></td>
        </tr>
        <tr>
            <td valign="top">Account Address</td>
            <td><textarea rows="2" cols="2" id="txt_vwcr_accountaddress" style="width: 183px; height: 46px" readonly="readonly"></textarea></td>
            <td colspan="2" valign="top">
                <table>
                    <tr>
                        <td> &nbsp;&nbsp;Hotel Contact No.&nbsp;&nbsp;</td>
                        <td><input type="text" id="txt_vwcr_hotelcontact" style="width:184px;"   /></td>
                    </tr>
                    <tr>
                        <td> &nbsp;&nbsp;Frequency of Visit&nbsp;&nbsp;</td>
                        <td><input type="text" id="txt_vwcr_freqvisit" style="width:184px;"/></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
        <div class="tracker"  style="display:none" >
         <h2>Track Services</h2>
         <div style="float:left" class="mapcanvas11">
         </div>
         <div>
            <table>
                <tr>
                    <td>Time In: </td>
                    <td><span id="time_in"></span></td>
                </tr>
                <tr>
                    <td>Location:</td>
                    <td><span id="location_in"></span></td>
                </tr>
                <tr>
                    <td>Time Out:</td>
                    <td><span id="time_out"></span></td>
                </tr>
                <tr>
                    <td>Location:</td>
                    <td><span id="location_out"></span></td>
                </tr>
            </table>
         </div>
     </div>
     <br />	
                                                                  
	<ul class="margin">
		<li id="vwcrcollection"><a href="#vwcallreporttab-1">Collection</a></li>
		<li id="vwcrmsde"><a href="#vwcallreporttab-2">Merchandise</a></li>
		<li id="vwcrsales"><a href="#vwcallreporttab-3">Sales</a></li>
        <li id="vwcrcs"><a href="#vwcallreporttab-4">Customer Service</a></li>
        <li id="vwcrinventory"><a href="#vwcallreporttab-5">Inventory</a></li>
        <li id="VisitLogs"><a href="#">Visited Logs</a></li>
	</ul>
	<div id="vwcallreporttab-1" class="margin">
		<!-- TAB-1 CONTENT START -->
         <table>
                <tr>
                    <td>Dated Check:</td>
                    <td><input type="text" id="txt_cr_datecheck" class="auto collection" /></td>
                </tr>
                <tr>
                    <td>Post Dated Check:</td>
                    <td><input type="text" id="txt_cr_postdatedcheck" class="auto collection" /></td>
                </tr>
                <tr>
                    <td>Total:</td>
                    <td><input type="text" readonly="readonly" id="txt_cr_totalcollection" /></td>
                </tr>
                <tr>
                    <td>Remarks</td>
                    <td><input type="text" id="txt_cr_colremarks"/></td>
                </tr>
        </table>
		<!-- TAB-1 CONTENT END -->
	</div>
    <!-- TAB-2 CONTENT START -->
	<div id="vwcallreporttab-2" class="margin">
        <table>
            <tr>
                <td>Store Check</td>
            </tr>
            <tr>
                <td><textarea rows="2" cols="2" id="txt_vw_cr_pstorecheck" style="width: 371px; height: 74px" <%--readonly="readonly"--%>></textarea></td>
            </tr>
            <tr>
                <td style="width:0.5px;"></td>
            </tr>
            <tr>
                <td>Store Checking Result</td>
            </tr>
            <tr>
                <td><textarea rows="2" cols="2" id="txt_vw_cr_storecheckingres" style="width: 371px; height: 74px"></textarea></td>
            </tr>
            <tr>
                <td style="width:0.5px;"></td>
            </tr>
            <tr>
                <td>Product Presentation Result</td>
            </tr>
            <tr>
                <td>
                    <table id="tbl_vw_cr_msdedtl" cellspacing="0" cellpadding="0">
                        <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px;font-weight:bold; text-align:center;">
                            <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                            <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Product Presented</td>
                            <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Counter Clerk</td>
                            <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Mobile No.</td>
                            <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Remarks</td>
                            <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;" width="1px;"></td> 
                        </tr>
                        <tr class="last_row">
                            <td class="hiddenTd"></td>
                            <td><input type="text" id="txt_vw_cr_productpresented"/></td>
                            <td><input type="text" id="txt_vw_cr_counterclerk"/></td>
                            <td><input type="text" id="txt_vw_cr_mobileno"/></td>
                            <td><input type="text" id="txt_vw_cr_remarks"/></td>
                            <td width="1px;"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
	</div>
    <!-- TAB-2 CONTENT END -->

    <!-- TAB-3 CONTENT START -->
	<div id="vwcallreporttab-3" class="margin">
        <table id="tbl_vw_cr_salesdtl" cellpadding="0" cellspacing="0" style="width:100%">
            <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px;font-weight:bold; text-align:center;">
                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Brand</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Estimated Amount</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Actual Amount</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Details</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Remarks</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;" width="1px;"></td>
            </tr>
            <tr class="last_row">
                <td class="hiddenTd"></td>
                <td><input type="text" id="txt_vw_cr_salesBrand" style="width:98%;" /></td>
                <td><input type="text" id="txt_vw_cr_salesEstimatedAmt" style="width:98%;" /></td>
                <td><input type="text" id="txt_vw_cr_salesActualAmt" style="width:98%;" /></td>
                <td><input type="text" id="txt_vw_cr_details" style="width:98%;" /></td>
                <td><input type="text" id="txt_vw_cr_salesremarks" style="width:96%;" /></td>
                <td></td>
            </tr>
            <tr style="height:1px;">
                <td></td>
            </tr>
            <tr>
                <td class="hiddenTd"></td>
                <td><b>Total:</b></td>
                <td><input type="text" id="txt_vw_sales_totalEst" value="0" readonly="readonly" style="width:100%;" /></td>
                <td><input type="text" id="txt_vw_sales_totalAct" value="0" readonly="readonly" style="width:96%;"/></td>
            </tr>
        </table>
    
        <table>
            <tr>
                <td style="width: 388px">Competitors Activities</td>
            </tr>
            <tr>
                <td style="width: 388px"><textarea rows="2" cols="2" style="width: 416px; height: 71px;" id="txt_cr_competitorsAct"></textarea></td>
            </tr>
        </table>

        <table>
            <tr>
            <td>With orders?</td>
                <td style="white-space:nowrap;">Yes <input type="checkbox" id="chk_vw_yes" /> &nbsp; No <input type="checkbox" id="chk_no" /></td>
            </tr>
            <tr>
                <td>Next Call Date</td>
                <td colspan="4"><input type="text" id="txt_vw_cr_nextcalldate" /></td>
            </tr>
            <tr>
                <td>Other Information</td>
                <td colspan="4"><input type="text" id="txt_vw_cr_otherInfo" /></td>
            </tr>
            <tr>
                <td>P.O Attachment</td>
                <td><input type="text" readonly="readonly" id="txt_vw_FileAttachment2"/> &nbsp; <a href="javascript:;" id="lnk_vw_view_attach2" >View</a></td>
            </tr>
        </table>
	</div>
    <!-- TAB-3 CONTENT END -->


    <!-- TAB-4 CONTENT START -->
	<div id="vwcallreporttab-4" class="margin">
        <table id="tbl_vw_cs_callreport">
            <tr>
                <td>Issues and Concern</td>
                <td colspan="3"><textarea style="width: 250%; height: 65px;" id="txt_vw_cs_callrepIssue"></textarea></td>
            </tr>
            <tr>
                <td colspan="4">Previous Order and Delivery Results</td>
            </tr>
            <tr>
                <td>Delivery:</td>
                <td>On Time<input type="checkbox" id="chk_vw_cs_callrepOntime" /></td>
                <td>Delay<input type="checkbox" id="chk_vw_cs_callrepDelay" /></td>
            </tr>
            <tr>
                <td>Order Status</td>
                <td>Complete<input type="checkbox" id="chk_vw_cs_callrepcomplete" /></td>
                <td>Incomeplete<input type="checkbox" id="chk_vw_cs_callrepincomplete" /></td>
            </tr>
            <tr>
                <td>Summary of Items Lacking</td>
                <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_vw_cs_callrepSummarylacking"></textarea></td>
            </tr>
            <tr>
                <td>Remarks from Customer</td>
                <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_vw_cs_callrepRemarks"></textarea></td>
            </tr>
            <tr>
                <td>Recomendation</td>
                <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_vw_cs_callrepRecommend"></textarea></td>
            </tr>
            <tr>
                <td>Time Table</td>
                <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_vw_cs_callrepTimetable"></textarea></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </div>
    <!-- TAB-4 CONTENT END -->


    <!-- TAB-5 CONTENT START -->
	<div id="vwcallreporttab-5" class="margin">
        <p>
            <span id="cr_create_inventory_count_link"></span>
            <br />
            <span id="cr_view_inventory_count_link"></span>
        </p>
	</div>
    <!-- TAB-5 CONTENT END -->

    <!----TAB-6 CONTENT START--->
    <div id="callreporttab-6" style="min-height:60px;" class="margin"></div>
    <!----TAB-6 CONTENT END--->

     &nbsp;</div>
	</div>
    </div>
<div id="div_last_element"></div>
</asp:Content>
