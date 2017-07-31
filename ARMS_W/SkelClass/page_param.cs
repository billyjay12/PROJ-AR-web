using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARMS_W.Class;

namespace ARMS_W.SkelClass
{
	public class page_param
	{

		public class customer_accountdetails 
		{
			public string curr_doc_DocStatus { get; set; }
			public string curr_doc_DocChangesStatus { get; set; }

			public string DocCCaNum { get; set; }

			public _User oUsr { get; set; }
			public _Document oDocumnt { get; set; }

		}

		public class customer_savecustomer
		{
			public string acct_category_value { get; set; }
			public string acct_category_prem { get; set; }
			public string acct_business_class { get; set; }
			public string acct_type_of_account { get; set; }
			public string acct_classification { get; set; }
			public string acct_type { get; set; }
			public string acct_key_account { get; set; }
			public string acct_code { get; set; }
			public string acct_class { get; set; }
			public string acct_name { get; set; }
			public string proposed_new_acct_name { get; set; }
			public string acct_phone_no { get; set; }
			public string acct_phone_no_2 { get; set; }
			public string acct_cellphone { get; set; }
			public string acct_acct_officer { get; set; }
			public string proposed_new_acct_officer { get; set; }
			public string acct_fax_no { get; set; }
			public string acct_territory { get; set; }
			public string proposed_new_acct_territory { get; set; }
			public string acct_email { get; set; }
			public string acct_office_hours { get; set; }
			public string acct_area { get; set; }
			public string proposed_acct_new_area { get; set; }
			public string acct_store_hours { get; set; }
			public string acct_region { get; set; }
			public string proposed_new_acct_region { get; set; }
			public string acct_years_in_business { get; set; }
			public string acct_years_with_matimco { get; set; }
			public string acct_tax_id { get; set; }
			public string acct_vat_no { get; set; }
			public string acct_reg_name { get; set; }
			public string proposed_new_acct_reg_name { get; set; }
			public string acct_business_add { get; set; }
			public string proposed_new_acct_business_add { get; set; }
			public string acct_delivery_add { get; set; }
			public string proposed_new_acct_delivery_add { get; set; }
			public string acct_business_type { get; set; }
			public string sole_owner_name { get; set; }
			public string sole_nationality { get; set; }
			public string sole_gen_manager { get; set; }
			public string sole_fin_manager { get; set; }
			public string sole_others { get; set; }
			public List<string> list_of_partner { get; set; }
			public string partner_gen_manager { get; set; }
			public string partner_fin_manager { get; set; }
			public string partner_others { get; set; }
			public string corp_date_inc { get; set; }
			public string corp_auth_cap_stock { get; set; }
			public string corp_subc_cap_stock { get; set; }
			public string corp_paidin_cap_stock { get; set; }
			public List<string> list_major_stockholder { get; set; }
			public string corp_ceo { get; set; }
			public string corp_vp_fin { get; set; }
			public string corp_gen_man { get; set; }
			public string acct_num_employees { get; set; }
			public List<string> list_of_employee_no { get; set; }
			public string acct_article_of_inc { get; set; }
			public string acct_article_of_inc_forupload { get; set; }
			public string acct_financial_statement { get; set; }
			public string acct_financial_statement_forupload { get; set; }
			public string acct_itr { get; set; }
			public string acct_itr_forupload { get; set; }
			public string acct_bir_reg { get; set; }
			public string acct_bir_reg_forupload { get; set; }
			public string acct_business_permit { get; set; }
			public string acct_business_permit_forupload { get; set; }
			public string acct_attch_other { get; set; }
			public string acct_attch_other_forupload { get; set; }

			/* Code added by Billy Jay (04/23/2015) */
			public string acct_ini_po_details { get; set; }
			public string acct_prop_credit_term_architectural_brand { get; set; }
			public string proposed_new_acct_prop_credit_term_architectural_brand { get; set; }
			public string acct_prop_credit_term_ecofor_lumber { get; set; }
			public string proposed_new_acct_prop_credit_term_ecofor_lumber { get; set; }
			public string acct_prop_credit_term_ecofor_plywood { get; set; }
			public string proposed_new_acct_prop_credit_term_ecofor_plywood { get; set; }

			public string acct_prop_credit_term_remarks_architectural_brand { get; set; }
			public string proposed_new_acct_credit_term_remarks_architectural_brand { get; set; }
			public string acct_prop_credit_term_remarks_ecofor_lumber { get; set; }
			public string proposed_new_acct_credit_term_remarks_ecofor_lumber { get; set; }
			public string acct_prop_credit_term_remarks_ecofor_plywood { get; set; }
			public string proposed_new_acct_credit_term_remarks_ecofor_plywood { get; set; }

			public string acct_prop_order_limit_ab { get; set; }
			public string proposed_new_acct_prop_order_limit_ab { get; set; }
			public string acct_prop_order_limit_tr { get; set; }
			public string proposed_new_acct_prop_order_limit_tr { get; set; }

			public string acct_prop_order_limit_remarks_ab { get; set; }
			public string proposed_new_acct_prop_order_limit_remarks_ab { get; set; }
			public string acct_prop_order_limit_remarks_tr { get; set; }
			public string proposed_new_acct_prop_order_limit_remarks_tr { get; set; }

			/* End Code added by Billy Jay (04/23/2015) */

			public string acct_prop_credit_term { get; set; }
			public string proposed_new_acct_prop_credit_term { get; set; }
			public string acct_prop_credit_limit { get; set; }
			public string proposed_new_acct_prop_credit_limit { get; set; }
			public string acct_prop_credit_term_remarks { get; set; }
			public string proposed_new_acct_prop_credit_term_remarks { get; set; }
			public string acct_prop_credit_limit_remarks { get; set; }
			public string proposed_new_acct_prop_credit_limit_remarks { get; set; }
			public string acct_mw_price_code { get; set; }
			public string proposed_new_acct_mw_price_code { get; set; }
			public string acct_mw_price_commision_disc { get; set; }
			public string proposed_new_acct_mw_price_commision_disc { get; set; }
			public string acct_mw_price_desc { get; set; }
			public string proposed_new_acct_mw_price_desc { get; set; }
			public string acct_mw_price_remarks { get; set; }
			public string proposed_new_acct_mw_price_remarks { get; set; }
			public string acct_ww_price_code { get; set; }
			public string proposed_new_acct_ww_price_code { get; set; }
			public string acct_ww_price_desc { get; set; }
			public string proposed_new_acct_ww_price_desc { get; set; }
			public string acct_ww_price_commision_disc { get; set; }
			public string proposed_new_acct_ww_price_commision_disc { get; set; }
			public string acct_ww_price_remarks { get; set; }
			public string proposed_new_acct_ww_price_remarks { get; set; }
			public string acct_pwf_price_code { get; set; }
			public string proposed_new_acct_pwf_price_code { get; set; }
			public string acct_pwf_price_desc { get; set; }
			public string proposed_new_acct_pwf_price_desc { get; set; }
			public string acct_pwf_price_commision_disc { get; set; }
			public string proposed_new_acct_pwf_price_commision_disc { get; set; }
			public string acct_pwf_price_remarks { get; set; }
			public string proposed_new_acct_pwf_price_remarks { get; set; }
			public string acct_pwr_price_code { get; set; }
			public string proposed_new_acct_pwr_price_code { get; set; }
			public string acct_pwr_price_desc { get; set; }
			public string proposed_new_acct_pwr_price_desc { get; set; }
			public string acct_pwr_price_commision_disc { get; set; }
			public string proposed_new_acct_pwr_price_commision_disc { get; set; }
			public string acct_pwr_price_remarks { get; set; }
			public string proposed_new_acct_pwr_price_remarks { get; set; }
			public string acct_gw_price_code { get; set; }
			public string proposed_new_acct_gw_price_code { get; set; }
			public string acct_gw_price_desc { get; set; }
			public string proposed_new_acct_gw_price_desc { get; set; }
			public string acct_gw_price_commision_disc { get; set; }
			public string proposed_new_acct_gw_price_commision_disc { get; set; }
			public string acct_gw_price_remarks { get; set; }
			public string proposed_new_acct_gw_price_remarks { get; set; }
			public string acct_tw_price_code { get; set; }
			public string proposed_new_acct_tw_price_code { get; set; }
			public string acct_tw_price_desc { get; set; }
			public string proposed_new_acct_tw_price_desc { get; set; }
			public string acct_tw_price_commision_disc { get; set; }
			public string proposed_new_acct_tw_price_commision_disc { get; set; }
			public string acct_tw_price_remarks { get; set; }
			public string proposed_new_acct_tw_price_remarks { get; set; }

			public string acct_mz_price_code { get; set; }
			public string proposed_new_acct_mz_price_code { get; set; }
			public string acct_mz_price_desc { get; set; }
			public string proposed_new_acct_mz_price_desc { get; set; }
			public string acct_mz_price_commision_disc { get; set; }
			public string proposed_new_acct_mz_price_commision_disc { get; set; }
			public string acct_mz_price_remarks { get; set; }
			public string proposed_new_acct_mz_price_remarks { get; set; }

			public string acct_nw_price_code { get; set; }
			public string proposed_new_acct_nw_price_code { get; set; }
			public string acct_nw_price_desc { get; set; }
			public string proposed_new_acct_nw_price_desc { get; set; }
			public string acct_nw_price_commision_disc { get; set; }
			public string proposed_new_acct_nw_price_commision_disc { get; set; }
			public string acct_nw_price_remarks { get; set; }
			public string proposed_new_acct_nw_price_remarks { get; set; }

			public string acct_ec_price_code { get; set; }
			public string proposed_new_acct_ec_price_code { get; set; }
			public string acct_ec_price_desc { get; set; }
			public string proposed_new_acct_ec_price_desc { get; set; }
			public string acct_ec_price_commision_disc { get; set; }
			public string proposed_new_acct_ec_price_commision_disc { get; set; }
			public string acct_ec_price_remarks { get; set; }
			public string proposed_new_acct_ec_price_remarks { get; set; }

			public string acct_ecu_price_code { get; set; }
			public string proposed_new_acct_ecu_price_code { get; set; }
			public string acct_ecu_price_desc { get; set; }
			public string proposed_new_acct_ecu_price_desc { get; set; }
			public string acct_ecu_price_commision_disc { get; set; }
			public string proposed_new_acct_ecu_price_commision_disc { get; set; }
			public string acct_ecu_price_remarks { get; set; }
			public string proposed_new_acct_ecu_price_remarks { get; set; }

			public string acct_socio_eco_class { get; set; }
			public string acct_num_outlets { get; set; }
			public List<string> list_of_outlets { get; set; }
			public List<string> list_of_events { get; set; }
			public List<string> list_of_major_customer { get; set; }
			public string acct_major_prod_line { get; set; }
			public string acct_other_prod_line { get; set; }
			public string acct_supplier_on_plywood { get; set; }
			public string acct_supplier_on_steel { get; set; }
			public string acct_supplier_on_cement { get; set; }
			public string acct_supplier_on_con_hollowblock { get; set; }
			public string acct_supplier_on_others { get; set; }
			public string acct_major_vol_business { get; set; }
			public string acct_monthly_wood_vol { get; set; }
			public string acct_discount_enjoyed { get; set; }
			public List<string> list_of_other_wood_suppliers { get; set; }
			public string acct_ccanum { get; set; }
			public string other_changes { get; set; }

			public string apprver_remarks { get; set; }

			public List<string> list_of_delivery_address { get; set; }

			#region WORKAROUND
			public void StripNull()
			{
				if (acct_category_value == null) acct_category_value = "";
				if (acct_category_prem == null) acct_category_prem = "";
				if (acct_business_class == null) acct_business_class = "";
				if (acct_classification == null) acct_classification = "";
				if (acct_type == null) acct_type = "";
				if (acct_key_account == null) acct_key_account = "";
				if (acct_code == null) acct_code = "";
				if (acct_class == null) acct_class = "";
				if (acct_name == null) acct_name = "";
				if (proposed_new_acct_name == null) proposed_new_acct_name = "";
				if (acct_phone_no == null) acct_phone_no = "";
				if (acct_phone_no_2 == null) acct_phone_no_2 = "";
				if (acct_cellphone == null) acct_cellphone = "";
				if (acct_acct_officer == null) acct_acct_officer = "";
				if (proposed_new_acct_officer == null) proposed_new_acct_officer = "";
				if (acct_fax_no == null) acct_fax_no = "";
				if (acct_territory == null) acct_territory = "";
				if (proposed_new_acct_territory == null) proposed_new_acct_territory = "";
				if (acct_email == null) acct_email = "";
				if (acct_office_hours == null) acct_office_hours = "";
				if (acct_area == null) acct_area = "";
				if (proposed_acct_new_area == null) proposed_acct_new_area = "";
				if (acct_store_hours == null) acct_store_hours = "";
				if (acct_region == null) acct_region = "";
				if (proposed_new_acct_region == null) proposed_new_acct_region = "";
				if (acct_years_in_business == null) acct_years_in_business = "";
				if (acct_years_with_matimco == null) acct_years_with_matimco = "";
				if (acct_tax_id == null) acct_tax_id = "";
				if (acct_vat_no == null) acct_vat_no = "";
				if (acct_reg_name == null) acct_reg_name = "";
				if (proposed_new_acct_reg_name == null) proposed_new_acct_reg_name = "";
				if (acct_business_add == null) acct_business_add = "";
				if (proposed_new_acct_business_add == null) proposed_new_acct_business_add = "";
				if (acct_delivery_add == null) acct_delivery_add = "";
				if (proposed_new_acct_delivery_add == null) proposed_new_acct_delivery_add = "";
				if (acct_business_type == null) acct_business_type = "";
				if (sole_owner_name == null) sole_owner_name = "";
				if (sole_nationality == null) sole_nationality = "";
				if (sole_gen_manager == null) sole_gen_manager = "";
				if (sole_fin_manager == null) sole_fin_manager = "";
				if (sole_others == null) sole_others = "";
				if (partner_gen_manager == null) partner_gen_manager = "";
				if (partner_fin_manager == null) partner_fin_manager = "";
				if (partner_others == null) partner_others = "";
				if (corp_date_inc == null) corp_date_inc = "";
				if (corp_auth_cap_stock == null) corp_auth_cap_stock = "";
				if (corp_subc_cap_stock == null) corp_subc_cap_stock = "";
				if (corp_paidin_cap_stock == null) corp_paidin_cap_stock = "";
				if (corp_ceo == null) corp_ceo = "";
				if (corp_vp_fin == null) corp_vp_fin = "";
				if (corp_gen_man == null) corp_gen_man = "";
				if (acct_num_employees == null) acct_num_employees = "";
				if (acct_article_of_inc == null) acct_article_of_inc = "";
				if (acct_article_of_inc_forupload == null) acct_article_of_inc_forupload = "";
				if (acct_financial_statement == null) acct_financial_statement = "";
				if (acct_financial_statement_forupload == null) acct_financial_statement_forupload = "";
				if (acct_itr == null) acct_itr = "";
				if (acct_itr_forupload == null) acct_itr_forupload = "";
				if (acct_bir_reg == null) acct_bir_reg = "";
				if (acct_bir_reg_forupload == null) acct_bir_reg_forupload = "";
				if (acct_business_permit == null) acct_business_permit = "";
				if (acct_business_permit_forupload == null) acct_business_permit_forupload = "";
				if (acct_attch_other == null) acct_attch_other = "";
				if (acct_attch_other_forupload == null) acct_attch_other_forupload = "";

				/* Code added by Billy Jay (04/23/2015) */

				if (acct_ini_po_details == null) acct_ini_po_details = "";

				if (acct_prop_credit_term_architectural_brand == null) acct_prop_credit_term_architectural_brand = "";
				if (acct_prop_credit_term_ecofor_lumber == null) acct_prop_credit_term_ecofor_lumber = "";
				if (acct_prop_credit_term_ecofor_plywood == null) acct_prop_credit_term_ecofor_plywood = "";

				if (proposed_new_acct_prop_credit_term_architectural_brand == null) proposed_new_acct_prop_credit_term_architectural_brand = "";
				if (proposed_new_acct_prop_credit_term_ecofor_lumber == null) proposed_new_acct_prop_credit_term_ecofor_lumber = "";
				if (proposed_new_acct_prop_credit_term_ecofor_plywood == null) proposed_new_acct_prop_credit_term_ecofor_plywood = "";


				if (acct_prop_credit_term_remarks_architectural_brand == null) acct_prop_credit_term_remarks_architectural_brand = "";
				if (acct_prop_credit_term_remarks_ecofor_lumber == null) acct_prop_credit_term_remarks_ecofor_lumber = "";
				if (acct_prop_credit_term_remarks_ecofor_plywood == null) acct_prop_credit_term_remarks_ecofor_plywood = "";

				if (proposed_new_acct_credit_term_remarks_architectural_brand == null) proposed_new_acct_credit_term_remarks_architectural_brand = "";
				if (proposed_new_acct_credit_term_remarks_ecofor_lumber == null) proposed_new_acct_credit_term_remarks_ecofor_lumber = "";
				if (proposed_new_acct_credit_term_remarks_ecofor_plywood == null) proposed_new_acct_credit_term_remarks_ecofor_plywood = "";

				if (acct_prop_order_limit_ab == null) acct_prop_order_limit_ab = "";
				if (acct_prop_order_limit_tr == null) acct_prop_order_limit_tr = "";

				if (proposed_new_acct_prop_order_limit_ab == null) proposed_new_acct_prop_order_limit_ab = "";
				if (proposed_new_acct_prop_order_limit_tr == null) proposed_new_acct_prop_order_limit_tr = "";

				if (acct_prop_order_limit_remarks_ab == null) acct_prop_order_limit_remarks_ab = "";
				if (acct_prop_order_limit_remarks_tr == null) acct_prop_order_limit_remarks_tr = "";
				if (proposed_new_acct_prop_order_limit_remarks_tr == null) proposed_new_acct_prop_order_limit_remarks_tr = "";
				if (proposed_new_acct_prop_order_limit_remarks_ab == null) proposed_new_acct_prop_order_limit_remarks_ab = "";


				/* End Code added by Billy Jay (04/23/2015) */

				if (acct_prop_credit_term == null) acct_prop_credit_term = "";
				if (proposed_new_acct_prop_credit_term == null) proposed_new_acct_prop_credit_term = "";
				if (acct_prop_credit_limit == null) acct_prop_credit_limit = "";
				if (proposed_new_acct_prop_credit_limit == null) proposed_new_acct_prop_credit_limit = "";
				if (acct_prop_credit_term_remarks == null) acct_prop_credit_term_remarks = "";
				if (proposed_new_acct_prop_credit_term_remarks == null) proposed_new_acct_prop_credit_term_remarks = "";
				if (acct_prop_credit_limit_remarks == null) acct_prop_credit_limit_remarks = "";
				if (proposed_new_acct_prop_credit_limit_remarks == null) proposed_new_acct_prop_credit_limit_remarks = "";
				if (acct_mw_price_code == null) acct_mw_price_code = "";
				if (proposed_new_acct_mw_price_code == null) proposed_new_acct_mw_price_code = "";
				if (acct_mw_price_commision_disc == null) acct_mw_price_commision_disc = "";
				if (proposed_new_acct_mw_price_commision_disc == null) proposed_new_acct_mw_price_commision_disc = "";
				if (acct_mw_price_desc == null) acct_mw_price_desc = "";
				if (proposed_new_acct_mw_price_desc == null) proposed_new_acct_mw_price_desc = "";
				if (acct_mw_price_remarks == null) acct_mw_price_remarks = "";
				if (proposed_new_acct_mw_price_remarks == null) proposed_new_acct_mw_price_remarks = "";
				if (acct_ww_price_code == null) acct_ww_price_code = "";
				if (proposed_new_acct_ww_price_code == null) proposed_new_acct_ww_price_code = "";
				if (acct_ww_price_desc == null) acct_ww_price_desc = "";
				if (proposed_new_acct_ww_price_desc == null) proposed_new_acct_ww_price_desc = "";
				if (acct_ww_price_commision_disc == null) acct_ww_price_commision_disc = "";
				if (proposed_new_acct_ww_price_commision_disc == null) proposed_new_acct_ww_price_commision_disc = "";
				if (acct_ww_price_remarks == null) acct_ww_price_remarks = "";
				if (proposed_new_acct_ww_price_remarks == null) proposed_new_acct_ww_price_remarks = "";
				if (acct_pwf_price_code == null) acct_pwf_price_code = "";
				if (proposed_new_acct_pwf_price_code == null) proposed_new_acct_pwf_price_code = "";
				if (acct_pwf_price_desc == null) acct_pwf_price_desc = "";
				if (proposed_new_acct_pwf_price_desc == null) proposed_new_acct_pwf_price_desc = "";
				if (acct_pwf_price_commision_disc == null) acct_pwf_price_commision_disc = "";
				if (proposed_new_acct_pwf_price_commision_disc == null) proposed_new_acct_pwf_price_commision_disc = "";
				if (acct_pwf_price_remarks == null) acct_pwf_price_remarks = "";
				if (proposed_new_acct_pwf_price_remarks == null) proposed_new_acct_pwf_price_remarks = "";
				if (acct_pwr_price_code == null) acct_pwr_price_code = "";
				if (proposed_new_acct_pwr_price_code == null) proposed_new_acct_pwr_price_code = "";
				if (acct_pwr_price_desc == null) acct_pwr_price_desc = "";
				if (proposed_new_acct_pwr_price_desc == null) proposed_new_acct_pwr_price_desc = "";
				if (acct_pwr_price_commision_disc == null) acct_pwr_price_commision_disc = "";
				if (proposed_new_acct_pwr_price_commision_disc == null) proposed_new_acct_pwr_price_commision_disc = "";
				if (acct_pwr_price_remarks == null) acct_pwr_price_remarks = "";
				if (proposed_new_acct_pwr_price_remarks == null) proposed_new_acct_pwr_price_remarks = "";
				if (acct_gw_price_code == null) acct_gw_price_code = "";
				if (proposed_new_acct_gw_price_code == null) proposed_new_acct_gw_price_code = "";
				if (acct_gw_price_desc == null) acct_gw_price_desc = "";
				if (proposed_new_acct_gw_price_desc == null) proposed_new_acct_gw_price_desc = "";
				if (acct_gw_price_commision_disc == null) acct_gw_price_commision_disc = "";
				if (proposed_new_acct_gw_price_commision_disc == null) proposed_new_acct_gw_price_commision_disc = "";
				if (acct_gw_price_remarks == null) acct_gw_price_remarks = "";
				if (proposed_new_acct_gw_price_remarks == null) proposed_new_acct_gw_price_remarks = "";
				if (acct_tw_price_code == null) acct_tw_price_code = "";
				if (proposed_new_acct_tw_price_code == null) proposed_new_acct_tw_price_code = "";
				if (acct_tw_price_desc == null) acct_tw_price_desc = "";
				if (proposed_new_acct_tw_price_desc == null) proposed_new_acct_tw_price_desc = "";
				if (acct_tw_price_commision_disc == null) acct_tw_price_commision_disc = "";
				if (proposed_new_acct_tw_price_commision_disc == null) proposed_new_acct_tw_price_commision_disc = "";
				if (acct_tw_price_remarks == null) acct_tw_price_remarks = "";
				if (proposed_new_acct_tw_price_remarks == null) proposed_new_acct_tw_price_remarks = "";

				if (acct_mz_price_code == null) acct_mz_price_code = "";
				if (proposed_new_acct_mz_price_code == null) proposed_new_acct_mz_price_code = "";
				if (acct_mz_price_desc == null) acct_mz_price_desc = "";
				if (proposed_new_acct_mz_price_desc == null) proposed_new_acct_mz_price_desc = "";
				if (acct_mz_price_commision_disc == null) acct_mz_price_commision_disc = "";
				if (proposed_new_acct_mz_price_commision_disc == null) proposed_new_acct_mz_price_commision_disc = "";
				if (acct_mz_price_remarks == null) acct_mz_price_remarks = "";
				if (proposed_new_acct_mz_price_remarks == null) proposed_new_acct_mz_price_remarks = "";

				if (acct_nw_price_code == null) acct_nw_price_code = "";
				if (proposed_new_acct_nw_price_code == null) proposed_new_acct_nw_price_code = "";
				if (acct_nw_price_desc == null) acct_nw_price_desc = "";
				if (proposed_new_acct_nw_price_desc == null) proposed_new_acct_nw_price_desc = "";
				if (acct_nw_price_commision_disc == null) acct_nw_price_commision_disc = "";
				if (proposed_new_acct_nw_price_commision_disc == null) proposed_new_acct_nw_price_commision_disc = "";
				if (acct_nw_price_remarks == null) acct_nw_price_remarks = "";
				if (proposed_new_acct_nw_price_remarks == null) proposed_new_acct_nw_price_remarks = "";

				if (acct_ec_price_code == null) acct_ec_price_code = "";
				if (proposed_new_acct_ec_price_code == null) proposed_new_acct_ec_price_code = "";
				if (acct_ec_price_desc == null) acct_ec_price_desc = "";
				if (proposed_new_acct_ec_price_desc == null) proposed_new_acct_ec_price_desc = "";
				if (acct_ec_price_commision_disc == null) acct_ec_price_commision_disc = "";
				if (proposed_new_acct_ec_price_commision_disc == null) proposed_new_acct_ec_price_commision_disc = "";
				if (acct_ec_price_remarks == null) acct_ec_price_remarks = "";
				if (proposed_new_acct_ec_price_remarks == null) proposed_new_acct_ec_price_remarks = "";

				if (acct_ecu_price_code == null) acct_ecu_price_code = "";
				if (proposed_new_acct_ecu_price_code == null) proposed_new_acct_ecu_price_code = "";
				if (acct_ecu_price_desc == null) acct_ecu_price_desc = "";
				if (proposed_new_acct_ecu_price_desc == null) proposed_new_acct_ecu_price_desc = "";
				if (acct_ecu_price_commision_disc == null) acct_ecu_price_commision_disc = "";
				if (proposed_new_acct_ecu_price_commision_disc == null) proposed_new_acct_ecu_price_commision_disc = "";
				if (acct_ecu_price_remarks == null) acct_ecu_price_remarks = "";
				if (proposed_new_acct_ecu_price_remarks == null) proposed_new_acct_ecu_price_remarks = "";

				if (acct_socio_eco_class == null) acct_socio_eco_class = "";
				if (acct_num_outlets == null) acct_num_outlets = "";
				if (acct_major_prod_line == null) acct_major_prod_line = "";
				if (acct_other_prod_line == null) acct_other_prod_line = "";
				if (acct_supplier_on_plywood == null) acct_supplier_on_plywood = "";
				if (acct_supplier_on_steel == null) acct_supplier_on_steel = "";
				if (acct_supplier_on_cement == null) acct_supplier_on_cement = "";
				if (acct_supplier_on_con_hollowblock == null) acct_supplier_on_con_hollowblock = "";
				if (acct_supplier_on_others == null) acct_supplier_on_others = "";
				if (acct_major_vol_business == null) acct_major_vol_business = "";
				if (acct_monthly_wood_vol == null) acct_monthly_wood_vol = "";
				if (acct_discount_enjoyed == null) acct_discount_enjoyed = "";
				if (acct_ccanum == null) acct_ccanum = "";
				if (other_changes == null) other_changes = "";
				if (apprver_remarks == null) apprver_remarks = "";

				if (list_of_partner == null) list_of_partner = new List<string>();
				if (list_major_stockholder == null) list_major_stockholder = new List<string>();
				if (list_of_employee_no == null) list_of_employee_no = new List<string>();
				if (list_of_outlets == null) list_of_outlets = new List<string>();
				if (list_of_events == null) list_of_events = new List<string>();
				if (list_of_major_customer == null) list_of_major_customer = new List<string>();
				if (list_of_other_wood_suppliers == null) list_of_other_wood_suppliers = new List<string>();
			}
			#endregion
		}


		public class customer_savecreditinvestigationinfo 
		{ 
			public string acct_code { get; set; }
			public List<string> list_of_bank { get; set; }
			public List<string> list_of_land { get; set; }
			public List<string> list_of_building { get; set; }
			public List<string> list_of_vehicle { get; set; }
			public string acct_other_assets { get; set; }
			public List<string> list_of_assets { get; set; }
			public string acct_ccanum { get; set; }
			public string acct_cibi_remarks { get; set; }
			public string acct_supplyinfo_remarks { get; set; }

			#region WORKAROUND
			public void StripNull() 
			{ 
				if( acct_code == null) acct_code = "";
				if (list_of_bank == null) list_of_bank = new List<string>();
				if (list_of_land == null) list_of_land = new List<string>();
				if (list_of_building == null) list_of_building = new List<string>();
				if (list_of_vehicle == null) list_of_vehicle = new List<string>();
				if (acct_other_assets == null) acct_other_assets = "";
				if (list_of_assets == null) list_of_assets = new List<string>();
				if (acct_ccanum == null) acct_ccanum = "";
				if (acct_cibi_remarks == null) acct_cibi_remarks = "";
				if (acct_supplyinfo_remarks == null) acct_supplyinfo_remarks = "";
			}
			#endregion
		}

		public class customer_saveeditedinfo
		{
			public string acct_code { get; set; }
			public List<string> list_of_bank { get; set; }
			public List<string> list_of_land { get; set; }
			public List<string> list_of_building { get; set; }
			public List<string> list_of_vehicle { get; set; }
			public string acct_other_assets { get; set; }
			public List<string> list_of_assets { get; set; }
			public string acct_ccanum { get; set; }
			public string acct_cibi_remarks { get; set; }
			public string acct_supplyinfo_remarks { get; set; }

			/* Code added by Billy Jay (04/23/2015) */
			public string acct_prop_credit_terms_architectural_brand { get; set; }
			public string acct_prop_credit_terms_ecofor_lumber { get; set; }
			public string acct_prop_credit_terms_ecofor_plywood { get; set; }

			public string acct_prob_order_limit_ab { get; set; }
			public string acct_prob_order_limit_tr { get; set; }
			/* End Code added by Billy Jay (04/23/2015) */

			public string acct_prop_credit_term { get; set; }
			public string acct_prop_credit_limit { get; set; }

			public string str_changes { get; set; }

			#region WORKAROUND
			public void StripNull()
			{
				if( acct_code == null) acct_code = "";
				if( list_of_bank == null) list_of_bank = new List<string>();
				if( list_of_land == null) list_of_land = new List<string>();
				if( list_of_building == null) list_of_building = new List<string>();
				if( list_of_vehicle == null) list_of_vehicle = new List<string>();
				if( acct_other_assets == null) acct_other_assets = "";
				if( list_of_assets == null) list_of_assets = new List<string>();
				if( acct_ccanum == null) acct_ccanum = "";
				if( acct_cibi_remarks == null) acct_cibi_remarks = "";
				if( acct_supplyinfo_remarks == null) acct_supplyinfo_remarks = "";
				if( acct_prop_credit_term == null) acct_prop_credit_term = "";
				if( acct_prop_credit_limit == null) acct_prop_credit_limit = "";
				
				if( acct_prop_credit_terms_architectural_brand == null) acct_prop_credit_terms_architectural_brand = "";
				if( acct_prop_credit_terms_ecofor_lumber == null) acct_prop_credit_terms_ecofor_lumber = "";
				if( acct_prop_credit_terms_ecofor_plywood == null) acct_prop_credit_terms_ecofor_plywood = "";

				if (acct_prob_order_limit_ab == null) acct_prob_order_limit_ab = "";
				if (acct_prob_order_limit_tr == null) acct_prob_order_limit_tr = "";
			}
			#endregion
		}

		public class customer_addcustomer 
		{ 
			public string acct_classification { get; set; }
			public string acct_category_value { get; set; }
			public string acct_category_prem { get; set; }
			public string acct_business_class { get; set; }
			public string acct_type_of_account { get; set; }
			public string acct_type { get; set; }
			public string acct_key_account { get; set; }
			public string acct_code { get; set; }
			public string acct_class { get; set; }
			public string acct_name { get; set; }
			public string acct_phone_no { get; set; }
			public string acct_phone_no_2 { get; set; }
			public string acct_cellphone { get; set; }
			public string acct_acct_officer { get; set; }
			public string acct_fax_no { get; set; }
			public string acct_territory { get; set; }
			public string acct_email { get; set; }
			public string acct_office_hours { get; set; }
			public string acct_area { get; set; }
			public string acct_store_hours { get; set; }
			public string acct_region { get; set; }
			public string acct_years_in_business { get; set; }
			public string acct_years_with_matimco { get; set; }
			public string acct_tax_id { get; set; }
			public string acct_vat_no { get; set; }
			public string acct_reg_name { get; set; }
			public string acct_business_add { get; set; }
			public string acct_delivery_add { get; set; }
			public string acct_business_type { get; set; }
			public string sole_owner_name { get; set; }
			public string sole_nationality { get; set; }
			public string sole_gen_manager { get; set; }
			public string sole_fin_manager { get; set; }
			public string sole_others { get; set; }
			public List<string> list_of_partner { get; set; }
			public string partner_gen_manager { get; set; }
			public string partner_fin_manager { get; set; }
			public string partner_others { get; set; }
			public string corp_date_inc { get; set; }
			public string corp_auth_cap_stock { get; set; }
			public string corp_subc_cap_stock { get; set; }
			public string corp_paidin_cap_stock { get; set; }
			public List<string> list_major_stockholder { get; set; }
			public string corp_ceo { get; set; }
			public string corp_vp_fin { get; set; }
			public string corp_gen_man { get; set; }
			public string acct_num_employees { get; set; }
			public List<string> list_of_employee_no { get; set; }
			public string acct_article_of_inc { get; set; }
			public string acct_financial_statement { get; set; }
			public string acct_itr { get; set; }
			public string acct_bir_reg { get; set; }
			public string acct_attch_other { get; set; }
			public string acct_business_permit { get; set; }

			/* Code added by Billy Jay (04/23/2015) */
			public string acct_ini_po_details { get; set; }

			public string acct_prop_credit_term_architectural_brand{ get; set; }
			public string acct_prop_credit_term_eco_lumber{ get; set; }
			public string acct_prop_credit_term_eco_plywood{ get; set; }
			public string acct_prop_credit_term_architectural_brand_remarks{ get; set; }
			public string acct_prop_credit_term_eco_lumber_remarks{ get; set; }
			public string acct_prop_credit_term_eco_plywood_remarks { get; set; }


			public string acct_prop_order_limit_ab { get; set; }
			public string acct_prop_order_limit_tr { get; set; }

			public string acct_prop_order_limit_remarks_ab { get; set; }
			public string acct_prop_order_limit_remarks_tr { get; set; } 

			/* End Code added by Billy Jay (04/23/2015) */
			public string acct_prop_credit_term { get; set; } 
			public string acct_prop_credit_limit { get; set; } 
			public string acct_prop_credit_term_remarks { get; set; } 
			public string acct_prop_credit_limit_remarks { get; set; }

			public string acct_mw_price_code { get; set; } 
			public string acct_mw_price_desc { get; set; } 
			public string acct_mw_price_commision_disc { get; set; } 
			public string acct_mw_price_remarks { get; set; }

			public string acct_ww_price_code { get; set; } 
			public string acct_ww_price_desc { get; set; } 
			public string acct_ww_price_commision_disc { get; set; } 
			public string acct_ww_price_remarks { get; set; }

			public string acct_pwf_price_code { get; set; } 
			public string acct_pwf_price_desc { get; set; } 
			public string acct_pwf_price_commision_disc { get; set; } 
			public string acct_pwf_price_remarks { get; set; }

			public string acct_pwr_price_code { get; set; } 
			public string acct_pwr_price_desc { get; set; } 
			public string acct_pwr_price_commision_disc { get; set; } 
			public string acct_pwr_price_remarks { get; set; }

			public string acct_gw_price_code { get; set; } 
			public string acct_gw_price_desc { get; set; } 
			public string acct_gw_price_commision_disc { get; set; } 
			public string acct_gw_price_remarks { get; set; }

			public string acct_tw_price_code { get; set; } 
			public string acct_tw_price_desc { get; set; } 
			public string acct_tw_price_commision_disc { get; set; } 
			public string acct_tw_price_remarks { get; set; }

			public string acct_mz_price_code { get; set; }
			public string acct_mz_price_desc { get; set; }
			public string acct_mz_price_commision_disc { get; set; }
			public string acct_mz_price_remarks { get; set; }

			public string acct_nw_price_code { get; set; }
			public string acct_nw_price_desc { get; set; }
			public string acct_nw_price_commision_disc { get; set; }
			public string acct_nw_price_remarks { get; set; }

			public string acct_ec_price_code { get; set; }
			public string acct_ec_price_desc { get; set; }
			public string acct_ec_price_commision_disc { get; set; }
			public string acct_ec_price_remarks { get; set; }

			public string acct_ecu_price_code { get; set; }
			public string acct_ecu_price_desc { get; set; }
			public string acct_ecu_price_commision_disc { get; set; }
			public string acct_ecu_price_remarks { get; set; }

			public string acct_socio_eco_class { get; set; }
			public string acct_num_outlets { get; set; }
			public List<string> list_of_outlets { get; set; }
			public List<string> list_of_events { get; set; }
			public List<string> list_of_major_customer { get; set; }
			public string acct_major_prod_line { get; set; }
			public string acct_other_prod_line { get; set; }
			public string acct_supplier_on_plywood { get; set; }
			public string acct_supplier_on_steel { get; set; }
			public string acct_supplier_on_cement { get; set; }
			public string acct_supplier_on_con_hollowblock { get; set; }
			public string acct_supplier_on_others { get; set; }
			public string acct_major_vol_business { get; set; }
			public string acct_monthly_wood_vol { get; set; }
			public string acct_discount_enjoyed { get; set; }
			public List<string> list_of_other_wood_suppliers { get; set; }
			public string request_id { get; set; }

			#region WORKAROUND
			public void StripNull() 
			{ 
				if( acct_classification == null) acct_classification = "";
				if (acct_category_value == null) acct_category_value = "";
				if (acct_category_prem == null) acct_category_prem = "";
				if (acct_business_class == null) acct_business_class = "";
				if (acct_type == null) acct_type = "";
				if (acct_key_account == null) acct_key_account = "";
				if (acct_code == null) acct_code = "";
				if (acct_class == null) acct_class = "";
				if (acct_name == null) acct_name = "";
				if (acct_phone_no == null) acct_phone_no = "";
				if (acct_phone_no_2 == null) acct_phone_no_2 = "";
				if (acct_cellphone == null) acct_cellphone = "";
				if (acct_acct_officer == null) acct_acct_officer = "";
				if (acct_fax_no == null) acct_fax_no = "";
				if (acct_territory == null) acct_territory = "";
				if (acct_email == null) acct_email = "";
				if (acct_office_hours == null) acct_office_hours = "";
				if (acct_area == null) acct_area = "";
				if (acct_store_hours == null) acct_store_hours = "";
				if (acct_region == null) acct_region = "";
				if (acct_years_in_business == null) acct_years_in_business = "";
				if (acct_years_with_matimco == null) acct_years_with_matimco = "";
				if (acct_tax_id == null) acct_tax_id = "";
				if (acct_vat_no == null) acct_vat_no = "";
				if (acct_reg_name == null) acct_reg_name = "";
				if (acct_business_add == null) acct_business_add = "";
				if (acct_delivery_add == null) acct_delivery_add = "";
				if (acct_business_type == null) acct_business_type = "";
				if (sole_owner_name == null) sole_owner_name = "";
				if (sole_nationality == null) sole_nationality = "";
				if (sole_gen_manager == null) sole_gen_manager = "";
				if (sole_fin_manager == null) sole_fin_manager = "";
				if (sole_others == null) sole_others = "";
				if (list_of_partner == null) list_of_partner = new List<string>();
				if (partner_gen_manager == null) partner_gen_manager = "";
				if (partner_fin_manager == null) partner_fin_manager = "";
				if (partner_others == null) partner_others = "";
				if (corp_date_inc == null) corp_date_inc = "";
				if (corp_auth_cap_stock == null) corp_auth_cap_stock = "";
				if (corp_subc_cap_stock == null) corp_subc_cap_stock = "";
				if (corp_paidin_cap_stock == null) corp_paidin_cap_stock = "";
				if (list_major_stockholder == null) list_major_stockholder = new List<string>();
				if (corp_ceo == null) corp_ceo = "";
				if (corp_vp_fin == null) corp_vp_fin = "";
				if (corp_gen_man == null) corp_gen_man = "";
				if (acct_num_employees == null) acct_num_employees = "";
				if (list_of_employee_no == null) list_of_employee_no = new List<string>();
				if (acct_article_of_inc == null) acct_article_of_inc = "";
				if (acct_financial_statement == null) acct_financial_statement = "";
				if (acct_itr == null) acct_itr = "";
				if (acct_bir_reg == null) acct_bir_reg = "";
				if (acct_attch_other == null) acct_attch_other = "";
				if (acct_business_permit == null) acct_business_permit = "";

				/* Code added by Billy Jay (04/23/2015) */
				if (acct_ini_po_details == null) acct_ini_po_details = "";
				if (acct_prop_credit_term_architectural_brand == null) acct_prop_credit_term_architectural_brand = "";
				if (acct_prop_credit_term_eco_lumber == null) acct_prop_credit_term_eco_lumber = "";
				if (acct_prop_credit_term_eco_plywood == null) acct_prop_credit_term_eco_plywood = "";
				if (acct_prop_credit_term_architectural_brand_remarks == null) acct_prop_credit_term_architectural_brand_remarks = "";
				if (acct_prop_credit_term_eco_lumber_remarks == null) acct_prop_credit_term_eco_lumber_remarks = "";
				if (acct_prop_credit_term_eco_plywood_remarks == null) acct_prop_credit_term_eco_plywood_remarks = "";

				if (acct_prop_order_limit_ab == null) acct_prop_order_limit_ab = "";
				if (acct_prop_order_limit_tr == null) acct_prop_order_limit_tr = "";
				if (acct_prop_order_limit_remarks_ab == null) acct_prop_order_limit_remarks_ab = "";
				if (acct_prop_order_limit_remarks_tr == null) acct_prop_order_limit_remarks_tr = "";

				/* End Code added by Billy Jay (04/23/2015) */
				if (acct_prop_credit_term == null) acct_prop_credit_term = "";
				if (acct_prop_credit_limit == null) acct_prop_credit_limit = "";
				if (acct_prop_credit_term_remarks == null) acct_prop_credit_term_remarks = "";
				if (acct_prop_credit_limit_remarks == null) acct_prop_credit_limit_remarks = "";

				if (acct_mw_price_code == null) acct_mw_price_code = "";
				if (acct_mw_price_desc == null) acct_mw_price_desc = "";
				if (acct_mw_price_commision_disc == null) acct_mw_price_commision_disc = "";
				if (acct_mw_price_remarks == null) acct_mw_price_remarks = "";

				if (acct_ww_price_code == null) acct_ww_price_code = "";
				if (acct_ww_price_desc == null) acct_ww_price_desc = "";
				if (acct_ww_price_commision_disc == null) acct_ww_price_commision_disc = "";
				if (acct_ww_price_remarks == null) acct_ww_price_remarks = "";

				if (acct_pwf_price_code == null) acct_pwf_price_code = "";
				if (acct_pwf_price_desc == null) acct_pwf_price_desc = "";
				if (acct_pwf_price_commision_disc == null) acct_pwf_price_commision_disc = "";
				if (acct_pwf_price_remarks == null) acct_pwf_price_remarks = "";

				if (acct_pwr_price_code == null) acct_pwr_price_code = "";
				if (acct_pwr_price_desc == null) acct_pwr_price_desc = "";
				if (acct_pwr_price_commision_disc == null) acct_pwr_price_commision_disc = "";
				if (acct_pwr_price_remarks == null) acct_pwr_price_remarks = "";

				if (acct_gw_price_code == null) acct_gw_price_code = "";
				if (acct_gw_price_desc == null) acct_gw_price_desc = "";
				if (acct_gw_price_commision_disc == null) acct_gw_price_commision_disc = "";
				if (acct_gw_price_remarks == null) acct_gw_price_remarks = "";

				if (acct_tw_price_code == null) acct_tw_price_code = "";
				if (acct_tw_price_desc == null) acct_tw_price_desc = "";
				if (acct_tw_price_commision_disc == null) acct_tw_price_commision_disc = "";
				if (acct_tw_price_remarks == null) acct_tw_price_remarks = "";

				if (acct_mz_price_code == null) acct_mz_price_code = "";
				if (acct_mz_price_desc == null) acct_mz_price_desc = "";
				if (acct_mz_price_commision_disc == null) acct_mz_price_commision_disc = "";
				if (acct_mz_price_remarks == null) acct_mz_price_remarks = "";

				if (acct_nw_price_code == null) acct_nw_price_code = "";
				if (acct_nw_price_desc == null) acct_nw_price_desc = "";
				if (acct_nw_price_commision_disc == null) acct_nw_price_commision_disc = "";
				if (acct_nw_price_remarks == null) acct_nw_price_remarks = "";

				if (acct_ec_price_code == null) acct_ec_price_code = "";
				if (acct_ec_price_desc == null) acct_ec_price_desc = "";
				if (acct_ec_price_commision_disc == null) acct_ec_price_commision_disc = "";
				if (acct_ec_price_remarks == null) acct_ec_price_remarks = "";

				if (acct_ecu_price_code == null) acct_ecu_price_code = "";
				if (acct_ecu_price_desc == null) acct_ecu_price_desc = "";
				if (acct_ecu_price_commision_disc == null) acct_ecu_price_commision_disc = "";
				if (acct_ecu_price_remarks == null) acct_ecu_price_remarks = "";

				if (acct_socio_eco_class == null) acct_socio_eco_class = "";
				if (acct_num_outlets == null) acct_num_outlets = "";
				if (list_of_outlets == null) list_of_outlets = new List<string>();
				if (list_of_events == null) list_of_events = new List<string>();
				if (list_of_major_customer == null) list_of_major_customer = new List<string>();
				if (acct_major_prod_line == null) acct_major_prod_line = "";
				if (acct_other_prod_line == null) acct_other_prod_line = "";
				if (acct_supplier_on_plywood == null) acct_supplier_on_plywood = "";
				if (acct_supplier_on_steel == null) acct_supplier_on_steel = "";
				if (acct_supplier_on_cement == null) acct_supplier_on_cement = "";
				if (acct_supplier_on_con_hollowblock == null) acct_supplier_on_con_hollowblock = "";
				if (acct_supplier_on_others == null) acct_supplier_on_others = "";
				if (acct_major_vol_business == null) acct_major_vol_business = "";
				if (acct_monthly_wood_vol == null) acct_monthly_wood_vol = "";
				if (acct_discount_enjoyed == null) acct_discount_enjoyed = "";
				if (list_of_other_wood_suppliers == null) list_of_other_wood_suppliers = new List<string>();
				if (request_id == null) request_id = "";
			}
			#endregion
		}

		public class mmagreement_addnewmeetingsandagreements 
		{
			public string acct_code { get; set; }
			public string acct_name { get; set; }
			public string acct_officer { get; set; }
			public string meeting_type { get; set; }
			public DateTime meeting_date { get; set; }
			public string meeting_objective { get; set; }
			public List<string> list_of_attendees { get; set; }
			public List<string> list_of_actions { get; set; }
			public List<string> meeting_signed_minutes { get; set; }
			public string meeting_prepared_by { get; set; }
		}

		public class gtaccount_pareto
		{
			public List<gtacctlist> gtAccountList { get; set; }
			public string _acctCode { get; set; }
			public bool _Pareto { get; set; }

			public class gtacctlist
			{
				public string acct_ccanum { get; set; }
				public string acctCode { get; set; }
				public string whsInchargeName { get; set; }
				public string whsInchargeNum { get; set; }
				public string acctName { get; set; }
				public bool Pareto { get; set; }
				public string area { get; set; }
				public string firstNameSO { get; set; }
				public string lastNameSO { get; set; }
			}
		}

		

		public class CustOutletsDetails
		{
			public string ccaNum { get; set; }

			public string acctCode { get; set; }
			public string acctName { get; set; }
			public string acctAddress { get; set; }


			public long custOutletId { get; set; }
			public string custOutletName { get; set; }
			public string custOutletLocation { get; set; }
			public string custStoreSize { get; set; }
			public string custWhsSize { get; set; }
			public string status { get; set; }

			public string empId { get; set; }
			public string empName { get; set; }

			public DateTime? prevCountDate { get; set; }
			public DateTime? nextCountDate { get; set; }
		}


		public class SalesTarget
		{
			public int year { get; set; }
			public string month { get; set; }
			public string empid { get; set; }
			public string empfullname { get; set; }
			public decimal? salestarget { get; set; }
			public decimal? prevsalestarget { get; set; }
			public string remarks { get; set; }
		}
		
		public class SalesTargetMaintenance
		{
			public string Code { get; set; }
			public string Description { get; set; }
			public decimal Amount { get; set; }

			public List<codelist> list_code { get; set; }
			public List<codelist> list_code_deleted { get; set; }

			public class codelist
			{
				public string _code { get; set; }
				public string _desc { get; set; }
				public decimal _amount { get; set; }
			}
		}

		public class DayCycleMaintenance
		{
			public int DayCycleCount { get; set; }
			public int rangeDayCycle { get; set; }
			public int startDayOfTheMonth { get; set; }
		}

		public class inventoryCount
		{
			public string act_type { get; set; }
			public string appvr_remarks { get; set; }
			public bool newWhsIncharge { get; set; }

			public string inventoryCountId { get; set; }
			public string empId { get; set; }
			public string empFirstName { get; set; }
			public string empLastName { get; set; }
			public string acctCode { get; set; }
			public string acctName { get; set; }
			public string acctAddress { get; set; }
			public int custOutletsID { get; set; }
			public string whsInchargeID { get; set; }
			public DateTime? prevCountDate { get; set; }
			public DateTime? dateTimeStamp { get; set; }
			public DateTime? nextCountDate { get; set; }
			public int? countRange { get; set; }
			public string remarks { get; set; }
			public decimal? totalAmount { get; set; }
			public string pareto { get; set; }
			public string area { get; set; }
			public string territoryName { get; set; }
			public int documentstatusid { get; set; }
			public int doctypeid { get; set; }
			public string inventoryCountStatus { get; set; }

			public DateTime? actualCountDate { get; set; }

			public DateTime? actualCountEndValidDate { get; set; }

			public DateTime? StartCountDate { get; set; }

			public string forthemonth { get; set; }


			public decimal? totalVarianceAmt { get; set; }

			public decimal? totalForecastAmt0 { get; set; }

			public decimal? totalForecastAmt1 { get; set; }
			public decimal? totalForecastAmt2 { get; set; }
			public decimal? totalForecastAmt3 { get; set; }

			//public decimal? totalSuggestedForecastAmt { get; set; }


			public DateTime? CountDueDateOn { get; set; }

			public string inventoryCountlist_tablebuilder_freeze { get; set; }
			public string inventoryCountlist_tablebuilder { get; set; }


		   // public DateTime forthemonth { get; set; }

			public List<inventoryCountdetails> inventorycount_list { get; set; }

			public whsIncharge whs_details { get; set; }
			public custoutletDetails custoutlet_details { get; set; }


			public class whsIncharge
			{
				public string whsInchargeID { get; set; }
				public string whsInchargeFirstName { get; set; }
				public string whsInchargeMiddleName { get; set; }
				public string whsInchargeLastName { get; set; }
				public string whsInchargeContactNo { get; set; }
			}

			public class custoutletDetails
			{
				public long? custOutletsID { get; set; }
				public string outletName { get; set; }
				public string outletLocation { get; set; }
			} 

			public class inventoryCountdetails
			{
				public string inventoryCountId { get; set; }
				public int? lineId { get; set; }
				public string itemCode { get; set; }
				public string ssr { get; set; }
				public int? begNvPcs { get; set; }
				public int? sellIn { get; set; }
				public int? endNvPcs { get; set; }
				public int? netOnHand { get; set; }

				public int? actualSellOutPcs { get; set; }
				public decimal? actualSellOutAmt { get; set; }

				public int? forecastFTMpcs0 { get; set; }
				public decimal? forecastFTMamt0 { get; set; }

				public int? forecastFTMpcs1 { get; set; }
				public decimal? forecastFTMamt1 { get; set; }

				public int? forecastFTMpcs2 { get; set; }
				public decimal? forecastFTMamt2 { get; set; }

				public int? forecastFTMpcs3 { get; set; }
				public decimal? forecastFTMamt3 { get; set; }

				public int? variancePcs { get; set; }
				public decimal? varianceAmt { get; set; }

				public string remarks { get; set; }

				//item details
				public string brand { get; set; }
				public string prodGrp { get; set; }
				public string itemDesc { get; set; }
				public decimal? itemPrice { get; set; }
			}


		}

		public class itemFile
		{
			//item details
			public string brand { get; set; }
			public string prodGrp { get; set; }
			public string itemDesc { get; set; }
			public decimal? itemPrice { get; set; }
			public int? sellin { get; set; }
			public string itemCode { get; set; }
			public string itemCodeDesc { get; set; }
		}

		public class DocumentStatus
		{
			public string statusDesc { get; set; }
			public int stateId { get; set; }
			public int roleId { get; set; }
			public int doctype { get; set; }
		}

		public class inventoryCountList
		{
			public string inventoryCountId { get; set; }
			public string empId { get; set; }
			public string empName { get; set; }
			public string acctCode { get; set; }
			public string acctName { get; set; }
			public string acctAddress { get; set; }
			public int custOutletsID { get; set; }
			public string statusDesc { get; set; }
			public int roleId { get; set; }
			public string roleCode { get; set; }
			public int DocumentStatusId { get; set; }

			public string inventoryCountMonth { get; set; }
			public string invStatus{get; set;}
		}

		public class inventoryCountAuditList
		{
			public string inventoryCountAuditId { get; set; }
			public string inventoryCountId { get; set; }
			public string empId { get; set; }
			public string empName { get; set; }
			public string acctCode { get; set; }
			public string outletName { get; set; }
			public string outletLocation { get; set; }
			public int custOutletsID { get; set; }

			public string AuditedBy { get; set; }
			public string invStatus { get; set; }
		}


		public class nextInventoryCount
		{
			public string lineId { get; set; }
			public string acctCode { get; set; }
			public int custOutletsID { get; set; }
			public DateTime? prevCountDate { get; set; }
		  public DateTime? CountDueDateOn { get; set; }
		   // public DateTime? validCountingDate { get; set; }
			public DateTime? startCountDate { get; set; }
			public DateTime? endCountDate { get; set; }
			public DateTime? nextCountDueDate { get; set; }

			public DateTime? actualCountStartValidDate { get; set; }
			public DateTime? actualCountEndValidDate { get; set; }

			public string countInvStatus { get; set; }

			public List<nextInventoryCountDetail> nextInventory_detail { get; set; }

			public class nextInventoryCountDetail
			{
				public string lineId { get; set; }
				public string itemCode { get; set; }
				public string ssr { get; set; }
				public int begNvPcs { get; set; }

				public int? netSellIn { get; set; }

				public int? forecastFTMpcs0 { get; set; }
				public decimal? forecastFTMamt0 { get; set; }

				public int? forecastFTMpcs1 { get; set; }
				public decimal? forecastFTMamt1 { get; set; }

				public int? forecastFTMpcs2 { get; set; }
				public decimal? forecastFTMamt2 { get; set; }


				public string itemDesc { get; set; }
				public string brand { get; set; }
				public string prodGrp { get; set; }
				public decimal? itemPrice { get; set; }
			}
		}

		//public class inventoryCountStatusHdr
		//{
		//    public int in_statusId { get; set; }
		//    public DateTime monthyear { get; set; }

		//    public bool hasRow { get; set; }

		//    public List<inventoryCountStatusDtl> statusDetails { get; set; }

		//    public class inventoryCountStatusDtl
		//    {
		//        public int in_statusId { get; set; }
		//        public int custOutletsID { get; set; }
		//        public string ch_empIDNo { get; set; }
		//        public string ch_status { get; set; }
		//        public string inventoryCountId { get; set; }
		//    }
		//}

		public class inventoryCountAudit_save
		{
			public string inventoryCountAuditId { get; set; }
			public string inventoryCountId { get; set; }
			public DateTime? date { get; set; }
			public string remarks { get; set; }
			public List<inventoryCountAuditDetails_save> inventoryAudit_details { get; set; }

			public class inventoryCountAuditDetails_save
			{
				public int? lineId { get; set; }
				public string itemCode { get; set; }
				public int? actualCount { get; set; }
				public string remarks { get; set; }
			}
		}

		public class inventoryCountAuditDetails
		{
			public string inventoryCountAuditId { get; set; }

			public string auditedByID { get; set; }
			public string auditedByName { get; set; }

			public string inventoryCountId { get; set; }
			public string empId { get; set; }
			public string empFirstName { get; set; }
			public string empLastName { get; set; }
			public string acctCode { get; set; }
			public string acctName { get; set; }
			public string acctAddress { get; set; }
			public int custOutletsID { get; set; }
			public string whsInchargeID { get; set; }
			public DateTime? date { get; set; }
			public string remarks { get; set; }
			public decimal? totalCount { get; set; }
			public string inventoryCountlist_tablebuilder { get; set; }
			public string pareto { get; set; }
			public string area { get; set; }
			public string territoryName { get; set; }
			public string inventoryCountStatus { get; set; }

			public string forthemonth { get; set; }

			public List<inventoryCountAuditdetails> inventorycount_list { get; set; }
			public whsIncharge whs_details { get; set; }
			public custoutletDetails custoutlet_details { get; set; }


			public class whsIncharge
			{
				public string whsInchargeID { get; set; }
				public string whsInchargeFirstName { get; set; }
				public string whsInchargeMiddleName { get; set; }
				public string whsInchargeLastName { get; set; }
				public string whsInchargeContactNo { get; set; }
			}

			public class custoutletDetails
			{
				public int custOutletsID { get; set; }
				public string outletName { get; set; }
				public string outletLocation { get; set; }
			}

			public class inventoryCountAuditdetails
			{
				public string inventoryCountId { get; set; }
				public int? lineId { get; set; }
				public string itemCode { get; set; }
				public int? actualCount { get; set; }

				public string remarks { get; set; }

				//item details
				public string brand { get; set; }
				public string prodGrp { get; set; }
				public string itemDesc { get; set; }
			}
		}

		public class employeeDetail
		{
			public string empIDNo { get; set; }
			public string empFirstName { get; set; }
			public string empLastName { get; set; }

			public string empFullName { get; set; }
		}

		public class userRoles
		{
			public string roleCode { get; set; }
		}

		public class ObjectiveHdr
		{
			public string objectiveCode { get; set; }
			public string objectiveDesc { get; set; }
			public string picturePath { get; set; }
			public List<Objectives> objective_list { get; set; }


			public class Objectives
			{
				public string objectiveCode { get; set; }
				public string objectiveDesc { get; set; }
				public string FieldName { get; set; }
				public bool isUsed { get; set; }
			}

		}

	 

		public class UpdateCallreport
		{
		  public string cFullCollection {get;set;}
		  public string cPartialCollection { get; set;}
		  public string cNoCollection { get; set; }
		  public string EventID { get; set; }
		  public string AccountCode { get; set; }
		  public string ObjectiveCode { get; set; }
		  public int Year { get; set; }
		  public string Month { get; set; }
		  public int Day { get; set; }
		  public string EventDesc { get; set; }
		  public string StoreCheckingResult { get; set; }
		  public string LineNum { get; set; }
		  public string WithOrder { get; set; }
		  public string NextCalldate { get; set; }
		  public string OtherInformation { get; set; }
		  public string CompetitorActivities { get; set; }
		  public string IssuesAndConcerns {get;set;}
		  public string Delivery { get; set;}
		  public string Orders{ get; set;}
		  public string SummaryLackingItems { get; set;}
		  public string Remarks {get;set;}
		  public string Recommendation { get; set;}
		  public string TimeTable { get; set; }

		  public List<presented> Presented_list { get; set; }

		  public class presented 
		  {
			  public string Brand { get; set; }
			  public string CounterClerk { get; set; }
		  
		  }

		 }



		public class UpdateCallreportHasForceObjective
		{


		   // public List<collection> collection_list { get; set; }
			public string EventID { get; set; }
			public int Year { get; set; }
			public string Month { get; set; }
			public int Day { get; set; }
			public string SoId { get; set; }
			public List<forceObjective> forceObjective_list { get; set; }

			public class forceObjective
			{
				public string Objdesc { get; set; }
				public string AccountCode { get; set; }
				public string AccountName { get; set; }
				public string AccountAddress { get; set; }
				public string AccountClass { get; set; }
				public string ContactPerson { get; set; }
				public string ContactNumber { get; set; }
				public string ObjectiveCode { get; set; }
				public string Brand { get; set; }
				public string Amount { get; set; }
				public string cFullCollection { get; set; }
				public string cPartialCollection { get; set; }
				public string cNoCollection { get; set; }



			}

		
		}


		#region Coverage Hdr

		public class CoverageHdr
		{
			public string EventID { get; set; }
			public int Year { get; set; }
			public string Month { get; set; }
			public int Day { get; set; }
			public string EmpIdNo { get; set; }
			public int DoctypeId { get; set; }
			public int DocumentStatusId { get; set; }
			public string action_type { get; set; }
			public string LineNum { get; set; }
			public string AccountCode { get; set; }
			public string ObjectiveCode { get; set; }
			public string ContactPerson { get; set; }
			public string ContactPersonNo { get; set; }
			public string StoreChecking { get; set; }
			public string IssuesAndConcerns { get; set; }
			public string cFullCollection { get; set; }
			public string cPartialCollection { get; set; }
			public string cNoCollection { get; set; }
			public string StoreCheckingResult { get; set; }
			public string Delivery { get; set; }	
			public string Orders { get; set; }	
			public string SummaryLackingItems { get; set; }	
			public string Recommendation { get; set; }	
			public string TimeTable	{ get; set; }
			public string Remarks { get; set; }
			public string CompetitorActivities { get; set; }
			public string WithOrder { get; set; }
			public string NextCallDate { get; set; }
			public string OtherInformation { get; set; }
			public DateTime Tmein { get; set; }
			public DateTime Tmeout { get; set; }
			public int Numvisit { get; set; }
			public string HotelName { get; set; }
			public string HotelContactNum { get; set; }
			public string isDeleted { get; set; }
			public string Attachment { get; set; }
			public string ProductPresentationResult { get; set; }
			public List<collections> collection_list { get; set; }
			public List<merchandising> merchandising_list { get; set; }
			public List<sales> sales_list { get; set; }
			public List<customersrvc> customersrv_list { get; set; }
			public List<collections> uncollection_list { get; set; }
			public List<sales> unsales_list { get; set; }
			public List<changesdtls> Acct_dtls { get; set; }
			public List<merchandising> unmerchandising_list { get; set; }
			public string ApprvrRrmks { get; set; }
			public decimal? ColPostDatedCheck { get; set; }
			public decimal? ColDatedCheck { get; set; }
			public decimal? ColTotal { get; set; }
			public string ColRemarks { get; set; }

			public class collections 
			{
				public string Objdesc { get; set; }
				public string ObjectiveCode { get; set; }
				public string Brand { get; set; }
				public string Amount { get; set; }
				public string ActualAmount { get; set; }
				public string Remarks { get; set; }

			
			}
			public class merchandising 
			{

				public string Objdesc { get; set; }
				public string ObjectiveCode { get; set; }
				public string Brand { get; set; }
				public string Amount { get; set; }
				public string Productpresented { get; set; }
				public string counterclerk { get; set; }
				public string CounterClerkNo { get; set; }
				public string Remarks { get; set; }

			}
			public class sales
			{

				public string Objdesc { get; set; }
				public string ObjectiveCode { get; set; }
				public string Brand { get; set; }
				public string Amount { get; set; }
				public string ActualAmount { get; set; }
				public string Remarks { get; set; }
				public string dtlsRrmks { get; set; }


			}

			public class customersrvc
			{

				public string Objdesc { get; set; }
				public string ObjectiveCode { get; set; }
				public string Brand { get; set; }
				public string Amount { get; set; }
			
			}

			public class changesdtls 
			{

				public int Day { get; set; }
				public string AccountCode { get; set; }
				public int cAcctStatus { get; set; }
				public string RmrkChanges { get;set;}

			
			}


		
		}

		#endregion

		public class UpdateCallreports
		{
			public string EventID { get; set; }
			public int Year { get; set; }
			public string Month { get; set; }
			public int Day { get; set; }
			public string AccountCode { get; set; }
			public string EmpIdNo { get; set; }
			public string cFullCollection { get; set; }
			public string cPartialCollection { get; set; }
			public string cNoCollection { get; set; }
			public List<callreportDtls> callreport_dtls { get; set; }


			public class callreportDtls
			{
			   public string ObjectiveCode  { get; set; }
			   public string Brand { get; set; }
			   public string PlannedAmount { get; set; }
			   public string ActualAmount { get; set; }
			   public string CounterClerk { get; set; }
			   public string ProductPresented { get; set; }
  
			}
		
		}


		public class CallReportLocation
		{
			public string act_type { get; set; }
			public string eventMonth { get; set; }
			public int eventYear { get; set; }
			public int eventDay { get; set; }
			public string empId { get; set; }
			public string EventID { get; set; }
			public string acctCode { get; set; }
			public string contactPerson { get; set; }
			public string contactPersonNo { get; set; }

			public string LineNum { get; set; }
			public string Longitude { get; set; }
			public string Latitude { get; set; }
			public string Address { get; set; }
			public DateTime Time { get; set; }
			//public string CheckInAddress { get; set; }
			//public TimeSpan CheckInTime { get; set; }
		   // public string CheckOutAddress { get; set; }
		   // public TimeSpan CheckOutTime { get; set; }
		}



		public class EventHdr
		{


			public string EventID { get; set; }
			public int Year { get; set; }
			public string Month { get; set; }
			public int Day { get; set; }
			public string EmpIdNo { get; set; }
			public int DoctypeId { get; set; }
			public int DocumentStatusId { get; set; }
			public string action_type { get; set; }
			public List<collection> collection_list { get; set; }
			public List<Merchandise> msde_list { get; set; }
			public List<Sales> sales_list { get; set; }
			public List<CustomerSrvc> customersrvc_list { get; set; }


			public class collection 
			{
				public string Objdesc { get; set; }
				public string AccountCode { get; set; }
				public string AccountName { get; set; }
				public string AccountAddress { get; set; }
				public string AccountClass { get; set; }
				public string ContactPerson { get; set; }
				public string ContactNumber {get; set;}
				public string ObjectiveCode { get; set; } 
				public string Brand { get; set; }
				public string Amount { get; set; }
	

			
			}

			public class Merchandise
			{
				public string Objdesc { get; set; }
				public string ObjectiveCode { get; set; }
				public string AccountCode { get; set; }
				public string AccountName { get; set; }
				public string AccountAddress { get; set; }
				public string AccountClass { get; set; }
				public string ContactPerson { get; set; }
			
			}

			public class Sales
			{

				public string Objdesc { get; set; }
				public string AccountCode { get; set; }
				public string AccountName { get; set; }
				public string AccountAddress { get; set; }
				public string AccountClass { get; set; }
				public string ContactPerson { get; set; }
				public string ContactNumber { get; set; }
				public string ObjectiveCode { get; set; }
				public string Brand { get; set; }
				public string Amount { get; set; }
			
			}

			public class CustomerSrvc
			{
				public string Objdesc { get; set; }
				public string AccountCode { get; set; }
				public string AccountName { get; set; }
				public string AccountAddress { get; set; }
				public string AccountClass { get; set; }
				public string ContactPerson { get; set; }
				public string ContactPersonNo { get; set; }

			}
		
		}


		public class EventDtl
		{
			public string EventID { get; set; }
			public Int32 Year { get; set; }
			public string Month { get; set; }
			public Int32 Day { get; set; }
			public string AccountCode { get; set; }
			public string ObjectiveCode { get; set; }
			public string IsDeleted { get; set; }
			public string isPlanned { get; set; }
			public string IsAnEdit { get; set; }
			public int AcctStatus { get; set; }
			public string hasCallreport { get; set; }



		}

		public class save_uploaded_monthly_coverage
		{
			public int counter_id { get; set; }
			public string EmpIdNo { get; set; }
			public int Year { get; set; }
			public string Month { get; set; }
			public string Day { get; set; }
			public List<fullcalendarEvents> list { get; set; }
		}

		public class fullcalendarEvents
		{
			public int id { get; set; }
			public string title { get; set; }
			public string start { get; set; }
			public string end { get; set; }
			public string backgroundColor { get; set; }
			public bool editable { get; set; }
			public bool allday { get; set; }

			public string account_code { get; set; }
			public string contact_person { get; set; }
			public string contact_person_no { get; set; }
			//public string contact_num { get; set; }
			public string store_checking { get; set; }
			public string issues_and_concerns { get; set; }
			public string hotel_name { get; set; }
			public string hotel_num { get; set; }

			public List<account_objectives> list_objectives {get;set;}

			public class account_objectives
			{
				public string account_code { get; set; }
				public string objective_code { get; set; }
				public string brand { get; set; }
				public string planned_amount { get; set; }
				public string counter_clerk { get; set; }
				public string counter_clerk_no { get; set; }
				public string product_presented { get; set; }
				public string details_remark { get; set; }
			}
			
		}

		public class coverageplan_save_upload
		{
			public int event_day { get; set; }
			public string event_month { get; set; }
			public int event_year { get; set; }
			public string so_id { get; set; }
			public int counter_id { get; set; }

			public List<account_codes> list_accounts {get;set;}

			public class account_codes
			{
				public string account_code { get; set; }
				public string contact_person { get; set; }
				public string contact_person_no { get; set; }
				//public string contact_num { get; set; }
				public string store_checking { get; set; }
				public string issues_and_concerns { get; set; }
				public string hotel_name { get; set; }
				public string hotel_num { get; set; }
		
				public List<account_objectives> list_objectives {get;set;}

				public class account_objectives
				{
					public string objective_code { get; set; }
					public string brand { get; set; }
					public string planned_amount { get; set; }
					public string counter_clerk { get; set; }
					public string counter_clerk_no { get; set; }
					public string product_presented { get; set; }
				}
			}
		}

		public class SoAccount
		{
			public string CardCode { get; set; }
			public string CardName { get; set; }
			public string Address { get; set; }
			public string acctClassfxn { get; set; }
			public string Phone1 { get; set; }
			public string CntctPrsn { get; set; }
			public int? numberOfVisits { get; set; }
			public string isDeleted { get; set; }
			public string hasCallReport { get; set; }

			public string Month { get; set; }
			public string Year { get; set; }
			public string Day { get; set; }
			public string EventId { get; set; }
			public string HotelName { get; set; }
			public string HotelContactNum { get; set; }



		}

		public class lookupContactPerson
		{
			public string Eventmonth { get; set; }
			public string soId { get; set; }
			public int Eventday { get; set; }
			public int Eventyear { get; set; }
			public string acctCode { get; set; }
		}

		public class FilesUploaded
		{
			public long id { get; set; }
			public string FileAttachment { get; set; }
			public DateTime DateTimeStamp { get; set; }
			public string UploadedBy { get; set; }
			public string FileDescription { get; set; }
		}


	   
	}
}


