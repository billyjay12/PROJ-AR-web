<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="System.Web.Mvc.Html5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>--%>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/myObjective.js" type="text/javascript"></script>
      
    <script src="<%=ResolveUrl("~/") %>Scripts/1.10.3/ui/jquery-ui.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/timekeepingjavascript.js" type="text/javascript"></script>
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false"  type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/") %>Scripts/memo_geolocation.js" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/memo_geolocation2.js" type="text/javascript"></script>--%>

    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>

   <script src="<%=ResolveUrl("~/") %>Scripts/autoNumeric.js" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("~/") %>Scripts/autoLoader.js" type="text/javascript"></script>--%>
    <% 
        string Eventdate=ViewData["date"].ToString();
        string Eventday = ViewData["day"].ToString();
        string Eventmonth = ViewData["month"].ToString();
        string Eventyear = ViewData["year"].ToString();
        string soId = ViewData["soId"].ToString();
        string acctCode = ViewData["acctCode"].ToString();
        string enableCallReport = ViewData["enableCallReport"].ToString();
    %>
    
    <style type="text/css">
        input[readonly] { background-color:#F0F0F0; }
    
        .tracker {border:2px solid #0094ff; border-top:20px;}
        .tracker h2 {background:#0094ff; color:white; padding:10px;  vertical-align:top;}
        .tracker p {color:#333;padding:10px;}
        .tracker {
            -moz-border-radius-topright:5px;
            -moz-border-radius-topleft:5px;
            -webkit-border-top-right-radius:5px;
            -webkit-border-top-left-radius:5px;
        }
    
        .tabcolor > a { background-color:#FFD700; }
        textarea[readonly] { background-color:#F0F0F0; }
        .fld_amount { text-align:right; }
        .data { white-space:nowrap; }
        .margin {  margin-right:20px; }
    </style>    

	<script type="text/javascript" language="javascript">
        var Eventdate = "<%: Eventdate %>";
        var Eventday = "<%: Eventday %>";
        var Eventmonth = "<%: Eventmonth %>";
        var Eventyear = "<%: Eventyear %>";
        var soId = "<%: soId %>";
        var qrystring_acctCode = "<%: acctCode %>";
        var enableCallReport_ = "<%:enableCallReport %>";

        var baseurl = "<%= ResolveUrl("~/") %>";
        var LineNum="";
		$(function () {
			$("#tabs").tabs();
            $("#header_tab").tabs();
            $("#callreporttab").tabs();
            $("#tabs_assign").tabs();
			$("#sub_tab").tabs({
				select: function (event, ui) {
					
				}
			});
            if(enableCallReport_=="False"){
                $("a[href=#callreporttab]").parent().hide();
              //    $("#header_tab").find("ul > li:nth-child(2)").remove();
                //  $("#header_tab ul li:nth-child(2)").remove();
            }
          LineNum= txt_hidden_cr_linenum.attr("value");
		});
    </script>

    <div class="bl_box" style="height:inherit; width:100%;" >
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Coverage Plan</b>
                </td>
                <td align="right">
                 <span id="spn_dateholder" style="font-size:medium; font-weight:bold;"></span>
                </td>
            </tr>
        </table>
    </div>

    <div id="header_tab" style="width:99%; <%--height:100%;--%> <%--overflow:scroll;--%>">
    <ul>
		<li ><a href="#tabs">Coverage</a></li>
		<li><a href="#callreporttab">Call Report</a></li>
	</ul>
    
	<div id="tabs" style="width:98%;" class="margin" >
     <!--ADDED--->
     <div class="margin">
        <span style="float:right;">
            [<span style="color:red; font-weight:bold" id="spn_eventstatus"><h></h></span>]
        </span>
        <br />
        <table id="tbl_header_info">
            <tr>
                <td>Account Name</td>
                <td><input type="text" id="txt_accountName" style="width:184px;"  readonly="readonly"/></td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;Contact Person</td>
                <td><input type="text" id="txt_contactPerson" style="width:184px;"   /><input type="hidden" id="txt_hidden_eventId"/></td>
            </tr>
            <tr>
                <td>Account Code</td>
                <td ><input type="text" id="txt_acctcode" style="width:184px;" readonly="readonly" /></td>
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;Contact Number</td>
                <td><input type="text" id="txt_ContactNumber" style="width:184px;"   /><input type="hidden" id="txt_hidden_linenum"/></td>
            </tr>

            <tr>
                <td>Account Class</td>
                <td ><input type="text" id="txt_accountClass" style="width:184px;"   readonly="readonly"/></td>
                <td></td>
                <td style="white-space:nowrap;">&nbsp;&nbsp;&nbsp;Hotel Name</td>
                <td><input type="text" id="txt_hotelname" style="width:184px;"   /></td>
            </tr>

            <tr>
                <td valign="top">Account Address</td>
                <td colspan="2"><textarea rows="2" cols="2" id="txt_accountAddress" style="width: 183px; height: 46px" readonly="readonly"></textarea></td>
                <td valign="top" style="white-space:nowrap;">&nbsp;&nbsp;&nbsp;Hotel Contact Number</td>
                <td valign="top"><input type="text" id="txt_hotel_contact" style="width:184px;"   /></td>
            </tr>
        </table>
     </div>
   
     <!--END ADDED-->
	<ul class="margin" style="margin-right:20px">
		<li><a href="#tabs-1">Collection</a></li>
		<li><a href="#tabs-2">Merchandise</a></li>
		<li><a href="#tabs-3">Sales</a></li>
        <li><a href="#tabs-4">Customer Service</a></li>
        <li id="inventory"><a href="#tabs-5">Inventory</a></li>
        <li id="routeChanges"><a href="#">Route Changes</a></li>
	</ul>
    <div id="tabs-1" style="min-height:300px;" class="margin">
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
	<div id="tabs-2" style="min-height:300px" class="margin">
         <table id="tbl_merchandise">
             <tr>
                <td>Store Checking</td>
             </tr>
             <tr>
                <td><textarea style="width: 335px; height: 103px" id="txt_storechecking" maxlength="200"></textarea></td>
             </tr>
         </table> 

         <table id="tbl_mse_details" cellpadding="0" cellspacing="0">
             <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center; font-weight:bold;">
                 <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                 <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Product to present</td>
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
                 <td><img  src="<%=ResolveUrl("~/")%>Images/add.png" class="btn_add" /></td>
                 <td width="10px;"></td>
             </tr>
         </table>
	</div>
    <!-- TAB-2 CONTENT END -->

    <!-- TAB-3 CONTENT START -->
	<div id="tabs-3" style="min-height:300px;" class="margin">
        <table id="tbl_sales_dtls" cellpadding="0" cellspacing="0">
            <tr  style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px; text-align:center; font-weight:bold;">
                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Brand</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Amount</td>

                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Details</td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;" width="1px;"></td>
            </tr>
            <tr class="last_row">
                <td class="hiddenTd"></td>
                <td><input type="text" id="txt_salesBrand" readonly="readonly" /></td>
                <td><input type="text" id="txt_salesAmount" class="auto" /></td>
                <td><input type="text" id="txt_salesRemarks" /></td>
                <td><img src="<%=ResolveUrl("~/") %>Images/add.png" class="btn_add" /></td>
                <td></td>
            </tr>
                <td style="height:1px;"></td>
            <tr>
                <td colspan="1" align="right"><b>Total</b></td>
                <td><input type="text" id="txt_total_sales" readonly="readonly" value="0" /></td>
            </tr>
        </table>
	</div>
    <!-- TAB-3 CONTENT END -->
    <!-- TAB-4 CONTENT START -->
	<div id="tabs-4" style="min-height:300px;" class="margin">
        <table id="tbl_custService">
            <tr>
                <td>Issues and Concerns</td>
            </tr>
            <tr>
                <td><textarea style="width: 335px; height: 103px" id="txt_issue_concern"  maxlength="200"></textarea></td>
            </tr>
        </table>     
	</div>
    <!-- TAB-4 CONTENT END -->
    <!-- TAB-5 CONTENT START -->
    <div id="tabs-5" style="min-height:300px" class="margin">
        <p>
        <span id="create_inventory_count_link"></span>
        <br />
        <span id="view_inventory_count_link"></span>
        </p>
	</div>
    <!-- TAB-5 CONTENT END -->
    <!-- TAB-6 CONTENT START -->
    <!-- TAB-6 CONTENT END -->

    <%--<input type="button" id="btn_saveObj" value="Save Only" />--%> <%--&nbsp; &nbsp; &nbsp; <input type="button" id="btn_saveandsend" value="Save and Send" />--%>
    <div id="div_delete">
        <input type="button" id="btn_delete" value="Delete" />
    </div>
    <br />
    <div id="div_saveDraft">
	    <input type="button" id="btn_saveDraft" value="Save as draft" /> /  <input type="button" id="btn_cancelDraft" value="Cancel" /> 
    </div>
    <br />
    <div id="div_edit">
     <%--<input type="button" id="btn_coverageEdit" value="Edit" />--%>
    </div>
    <br />
    <div id="div_update">
        <span id="spn_delete">
            <input type ="button" id="btn_softdelete" value="Delete" disabled="disabled"/> /
        </span>
        <input type="button" id="btn_saveChanges" value="Save" disabled="disabled" /> /
        <input type="button" id="btn_cancelChanges" value="Cancel" disabled="disabled" /> 
    </div>
    </div>

    <div id="callreporttab" style="width:98%;">
        <div class="margin">
             <span style="float:right;">
                [<span style="color:red; font-weight:bold" id="spn_callreport_status"><h></h></span>]
            </span>
            <br />
            <table >
                <tr>
                    <td>Unplanned</td>
                    <td><input type="checkbox" id="chk_isunPlanned" /></td>
                </tr>
            </table>
            <table id="tbl_headerCallrep" >
                <tr>
                    <td>Account Name</td>
                    <td ><input type="text" id="txt_cr_accountname" style="width:184px;"  readonly="readonly"/></td>
      
                    <td>&nbsp;&nbsp;&nbsp;Contact Person</td>
                    <td>&nbsp;&nbsp;&nbsp;<input type="text" id="txt_cr_contactperson" style="width:184px;"   /><input type="hidden" id="txt_hidden_cr_soId" /><input type="hidden" id="txt_hiden_cr_eventId" /></td>
                </tr>
                <tr>
                    <td>Account Code</td>
                    <td ><input type="text" id="txt_cr_accountCode" style="width:184px;" readonly="readonly" /></td>
      
                    <td>&nbsp;&nbsp;&nbsp;Contact Number</td>
                    <td>&nbsp;&nbsp;&nbsp;<input type="text" id="txt_cr_contactpersonNo" style="width:184px;"   /><input type="hidden" id="txt_hidden_cr_month" /><input type="hidden" id="txt_hidden_cr_linenum" /></td>
                </tr>
                <tr>
                    <td>Account Class</td>
                    <td ><input type="text" id="txt_cr_accountclass" style="width:184px;"   readonly="readonly"/></td>
      
                    <td style="white-space:nowrap;">&nbsp;&nbsp;&nbsp;Hotel Name</td>
                    <td>&nbsp;&nbsp;&nbsp;<input type="text" id="txt_cr_hotelname" style="width:184px;"   /><input type="hidden" id="txt_hidden_cr_day" /><input type="hidden" id="txt_hidden_cr_year" /></td>
                </tr>

                <tr>
                    <td valign="top">Account Address</td>
                    <td><textarea id="txt_cr_accountaddress" style="width: 183px; height: 46px" readonly="readonly"></textarea></td>
                    <td colspan="2" valign="top">
                        <table>
                            <tr>
                                <td> &nbsp;&nbsp;Hotel Contact No.&nbsp;&nbsp;</td>
                                <td><input type="text" id="txt_cr_hotelcontact" style="width:184px;"   /></td>
                            </tr>
                            <tr>
                                <td> &nbsp;&nbsp;Frequency of Visit&nbsp;&nbsp;</td>
                                <td><input type="text" id="txt_freqvisit" style="width:184px;" readonly="readonly"  /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    <br />
        <div class="tracker margin">
            <h2>Geo Locator</h2>
            <%--<div style="height:200px;" class="mapcanvas11" id="map_canvas">
            </div>--%>
            <div class="mapcanvas11" id="map_canvas"></div>
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
                    <tr>
                        <td colspan="2">
                            <input type="button" class="btn_getLocation" value="Check In" id="btn_checkin"  style="padding:0 5px 0 5px;margin:0; height:22px;" />
                            <input type="button" class="btn_getLocation" value="Check Out" id="btn_checkout" style="padding:0 5px 0 5px;margin:0; height:22px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    <br />	 

	<ul class="margin">
		<li id="crcollection"><a href="#callreporttab-1">Collection</a></li>
		<li id="crmerchandise"><a href="#callreporttab-2">Merchandise</a></li>
		<li id="crsales"><a href="#callreporttab-3">Sales</a></li>
        <li id="crcs"><a href="#callreporttab-4">Customer Service</a></li>
        <li id="crinventory"><a href="#callreporttab-5">Inventory</a></li>
        <li id="Logchanges"><a href="#">Log Changes</a></li>
        <li id="VisitLogs"><a href="#">Visited Logs</a></li>
	</ul>

        <%--<div id="callreporttab-1" style="min-height:300px">--%>
        <div id="callreporttab-1" class="margin" >
	        <!-- TAB-1 CONTENT START -->
          <%--  <table id="tbl_cr_collection" cellpadding="1" cellspacing="1">
                <tr>
                    <td></td>
                    <td align="center">Amount</td>
                </tr>
                <tr>
                    <td>Past Due:</td>
                    <td><input type="text" class="fld_amount" id="txt_cr_pastdue_amount" readonly="readonly"/></td>
                </tr>
                <tr>
                    <td>Collectible:</td>
                    <td><input type="text"  class="fld_amount" id="txt_cr_collectible_amount" readonly="readonly" /></td>
                </tr>
                <tr>
                    <td>Due for the Month:</td>
                    <td><input type="text"  class="fld_amount" id="txt_cr_dueforthemonth_amount" readonly="readonly" /></td>
                </tr>
                <tr>
                    <td>Sales A/R:</td>
                    <td><input type="text"  class="fld_amount" id="txt_cr_salesar_amount" readonly="readonly" /></td>
                </tr>
                <tr>
                    <td align="right">Total:</td>
                    <td><input type="text" class="fld_amount" id="txt_cr_total_amount" readonly="readonly"/></td>
                </tr>
                <tr>
                    <td align="right">Planned Amount:</td>
                    <td><input type="text" class="fld_amount" id="txt_cr_planned_amount" readonly="readonly"/></td>
                </tr>
                <tr>
                    <td align="right">Actual Amount:</td>
                    <td><input type="text" class="fld_amount" id="txt_cr_colactual_amount"</td>
                </tr>
            </table>--%>
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
                    <td><input type="text" readonly="readonly" class="auto" id="txt_cr_totalcollection" /></td>
                </tr>
                <tr>
                    <td>Remarks</td>
                    <td><input type="text" id="txt_cr_colremarks"/></td>
                </tr>
            </table>
		    <!-- TAB-1 CONTENT END -->
	    </div>

        <!-- TAB-2 CONTENT START -->
        <div id="callreporttab-2" class="margin">
            <table>
                <tr>
                    <td>Store Check</td>
                </tr>
                <tr>
                    <td><textarea rows="2" id="txt_cr_pstorecheck" style="width: 371px; height: 74px" maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td style="width:0.5px;"></td>
                </tr>
                <tr>
                    <td>Store Checking Result</td>
                </tr>
                <tr>
                    <td><textarea rows="2" id="txt_cr_storecheckingres" style="width: 371px; height: 74px"  maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td style="width:0.5px;"></td>
                </tr>
                <tr>
                    <td>Product Presentation Result</td>
                </tr>
                <tr>
                    <td>
                        <table id="tbl_cr_msdedtl" cellpadding="0" cellspacing="0">
                            <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px;font-weight:bold; text-align:center;">
                                <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Product Presented</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Counter Clerk</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Mobile No.</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Remarks</td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                                <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;" width="10px;">&nbsp;</td>
                            </tr>
                            <tr class="last_row">
                                <td class="hiddenTd"></td>
                                <td><input type="text" id="txt_cr_productpresented"/></td>
                                <td><input type="text" id="txt_cr_counterclerk"/></td>
                                <td><input type="text" id="txt_cr_mobileno"/></td>
                                <td><input type="text" id="txt_cr_remarks"/></td>
                                <td><img alt="" src="<%=ResolveUrl("~/")%>Images/add.png" class="btn_add" /></td>
                                <td width="10px;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
	    </div>
        <!-- TAB-2 CONTENT END -->

        <!-- TAB-3 CONTENT START -->
	    <div id="callreporttab-3" class="margin">
            <table id="tbl_cr_salesdtl" cellpadding="0" cellspacing="0" style="width:100%">
                <tr style="background: url(<%=ResolveUrl("~/") %>Images/grid_header.gif) repeat-x; padding:0px;font-weight:bold; text-align:center;">
                    <td class="hiddenTd" style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Brand</td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Estimated Amount</td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Actual Amount</td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Details</td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;">Remarks</td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat left top;"></td>
                    <td style="background:url(<%=ResolveUrl("~/") %>Images/grid_header_divider.gif) no-repeat right top;">&nbsp;</td>
                </tr>
                <tr class="last_row">
                    <td class="hiddenTd"></td>
                    <td><input type="text" id="txt_cr_salesBrand" readonly="readonly" style="width:98%;" /></td>
                    <td><input type="text" id="txt_cr_salesEstimatedAmt" class="auto" style="width:98%;" /></td>
                    <td><input type="text" id="txt_cr_salesActualAmt" class="auto" style="width:98%;" /></td>
                    <td><input type="text" id="txt_cr_details"  style="width:98%;"/></td>                            
                    <td><input type="text" id="txt_cr_salesremarks" style="width:96%;"/></td>
                    <td><img alt="" src="<%=ResolveUrl("~/")%>Images/add.png" class="btn_add" /></td>
                </tr>
               <%-- <td style="height:1px;"></td> to be remove--%>
                <tr>
                    <td colspan="1" align="right"><b>Total &nbsp;</b></td>
                    <td><input type="text" id="txt_cr_total_salesEstAmt" style="width:98%;" readonly="readonly" value="0" /></td>
                    <td><input type="text" id="txt_cr_total_salesActtAmt" readonly="readonly" value="0" style="width:98%;" /></td>
                </tr>
            </table>
    
            <table>
                <tr>
                    <td style="width: 388px">Competitors Activities</td>
                </tr>
                <tr>
                    <td style="width: 388px"><textarea rows="2" style="width: 416px; height: 71px;" id="txt_cr_competitorsAct"  maxlength="200"></textarea></td>
                </tr>
            </table>

            <table>
                <tr>
                    <td>With orders?</td>
                    <td style="white-space:nowrap;">Yes <input type="checkbox" id="chk_yes" /> &nbsp; No <input type="checkbox" id="chk_no" /></td>
                </tr>
                <tr>
                    <td>Next Call Date</td>
                    <td colspan="4"><input type="text" id="txt_cr_nextcalldate" /></td>
                </tr>
                <tr>
                    <td>Other Information</td>
                    <td colspan="4"><input type="text" id="txt_cr_otherInfo" /></td>
                </tr>
                <tr>
                    <td valign="top">P.O Attachment</td>
                    
                    <td id="tdUploadBtn">
                    <form id="formUpload" action="<%=ResolveUrl("~/") %>Calendar/Upload" method="post" enctype="multipart/form-data">
                                                    <input type="file" name="file" id="txtAttachment" />
                                                </form>
                    </td>
                    
                    <td id="tdDownloadBtn"><input  type="text" id="filename" readonly="readonly"/><a style="color:blue; text-decoration:none;" id="DwnldFile">  Download</a> </td>
                    <td id="tdModBtn"><a style="color:blue; cursor:pointer;" id="changePOAttachment">| Edit</a> <a style="color:blue; cursor:pointer;" id="cancelEdit">| Cancel</a></td>
                </tr>
            </table>
	    </div>
        <!-- TAB-3 CONTENT END -->

        <!-- TAB-4 CONTENT START -->
	    <div id="callreporttab-4" class="margin">
            <table id="tbl_cs_callreport">
                <tr>
                    <td>Issues and Concern</td>
                    <td colspan="3"><textarea rows="2" style="width: 250%; height: 65px;" id="txt_cs_callrepIssue"  maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td colspan="4">Previous Order and Delivery Results</td>
                </tr>
                <tr>
                    <td>Delivery:</td>
                    <td>On Time<input type="checkbox" id="chk_cs_callrepOntime" /></td>
                    <td>Delay<input type="checkbox" id="chk_cs_callrepDelay" /></td>
                </tr>
                <tr>
                    <td>Order Status</td>
                    <td>Complete<input type="checkbox" id="chk_cs_callrepcomplete" /></td>
                    <td>Incomeplete<input type="checkbox" id="chk_cs_callrepincomplete" /></td>
                </tr>
                <tr>
                    <td>Summary of Items Lacking</td>
                    <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_cs_callrepSummarylacking"  maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td>Remarks from Customer</td>
                    <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" 
                            id="txt_cs_callrepRemarks"  maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td>Recomendation</td>
                    <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_cs_callrepRecommend"  maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td>Time Table</td>
                    <td colspan="3"><textarea rows="2" cols="2" style="width: 250%; height: 65px;" id="txt_cs_callrepTimetable"  maxlength="200"></textarea></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
	    </div>
        <!-- TAB-4 CONTENT END -->
        <!-- TAB-5 CONTENT START -->
	    <div id="callreporttab-5" style="min-height:50px" class="margin">
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

         <!----TAB-7 CONTENT START--->
        <div id="callreporttab-7" style="min-height:60px;" class="margin"></div>
        <!----TAB-7 CONTENT END--->

        <hr />
        <div id="div_callreport">
            <input type="button" id="btn_updatecallrep" value="Save Call Report"/> / <input type="button" id="btn_cancelcallreport" value="Cancel"/>
        </div>
    </div>
	</div>
    </div>
<hr />
 <input type="hidden" id="txt_Eventday" /> &nbsp;&nbsp;&nbsp;<input type="hidden" id="txt_Eventmonth" />&nbsp;&nbsp;&nbsp;<input type="hidden" id="txt_Eventyear" />

<div id="div_last_element"></div>
</asp:Content>



