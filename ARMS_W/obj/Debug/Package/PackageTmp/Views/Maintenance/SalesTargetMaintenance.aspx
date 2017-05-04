<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        int year = (int)ViewData["first_year"];
         %>

    <script src="<%=ResolveUrl("~/") %>Scripts/SalesTargetMaintenance.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jLib.js" type="text/javascript"></script>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/CustomerRegulareForm.css" rel="stylesheet" type="text/css" />
    
    <link href="<%=ResolveUrl("~/") %>Content/chosen.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/json2.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/toggleMenu.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/jquery.blockUI.js" type="text/javascript"></script>

    
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/chosen.jquery.min.js" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/") %>Scripts/autoNumeric.js" type="text/javascript"></script>

    <style type="text/css">

        .required { background-color:#fff7dd; border:1px solid #4e4e4e; }
        .clear { clear:both }
        td[mrk="lud_table_tr_td"]:hover {  background-color:#ededed;   } 
                
    </style>
     
    <script language="javascript" type="text/javascript" >
        var year = "<%: year %>";
    </script>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                         <b>Sales Target Maintenance</b>
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
                <li><a href="#tabs-2">Sales Target</a></li>
                <li><a href="#tabs-1">Add/Update</a></li>
                <li><a href="<%:ResolveUrl("~/") %>Maintenance/getSalesTargetLogChanges">Log Changes</a></li>
            </ul>
            <div id="tabs-1">
	            <div id="main_div">
		                <div>
			                <table>
				                <tr>
                                    <td>Year</td>
                                    <%--<td><input type="text" id="txt_year" style="width:60px"/></td>--%>
                                    <td><select id="select_year"></select></td>
				                </tr>
                                <tr>
                                    <td>Month</td>
                                    <td>
                                        <select id="select_month">
                                            <option value="january">January</option>
                                            <option value="february">February</option>
                                            <option value="march">March</option>
                                            <option value="april">April</option>
                                            <option value="may">May</option>
                                            <option value="june">June</option>
                                            <option value="july">July</option>
                                            <option value="august">August</option>
                                            <option value="september">September</option>
                                            <option value="october">October</option>
                                            <option value="november">November</option>
                                            <option value="december">December</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>SO Name</td>
					                <%--<td><input type="text" id="txt_soname"/><label> <img alt="Drop down list of employee" id="search_name" src="<%=ResolveUrl("~/") %>Images/magnifier.png" /> </label></td>--%>
                                    <td> <label> <img alt="" src="<%=ResolveUrl("~/") %>Images/resultset_previous.png" onclick="javascript:previousSO()" /> </label> <select id="txt_soname"></select> <label> <img alt="" src="<%=ResolveUrl("~/") %>Images/resultset_next.png" onclick="javascript:nextSO()" /></label></td>
                                </tr>
                                <tr>
                                    <td>Current Sales Target</td>
                                    <td><input type="text" id="txt_cur_salestarget" style="background-color:#ededed; text-align:right;" readonly="readonly" /> <label>Status: <span id="txt_status" style="font:10px; font-family:italic; color:red"></span></label> </td>
                                </tr>
                                <tr>
                                    <td>New Sales Target:</td>
					                <td><input type="text" id="txt_amount" style="text-align:right"/></td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
						                <input type="button" value="Add" id="btn_add"/>
						                <input type="button" value="Update amount" id="btn_update"/>
						                <input type="button" value="Delete" id="btn_delete"/>
					                </td>
                                </tr>
				
			                </table>
		                </div>
		                <hr />
		               
                        <div>
                            <span> <a href="javascript:;" id="lnk_upload_salestarget"> Upload Sales Target </a>&nbsp;/&nbsp;<a href="<%:ResolveUrl("~/") %>Template/SalesTarget_template.xlsx" id="lnk_download_template"> Download template </a>&nbsp;</span>
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        <div style="float:left;" class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix">
                                            <h2>[Current Items]</h2>
			                                <table id="tbl_details">
                                                <thead>
				                                    <tr>
                                                        <th>Year</th>
                                                        <th>Month</th>
					                                    <th>SO Name</th>
                                                        <th>Current Sales Target</th>
					                                    <th>New Sales Target</th>
                                                        <th>Add/Update</th>
				                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr class="last_row">
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td><input type="text" style="width:60px" /></td>
                                                        <td><input type="text" style="width:80px"/></td>
                                                        <td><input type="text" /></td>
                                                        <td><input type="text"/></td>
                                                        <td><input type="text" /></td>
                                                        <td><input type="text" /></td>
                                                    </tr>
                                                </tfoot>
			                                </table>
		                                </div>
                                    </td>
                                    <td valign="top">
                                        <div style="float:right;" id="div_tbl_delete" class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix">
                                            <h2>[To be Remove Items]</h2>
			                                <table id="tbl_details_delete">
                                                <thead>
				                                    <tr>
					                                    <th>Description</th>
					                                    <th>Amount</th>
				                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr class="last_row">
                                                    </tr>
                                                </tbody>
			                                </table>
		                                </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                         <div class="div_blinker">
                            <input type="button" value="Save" id="btn_save" />
                            <input type="button" value="Cancel" id="btn_cancel" />       
                        </div>
                </div>
	        </div>
            <div id="tabs-2">
                     <div>
                         <table>
                            <tr>
                                <td>Year</td>
                                <td>
                                    <select id="select_listsalestarget_year">
                                        <option value="ALL">All</option>
                                    </select>
                                </td>
                                <td>Month</td>
                                <td>
                                    <select id="select_listsalestarget_month">
                                        <option value="ALL">All</option>
                                        <option value="JANUARY">January</option>
                                        <option value="FEBRUARY">February</option>
                                        <option value="MARCH">March</option>
                                        <option value="APRIL">April</option>
                                        <option value="MAY">May</option>
                                        <option value="JUNE">June</option>
                                        <option value="JULY">July</option>
                                        <option value="AUGUST">August</option>
                                        <option value="SEPTEMBER">September</option>
                                        <option value="OCTOBER">October</option>
                                        <option value="NOVEMBER">November</option>
                                        <option value="DECEMBER">December</option>
                                    </select>
                                </td>
                                 <td>SO Name</td> 
                                <td>
                                    <select id="select_listsalestarget_so">
                                        <option code="ALL">All</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <div style="float:left;" class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix">
			                            <table id="tbl_listofsalestarget">
                                            <thead>
				                                <tr>
                                                    <th>Year</th>
                                                    <th>Month</th>
					                                <th>SO Name</th>
					                                <th>Amount</th>
				                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="last_row">
                                                </tr>
                                            </tbody>
			                            </table>
		                            </div>
                                </td>
                              </tr>
                        </table>
                    </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="lookup_code" />
</asp:Content>
