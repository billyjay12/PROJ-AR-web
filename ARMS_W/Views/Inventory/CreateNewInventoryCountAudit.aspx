﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% string inventory_id = ViewData["InventoryCountId"].ToString(); %>
    <script src="<%=ResolveUrl("~/") %>Scripts/createnewinventorycountAudit.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />

	<%--<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>--%>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<%--<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("~/") %>Scripts/json2.js" type="text/javascript"></script>
        <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>
  
      <%--<script src="http://code.jquery.com/jquery-1.9.1.js"></script>--%>
  <%--<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>--%>

    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>

     <script language="javascript" type="text/javascript" >
         var inventorycount_id = "<%: inventory_id %>";
    </script>
    <style type="text/css">
                .clear { clear:both }
                .readonly { background-color:#ededed }
                .required {background-color:#fff7dd;}
            </style>
    

     <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>Create New Inventory Count Audit</b>
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
  
	    <div id="main_div"  class="simple_box">
            
               <div>
		            <table id="tbl_account_info" cellspacing="0">
                      
                        <tr>
                            <td colspan="9" style="text-align:right">Audit Inventory Count ID:</td>
                            <td><input type="text" value="system generated" id="txt_inventoryauditid" style="font-style:italic; font-size:10px; text-align:center; background-color:#ededed"/></td>
                        </tr>
                        <tr>
                            <td colspan="9" align="right">
                                <span style="white-space:nowrap"> 
                                    Inventory Count For The Month of 
                                </span>
                            </td>
                            <td><input type="text" id="txt_ftm"/></td>
                        </tr>
                        <tr>
                            <td colspan="9" style="text-align:right">Date: </td>
                            <td><input type="text" id="txt_currdate" style="background-color:#ededed"/></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Reference Inventory Count ID:</span></td>
                            <td><input type="text" id="txt_inventoryid" style="font-style:italic; font-size:10px; text-align:center; background-color:#ededed"  /></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Sales Officer ID</span></td>
                            <td><input type="text" id="txt_empId"/></td>
                        </tr>
                        <tr>
                            <td><span style="white-space:nowrap">Sales Officer Name</span></td>
                            <td  colspan="2"><input type="text" style="width:98%" id="txt_empName"/> </td>
                        </tr>
                        <tr>
                            <td>Account Code: </td>
                            <td><input type="text" id="txt_acctCode"/></td>
                            <td></td>
                            <td></td>
                            <td colspan="5"  style="width:100%; text-align:right"><span style="white-space:nowrap">Pareto: </span></td>
                            <td>
                                <input type="radio" id="btnrad_yes" disabled="disabled" />Yes
                                <input type="radio" id="btnrad_no" disabled="disabled" />No
                            </td>
                        </tr>
			            <tr>
				            <td><span style="white-space:nowrap">Account Name:</span></td>
				            <td colspan="2"><input type="text"  style="width:98%"  id="txt_acctName"/></td>
                            <td colspan="6"  style="width:98%; text-align:right"><span style="white-space:nowrap">Area: </span></td>
                            <td><input type="text" id="txt_area" /></td>
			            </tr>
			            <tr>
				            <td><span style="white-space:nowrap">Account Address</span></td>
				            <td  colspan="2"><input type="text" style="width:98%" id="txt_acctAddress"/> </td>
			            </tr>
                        <tr>
                            <td><span style="white-space:nowrap" >Warehouse In-Charge ID:</span></td>
                            <td>
                                    <input type="text"  id="txt_whsId" style="font-style:italic; font-size:10px; text-align:center; background-color:#ededed"  />
                            </td>
                        </tr>
                        <tr>
                            <td width="110px" class="cellWhsName">
                                <div style="font-size:10px; margin-top:-3px; "><b>Warehouse In-Charge</b></div>
                                <div style="font-size:10px">(First/Middle/Last)</div>
                            </td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsFirstName" value="First Name" /></td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsMiddleName" value="Middle Name" /></td>
                            <td class="cellWhsName"><input type="text"  id="txt_whsLastName" value="Last Name" /></td>
                        </tr>
                        
			            <tr>
				            <td><span style="white-space:nowrap">Contact No: </span></td>
				            <td  class="cellWhsName"><input type="text" id="txt_whscontactno" /></td>
			            </tr>
		            </table>
               </div>
               <hr/>
               <div>
             <%--       <div style="overflow:auto;">--%>
		            <table id="tbl_details" cellpadding="2" border="0" cellspacing="0"  >
		            <thead>
			            <tr class="tbl_header">
				            <th class="header colored"style="width:40px"><span style="white-space:nowrap">LINE NO</span></th>
				            <th class="header" style="width:60px"> BRAND </th>
				            <th class="header" style="width:70px"> PROD GRP </th>
				            <th class="header colored"  style="width:70px"> ITEM CODE </th>
				            <th class="header" style="width:250px"> ITEM DESCRIPTION </th>
				            <th class="header" style="width:100px">ACTUAL COUNT </th>
				            <th class="header colored"style="width:100px"> REMARKS </th>
			            </tr>
			        </thead>
                    <tbody>
			            <tr class="last_row">
				            <td></td>
				            <td><input type="text" style="width:60px; background-color:#ededed" id="txt_brand"/></td>
				            <td><input type="text" style="width:70px; background-color:#ededed " id="txt_prodgroup"/></td>
                            <td><input type="text" style="width:120px; background-color:#ededed " id="txt_itmcode" /></td>
				            <td><input type="text" style="width:250px;" id="txt_itmdesc" /></td>
				            <td><input type="text" style="width:100px;" id="txt_actualCount" maxlength="5" /></td>
				            <td><input type="text" id="txt_remarks"/></td>
                            <td><img class="btn" alt="" id="btn_add" src="<%=ResolveUrl("~/") %>Images/Add.png" /> </td>
			            </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
				            <td></td>
                            <td></td>
                            <td></td>
                            <td align="right">Total: </td>
				            <td><input type="text" id="txt_totalCount" style="width:100px;text-align:right" /></td>
				            
				            <td></td>
				            <td></td>
				            <td></td>
				            <td></td>
                        </tr>
			        </tfoot>
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
                
                <div class="div_blinker" >
                    <div id="grp_edit">
                        <input type="button" value="Edit" id="btn_edit" />
                        <input type="button" value="Done Editing" id="btn_done_edit" />
                    </div>
                    <br />
                    <div id="grp_save">
                        <input type="button" value="Save" id="btn_save" /> 
                        <input type="button" value="Cancel" id="btn_cancel"/>
                    </div>
                </div>
            <%-- </div>--%>
        </div>
    </div>
    <div id="div_last_element"></div>
</asp:Content>
