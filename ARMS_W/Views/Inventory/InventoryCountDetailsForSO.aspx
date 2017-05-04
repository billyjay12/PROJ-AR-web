<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="System.Web.Mvc.Html5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <% string inventory_id = ViewData["InventoryCountId"].ToString(); %>
    <script src="<%=ResolveUrl("~/") %>Scripts/inventorycountdetailsforSO.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/json2.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>

    <%--              INVENTORY COUNT - SAVE AS DRAFT VIEW         --%>

     <script language="javascript" type="text/javascript" >
         var inventorycount_id = "<%: inventory_id %>";
    </script>

    <style type="text/css">
        .clear { clear:both }
        .readonly { background-color:#ededed }
        .required {background-color:#fff7dd;}
        .chzn-container.chzn-container-single.chzn-container-active input
         {
             width:92%;
         }
         table th { height:54px; }
    </style>

     <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>Inventory Count Details For SO</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>
         <div class="page_header_y">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <a id="btn_menu" href="javascript:;" ><img alt="" src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                    </td>
               
                </tr>
            </table>
        </div>
    
    <div id="tab_main" class="simple_box">
        <ul>
            <li><a href="#tabs-1">Inventory Count</a></li>
            <%--<li><a href="<%:ResolveUrl("~/")%>Inventory/getInventoryCountRouteChanges?InventoryCountId=<%=inventory_id %>">Route Changes </a></li>--%>
        </ul>
        <div id="tabs-1">
	    <div id="main_div">
               <div id="doc_status"></div>
               <div>
		            <table id="tbl_account_info" cellspacing="0">
                      
                        <tr>
                            <td colspan="8" style="text-align:right">Inventory Count ID:</td>
                            <td><input type="text" value="system generated" id="txt_inventoryid" style="font-style:italic; font-size:10px; text-align:center; background-color:#ededed" readonly="readonly" /></td>
                        </tr>
                        <tr>
                               <td colspan="8" align="right">
                                <span style="white-space:nowrap"> 
                                    Inventory Count For The Month of 
                                </span>
                            </td>
                            <td><input type="text" id="txt_ftm"/></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Sales Officer ID</span></td>
                            <td colspan="1">
                                <div style="float:left;">
                                    <input type="text" id="txt_empId"/>
                                </div>
                                <div style="float:right">
                                    <img class="btn" alt="" id="btn_searchSOID" style="display:none" src="<%=ResolveUrl("~/") %>Images/magnifier.png" />
                                </div>
                                <div class="clear"></div>
                             </td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Sales Officer Name</span></td>
                              <td  colspan="2"><input type="text" style="width:100%" id="txt_empName"/> </td>
                        </tr>
                        <tr style="height:20px"></tr>
                        <tr>
                            <td>Account Code: </td>
                            <td>
                                <div style="float:left;">
                                    <input type="text" id="txt_acctCode"/>
                                </div>
                                <div style="float:right">
                                    <img class="btn" alt="" id="btn_searchAcctCode" src="<%=ResolveUrl("~/") %>Images/magnifier.png" />
                                </div>
                                <div class="clear"></div>
                            </td>
                            <td colspan="6"  style="width:100%; text-align:right"><span style="white-space:nowrap">Pareto: </span></td>
                            <td><input type="radio" id="btnrad_yes" disabled="disabled" />Yes
                                <input type="radio" id="btnrad_no" disabled="disabled" />No
                                </td>
                        </tr>
			            <tr>
				            <td><span style="white-space:nowrap">Account Name:</span></td>
				            <td  colspan="2">
                                 <div style="float:left;">
                                    <input type="text" id="txt_acctName" style="width:260%" />
                                </div>
                                <div style="float:right">
                                    <img class="btn" id="btn_searchAcctName" src="<%=ResolveUrl("~/") %>Images/magnifier.png" />
                                </div>
                                <div class="clear"></div>
                            </td>
                            <td colspan="5"  style="width:100%; text-align:right"><span style="white-space:nowrap">Area: </span></td>
                            <td><input type="text" id="txt_area" /></td>
			            </tr>
			            <tr>
				            <td><span style="white-space:nowrap">Account Address</span></td>
				            <td  colspan="2"><input type="text" style="width:100%" id="txt_acctAddress"/> </td>
                            <td colspan="5"  style="width:100%; text-align:right"><span style="white-space:nowrap">Territory </span></td>
                            <td><input type="text" id="txt_territory" /></td>
			            </tr>
                        <tr style="height:20px"></tr>
                        <tr>
                            <td><span style="white-space:nowrap" >Warehouse In-Charge ID:</span></td>
                            <td colspan="2">
                                    <input type="text"  id="txt_whsId" value="system generated" readonly="readonly" style="font-style:italic; font-size:10px; text-align:center; background-color:#ededed"  />
                                    <img alt="" class="btn" id="btn_searchWhsIncharge" src="<%=ResolveUrl("~/") %>Images/magnifier.png" />
                                    <a href="javascript:newWhsIncharge()">new</a>
                               
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="cellWhsName">
                                <div style="font-size:10px; margin-top:-3px; "><b>Warehouse In-Charge</b></div>
                                <div style="font-size:10px">(First/Middle/Last)</div>
                            </td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsFirstName" value="First Name" /></td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsMiddleName"  value="Middle Name" /></td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsLastName" value="Last Name" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                           
                        </tr>
			            <tr>
				            <td colspan="1"><span style="white-space:nowrap">Contact No: </span></td>
				            <td  class="cellWhsName"><input type="text" id="txt_whscontactno" /></td>
                            <td colspan="7">
                               
                            </td>
			            </tr>
		            </table>
               </div>
               <hr/>
                <div>
                    <table>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>Status: </td>
                            <td align="center"><span style="font-weight:bolder" id="spn_statusCount"></span></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Previous Count Date:</span></td>
                            <td><input type="text" id="txt_prevcountdate" /></td>
                            <td><span style="white-space:nowrap">Actual Count Date:</span></td>
                            <td><input type="text" id="txt_currdate"/></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Next Count Due On:</span></td>
                            <td><input type="text" id="txt_nxtcount"/></td>
                            <td><span style="white-space:nowrap">Count Range (In days):</span></td>
                            <td><input type="text" id="txt_countrange"/></td>
                        </tr>
                    </table>
                </div>
               <br />
               <div  style="overflow-x: scroll; ">
                <%-- <div style="overflow-x: scroll; ">
                    <div>
                    <table id="tbl_details" cellpadding="2" border="0" cellspacing="0" >
		            <thead>
			            <tr class="tbl_header">
				            <th class="header colored" rowspan="2" style="width:40px"><span style="white-space:nowrap">LINE NO</span></th>
				            <th class="header" rowspan="2" style="width:60px"> BRAND </th>
				            <th class="header" rowspan="2" style="width:70px"> PROD GRP </th>
				            <th class="header colored" rowspan="2" style="width:70px"> ITEM CODE </th>
				            <th class="header" rowspan="2" style="width:250px"> ITEM DESCRIPTION </th>
				            <th class="header colored" rowspan="2"  style="width:50px"> IDEAL INVENTORY </th>
				            <th class="header" rowspan="2" style="width:60px">BEG INV (PCS) </th>
                            <th class="header" rowspan="2" style="width:60px"> Net SellIn (PCS) </th>
				            <th class="header" rowspan="2" style="width:60px"> END INV (PCS)</th>
                            <th class="header" rowspan="2" style="width:60px"> Net SellIn On Hand (PCS) </th>
				            <th class="colored" colspan="2" style="width:50px"> 
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">ACTUAL SELL-OUT</td>
                                    </tr>
                                    <tr>
                                        <td>PCS</td>
                                        <td>AMOUNT</td>
                                    </tr>
                                </table>
                                
                            </th>
				            <th class="colored" colspan="2" style="width:50px"> Sales Forecast (0)</th>
                            <th class="colored" colspan="2" style="width:50px"> Sales Forecast (1)</th>
                            <th class="colored" colspan="2" style="width:50px"> Sales Forecast (2)</th>
                            <th class="colored" colspan="2" style="width:50px"> Sales Forecast (3)</th>
				            <th class="header colored" rowspan="2" style="width:100px"> REMARKS </th>
			            </tr>
			        </thead>
                    <tbody>
			                <tr class="last_row">
				                <td></td>
				                <td> <select id="txt_brand" style="width:60px"></select></td>
				                <td><input type="text" style="width:70px; background-color:#ededed " id="txt_prodgroup" readonly="readonly"/></td>
                                <td><input type="text" style="width:120px;background-color:#ededed" id="txt_itmcode" readonly="readonly" /></td>
				                <td><input type="text" style="width:250px;" id="txt_itmdesc" readonly="readonly" /></td>
				                <td><input type="number" style="width:80px" oninput="maxLengthCheck(this)" maxLength="6" id="txt_ssr"/></td>
				                <td><input type="text" style="width:60px;background-color:#ededed" id="txt_begnv" readonly="readonly"/></td>
                                <td><input type="text" style="width:60px;background-color:#ededed" id="txt_netSellIn" readonly="readonly"/></td>
				                <td><input type="number" style="width:60px" oninput="maxLengthCheck(this)" maxLength="6" id="txt_endnv"/></td>
                                <td><input type="text" style="width:60px;background-color:#ededed" id="txt_netonhand" readonly="readonly"/></td>
				                <td><input type="text" style="width:50px;background-color:#ededed" id="txt_act_selloutpcs" readonly="readonly"/></td>
				                <td><input type="text" style="width:80px; background-color:#ededed"  readonly="readonly" /></td>
                                <td><input type="text" style="width:50px; background-color:#ededed;" readonly="readonly" id="txt_forecastPcs0"/></td>
                                <td><input type="text" style="width:80px; background-color:#ededed;" readonly="readonly" /></td>
                                <td><input type="number" style="width:50px;" oninput="maxLengthCheck(this)" maxLength="6" id="txt_forecastPcs1"/></td>
                                <td><input type="text" style="width:80px; background-color:#ededed;" readonly="readonly" /></td>
                                <td><input type="number" style="width:50px;" oninput="maxLengthCheck(this)" maxLength="6" id="txt_forecastPcs2"/></td>
                                <td><input type="text" style="width:80px; background-color:#ededed;" readonly="readonly" /></td>
                                <td><input type="number" style="width:50px;" oninput="maxLengthCheck(this)" maxLength="6" id="txt_forecastPcs3"/></td>
                                <td><input type="text" style="width:80px; background-color:#ededed;" readonly="readonly" /></td>
				                <td><input type="text" id="txt_remarks"/></td>
                                <td><img class="btn" id="btn_add" alt="" src="<%=ResolveUrl("~/") %>Images/Add.png" /> </td>
                                <td><input type="text" style="display:none" id="txt_hidAmt" /></td>
			                </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
				            <td></td>
                            <td></td>
                            <td></td>
				            <td></td>
                            <td></td>
                            <td></td>
				            <td></td>
				            <td></td>
				            <td></td>
				            <td align="right">Total: </td>
				            <td><input type="text" id="txt_totalamount" style="width:80px; text-align:right" /></td>
				            <td align="right">Total: </td>
				            <td><input type="text" id="txt_totalamount0" style="width:80px; text-align:right" /></td>
                            <td align="right">Total: </td>
				            <td><input type="text" id="txt_totalamount1" style="width:80px; text-align:right" /></td>
                            <td align="right">Total: </td>
				            <td><input type="text" id="txt_totalamount2" style="width:80px; text-align:right" /></td>
                            <td align="right">Total: </td>
				            <td><input type="text" id="txt_totalamount3" style="width:80px; text-align:right" /></td>
                            <td></td>
                        </tr>
			        </tfoot>
		            </table>
                    </div>
                </div>--%>
                 <div>
                    <fieldset style="width:600px;">
                        <legend style="font-weight:bold; text-align:left; vertical-align:top; "><input type="checkbox" name="col1000" id="master_checkbox" style="display:none" checked="checked" /><label>Show Columns</label></legend>
                        <table id="tbl_column_controller" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><input type="checkbox" name="col1" tbltype="freezehdr" class="chkbox_column" checked="checked" /></td>
                                <td>Line</td>
                                <td><input type="checkbox" name="col3" tbltype="freezehdr" class="chkbox_column" checked="checked" /></td>
                                <td>Product Group</td>
                                <td><input type="checkbox" name="col4" tbltype="freezehdr" class="chkbox_column" checked="checked" /></td>
                                <td>Item Code</td>
                            </tr>
                            <tr>
                                <td><input type="checkbox" name="col1" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                <td>Ideal Inventory</td>
                                <td><input type="checkbox" name="col2" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                <td>Beginning Inventory</td>
                                <td><input type="checkbox"name="col3" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                <td>Net SellIn</td>
                                <td><input type="checkbox"name="col4" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                <td>Ending Inventory</td>
                            </tr>
                            <tr>  
                                <td><input type="checkbox" class="chkbox_column" tbltype="scrollhdr" name="col5"  checked="checked" /></td>
                                <td>Net inventory on hand</td>
                                <td><input type="checkbox" class="chkbox_column" tbltype="scrollhdr" name="col6" checked="checked" /></td>
                                <td>Actual Sell-Out</td>
                                <td><input type="checkbox" class="chkbox_column" tbltype="scrollhdr" name="col7" checked="checked" /></td>
                                <td>Sales Forecast(0)</td>
                                <td><input type="checkbox" class="chkbox_column" tbltype="scrollhdr" name="col9" checked="checked" /></td>
                                <td>Remarks</td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <br />
                 <div>
		                <table id="tbl_details" cellpadding="0" border="0" cellspacing="0" >
			                    <tr class="tbl_header">
                                    <td valign="top">
                                        <div style="overflow:hidden" class="freeze_header">
                                            <table id="tbl_freeze_header" cellpadding="2" border="0" cellspacing="0">
                                                <tr>
				                                    <th class="header colored"  style="width:40px">LINE</th>
				                                    <th class="header" style="width:60px"> BRAND </th>
				                                    <th class="header"  style="width:40px"> PROD GRP </th>
				                                    <th class="header colored" style="width:70px"> ITEM CODE </th>
				                                    <th class="header" style="width:250px"> ITEM DESCRIPTION </th>
                                                     
                                                </tr>
                                                 <tr class="last_row">
				                                    <td></td>
				                                    <td><select id="txt_brand" style="width:60px"></select></td>
				                                    <td><input type="text" style="width:50px; background-color:#ededed " id="txt_prodgroup" readonly="readonly"/></td>
                                                    <td><input type="text" style="width:120px;background-color:#ededed" id="txt_itmcode" readonly="readonly" /></td>
				                                    <td><input type="text" style="width:250px;" id="txt_itmdesc" readonly="readonly" /></td>
			                                    </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td valign="top">
                                       <div style="overflow-x:scroll; width:1000px;" class="other_header">
                                           <table id="tbl_scroll_header" cellpadding="2" border="0" cellspacing="0">
                                                <tr>
				                                    <th class="header colored"   style="width:50px"> IDEAL INVENTORY </th>
				                                    <th class="header" style="width:60px">BEG INV (PCS) </th>
                                                    <th class="header"  style="width:60px"> Net SellIn (PCS) </th>
				                                    <th class="header"  style="width:60px"> END INV (PCS)</th>
                                                    <th class="header"  style="width:60px"> NET INV ON HAND(PCS)</th>
				                                    <th class="colored" colspan="2" style="width:50px"> 
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">ACTUAL SELL-OUT</td>
                                                            </tr>
                                                            <tr>
                                                                <td>PCS</td>
                                                                <td>AMOUNT</td>
                                                            </tr>
                                                        </table>
                                
                                                    </th>
                                                    <th class="colored" colspan="2" style="width:50px"> 
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">Sales Forecast (0)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>PCS</td>
                                                                <td>AMOUNT</td>
                                                            </tr>
                                                        </table>
                                
                                                    </th>
                                                    <th class="colored" colspan="2" style="width:50px"> 
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">Sales Forecast (1)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>PCS</td>
                                                                <td>AMOUNT</td>
                                                            </tr>
                                                        </table>
                                
                                                    </th>
                                                   <%-- <th class="colored" colspan="2" style="width:50px"> 
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">Sales Forecast (2)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>PCS</td>
                                                                <td>AMOUNT</td>
                                                            </tr>
                                                        </table>
                                
                                                    </th>
                                                    <th class="colored" colspan="2" style="width:50px"> 
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">Sales Forecast (3)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>PCS</td>
                                                                <td>AMOUNT</td>
                                                            </tr>
                                                        </table>
                                
                                                    </th>--%>
				                                    <th class="header colored" style="width:100px"> REMARKS </th>
                                                </tr>
                                                <tr class="last_row">
                                                    <td><input type="number" style="width:80px"  id="txt_ssr" oninput="maxLengthCheck(this)" maxLength="6" class="num_format"/></td>
				                                    <td><input type="text" style="width:60px;background-color:#ededed" id="txt_begnv" readonly="readonly" class="num_format"/></td>
                                                    <td><input type="text" style="width:60px;background-color:#ededed" id="txt_netSellIn" readonly="readonly"/></td>
				                                    <td><input type="number" style="width:80px" oninput="maxLengthCheck(this)" maxLength="6"  id="txt_endnv"/></td>
                                                    <td><input type="text" style="width:60px;background-color:#ededed" id="txt_netonhand" readonly="readonly"/></td>
				                                    <td><input type="text" style="width:50px;background-color:#ededed" id="txt_act_selloutpcs" readonly="readonly"/></td>
				                                    <td><input type="text" style="width:80px; background-color:#ededed" readonly="readonly" /></td>
                                                    <td><input type="text" style="width:70px; background-color:#ededed;" readonly="readonly" id="txt_forecastPcs0"/></td>
                                                    <td><input type="text" style="width:80px; background-color:#ededed;" readonly="readonly" /></td>
                                                    <td><input type="number" style="width:70px;" oninput="maxLengthCheck(this)" maxLength="6" id="txt_forecastPcs1" class="num_format"/></td>
                                                    <td><input type="text" style="width:80px; background-color:#ededed" readonly="readonly" /></td>
                                                    <%--<td><input type="number" style="width:70px;" oninput="maxLengthCheck(this)" maxLength="6" id="txt_forecastPcs2" class="num_format"/></td>
                                                    <td><input type="text" style="width:80px; background-color:#ededed" readonly="readonly" /></td>
                                                    <td><input type="number" style="width:70px;" oninput="maxLengthCheck(this)" maxLength="6" id="txt_forecastPcs3" class="num_format"/></td>
                                                    <td><input type="text" style="width:80px; background-color:#ededed" readonly="readonly" /></td>--%>
				                                    <td><input type="text" id="txt_remarks"/></td>
                                                    <td><img class="btn" id="btn_add" alt="" src="<%=ResolveUrl("~/") %>Images/Add.png" /> </td>
                                                    <td><input type="text" style="display:none" id="txt_hidAmt" /></td>
                                                </tr>
                                            
                                                <tr class="tr_total">
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
				                                    <td></td>
				                                    <td></td>
				                                    <td align="right">Total: </td>
				                                    <td><input type="text" id="txt_totalamount" style="width:80px; text-align:right" /></td>
				                                    <td align="right">Total: </td>
				                                    <td><input type="text" id="txt_totalamount0" style="width:80px; text-align:right" /></td>
                                                    <td align="right">Total: </td>
				                                    <td><input type="text" id="txt_totalamount1" style="width:80px; text-align:right" /></td>
                                                    <%--<td align="right">Total: </td>
				                                    <td><input type="text" id="txt_totalamount2" style="width:80px; text-align:right" /></td>
                                                    <td align="right">Total: </td>
				                                    <td><input type="text" id="txt_totalamount3" style="width:80px; text-align:right" /></td>--%>
                                                    <td></td>
                                                    <td></td>
				                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                        </td>
			                    </tr>
		                </table>
                    </div>
                <div>
                       <table>
                            <tr>
                                <td valign="top"><span style="white-space:nowrap">Overall Remarks</span></td>
                                <td><textarea cols="4" rows="4"  style="resize:none; width:300px; height:40px;" id="txt_overallRemarks"></textarea></td>
                                
                            </tr>
                        </table>
                </div>
                <div  class="div_blinker" >
                    <div id="grp_edit">
                       <input type="button" value="Edit" id="btn_edit" /> 
                       <input type="button" value="Done Editing" id="btn_doneEdit"/>
                    </div>
                    <br />
                    <div id="grp_save">
                            <input type="button" value="Save as Draft" id="btn_save_draft" />
                            <input type="button" value="Save" id="btn_save" /> 
                            <input type="button" style="display:none" value="Cancel" id="btn_cancel"/>
                    </div>
                </div>
             </div>
        </div>
        </div>
        <div id="tabs-2"></div>
    </div>
    
    </div>
     <div id="div_last_element"></div>
</asp:Content>
