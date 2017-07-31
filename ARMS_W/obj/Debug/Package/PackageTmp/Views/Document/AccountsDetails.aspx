<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %><%@ Import Namespace="ARMS_W.SkelClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<% 
		
		string DocCCaNum = Request.QueryString["ccanum"].ToString();
		const int IS_NOT_FOUND = -1;
		string CardCode = "";
		bool personalInfo_tab_access_only = false;
		
		ARMS_W.SkelClass.Document.AccountDetails acct_dtls = (ARMS_W.SkelClass.Document.AccountDetails)ViewData["AccountDetailsInfo"];
		
		_User oUsr = (_User)Session["Ousr"];
		_Document oDocumnt = new _Document("CCA", DocCCaNum);
		
		CardCode = acct_dtls.CardCode;
						
		string cca_region = "";
		
		   
		string CurrDocMessage = AppHelper.GetAccDocStatusMessage(acct_dtls.curr_doc_DocStatus /*customerheader*/, acct_dtls.curr_doc_DocChangesStatus/*proposedchangesca*/, DocCCaNum, "", acct_dtls.proposedChangesCA.is_sent_back);

		// ADDED TO ALLOW Personal Info Tab ONLY   
		if (
			acct_dtls.curr_doc_DocStatus == "1000" &&
			acct_dtls.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("csr") &&
			(
				oUsr.HasPositionOf("sim") != IS_NOT_FOUND
			)
			&&
			(
				oUsr.HasRegionOf("sim", oDocumnt.Region) == true
			)
			)
		{
			personalInfo_tab_access_only = true;
		}
	  
	%>



	<link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
	<link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/AccountsDetails.js" type="text/javascript"></script>

	<script type="text/javascript" language="javascript">
		
		var baseUrl = "<%= ResolveUrl("~/") %>";
		var isAllDataLoaded = false;

		var customerHeaderStatus = '<%:acct_dtls.curr_doc_DocStatus %>';

		var mouse_click_marking = "FALSE";
		var personalInfo_tab_access_only_ = '<%:personalInfo_tab_access_only %>';

		SetBubbles();

		var g_number_of_banks = 0;
		g_number_of_banks = <%: acct_dtls.depositoryBanks.Count %>;        

		var g_acct_code = "";
		var mtd_sales = "";
		var grp_name = "";

		/* other data to TRACK */
		var g_list_of_partners = "";
		var g_list_of_corpos = "";
		var g_list_of_employees = "";
		var g_list_of_outlets = "";
		var g_list_of_majorcustomers = "";
		var g_list_of_wood_suppliers = "";
		var g_list_of_banks = "";
		var g_list_of_lands = "";
		var g_list_of_buildings = "";
		var g_list_of_vehicles = "";
		var g_list_of_other_business = "";
		var g_list_of_events = "";

		
		var g_list_of_activities = "";

		/* used to track changes */
		var g_acct_category_value = "";
		var g_acct_category_prem = "";
		var g_acct_BusinessClass = "";
		var g_acct_TransType = "";

		var g_acct_name = "";
		var g_acct_acct_officer  = "";
		var g_acct_territory = "";
		var g_acct_area = "";
		var g_acct_region = "";
		var g_acct_phone_no = "";
		var g_acct_phone_no_2 = "";
		var g_acct_cellphone = "";
		var g_acct_reg_name = "";
		var g_acct_business_add = "";
		var g_acct_delivery_add = "";

		/* Code added by Billy Jay (04/23/2015) */
		var g_acct_iniPOdetails = "";
		var g_acct_prop_credit_term_architectural_brand = "";
		var g_acct_prop_credit_term_ecofor_lumber = "";
		var g_acct_prop_credit_term_ecofor_plywood = "";

		var g_acct_prop_credit_term_remarks_architectural_brand = "";
		var g_acct_prop_credit_term_remarks_ecofor_lumber = "";
		var g_acct_prop_credit_term_remarks_ecofor_plywood = "";
		
		var g_acct_prob_order_limit_ab = "";
		var g_acct_prob_order_limit_tr = "";

		var g_acct_prob_order_limit_remarks_ab = "";
		var g_acct_prob_order_limit_remarks_tr = "";
		/* Code added by Billy Jay (04/23/2015) */

		var g_acct_prop_credit_term = "";
		var g_acct_prop_credit_limit = "";
		var g_acct_prop_credit_term_remarks = "";
		var g_acct_prop_credit_limit_remarks = "";
		var g_acct_mw_price_code = "";
		var g_acct_mw_price_desc = ""; 
		var g_acct_mw_price_commision_disc = "";
		var g_acct_mw_price_remarks = "";
		var g_acct_ww_price_code = "";
		var g_acct_ww_price_desc = "";
		var g_acct_ww_price_commision_disc = "";
		var g_acct_ww_price_remarks = "";
		var g_acct_pwf_price_code = "";
		var g_acct_pwf_price_desc = "";
		var g_acct_pwf_price_commision_disc = "";
		var g_acct_pwf_price_remarks = "";
		var g_acct_pwr_price_code = "";
		var g_acct_pwr_price_desc = "";
		var g_acct_pwr_price_commision_disc = "";
		var g_acct_pwr_price_remarks = "";
		var g_acct_gw_price_code = "";
		var g_acct_gw_price_desc = "";
		var g_acct_gw_price_commision_disc = "";
		var g_acct_gw_price_remarks = "";
		var g_acct_tw_price_code = "";
		var g_acct_tw_price_desc = "";
		var g_acct_tw_price_commision_disc = "";
		var g_acct_tw_price_remarks = "";

		var g_acct_mz_price_code = "";
		var g_acct_mz_price_desc = "";
		var g_acct_mz_price_commision_disc = "";
		var g_acct_mz_price_remarks = "";

		var g_acct_nw_price_code = "";
		var g_acct_nw_price_desc = "";
		var g_acct_nw_price_commision_disc = "";
		var g_acct_nw_price_remarks = "";

		var g_acct_ec_price_code = "";
		var g_acct_ec_price_desc = "";
		var g_acct_ec_price_commision_disc = "";
		var g_acct_ec_price_remarks = "";

		var g_acct_ecu_price_code = "";
		var g_acct_ecu_price_desc = "";
		var g_acct_ecu_price_commision_disc = "";
		var g_acct_ecu_price_remarks = "";

		$(function () {
			$("#tabs").tabs({ ajaxOptions: { cache: false }, cache: true });
			$("#sub_tab").tabs({
				select: function (event, ui) {
					if(isAllDataLoaded){
						clear_values_sub_tab();
					   // alert("Triggered"); --commented oct 25 2013
					}
					
				}
			});
			
			LoadData();

			$("#slt_acct_classification").change(
				function (){ SwitchR(); }
			);

			 $("#acc_type_direct").change(
				function (){ SwitchR(); }
			 );

			 $("#acc_type_indirect").change(
				function (){ SwitchR(); }
			 );

			//$("#txt_corpo_inc_date").datepicker();
				// 
			var d = new Date();
			var curr_year = d.getFullYear();
			$("#txt_corpo_inc_date").datepicker({ changeMonth: true, changeYear: true, yearRange: "1940:" + curr_year.toString() });
			
			SwitchR();

			// KEYPRESS
			BindToTextFormatting("txt_credit_limit");
			BindToTextFormatting("txt_order_limit_ab");
			BindToTextFormatting("txt_order_limit_tr");

			BindApproverButtons();

			LoadAcountStatus();

			if(personalInfo_tab_access_only_=="True") {
				PeronalInfoTabAccessOnly();
			}

		});

		function PeronalInfoTabAccessOnly(){

//          $("input").attr("disabled","disabled");
//          $("select").attr("disabled","disabled");
//          $("img").hide();

//          $("#tbl_birthday_events input[type=text]").removeAttr("disabled");
//          $("#tbl_specified_events input[type=text]").removeAttr("disabled");
//          
//          $("#tbl_birthday_events img").show();
//          $("#tbl_specified_events img").show();

			$("#tbl_birthday_events tr:last").show();
			$("#tbl_birthday_events tr td img").show();

			$("#tbl_specified_events tr:last").show();
			$("#tbl_specified_events tr td img").show();

		}

		function SwitchR(){
			RemoveClass();

			var acct_type = ""; // = $("#acc_type_direct").attr('checked') == true ? "direct": "indirect";
			
			if ($("#acc_type_direct").attr('checked') == 'checked'){
				acct_type = "direct";
			} else {
				acct_type = "indirect";
			}

			SwitchRequiredFields($("#slt_acct_classification").attr("value"), acct_type);

			$("#txt_acct_classification").attr("value", $("#slt_acct_classification").attr("value"));
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

		function LoadData() {
			var m_acct_category_value = "";
			var m_acct_category_prem = "";
			var m_acct_BusinessClass = "";
			var m_acct_TypeOfAccount = "";
			var m_acct_TransType = "";

			var m_acct_ccanum = "";
			var m_acct_type = "";
			var m_acct_classification = "";
			var m_acct_key_account = "";
			var m_acct_code = "";
			var m_acct_class = "";
			var m_acct_name = "";
			var m_acct_phone_no = "";
			var m_acct_phone_no_2 = "";
			var m_acct_cellphone = "";
			var m_acct_acct_officer = "";
			var m_acct_fax_no = "";
			var m_acct_territory = "";
			var m_acct_email = "";
			var m_acct_office_hours = "";
			var m_acct_area = "";
			var m_acct_store_hours = "";
			var m_acct_region = "";
			var m_acct_years_in_business = "";
			var m_acct_years_with_matimco = "";
			var m_acct_tax_id = "";
			var m_acct_vat_no = "";
			var m_acct_reg_name = "";
			var m_acct_business_add = "";
			var m_acct_delivery_add = "";
			var m_acct_num_employees = "";
			var m_final_acct_code = "";

			g_acct_code = "<% Response.Write(acct_dtls.acctCode); %>";

			mtd_sales = "<% Response.Write(acct_dtls.MTDSales); %>";
			grp_name = "<% Response.Write(acct_dtls.GrpName); %>";

			m_acct_ccanum = "<% Response.Write(acct_dtls.ccaNum); %>";
			m_acct_type = "<% Response.Write(acct_dtls.acctType); %>";
			m_acct_classification = "<% Response.Write(acct_dtls.acctClassfxn); %>";
			m_acct_key_account = "<% Response.Write(acct_dtls.keyAcct); %>";
			m_acct_code = "<% Response.Write(acct_dtls.acctCode); %>";
			m_acct_class = "<% Response.Write(acct_dtls.acctClass); %>";
			m_acct_name = "<% Response.Write(acct_dtls.acctName); %>";
			m_acct_phone_no = "<% Response.Write(acct_dtls.telNum); %>";

			m_acct_phone_no_2 = "<% Response.Write(acct_dtls.telNum2); %>";
			m_acct_cellphone = "<% Response.Write(acct_dtls.MobileNum); %>";

			m_acct_acct_officer = "<% Response.Write(acct_dtls.acctOffcr); %>";
			m_acct_fax_no = "<% Response.Write(acct_dtls.faxNum); %>";
			m_acct_territory = "<% Response.Write(acct_dtls.territory); %>";
			m_acct_email = "<% Response.Write(acct_dtls.emailAdd); %>";
			m_acct_office_hours = "<% Response.Write(acct_dtls.offceHrs); %>";
			m_acct_area = "<% Response.Write(acct_dtls.area); %>";
			m_acct_store_hours = "<% Response.Write(acct_dtls.storeHrs); %>";
			m_acct_region = "<% Response.Write(acct_dtls.region); %>";
			<% cca_region = acct_dtls.region; %>
			m_acct_years_in_business = "<% Response.Write(acct_dtls.yrsInBusiness); %>";
			m_acct_years_with_matimco = "<% Response.Write(acct_dtls.yrsWdMTC); %>";
			m_acct_tax_id = "<% Response.Write(acct_dtls.TIN); %>";
			m_acct_vat_no = "<% Response.Write(acct_dtls.VATregNum); %>";
			m_acct_reg_name = "<% Response.Write(acct_dtls.regBusName); %>";
			m_acct_business_add = "<% Response.Write(acct_dtls.bussAdd); %>";
			m_acct_delivery_add = "<% Response.Write(acct_dtls.delAdd); %>";
			m_acct_num_employees = "<% Response.Write(acct_dtls.TotalNumOfEmp); %>";
			m_final_acct_code = "<% Response.Write(acct_dtls.SapAcctCode); %>";
				
			m_acct_category_value = "<% Response.Write(acct_dtls.acctCategoryVal); %>";
			m_acct_category_prem = "<% Response.Write(acct_dtls.acctCategoryPrem); %>";
			
			m_acct_BusinessClass = "<% Response.Write(acct_dtls.acctBusinessClass); %>";

			m_acct_TypeOfAccount = "<% Response.Write(acct_dtls.acctTypeOfAccount); %>";

			// required fields
			// SwitchRequiredFields(m_acct_classification);

			$("#txt_mtd_sales").attr("value",mtd_sales);

			$("#txt_acct_territory").attr("grp_name_",grp_name);
			
			if (m_acct_type == "DIRECT"){
				$("#acc_type_direct").attr('checked', 'checked');
			} else {
				$("#acc_type_indirect").attr('checked', 'checked');
			}
			
			if (m_acct_key_account == "False" || m_acct_key_account == "0" || m_acct_key_account == ""){
				$("#acc_key_no").attr('checked', 'checked');
			} else {
				$("#acc_key_yes").attr('checked', 'checked');
			}

			$("#txt_acct_ccanum").attr('value', m_acct_ccanum);
			$("#txt_acct_classification").attr('value', m_acct_classification);
			
			$("#slt_acct_classification option").removeAttr("selected");
			$("#slt_acct_classification option[value=" + m_acct_classification + "]").attr('selected', 'selected');

			// SAVE ORIGINAL VALUE
			$("#txt_acct_classification").attr('orig_value', m_acct_classification);
			$("#txt_category_value").attr("value", m_acct_category_value);
			$("#txt_category_prem").attr("value", m_acct_category_prem);
			$("#txt_buss_class").attr('value', m_acct_BusinessClass);
			$("#txt_type_of_account").attr({
				'value': m_acct_TypeOfAccount,
				'value_id':(m_acct_TypeOfAccount=='Retained Account'?'R':'U')
			});
			$("#txt_acct_code").attr('value', m_acct_code);
			$("#txt_acct_class").attr('value', m_acct_class);
			$("#txt_acct_name").attr('value', m_acct_name);
			$("#txt_phone_no").attr('value', m_acct_phone_no);
			$("#txt_phone_no_2").attr('value', m_acct_phone_no_2);
			$("#txt_cellphone").attr('value', m_acct_cellphone);
			$("#txt_acct_officer").attr('value', m_acct_acct_officer);
			$("#txt_fax_no").attr('value', m_acct_fax_no);
			$("#txt_acct_territory").attr('value', m_acct_territory);
			$("#txt_email_add").attr('value', m_acct_email);
			$("#txt_office_hours").attr('value', m_acct_office_hours);
			$("#txt_area").attr('value', m_acct_area);
			$("#txt_store_hours").attr('value', m_acct_store_hours);
			$("#txt_region").attr('value', m_acct_region);
			$("#txt_yrs_business").attr('value', m_acct_years_in_business);
			$("#txt_yrs_matimco").attr('value', m_acct_years_with_matimco);
			$("#txt_tax_id").attr('value', m_acct_tax_id);
			$("#txt_vat_no").attr('value', m_acct_vat_no);
			$("#txt_reg_name").attr('value', m_acct_reg_name);
			$("#txt_business_add").attr('value', m_acct_business_add);
			$("#txt_delivery_add").attr('value', m_acct_delivery_add);
			$("#txt_no_employees").attr('value', m_acct_num_employees);
			$("#txt_final_acct_code").attr('value', m_final_acct_code);

			if(m_final_acct_code == ""){
				$("#txt_final_account_code").attr('value', m_acct_code);
			} else {
				$("#txt_final_account_code").attr('value', m_final_acct_code);
			}

			/* SAVE ORIGINAL VALUES */
			$("#txt_buss_class").attr('orig_value', m_acct_BusinessClass);
			$("#txt_type_of_account").attr('orig_value', m_acct_TypeOfAccount);

			$("#txt_acct_code").attr('orig_value', m_acct_code);
			$("#txt_acct_class").attr('orig_value', m_acct_class);
			$("#txt_acct_name").attr('orig_value', m_acct_name);
			$("#txt_phone_no").attr('orig_value', m_acct_phone_no);
			$("#txt_phone_no_2").attr('orig_value', m_acct_phone_no_2);
			$("#txt_cellphone").attr('orig_value', m_acct_cellphone);
			$("#txt_acct_officer").attr('orig_value', m_acct_acct_officer);
			$("#txt_fax_no").attr('orig_value', m_acct_fax_no);
			$("#txt_acct_territory").attr('orig_value', m_acct_territory);
			$("#txt_email_add").attr('orig_value', m_acct_email);
			$("#txt_office_hours").attr('orig_value', m_acct_office_hours);
			$("#txt_area").attr('orig_value', m_acct_area);
			$("#txt_store_hours").attr('orig_value', m_acct_store_hours);
			$("#txt_region").attr('orig_value', m_acct_region);
			$("#txt_yrs_business").attr('orig_value', m_acct_years_in_business);
			$("#txt_yrs_matimco").attr('orig_value', m_acct_years_with_matimco);
			$("#txt_tax_id").attr('orig_value', m_acct_tax_id);
			$("#txt_vat_no").attr('orig_value', m_acct_vat_no);
			$("#txt_reg_name").attr('orig_value', m_acct_reg_name);
			$("#txt_business_add").attr('orig_value', m_acct_business_add);
			$("#txt_delivery_add").attr('orig_value', m_acct_delivery_add);
			$("#txt_no_employees").attr('orig_value', m_acct_num_employees);
			$("#txt_final_acct_code").attr('orig_value', m_final_acct_code);

			// BUSINESS TYPE
			var m_acct_business_type;
			m_acct_business_type = '<% Response.Write(acct_dtls.busType); %>';

			// HIDE NOTE
			$("#sub_tab").prev().hide();

			if( m_acct_business_type == "0" ){
				// select default tab
				$('#sub_tab').tabs("select", 0);
				
				/* SOLE PROPRIETORSHIP */
				var m_sole_owner_name = "";
				var m_sole_nationality = "";
				var m_sole_gen_manager = "";
				var m_sole_fin_manager = "";
				var m_sole_others = "";
				
				m_sole_owner_name = "<% Response.Write(acct_dtls.ownerCEO); %>";
				m_sole_nationality = "<% Response.Write(acct_dtls.nationality); %>";
				m_sole_gen_manager = "<% Response.Write(acct_dtls.genMgr); %>";
				m_sole_fin_manager = "<% Response.Write(acct_dtls.financeHead); %>";
				m_sole_others = "";
			
				$("#txt_sole_owner_name").attr('value', m_sole_owner_name);
				$("#txt_sole_nationality").attr('value', m_sole_nationality);
				$("#txt_sole_gen_manager").attr('value', m_sole_gen_manager);
				$("#txt_sole_fin_manager").attr('value', m_sole_fin_manager);
				$("#txt_sole_others").attr('value', m_sole_others);

				/* SAVE ORIGINAL VALUES */
				$("#txt_sole_owner_name").attr('orig_value', m_sole_owner_name);
				$("#txt_sole_nationality").attr('orig_value', m_sole_nationality);
				$("#txt_sole_gen_manager").attr('orig_value', m_sole_gen_manager);
				$("#txt_sole_fin_manager").attr('orig_value', m_sole_fin_manager);
				$("#txt_sole_others").attr('orig_value', m_sole_others);
			}

			if( m_acct_business_type == "1" ){
				// select default tab
				$('#sub_tab').tabs("select", 1);

				/* PARNERSHIP */
				var m_partner_gen_manager = "";
				var m_partner_fin_manager = "";

				m_partner_gen_manager = "<% Response.Write(acct_dtls.genMgr); %>";
				m_partner_fin_manager = "<% Response.Write(acct_dtls.financeHead); %>";

				$("#txt_partner_gen_manager").attr('value', m_partner_gen_manager);
				$("#txt_partner_fin_manager").attr('value', m_partner_fin_manager);

				/* SAVE ORIGINAL VALUES */
				$("#txt_partner_gen_manager").attr('orig_value', m_partner_gen_manager);
				$("#txt_partner_fin_manager").attr('orig_value', m_partner_fin_manager);

				// list of people
				<%  foreach ( ARMS_W.SkelClass.Document.AccountDetails.busTypeDtl row in acct_dtls.busTypeDtls) { %>
					<% 
						Response.Write("AddDataTBL('tbl_partner_list',");
						Response.Write("\"" + row.partnerStockHolder + "\""); Response.Write(",");
						Response.Write("\"" + row.nationality + "\""); Response.Write(",");
						Response.Write("\"" + row.capitalPerOwned + "\""); 
						Response.Write(");\n");
					%>
					if (g_list_of_partners.length > 0){ g_list_of_partners = g_list_of_partners + "$"; }
					g_list_of_partners = g_list_of_partners + "<% Response.Write(row.partnerStockHolder + "|" + row.nationality + "|" + row.capitalPerOwned + "|");  %>";
				<%  } %>
			}

			if( m_acct_business_type == "2" ){
				// select default tab
				$('#sub_tab').tabs("select", 2);

				/* CORPORATION */
				var m_corp_date_inc = "";
				var m_corp_gen_man = "";
				var m_corp_vp_fin = "";
				var m_corp_ceo = "";
				var m_corp_paidin_cap_stock = "";
				var m_corp_subc_cap_stock = "";
				var m_corp_auth_cap_stock = "";

				m_corp_date_inc = "<% Response.Write(acct_dtls.dateOfIncorporationFormatted); %>";
				m_corp_gen_man = "<% Response.Write(acct_dtls.genMgr); %>";
				m_corp_vp_fin = "<% Response.Write(acct_dtls.financeHead); %>";
				m_corp_ceo = "<% Response.Write(acct_dtls.ownerCEO); %>";
				m_corp_paidin_cap_stock = "<% Response.Write(acct_dtls.paidInCapStocks); %>";
				m_corp_subc_cap_stock = "<% Response.Write(acct_dtls.subscribedCapStocks); %>";
				m_corp_auth_cap_stock = "<% Response.Write(acct_dtls.authorizedCapStocks); %>";

				$("#txt_corpo_inc_date").attr('value', m_corp_date_inc);
				$("#txt_corpo_auth_cap_stock").attr('value', m_corp_auth_cap_stock);
				$("#txt_corpo_subscb_cap_stock").attr('value', m_corp_subc_cap_stock);
				$("#txt_corpo_paidin_cap_stock").attr('value', m_corp_paidin_cap_stock);
				$("#txt_corpo_ceo").attr('value', m_corp_ceo);
				$("#txt_corpo_vp_fin").attr('value', m_corp_vp_fin);
				$("#txt_corpo_gen_man").attr('value', m_corp_gen_man);

				// SAVE ORIGINAL VALUES
				$("#txt_corpo_inc_date").attr('orig_value', m_corp_date_inc);
				$("#txt_corpo_auth_cap_stock").attr('orig_value', m_corp_auth_cap_stock);
				$("#txt_corpo_subscb_cap_stock").attr('orig_value', m_corp_subc_cap_stock);
				$("#txt_corpo_paidin_cap_stock").attr('orig_value', m_corp_paidin_cap_stock);
				$("#txt_corpo_ceo").attr('orig_value', m_corp_ceo);
				$("#txt_corpo_vp_fin").attr('orig_value', m_corp_vp_fin);
				$("#txt_corpo_gen_man").attr('orig_value', m_corp_gen_man);

				// list of people
				<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.busTypeDtl row in acct_dtls.busTypeDtls) { %>
					<% 
						Response.Write("AddDataTBL('tbl_corpo_list',");
						Response.Write("\"" + row.partnerStockHolder + "\""); Response.Write(",");
						Response.Write("\"" + row.nationality + "\""); Response.Write(",");
						Response.Write("\"" + row.capitalPerOwned + "\""); 
						Response.Write(");\n");
					%>
					if (g_list_of_corpos.length > 0){ g_list_of_corpos = g_list_of_corpos + "$"; }
					g_list_of_corpos = g_list_of_corpos + "<% Response.Write(row.partnerStockHolder + "|" + row.nationality + "|" + row.capitalPerOwned + "|");  %>";
				<%  } %>

			}

			// list of employee no.
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.empInventory row in acct_dtls.empInventorys) { %>
				<% 
					Response.Write("AddDataTBL('tbl_emp_pos_list', ");
					Response.Write("\"" + row.position + "\""); Response.Write(",");
					Response.Write("\"" + row.numOfEmp + "\""); 
					Response.Write(");\n");
				%>
				if (g_list_of_employees.length > 0){ g_list_of_employees = g_list_of_employees + "$"; }
				g_list_of_employees = g_list_of_employees + '<% Response.Write(row.position + "|" + row.numOfEmp + "|");  %>';
			<%  } %>

			// attachments
			var m_attch_AOI = "";
			var m_attch_FS = "";
			var m_attch_ITR = "";
			var m_attch_BIR = "";
			var m_attch_LBP = "";
			var m_attch_OTHER = "";

			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.custAttachment row in acct_dtls.custAttachments) { %>
				<% if ( row.attachType == "AOI" ){ %>
					m_attch_AOI = "<% Response.Write(row.AttachPath); %>";
					// for viewing the file
					$("#txt_articles_of_inc").parent().parent().find("td:last").append(GenerateFileDlLink(m_acct_ccanum, m_attch_AOI));
				<% } %>

				<% if ( row.attachType == "FS" ){ %>
					m_attch_FS = "<% Response.Write(row.AttachPath); %>";
					// for viewing the file
					$("#txt_financial_statement").parent().parent().find("td:last").append(GenerateFileDlLink(m_acct_ccanum, m_attch_FS));
				<% } %>

				<% if ( row.attachType == "ITR" ){ %>
					m_attch_ITR = "<% Response.Write(row.AttachPath); %>";
					// for viewing the file
					$("#txt_ITR").parent().parent().find("td:last").append(GenerateFileDlLink(m_acct_ccanum, m_attch_ITR));
				<% } %>

				<% if ( row.attachType == "BIR" ){ %>
					m_attch_BIR = "<% Response.Write(row.AttachPath); %>";
					// for viewing the file
					$("#txt_bir_reg").parent().parent().find("td:last").append(GenerateFileDlLink(m_acct_ccanum, m_attch_BIR));
				<% } %>

				<% if ( row.attachType == "BP" ){ %>
					m_attch_LBP = "<% Response.Write(row.AttachPath); %>";
					// for viewing the file
					$("#txt_business_permit").parent().parent().find("td:last").append(GenerateFileDlLink(m_acct_ccanum, m_attch_LBP));
				<% } %>

				<% if ( row.attachType == "OTHER" ){ %>
					m_attch_OTHER = "<% Response.Write(row.AttachPath); %>";
					// for viewing the file
					$("#txt_attch_other").parent().parent().find("td:last").append(GenerateFileDlLink(m_acct_ccanum, m_attch_OTHER));
				<% } %>

			<%  } %>
			

			$("#txt_articles_of_inc").attr('value', m_attch_AOI);
			$("#txt_financial_statement").attr('value', m_attch_FS);
			$("#txt_ITR").attr('value', m_attch_ITR);
			$("#txt_bir_reg").attr('value', m_attch_BIR);
			$("#txt_business_permit").attr('value', m_attch_LBP);
			$("#txt_attch_other").attr('value', m_attch_OTHER);
			
			// SAVE ORIGINAL VALUES
			$("#txt_articles_of_inc").attr('orig_value', m_attch_AOI);
			$("#txt_financial_statement").attr('orig_value', m_attch_FS);
			$("#txt_ITR").attr('orig_value', m_attch_ITR);
			$("#txt_bir_reg").attr('orig_value', m_attch_BIR);
			$("#txt_business_permit").attr('orig_value', m_attch_LBP);
			$("#txt_attch_other").attr('value', m_attch_OTHER);

			// custBusHdr
			var m_acct_prop_credit_term = "";
			var m_acct_prop_credit_limit = "";
			var m_acct_socio_eco_class = "";
			var m_acct_num_outlets = "";
			var m_acct_credt_term_remarks = "";
			var m_acct_credt_limit_remarks = "";
			
			/* Code added by Billy Jay (04/23/2015) */
			var m_acct_iniPODetails = "";
			var m_acct_prob_order_limit_ab = "";
			var m_acct_prob_order_limit_tr = "";
			
			var m_acct_order_limit_remarks_ab = "";
			var m_acct_order_limit_remarks_tr = "";

			var m_acct_prop_credit_term_architectural_brand = "";
			var m_acct_prop_credit_term_ecofor_lumber = "";
			var m_acct_prop_credit_term_ecofor_plywood ="";

			var m_acct_credt_term_remarks_architectural_brand = "";
			var m_acct_credt_term_remarks_ecofor_lumber = "";
			var m_acct_credt_term_remarks_ecofor_plywood = "";

			m_acct_iniPODetails = "<% Response.Write(acct_dtls.initialPODetails); %>";

			m_acct_prob_order_limit_ab = "<% Response.Write(acct_dtls.probOrderLimit_AB); %>";
			m_acct_prob_order_limit_tr = "<% Response.Write(acct_dtls.probOrderLimit_TR); %>";
			
			m_acct_order_limit_remarks_ab = "<% Response.Write(acct_dtls.OrderLimitRemarks_AB); %>";
			m_acct_order_limit_remarks_tr = "<% Response.Write(acct_dtls.OrderLimitRemarks_TR); %>";

			m_acct_prop_credit_term_architectural_brand = "<% Response.Write(acct_dtls.propCredTermsArchitecturalBrand); %>";
			m_acct_prop_credit_term_ecofor_lumber = "<% Response.Write(acct_dtls.propCredTermsEcoforLumber); %>";
			m_acct_prop_credit_term_ecofor_plywood = "<% Response.Write(acct_dtls.propCredTermsEcoforPlywood); %>";
			
			m_acct_credt_term_remarks_architectural_brand = "<% Response.Write(acct_dtls.CredTermRemarksArchitecturalBrand); %>";
			m_acct_credt_term_remarks_ecofor_lumber = "<% Response.Write(acct_dtls.CredTermRemarksEcoforLumber); %>";
			m_acct_credt_term_remarks_ecofor_plywood = "<% Response.Write(acct_dtls.CredTermRemarksEcoforPlywood); %>";

			$("#txt_ini_po_details").attr('value',m_acct_iniPODetails);

			$("#txt_order_limit_ab").attr('value',  m_acct_prob_order_limit_ab);
			$("#txt_order_limit_tr").attr('value', m_acct_prob_order_limit_tr);
			
			$("#txt_order_limit_remarks_ab").attr('value', m_acct_order_limit_remarks_ab);
			$("#txt_order_limit_remarks_tr").attr('value', m_acct_order_limit_remarks_tr);

			$("#txt_credit_terms_architectural_brand").attr('value', m_acct_prop_credit_term_architectural_brand);
			$("#txt_credit_terms_eco_lumber").attr('value', m_acct_prop_credit_term_ecofor_lumber);
			$("#txt_credit_terms_eco_plywood").attr('value', m_acct_prop_credit_term_ecofor_plywood);

			$("#txt_credit_terms_architectural_brand_remarks").attr('value', m_acct_credt_term_remarks_architectural_brand);
			$("#txt_credit_terms_eco_lumber_remarks").attr('value', m_acct_credt_term_remarks_ecofor_lumber);
			$("#txt_credit_terms_eco_plywood_remarks").attr('value', m_acct_credt_term_remarks_ecofor_plywood);

			
			/* End Code added by Billy Jay (04/23/2015) */

			m_acct_prop_credit_term = "<% Response.Write(acct_dtls.propCredTerms); %>";
			m_acct_prop_credit_limit = "<% Response.Write(acct_dtls.probCredLimit); %>";
			m_acct_socio_eco_class = "<% Response.Write(acct_dtls.sociaEcoClass); %>";
			m_acct_num_outlets = "<% Response.Write(acct_dtls.numOfOutlet); %>";
			m_acct_credt_term_remarks = "<% Response.Write(acct_dtls.CredTermRemarks); %>";
			m_acct_credt_limit_remarks = "<% Response.Write(acct_dtls.CredLimitRemarks); %>";

			$("#txt_credit_terms").attr('value', m_acct_prop_credit_term);
			$("#txt_credit_limit").attr('value', m_acct_prop_credit_limit);
			$("#txt_eco_class_of_customer").attr('value', m_acct_socio_eco_class);
			$("#txt_no_of_outlets").attr('value', m_acct_num_outlets);
			$("#txt_credit_terms_remarks").attr('value', m_acct_credt_term_remarks);
			$("#txt_credit_limit_remarks").attr('value', m_acct_credt_limit_remarks);

			// SAVE ORIGINAL VALUES

			/* Code added by Billy Jay (04/23/2015) */

			$("#txt_credit_terms_architectural_brand").attr('orig_value', m_acct_prop_credit_term_architectural_brand);
			$("#txt_credit_terms_eco_lumber").attr('orig_value', m_acct_prop_credit_term_ecofor_lumber);
			$("#txt_credit_terms_eco_plywood").attr('orig_value', m_acct_prop_credit_term_ecofor_plywood);

			$("#txt_credit_terms_architectural_brand_remarks").attr('orig_value', m_acct_credt_term_remarks_architectural_brand);
			$("#txt_credit_terms_eco_lumber_remarks").attr('orig_value', m_acct_credt_term_remarks_ecofor_lumber);
			$("#txt_credit_terms_eco_plywood_remarks").attr('orig_value', m_acct_credt_term_remarks_ecofor_plywood);

			
			$("#txt_order_limit_ab").attr('orig_value', m_acct_prob_order_limit_ab);
			$("#txt_order_limit_tr").attr('orig_value', m_acct_prob_order_limit_tr);
			
			$("#txt_order_limit_remarks_ab").attr('orig_value', m_acct_order_limit_remarks_ab);
			$("#txt_order_limit_remarks_tr").attr('orig_value', m_acct_order_limit_remarks_tr);
			/* End Code added by Billy Jay (04/23/2015) */

			$("#txt_credit_terms").attr('orig_value', m_acct_prop_credit_term);
			$("#txt_credit_limit").attr('orig_value', m_acct_prop_credit_limit);
			$("#txt_eco_class_of_customer").attr('orig_value', m_acct_socio_eco_class);
			$("#txt_no_of_outlets").attr('orig_value', m_acct_num_outlets);
			$("#txt_credit_terms_remarks").attr('orig_value', m_acct_credt_term_remarks);
			$("#txt_credit_limit_remarks").attr('orig_value', m_acct_credt_limit_remarks);

			var m_acct_mw_price_code = "";
			var m_acct_mw_price_desc = "";
			var m_acct_mw_price_commdisc = "";
			var m_acct_mw_price_remarks = "";
			var m_acct_ww_price_code = "";
			var m_acct_ww_price_desc = "";
			var m_acct_ww_price_commdisc = "";
			var m_acct_ww_price_remarks = "";
			var m_acct_pwf_price_code = "";
			var m_acct_pwf_price_desc = "";
			var m_acct_pwf_price_commdisc = "";
			var m_acct_pwf_price_remarks = "";
			var m_acct_pwr_price_code = "";
			var m_acct_pwr_price_desc = "";
			var m_acct_pwr_price_commdisc = "";
			var m_acct_pwr_price_remarks = "";
			var m_acct_gw_price_code = "";
			var m_acct_gw_price_desc = "";
			var m_acct_gw_price_commdisc = "";
			var m_acct_gw_price_remarks = "";
			var m_acct_tw_price_code = "";
			var m_acct_tw_price_desc = "";
			var m_acct_tw_price_commdisc = "";
			var m_acct_tw_price_remarks = "";

			var m_acct_mz_price_code = "";
			var m_acct_mz_price_desc = "";
			var m_acct_mz_price_commdisc = "";
			var m_acct_mz_price_remarks = "";

			var m_acct_nw_price_code = "";
			var m_acct_nw_price_desc = "";
			var m_acct_nw_price_commdisc = "";
			var m_acct_nw_price_remarks = "";

			var m_acct_ec_price_code = "";
			var m_acct_ec_price_desc = "";
			var m_acct_ec_price_commdisc = "";
			var m_acct_ec_price_remarks = "";

			var m_acct_ecu_price_code = "";
			var m_acct_ecu_price_desc = "";
			var m_acct_ecu_price_commdisc = "";
			var m_acct_ecu_price_remarks = "";

			// propsedPrice
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.propsedPrice row in acct_dtls.propsedPrices) { %>
				
				<% if(row.brandType == "MW"){ %>
					m_acct_mw_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_mw_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_mw_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_mw_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "WW"){ %>
					m_acct_ww_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_ww_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_ww_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_ww_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "PWF"){ %>
					m_acct_pwf_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_pwf_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_pwf_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_pwf_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "PWR"){ %>
					m_acct_pwr_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_pwr_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_pwr_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_pwr_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "GW"){ %>
					m_acct_gw_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_gw_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_gw_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_gw_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "TW"){ %>
					m_acct_tw_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_tw_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_tw_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_tw_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "MZ"){ %>
					m_acct_mz_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_mz_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_mz_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_mz_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "NW"){ %>
					m_acct_nw_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_nw_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_nw_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_nw_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>

				<% if(row.brandType == "EC"){ %>
					m_acct_ec_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_ec_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_ec_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_ec_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>


				<% if(row.brandType == "ECU"){ %>
					m_acct_ecu_price_code = "<% Response.Write(row.priceListCode); %>";
					m_acct_ecu_price_desc = "<% Response.Write(row.codeDesc); %>";
					m_acct_ecu_price_commdisc = "<% Response.Write(row.CommisionDiscounts); %>";
					m_acct_ecu_price_remarks = "<% Response.Write(row.remarks); %>";
				<% } %>
				
			<%  } %>

			$("#txt_mw_price_code").attr('value', m_acct_mw_price_code);
			$("#txt_mw_price_desc").attr('value', m_acct_mw_price_desc);
			$("#txt_mw_price_commision_disc").attr('value', m_acct_mw_price_commdisc);
			$("#txt_mw_price_remarks").attr('value', m_acct_mw_price_remarks);

			$("#txt_ww_price_code").attr('value', m_acct_ww_price_code);
			$("#txt_ww_price_desc").attr('value', m_acct_ww_price_desc);
			$("#txt_ww_price_commision_disc").attr('value', m_acct_ww_price_commdisc);
			$("#txt_ww_price_remarks").attr('value', m_acct_ww_price_remarks);

			$("#txt_pwf_price_code").attr('value', m_acct_pwf_price_code);
			$("#txt_pwf_price_desc").attr('value', m_acct_pwf_price_desc);
			$("#txt_pwf_price_commision_disc").attr('value', m_acct_pwf_price_commdisc);
			$("#txt_pwf_price_remarks").attr('value', m_acct_pwf_price_remarks);

			$("#txt_pwr_price_code").attr('value', m_acct_pwr_price_code);
			$("#txt_pwr_price_desc").attr('value', m_acct_pwr_price_desc);
			$("#txt_pwr_price_commision_disc").attr('value', m_acct_pwr_price_commdisc);
			$("#txt_pwr_price_remarks").attr('value', m_acct_pwr_price_remarks);

			$("#txt_gw_price_code").attr('value', m_acct_gw_price_code);
			$("#txt_gw_price_desc").attr('value', m_acct_gw_price_desc);
			$("#txt_gw_price_commision_disc").attr('value', m_acct_gw_price_commdisc);
			$("#txt_gw_price_remarks").attr('value', m_acct_gw_price_remarks);

			$("#txt_tw_price_code").attr('value', m_acct_tw_price_code);
			$("#txt_tw_price_desc").attr('value', m_acct_tw_price_desc);
			$("#txt_tw_price_commision_disc").attr('value', m_acct_tw_price_commdisc);
			$("#txt_tw_price_remarks").attr('value', m_acct_tw_price_remarks);

			$("#txt_mz_price_code").attr('value', m_acct_mz_price_code);
			$("#txt_mz_price_desc").attr('value', m_acct_mz_price_desc);
			$("#txt_mz_price_commision_disc").attr('value', m_acct_mz_price_commdisc);
			$("#txt_mz_price_remarks").attr('value', m_acct_mz_price_remarks);

			$("#txt_nw_price_code").attr('value', m_acct_nw_price_code);
			$("#txt_nw_price_desc").attr('value', m_acct_nw_price_desc);
			$("#txt_nw_price_commision_disc").attr('value', m_acct_nw_price_commdisc);
			$("#txt_nw_price_remarks").attr('value', m_acct_nw_price_remarks);

			$("#txt_ec_price_code").attr('value', m_acct_ec_price_code);
			$("#txt_ec_price_desc").attr('value', m_acct_ec_price_desc);
			$("#txt_ec_price_commision_disc").attr('value', m_acct_ec_price_commdisc);
			$("#txt_ec_price_remarks").attr('value', m_acct_ec_price_remarks);

			$("#txt_ecu_price_code").attr('value', m_acct_ecu_price_code);
			$("#txt_ecu_price_desc").attr('value', m_acct_ecu_price_desc);
			$("#txt_ecu_price_commision_disc").attr('value', m_acct_ecu_price_commdisc);
			$("#txt_ecu_price_remarks").attr('value', m_acct_ecu_price_remarks);

			// SAVE ORIGINAL VALUES
			$("#txt_mw_price_code").attr('orig_value', m_acct_mw_price_code);
			$("#txt_mw_price_desc").attr('orig_value', m_acct_mw_price_desc);
			$("#txt_mw_price_commision_disc").attr('orig_value', m_acct_mw_price_commdisc);
			$("#txt_mw_price_remarks").attr('orig_value', m_acct_mw_price_remarks);

			$("#txt_ww_price_code").attr('orig_value', m_acct_ww_price_code);
			$("#txt_ww_price_desc").attr('orig_value', m_acct_ww_price_desc);
			$("#txt_ww_price_commision_disc").attr('orig_value', m_acct_ww_price_commdisc);
			$("#txt_ww_price_remarks").attr('orig_value', m_acct_ww_price_remarks);

			$("#txt_pwf_price_code").attr('orig_value', m_acct_pwf_price_code);
			$("#txt_pwf_price_desc").attr('orig_value', m_acct_pwf_price_desc);
			$("#txt_pwf_price_commision_disc").attr('orig_value', m_acct_pwf_price_commdisc);
			$("#txt_pwf_price_remarks").attr('orig_value', m_acct_pwf_price_remarks);

			$("#txt_pwr_price_code").attr('orig_value', m_acct_pwr_price_code);
			$("#txt_pwr_price_desc").attr('orig_value', m_acct_pwr_price_desc);
			$("#txt_pwr_price_commision_disc").attr('orig_value', m_acct_pwr_price_commdisc);
			$("#txt_pwr_price_remarks").attr('orig_value', m_acct_pwr_price_remarks);

			$("#txt_gw_price_code").attr('orig_value', m_acct_gw_price_code);
			$("#txt_gw_price_desc").attr('orig_value', m_acct_gw_price_desc);
			$("#txt_gw_price_commision_disc").attr('orig_value', m_acct_gw_price_commdisc);
			$("#txt_gw_price_remarks").attr('orig_value', m_acct_gw_price_remarks);

			$("#txt_tw_price_code").attr('orig_value', m_acct_tw_price_code);
			$("#txt_tw_price_desc").attr('orig_value', m_acct_tw_price_desc);
			$("#txt_tw_price_commision_disc").attr('orig_value', m_acct_tw_price_commdisc);
			$("#txt_tw_price_remarks").attr('orig_value', m_acct_tw_price_remarks);

			$("#txt_mz_price_code").attr('orig_value', m_acct_mz_price_code);
			$("#txt_mz_price_desc").attr('orig_value', m_acct_mz_price_desc);
			$("#txt_mz_price_commision_disc").attr('orig_value', m_acct_mz_price_commdisc);
			$("#txt_mz_price_remarks").attr('orig_value', m_acct_mz_price_remarks);

			$("#txt_nw_price_code").attr('orig_value', m_acct_nw_price_code);
			$("#txt_nw_price_desc").attr('orig_value', m_acct_nw_price_desc);
			$("#txt_nw_price_commision_disc").attr('orig_value', m_acct_nw_price_commdisc);
			$("#txt_nw_price_remarks").attr('orig_value', m_acct_nw_price_remarks);

			$("#txt_ec_price_code").attr('orig_value', m_acct_ec_price_code);
			$("#txt_ec_price_desc").attr('orig_value', m_acct_ec_price_desc);
			$("#txt_ec_price_commision_disc").attr('orig_value', m_acct_ec_price_commdisc);
			$("#txt_ec_price_remarks").attr('orig_value', m_acct_ec_price_remarks);

			$("#txt_ecu_price_code").attr('orig_value', m_acct_ecu_price_code);
			$("#txt_ecu_price_desc").attr('orig_value', m_acct_ecu_price_desc);
			$("#txt_ecu_price_commision_disc").attr('orig_value', m_acct_ecu_price_commdisc);
			$("#txt_ecu_price_remarks").attr('orig_value', m_acct_ecu_price_remarks);

			 // customerEvents
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.customerEvents row in acct_dtls.customerEventss.Where(o=>o.bool_specifiedevent==false)) { %>
				<% 
					Response.Write("AddDataTBL('tbl_birthday_events', ");
					Response.Write("\"" + row.ch_name + "\""); Response.Write(",");
					Response.Write("\"" + row.ch_event + "\""); Response.Write(",");
					Response.Write("\"" + (row.dt_date.HasValue?row.dt_date.Value.ToShortDateString():"") + "\""); Response.Write(",");
					Response.Write("\"" + row.ch_contactnumber + "\"");
					Response.Write(");\n");  
				%> 
			   
				if (g_list_of_events.length > 0){ g_list_of_events = g_list_of_events + "$"; }
				g_list_of_events = g_list_of_events + "<% Response.Write(row.ch_name + "|" + row.ch_event + "|" + row.dt_date + "|" + row.ch_contactnumber + "|");  %>";
			   
				<%  } %>

			// customerEvents
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.customerEvents row in acct_dtls.customerEventss.Where(o=>o.bool_specifiedevent==true)) { %>
				<% 
					Response.Write("AddDataTBL('tbl_specified_events', ");
					Response.Write("\"" + row.ch_name + "\""); Response.Write(",");
					Response.Write("\"" + row.ch_event + "\""); Response.Write(",");
					Response.Write("\"" +  (row.dt_date.HasValue?row.dt_date.Value.ToShortDateString():"") + "\""); Response.Write(",");
					Response.Write("\"" + row.ch_contactnumber + "\"");
					Response.Write(");\n");   
				   
				%>
				<%  } %>

			// custOutlets
			// list of outlets
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.custOutlets row in acct_dtls.custOutletss) { %>
				<% 
					Response.Write("AddDataTBL('tbl_outlet_list', ");
					Response.Write("\"" + row.name + "\""); Response.Write(",");
					Response.Write("\"" + row.location + "\""); Response.Write(",");
					Response.Write("\"" + row.storeSize + "\""); Response.Write(",");
					Response.Write("\"" + row.wreHouseSize + "\""); 
					Response.Write(");\n");
				%>  
				if (g_list_of_outlets.length > 0){ g_list_of_outlets = g_list_of_outlets + "$"; }
				g_list_of_outlets = g_list_of_outlets + "<% Response.Write(row.name + "|" + row.location + "|" + row.storeSize + "|" + row.wreHouseSize + "|");  %>";
			<%  } %>

			// majorCustomer
			// list of customer
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.majorCustomer row in acct_dtls.majorCustomers) { %>
				<% 
					Response.Write("AddDataTBL('tbl_mjcust_list', ");
					Response.Write("\"" + row.name + "\""); Response.Write(",");
					Response.Write("\"" + row.address + "\""); Response.Write(",");
					Response.Write("\"" + row.sellingTerms + "\""); Response.Write(",");
					Response.Write("\"" + row.estMonthPur + "\""); 
					Response.Write(");\n");
				%>
				if (g_list_of_majorcustomers.length > 0){ g_list_of_majorcustomers = g_list_of_majorcustomers + "$"; }
				g_list_of_majorcustomers = g_list_of_majorcustomers + "<% Response.Write(row.name + "|" + row.address + "|" + row.sellingTerms + "|" + row.estMonthPur + "|");  %>";
			<%  } %>

			// customer Activities
			// activities
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.Activities row in acct_dtls.activities) { %>
				<% 
					Response.Write("AddDataTBL('tblActivities', ");
					Response.Write("\"" + row.action + "\""); Response.Write(",");
					Response.Write("\"" + row.type + "\""); Response.Write(",");
					Response.Write("\"" + row.contactperson + "\""); Response.Write(",");
					Response.Write("\"" + row.subject + "\""); Response.Write(",");
					Response.Write("\"" + row.startdatetme + "\""); Response.Write(",");
					Response.Write("\"" + row.enddatetime + "\""); Response.Write(",");
					Response.Write("\"" + row.content + "\""); Response.Write(",");
					Response.Write("\"" + row.remarks + "\""); 
					Response.Write(");\n");
				%>
				
				if (g_list_of_activities.length > 0){ g_list_of_activities = g_list_of_activities + "$"; }
				g_list_of_activities = g_list_of_activities + "<% Response.Write(row.action + "|" + row.type + "|" + row.contactperson + "|" + row.subject + "|" + row.startdatetme + "|" + row.enddatetime + "|" + row.content + "|" + row.remarks + "|");  %>";
		   
			   <%  } %>
			$("#tblActivities tr").find("td:eq(2) input,td:eq(3) input, td:eq(6) input,td:eq(7) input").attr("tobubble","tobubble");
			$("#tblActivities tr").find("td:eq(0), td:eq(0) input").css("width","60px"); 
			$("#tblActivities tr").find("td:eq(1), td:eq(1) input").css("width","130px");
			$("#tblActivities tr").find("td:eq(2), td:eq(2) input").css("min-width","100px");
			$("#tblActivities tr").find("td:eq(3), td:eq(3) input").css("min-width","100px");
			$("#tblActivities tr").find("td:eq(4), td:eq(4) input").css("min-width","120px");
			$("#tblActivities tr").find("td:eq(5), td:eq(5) input").css("min-width","120px");
			$("#tblActivities tr").find("td:eq(6), td:eq(6) input").css("min-width","150px");
		   // $("#tblActivities tr").find("td:eq(1),td:eq(4),td:eq(5)").css("width","140px");
		  //  $("#tblActivities tr").find("td:eq(0) input,td:eq(1) input,td:eq(4) input,td:eq(5) input").css("width","90%");
		  //  $("#tblActivities tr").find("td:eq(2) input,td:eq(3) input").css("width","96%");
			$("#tblActivities").find("img").css('display','none');
			// CIBI remarks and Supplier Info Remarks 
			var m_acct_cibi_remarks = "";
			var m_acct_supplyinfo_remarks = "";

			m_acct_cibi_remarks = "<% Response.Write(acct_dtls.CIBI_remarks); %>";
			m_acct_supplyinfo_remarks = "<% Response.Write(acct_dtls.SupplyInfo_remarks); %>";

			$("#txt_cibi_remakrs").val(m_acct_cibi_remarks.replace(/&#10;/g, "\n"));
			$("#txt_supplyinfo_remakrs").val(m_acct_supplyinfo_remarks.replace(/&#10;/g, "\n"));
			
			// SAVE ORIGINAL VALUES
			$("#txt_cibi_remakrs").attr('orig_value', m_acct_cibi_remarks);
			$("#txt_supplyinfo_remakrs").attr('orig_value', m_acct_supplyinfo_remarks);

			// products
			var m_acct_major_prod_line = "";
			var m_acct_other_prod_line = "";
			var m_acct_supplier_on_plywood = "";
			var m_acct_supplier_on_steel = "";
			var m_acct_supplier_on_cement = "";
			var m_acct_supplier_on_con_hollowblock = "";
			var m_acct_supplier_on_others = "";
			var m_acct_major_vol_business = "";
			var m_acct_monthly_wood_vol = "";
			var m_acct_discount_enjoyed = "";

			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.products row in acct_dtls.productss) { %>
				m_acct_major_prod_line = "<% Response.Write(row.majorLine); %>";
				m_acct_other_prod_line = "<% Response.Write(row.otherProductLine); %>";
				m_acct_supplier_on_plywood = "<% Response.Write(row.suppPlywood); %>";
				m_acct_supplier_on_steel = "<% Response.Write(row.suppSteel); %>";
				m_acct_supplier_on_cement = "<% Response.Write(row.suppCement); %>";
				m_acct_supplier_on_con_hollowblock = "<% Response.Write(row.suppCHB); %>";
				m_acct_supplier_on_others = "<% Response.Write(row.suppOthers); %>";
				m_acct_major_vol_business = "<% Response.Write(row.volValueDriver); %>";
				m_acct_monthly_wood_vol = "<% Response.Write(row.woodVolValue); %>";
				m_acct_discount_enjoyed = "<% Response.Write(row.discounts); %>";
			<%  } %>

			$("#txt_prod_major_line").attr('value', m_acct_major_prod_line);
			$("#txt_prod_other_line").attr('value', m_acct_other_prod_line);
			$("#txt_const_mat_plywood").attr('value', m_acct_supplier_on_plywood);
			$("#txt_const_mat_steel").attr('value', m_acct_supplier_on_steel);
			$("#txt_const_mat_cement").attr('value', m_acct_supplier_on_cement);
			$("#txt_const_mat_hb").attr('value', m_acct_supplier_on_con_hollowblock);
			$("#txt_const_mat_others").attr('value', m_acct_supplier_on_others);
			$("#txt_major_vol_business").attr('value', m_acct_major_vol_business);
			$("#txt_wood_vol").attr('value', m_acct_monthly_wood_vol);
			$("#txt_discount_enjoyed").attr('value', m_acct_discount_enjoyed);

			// SAVE ORIGINAL VALUES
			$("#txt_prod_major_line").attr('orig_value', m_acct_major_prod_line);
			$("#txt_prod_other_line").attr('orig_value', m_acct_other_prod_line);
			$("#txt_const_mat_plywood").attr('orig_value', m_acct_supplier_on_plywood);
			$("#txt_const_mat_steel").attr('orig_value', m_acct_supplier_on_steel);
			$("#txt_const_mat_cement").attr('orig_value', m_acct_supplier_on_cement);
			$("#txt_const_mat_hb").attr('orig_value', m_acct_supplier_on_con_hollowblock);
			$("#txt_const_mat_others").attr('orig_value', m_acct_supplier_on_others);
			$("#txt_major_vol_business").attr('orig_value', m_acct_major_vol_business);
			$("#txt_wood_vol").attr('orig_value', m_acct_monthly_wood_vol);
			$("#txt_discount_enjoyed").attr('orig_value', m_acct_discount_enjoyed);

			// otherWoodSupp
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.otherWoodSupp row in acct_dtls.otherWoodSupps) { %>
				<% 
					Response.Write("AddDataTBL('tbl_wood_supplier', ");
					Response.Write("\"" + row.supplier + "\""); Response.Write(",");
					Response.Write("\"" + row.monthVolVal + "\""); Response.Write(",");
					Response.Write("\"" + row.contactPerson + "\""); Response.Write(",");
					Response.Write("\"" + row.contactNum + "\""); Response.Write(",");
					Response.Write("\"" + row.prodPurchase + "\""); Response.Write(",");
					Response.Write("\"" + row.creditTerms + "\""); Response.Write(",");
					Response.Write("\"\"");
					Response.Write(");\n");
				%>
				if (g_list_of_wood_suppliers.length > 0){ g_list_of_wood_suppliers = g_list_of_wood_suppliers + "$"; }
				g_list_of_wood_suppliers = g_list_of_wood_suppliers + '<% Response.Write(row.supplier + "|" + row.monthVolVal + "|" + row.contactPerson + "|" + row.contactNum + "|" + row.prodPurchase + "|" + row.creditTerms + "|");  %>';
			<%  } %>

			// depositoryBank
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.depositoryBank row in acct_dtls.depositoryBanks) { %>
				<% 
					Response.Write("AddDataTBL('tbl_bank_list', ");
					Response.Write("\"" + row.bankName + "\""); Response.Write(",");
					Response.Write("\"" + row.branch + "\""); Response.Write(",");
					Response.Write("\"" + row.address + "\""); Response.Write(",");
					Response.Write("\"" + row.account + "\""); Response.Write(",");
					Response.Write("\"" + row.contactPerson + "\""); Response.Write(",");
					Response.Write("\"" + row.contactNumber + "\""); Response.Write(",");
					Response.Write("\"" + row.aveDeposit + "\""); Response.Write(",");
					Response.Write("\"" + row.remarks + "\"");
					Response.Write(");\n");
				%>
				if (g_list_of_banks.length > 0){ g_list_of_banks = g_list_of_banks + "$"; }
				g_list_of_banks = g_list_of_banks + "<% Response.Write(row.bankName + "|" + row.branch + "|" + row.address + "|" + row.account + "|" + row.contactPerson + "|" + row.contactNumber + "|" + row.aveDeposit + "|" + row.remarks + "|");  %>";
			<%  } %>

			// asset_land
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.assets_land row in acct_dtls.assets_lands) { %>
				<% 
					Response.Write("AddDataTBL('tbl_land_list', ");
					Response.Write("\"" + row.type + "\""); Response.Write(",");
					Response.Write("\"" + row.area + "\""); Response.Write(",");
					Response.Write("\"" + row.location + "\""); Response.Write(",");
					Response.Write("\"" + row.owner + "\"");
					Response.Write(");\n");
				%>
				if (g_list_of_banks.length > 0){ g_list_of_banks = g_list_of_banks + "$"; }
				g_list_of_banks = g_list_of_banks + '<% Response.Write(row.type + "|" + row.area + "|" + row.location + "|" + row.owner + "|");  %>';
			<%  } %>

			// assets_building
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.assets_building row in acct_dtls.assets_buildings) { %>
				<% 
					Response.Write("AddDataTBL('tbl_building_list', ");
					Response.Write("\"" + row.type + "\""); Response.Write(",");
					Response.Write("\"" + row.area + "\""); Response.Write(",");
					Response.Write("\"" + row.location + "\""); Response.Write(",");
					Response.Write("\"" + row.owner + "\"");
					Response.Write(");\n");
				%>
				if (g_list_of_buildings.length > 0){ g_list_of_buildings = g_list_of_buildings + "$"; }
				g_list_of_buildings = g_list_of_buildings + '<% Response.Write(row.type + "|" + row.area + "|" + row.location + "|" + row.owner + "|");  %>';
			<%  } %>

			// assets_vehicle
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.assets_vehicle row in acct_dtls.assets_vehicles) { %>
				<% 
					Response.Write("AddDataTBL('tbl_vehicle_list', ");
					Response.Write("\"" + row.type + "\""); Response.Write(",");
					Response.Write("\"" + row.model + "\""); Response.Write(",");
					Response.Write("\"" + row.quantity + "\"");
					Response.Write(");\n");
				%>
				if (g_list_of_vehicles.length > 0){ g_list_of_vehicles = g_list_of_vehicles + "$"; }
				g_list_of_vehicles = g_list_of_vehicles + '<% Response.Write(row.type + "|" + row.model + "|" + row.quantity + "|");  %>';
			<%  } %>

			// assets_other
			 var m_acct_other_asset = "";
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.assets_other row in acct_dtls.assets_others) { %>
				m_acct_other_asset = "<% Response.Write(row.otherAssets); %>";
			<%  } %>

			$("#txt_other_assets").attr('value', m_acct_other_asset);

			// SAVE ORIGINAL VALUES
			$("#txt_other_assets").attr('orig_value', m_acct_other_asset);

			// otherBusiness
			<%  foreach (ARMS_W.SkelClass.Document.AccountDetails.otherBusiness row in acct_dtls.otherBusinesss) { %>
				<% 
					Response.Write("AddDataTBL('tbl_asset_list', ");
					Response.Write("\"" + row.regName + "\""); Response.Write(",");
					Response.Write("\"" + row.nature + "\""); Response.Write(",");
					Response.Write("\"" + row.location + "\""); Response.Write(",");
					Response.Write("\"" + row.percentOwnership + "\"");
					Response.Write(");\n");
				%>
				if (g_list_of_other_business.length > 0){ g_list_of_other_business = g_list_of_other_business + "$"; }
				g_list_of_other_business = g_list_of_other_business + '<% Response.Write(row.regName + "|" + row.nature + "|" + row.location + "|" + row.percentOwnership + "|");  %>';
			<%  } %>
			

			// hide tabs if curr_doc_DocStatus != 1
			if('<%: acct_dtls.curr_doc_DocStatus %>' != "1000")
			{
				// if still on the process of creating the account, disable
				$("#tabs").tabs("option", "disabled", [5, 6, 7, 8, 9, 10]);
			}
			else
			{
				// disable editing of proposed_code
				$("#txt_acct_code").attr("readonly", "readonly");
			}

			<% 
				//if(userHeader["position"].ToString() != "csr")
				if (
					oUsr.HasPositionOf("csr") == IS_NOT_FOUND 
					// ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
					&& oUsr.HasPositionOf("csm") == IS_NOT_FOUND 
					)
				{
					%> DisableEditing(m_acct_business_type); <%
				}
			%>

			<% 
				//if ( userHeader["position"].ToString() == "csr" )
				if (
				oUsr.HasPositionOf("csr") != IS_NOT_FOUND 
				// ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
				|| oUsr.HasPositionOf("csm") != IS_NOT_FOUND 
				)
				{
					if( 
						(acct_dtls.curr_doc_DocStatus != "1000" && acct_dtls.curr_doc_DocStatus != AppHelper.GetUserPositionId("csr")) 
						|| 
						acct_dtls.curr_doc_DocChangesStatus != AppHelper.GetUserPositionId("csr") 
					){
						%> DisableEditing(m_acct_business_type); <%
					}

					// IS USER' REGION DOES NOT MATCH, DISABLE
					if (oUsr.HasRegionOf("csr", oDocumnt.Region) == false 
					// ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
					&& oUsr.HasRegionOf("csm", oDocumnt.Region) == false
					)
					{
						%> DisableEditing(m_acct_business_type); <%
					}
				}

				// FOR CNC

			%>

			<% 

			// CI INFO
			if ( 
			acct_dtls.curr_doc_DocStatus != AppHelper.GetUserPositionId("cnc") && 
			acct_dtls.curr_doc_DocStatus != AppHelper.GetUserPositionId("fnm") && 
			oUsr.HasPositionOf("cnc") == IS_NOT_FOUND && 
			oUsr.HasPositionOf("fnm") == IS_NOT_FOUND
			)
			{
				// DISABLE EDITING OF CI INFO
				%> DisableEditingCI(); <%
			}

			// CREDIT LIMIT
			if (
			( acct_dtls.curr_doc_DocStatus != "1" || acct_dtls.curr_doc_DocStatus != "1000" ) && 
			acct_dtls.curr_doc_DocStatus != "1008" && 
			(
				oUsr.HasPositionOf("csr") == IS_NOT_FOUND 
				// ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
				&& oUsr.HasPositionOf("csm") == IS_NOT_FOUND 
			)
			&& 
			oUsr.HasPositionOf("fnm") == IS_NOT_FOUND
			)
			{
				// DISABLE EDITING OF CREDIT LIMIT
				%> DisAbleCreditLT(); <%
			}
				

			%>

			// status message
			var doc_stat_msg = "<%: CurrDocMessage %>";
			$("#doc_stat_msg").html(doc_stat_msg);

			// save original values
			g_acct_name = m_acct_name;
			g_acct_acct_officer = m_acct_acct_officer;
			g_acct_territory = m_acct_territory;
			g_acct_area = m_acct_area;
			g_acct_region = m_acct_region;
			g_acct_phone_no = m_acct_phone_no;
			g_acct_phone_no_2 = m_acct_phone_no_2;
			g_acct_cellphone = m_acct_cellphone;
			g_acct_reg_name = m_acct_reg_name;
			g_acct_business_add = m_acct_business_add;
			g_acct_delivery_add = m_acct_delivery_add;

			/* Code added by Billy Jay (04/23/2015) */
			g_acct_prop_credit_term_architectural_brand = m_acct_prop_credit_term_architectural_brand;
			g_acct_prop_credit_term_ecofor_lumber = m_acct_prop_credit_term_ecofor_lumber;
			g_acct_prop_credit_term_ecofor_plywood = m_acct_prop_credit_term_ecofor_plywood;

			g_acct_prop_credit_term_remarks_architectural_brand = m_acct_credt_term_remarks_architectural_brand;
			g_acct_prop_credit_term_remarks_ecofor_lumber = m_acct_credt_term_remarks_ecofor_lumber;
			g_acct_prop_credit_term_remarks_ecofor_plywood = m_acct_credt_term_remarks_ecofor_plywood;

			
			g_acct_prop_credit_term_remarks = m_acct_credt_term_remarks;
			g_acct_prop_credit_limit_remarks = m_acct_credt_limit_remarks;
			
			g_acct_prob_order_limit_ab = m_acct_prob_order_limit_ab;
			g_acct_prob_order_limit_tr = m_acct_prob_order_limit_tr;

			
			g_acct_prob_order_limit_remarks_ab = m_acct_order_limit_remarks_ab;
			g_acct_prob_order_limit_remarks_tr = m_acct_order_limit_remarks_tr;


			/* End Code added by Billy Jay (04/23/2015) */

			g_acct_prop_credit_term = m_acct_prop_credit_term;
			g_acct_prop_credit_limit = m_acct_prop_credit_limit;
			g_acct_mw_price_code = m_acct_mw_price_code;
			g_acct_mw_price_desc = m_acct_mw_price_desc;
			g_acct_mw_price_commision_disc = m_acct_mw_price_commdisc;
			g_acct_mw_price_remarks = m_acct_mw_price_remarks;
			g_acct_ww_price_code = m_acct_ww_price_code;
			g_acct_ww_price_desc = m_acct_ww_price_desc;
			g_acct_ww_price_commision_disc = m_acct_ww_price_commdisc;
			g_acct_ww_price_remarks = m_acct_ww_price_remarks;
			g_acct_pwf_price_code = m_acct_pwf_price_code;
			g_acct_pwf_price_desc = m_acct_pwf_price_desc;
			g_acct_pwf_price_commision_disc = m_acct_pwf_price_commdisc;
			g_acct_pwf_price_remarks = m_acct_pwf_price_remarks;
			g_acct_pwr_price_code = m_acct_pwr_price_code;
			g_acct_pwr_price_desc = m_acct_pwr_price_desc;
			g_acct_pwr_price_commision_disc = m_acct_pwr_price_commdisc;
			g_acct_pwr_price_remarks = m_acct_pwr_price_remarks;
			g_acct_gw_price_code = m_acct_gw_price_code;
			g_acct_gw_price_desc = m_acct_gw_price_desc;
			g_acct_gw_price_commision_disc = m_acct_gw_price_commdisc;
			g_acct_gw_price_remarks = m_acct_gw_price_remarks;
			g_acct_tw_price_code = m_acct_tw_price_code;
			g_acct_tw_price_desc = m_acct_tw_price_desc;
			g_acct_tw_price_commision_disc = m_acct_tw_price_commdisc;
			g_acct_tw_price_remarks = m_acct_tw_price_remarks;

			g_acct_mz_price_code = m_acct_mz_price_code;
			g_acct_mz_price_desc = m_acct_mz_price_desc;
			g_acct_mz_price_commision_disc = m_acct_mz_price_commdisc;
			g_acct_mz_price_remarks = m_acct_mz_price_remarks;

			g_acct_nw_price_code = m_acct_nw_price_code;
			g_acct_nw_price_desc = m_acct_nw_price_desc;
			g_acct_nw_price_commision_disc = m_acct_nw_price_commdisc;
			g_acct_nw_price_remarks = m_acct_nw_price_remarks;

			g_acct_ec_price_code = m_acct_ec_price_code;
			g_acct_ec_price_desc = m_acct_ec_price_desc;
			g_acct_ec_price_commision_disc = m_acct_ec_price_commdisc;
			g_acct_ec_price_remarks = m_acct_ec_price_remarks;

			g_acct_ecu_price_code = m_acct_ecu_price_code;
			g_acct_ecu_price_desc = m_acct_ecu_price_desc;
			g_acct_ecu_price_commision_disc = m_acct_ecu_price_commdisc;
			g_acct_ecu_price_remarks = m_acct_ecu_price_remarks;

			/* if the proposedChanges.routeType != "" */
			/* display the proposed changes instead of the original */
			<% 
			if( acct_dtls.proposedChangesCA.route_type != "") { 
			%>
				var n_val_acctName = "<% Response.Write(acct_dtls.proposedChangesCA.acctName); %>";
				var n_val_acctOffcr = "<% Response.Write(acct_dtls.proposedChangesCA.acctOffcr); %>";
				var n_val_territory = "<% Response.Write(acct_dtls.proposedChangesCA.territory); %>";
				var n_val_area = "<% Response.Write(acct_dtls.proposedChangesCA.area); %>";
				var n_val_region = "<% Response.Write(acct_dtls.proposedChangesCA.region); %>";
				var n_val_regBusName = "<% Response.Write(acct_dtls.proposedChangesCA.regBusName); %>";
				var n_val_bussAdd = "<% Response.Write(acct_dtls.proposedChangesCA.bussAdd); %>";
				var n_val_delAdd = "<% Response.Write(acct_dtls.proposedChangesCA.delAdd); %>";
				/* Code added by Billy Jay (04/23/2015) */
				var n_val_propCredTermsArchitecturalBrand = "<% Response.Write(acct_dtls.proposedChangesCA.propCredTermsArchitecturalBrand); %>";
				var n_val_propCredTermsEcoforLumber = "<% Response.Write(acct_dtls.proposedChangesCA.propCredTermsEcoforLumber); %>";
				var n_val_propCredTermsEcoforPlywood = "<% Response.Write(acct_dtls.proposedChangesCA.propCredTermsEcoforPlywood); %>";

				var n_val_CredTermsRemarksArchitecturalBrand = "<% Response.Write(acct_dtls.proposedChangesCA.CredTermRemarksArchitecturalBrand); %>";
				var n_val_CredTermsRemarksEcoforLumber = "<% Response.Write(acct_dtls.proposedChangesCA.CredTermRemarksEcoforLumber); %>";
				var n_val_CredTermsRemarksEcoforPlywood = "<% Response.Write(acct_dtls.proposedChangesCA.CredTermRemarksEcoforPlywood); %>";
				
				var n_val_probOrderLimit_AB = "<% Response.Write(acct_dtls.proposedChangesCA.probOrderLimit_AB); %>";
				var n_val_probOrderLimit_TR = "<% Response.Write(acct_dtls.proposedChangesCA.probOrderLimit_TR); %>";
				
				var n_val_OrderLimitRemarks_AB = "<% Response.Write(acct_dtls.proposedChangesCA.OrderLimitRemarks_AB); %>";
				var n_val_OrderLimitRemarks_TR = "<% Response.Write(acct_dtls.proposedChangesCA.OrderLimitRemarks_TR); %>";

				/* End Code added by Billy Jay (04/23/2015) */
				var n_val_propCredTerms = "<% Response.Write(acct_dtls.proposedChangesCA.propCredTerms); %>";
				var n_val_propCredLimit = "<% Response.Write(acct_dtls.proposedChangesCA.propCredLimit); %>";
				
				var n_val_CredTermsRemarks = "<% Response.Write(acct_dtls.proposedChangesCA.CredTermRemarks); %>";
				var n_val_CredLimitRemarks = "<% Response.Write(acct_dtls.proposedChangesCA.CredLimitRemarks); %>";

				var n_val_pl_priceListCode_mw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_mw); %>";
				var n_val_pl_codeDesc_mw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_mw); %>";
				var n_val_pl_CommDisc_mw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_mw); %>";
				var n_val_pl_remarks_mw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_mw); %>";

				var n_val_pl_priceListCode_ww = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_ww); %>";
				var n_val_pl_codeDesc_ww = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_ww); %>";
				var n_val_pl_CommDisc_ww = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_ww); %>";
				var n_val_pl_remarks_ww = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_ww); %>";

				var n_val_pl_priceListCode_pwf = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_pwf); %>";
				var n_val_pl_codeDesc_pwf = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_pwf); %>";
				var n_val_pl_CommDisc_pwf = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_pwf); %>";
				var n_val_pl_remarks_pwf = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_pwf); %>";

				var n_val_pl_priceListCode_pwr = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_pwr); %>";
				var n_val_pl_codeDesc_pwr = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_pwr); %>";
				var n_val_pl_CommDisc_pwr = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_pwr); %>";
				var n_val_pl_remarks_pwr = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_pwr); %>";

				var n_val_pl_priceListCode_gw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_gw); %>";
				var n_val_pl_codeDesc_gw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_gw); %>";
				var n_val_pl_CommDisc_gw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_gw); %>";
				var n_val_pl_remarks_gw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_gw); %>";

				var n_val_pl_priceListCode_tw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_tw); %>";
				var n_val_pl_codeDesc_tw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_tw); %>";
				var n_val_pl_CommDisc_tw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_tw); %>";
				var n_val_pl_remarks_tw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_tw); %>";

				var n_val_pl_priceListCode_mz = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_mz); %>";
				var n_val_pl_codeDesc_mz = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_mz); %>";
				var n_val_pl_CommDisc_mz = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_mz); %>";
				var n_val_pl_remarks_mz = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_mz); %>";

				var n_val_pl_priceListCode_nw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_nw); %>";
				var n_val_pl_codeDesc_nw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_nw); %>";
				var n_val_pl_CommDisc_nw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_nw); %>";
				var n_val_pl_remarks_nw = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_nw); %>";

				var n_val_pl_priceListCode_ec = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_ec); %>";
				var n_val_pl_codeDesc_ec = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_ec); %>";
				var n_val_pl_CommDisc_ec = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_ec); %>";
				var n_val_pl_remarks_ec = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_ec); %>";

				var n_val_pl_priceListCode_ecu = "<% Response.Write(acct_dtls.proposedChangesCA.pl_priceListCode_ecu); %>";
				var n_val_pl_codeDesc_ecu = "<% Response.Write(acct_dtls.proposedChangesCA.pl_codeDesc_ecu); %>";
				var n_val_pl_CommDisc_ecu = "<% Response.Write(acct_dtls.proposedChangesCA.pl_CommDisc_ecu); %>";
				var n_val_pl_remarks_ecu = "<% Response.Write(acct_dtls.proposedChangesCA.pl_remarks_ecu); %>";
		   
				$("#txt_acct_name").attr('value', n_val_acctName);
				$("#txt_acct_officer").attr('value', n_val_acctOffcr);
				$("#txt_acct_territory").attr('value', n_val_territory);
				$("#txt_area").attr('value', n_val_area);
				$("#txt_region").attr('value', n_val_region);
				$("#txt_reg_name").attr('value', n_val_regBusName);
				$("#txt_business_add").attr('value', n_val_bussAdd);
				$("#txt_delivery_add").attr('value', n_val_delAdd);

				/* Code added by Billy Jay (04/23/2015) */
				$("#txt_credit_terms_architectural_brand").attr('value', n_val_propCredTermsArchitecturalBrand);
				$("#txt_credit_terms_eco_lumber").attr('value', n_val_propCredTermsEcoforLumber);
				$("#txt_credit_terms_eco_plywood").attr('value', n_val_propCredTermsEcoforPlywood);

				$("#txt_credit_terms_architectural_brand_remarks").attr('value', n_val_CredTermsRemarksArchitecturalBrand);
				$("#txt_credit_terms_eco_lumber_remarks").attr('value', n_val_CredTermsRemarksEcoforLumber);
				$("#txt_credit_terms_eco_plywood_remarks").attr('value', n_val_CredTermsRemarksEcoforPlywood);

				
				$("#txt_order_limit_ab").attr('value', n_val_probOrderLimit_AB);
				$("#txt_order_limit_tr").attr('value', n_val_probOrderLimit_TR);
				
				$("#txt_order_limit_remarks_ab").attr('value', n_val_OrderLimitRemarks_AB);
				$("#txt_order_limit_remarks_tr").attr('value', n_val_OrderLimitRemarks_TR);
				/* End Code added by Billy Jay (04/23/2015) */

				$("#txt_credit_terms").attr('value', n_val_propCredTerms);
			   // alert("<% Response.Write(acct_dtls.proposedChangesCA.route_type); %>");
				$("#txt_credit_limit").attr('value', n_val_propCredLimit);
				$("#txt_credit_terms_remarks").attr('value', n_val_CredTermsRemarks);
				$("#txt_credit_limit_remarks").attr('value', n_val_CredLimitRemarks);

				$("#txt_mw_price_code").attr('value', n_val_pl_priceListCode_mw);
				$("#txt_mw_price_desc").attr('value', n_val_pl_codeDesc_mw);
				$("#txt_mw_price_commision_disc").attr('value', n_val_pl_CommDisc_mw);
				$("#txt_mw_price_remarks").attr('value', n_val_pl_remarks_mw);
				$("#txt_ww_price_code").attr('value', n_val_pl_priceListCode_ww);
				$("#txt_ww_price_desc").attr('value', n_val_pl_codeDesc_ww);
				$("#txt_ww_price_commision_disc").attr('value', n_val_pl_CommDisc_ww);
				$("#txt_ww_price_remarks").attr('value', n_val_pl_remarks_ww);
				$("#txt_pwf_price_code").attr('value', n_val_pl_priceListCode_pwf);
				$("#txt_pwf_price_desc").attr('value', n_val_pl_codeDesc_pwf);
				$("#txt_pwf_price_commision_disc").attr('value', n_val_pl_CommDisc_pwf);
				$("#txt_pwf_price_remarks").attr('value', n_val_pl_remarks_pwf);
				$("#txt_pwr_price_code").attr('value', n_val_pl_priceListCode_pwr);
				$("#txt_pwr_price_desc").attr('value', n_val_pl_codeDesc_pwr);
				$("#txt_pwr_price_commision_disc").attr('value', n_val_pl_CommDisc_pwr);
				$("#txt_pwr_price_remarks").attr('value', n_val_pl_remarks_pwr);
				$("#txt_gw_price_code").attr('value', n_val_pl_priceListCode_gw);
				$("#txt_gw_price_desc").attr('value', n_val_pl_codeDesc_gw);
				$("#txt_gw_price_commision_disc").attr('value', n_val_pl_CommDisc_gw);
				$("#txt_gw_price_remarks").attr('value', n_val_pl_remarks_gw);
				$("#txt_tw_price_code").attr('value', n_val_pl_priceListCode_tw);
				$("#txt_tw_price_desc").attr('value', n_val_pl_codeDesc_tw);
				$("#txt_tw_price_commision_disc").attr('value', n_val_pl_CommDisc_tw);
				$("#txt_tw_price_remarks").attr('value', n_val_pl_remarks_tw);

				$("#txt_mz_price_code").attr('value', n_val_pl_priceListCode_mz);
				$("#txt_mz_price_desc").attr('value', n_val_pl_codeDesc_mz);
				$("#txt_mz_price_commision_disc").attr('value', n_val_pl_CommDisc_mz);
				$("#txt_mz_price_remarks").attr('value', n_val_pl_remarks_mz);

				$("#txt_nw_price_code").attr('value', n_val_pl_priceListCode_nw);
				$("#txt_nw_price_desc").attr('value', n_val_pl_codeDesc_nw);
				$("#txt_nw_price_commision_disc").attr('value', n_val_pl_CommDisc_nw);
				$("#txt_nw_price_remarks").attr('value', n_val_pl_remarks_nw);

				$("#txt_ec_price_code").attr('value', n_val_pl_priceListCode_ec);
				$("#txt_ec_price_desc").attr('value', n_val_pl_codeDesc_ec);
				$("#txt_ec_price_commision_disc").attr('value', n_val_pl_CommDisc_ec);
				$("#txt_ec_price_remarks").attr('value', n_val_pl_remarks_ec);

				$("#txt_ecu_price_code").attr('value', n_val_pl_priceListCode_ecu);
				$("#txt_ecu_price_desc").attr('value', n_val_pl_codeDesc_ecu);
				$("#txt_ecu_price_commision_disc").attr('value', n_val_pl_CommDisc_ecu);
				$("#txt_ecu_price_remarks").attr('value', n_val_pl_remarks_ecu);

				/* put green background on the changed fields */
				// ALSO SAVE ORIGINAL VALUES
				$("#txt_acct_name").attr('orig_value', n_val_acctName);
				$("#txt_acct_officer").attr('orig_value', n_val_acctOffcr);
				$("#txt_acct_territory").attr('orig_value', n_val_territory);
				$("#txt_area").attr('orig_value', n_val_area);
				$("#txt_region").attr('orig_value', n_val_region);
				$("#txt_reg_name").attr('orig_value', n_val_regBusName);
				$("#txt_business_add").attr('orig_value', n_val_bussAdd);
				$("#txt_delivery_add").attr('orig_value', n_val_delAdd);

				/* Code added by Billy Jay (04/23/2015) */
				$("#txt_credit_terms_architectural_brand").attr('orig_value', n_val_propCredTermsArchitecturalBrand);
				$("#txt_credit_terms_eco_lumber").attr('orig_value', n_val_propCredTermsEcoforLumber);
				$("#txt_credit_terms_eco_plywood").attr('orig_value', n_val_propCredTermsEcoforPlywood);

				$("#txt_credit_terms_architectural_brand_remarks").attr('orig_value', n_val_CredTermsRemarksArchitecturalBrand);
				$("#txt_credit_terms_eco_lumber_remarks").attr('orig_value', n_val_CredTermsRemarksEcoforLumber);
				$("#txt_credit_terms_eco_plywood_remarks").attr('orig_value', n_val_CredTermsRemarksEcoforPlywood);

				
				$("#txt_order_limit_ab").attr('orig_value', n_val_probOrderLimit_AB);
				$("#txt_order_limit_tr").attr('orig_value', n_val_probOrderLimit_TR);
				
				$("#txt_order_limit_remarks_ab").attr('orig_value', n_val_OrderLimitRemarks_AB);
				$("#txt_order_limit_remarks_tr").attr('orig_value', n_val_OrderLimitRemarks_TR);
				/* End Code added by Billy Jay (04/23/2015) */

				$("#txt_credit_terms").attr('orig_value', n_val_propCredTerms);
				$("#txt_credit_limit").attr('orig_value', n_val_propCredLimit);
				$("#txt_credit_terms_remarks").attr('orig_value', n_val_CredTermsRemarks);
				$("#txt_credit_limit_remarks").attr('orig_value', n_val_CredLimitRemarks);

				$("#txt_mw_price_code").attr('orig_value', n_val_pl_priceListCode_mw);
				$("#txt_mw_price_desc").attr('orig_value', n_val_pl_codeDesc_mw);
				$("#txt_mw_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_mw);
				$("#txt_mw_price_remarks").attr('orig_value', n_val_pl_remarks_mw);
				$("#txt_ww_price_code").attr('orig_value', n_val_pl_priceListCode_ww);
				$("#txt_ww_price_desc").attr('orig_value', n_val_pl_codeDesc_ww);
				$("#txt_Ww_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_ww);
				$("#txt_ww_price_remarks").attr('orig_value', n_val_pl_remarks_ww);
				$("#txt_pwf_price_code").attr('orig_value', n_val_pl_priceListCode_pwf);
				$("#txt_pwf_price_desc").attr('orig_value', n_val_pl_codeDesc_pwf);
				$("#txt_pwf_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_pwf);
				$("#txt_pwf_price_remarks").attr('orig_value', n_val_pl_remarks_pwf);
				$("#txt_pwr_price_code").attr('orig_value', n_val_pl_priceListCode_pwr);
				$("#txt_pwr_price_desc").attr('orig_value', n_val_pl_codeDesc_pwr);
				$("#txt_pwr_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_pwr);
				$("#txt_pwr_price_remarks").attr('orig_value', n_val_pl_remarks_pwr);
				$("#txt_gw_price_code").attr('orig_value', n_val_pl_priceListCode_gw);
				$("#txt_gw_price_desc").attr('orig_value', n_val_pl_codeDesc_gw);
				$("#txt_gw_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_gw);
				$("#txt_gw_price_remarks").attr('orig_value', n_val_pl_remarks_gw);
				$("#txt_tw_price_code").attr('orig_value', n_val_pl_priceListCode_tw);
				$("#txt_tw_price_desc").attr('orig_value', n_val_pl_codeDesc_tw);
				$("#txt_tw_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_tw);
				$("#txt_tw_price_remarks").attr('orig_value', n_val_pl_remarks_tw);

				$("#txt_mz_price_code").attr('orig_value', n_val_pl_priceListCode_mz);
				$("#txt_mz_price_desc").attr('orig_value', n_val_pl_codeDesc_mz);
				$("#txt_mz_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_mz);
				$("#txt_mz_price_remarks").attr('orig_value', n_val_pl_remarks_mz);
				
				$("#txt_nw_price_code").attr('orig_value', n_val_pl_priceListCode_nw);
				$("#txt_nw_price_desc").attr('orig_value', n_val_pl_codeDesc_nw);
				$("#txt_tnw_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_nw);
				$("#txt_nw_price_remarks").attr('orig_value', n_val_pl_remarks_nw);

				$("#txt_ec_price_code").attr('orig_value', n_val_pl_priceListCode_ec);
				$("#txt_ec_price_desc").attr('orig_value', n_val_pl_codeDesc_ec);
				$("#txt_ec_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_ec);
				$("#txt_ec_price_remarks").attr('orig_value', n_val_pl_remarks_ec);

				$("#txt_ecu_price_code").attr('orig_value', n_val_pl_priceListCode_ecu);
				$("#txt_ecu_price_desc").attr('orig_value', n_val_pl_codeDesc_ecu);
				$("#txt_ecu_price_commision_disc").attr('orig_value', n_val_pl_CommDisc_ecu);
				$("#txt_ecu_price_remarks").attr('orig_value', n_val_pl_remarks_ecu);
				 
				if (g_acct_name != n_val_acctName ) { $("#txt_acct_name").addClass("changed_fields"); }
				if (g_acct_acct_officer != n_val_acctOffcr ) { $("#txt_acct_officer").addClass("changed_fields"); }
				if (g_acct_territory != n_val_territory ) { $("#txt_acct_territory").addClass("changed_fields"); }
				if (g_acct_area != n_val_area ) { $("#txt_area").addClass("changed_fields"); }
				if (g_acct_region != n_val_region ) { $("#txt_region").addClass("changed_fields"); }
				if (g_acct_reg_name != n_val_regBusName ) { $("#txt_reg_name").addClass("changed_fields"); }
				if (g_acct_business_add != n_val_bussAdd ) { $("#txt_business_add").addClass("changed_fields"); }
				if (g_acct_delivery_add != n_val_delAdd ) { $("#txt_delivery_add").addClass("changed_fields"); }

				/* Code added by Billy Jay (04/23/2015) */

				if (g_acct_prop_credit_term_architectural_brand != n_val_propCredTermsArchitecturalBrand ) { $("#txt_credit_terms_architectural_brand").addClass("changed_fields"); }
				if (g_acct_prop_credit_term_ecofor_lumber != n_val_propCredTermsEcoforLumber ) { $("#txt_credit_terms_eco_lumber").addClass("changed_fields"); }
				if (g_acct_prop_credit_term_ecofor_plywood != n_val_propCredTermsEcoforPlywood ) { $("#txt_credit_terms_eco_plywood").addClass("changed_fields"); }

				if (g_acct_prop_credit_term_remarks_architectural_brand  != n_val_CredTermsRemarksArchitecturalBrand ) { $("#txt_credit_terms_architectural_brand_remarks").addClass("changed_fields"); }
				if (g_acct_prop_credit_term_remarks_ecofor_lumber != n_val_CredTermsRemarksEcoforLumber ) { $("#txt_credit_terms_eco_lumber_remarks").addClass("changed_fields"); }
				if (g_acct_prop_credit_term_remarks_ecofor_plywood != n_val_CredTermsRemarksEcoforPlywood ) { $("#txt_credit_terms_eco_plywood_remarks").addClass("changed_fields"); }
				
				if (g_acct_prob_order_limit_ab != n_val_probOrderLimit_AB ) { $("#txt_order_limit_ab").addClass("changed_fields"); }
				if (g_acct_prob_order_limit_tr != n_val_probOrderLimit_TR ) { $("#txt_order_limit_tr").addClass("changed_fields"); }

				if (g_acct_prob_order_limit_remarks_ab != n_val_OrderLimitRemarks_AB ) { $("#txt_order_limit_remarks_ab").addClass("changed_fields"); }
				if (g_acct_prob_order_limit_remarks_tr != n_val_OrderLimitRemarks_TR ) { $("#txt_order_limit_remarks_tr").addClass("changed_fields"); }
				/* End Code added by Billy Jay (04/23/2015) */

				if (g_acct_prop_credit_term != n_val_propCredTerms ) { $("#txt_credit_terms").addClass("changed_fields"); }
				if (g_acct_prop_credit_limit != n_val_propCredLimit ) { $("#txt_credit_limit").addClass("changed_fields"); }
				if (g_acct_prop_credit_term_remarks != n_val_CredTermsRemarks ) { $("#txt_credit_terms_remarks").addClass("changed_fields"); }
				if (g_acct_prop_credit_limit_remarks != n_val_CredLimitRemarks ) { $("#txt_credit_limit_remarks").addClass("changed_fields"); }

				if (g_acct_mw_price_code != n_val_pl_priceListCode_mw ) { $("#txt_mw_price_code").addClass("changed_fields"); }
				if (g_acct_mw_price_desc != n_val_pl_codeDesc_mw ) { $("#txt_mw_price_desc").addClass("changed_fields"); }
				if (g_acct_mw_price_commision_disc != n_val_pl_CommDisc_mw) { $("#txt_mw_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_mw_price_remarks != n_val_pl_remarks_mw ) { $("#txt_mw_price_remarks").addClass("changed_fields"); }
				if (g_acct_ww_price_code != n_val_pl_priceListCode_ww ) { $("#txt_ww_price_code").addClass("changed_fields"); }
				if (g_acct_ww_price_desc != n_val_pl_codeDesc_ww ) { $("#txt_ww_price_desc").addClass("changed_fields"); }
				if (g_acct_ww_price_commision_disc != n_val_pl_CommDisc_ww) { $("#txt_ww_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_ww_price_remarks != n_val_pl_remarks_ww ) { $("#txt_ww_price_remarks").addClass("changed_fields"); }
				if (g_acct_pwf_price_code != n_val_pl_priceListCode_pwf ) { $("#txt_pwf_price_code").addClass("changed_fields"); }
				if (g_acct_pwf_price_desc != n_val_pl_codeDesc_pwf ) { $("#txt_pwf_price_desc").addClass("changed_fields"); }
				if (g_acct_pwf_price_commision_disc != n_val_pl_CommDisc_pwf) { $("#txt_pwf_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_pwf_price_remarks != n_val_pl_remarks_pwf ) { $("#txt_pwf_price_remarks").addClass("changed_fields"); }
				if (g_acct_pwr_price_code != n_val_pl_priceListCode_pwr ) { $("#txt_pwr_price_code").addClass("changed_fields"); }
				if (g_acct_pwr_price_desc != n_val_pl_codeDesc_pwr ) { $("#txt_pwr_price_desc").addClass("changed_fields"); }
				if (g_acct_pwr_price_commision_disc != n_val_pl_CommDisc_pwr) { $("#txt_pwr_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_pwr_price_remarks != n_val_pl_remarks_pwr ) { $("#txt_pwr_price_remarks").addClass("changed_fields"); }
				if (g_acct_gw_price_code != n_val_pl_priceListCode_gw ) { $("#txt_gw_price_code").addClass("changed_fields"); }
				if (g_acct_gw_price_desc != n_val_pl_codeDesc_gw ) { $("#txt_gw_price_desc").addClass("changed_fields"); }
				if (g_acct_gw_price_commision_disc != n_val_pl_CommDisc_gw) { $("#txt_gw_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_gw_price_remarks != n_val_pl_remarks_gw ) { $("#txt_gw_price_remarks").addClass("changed_fields"); }
				if (g_acct_tw_price_code != n_val_pl_priceListCode_tw ) { $("#txt_tw_price_code").addClass("changed_fields"); }
				if (g_acct_tw_price_desc != n_val_pl_codeDesc_tw ) { $("#txt_tw_price_desc").addClass("changed_fields"); }
				if (g_acct_tw_price_commision_disc != n_val_pl_CommDisc_tw) { $("#txt_tw_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_tw_price_remarks != n_val_pl_remarks_tw ) { $("#txt_tw_price_remarks").addClass("changed_fields"); }

				if (g_acct_mz_price_code != n_val_pl_priceListCode_mz ) { $("#txt_mz_price_code").addClass("changed_fields"); }
				if (g_acct_mz_price_desc != n_val_pl_codeDesc_mz ) { $("#txt_mz_price_desc").addClass("changed_fields"); }
				if (g_acct_mz_price_commision_disc != n_val_pl_CommDisc_mz) { $("#txt_mz_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_mz_price_remarks != n_val_pl_remarks_mz ) { $("#txt_mz_price_remarks").addClass("changed_fields"); }

				if (g_acct_nw_price_code != n_val_pl_priceListCode_nw ) { $("#txt_nw_price_code").addClass("changed_fields"); }
				if (g_acct_nw_price_desc != n_val_pl_codeDesc_nw ) { $("#txt_nw_price_desc").addClass("changed_fields"); }
				if (g_acct_nw_price_commision_disc != n_val_pl_CommDisc_nw) { $("#txt_nw_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_nw_price_remarks != n_val_pl_remarks_nw ) { $("#txt_nw_price_remarks").addClass("changed_fields"); }

				if (g_acct_ec_price_code != n_val_pl_priceListCode_ec ) { $("#txt_ec_price_code").addClass("changed_fields"); }
				if (g_acct_ec_price_desc != n_val_pl_codeDesc_ec ) { $("#txt_ec_price_desc").addClass("changed_fields"); }
				if (g_acct_ec_price_commision_disc != n_val_pl_CommDisc_ec) { $("#txt_ec_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_ec_price_remarks != n_val_pl_remarks_ec ) { $("#txt_ec_price_remarks").addClass("changed_fields"); }

				if (g_acct_ecu_price_code != n_val_pl_priceListCode_ecu ) { $("#txt_ecu_price_code").addClass("changed_fields"); }
				if (g_acct_ecu_price_desc != n_val_pl_codeDesc_ecu ) { $("#txt_ecu_price_desc").addClass("changed_fields"); }
				if (g_acct_ecu_price_commision_disc != n_val_pl_CommDisc_ec) { $("#txt_ecu_price_commision_disc").addClass("changed_fields"); }
				if (g_acct_ecu_price_remarks != n_val_pl_remarks_ecu ) { $("#txt_ecu_price_remarks").addClass("changed_fields"); }
			<% } %>
			var m_sec_state_doc_status = "<%:AppHelper.GetDocStateMsg(acct_dtls.curr_doc_DocChangesStatus) %>";
			var m_sec_state_doc_status_id = "<%:acct_dtls.curr_doc_DocChangesStatus %>";
			
			isAllDataLoaded = true;
		}
		
	</script>


	<div class="bl_box">
	<div class="page_header">
		<table border="0" cellpadding="0" cellspacing="0" >
			<tr>
				<td align="left">
					<b>Account Details</b>
				</td>
				<td align="right">
					<%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
				</td>
			</tr>
		</table>
	</div>
	
	<span id="span_bubbler_checker" style="display:none;"></span>
	
	<div id="tabs">
	<ul>
		<li><a href="#tabs-1">Main Info</a></li>
		<li><a href="#tabs-23">Personal Info</a></li>
		<li><a href="#tabs-2">Business</a></li>
		<li><a href="#tabs-3">Products</a></li>
		<li><a href="#tabs-4">Credit Investigation</a></li>
		<li style="display:none;"><a href="#tabs-5">Personal Info</a></li>
		<li style="display:none;"><a href="#tabs-6">Strategies</a></li>
		<li style="display:none;"><a href="#tabs-7">Activities and Events</a></li>
		<li style="display:none;"><a href="#tabs-8">Business Review</a></li>
		<li style="display:none;"><a href="#tabs-9">Contracts and Agreements</a></li>
		<li><a href="#tabs-24">Activities</a></li>
		<li style="display:none;"><a href="#tabs-10">Change Log</a></li>
		<li><a href="<%=ResolveUrl("~/") %>Customer/GetCCADocChanges?ccanum=<%: DocCCaNum %>">Other Changes</a></li>
		<li><a href="<%=ResolveUrl("~/") %>Customer/GetCCADocRemarks?ccanum=<%: DocCCaNum %>">Approver Remarks</a></li>
	</ul>
	<div id="tabs-1">
		<!-- TAB-1 CONTENT START -->

		<table width="100%" cellpadding="1" cellspacing="0" border="0">
			<tr>
				<td colspan="5" align="right">
					<span id="doc_stat_msg"></span>
				</td>
			</tr>
			<tr>
				<td colspan="5" align="right">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td>CCA Number</td>
				<td><input type="text" id="txt_acct_ccanum" readonly="readonly" /></td>
				<td></td>
				<td>Final Account Code</td>
				<td>
					<input type="text" id="txt_final_acct_code" readonly="readonly" class="readonly_fields" />
				</td>
			</tr>
			<tr>
				<td>Account Classification</td>
				<td>
					<select id="slt_acct_classification">
						<option value="REGULAR" >Regular</option>
						<option value="WALKIN" >Walk-In</option>
					</select>
					
					<input type="hidden" id="txt_acct_classification" readonly="readonly" />
				   
				</td>
				<td></td>
				<td></td>
				<td></td>
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
					<label><input type="radio" id="acc_key_yes" name="option2" value="Direct" checked="checked" />Yes</label>
				
					<label><input type="radio" id="acc_key_no" name="option2" value="Indirect" />No</label>
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
					<input type="hidden" onclick="javascript:LookUpData('txt_acct_class', 'ListOfBPClass');" id="txt_acct_class" name="txt_acct_class" readonly="readonly" />
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
					<input type="text" id="txt_type_of_account" readonly="readonly" onclick="javascript:LookUpData('txt_type_of_account', 'ListOfTypeOfAccounts');" />
				</td>
				<%--<td>Account Status</td>
				<td>
					<input type="text" id="txt_transaction_type" readonly="readonly" />
				</td>--%>
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
					<input type="text" id="txt_business_add" style="width:99%;" maxlength="200" />
				</td>
			</tr>
			<tr>
				<td>Delivery Address</td>
				<td colspan="4">
					<input type="text" id="txt_delivery_add" style="width:99%;" maxlength="200" />
				</td>
			</tr>
			<tr>
				<td>MTD Sales</td>
				<td colspan="4">
					<input type="text" id="txt_mtd_sales" readonly="readonly" />
				</td>
			</tr>
		</table>
		<br />
		<% if (acct_dtls.curr_doc_DocStatus == "1000")
		   { %>
		<div class="blinker" >
			<!-- Financial Information -->
			<table cellspacing="0" cellpadding="3" border="0">
				<tr style="background:#ededed;">
					<td colspan="5" align="left" ><b>Financial Information</b></td>
				</tr>
				<tr>
					<td valign="top">
						<table cellspacing="0" cellpadding="3" border="0" style=" border:1px solid #ededed; font-family:Arial; font-size:11px; ">
							<tr>
								<td>MTD Sales</td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('mtdytd_sales');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
							<tr>
								<td>Pending Order</td>
								<td></td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('pending_orders');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
							<tr>
								<td>Deliveries</td>
								<td></td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('deliveries');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td valign="top">
						<table cellspacing="0" cellpadding="3" border="0" style=" border:1px solid #ededed; font-family:Arial; font-size:11px; ">
							<tr>
								<td>Balance Order</td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('balance_order');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
							<tr>
								<td>Collections</td>
								<td></td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('collections');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
							<tr>
								<td>Account Balance</td>
								<td></td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('account_balance');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td valign="top">
						<table cellspacing="0" cellpadding="3" border="0" style=" border:1px solid #ededed; font-family:Arial; font-size:11px; ">
							<tr>
								<td>Bounce Checks</td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('bounce_checks');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
							<tr>
								<td>Past Dues</td>
								<td></td>
								<td>
									<a href="javascript:;" onclick="javascript:ShowReportViewer('past_dues');" ><img src="<%=ResolveUrl("~/") %>Images/report_magnify.png" style="border:0;" /></a>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!-- financial info -->
		</div>
		<br />
		<% } %>
		<table width="100%" cellpadding="1" cellspacing="0" border="0">
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
										<td><a href="javascript:AddEntryCommon('tbl_partner_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
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
										<td><a href="javascript:AddEntryCommon('tbl_corpo_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
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
					<div class="blinker">
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
					<div class="blinker">
						<h2>Attachments (Pre-Enrollment Documents)</h2>
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td>
									<!-- Articles of Incorporation -->
									Customer Info Sheet
								</td>
								<td>
									<input type="text" id="txt_articles_of_inc" name="txt_articles_of_inc" onclick="javascript:CreateUploadingBox('txt_articles_of_inc');" readonly="readonly" />
									<input type="hidden" id="txt_articles_of_inc_forupload" />
								</td>
								<td>
									<a href="javascript:;" onclick="javascript:DeleteFileAttachment('AOI');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>
									<!-- Financial Statements -->
									Bank Authorization
								</td>
								<td>
									<input type="text" id="txt_financial_statement" name="txt_financial_statement" onclick="javascript:CreateUploadingBox('txt_financial_statement');" readonly="readonly" />
									<input type="hidden" id="txt_financial_statement_forupload" />
								</td>
								<td>
									<a href="javascript:;" onclick="javascript:DeleteFileAttachment('FS');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>Income Tax Return</td>
								<td>
									<input type="text" id="txt_ITR" name="txt_ITR" onclick="javascript:CreateUploadingBox('txt_ITR');" readonly="readonly" />
									<input type="hidden" id="txt_ITR_forupload" />
								</td>
								<td>   
									<a href="javascript:;" onclick="javascript:DeleteFileAttachment('ITR');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>BIR Registration</td>
								<td>
									<input type="text" id="txt_bir_reg" name="txt_bir_reg" onclick="javascript:CreateUploadingBox('txt_bir_reg');" readonly="readonly" />
									<input type="hidden" id="txt_bir_reg_forupload" />
								</td>
								<td>
									<a href="javascript:;" onclick="javascript:DeleteFileAttachment('BIR');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>Latest Business Permit</td>
								<td>
									<input type="text" id="txt_business_permit" name="txt_business_permit" onclick="javascript:CreateUploadingBox('txt_business_permit');" readonly="readonly" />
									<input type="hidden" id="txt_business_permit_forupload" />
								</td>
								<td>
									<a href="javascript:;" onclick="javascript:DeleteFileAttachment('BP');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>Other</td>
								<td>
									<input type="text" id="txt_attch_other" name="txt_attch_other" onclick="javascript:CreateUploadingBox('txt_attch_other');" readonly="readonly" />
									<input type="hidden" id="txt_attch_other_forupload" />
								</td>
								<td>
									<a href="javascript:;" onclick="javascript:DeleteFileAttachment('OTHER');"><img src="<%=ResolveUrl("~/") %>Images/delete.png" style="border:0;" /></a>
								</td>
								<td></td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
			
		</table>

		<div >
			<i style="color:Red; font-size: 10">
				To fast track your account approval process, please provide complete bank information
			</i> 
		</div>
		<!-- TAB-1 CONTENT END -->
	</div>

	<div id="tabs-2">
		<!-- TAB-2 CONTENT START -->
		
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
				<td><input type="text" id="txt_credit_limit" /></td>
				<td><input type="text" id="txt_credit_limit_remarks" tobubble="tobubble" /></td>
			</tr>
			<tr>
				<td>Proposed AB Order Limit</td>
				<td><input type="text" id="txt_order_limit_ab" /></td>
				<td><input type="text" id="txt_order_limit_remarks_ab"  tobubble="tobubble" /></td>
			</tr>
			<tr>
				<td>Proposed TR Order Limit</td>
				<td><input type="text" id="txt_order_limit_tr" /></td>
				<td><input type="text" id="txt_order_limit_remarks_tr"  tobubble="tobubble" /></td>
			</tr>
			<tr>
				<td>Proposed Credit Terms</td>
				<td><input type="text" id="txt_credit_terms" onclick="javascript:LookUpData('txt_credit_terms', 'ListOfPaymentGroup');" readonly="readonly" /></td>
				<td><input type="text" id="txt_credit_terms_remarks" tobubble="tobubble" /></td>
			</tr>
			<tr>
				<td>Proposed Credit Terms:</td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td style="text-indent:30px">Architectural Brand </td>
				<td><input type="text" id="txt_credit_terms_architectural_brand" onclick="javascript:LookUpData('txt_credit_terms_architectural_brand', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
				<td><input type="text" id="txt_credit_terms_architectural_brand_remarks" tobubble="tobubble" /></td>
			</tr>
			<tr>
				<td style="text-indent:30px ">Ecofor Lumber </td>
				<td><input type="text" id="txt_credit_terms_eco_lumber" onclick="javascript:LookUpData('txt_credit_terms_eco_lumber', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
				<td><input type="text" id="txt_credit_terms_eco_lumber_remarks" tobubble="tobubble"  /></td>
			</tr>
			<tr>
				<td style="text-indent:30px">Ecofor Plywood </td>
				<td><input type="text" id="txt_credit_terms_eco_plywood" onclick="javascript:LookUpData('txt_credit_terms_eco_plywood', 'ListOfPaymentGroup');" value="- Cash Basic -" readonly="readonly" /></td>
				<td><input type="text" id="txt_credit_terms_eco_plywood_remarks" tobubble="tobubble" /></td>
			</tr>
		</table>
		<br />
		<table cellpadding="1" cellspacing="0" border="0">
			<tr>
				<td align="center">Proposed Price Lists</td>
				<td align="center">Code</td>
				<td align="center">Description</td>
				<td align="center">Comm. & Disc.</td>
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
		<div class="blinker">
			Socio Economic Class of Customers: <input type="text" id="txt_eco_class_of_customer" /> <br />
			Number of Outlets: <input type="text" id="txt_no_of_outlets" /> <br />
		</div><br />
		<div class="blinker">
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
					<td><a href="javascript:AddEntryCommon('tbl_outlet_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="blinker">
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
					<td><a href="javascript:AddEntryCommon('tbl_mjcust_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
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
		<br />
		<div class="blinker">
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
		<br />
		<div class="blinker">
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
		</div>
		<br />
		<div class="blinker">
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
					<td><input style="width:95%;" type="text" ></td>
					<td><a href="javascript:AddEntryCommon('tbl_wood_supplier');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
	</div>

	<!-- CI INFO TAB -->
	<div id="tabs-4">
		<div class="simple_box" style="border:none;">
			<i style="color:Red; font-size: 12">The information under this tab is confidential and should not be shared with the customer.</i> 
		</div>
		<div class="blinker">
			Bank
			<table width="100%" id="tbl_bank_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td style="width:10%;" align="center">Bank</td>
					<td style="width:10%;" align="center">Branch</td>
					<td style="width:10%;" align="center">Address</td>
					<td style="width:10%;" align="center">Acct. No.</td>
					<td style="width:10%;" align="center">Cnct. Person</td>
					<td style="width:10%;" align="center">Cnct. No.</td>
					<td style="width:10%;" align="center">Avg. Daily Bal.</td>
					<td style="width:30%;" align="center">Remarks</td>
					<td align="center"></td>
				</tr>
				<tr>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><input style="width:95%;" type="text" /></td>
					<td><a href="javascript:AddEntryCommon('tbl_bank_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="blinker">
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
					<td><input style="95%" type="text" onclick="javascript:LookUpData('tbl_land_list tr:last td:nth-child(1) input[type=text]', 'ListOfLandType');" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryCommon('tbl_land_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="blinker">
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
					<td><input style="95%" type="text" onclick="javascript:LookUpData('tbl_building_list tr:last td:nth-child(1) input[type=text]', 'ListOfBuildingType');" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryCommon('tbl_building_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="blinker">
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
					<td><a href="javascript:AddEntryCommon('tbl_vehicle_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="blinker">
			Other Assets: <input type="text" id="txt_other_assets" />
		</div>
		<br />
		<div class="blinker">
			Other Business
			<table id="tbl_asset_list" cellpadding="1px" cellspacing="0" border="0">
				<tr>
					<td align="center">Registered Name</td>
					<td align="center">Nature</td>
					<td align="center">Location</td>
					<td align="center">% of Ownership</td>
					<td></td>
				</tr>
				<tr>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><input style="95%" type="text" /></td>
					<td><a href="javascript:AddEntryCommon('tbl_asset_list');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<br />
		<div class="blinker">
			<table cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td> CIBI Remarks</td>
				</tr>
				<tr>
					<td>
						
						<textarea id="txt_cibi_remakrs" style="font-family:Arial; font-size:11px; width:98%; height:100px;" ></textarea>
					</td>
				</tr>
				<tr>
					<td> Supplier Info Remarks</td>
				</tr>
				<tr>
					<td>
						 
						 <textarea id="txt_supplyinfo_remakrs" style="font-family:Arial; font-size:11px; width:98%; height:100px;" ></textarea>
					</td>
				</tr>
			</table>
		</div>
		
	</div>
	<!-- CI INFO TAB - END -->

	
	<!-- Activities and Events TAB-->
	<div id="tabs-24">
	  
		<b>Related Activities</b>
		<hr />
		<table id="tblRelatedActivities" cellpadding="1" cellspacing="0" border="0">
			<tr>
				<td>Activity:</td>
				<td><input type="text" id="txt_activity" value="All" name="txt_activity" readonly="readonly" onclick="javascript:LookUpData('txt_activity', 'ListOfActivities');" /></td>
				<td>Type:</td>
				<td><input type="text" id="txt_act_type" value="All" name="txt_act_type" readonly="readonly" onclick="javascript:LookUpData('txt_act_type', 'ListOfActivitiesType');" /></td>
			</tr>
		</table>  
		<br />
		<div class="blinker" style="overflow:auto">
			<table id="tblActivities" width="100%" cellpadding="1" cellspacing="0" border="0">
				<tr>
					<td>Activity</td>
					<td>Type</td>
					<td>Contact Person</td>
					<td>Subject</td>
					<td>Start Time</td>
					<td>End Time</td>
					<td>Content</td>
					<td>Remarks</td>
				</tr>
				<tr class="last_row" style="display:none">
					<td><input type="text" readonly="readonly" style="width:70%; background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style="width:70%; background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style="width:70%; background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style="width:70%; background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style="width:40%; background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style="width:40%; background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style=" background-color:#ededed" /></td>
					<td><input type="text" readonly="readonly" style=" background-color:#ededed" /></td>
				</tr>
			</table>
		</div>
	</div>
	<!--END Activities and Events TAB-->


	<div id="tabs-5">
		<!-- Personal -->
		
	</div>
	<div id="tabs-6">
		<!-- Strategies -->
		
	</div>
	<div id="tabs-7">
		<!-- Activities and Events -->
		
	</div>
	<div id="tabs-8">
		<!-- Business Review -->
		
	</div>
	<div id="tabs-9">
		<!-- Contracts and Agreements -->
		
	</div>
	<div id="tabs-10">
		<!-- Change Log -->
		CHANGE LOG
	</div>
	<div id="tabs-22">
		<!-- Other Changes -->
		<div id="div_other_changes" style="padding:11px;">
		</div>
	</div>
	 <div id="tabs-23">
		<!-- Personal Info -->
		<div class="blinker">
			<b>Birthday Events</b>
			<table id="tbl_birthday_events" width="100%" cellpadding="1" cellspacing="0" border="0">
				<tr>
					<td align="center">Name</td>
					<td align="center">Event</td>
					<td align="center">Date</td>
					<td align="center">Contact Number</td>
					<td align="center"></td>
				</tr>
				<tr class="row_input">
					<td><input type="text" maxlength="110" style="width:98%"/></td>
					<td><input type="text" maxlength="220" style="width:98%"/></td>
					<td><input type="text" style="width:98%"/></td>
					<td><input type="text" maxlength="20" style="width:98%"/></td>
					<td><a href="javascript:AddEntryCommon('tbl_birthday_events');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
		<hr />
		<div class="blinker">
			<b>Other Specified Events</b>
			<table id="tbl_specified_events" width="100%" cellpadding="1" cellspacing="0" border="0">
				<tr>
					<td align="center">Name</td>
					<td align="center">Event</td>
					<td align="center">Date</td>
					<td align="center">Contact Number</td>
					<td align="center"></td>
				</tr>
				<tr class="row_input">
					<td><input type="text" maxlength="110" style="width:98%;" /></td>
					<td><input type="text" maxlength="220" style="width:98%"/></td>
					<td><input type="text" style="width:98%"/></td>
					<td><input type="text" maxlength="20"style="width:98%"/></td>
					<td><a href="javascript:AddEntryCommon('tbl_specified_events');"><img src="<%=ResolveUrl("~/") %>Images/add.png" style="border:0;" /></a></td>
				</tr>
			</table>
		</div>
			  <%--  </td>
			</tr>
			<tr>
				<td style="border-top:1px solid; width:100%; height:5px;"></td>
			</tr>
			<tr>
				<td>Other Specified Events:</td>
			</tr>
		</table>--%>

		<div id="div_personal_info" style="padding:11px;">
		</div>
	</div>
	</div>
	
	<!-- BUTTONS FOR SAVING CREDIT INFO -->
	<div>
		<!-- SAVE CI INFO -->
		<%  if (oUsr.HasPositionOf("fnm") != IS_NOT_FOUND && acct_dtls.curr_doc_DocStatus == AppHelper.GetUserPositionId("fnm") && oUsr.HasRegionOf("fnm", oDocumnt.Region) == true) { %>
			<div class="div_blinker1">
			<center>
				<input type="button" value="Save Edited Info" onclick="javascript:SaveCIInfo_WithCredLT('true');" />
			</center>
			</div>
		<%  } %>

		<% if ( oUsr.HasPositionOf("cnc") != IS_NOT_FOUND && acct_dtls.curr_doc_DocStatus == AppHelper.GetUserPositionId("cnc") && oUsr.HasRegionOf("cnc", oDocumnt.Region) == true ) { %>
			<div class="div_blinker1">
			<center>
				<input type="button" value="Save Credit Inves. Info" onclick="javascript:SaveCIInfo('true');" />
			</center>
			</div>
		<% } %>

		<!-- SECOND PART -->
		<% if ( acct_dtls.curr_doc_DocStatus == "1000" && acct_dtls.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("cnc") && oUsr.HasPositionOf("cnc") != IS_NOT_FOUND && oUsr.HasRegionOf("cnc", oDocumnt.Region) == true ) { %>
			<div class="div_blinker1">
			<center>
				<input type="button" value="Save Credit Inves. Info" onclick="javascript:SaveCIInfo('true');" />
			</center>
			</div>
		<% } %>

		<% if ( acct_dtls.curr_doc_DocStatus == "1000" && acct_dtls.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("fnm") && oUsr.HasPositionOf("fnm") != IS_NOT_FOUND && oUsr.HasRegionOf("fnm", oDocumnt.Region) == true ) { %>
			<div class="div_blinker1">
			<center>
				<input type="button" value="Save Edited Info" onclick="javascript:SaveCIInfo_WithCredLT('true');" />
			</center>
			</div>
		<% } %>
		<!-- SAVE CI INFO -->
	</div>
	</div>
<hr />

<% 
	/*
	 buttons for approvers are separated in another page
	 */
	page_param.customer_accountdetails p_param = new page_param.customer_accountdetails();

	p_param.oUsr = oUsr;
	p_param.oDocumnt = oDocumnt;

	p_param.curr_doc_DocStatus = acct_dtls.curr_doc_DocStatus;
	p_param.curr_doc_DocChangesStatus = acct_dtls.curr_doc_DocChangesStatus;

	p_param.DocCCaNum = DocCCaNum;
	
	// 
	Html.RenderPartial("_accountdetails_p", p_param);
	
%>

</asp:Content>
