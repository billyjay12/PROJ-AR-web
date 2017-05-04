<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="ARMS_W.Class" %><%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
		// delete previously uploaded files
		try 
		{
			UploadHelper.DeletePrevFiles(UploadHelper.CcaUploadDirectory + Session["username"] + "\\"); 
		}
		catch ( Exception ex ) 
		{
			Response.Write("\n<!-- ");
			Response.Write(ex.Message);
			Response.Write(" -->\n");
		}
        
        // get some data from Lead
        DataTable customerLeadI;
        customerLeadI = SqlDbHelper.getDataDT("select * from customerLeadI where RequestId='" + Request.QueryString["RequestId"].ToString() + "'");
        DataRow CustomerLeadI_ROW = customerLeadI.Rows[0];
        
	%>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
	<link href="<%=ResolveUrl("~/") %>Content/WalkInForm.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/AcctCreateLeadFinalForm.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
            $("#tabs").tabs();
            $("#sub_tab").tabs({
				select: function (event, ui) {
					clear_values_sub_tab();
				}
			});

            LoadInitialData();

            // initital required field
             SwitchRequiredFields($("#slt_acct_classification").attr("value"));

            $("#slt_acct_classification").click(
                function (){
                    RemoveClass();
                    SwitchRequiredFields($("#slt_acct_classification").attr("value"));
                }
            );

            $("#acc_type_direct").change(
                function (){
                    //SwitchR();
                    SwitchRequiredFields($("#slt_acct_classification").attr("value"));
                }
             );

             $("#acc_type_indirect").change(
                function (){
                    //SwitchR();
                    SwitchRequiredFields($("#slt_acct_classification").attr("value"));
                }
             );

            // KEYPRESS
            BindToTextFormatting("txt_credit_limit");
            BindToTextFormatting("txt_order_limit_ab");
            BindToTextFormatting("txt_order_limit_tr");

        });

        function LoadInitialData(){
            $("#txt_acct_code").attr("value", "<%: CustomerLeadI_ROW["sapleadcode"].ToString() %>");
            $("#txt_acct_name").attr("value", "<%: CustomerLeadI_ROW["name"].ToString() %>");
            $("#txt_business_add").attr("value", "<%: CustomerLeadI_ROW["address"].ToString() %>");
            $("#txt_phone_no").attr("value", "<%: CustomerLeadI_ROW["contactno"].ToString() %>");
            $("#txt_email_add").attr("value", "<%: CustomerLeadI_ROW["e_mail"].ToString() %>");
            $("#txt_acct_officer").attr("value", "<%: CustomerLeadI_ROW["SalesOfficer"].ToString() %>");
            $("#txt_request_id").attr("value", "<%: CustomerLeadI_ROW["RequestId"].ToString() %>");
        }

        function clear_values_sub_tab() {
			$("#txt_sole_owner_name").attr('value', '');
			$("#txt_sole_nationality").attr('value', '');
			$("#txt_sole_gen_manager").attr('value', '');
			$("#txt_sole_fin_manager").attr('value', '');
			$("#txt_sole_others").attr('value', '');

			// list_of_partner
			var rowid = $("#tbl_partner_list tr").length - 2;
			for (var i = 0; i < rowid; i++) {
				$("#tbl_partner_list tr:nth-child(2)").remove();
			}

			$("#txt_partner_gen_manager").attr('value', '');
			$("#txt_partner_fin_manager").attr('value', '');
			$("#txt_partner_others").attr('value', '');
			
            $("#txt_corpo_inc_date").attr('value', '');
            var d = new Date();
            var curr_year = d.getFullYear();
			$("#txt_corpo_inc_date").datepicker({ changeMonth: true, changeYear: true, yearRange: "1940:" + curr_year.toString() });

			$("#txt_corpo_auth_cap_stock").attr('value', '');
			$("#txt_corpo_subscb_cap_stock").attr('value', '');
			$("#txt_corpo_paidin_cap_stock").attr('value', '');

			// list_major_stockholder
			rowid = $("#tbl_corpo_list tr").length - 2;
			for (var i = 0; i < rowid; i++) {
				$("#tbl_corpo_list tr:nth-child(2)").remove();
			}

			$("#txt_corpo_ceo").attr('value', '');
			$("#txt_corpo_vp_fin").attr('value', '');
			$("#txt_corpo_gen_man").attr('value', '');
		}

    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Reclass to Customer</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div id="tabs">
	<ul>
		<li><a href="#tabs-1">Main Info</a></li>
		<li><a href="#tabs-2">Business</a></li>
		<li><a href="#tabs-3">Products</a></li>
		<li style="display:none;"><a href="#tabs-4">Credit Invistigation Info</a></li>
	</ul>
	<div id="tabs-1">
		<!-- TAB-1 CONTENT START -->
		<table width="100%" cellpadding="1" cellspacing="0" border="0">
            <tr>
				<td><!-- Request ID --></td>
				<td align="left" colspan="4">
					<input type="hidden" id="txt_request_id" />
				</td>
			</tr>
			<tr>
				<td>Account Classification</td>
				<td align="left" colspan="4">
					<select id="slt_acct_classification">
                        <option value="REGULAR" >Regular</option>
                        <option value="WALKIN" >Walk-In</option>
                    </select>
				</td>
			</tr>
			<tr>
				<td>Account Type</td>
				<td align="left" colspan="4">
					<label><input type="radio" id="acc_type_direct" name="option1" value="Direct" checked="checked" />Direct</label>
					&nbsp;&nbsp;
					<label><input type="radio" id="acc_type_indirect" name="option1" value="Indirect" />Indirect</label>
				</td>
			</tr>
			<tr>
				<td>Key Account</td>
				<td colspan="4" align="left">
					<label><input type="radio" id="acc_key_yes" name="option2" />Yes</label> &nbsp;
					<label><input type="radio" id="acc_key_no" name="option2" checked="checked" />No</label>
				</td>
			</tr>
			<tr>
				<td>Proposed Account Code</td>
				<td>
					<input type="text" id="txt_acct_code" maxlength="15" />
				</td>
				<td></td>
				<td>
                    <!-- Account Class -->
                    Account Officer
                </td>
				<td>
					<input type="hidden" onclick="javascript:LookUpData('txt_acct_class', 'ListOfBPClass');" id="txt_acct_class" name="txt_acct_class" readonly="readonly"  />
                    <input type="text" onclick="javascript:LookUpData('txt_acct_officer', 'ListOfSo');" id="txt_acct_officer" name="txt_acct_officer" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Account Name</td>
				<td>
					<input type="text" id="txt_acct_name" />
				</td>
				<td></td>
				<td>Territory</td>
				<td>
                    <input type="text" onclick="javascript:LookUpData('txt_acct_territory', 'ListOfTerritory');" id="txt_acct_territory" name="txt_acct_territory" readonly="readonly" />
                </td>
			</tr>
			<tr>
				<td>Fax</td>
				<td>
					<input type="text" id="txt_fax_no" maxlength="20" />
				</td>
				<td></td>
				<td>Area</td>
				<td>
					<input type="text" id="txt_area" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Phone no.</td>
				<td>
                    <input type="text" id="txt_phone_no" maxlength="20" />
				</td>
				<td></td>
				<td>Region</td>
				<td>
					<input type="text" id="txt_region" readonly="readonly" />
				</td>
			</tr>
            <tr>
				<td>Phone no. 2</td>
				<td>
					<input type="text" id="txt_phone_no_2" maxlength="20" />
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
            <tr>
				<td>Mobile</td>
				<td>
					<input type="text" id="txt_cellphone" maxlength="50" />
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>E-mail</td>
				<td>
					<input type="text" id="txt_email_add" />
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>Office Hours</td>
				<td>
					<input type="text" id="txt_office_hours" />
				</td>
				<td></td>
				<td>Account Category Value Brands</td>
				<td>
                    <input type="text" id="txt_category_value" readonly="readonly" onclick="javascript:LookUpData('txt_category_value', 'ListOfCategoryBrands');" />
				</td>
			</tr>
			<tr>
				<td>Store Hours</td>
				<td>
					<input type="text" id="txt_store_hours" />
				</td>
				<td></td>
				<td>Account Category Premium Brands</td>
				<td>
                    <input type="text" id="txt_category_prem" readonly="readonly" onclick="javascript:LookUpData('txt_category_prem', 'ListOfCategoryBrands');" />
				</td>
			</tr>
			<tr>
				<td>No. of yrs in Business</td>
				<td>
					<input type="text" id="txt_yrs_business" />
				</td>
				<td></td>
				<td>Business Classification</td>
				<td>
                    <input type="text" id="txt_buss_class" readonly="readonly" onclick="javascript:LookUpData('txt_buss_class', 'ListOfBusinessClass');" />
                </td>
			</tr>
			<tr>
				<td>No. of yrs w/ Matimco Inc.</td>
				<td>
					<input type="text" id="txt_yrs_matimco" />
				</td>
				<td></td>
				<td>Type of Account</td>
                <td>
                    <input type="text" id="txt_type_of_account" readonly="readonly"  onclick="javascript:LookUpData('txt_type_of_account', 'ListOfTypeOfAccounts');" />
                </td>
			</tr>
			<tr>
				<td>Tax ID</td>
				<td>
					<input type="text" id="txt_tax_id" onkeypress="return txt_tax_id_onkeypress(event)"/>
				</td>
				<td></td>
				<td>VAT Type</td>
				<td>
					<input type="text" id="txt_vat_no" onclick="javascript:LookUpData('txt_vat_no', 'ListOfVat');" readonly="readonly" />
				</td>
			</tr>
			<tr>
				<td>Registered Name</td>
				<td colspan="4">
					<input type="text" id="txt_reg_name" />
				</td>
			</tr>
			<tr>
				<td>Business Address</td>
				<td colspan="4">
					<input type="text" id="txt_business_add" style="width:99%;" />
				</td>
			</tr>
			<tr>
				<td>Delivery Address</td>
				<td colspan="4">
					<input type="text" id="txt_delivery_add" style="width:99%;" />
				</td>
			</tr>
			<tr>
				<td colspan="5" >
					<!-- BUSINESS TYPE - START -->
					<div class="margin_box" style="background-color:#ededed;">

						<div class="simple_box" style="font-weight:bold; font-size:11px; background-color:#fff9df; color:#000000; border:1px solid #ffe787;">
							
								<table cellspacing="0" cellpadding="0" border="0">
								<tr>
									<td valign="middle"><img src="<%=ResolveUrl("~/") %>Images/information.png" style="border:0;" /></td>
									<td valign="middle">&nbsp;&nbsp;</td>
									<td valign="middle"><b>Note:</b></td>
								</tr>
								</table>
								<i> 
									Under business type [Sole Proprietorship/Partnership/Corporation], 
									click first the [TAB] before filling in the info/data.
									Clicking other businness type will erase the info/data under 
									business type.
								</i>
						</div>

						<div id="sub_tab">
							<ul>
								<li><a href="#tabs-11">Sole Proprietorship</a></li>
								<li><a href="#tabs-12">Partnership</a></li>
								<li><a href="#tabs-13">Corporation</a></li>
							</ul>
							<div id="tabs-11">
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>Name of Owner</td>
										<td>
											<input type="text" id="txt_sole_owner_name" />
										</td>
									</tr>
									<tr>
										<td>Nationality</td>
										<td>
											<input type="text" id="txt_sole_nationality" />
										</td>
									</tr>
									<tr>
										<td>General Manager</td>
										<td>
											<input type="text" id="txt_sole_gen_manager" />
										</td>
									</tr>
									<tr>
										<td>Finance Manager</td>
										<td>
											<input type="text" id="txt_sole_fin_manager" />
										</td>
									</tr>
									<tr>
										<td>Others</td>
										<td>
											<input type="text" id="txt_sole_others" />
										</td>
									</tr>
								</table>
							</div>
							<div id="tabs-12">
								<table id="tbl_partner_list" cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td align="center">Partner</td>
										<td align="center">Nationality</td>
										<td align="center">Contributed Capital</td>
										<td></td>
									</tr>
									<tr>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td><a href="javascript:AddEntryPartnership();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
									</tr>
								</table>
								<br />
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>General Manager</td>
										<td><input type="text" id="txt_partner_gen_manager" /></td>
									</tr>
									<tr>
										<td>Finance Manager</td>
										<td><input type="text" id="txt_partner_fin_manager" /></td>
									</tr>
									<tr>
										<td>Others</td>
										<td><input type="text" id="txt_partner_others" /></td>
									</tr>
								</table>
							</div>
							<div id="tabs-13">
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>Date of Incorporation</td>
										<td><input type="text" id="txt_corpo_inc_date" readonly="readonly" /></td>
									</tr>
									<tr>
										<td>Authorized Capital Stock</td>
										<td><input type="text" id="txt_corpo_auth_cap_stock" /></td>
									</tr>
									<tr>
										<td>Subscribed Capital Stock</td>
										<td><input type="text" id="txt_corpo_subscb_cap_stock" /></td>
									</tr>
									<tr>
										<td>Paid-In Capital Stock</td>
										<td><input type="text" id="txt_corpo_paidin_cap_stock" /></td>
									</tr>
								</table>
								<br />
								<table id="tbl_corpo_list" cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td align="center">Major Stockholders</td>
										<td align="center">Nationality</td>
										<td align="center">%Owned</td>
										<td></td>
									</tr>
									<tr>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td align="center"><input type="text" /></td>
										<td><a href="javascript:AddEntryCorporation();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
									</tr>
								</table>
								<br />
								<table cellpadding="1" cellspacing="0" border="0">
									<tr>
										<td>President / CEO</td>
										<td><input type="text" id="txt_corpo_ceo" /></td>
									</tr>
									<tr>
										<td>VP Finance</td>
										<td><input type="text" id="txt_corpo_vp_fin" /></td>
									</tr>
									<tr>
										<td>General Manager</td>
										<td><input type="text" id="txt_corpo_gen_man" /></td>
									</tr>
								</table>
							</div>
						</div>

					</div>
					<!-- BUSINESS TYPE - END -->
				</td>
			</tr>
			<tr>
				<td colspan="5" style="padding:5px;">
					<div class="simple_box">
						<table id="tbl_emp_pos_list" cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td>No. of Employees</td>
								<td><input type="text" id="txt_no_employees" /></td>
							</tr>
							<tr>
								<td align="center">Position</td>
								<td align="center">No. of Employees</td>
								<td></td>
							</tr>
							<tr>
								<td><input type="text" /></td>
								<td><input type="text" /></td>
								<td><a href="javascript:AddEntryEmployeePos();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="5" style="padding:5px;">
					<div class="simple_box">
						<h2>Attachments</h2>
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td>Pre-Enrollment Documents</td>
								<td></td>
                                <td></td>
							</tr>
							<tr>
								<td>
                                    <!-- Articles of Incorporation -->
                                    Customer Info Sheet
                                </td>
								<td>
									<input type="text" id="txt_articles_of_inc" name="txt_articles_of_inc" onclick="javascript:CreateUploadingBox('txt_articles_of_inc');" readonly="readonly" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('AOI');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
							</tr>
							<tr>
								<td>
                                    <!-- Financial Statements -->
                                    Bank Authorization
                                </td>
								<td>
									<input type="text" id="txt_financial_statement" name="txt_financial_statement" onclick="javascript:CreateUploadingBox('txt_financial_statement');" readonly="readonly" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('FS');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
							</tr>
							<tr>
								<td>Income Tax Return</td>
								<td>
									<input type="text" id="txt_ITR" name="txt_ITR" onclick="javascript:CreateUploadingBox('txt_ITR');" readonly="readonly" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('ITR');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
							</tr>
							<tr>
								<td>BIR Registration</td>
								<td>
									<input type="text" id="txt_bir_reg" name="txt_bir_reg" onclick="javascript:CreateUploadingBox('txt_bir_reg');" readonly="readonly" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('BIR');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
							</tr>
							<tr>
								<td>Latest Business Permit</td>
								<td>
									<input type="text" id="txt_business_permit" name="txt_business_permit" onclick="javascript:CreateUploadingBox('txt_business_permit');" readonly="readonly" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('BP');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
							</tr>
                            <tr>
								<td>Other</td>
								<td>
									<input type="text" id="txt_attch_other" name="txt_attch_other" onclick="javascript:CreateUploadingBox('txt_attch_other');" readonly="readonly" />
								</td>
                                <td>
                                    <a href="javascript:;" onclick="javascript:DeleteFileAttachment('OTHER');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
                                </td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
		</table>
        <div ><i style="color:Red; font-size: 10">To fast track your account approval process, please provide 
             complete bank information</i> </div>
		<!-- TAB-1 CONTENT END -->
	</div>
	<div id="tabs-2">
		<!-- TAB-2 CONTENT START -->
		<%--<table cellpadding="1" cellspacing="0" border="0">
            <tr>
                <td></td>
                <td></td>
                <td align="center">Remarks</td>
            </tr>
			<tr>
				<td>Proposed Credit Terms</td>
				<td><input type="text" id="txt_credit_terms" onclick="javascript:LookUpData('txt_credit_terms', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
                <td><input type="text" id="txt_credit_terms_remarks" /></td>
			</tr>
			<tr>
				<td>Proposed Credit Limit</td>
				<td><input type="text" id="txt_credit_limit" value="0" /></td>
                <td><input type="text" id="txt_credit_limit_remarks" /></td>
			</tr>
		</table>--%>
         <table cellpadding="1" cellspacing="0" border="0">
            <tr>
                <td>Initial Purchase Order Details: </td>
                <td></td>
                <td></td>
            </tr>
            <tr valign="top" style="height:50px">
                <td colspan="3"><textarea rows="3" cols="3" id="txt_ini_po_details" style="width:100%; height:100%"></textarea></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td align="center">Remarks</td>
            </tr>
			<tr>
				<td>Proposed Credit Limit</td>
				<td><input type="text" id="txt_credit_limit" value="0" /></td>
                <td><input type="text" id="txt_credit_limit_remarks" /></td>
			</tr>
             <tr>
				<td>Proposed AB Order Limit</td>
				<td><input type="text" id="txt_order_limit_ab" value="0" /></td>
                <td><input type="text" id="txt_order_limit_remarks_ab" /></td>
			</tr>
            <tr>
				<td>Proposed TR Order Limit</td>
				<td><input type="text" id="txt_order_limit_tr" value="0" /></td>
                <td><input type="text" id="txt_order_limit_remarks_tr" /></td>
			</tr>
			<tr>
				<td>Proposed Credit Terms</td>
				<td><input type="text" id="txt_credit_terms" onclick="javascript:LookUpData('txt_credit_terms', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
                <td><input type="text" id="txt_credit_terms_remarks" /></td>
			</tr>
            <tr>
                <td>Proposed Credit Terms:</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-indent:30px">Architectural Brand </td>
                <td><input type="text" id="txt_credit_terms_architectural_brand" onclick="javascript:LookUpData('txt_credit_terms_architectural_brand', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
                <td><input type="text" id="txt_credit_terms_architectural_brand_remarks" /></td>
            </tr>
            <tr>
                <td style="text-indent:30px ">Ecofor Lumber </td>
                <td><input type="text" id="txt_credit_terms_eco_lumber" onclick="javascript:LookUpData('txt_credit_terms_eco_lumber', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
                <td><input type="text" id="txt_credit_terms_eco_lumber_remarks" /></td>
            </tr>
            <tr>
                <td style="text-indent:30px">Ecofor Plywood </td>
                <td><input type="text" id="txt_credit_terms_eco_plywood" onclick="javascript:LookUpData('txt_credit_terms_eco_plywood', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
                <td><input type="text" id="txt_credit_terms_eco_plywood_remarks" /></td>
            </tr>
		</table>
        <br />
		<table cellpadding="1" cellspacing="0" border="0">
			<tr>
				<td align="center">Proposed Price Lists</td>
				<td align="center">Code</td>
				<td align="center">Description</td>
				<td align="center">Remarks</td>
			</tr>
			<tr>
				<td>Matwood</td>
				<td><input type="text" id="txt_mw_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_mw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_mw_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_mw_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_mw_price_remarks" style="width:300px;" /></td>
			</tr>
			<tr>
				<td>WeatherWood</td>
				<td><input type="text" id="txt_ww_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_ww_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_ww_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_ww_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_ww_price_remarks" style="width:300px;" /></td>
			</tr>
            <tr>
				<td>PCW Frames</td>
				<td><input type="text" id="txt_pwf_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_pwf_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_pwf_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_pwf_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_pwf_price_remarks" style="width:300px;" /></td>
			</tr>
            <tr>
				<td>PCW Regular Items</td>
				<td><input type="text" id="txt_pwr_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_pwr_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_pwr_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_pwr_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_pwr_price_remarks" style="width:300px;" /></td>
			</tr>
			<tr>
				<td>GudWood</td>
				<td><input type="text" id="txt_gw_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_gw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_gw_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_gw_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_gw_price_remarks" style="width:300px;" /></td>
			</tr>
            <tr>
				<td>TrussWood</td>
				<td><input type="text" id="txt_tw_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_tw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_tw_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_tw_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_tw_price_remarks" style="width:300px;" /></td>
			</tr>
               <tr>
				<td>MuzuWood</td>
				<td><input type="text" id="txt_mz_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_mz_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_mz_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_mz_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_mz_price_remarks" style="width:300px;" /></td>
			</tr>
            <tr>
				<td>NuWood</td>
				<td><input type="text" id="txt_nw_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_nw_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_nw_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_nw_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_nw_price_remarks" style="width:300px;" /></td>
			</tr>
            <tr>
				<td>Ecofor (Treated)</td>
				<td><input type="text" id="txt_ec_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_ec_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_ec_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_ec_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_ec_price_remarks" style="width:300px;" /></td>
			</tr>
            <tr>
				<td>Ecofor (UnTreated)</td>
				<td><input type="text" id="txt_ecu_price_code" style="width:100px;" onclick="javascript:LookUpData('txt_ecu_price_code', 'ListOfPriceCode');" readonly="readonly" /></td>
				<td><input type="text" id="txt_ecu_price_desc" readonly="readonly" style="width:130px;" /></td>
                <td><input type="text" id="txt_ecu_price_commision_disc" style="text-align:right; width:50px;" /> <b>%</b> </td>
				<td><input type="text" id="txt_ecu_price_remarks" style="width:300px;" /></td>
			</tr>
		</table>
		<br />
		<div class="simple_box">
			Socio Economic Class of Customers: <input type="text" id="txt_eco_class_of_customer" /> <br />
			Number of Outlets: <input type="text" id="txt_no_of_outlets" /> <br />
		</div><br />
		<div class="simple_box">
			Outlets <br />
			<table id="tbl_outlet_list" width="100%" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Name of Outlets</td>
					<td align="center">Location</td>
					<td align="center">Store Size</td>
					<td align="center">Warehouse Size</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryOutlet();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Major Customers <br />
			<table id="tbl_mjcust_list" width="100%" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Name</td>
					<td align="center">Address</td>
					<td align="center">Selling Terms</td>
					<td align="center">Est. Monthly Purchases</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryMajorCustomer();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<!-- TAB-2 CONTENT END -->
	</div>
	<div id="tabs-3">
		<table width="100%" cellpadding="1px" cellspacing="0" border="0">
			<tr>
				<td>Major Line</td>
				<td><input type="text" id="txt_prod_major_line" /></td>
			</tr>
			<tr>
				<td>Other Product Lines</td>
				<td><input type="text" id="txt_prod_other_line" /></td>
			</tr>
		</table>
		<div class="simple_box">
			Construction Materials
			<table cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td></td>
					<td align="center">Top Suppliers</td>
				</tr>
				<tr>
					<td>Plywood</td>
					<td><input type="text" id="txt_const_mat_plywood" /></td>
				</tr>
				<tr>
					<td>Steel</td>
					<td><input type="text" id="txt_const_mat_steel" /></td>
				</tr>
				<tr>
					<td>Cement</td>
					<td><input type="text" id="txt_const_mat_cement" /></td>
				</tr>
				<tr>
					<td>Concrete HollowBlock</td>
					<td><input type="text" id="txt_const_mat_hb" /></td>
				</tr>
				<tr>
					<td>Others</td>
					<td><input type="text" id="txt_const_mat_others" /></td>
				</tr>
			</table>
		</div>
		<table cellpadding="1px" cellspacing="0" border="0">
			<tr>
				<td>Major Volume/ Value Drivers of the Business</td>
				<td><input type="text" id="txt_major_vol_business" /></td>
			</tr>
			<tr>
				<td>Monthly Wood Volume/Value</td>
				<td><input type="text" id="txt_wood_vol" /></td>
			</tr>
			<tr>
				<td>Discounts Enjoyed</td>
				<td><input type="text" id="txt_discount_enjoyed" /></td>
			</tr>
		</table>
		<div class="simple_box">
			Other Wood Suppliers
			<table id="tbl_wood_supplier" width="100%" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Supplier</td>
					<td align="center">Monthly Volume/Value</td>
					<td align="center">Contact Person</td>
					<td align="center">Contact Number</td>
					<td align="center">Products Usually Purchased</td>
					<td align="center">Credit Terms</td>
					<td align="center">Other Deals Offered</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryWoodSupplier();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
	</div>
	<div id="tabs-4">
        <div class="simple_box" style="border:none;"><i style="color:Red; font-size: 12">The information under this tab is confidential and should not be shared with the customer.</i> </div>
		
		<div class="simple_box">
			Bank
			<table width="100%" id="tbl_bank_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Bank</td>
					<td align="center">Branch</td>
					<td align="center">Address</td>
					<td align="center">Acct. No.</td>
					<td align="center">Contact No.</td>
					<td align="center">Contact Person</td>
					<td align="center">Avg. Daily Bal.</td>
					<td align="center">Remarks</td>
					<td align="center"></td>
				</tr>
				<tr>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><input style="width:98%;" type="text" /></td>
					<td><a href="javascript:AddEntryBank();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Land
			<table id="tbl_land_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Type</td>
					<td align="center">Area (sq. m.)</td>
					<td align="center">Location</td>
					<td align="center">Owned by</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryLands();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Building
			<table id="tbl_building_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Type</td>
					<td align="center">Area (sq. m.)</td>
					<td align="center">Location</td>
					<td align="center">Owned by</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryBuildings();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Vehicles
			<table id="tbl_vehicle_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Type</td>
					<td align="center">Model</td>
					<td align="center">Quantity</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryVehicles();"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="simple_box">
			Other Assets: <input type="text" id="txt_other_assets" />
		</div>
	</div>
	</div>
    </div>

    <hr />

<center>
	<input type="button" value="Save" onclick="javascript:Save_Doc();" /> 
	&nbsp;&nbsp; / &nbsp;&nbsp;
	<input type="button" value="Cancel" onclick="javascript:Cancel();" /> 
</center>
</asp:Content>
