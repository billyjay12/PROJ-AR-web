<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <% string inventory_id = ViewData["InventoryCountId"].ToString(); %>

      <script src="<%=ResolveUrl("~/") %>Scripts/inventoryCountDetails.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Content/demo_table_jui.css"  type="text/css">

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/json2.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.dataTables.js" type="text/javascript"></script>
    
    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript" >
        var inventorycount_id = "<%: inventory_id %>";
    </script>

    <style type="text/css">
        .clear { clear:both }
        .readonly { background-color:#ededed }
        .DataTables_sort_icon.css_right.ui-icon.ui-icon-triangle-1-n
        {
            float:right;
        }   
        table th { height:54px; }
     
    </style>


    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b> Inventory Count Details </b>
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
                    <td align"left" valign="middle">
                        <a id="btn_menu" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/resultset_next.png" id="img_con" style="border:0;" /> MENU</a>
                    </td>
               
                </tr>
            </table>
        </div>

        <div id="tab_main" class="simple_box">
            <ul>
                <li><a href="#tabs-1">Inventory Count</a></li>
                <%--<li><a href="<%:ResolveUrl("~/")%>Inventory/getInventoryCountRouteChanges?InventoryCountId=<%=inventory_id %>">Log </a></li>--%>
            </ul>
            <div id="tabs-1">
	            <div id="main_div">
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
                            <td><input type="text" id="txt_ftm" /></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Sales Officer ID</span></td>
                            <td colspan="1"><input type="text" id="txt_empId" style="width:110%"/></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Sales Officer Name</span></td>
                            <td  colspan="2"><input type="text" style="width:98%" id="txt_empName"/> </td>
                        </tr>
                        <tr>
                            <td>Account Code: </td>
                            <td><input type="text" id="txt_acctCode"style="width:110%"/></td>
                            <td colspan="6"  style="width:100%; text-align:right"><span style="white-space:nowrap">Pareto: </span></td>
                            <td>
                                <input type="radio" id="btnrad_yes" disabled="disabled"/> Yes
                                <input type="radio" id="btnrad_no" disabled="disabled"/> No
                            </td>
                        </tr>
			            <tr>
				            <td><span style="white-space:nowrap">Account Name:</span></td>
				            <td  colspan="2"><input type="text" id="txt_acctName" style="width:98%"/></td>
                            <td colspan="5"  style="width:100%; text-align:right"><span style="white-space:nowrap">Area: </span></td>
                            <td><input type="text" id="txt_area" /></td>
			            </tr>
			            <tr>
				            <td><span style="white-space:nowrap">Account Address</span></td>
				            <td  colspan="2"><input type="text" style="width:98%" id="txt_acctAddress"/> </td>
                            <td colspan="5"  style="width:100%; text-align:right"><span style="white-space:nowrap">Territory </span></td>
                            <td><input type="text" id="txt_territory" /></td>
			            </tr>
                        <tr>
                            <td><span style="white-space:nowrap" >Warehouse In-Charge ID:</span></td>
                            <td colspan="2"><input type="text"  id="txt_whsId" value="system generated" readonly="readonly" style="font-style:italic; font-size:10px; text-align:center; background-color:#ededed"  /></td>
                        </tr>
                        <tr>
                            <td width="110px" class="cellWhsName">
                                <div style="font-size:10px; margin-top:-3px; "><b>Warehouse In-Charge</b></div>
                                <div style="font-size:10px">(First/Middle/Last)</div>
                            </td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsFirstName"  /></td>
                            <td class="cellWhsName" ><input type="text"  id="txt_whsMiddleName"   /></td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsLastName"  /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
			            <tr>
				            <td colspan="1"><span style="white-space:nowrap">Contact No: </span></td>
				            <td><input type="text" id="txt_whscontactno" /></td>
			            </tr>
		            </table>
                    </div>

                    <hr/>

                    <div>
                        
                    <div style="float:left">
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
                    <div class="clear"></div>
                    </div>
                    <br />
                    <div style="overflow-x: scroll;">
                         <div>
                         <fieldset style="width:650px;">
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
                                        <td><input type="checkbox" name="col3" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Net SellIn</td>
                                        <td><input type="checkbox" name="col4" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Ending Inventory</td>
                                        <td><input type="checkbox" name="col5" tbltype="scrollhdr" class="chkbox_column"  checked="checked" /></td>
                                        <td>Net inventory on hand</td>
                                    </tr>
                                    <tr>  
                                        <td><input type="checkbox" name="col6" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Actual Sell-Out</td>
                                        <td><input type="checkbox" name="col7" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Variance</td>
                                        <td><input type="checkbox" name="col8" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Sales Forecast(0)</td>
                                        <td><input type="checkbox" name="col9" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Sales Forecast(1)</td>
                                        <td><input type="checkbox" name="col14" tbltype="scrollhdr" class="chkbox_column" checked="checked" /></td>
                                        <td>Remarks</td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <br />
                     <%-- <div style="overflow-x:scroll">--%>
                        <div>
		           <%--     <table id="tbl_details" cellpadding="2" border="0" cellspacing="0">
                            <thead>
			                    <tr class="tbl_header">
				                    <th class="header colored ui-state-default" style="width:40px"><span style="white-space:nowrap">LINE NO</span></th>
				                    <th class="header ui-state-default" style="width:60px"> BRAND </th>
				                    <th class="header ui-state-default" style="width:70px"> PROD GRP </th>
				                    <th class="header colored ui-state-default" style="width:70px"> ITEM CODE </th>
				                    <th class="header ui-state-default" style="width:250px"> ITEM DESCRIPTION </th>
				                    <th class="header colored ui-state-default"  style="width:50px"> Ideal Inventory </th>
				                    <th class="header ui-state-default" style="width:60px">
					                     BEG INV (PCS)
				                    </th>
                                    <th class="header ui-state-default" style="width:60px">
					                     SELL IN (PCS)
				                    </th>
				                    <th class="header ui-state-default" style="width:60px">
					                     END INV (PCS)
				                    </th>
                                      <th class="header ui-state-default" style="width:60px">
					                     NET SELL IN ON HAND (PCS)
				                    </th>
				                    <th class="colored ui-state-default" colspan="2" style="width:50px"> 
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
                                    <th colspan="2" class="ui-state-default"  style="width:50px">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2"> VARIANCE</td>
                                            </tr>
                                            <tr>
                                                <td>PCS</td>
                                                <td>AMOUNT</td>
                                            </tr>
                                        </table>
                                    </th>
                                    <th colspan="2" class="ui-state-default" style="width:50px">
                                         <table width="100%">
                                                <tr>
                                                    <td colspan="2">  SALES FORECAST (0) </td>
                                                </tr>
                                                <tr>
                                                    <td>PCS</td>
                                                    <td>AMOUNT</td>
                                                </tr>
                                         </table>
                                    </th>
                                    <th colspan="2" class="ui-state-default" style="width:50px">
                                         <table width="100%">
                                                <tr>
                                                    <td colspan="2">  SALES FORECAST (1) </td>
                                                </tr>
                                                <tr>
                                                    <td>PCS</td>
                                                    <td>AMOUNT</td>
                                                </tr>
                                         </table>
                                    </th>
                                     <th colspan="2" class="ui-state-default" style="width:50px">
                                         <table width="100%">
                                                <tr>
                                                    <td colspan="2">  SALES FORECAST (2) </td>
                                                </tr>
                                                <tr>
                                                    <td>PCS</td>
                                                    <td>AMOUNT</td>
                                                </tr>
                                         </table>
                                    </th>
                                     <th colspan="2" class="ui-state-default" style="width:50px">
                                         <table width="100%">
                                                <tr>
                                                    <td colspan="2">  SALES FORECAST (3) </td>
                                                </tr>
                                                <tr>
                                                    <td>PCS</td>
                                                    <td>AMOUNT</td>
                                                </tr>
                                         </table>
                                    </th>
				                    <th class="header colored ui-state-default"  style="width:100px"> REMARKS </th>
			                    </tr>
                            </thead>
                            <tbody>
                                <tr class="last_row"></tr>
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
				                    <td><input type="text" id="txt_totalActAmount" style="width:80px; text-align:right" /></td>
				                    <td align="right">Total: </td>
                                    <td><input type="text" id="txt_totalVarAmount" style="width:80px; text-align:right" /></td>
                                    <td align="right">Total: </td>
                                    <td><input type="text" id="txt_totalSForecastAmount0" style="width:80px; text-align:right"/></td>
                                    <td align="right">Total: </td>
                                    <td><input type="text" id="txt_totalSForecastAmount1" style="width:80px; text-align:right"/></td>
                                    <td align="right">Total: </td>
                                    <td><input type="text" id="txt_totalSForecastAmount2" style="width:80px; text-align:right"/></td>
                                    <td align="right">Total: </td>
                                    <td><input type="text" id="txt_totalSForecastAmount3" style="width:80px; text-align:right"/></td>
                                    <td></td>
                                    <td></td>
				                    <td></td>
                                    <td></td>
                                </tr>
			                </tfoot>
		                </table>--%>
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
                                                    <th colspan="2" class="ui-state-default"  style="width:50px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2"> VARIANCE</td>
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
                                                    <%--<th class="colored" colspan="2" style="width:50px"> 
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
                                                </tr>
                                            
                                                <tr class="tr_total">
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
<%--                                                    <td align="right">Total: </td>
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
                     <%-- </div>--%>
                        <div>
                            <table>
                                <tr>
                                    <td valign="top">Overall Remarks</td>
                                     <td><textarea cols="4" rows="4"  style="resize:none; width:300px; height:40px;" id="txt_overallRemarks" readonly="readonly"></textarea></td>
                                </tr>
                                <tr>
                                    <td>Date Encoded</td>
                                    <td><input type="text" id="txt_dateencoded" readonly="readonly"/></td>
                                </tr>
                            </table>
                        </div>

                    </div>

                   

                </div>

                
               </div>
           </div>
           <div id="tabs-2">
           </div>
        </div>
        <input type="button" id="aaaa"/>
    </div>
</asp:Content>
