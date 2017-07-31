using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Text;
using ARMS_W.Class;
using ARMS_W.GLOBALS;

namespace ARMS_W.Controllers
{
	public class CustomerController : Controller
	{
		//
		// GET: /Customer/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult AcctCreateRegularForm()
		{
			return View();
		}

		public ActionResult AcctCreateLeadForm()
		{
			return View();
		}

		public ActionResult AcctCreateLeadFinalForm()
		{
			return View();
		}

		public ActionResult AcctCreateWalkInForm()
		{
			return View();
		}

		public ActionResult AccountMasterFile()
		{
			return View();
		}

		[AuthorizeUsr]
		public string GetFilteredList(string _str_data, string par1 = "", string par2 = "", string tArea = "", string array1 = "")
		{
			string strquery = "", str_region = "", str_channel = "", str_area = "";
			_User oUsr = new _User(HttpContext.Session["username"].ToString());

			foreach (_Roles rls in oUsr.Roles)
			{
				foreach (string region_name in rls.Region)
				{
					if (str_region != "") str_region = str_region + ",";
					str_region = "'" + region_name + "'";
				}

				foreach (string channel_name in rls.Channel)
				{
					if (str_channel != "") str_channel = str_channel + ",";
					str_channel = "'" + channel_name + "'";
				}

				foreach (string area_name in rls.Area)
				{
					if (str_area != "") str_area = str_area + ",";
					str_area = "'" + area_name + "'";
				}
			}

			if (_str_data == "ListOfSo")
			{
				strquery = @" exec arms2_sp_web_arms_param_ListOfSo " + str_region + "";
			}

			if (_str_data == "ListOfASM")
			{

			}

			if (_str_data == "ListOfTerritory")
			{
				strquery = @" exec arms2_sp_web_arms_param_ListOfTerritory " + str_region + "";
			}

			if (_str_data == "ListOfVat")
			{
				strquery = @"
					SELECT 'VAT', 'VAT' union 
					SELECT 'NON-VAT', 'NON-VAT' union 
					SELECT 'ZERO-RATED', 'ZERO-RATED' 
					";
			}

			if (_str_data == "ListOfPaymentGroup")
			{
				strquery = "" +
					"SELECT '', pymntGroup FROM SAPSERVER.MATIMCO.dbo.octg group by pymntGroup" +
					"";
			}

			if (_str_data == "ListOfPriceCode")
			{
				strquery = "select '', FldValue, Descr from SAPSERVER.MATIMCO.dbo.ufd1 where tableID='OCRD'and FieldID='9'";
			}

			if (_str_data == "ListOfLandType")
			{
				strquery = @"
					select '', 'Agricultural' union 
					select '', 'Commercial' union 
					select '', 'Residential' 
					";
			}

			if (_str_data == "ListOfBuildingType")
			{
				strquery = @"
					select '', 'Commercial' union 
					select '', 'Residential' 
					";
			}

			if (_str_data == "ListOfBusinessClass")
			{
				strquery = @"
					select '', 'Direct Distributor' union 
					select '', 'Architect' union 
					select '', 'Contractor' union 
					select '', 'Developer' union 
					select '', 'DIY Outlet' union 
					select '', 'EMAT Account' union 
					select '', 'End User' union 
					select '', 'Full Distributor' union 
					select '', 'Non-GT Hardware' 
					";
			}

			if (_str_data == "ListOfCategoryBrands")
			{
				strquery = @"
					select '', 'M' union 
					select '', 'MM' union 
					select '', 'MMM' union 
					select '', 'Memat' union 
					select '', 'MSTAR' 
					";
			}

			if (_str_data == "ListofCoverageyear")
			{
				strquery = @"
							SELECT DISTINCT Year FROM COVERAGEHDR ORDER BY Year DES";

			}

			if (_str_data == "ListOfTypeOfAccounts")
			{
				strquery = @"
						SELECT 'R','Retained Account' union
						SELECT 'U','Unretained Account'
					";
			}

			if (_str_data == "ListOfActivities")
			{
				strquery = @"
							SELECT '','Phone Call' union
							SELECT '','Meeting' union
							SELECT '','Task' union
							SELECT '','Note' union
							SELECT '','Other' union
							SELECT '','All'
					";
			}

			if (_str_data == "ListOfActivitiesType")
			{
				strquery = @"SELECT '',Name FROM SAPSERVER.MATIMCO.dbo.OCLT union
							 SELECT '','All'
						   ";
			}



			try
			{
				DataTable tmp_table = SqlDbHelper.getDataDT(strquery);
				return "00:" + StringHelper.ConvertDataTableToString(tmp_table);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}
		}

		[AuthorizeUsr]
		[HttpPost]
		public string AddCustomer(SkelClass.page_param.customer_addcustomer p_param)
		{
			p_param.StripNull();
			SQLTransaction mt_trans = new SQLTransaction();
			string newCCANumber = "", new_busTypeHdr_busdocid = "", fin_acct_classification = "";
			DataTable _gtable = SqlDbHelper.getDataDT(SqlQueryHelper.GenerateNewCCANumber());



			DataTable customerHeader = null;


			customerHeader = SqlDbHelper.getDataDT(SqlQueryHelper.check_customer_code_on_create(p_param.acct_code, p_param.request_id));

			foreach (DataRow itm in customerHeader.Rows)
			{
				return SActionResult.Error + "PROPOSED ACCOUNT CODE ALREADY EXIST!";
			}

			if (_gtable.Rows.Count > 0)
			{
				newCCANumber = _gtable.Rows[0].ItemArray[0].ToString();
			}
			else
			{
				throw new FormatException("CCA Number not available.");
			}

			_gtable = SqlDbHelper.getDataDT("select isnull((select max(busdocid) from dbo.busTypeHdr),0) + 1");
			if (_gtable.Rows.Count > 0)
			{
				new_busTypeHdr_busdocid = _gtable.Rows[0].ItemArray[0].ToString();
			}
			else
			{
				throw new FormatException("CCA Number not available.");
			}

			try
			{

				mt_trans.StartTransaction();

				if (p_param.acct_classification == "WALKIN")
				{
					fin_acct_classification = CCA.EAccountClassification.WALKIN.ToString();
				}
				else if (p_param.acct_classification == "REGULAR")
				{
					fin_acct_classification = CCA.EAccountClassification.REGULAR.ToString();
				}

				// -- customerHeader --

				mt_trans.InsertTo("customerHeader",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber}
						,{"acctCode", p_param.acct_code}
						,{"status", "1"}
						,{"acctType", p_param.acct_type.ToUpper()}
						,{"acctClassfxn", p_param.acct_classification}
						,{"keyAcct", Convert.ToByte(p_param.acct_key_account)}
						,{"acctName",p_param.acct_name}
						,{"acctClass", p_param.acct_class}
						,{"acctOffcr", p_param.acct_acct_officer}
						,{"territory", p_param.acct_territory}
						,{"area", p_param.acct_area}
						,{"region", p_param.acct_region}
						,{"emailAdd", p_param.acct_email}
						,{"telNum", p_param.acct_phone_no}
						,{"faxNum", p_param.acct_fax_no}
						,{"offceHrs", p_param.acct_office_hours}
						,{"storeHrs", p_param.acct_store_hours}
						,{"yrsInBusiness", p_param.acct_years_in_business != "" ? Convert.ToInt16(p_param.acct_years_in_business): (int?)null }
						,{"yrsWdMTC", p_param.acct_years_with_matimco != "" ? Convert.ToInt16(p_param.acct_years_with_matimco): (int?)null }
						,{"TIN", p_param.acct_tax_id}
						,{"VATregNum", p_param.acct_vat_no}
						,{"regBusName", p_param.acct_reg_name}
						,{"bussAdd", p_param.acct_business_add}
						,{"delAdd", p_param.acct_delivery_add}
						,{"TotalNumOfEmp", p_param.acct_num_employees != "" ? Convert.ToInt16(p_param.acct_num_employees): (int?)null}
						,{"acctCategoryVal", p_param.acct_category_value}
						,{"acctBusinessClass", p_param.acct_business_class}
						,{"acctCategoryPrem", p_param.acct_category_prem}
						,{"telNum2", p_param.acct_phone_no_2}
						,{"MobileNum", p_param.acct_cellphone}
						,{"TypeOfAccount",p_param.acct_type_of_account}
					}
				);

				// -- empInventory --
				if (p_param.list_of_employee_no.Count > 0)
				{
					foreach (string strn in p_param.list_of_employee_no)
					{
						string[] tmp_columns = strn.Split('|');
						mt_trans.InsertTo("empInventory",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"position", tmp_columns[0]}
								,{"numOfEmp", tmp_columns[1] != "" ? Convert.ToInt32(tmp_columns[1]): (int?)null }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
				}

				// -- custAttachment --

				// acct_article_of_inc
				if (p_param.acct_article_of_inc.Length > 0)
				{
					// copy the file
					if (UploadHelper.ProcessCcaAttachment(newCCANumber, Session["username"].ToString(), p_param.acct_article_of_inc) == true)
					{
						// insert to db
						mt_trans.InsertTo("custAttachment",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"attachType", "AOI"}
								,{"AttachPath", p_param.acct_article_of_inc }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
					else
					{
						throw new Exception("Error processing file[AOI]");
					}
				}

				// acct_financial_statement
				if (p_param.acct_financial_statement.Length > 0)
				{
					// copy the file
					if (UploadHelper.ProcessCcaAttachment(newCCANumber, Session["username"].ToString(), p_param.acct_financial_statement) == true)
					{
						// insert to db
						mt_trans.InsertTo("custAttachment",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"attachType", "FS"}
								,{"AttachPath", p_param.acct_financial_statement }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
					else
					{
						throw new Exception("Error processing file[FS]");
					}
				}

				// acct_itr
				if (p_param.acct_itr.Length > 0)
				{
					// copy the file
					if (UploadHelper.ProcessCcaAttachment(newCCANumber, Session["username"].ToString(), p_param.acct_itr) == true)
					{
						// insert to db
						mt_trans.InsertTo("custAttachment",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"attachType", "ITR"}
								,{"AttachPath", p_param.acct_itr }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
					else
					{
						throw new Exception("Error processing file[ITR]");
					}
				}

				// acct_bir_reg
				if (p_param.acct_bir_reg.Length > 0)
				{
					// copy the file
					if (UploadHelper.ProcessCcaAttachment(newCCANumber, Session["username"].ToString(), p_param.acct_bir_reg) == true)
					{
						// insert to db
						mt_trans.InsertTo("custAttachment",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"attachType", "BIR"}
								,{"AttachPath", p_param.acct_bir_reg }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
					else
					{
						throw new Exception("Error processing file[BIR]");
					}
				}

				// acct_business_permit
				if (p_param.acct_business_permit.Length > 0)
				{
					// copy the file
					if (UploadHelper.ProcessCcaAttachment(newCCANumber, Session["username"].ToString(), p_param.acct_business_permit) == true)
					{
						// insert to db
						mt_trans.InsertTo("custAttachment",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"attachType", "BP"}
								,{"AttachPath", p_param.acct_business_permit }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
					else
					{
						throw new Exception("Error processing file[BP]");
					}
				}

				// other attachment
				if (p_param.acct_attch_other.Length > 0)
				{
					// copy the file
					if (UploadHelper.ProcessCcaAttachment(newCCANumber, Session["username"].ToString(), p_param.acct_attch_other) == true)
					{
						// insert to db
						mt_trans.InsertTo("custAttachment",
							new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"attachType", "OTHER"}
								,{"AttachPath", p_param.acct_attch_other }
								,{"acctCode", p_param.acct_code}
							}
						);
					}
					else
					{
						throw new Exception("Error processing file[OTHER]");
					}
				}

				// busTypeHdr
				if (p_param.acct_business_type == "SoleProprietorship")
				{
					mt_trans.InsertTo("busTypeHdr",
						 new Dictionary<string, object>() { 
							{"busDocID", Convert.ToInt32(new_busTypeHdr_busdocid) }
							,{"ccaNum", newCCANumber}
							,{"acctCode", p_param.acct_code }
							,{"busType", 0}
							,{"ownerCEO", p_param.sole_owner_name}
							,{"genMgr", p_param.sole_gen_manager}
							,{"financeHead", p_param.sole_fin_manager}
							,{"nationality", p_param.sole_nationality}
						}
					 );

				}

				if (p_param.acct_business_type == "Partnership")
				{
					mt_trans.InsertTo("busTypeHdr",
						new Dictionary<string, object>() { 
							{"busDocID", Convert.ToInt32(new_busTypeHdr_busdocid) }
							,{"ccaNum", newCCANumber}
							,{"acctCode", p_param.acct_code }
							,{"busType", 1}
							,{"genMgr", p_param.partner_gen_manager}
							,{"financeHead", p_param.partner_fin_manager}
						}
					);
				}

				if (p_param.acct_business_type == "Corporation")
				{
					mt_trans.InsertTo("busTypeHdr",
						new Dictionary<string, object>() { 
							{"busDocID", Convert.ToInt32(new_busTypeHdr_busdocid) }
							,{"ccaNum", newCCANumber}
							,{"acctCode", p_param.acct_code }
							,{"busType", 2}
							,{"ownerCEO", p_param.corp_ceo}
							,{"genMgr", p_param.corp_gen_man}
							,{"financeHead", p_param.corp_vp_fin}
							,{"dateOfIncorporation", p_param.corp_date_inc != "" ? Convert.ToDateTime(p_param.corp_date_inc).ToString("MM/dd/yyyy") : null}
							,{"authorizedCapStocks", p_param.corp_auth_cap_stock}
							,{"subscribedCapStocks", p_param.corp_subc_cap_stock}
							,{"paidInCapStocks", p_param.corp_paidin_cap_stock}
						}
					);
				}

				// -- busTypeDtl --
				if (p_param.acct_business_type == "Partnership" && p_param.list_of_partner.Count > 0)
				{
					foreach (string strn in p_param.list_of_partner)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("busTypeDtl",
							new Dictionary<string, object>() { 
								{"busDocID", Convert.ToInt32(new_busTypeHdr_busdocid) }
								,{"ccaNum", newCCANumber}
								,{"partnerStockHolder", tmp_str[0] }
								,{"nationality", tmp_str[1]}
								,{"capitalPerOwned", tmp_str[2]}
							}
						);

					}
				}

				// -- busTypeDtl --
				if (p_param.acct_business_type == "Corporation" && p_param.list_major_stockholder.Count > 0)
				{
					foreach (string strn in p_param.list_major_stockholder)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("busTypeDtl",
							new Dictionary<string, object>() { 
								{"busDocID", Convert.ToInt32(new_busTypeHdr_busdocid) }
								,{"ccaNum", newCCANumber}
								,{"partnerStockHolder", tmp_str[0] }
								,{"nationality", tmp_str[1]}
								,{"capitalPerOwned", tmp_str[2]}
							}
						);

					}
				}

				// custBusHdr
				mt_trans.InsertTo("custBusHdr",
					new Dictionary<string, object>() { 
						{"busTermsID", 0 }
						,{"ccaNum", newCCANumber}
						,{"acctCode", p_param.acct_code }
						,{"initialPODetails", p_param.acct_ini_po_details} // added line by BJD 05/31/2016

						/* Code added by Billy Jay (04/23/2015) */
						,{"propCredTermsArchitecturalBrand", p_param.acct_prop_credit_term_architectural_brand}
						,{"propCredTermsEcoforLumber", p_param.acct_prop_credit_term_eco_lumber}
						,{"propCredTermsEcoforPlywood", p_param.acct_prop_credit_term_eco_plywood}

						,{"CredTermRemarksArchitecturalBrand", p_param.acct_prop_credit_term_architectural_brand_remarks}
						,{"CredTermRemarksEcoforLumber", p_param.acct_prop_credit_term_eco_lumber_remarks}
						,{"CredTermRemarksEcoforPlywood", p_param.acct_prop_credit_term_eco_plywood_remarks}

						
						,{"probOrderLimit_AB", p_param.acct_prop_order_limit_ab == "" ? 0 : Convert.ToDecimal(p_param.acct_prop_order_limit_ab)}
						,{"probOrderLimit_TR", p_param.acct_prop_order_limit_tr == "" ? 0 : Convert.ToDecimal(p_param.acct_prop_order_limit_tr)}
						
						,{"OrderLimitRemarks_AB", p_param.acct_prop_order_limit_remarks_ab} 
						,{"OrderLimitRemarks_TR", p_param.acct_prop_order_limit_remarks_tr} 
						/* End Code added by Billy Jay (04/23/2015) */

						,{"propCredTerms", p_param.acct_prop_credit_term}
						,{"probCredLimit", p_param.acct_prop_credit_limit}
						,{"sociaEcoClass", p_param.acct_socio_eco_class}
						,{"numOfOutlet", p_param.acct_num_outlets != "" ? Convert.ToInt32(p_param.acct_num_outlets) : (int?)null }
						,{"CredTermRemarks", p_param.acct_prop_credit_term_remarks}
						,{"CredLimitRemarks", p_param.acct_prop_credit_limit_remarks} 
					}
				);

				// custOutlets
				if (p_param.list_of_outlets.Count > 0)
				{
					foreach (string strn in p_param.list_of_outlets)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("custOutlets",
						   new Dictionary<string, object>() { 
								{"busTermsID", 0 }
								,{"ccaNum", newCCANumber}
								,{"name", tmp_str[0] }
								,{"location", tmp_str[1]}
								,{"storeSize", tmp_str[2]}
								,{"wreHouseSize", tmp_str[3]}
							}
					   );

					}
				}

				// customerEvents
				if (p_param.list_of_events.Count > 0)
				{
					foreach (string strn in p_param.list_of_events)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("customerEvents",
						   new Dictionary<string, object>() { 
								{"ccaNum", newCCANumber}
								,{"name", tmp_str[0] }
								,{"event", tmp_str[1]}
								,{"date", tmp_str[2]}
								,{"contactnumber", tmp_str[3]}
								,{"specialEvent", tmp_str[4]}
							}
					   );

					}
				}

				// majorCustomer
				if (p_param.list_of_major_customer.Count > 0)
				{
					foreach (string strn in p_param.list_of_major_customer)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("majorCustomer",
						   new Dictionary<string, object>() { 
								{"busTermsID", 0 }
								,{"ccaNum", newCCANumber}
								,{"name", tmp_str[0] }
								,{"address", tmp_str[1]}
								,{"sellingTerms", tmp_str[2]}
								,{"estMonthPur", tmp_str[3] != "" ? Convert.ToDecimal(tmp_str[3]) : (decimal?)null}
							}
					   );
					}
				}

				// propsedPrice
				// matwood - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "MW"}
						,{"priceListCode", p_param.acct_mw_price_code}
						,{"codeDesc", p_param.acct_mw_price_desc}
						,{"remarks", p_param.acct_mw_price_remarks}
						,{"CommisionDiscounts", p_param.acct_mw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_mw_price_commision_disc) : (decimal?)null }
					}
				);

				// weatherwood - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "WW"}
						,{"priceListCode", p_param.acct_ww_price_code}
						,{"codeDesc", p_param.acct_ww_price_desc}
						,{"remarks", p_param.acct_ww_price_remarks}
						,{"CommisionDiscounts", p_param.acct_ww_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_ww_price_commision_disc) : (decimal?)null}
					}
				);

				// pcw frame - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "PWF"}
						,{"priceListCode", p_param.acct_pwf_price_code}
						,{"codeDesc", p_param.acct_pwf_price_desc}
						,{"remarks", p_param.acct_pwf_price_remarks}
						,{"CommisionDiscounts", p_param.acct_pwf_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_pwf_price_commision_disc) : (decimal?)null}
					}
				);

				// pcw reg items - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "PWR"}
						,{"priceListCode", p_param.acct_pwr_price_code}
						,{"codeDesc", p_param.acct_pwr_price_desc}
						,{"remarks", p_param.acct_pwr_price_remarks}
						,{"CommisionDiscounts", p_param.acct_pwr_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_pwr_price_commision_disc) : (decimal?)null}
					}
				);

				// gudwood - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "GW"}
						,{"priceListCode", p_param.acct_gw_price_code}
						,{"codeDesc", p_param.acct_gw_price_desc}
						,{"remarks", p_param.acct_gw_price_remarks}
						,{"CommisionDiscounts", p_param.acct_gw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_gw_price_commision_disc) : (decimal?)null}
					}
				);

				// trusswood - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "TW"}
						,{"priceListCode", p_param.acct_tw_price_code}
						,{"codeDesc", p_param.acct_tw_price_desc}
						,{"remarks", p_param.acct_tw_price_remarks}
						,{"CommisionDiscounts", p_param.acct_tw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_tw_price_commision_disc) : (decimal?)null}
					}
				);

				// muzuwood - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "MZ"}
						,{"priceListCode", p_param.acct_mz_price_code}
						,{"codeDesc", p_param.acct_mz_price_desc}
						,{"remarks", p_param.acct_mz_price_remarks}
						,{"CommisionDiscounts", p_param.acct_mz_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_mz_price_commision_disc) : (decimal?)null}
					}
				);

				// nuwood - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "NW"}
						,{"priceListCode", p_param.acct_nw_price_code}
						,{"codeDesc", p_param.acct_nw_price_desc}
						,{"remarks", p_param.acct_nw_price_remarks}
						,{"CommisionDiscounts", p_param.acct_nw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_nw_price_commision_disc) : (decimal?)null}
					}
				);

				// ecofor treated - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "EC"}
						,{"priceListCode", p_param.acct_ec_price_code}
						,{"codeDesc", p_param.acct_ec_price_desc}
						,{"remarks", p_param.acct_ec_price_remarks}
						,{"CommisionDiscounts", p_param.acct_ec_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_ec_price_commision_disc) : (decimal?)null}
					}
				);

				// ecofor untreated - propsedPrice
				mt_trans.InsertTo("propsedPrice",
					new Dictionary<string, object>() { 
						{"ccaNum", newCCANumber} ,{"busTermsID", 0} ,{"brandType", "ECU"}
						,{"priceListCode", p_param.acct_ecu_price_code}
						,{"codeDesc", p_param.acct_ecu_price_desc}
						,{"remarks", p_param.acct_ecu_price_remarks}
						,{"CommisionDiscounts", p_param.acct_ecu_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_ecu_price_commision_disc) : (decimal?)null}
					}
				);

				// products
				mt_trans.InsertTo("products",
					new Dictionary<string, object>() { 
						{"prodID", 0}
						,{"ccaNum", newCCANumber }
						,{"acctCode", p_param.acct_code }
						,{"majorLine", p_param.acct_major_prod_line }
						,{"otherProductLine", p_param.acct_other_prod_line }
						,{"suppPlywood", p_param.acct_supplier_on_plywood }
						,{"suppSteel", p_param.acct_supplier_on_steel }
						,{"suppCement", p_param.acct_supplier_on_cement }
						,{"suppCHB", p_param.acct_supplier_on_con_hollowblock }
						,{"suppOthers", p_param.acct_supplier_on_others }
						,{"volValueDriver", p_param.acct_major_vol_business }
						,{"woodVolValue", p_param.acct_monthly_wood_vol }
						,{"discounts", p_param.acct_discount_enjoyed }
					}
				);

				// otherWoodSupp
				if (p_param.list_of_other_wood_suppliers.Count > 0)
				{
					foreach (string strn in p_param.list_of_other_wood_suppliers)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("otherWoodSupp",
							new Dictionary<string, object>() { 
								{"prodID", 0 }
								,{"ccaNum", newCCANumber }
								,{"supplier", tmp_str[0] }
								,{"monthVolVal", tmp_str[1] }
								,{"contactPerson", tmp_str[2] }
								,{"contactNum", tmp_str[3] }
								,{"prodPurchase", tmp_str[4] }
								,{"creditTerms", tmp_str[5] }
							}
						);

					}
				}

				// reclass
				if (p_param.request_id != "" && p_param.request_id != null)
				{
					// SaveReclassedLeadCustomer(request_id, newCCANumber);
					mt_trans.UpdateTo("customerLeadI",
						new Dictionary<string, object>() { { "ccaNum", newCCANumber } }
						, new Dictionary<string, object>() { { "RequestId", p_param.request_id } }
					);
				}

				// insert to default values to proposedChangesCA
				mt_trans.InsertTo("proposedChangesCA",
					new Dictionary<string, object>() { { "ccaNum", newCCANumber }, { "status", "1" } }
				);

				mt_trans.Committransaction();

				MarkCustomerCreationDocument("APPROVE", newCCANumber, "", fin_acct_classification);

				return SActionResult.Success + newCCANumber;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();

				return SActionResult.Error + ex.Message;
			}

		}

		[AuthorizeUsr]
		[HttpPost]
		public string SaveCustomer(SkelClass.page_param.customer_savecustomer p_param)
		{
			p_param.StripNull();
			SQLTransaction mt_trans = new SQLTransaction();
			DataTable _gtable = null;

			System.Data.DataRow doc_cur_stat_row = SqlDbHelper.getDataDT("select rtrim(ltrim(status)) as 'status' from customerHeader where ccanum='" + p_param.acct_ccanum + "'").Rows[0];
			string current_document_status = doc_cur_stat_row["status"].ToString();

			string current_busTypeHdr_busdocid = "";
			string new_busTypeHdr_busdocid = "";

			string proposed_changes_routetype = "";

			_gtable = SqlDbHelper.getDataDT("select isnull((select max(busdocid) from dbo.busTypeHdr where ccaNum='" + p_param.acct_ccanum + "'),0)");
			if (_gtable.Rows.Count > 0)
			{
				current_busTypeHdr_busdocid = _gtable.Rows[0].ItemArray[0].ToString();
			}
			else
			{
				throw new Exception("busDocId Number not available.");
			}

			_gtable = SqlDbHelper.getDataDT("select isnull((select max(busdocid) from dbo.busTypeHdr),0) + 1");
			if (_gtable.Rows.Count > 0)
			{
				new_busTypeHdr_busdocid = _gtable.Rows[0].ItemArray[0].ToString();
			}
			else
			{
				throw new Exception("busDocId Number not available.");
			}

			_gtable = SqlDbHelper.getDataDT("select routeType from proposedChangesCA where ccanum = ''");
			foreach (DataRow itm in _gtable.Rows)
			{
				proposed_changes_routetype = itm["routeType"].ToString();
			}

			System.Data.DataTable IsInTable = SqlDbHelper.getDataDT(SqlQueryHelper.tablecontain(p_param.acct_ccanum));
			System.Data.DataRow IsInTableRow = IsInTable.Rows[0];

			// additional checking of datas
			// phone no
			if (p_param.acct_phone_no.Length > 20 || p_param.acct_phone_no_2.Length > 20)
			{
				throw new Exception("Phone number must not exceed in 20 characters.");
			}

			try
			{

				mt_trans.StartTransaction();

				mt_trans.UpdateTo("customerHeader",
				new Dictionary<string, object>(){
					{"acctCode", p_param.acct_code }
					,{"acctType", p_param.acct_type.ToUpper() }
					,{"acctClassfxn", p_param.acct_classification }
					,{"keyAcct", p_param.acct_key_account }
					,{"acctName", p_param.acct_name }
					,{"acctClass", p_param.acct_class }
					,{"acctOffcr", p_param.acct_acct_officer }
					,{"territory", p_param.acct_territory }
					,{"area", p_param.acct_area }
					,{"region", p_param.acct_region }
					,{"emailAdd", p_param.acct_email }
					,{"telNum", p_param.acct_phone_no }
					,{"faxNum", p_param.acct_fax_no }
					,{"offceHrs", p_param.acct_office_hours }
					,{"storeHrs", p_param.acct_store_hours }
					,{"yrsInBusiness", p_param.acct_years_in_business != "" ? Convert.ToInt32(p_param.acct_years_in_business) : (int?)null }
					,{"yrsWdMTC", p_param.acct_years_with_matimco != "" ? Convert.ToInt32(p_param.acct_years_with_matimco) : (int?)null }
					,{"TIN", p_param.acct_tax_id }
					,{"VATregNum", p_param.acct_vat_no }
					,{"regBusName", p_param.acct_reg_name }
					,{"bussAdd", p_param.acct_business_add }
					,{"delAdd", p_param.acct_delivery_add }
					,{"TotalNumOfEmp", p_param.acct_num_employees != "" ? Convert.ToInt32(p_param.acct_num_employees) : (int?)null }
					,{"acctBusinessClass", p_param.acct_business_class }
					,{"acctCategoryVal", p_param.acct_category_value }
					,{"acctCategoryPrem", p_param.acct_category_prem }
					,{"telNum2", p_param.acct_phone_no_2 }
					,{"MobileNum", p_param.acct_cellphone }
					,{"TypeOfAccount", p_param.acct_type_of_account }
				}
				, new Dictionary<string, object>(){
					{"ccaNum", p_param.acct_ccanum}
				});

				// list of employee
				if (p_param.list_of_employee_no.Count > 0)
				{
					// delete all in the empInventory
					mt_trans.DeleteFrom("empInventory",
						new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } }
					);

					// insert back to empInventory
					foreach (string strn in p_param.list_of_employee_no)
					{
						string[] tmp_columns = strn.Split('|');
						mt_trans.InsertTo("empInventory",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }
							,{"position", tmp_columns[0] }
							,{"numOfEmp", tmp_columns[1] != "" ? Convert.ToInt32(tmp_columns[1]) : (int?)null }
							,{"acctCode", p_param.acct_code }
						});
					}
				}

				// ATTACHMENTS
				if (p_param.acct_article_of_inc_forupload == "true")
				{
					if (UploadHelper.ProcessCcaAttachment(p_param.acct_ccanum, Session["username"].ToString(), p_param.acct_article_of_inc) == false)
					{
						throw new Exception("Error processing file [acct_article_of_inc]");
					}
					else
					{

						mt_trans.DeleteFrom("custAttachment",
						new Dictionary<string, object>() { 
								{"attachType", "AOI"}, {"ccanum", p_param.acct_ccanum}
						});

						// INSERT
						mt_trans.InsertTo("custAttachment",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }, {"acctCode", p_param.acct_code }
							,{"attachType", "AOI" }, {"AttachPath", p_param.acct_article_of_inc }
						});
					}
				}

				if (p_param.acct_financial_statement_forupload == "true")
				{
					if (UploadHelper.ProcessCcaAttachment(p_param.acct_ccanum, Session["username"].ToString(), p_param.acct_financial_statement) == false)
					{
						throw new Exception("Error processing file [acct_financial_statement]");
					}
					else
					{
						mt_trans.DeleteFrom("custAttachment",
						new Dictionary<string, object>() { 
							{"attachType", "FS"}, {"ccanum", p_param.acct_ccanum}
						});

						// INSERT
						mt_trans.InsertTo("custAttachment",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }, {"acctCode", p_param.acct_code }
							,{"attachType", "FS" }, {"AttachPath", p_param.acct_financial_statement }
						});
					}
				}

				if (p_param.acct_itr_forupload == "true")
				{
					if (UploadHelper.ProcessCcaAttachment(p_param.acct_ccanum, Session["username"].ToString(), p_param.acct_itr) == false)
					{
						throw new Exception("Error processing file [acct_itr]");
					}
					else
					{
						mt_trans.DeleteFrom("custAttachment",
						new Dictionary<string, object>() { 
							{"attachType", "ITR"}, {"ccanum", p_param.acct_ccanum}
						});

						// INSERT
						mt_trans.InsertTo("custAttachment",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }, {"acctCode", p_param.acct_code }
							,{"attachType", "ITR" }, {"AttachPath", p_param.acct_itr }
						});
					}
				}

				if (p_param.acct_bir_reg_forupload == "true")
				{
					if (UploadHelper.ProcessCcaAttachment(p_param.acct_ccanum, Session["username"].ToString(), p_param.acct_bir_reg) == false)
					{
						throw new Exception("Error processing file [acct_bir_reg]");
					}
					else
					{
						mt_trans.DeleteFrom("custAttachment",
						new Dictionary<string, object>() { 
							{"attachType", "BIR"}, {"ccanum", p_param.acct_ccanum}
						});

						// INSERT
						mt_trans.InsertTo("custAttachment",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }, {"acctCode", p_param.acct_code }
							,{"attachType", "BIR" }, {"AttachPath", p_param.acct_bir_reg }
						});
					}
				}

				if (p_param.acct_business_permit_forupload == "true")
				{
					if (UploadHelper.ProcessCcaAttachment(p_param.acct_ccanum, Session["username"].ToString(), p_param.acct_business_permit) == false)
					{
						throw new Exception("Error processing file [acct_business_permit]");
					}
					else
					{
						mt_trans.DeleteFrom("custAttachment",
						new Dictionary<string, object>() { 
							{"attachType", "BP"}, {"ccanum", p_param.acct_ccanum}
						});

						// INSERT
						mt_trans.InsertTo("custAttachment",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }, {"acctCode", p_param.acct_code }
							,{"attachType", "BP" }, {"AttachPath", p_param.acct_business_permit }
						});
					}
				}

				if (p_param.acct_attch_other_forupload == "true")
				{
					if (UploadHelper.ProcessCcaAttachment(p_param.acct_ccanum, Session["username"].ToString(), p_param.acct_attch_other) == false)
					{
						throw new Exception("Error processing file [acct_attch_other]");
					}
					else
					{
						mt_trans.DeleteFrom("custAttachment",
						new Dictionary<string, object>() { 
							{"attachType", "OTHER"}, {"ccanum", p_param.acct_ccanum}
						});

						// INSERT
						mt_trans.InsertTo("custAttachment",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }, {"acctCode", p_param.acct_code }
							,{"attachType", "OTHER" }, {"AttachPath", p_param.acct_attch_other }
						});
					}
				}

				// check if exist in busTypeHdr
				if (IsInTableRow["busTypeHdr"].ToString().Trim() == "true")
				{
					// UPDATE
					// SOLE
					if (p_param.acct_business_type == "SoleProprietorship")
					{
						mt_trans.UpdateTo("busTypeHdr",
						new Dictionary<string, object>(){
							{"acctCode", p_param.acct_code }
							,{"busType", 0 }
							,{"ownerCEO", p_param.sole_owner_name }
							,{"genMgr", p_param.sole_gen_manager }
							,{"financeHead", p_param.sole_fin_manager }
							,{"nationality", p_param.sole_nationality }
						}
						, new Dictionary<string, object>(){
							{"ccaNum", p_param.acct_ccanum}
						});
					}

					// PARTNERSHIP
					if (p_param.acct_business_type == "Partnership")
					{
						mt_trans.UpdateTo("busTypeHdr",
						new Dictionary<string, object>(){
							{"acctCode", p_param.acct_code }
							,{"busType", 1 }
							,{"genMgr", p_param.partner_gen_manager }
							,{"financeHead", p_param.partner_fin_manager }
						}
						, new Dictionary<string, object>(){
							{"ccaNum", p_param.acct_ccanum}
						});

					}

					// CORPORATION
					if (p_param.acct_business_type == "Corporation")
					{
						mt_trans.UpdateTo("busTypeHdr",
						new Dictionary<string, object>(){
							{"acctCode", p_param.acct_code }
							,{"busType", 2 }
							,{"ownerCEO", p_param.corp_ceo }
							,{"genMgr", p_param.corp_gen_man }
							,{"financeHead", p_param.corp_vp_fin }
							,{"dateOfIncorporation", p_param.corp_date_inc != "" ? Convert.ToDateTime(p_param.corp_date_inc).ToString("MM/dd/yyyy") : null }
							,{"authorizedCapStocks", p_param.corp_auth_cap_stock }
							,{"subscribedCapStocks", p_param.corp_subc_cap_stock }
							,{"paidInCapStocks", p_param.corp_paidin_cap_stock }
						}
						, new Dictionary<string, object>(){
							{"ccaNum", p_param.acct_ccanum}
						});

					}
				}
				else
				{
					// INSERT
					// SOLE
					if (p_param.acct_business_type == "SoleProprietorship")
					{
						mt_trans.InsertTo("busTypeHdr",
						new Dictionary<string, object>() { 
							{"busDocId", new_busTypeHdr_busdocid }
							,{"ccaNum", p_param.acct_ccanum }
							,{"acctCode", p_param.acct_code }
							,{"busType", 0 }
							,{"ownerCEO", p_param.sole_owner_name }
							,{"genMgr", p_param.sole_gen_manager }
							,{"financeHead", p_param.sole_fin_manager }
							,{"nationality", p_param.sole_nationality }
						});

					}

					// PARTNERSHIP
					if (p_param.acct_business_type == "Partnership")
					{
						mt_trans.InsertTo("busTypeHdr",
						new Dictionary<string, object>() { 
							{"busDocId", new_busTypeHdr_busdocid }
							,{"ccaNum", p_param.acct_ccanum }
							,{"acctCode", p_param.acct_code }
							,{"busType", 1 }
							,{"genMgr", p_param.partner_gen_manager }
							,{"financeHead", p_param.partner_fin_manager }
						});
					}

					// CORPORATION
					if (p_param.acct_business_type == "Corporation")
					{
						mt_trans.InsertTo("busTypeHdr",
						new Dictionary<string, object>() { 
							{"busDocId", new_busTypeHdr_busdocid }
							,{"ccaNum", p_param.acct_ccanum }
							,{"acctCode", p_param.acct_code }
							,{"busType", 2 }
							,{"ownerCEO", p_param.corp_ceo }
							,{"genMgr", p_param.corp_gen_man }
							,{"financeHead", p_param.corp_vp_fin }
							,{"dateOfIncorporation", p_param.corp_date_inc }
							,{"authorizedCapStocks", p_param.corp_auth_cap_stock }
							,{"subscribedCapStocks", p_param.corp_subc_cap_stock }
							,{"paidInCapStocks", p_param.corp_paidin_cap_stock }
						});
					}

				}

				// busTypeDtl
				if (IsInTableRow["busTypeDtl"].ToString().Trim() == "true")
				{
					// delete
					mt_trans.DeleteFrom("busTypeDtl",
					new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
					});
				}

				// insert busTypeDtl
				if (IsInTableRow["busTypeHdr"].ToString().Trim() == "false")
				{
					current_busTypeHdr_busdocid = new_busTypeHdr_busdocid;
				}

				// PARTNERSHIP
				if (p_param.acct_business_type == "Partnership" && p_param.list_of_partner.Count > 0)
				{
					foreach (string strn in p_param.list_of_partner)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("busTypeDtl",
						new Dictionary<string, object>() { 
							{"busDocID", current_busTypeHdr_busdocid }
							,{"ccaNum", p_param.acct_ccanum }
							,{"partnerStockHolder", tmp_str[0] }
							,{"nationality", tmp_str[1] }
							,{"capitalPerOwned", tmp_str[2] }
						});
					}
				}

				// CORPORATION
				if (p_param.acct_business_type == "Corporation" && p_param.list_major_stockholder.Count > 0)
				{
					foreach (string strn in p_param.list_major_stockholder)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("busTypeDtl",
						new Dictionary<string, object>() { 
							{"busDocID", current_busTypeHdr_busdocid }
							,{"ccaNum", p_param.acct_ccanum }
							,{"partnerStockHolder", tmp_str[0] }
							,{"nationality", tmp_str[1] }
							,{"capitalPerOwned", tmp_str[2] }
						});
					}
				}

				// custBusHdr
				mt_trans.TryInsertUpdate("custBusHdr",
					new Dictionary<string, object>(){ 
						{"busTermsID", 0 }
						,{"acctCode", p_param.acct_code }
						/* Code added by Billy Jay (04/23/2015) */
						,{"propCredTermsArchitecturalBrand", p_param.acct_prop_credit_term_architectural_brand}
						,{"propCredTermsEcoforLumber",p_param.acct_prop_credit_term_ecofor_lumber}
						,{"propCredTermsEcoforPlywood",p_param.acct_prop_credit_term_ecofor_plywood}
						,{"CredTermRemarksArchitecturalBrand", p_param.acct_prop_credit_term_remarks_architectural_brand}
						,{"CredTermRemarksEcoforLumber", p_param.acct_prop_credit_term_remarks_ecofor_lumber}
						,{"CredTermRemarksEcoforPlywood",p_param.acct_prop_credit_term_remarks_ecofor_plywood}

						,{"probOrderLimit_AB",p_param.acct_prop_order_limit_ab==""?0: Convert.ToInt64(Convert.ToDecimal(p_param.acct_prop_order_limit_ab)) }
						,{"probOrderLimit_TR",p_param.acct_prop_order_limit_tr==""?0: Convert.ToInt64(Convert.ToDecimal(p_param.acct_prop_order_limit_tr)) }

						,{"OrderLimitRemarks_AB", p_param.acct_prop_order_limit_remarks_ab }
						,{"OrderLimitRemarks_TR", p_param.acct_prop_order_limit_remarks_tr }
						/* End Code added by Billy Jay (04/23/2015) */
						,{"propCredTerms", p_param.acct_prop_credit_term }
						,{"probCredLimit", p_param.acct_prop_credit_limit }
						,{"sociaEcoClass", p_param.acct_socio_eco_class }
						,{"numOfOutlet", p_param.acct_num_outlets != "" ? Convert.ToInt32(p_param.acct_num_outlets) : (int?)null }
						,{"CredTermRemarks", p_param.acct_prop_credit_term_remarks }
						,{"CredLimitRemarks", p_param.acct_prop_credit_limit_remarks }
						,{"initialPODetails",p_param.acct_ini_po_details}
					}
					, new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum }
					}
				);

				// custOutlets
				if (IsInTableRow["custOutlets"].ToString().Trim() == "true")
				{
					// DELETE
					mt_trans.DeleteFrom("custOutlets", new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
					});
				}

				// INSERT custOutlets
				if (p_param.list_of_outlets.Count > 0)
				{
					foreach (string strn in p_param.list_of_outlets)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("custOutlets",
						new Dictionary<string, object>() { 
							{"busTermsID", 0 }
							,{"ccaNum", p_param.acct_ccanum }
							,{"name", tmp_str[0] }
							,{"location", tmp_str[1] }
							,{"storeSize", tmp_str[2] }
							,{"wreHouseSize", tmp_str[3] }
						});

					}
				}

				// customerEvents
				if (IsInTableRow["customerEvents"].ToString().Trim() == "true")
				{
					// DELETE
					mt_trans.DeleteFrom("customerEvents", new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
					});
				}

				// INSERT customerEvents
				if (p_param.list_of_events.Count > 0)
				{
					foreach (string strn in p_param.list_of_events)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("customerEvents",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum }
							,{"name", tmp_str[0] }
							,{"event", tmp_str[1] }
							,{"date", tmp_str[2] }
							,{"contactnumber", tmp_str[3] }
							,{"specialevent", tmp_str[4] }
						});

					}
				}

				// majorCustomer
				if (IsInTableRow["majorCustomer"].ToString().Trim() == "true")
				{
					// DELETE
					mt_trans.DeleteFrom("majorCustomer", new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
					});
				}

				// insert to majorCustomer
				if (p_param.list_of_major_customer.Count > 0)
				{
					foreach (string strn in p_param.list_of_major_customer)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("majorCustomer",
						new Dictionary<string, object>() { 
							{"busTermsID", 0}
							,{"ccaNum", p_param.acct_ccanum}
							,{"name", tmp_str[0]}
							,{"address", tmp_str[1]}
							,{"sellingTerms", tmp_str[2]}
							,{"estMonthPur", tmp_str[3] != "" ? Convert.ToDecimal(tmp_str[3]) : (decimal?)null }
						});
					}
				}

				// PROPOSEDPRICE 
				//MW
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_mw_price_code }
					,{"codeDesc", p_param.acct_mw_price_desc }
					,{"remarks", p_param.acct_mw_price_remarks }
					,{"CommisionDiscounts", p_param.acct_mw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_mw_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "MW" }
				});

				// WW
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_ww_price_code }
					,{"codeDesc", p_param.acct_ww_price_desc }
					,{"remarks", p_param.acct_ww_price_remarks }
					,{"CommisionDiscounts", p_param.acct_ww_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_ww_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "WW" }
				});

				// PWF
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_pwf_price_code }
					,{"codeDesc", p_param.acct_pwf_price_desc }
					,{"remarks", p_param.acct_pwf_price_remarks }
					,{"CommisionDiscounts", p_param.acct_pwf_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_pwf_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "PWF" }
				});

				// PWR
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_pwr_price_code }
					,{"codeDesc", p_param.acct_pwr_price_desc }
					,{"remarks", p_param.acct_pwr_price_remarks }
					,{"CommisionDiscounts", p_param.acct_pwr_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_pwr_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "PWR" }
				});

				// GW
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_gw_price_code }
					,{"codeDesc", p_param.acct_gw_price_desc }
					,{"remarks", p_param.acct_gw_price_remarks }
					,{"CommisionDiscounts", p_param.acct_gw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_gw_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "GW" }
				});

				// TW
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_tw_price_code }
					,{"codeDesc", p_param.acct_tw_price_desc }
					,{"remarks", p_param.acct_tw_price_remarks }
					,{"CommisionDiscounts", p_param.acct_tw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_tw_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "TW" }
				});

				// MZ
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_mz_price_code }
					,{"codeDesc", p_param.acct_mz_price_desc }
					,{"remarks", p_param.acct_mz_price_remarks }
					,{"CommisionDiscounts", p_param.acct_mz_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_mz_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "MZ" }
				});

				// NW
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_nw_price_code }
					,{"codeDesc", p_param.acct_nw_price_desc }
					,{"remarks", p_param.acct_nw_price_remarks }
					,{"CommisionDiscounts", p_param.acct_nw_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_nw_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "NW" }
				});

				// EC
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_ec_price_code }
					,{"codeDesc", p_param.acct_ec_price_desc }
					,{"remarks", p_param.acct_ec_price_remarks }
					,{"CommisionDiscounts", p_param.acct_ec_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_ec_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "EC" }
				});

				// ECU
				mt_trans.TryInsertUpdate("propsedPrice",
				new Dictionary<string, object>() { 
					{"busTermsID", 0 }
					,{"priceListCode", p_param.acct_ecu_price_code }
					,{"codeDesc", p_param.acct_ecu_price_desc }
					,{"remarks", p_param.acct_ecu_price_remarks }
					,{"CommisionDiscounts", p_param.acct_ecu_price_commision_disc != "" ? Convert.ToDecimal(p_param.acct_ecu_price_commision_disc) : (decimal?)null }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum } ,{"brandType", "ECU" }
				});

				mt_trans.TryInsertUpdate("products",
				new Dictionary<string, object>(){ 
					{"prodID", 0 }
					,{"acctCode", p_param.acct_code }
					,{"majorLine", p_param.acct_major_prod_line }
					,{"otherProductLine", p_param.acct_other_prod_line }
					,{"suppPlywood", p_param.acct_supplier_on_plywood }
					,{"suppSteel", p_param.acct_supplier_on_steel }
					,{"suppCement", p_param.acct_supplier_on_cement }
					,{"suppCHB", p_param.acct_supplier_on_con_hollowblock }
					,{"suppOthers", p_param.acct_supplier_on_others }
					,{"volValueDriver", p_param.acct_major_vol_business }
					,{"woodVolValue", p_param.acct_monthly_wood_vol }
					,{"discounts", p_param.acct_discount_enjoyed }
				}
				, new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum }
				});


				// otherWoodSupp
				if (IsInTableRow["otherWoodSupp"].ToString().Trim() == "true")
				{
					// DELETE
					mt_trans.DeleteFrom("otherWoodSupp",
					new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum }
					});
				}

				// insert otherWoodSupp
				if (p_param.list_of_other_wood_suppliers.Count > 0)
				{
					foreach (string strn in p_param.list_of_other_wood_suppliers)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("otherWoodSupp",
						new Dictionary<string, object>() { 
							{"prodID", 0 }
							,{"ccaNum", p_param.acct_ccanum }
							,{"supplier", tmp_str[0] }
							,{"monthVolVal", tmp_str[1] }
							,{"contactPerson", tmp_str[2] }
							,{"contactNum", tmp_str[3] }
							,{"prodPurchase", tmp_str[4] }
							,{"creditTerms", tmp_str[5] }
						});
					}
				}

				CCA.EDocumentChanges RouteType = CCA.EDocumentChanges.none;
				// check if already in SAP
				if (current_document_status == "1000" && proposed_changes_routetype == "")
				{

					RouteType = CCA.DocumentChanges.GetRouteType(
							p_param.acct_name, p_param.proposed_new_acct_name,
							p_param.acct_acct_officer, p_param.proposed_new_acct_officer,
							p_param.acct_territory, p_param.proposed_new_acct_territory,
							p_param.acct_area, p_param.proposed_acct_new_area,
							p_param.acct_region, p_param.proposed_new_acct_region,
							p_param.acct_reg_name, p_param.proposed_new_acct_reg_name,
							p_param.acct_business_add, p_param.proposed_new_acct_business_add,
							p_param.acct_delivery_add, p_param.proposed_new_acct_delivery_add,

							/* Code added by Billy Jay (04/23/2015) */
							p_param.acct_prop_credit_term_architectural_brand, p_param.proposed_new_acct_prop_credit_term_architectural_brand,
							p_param.acct_prop_credit_term_ecofor_lumber, p_param.proposed_new_acct_prop_credit_term_ecofor_lumber,
							p_param.acct_prop_credit_term_ecofor_plywood, p_param.proposed_new_acct_prop_credit_term_ecofor_plywood,
							p_param.acct_prop_credit_term_remarks_architectural_brand, p_param.proposed_new_acct_credit_term_remarks_architectural_brand,
							p_param.acct_prop_credit_term_remarks_ecofor_lumber, p_param.proposed_new_acct_credit_term_remarks_ecofor_lumber,
							p_param.acct_prop_credit_term_remarks_ecofor_plywood, p_param.proposed_new_acct_credit_term_remarks_ecofor_plywood,
						// p_param.acct_prop_order_limit_ab, p_param.proposed_new_acct_prop_order_limit_ab,
						 p_param.acct_prop_order_limit_ab, (p_param.proposed_new_acct_prop_order_limit_ab == "" ? "0" : Convert.ToInt64(Convert.ToDecimal(p_param.proposed_new_acct_prop_order_limit_ab)).ToString()),
						//p_param.acct_prop_order_limit_tr, p_param.proposed_new_acct_prop_order_limit_tr,
							p_param.acct_prop_order_limit_ab, (p_param.proposed_new_acct_prop_order_limit_tr == "" ? "0" : Convert.ToInt64(Convert.ToDecimal(p_param.proposed_new_acct_prop_order_limit_tr)).ToString()),
							p_param.acct_prop_order_limit_remarks_ab, p_param.proposed_new_acct_prop_order_limit_remarks_ab,
							p_param.acct_prop_order_limit_remarks_tr, p_param.proposed_new_acct_prop_order_limit_remarks_tr,
						/* End Code added by Billy Jay (04/23/2015) */

							p_param.acct_prop_credit_limit, p_param.proposed_new_acct_prop_credit_limit,
							p_param.acct_prop_credit_term, p_param.proposed_new_acct_prop_credit_term,
							p_param.acct_prop_credit_term_remarks, p_param.proposed_new_acct_prop_credit_term_remarks,
							p_param.acct_prop_credit_limit_remarks, p_param.proposed_new_acct_prop_credit_limit_remarks,
							p_param.acct_mw_price_code, p_param.proposed_new_acct_mw_price_code,
							p_param.acct_mw_price_desc, p_param.proposed_new_acct_mw_price_desc,
							p_param.acct_mw_price_commision_disc, p_param.proposed_new_acct_mw_price_commision_disc,
							p_param.acct_mw_price_remarks, p_param.proposed_new_acct_mw_price_remarks,
							p_param.acct_ww_price_code, p_param.proposed_new_acct_ww_price_code,
							p_param.acct_ww_price_desc, p_param.proposed_new_acct_ww_price_desc,
							p_param.acct_ww_price_commision_disc, p_param.proposed_new_acct_ww_price_commision_disc,
							p_param.acct_ww_price_remarks, p_param.proposed_new_acct_ww_price_remarks,
							p_param.acct_pwf_price_code, p_param.proposed_new_acct_pwf_price_code,
							p_param.acct_pwf_price_desc, p_param.proposed_new_acct_pwf_price_desc,
							p_param.acct_pwf_price_commision_disc, p_param.proposed_new_acct_pwf_price_commision_disc,
							p_param.acct_pwf_price_remarks, p_param.proposed_new_acct_pwf_price_remarks,
							p_param.acct_pwr_price_code, p_param.proposed_new_acct_pwr_price_code,
							p_param.acct_pwr_price_desc, p_param.proposed_new_acct_pwr_price_desc,
							p_param.acct_pwr_price_commision_disc, p_param.proposed_new_acct_pwr_price_commision_disc,
							p_param.acct_pwr_price_remarks, p_param.proposed_new_acct_pwr_price_remarks,
							p_param.acct_gw_price_code, p_param.proposed_new_acct_gw_price_code,
							p_param.acct_gw_price_desc, p_param.proposed_new_acct_gw_price_desc,
							p_param.acct_gw_price_commision_disc, p_param.proposed_new_acct_gw_price_commision_disc,
							p_param.acct_gw_price_remarks, p_param.proposed_new_acct_gw_price_remarks,
							p_param.acct_tw_price_code, p_param.proposed_new_acct_tw_price_code,
							p_param.acct_tw_price_desc, p_param.proposed_new_acct_tw_price_desc,
							p_param.acct_tw_price_commision_disc, p_param.proposed_new_acct_tw_price_commision_disc,
							p_param.acct_tw_price_remarks, p_param.proposed_new_acct_tw_price_remarks,

							p_param.acct_mz_price_code, p_param.proposed_new_acct_mz_price_code,
							p_param.acct_mz_price_desc, p_param.proposed_new_acct_mz_price_desc,
							p_param.acct_mz_price_commision_disc, p_param.proposed_new_acct_mz_price_commision_disc,
							p_param.acct_mz_price_remarks, p_param.proposed_new_acct_mz_price_remarks,

							p_param.acct_nw_price_code, p_param.proposed_new_acct_nw_price_code,
							p_param.acct_nw_price_desc, p_param.proposed_new_acct_nw_price_desc,
							p_param.acct_nw_price_commision_disc, p_param.proposed_new_acct_nw_price_commision_disc,
							p_param.acct_nw_price_remarks, p_param.proposed_new_acct_nw_price_remarks,

							p_param.acct_ec_price_code, p_param.proposed_new_acct_ec_price_code,
							p_param.acct_ec_price_desc, p_param.proposed_new_acct_ec_price_desc,
							p_param.acct_ec_price_commision_disc, p_param.proposed_new_acct_ec_price_commision_disc,
							p_param.acct_ec_price_remarks, p_param.proposed_new_acct_ec_price_remarks,

							p_param.acct_ecu_price_code, p_param.proposed_new_acct_ecu_price_code,
							p_param.acct_ecu_price_desc, p_param.proposed_new_acct_ecu_price_desc,
							p_param.acct_ecu_price_commision_disc, p_param.proposed_new_acct_ecu_price_commision_disc,
							p_param.acct_ecu_price_remarks, p_param.proposed_new_acct_ecu_price_remarks
						);

					if (RouteType != CCA.EDocumentChanges.none)
					{

						mt_trans.UpdateTo("proposedChangesCA",
						new Dictionary<string, object>()
						{
							{"acctName", p_param.proposed_new_acct_name}
							,{"acctOffcr", p_param.proposed_new_acct_officer}
							,{"territory", p_param.proposed_new_acct_territory}
							,{"area", p_param.proposed_acct_new_area}
							,{"region", p_param.proposed_new_acct_region}
							,{"regBusName", p_param.proposed_new_acct_reg_name}
							,{"bussAdd", p_param.proposed_new_acct_business_add}
							,{"delAdd", p_param.proposed_new_acct_delivery_add}

							/* Code added by Billy Jay (04/23/2015) */
							,{"propCredTermsArchitecturalBrand" , p_param.proposed_new_acct_prop_credit_term_architectural_brand}
							,{"propCredTermsEcoforLumber", p_param.proposed_new_acct_prop_credit_term_ecofor_lumber}
							,{"propCredTermsEcoforPlywood", p_param.proposed_new_acct_prop_credit_term_ecofor_plywood}

							,{"CredTermRemarksArchitecturalBrand", p_param.proposed_new_acct_credit_term_remarks_architectural_brand}
							,{"CredTermRemarksEcoforLumber", p_param.proposed_new_acct_credit_term_remarks_ecofor_lumber}
							,{"CredTermRemarksEcoforPlywood", p_param.proposed_new_acct_credit_term_remarks_ecofor_plywood}

							
							,{"probOrderLimit_AB",p_param.proposed_new_acct_prop_order_limit_ab==""?0: Convert.ToInt64(Convert.ToDecimal(p_param.proposed_new_acct_prop_order_limit_ab))}
							,{"probOrderLimit_TR",p_param.proposed_new_acct_prop_order_limit_tr==""?0: Convert.ToInt64(Convert.ToDecimal(p_param.proposed_new_acct_prop_order_limit_tr))}
							,{"OrderLimitRemarks_AB", p_param.proposed_new_acct_prop_order_limit_remarks_ab}
							,{"OrderLimitRemarks_TR", p_param.proposed_new_acct_prop_order_limit_remarks_tr}

							/* End Code added by Billy Jay (04/23/2015) */

							,{"propCredTerms", p_param.proposed_new_acct_prop_credit_term}
							,{"propCredLimit", p_param.proposed_new_acct_prop_credit_limit}
							,{"CredTermRemarks", p_param.proposed_new_acct_prop_credit_term_remarks}
							,{"CredLimitRemarks", p_param.proposed_new_acct_prop_credit_limit_remarks}
							,{"pl_priceListCode_mw", p_param.proposed_new_acct_mw_price_code}
							,{"pl_codeDesc_mw", p_param.proposed_new_acct_mw_price_desc}
							,{"pl_CommDisc_mw", p_param.proposed_new_acct_mw_price_commision_disc}
							,{"pl_remarks_mw", p_param.proposed_new_acct_mw_price_remarks}
							,{"pl_priceListCode_ww", p_param.proposed_new_acct_ww_price_code}
							,{"pl_codeDesc_ww", p_param.proposed_new_acct_ww_price_desc}
							,{"pl_CommDisc_ww", p_param.proposed_new_acct_ww_price_commision_disc}
							,{"pl_remarks_ww", p_param.proposed_new_acct_ww_price_remarks}
							,{"pl_priceListCode_pwf", p_param.proposed_new_acct_pwf_price_code}
							,{"pl_codeDesc_pwf", p_param.proposed_new_acct_pwf_price_desc}
							,{"pl_CommDisc_pwf", p_param.proposed_new_acct_pwf_price_commision_disc}
							,{"pl_remarks_pwf", p_param.proposed_new_acct_pwf_price_remarks}
							,{"pl_priceListCode_pwr", p_param.proposed_new_acct_pwr_price_code}
							,{"pl_codeDesc_pwr", p_param.proposed_new_acct_pwr_price_desc}
							,{"pl_CommDisc_pwr", p_param.proposed_new_acct_pwr_price_commision_disc}
							,{"pl_remarks_pwr", p_param.proposed_new_acct_pwr_price_remarks}
							,{"pl_priceListCode_gw", p_param.proposed_new_acct_gw_price_code}
							,{"pl_codeDesc_gw", p_param.proposed_new_acct_gw_price_desc}
							,{"pl_CommDisc_gw", p_param.proposed_new_acct_gw_price_commision_disc}
							,{"pl_remarks_gw", p_param.proposed_new_acct_gw_price_remarks}
							,{"pl_priceListCode_tw", p_param.proposed_new_acct_tw_price_code}
							,{"pl_codeDesc_tw", p_param.proposed_new_acct_tw_price_desc}
							,{"pl_CommDisc_tw", p_param.proposed_new_acct_tw_price_commision_disc}
							,{"pl_remarks_tw", p_param.proposed_new_acct_tw_price_remarks}

							 ,{"pl_priceListCode_mz", p_param.proposed_new_acct_mz_price_code}
							,{"pl_codeDesc_mz", p_param.proposed_new_acct_mz_price_desc}
							,{"pl_CommDisc_mz", p_param.proposed_new_acct_mz_price_commision_disc}
							,{"pl_remarks_mz", p_param.proposed_new_acct_mz_price_remarks}

							 ,{"pl_priceListCode_nw", p_param.proposed_new_acct_nw_price_code}
							,{"pl_codeDesc_nw", p_param.proposed_new_acct_nw_price_desc}
							,{"pl_CommDisc_nw", p_param.proposed_new_acct_nw_price_commision_disc}
							,{"pl_remarks_nw", p_param.proposed_new_acct_nw_price_remarks}

							 ,{"pl_priceListCode_ec", p_param.proposed_new_acct_ec_price_code}
							,{"pl_codeDesc_ec", p_param.proposed_new_acct_ec_price_desc}
							,{"pl_CommDisc_ec", p_param.proposed_new_acct_ec_price_commision_disc}
							,{"pl_remarks_ec", p_param.proposed_new_acct_ec_price_remarks}

							 ,{"pl_priceListCode_ecu", p_param.proposed_new_acct_ecu_price_code}
							,{"pl_codeDesc_ecu", p_param.proposed_new_acct_ecu_price_desc}
							,{"pl_CommDisc_ecu", p_param.proposed_new_acct_ecu_price_commision_disc}
							,{"pl_remarks_ecu", p_param.proposed_new_acct_ecu_price_remarks}

							,{"status", "1"}
							,{"changesType", ""}
							,{"routeType", RouteType.ToString()}
						}
						, new Dictionary<string, object>()
						{
							{"ccanum", p_param.acct_ccanum}
						});

					}

				}

				if (p_param.other_changes.Length > 0)
				{
					// removed
					// mt_trans.CommandText = "delete from proposedChangesCA1 where ccanum='" + p_param.acct_ccanum + "'";

					// insert to proposedChangesCA1 table
					mt_trans.InsertTo("proposedChangesCA1",
						new Dictionary<string, object>() { 
							{"ccanum", p_param.acct_ccanum}
							,{"OthrChanges", p_param.other_changes}
							,{"timestmp", DateTime.Now }
							,{"username", Session["username"].ToString().ToUpper() }
							,{"docstatus", current_document_status }
						}
					);
				}


				mt_trans.Committransaction();

				if (current_document_status == "1000")
				{
					MarkDocumentChanges("PROPOSE_CHANGES", p_param.acct_ccanum, p_param.apprver_remarks, p_param.other_changes);
				}
				else
				{
					MarkCustomerCreationDocument("APPROVE", p_param.acct_ccanum, "", p_param.acct_classification, "", CCA.EDocumentChanges.none, "SAVE_COMMAND");
				}

				return SActionResult.Success + p_param.acct_ccanum;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
		}
		
		private void SaveReclassedLeadCustomer(string requestid, string acct_ccanum)
		{
			SQLTransaction mt_trans = new SQLTransaction();

			try
			{
				// update the customerLeadI.ccaNum
				mt_trans.StartTransaction();

				mt_trans.UpdateTo("customerLeadI",
				new Dictionary<string, object>() { 
					{"ccaNum", acct_ccanum}
				}
				, new Dictionary<string, object>() {
					{"RequestId", requestid}
				});

				mt_trans.Committransaction();
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				throw new Exception("Failed updating..!");
			}
		}


		[AuthorizeUsr]
		[HttpPost]
		public string MarkCustomerCreationDocument(string action_type, string acct_ccanum, string remarks, string acct_classification = "", string final_acct_code = "", CCA.EDocumentChanges DocChangesRouteType = CCA.EDocumentChanges.none, string UPDATE_NEW_COMMAND = "")
		{
			DataTable proposedChangesCA = null;
			DataTable customerHeader = null;
			DataTable custBusHdr = null;
			SQLTransaction mt_trans = new SQLTransaction();
			CCA.EAccountClassification AccountClassF;
			CCA.EAccountType AccountType = CCA.EAccountType.DIRECT;

			string ch_next_docstatus = "";
			string ch_curr_docstatus = "", ch_doc_region = "", ch_acct_type = "", ch_skiprole1 = "", ch_curr_docstatus2 = "", cnt = "", approved = "";
			string pc_curr_docstatus = "";
			string ch_proposed_cred_limit = "";
			string ch_grp_name = "", ch_area = "";

			bool SkipASM = false;
			bool SkipCHM = false;

			// AMPERSAND
			remarks = StringHelper.ReCodeCharacters(remarks);
			#region CHECK IF FOUND IN SAP
			if (final_acct_code != "")
			{
				// check if already found in sap
				string str_qry = SqlQueryHelper.check_customer_code(final_acct_code, acct_ccanum);

				DataTable chk_tbl = SqlDbHelper.getDataDT(str_qry);
				foreach (DataRow itm in chk_tbl.Rows)
				{
					return SActionResult.Error + "'" + final_acct_code + "' IS NOT AVAILABLE!";
				}

				// if found rollback/ throw an exception

			}
			#endregion

			#region RETRIEVE_DATA

			customerHeader = SqlDbHelper.getDataDT("select *, isnull(SkipRoles1,'') as 'SkipRoles12', (select count(*) from document_history where docid=customerHeader.ccanum) as 'cnt' from customerHeader where ccanum='" + acct_ccanum + "'");
			proposedChangesCA = SqlDbHelper.getDataDT("select * from proposedChangesCA where ccanum='" + acct_ccanum + "'");
			custBusHdr = SqlDbHelper.getDataDT("select * from custbushdr where ccanum='" + acct_ccanum + "'");

			foreach (DataRow itm in customerHeader.Rows)
			{
				ch_curr_docstatus = itm["status"].ToString().Trim();
				ch_doc_region = StringHelper.GetRegion(itm["region"].ToString().Trim());
				ch_acct_type = itm["acctType"].ToString().Trim();
				ch_skiprole1 = itm["SkipRoles12"].ToString().Trim();
				cnt = itm["cnt"].ToString().Trim();
				ch_curr_docstatus2 = itm["Status2"].ToString().Trim();
				SkipASM = itm["skipASM"].ToString() == "Y" ? true : false;
				SkipCHM = itm["skipCHM"].ToString() == "Y" ? true : false;
				ch_area = itm["area"].ToString();

				approved = itm["Approved"].ToString();//added by francis 7/4/2017
			}

			foreach (DataRow itm in custBusHdr.Rows)
			{
				ch_proposed_cred_limit = itm["probCredLimit"].ToString();
			}

			if (ch_curr_docstatus == "" || ch_doc_region == "" || ch_acct_type == "")
			{
				return SActionResult.Error + "Error getting info.";
			}

			foreach (DataRow itm in proposedChangesCA.Rows)
			{
				pc_curr_docstatus = itm["status"].ToString().Trim();
			}



			// channelGroup
			DataTable channelGroup = SqlDbHelper.getDataDT("select grp_name from ChannelGroup where area  = '" + ch_area + "'");
			foreach (DataRow itm in channelGroup.Rows)
			{
				ch_grp_name = itm["grp_name"].ToString().Trim();
			}
			#endregion

			// GET ACCOUNT CLASSIFICATION
			if (acct_classification == CCA.EAccountClassification.WALKIN.ToString())
			{
				AccountClassF = CCA.EAccountClassification.WALKIN;
				if (action_type == "APPROVE") ch_next_docstatus = AppHelper.GetAccWalkInNextStep(ch_curr_docstatus);

				if (Convert.ToDouble(ch_proposed_cred_limit.Replace(",", "")) >= 100000 && ch_curr_docstatus == "1")
				{
					ch_next_docstatus = "7";
				}

			}
			else if (acct_classification == CCA.EAccountClassification.REGULAR.ToString())
			{
				AccountClassF = CCA.EAccountClassification.REGULAR;

				// GET ACCOUNT TYPE
				if (ch_acct_type.ToUpper() == "DIRECT")
				{
					AccountType = CCA.EAccountType.DIRECT;
					if (action_type == "APPROVE") ch_next_docstatus = AppHelper.GetAccCreationNextStep(ch_curr_docstatus);

					/* THIS IS TO SKIP THE VPBSM AND VPTFI IF CREDIT LIMIT < 100,001.00 */
					if (Convert.ToDouble(ch_proposed_cred_limit.Replace(",", "")) <= 100000 && ch_next_docstatus == "6")
					{
						ch_next_docstatus = "1008";
					}

					//SKIP ASM due to re organization of sales. KAO,KAM,RSM
					//Start
					if ((ch_grp_name == "GTL" || ch_grp_name == "GTV") && ch_next_docstatus == AppHelper.GetUserPositionId("asm"))
					{
						// send to chm
						ch_next_docstatus = AppHelper.GetUserPositionId("chm");
					}
					//End

					if (SkipASM == true && action_type == "APPROVE" && ch_next_docstatus == AppHelper.GetUserPositionId("asm"))
					{
						// move it to chm
						ch_next_docstatus = AppHelper.GetUserPositionId("chm");
					}

					if (SkipCHM == true && action_type == "APPROVE" && ch_next_docstatus == AppHelper.GetUserPositionId("chm"))
					{
						// move it to cnc
						ch_next_docstatus = AppHelper.GetUserPositionId("cnc");
					}
				}
				else
				{
					AccountType = CCA.EAccountType.INDIRECT;
					if (action_type == "APPROVE") ch_next_docstatus = AppHelper.GetAccIndirectCreationNextStep(ch_curr_docstatus);
				}
			}
			else
			{
				AccountClassF = CCA.EAccountClassification.NONE;
			}


			/* IF ACTION_TYPE=DISAPPROVE, MAKE IT NEGATIVE VALUE */
			if (action_type == "DISAPPROVE")
			{
				ch_next_docstatus = "-" + ch_curr_docstatus;
			}
			else if (action_type == "SEND_BACK_TO_REQUESTER")
			{
				ch_next_docstatus = "1";
			}
			else if (action_type == "SEND_BACK_TO_CNC")
			{
				ch_next_docstatus = AppHelper.GetUserPositionId("cnc");
			}   

			try
			{
				mt_trans.StartTransaction();

				mt_trans.UpdateTo("customerHeader", new Dictionary<string, object>() { { "status", ch_next_docstatus } }, new Dictionary<string, object>() { { "ccaNum", acct_ccanum } });

				// FOR ASM skipASM tagging
				if (ch_next_docstatus == AppHelper.GetUserPositionId("chm"))
				{
					mt_trans.UpdateTo("customerHeader", new Dictionary<string, object>() { { "skipASM", "Y" } }, new Dictionary<string, object>() { { "ccaNum", acct_ccanum } });
				}

				// FOR CHM skipCHM tagging
				if (ch_next_docstatus == AppHelper.GetUserPositionId("cnc"))
				{
					mt_trans.UpdateTo("customerHeader", new Dictionary<string, object>() { { "skipCHM", "Y" } }, new Dictionary<string, object>() { { "ccaNum", acct_ccanum } });
				}

				#region PARALLEL_MODIFICATION
				/*
					PARRALEL MODIFICATION - START
				*/

				/* STATUS2 */
				if (action_type != "DISAPPROVE")
				{
					if (action_type == "APPROVE")
					{

						/*
							SINCE Status2 does not contain anything, if customerHeader.Status='', then use customerHeader.Status
						*/
						if (ch_curr_docstatus2 == "")
						{
							ch_curr_docstatus2 = ch_curr_docstatus;
						}
						/*
							IF customerHeader.SkipRoles1 = 'Y' then skip the 2 and 3
						*/
						if (ch_skiprole1 == "Y" && CCA.DocumentCreation.NextRoute(AccountClassF, AccountType, Convert.ToInt32(ch_curr_docstatus2)) == "2")
						{
							/* USE 3, so the the CCA.DocumentCreation.NextRoute will return 4 */
							ch_curr_docstatus2 = "3";
						}

						/* GET NEXT STATUS NUMBER */
						string ch_next_docstatus_2 = CCA.DocumentCreation.NextRoute(AccountClassF, AccountType, Convert.ToInt32(ch_curr_docstatus2));
						mt_trans.CommandText = "update customerHeader set Status2='" + ch_next_docstatus_2 + "' where ccaNum='" + acct_ccanum + "'";

						/* CCA.DocumentCreation.NextRoute will return 1000 if the routing will end */
						if (ch_next_docstatus_2 == "1000")
						{
							/* APPROVED='Y' */
							mt_trans.CommandText = "update customerHeader set approved='Y' where ccaNum='" + acct_ccanum + "'";
						}
					}

					if (action_type == "SEND_BACK_TO_REQUESTER")
					{
						mt_trans.CommandText = "update customerHeader set Status2='1' where ccaNum='" + acct_ccanum + "'";
					}

				}

				/* CANCELED='Y' */
				if (action_type == "DISAPPROVE")
				{
					mt_trans.CommandText = "update customerHeader set Canceled='Y' where ccaNum='" + acct_ccanum + "'";
				}
				/*
					PARRALEL MODIFICATION - END
				*/
				#endregion

				if (final_acct_code != "")
				{
					mt_trans.UpdateTo("customerHeader",
						new Dictionary<string, object>() { { "SapAcctCode", final_acct_code } },
						new Dictionary<string, object>() { { "ccaNum", acct_ccanum } });
				}

				mt_trans.Committransaction();

				// SEND MAIL
				SendMail(ch_doc_region, ch_next_docstatus, acct_ccanum, "");

				string new_action_type = "";

				// WORKAROUND
				if (ch_curr_docstatus == "1")
				{
					// if (ch_skiprole1 == "Y")
					if (Convert.ToInt32(cnt) > 1)
					{
						new_action_type = "UPDATE";
					}
					else
					{
						if (UPDATE_NEW_COMMAND == "SAVE_COMMAND")
						{
							new_action_type = "UPDATE";
						}
						else
						{
							new_action_type = "NEW_CUSTOMER";
						}
					}
				}
				else
				{
					new_action_type = action_type;
				}


				// SAVE TO LOG
				AppHelper.SaveToLOg("CCA", acct_ccanum, new_action_type, remarks, Session["username"].ToString(), ch_next_docstatus, ch_curr_docstatus, "CUSTOMER_CREATION");

				// IF APPROVED... ADD CCA TO CUSTSAPUPDATE
				if (ch_next_docstatus == "1000")
				{
					// UPDATE IN SAP
					UPDATE_IN_SAP(acct_ccanum);
				}

				return SActionResult.Success;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
		}


		[AuthorizeUsr]
		[HttpPost]
		public string SaveCreditInvestigationInfo(SkelClass.page_param.customer_savecreditinvestigationinfo p_param)
		{
			p_param.StripNull();
			SQLTransaction mt_trans = new SQLTransaction();

			try
			{
				mt_trans.StartTransaction();

				// depositoryBank
				mt_trans.DeleteFrom("depositoryBank", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } });
				if (p_param.list_of_bank.Count > 0)
				{
					foreach (string strn in p_param.list_of_bank)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("depositoryBank",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"bankName", tmp_str[0]}
							,{"branch", tmp_str[1]}
							,{"address", tmp_str[2]}
							,{"account", tmp_str[3]}
							,{"contactPerson", tmp_str[4]}
							,{"contactNumber", tmp_str[5]}
							,{"aveDeposit", tmp_str[6]}
							,{"remarks", tmp_str[7]}
						});
					}
				}

				// assets - land
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "land" } });
				if (p_param.list_of_land.Count > 0)
				{
					foreach (string strn in p_param.list_of_land)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("assets",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"assetClass", "land"}
							,{"Type", tmp_str[0]}
							,{"area", tmp_str[1]}
							,{"location", tmp_str[2]}
							,{"owner", tmp_str[3]}
							,{"quantity", null}
						});
					}
				}

				// assets - building
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "building" } });
				if (p_param.list_of_building.Count > 0)
				{
					foreach (string strn in p_param.list_of_building)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("assets",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"assetClass", "building"}
							,{"Type", tmp_str[0]}
							,{"area", tmp_str[1]}
							,{"location", tmp_str[2]}
							,{"owner", tmp_str[3]}
							,{"quantity", null}
						});
					}
				}

				// assets - vehicle
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "vehicles" } });
				if (p_param.list_of_vehicle.Count > 0)
				{
					foreach (string strn in p_param.list_of_vehicle)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("assets",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"otherAssets", ""}
							,{"assetClass", "vehicles"}
							,{"Type", tmp_str[0]}
							,{"model", tmp_str[1]}
							,{"quantity", Convert.ToInt32(tmp_str[2])}
						});
					}
				}

				// otherbusiness
				mt_trans.DeleteFrom("otherBusiness", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } });
				if (p_param.list_of_assets.Count > 0)
				{
					foreach (string strn in p_param.list_of_assets)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("otherBusiness",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"regName", tmp_str[0]}
							,{"nature", tmp_str[1]}
							,{"location", tmp_str[2]}
							,{"percentOwnership", Convert.ToDecimal(tmp_str[3])}
						});
					}
				}

				// other asset
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "other" } });
				if (p_param.acct_other_assets != "")
				{
					mt_trans.InsertTo("assets",
					new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
						,{"acctCode", p_param.acct_code}
						,{"otherAssets", p_param.acct_other_assets}
						,{"assetClass", "other"}
					});
				}

				// custCredInves
				mt_trans.DeleteFrom("custCredInves", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } });
				mt_trans.InsertTo("custCredInves",
				new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum}
					,{"CIBI_remarks", p_param.acct_cibi_remarks}
					,{"SupplyInfo_remarks", p_param.acct_supplyinfo_remarks}
				});

				mt_trans.Committransaction();
				return SActionResult.Success + p_param.acct_ccanum;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
		}

		[AuthorizeUsr]
		[HttpPost]
		public string SaveEditedInfo(SkelClass.page_param.customer_saveeditedinfo p_param)
		{
			p_param.StripNull();
			SQLTransaction mt_trans = new SQLTransaction();

			// CHECK IF PROPOSEDCHANGESCA.ROUTETYPE IS NOT NULL OR != ''
			string str_route_type = "";
			System.Data.DataTable RouteTypeTBL = SqlDbHelper.getDataDT("select routetype from proposedchangesca where ccanum='" + p_param.acct_ccanum + "'");
			foreach (System.Data.DataRow itm in RouteTypeTBL.Rows)
			{
				str_route_type = itm["routetype"].ToString();
			}

			// OLD VALUE OF CREDIT LIMIT AND CREDIT TERMS
			string str_prev_credlimit = "", str_prev_credterms = "", str_prev_credterms_architectural_brand = "", str_prev_credterms_ecofor_lumber = "", str_prev_credterms_ecofor_plywood = "", str_prev_orderlimit_ab = "", str_prev_orderlimit_tr = "";
			System.Data.DataTable prevReditLT = null;
			if (str_route_type != "")
			{
				prevReditLT = SqlDbHelper.getDataDT("select propCredTerms as 'propCredTerms', propCredLimit as 'probCredLimit', propCredTermsArchitecturalBrand as 'propCredTermsArchitecturalBrand', propCredTermsEcoforLumber as 'propCredTermsEcoforLumber', propCredTermsEcoforPlywood as 'propCredTermsEcoforPlywood', probOrderLimit_AB as 'probOrderLimit_AB', probOrderLimit_TR as 'probOrderLimit_TR' from proposedchangesca where ccanum='" + p_param.acct_ccanum + "'");
			}
			else
			{
				prevReditLT = SqlDbHelper.getDataDT("select propCredTerms, probCredLimit, propCredTermsArchitecturalBrand, propCredTermsEcoforLumber, propCredTermsEcoforPlywood, probOrderLimit_AB , probOrderLimit_TR  from custbushdr where ccanum='" + p_param.acct_ccanum + "'");
			}

			foreach (System.Data.DataRow itm in prevReditLT.Rows)
			{
				str_prev_credlimit = itm["probCredLimit"].ToString();
				str_prev_credterms = itm["propCredTerms"].ToString();

				/* Code added by Billy Jay (04/23/2015) */
				str_prev_orderlimit_ab = itm["probOrderLimit_AB"].ToString();
				str_prev_orderlimit_tr = itm["probOrderLimit_TR"].ToString();

				str_prev_credterms_architectural_brand = itm["propCredTermsArchitecturalBrand"].ToString();
				str_prev_credterms_ecofor_lumber = itm["propCredTermsEcoforLumber"].ToString();
				str_prev_credterms_ecofor_plywood = itm["propCredTermsEcoforPlywood"].ToString();
				/* End Code added by Billy Jay (04/23/2015) */
			}

			try
			{
				mt_trans.StartTransaction();

				// depositoryBank
				mt_trans.DeleteFrom("depositoryBank", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } });
				if (p_param.list_of_bank.Count > 0)
				{
					foreach (string strn in p_param.list_of_bank)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("depositoryBank",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"bankName", tmp_str[0]}
							,{"branch", tmp_str[1]}
							,{"address", tmp_str[2]}
							,{"account", tmp_str[3]}
							,{"contactPerson", tmp_str[4]}
							,{"contactNumber", tmp_str[5]}
							,{"aveDeposit", tmp_str[6]}
							,{"remarks", tmp_str[7]}
						});
					}
				}

				// assets - land
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "land" } });
				if (p_param.list_of_land.Count > 0)
				{
					foreach (string strn in p_param.list_of_land)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("assets",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"assetClass", "land"}
							,{"Type", tmp_str[0]}
							,{"area", tmp_str[1]}
							,{"location", tmp_str[2]}
							,{"owner", tmp_str[3]}
						});
					}
				}

				// assets - building
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "building" } });
				if (p_param.list_of_building.Count > 0)
				{
					foreach (string strn in p_param.list_of_building)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("assets",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"assetClass", "building"}
							,{"Type", tmp_str[0]}
							,{"area", tmp_str[1]}
							,{"location", tmp_str[2]}
							,{"owner", tmp_str[3]}
						});
					}
				}

				// assets - vehicle
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "vehicles" } });
				if (p_param.list_of_vehicle.Count > 0)
				{
					foreach (string strn in p_param.list_of_vehicle)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("assets",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"assetClass", "vehicles"}
							,{"Type", tmp_str[0]}
							,{"model", tmp_str[1]}
							,{"quantity", Convert.ToInt32(tmp_str[2])}
						});
					}
				}

				// otherbusiness
				mt_trans.DeleteFrom("otherBusiness", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } });
				if (p_param.list_of_assets.Count > 0)
				{
					foreach (string strn in p_param.list_of_assets)
					{
						string[] tmp_str = strn.Split('|');
						mt_trans.InsertTo("otherBusiness",
						new Dictionary<string, object>() { 
							{"ccaNum", p_param.acct_ccanum}
							,{"acctCode", p_param.acct_code}
							,{"regName", tmp_str[0]}
							,{"nature", tmp_str[1]}
							,{"location", tmp_str[2]}
							,{"percentOwnership", Convert.ToDecimal(tmp_str[3])}
						});
					}
				}

				// other asset
				mt_trans.DeleteFrom("assets", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum }, { "assetClass", "other" } });
				if (p_param.acct_other_assets != "")
				{
					mt_trans.InsertTo("assets",
					 new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
						,{"acctCode", p_param.acct_code}
						,{"otherAssets", p_param.acct_other_assets}
						,{"assetClass", "other"}
					});
				}

				// custCredInves
				mt_trans.DeleteFrom("custCredInves", new Dictionary<string, object>() { { "ccaNum", p_param.acct_ccanum } });
				mt_trans.InsertTo("custCredInves",
				new Dictionary<string, object>() { 
					{"ccaNum", p_param.acct_ccanum}
					,{"CIBI_remarks", p_param.acct_cibi_remarks}
					,{"SupplyInfo_remarks", p_param.acct_supplyinfo_remarks}
				});


				// CHECK IF CUSTOMER_CREATION OR CUSTOMER_CHANGES
				// CREDIT LIMIT, CREDIT TERMS
				// NEEDS MORE CHECKING

				if (str_route_type != "")
				{
					// IF CUSTOMER_CHANGES
					mt_trans.UpdateTo("proposedchangesca",
					new Dictionary<string, object>() { 
						{"propCredTerms", p_param.acct_prop_credit_term},
						{"propCredLimit", p_param.acct_prop_credit_limit},
						 /* Code added by Billy Jay (04/23/2015) */ 
						 
						{"probOrderLimit_AB", p_param.acct_prob_order_limit_ab == "" ? 0 : Convert.ToInt64(Convert.ToDecimal(p_param.acct_prob_order_limit_ab)) },
						{"probOrderLimit_TR", p_param.acct_prob_order_limit_tr == "" ? 0 : Convert.ToInt64(Convert.ToDecimal(p_param.acct_prob_order_limit_tr)) },

						 {"propCredTermsArchitecturalBrand", p_param.acct_prop_credit_terms_architectural_brand},
						 {"propCredTermsEcoforLumber", p_param.acct_prop_credit_terms_ecofor_lumber},
						 {"propCredTermsEcoforPlywood", p_param.acct_prop_credit_terms_ecofor_plywood}
						 /* End Code added by Billy Jay (04/23/2015 */
					}, new Dictionary<string, object>() { 
						{"ccanum", p_param.acct_ccanum}
					});
				}
				else
				{
					// IF CUSTOMER_CREATION
					mt_trans.UpdateTo("custBusHdr",
					new Dictionary<string, object>() { 
						{"propCredTerms", p_param.acct_prop_credit_term},

						{"probCredLimit", p_param.acct_prop_credit_limit},
						 /* Code added by Billy Jay (04/23/2015) */
						{"probOrderLimit_AB", p_param.acct_prob_order_limit_ab == "" ? 0 : Convert.ToInt64(Convert.ToDecimal(p_param.acct_prob_order_limit_ab)) },
						{"probOrderLimit_TR", p_param.acct_prob_order_limit_tr == "" ? 0 : Convert.ToInt64(Convert.ToDecimal(p_param.acct_prob_order_limit_tr)) },

						 {"propCredTermsArchitecturalBrand" ,p_param.acct_prop_credit_terms_architectural_brand},
						 {"propCredTermsEcoforLumber", p_param.acct_prop_credit_terms_ecofor_lumber},
						 {"propCredTermsEcoforPlywood", p_param.acct_prop_credit_terms_ecofor_plywood}
						 /* End Code added by Billy Jay (04/23/2015) */
					}, new Dictionary<string, object>() { 
						{"ccaNum", p_param.acct_ccanum}
					});
				}

				// save the changes
				mt_trans.InsertTo("proposedChangesCA1", new Dictionary<string, object>() { 
					{"ccanum", p_param.acct_ccanum }
					,{"OthrChanges", p_param.str_changes}
					,{"timestmp", DateTime.Now}
					,{"username", Session["username"].ToString().ToUpper()}
				});

				mt_trans.Committransaction();
				return SActionResult.Success + p_param.acct_ccanum;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
		}

		[AuthorizeUsr]
		[HttpPost]
		[ParseUrl]
		public string MarkDocumentChanges(string action_type, string acct_ccanum, string remarks, string other_changes = "")
		{
			SQLTransaction mt_trans = new SQLTransaction();
			string doc_region = "", cur_docstatus = "", route_type = "", new_docstatus = "", formmated_msg_to_embed = "", is_sent_back = "", ch_proposed_cred_limit = "";
			string c_status = "", acct_code = "", area = "", grp_name = "";

			bool update_to_sap = false;

			bool SkipASM = false;
			bool SkipCHM = false;

			// customerHeader
			DataTable customerHeader = SqlDbHelper.getDataDT("select * from customerheader where ccanum  = '" + acct_ccanum + "'");
			foreach (DataRow itm in customerHeader.Rows)
			{
				c_status = itm["status"].ToString().Trim();
				acct_code = itm["acctCode"].ToString();
			}


			// proposedChangesCA
			DataTable proposedChangesCA = SqlDbHelper.getDataDT("select * from dbo.proposedChangesCA where ccaNum='" + acct_ccanum + "'");
			DataRow proposedChangesCA_row = proposedChangesCA.Rows[0];

			foreach (DataRow item in proposedChangesCA.Rows)
			{
				doc_region = item["region"].ToString().Trim();
				cur_docstatus = item["status"].ToString().Trim();
				route_type = item["routeType"].ToString().Trim();
				is_sent_back = item["is_sent_back"].ToString().Trim();
				ch_proposed_cred_limit = item["propCredLimit"].ToString().Trim();
				SkipASM = item["SkipASM"].ToString() == "Y" ? true : false;
				SkipCHM = item["SkipCHM"].ToString() == "Y" ? true : false;
				area = item["area"].ToString();
			}

			// channelGroup
			DataTable channelGroup = SqlDbHelper.getDataDT("select grp_name from ChannelGroup where area  = '" + area + "'");
			foreach (DataRow itm in channelGroup.Rows)
			{
				grp_name = itm["grp_name"].ToString().Trim();
			}


			if (route_type == "route_1")
			{
				// if send back to requestor
				new_docstatus = action_type ==
					CCA.EDocActionType.SEND_BACK_TO_REQUESTER.ToString() ? "1" : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_1, cur_docstatus);

				// if sendback to cnc
				new_docstatus = action_type ==
					CCA.EDocActionType.SEND_BACK_TO_CNC.ToString() ? "1" : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_1, cur_docstatus);

				//SKIP ASM due to re organization of sales. KAO,KAM,RSM
				//Start
				if ((grp_name == "GTL" || grp_name == "GTV") && new_docstatus == AppHelper.GetUserPositionId("asm"))
				{
					// send to chm
					new_docstatus = AppHelper.GetUserPositionId("chm");
				}
				//End

				#region SKIP_ASM_CHM
				if (SkipASM == true && new_docstatus == AppHelper.GetUserPositionId("asm"))
				{
					// send to chm
					new_docstatus = AppHelper.GetUserPositionId("chm");
				}

				if (SkipCHM == true && new_docstatus == AppHelper.GetUserPositionId("chm"))
				{
					// send to chm
					new_docstatus = AppHelper.GetUserPositionId("vpbsm");
				}
				#endregion

				#region SKIP_VPBSM_VPTFI
				if (Convert.ToDouble(ch_proposed_cred_limit.Replace(",", "")) <= 100000 && new_docstatus == AppHelper.GetUserPositionId("vpbsm"))
				{
					new_docstatus = AppHelper.GetUserPositionId("cnc");
				}

				if (Convert.ToDouble(ch_proposed_cred_limit.Replace(",", "")) <= 100000 && new_docstatus == AppHelper.GetUserPositionId("vptfi"))
				{
					new_docstatus = "1000";
				}
				#endregion

			}

			if (route_type == "route_2")
			{
				// if send back to requestor
				new_docstatus = action_type ==
					CCA.EDocActionType.SEND_BACK_TO_REQUESTER.ToString() ? "1" : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_2, cur_docstatus);

				// if sendback to cnc
				new_docstatus = action_type ==
					CCA.EDocActionType.SEND_BACK_TO_CNC.ToString() ? AppHelper.GetUserPositionId("cnc") : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_2, cur_docstatus);

				//SKIP ASM due to re organization of sales. KAO,KAM,RSM
				//Start
				if ((grp_name == "GTL" || grp_name == "GTV") && new_docstatus == AppHelper.GetUserPositionId("asm"))
				{
					// send to chm
					new_docstatus = AppHelper.GetUserPositionId("chm");
				}
				//End
			}

			if (route_type == "route_3")
			{
				// if send back to requestor
				new_docstatus = action_type ==
					CCA.EDocActionType.SEND_BACK_TO_REQUESTER.ToString() ? "1" : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_3, cur_docstatus);

				// if sendback to cnc
				new_docstatus = action_type ==
					CCA.EDocActionType.SEND_BACK_TO_CNC.ToString() ? AppHelper.GetUserPositionId("cnc") : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_3, cur_docstatus);

				#region SKIP_VPTFI

				if (Convert.ToDouble(ch_proposed_cred_limit.Replace(",", "")) <= 100000 && new_docstatus == AppHelper.GetUserPositionId("vptfi"))
				{
					new_docstatus = "1000";
				}
				#endregion

			}

            if (route_type == "route_4")
            {
                new_docstatus = action_type ==
                    CCA.EDocActionType.SEND_BACK_TO_REQUESTER.ToString() ? "1" : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_4, cur_docstatus);

                // if sendback to cnc
                new_docstatus = action_type ==
                    CCA.EDocActionType.SEND_BACK_TO_CNC.ToString() ? AppHelper.GetUserPositionId("cnc") : CCA.DocumentChanges.NextRoute(CCA.EDocumentChanges.route_4, cur_docstatus);
            }


			string branch = doc_region.ToUpper().IndexOf("LUZON") > -1 ? "Luzon" : "Vismin";
			string disapprove_docstatus = (Convert.ToInt32(cur_docstatus) * -1).ToString();

			// do not continue if no routing to do
			if (route_type == "")
			{
				if (other_changes != "") new_docstatus = "1";
				else return "03: No Route type specified!";
			}

			try
			{

				mt_trans.StartTransaction();

				// if send back to requestor
				if (action_type == CCA.EDocActionType.SEND_BACK_TO_REQUESTER.ToString())
				{
					mt_trans.UpdateTo("proposedChangesCA",
						new Dictionary<string, object>() { 
							{ "status", "1" }, { "is_sent_back", 1 } 
						}, new Dictionary<string, object>() { 
							{ "ccaNum", acct_ccanum } 
					});

					new_docstatus = AppHelper.GetUserPositionId("csr");
				}

				// if send back to cnc
				if (action_type == CCA.EDocActionType.SEND_BACK_TO_CNC.ToString())
				{
					mt_trans.UpdateTo("proposedChangesCA",
						new Dictionary<string, object>() { 
							{ "status", AppHelper.GetUserPositionId("cnc") }, { "is_sent_back", 1 } 
						}, new Dictionary<string, object>() { 
							{ "ccaNum", acct_ccanum } 
					});

					new_docstatus = AppHelper.GetUserPositionId("cnc");
				}

				if (action_type == CCA.EDocActionType.APPROVE.ToString() || action_type == CCA.EDocActionType.PROPOSE_CHANGES.ToString())
				{
					mt_trans.UpdateTo("proposedChangesCA",
						new Dictionary<string, object>() { 
							{"status", new_docstatus}, {"is_sent_back", 0} 
						}, new Dictionary<string, object>() { 
							{"ccaNum", acct_ccanum}
					});

					#region MARK_SKIPASM_SKIPCHM
					// if chm, the mark skipASM = true
					if (route_type == "route_1")
					{
						if (new_docstatus == AppHelper.GetUserPositionId("chm"))
						{
							mt_trans.UpdateTo("proposedChangesCA",
								new Dictionary<string, object>() { { "SkipASM", "Y" } },
								new Dictionary<string, object>() { {"ccaNum", acct_ccanum}
							});
						}

						if (new_docstatus == AppHelper.GetUserPositionId("vpbsm") || new_docstatus == AppHelper.GetUserPositionId("cnc"))
						{
							mt_trans.UpdateTo("proposedChangesCA",
								new Dictionary<string, object>() { { "SkipCHM", "Y" } },
								new Dictionary<string, object>() { {"ccaNum", acct_ccanum}
							});
						}
					}

					#endregion
				}

				if (action_type == CCA.EDocActionType.DISAPPROVE.ToString())
				{
					#region DISAPPROVE
					mt_trans.UpdateTo("proposedChangesCA", new Dictionary<string, object>() { 
						{"acctName", "" }
						,{"acctOffcr", "" }
						,{"territory", "" },{"area", "" },{"region", "" }
						,{"regBusName", "" }
						,{"bussAdd", "" },{"delAdd", "" }
						/* Code added by Billy Jay (04/23/2015) */
						,{"propCredTermsArchitecturalBrand", "" },{"propCredTermsEcoforLumber", "" },{"propCredTermsEcoforPlywood", "" }
						,{"CredTermRemarksArchitecturalBrand", "" },{"CredTermRemarksEcoforLumber", "" },{"CredTermRemarksEcoforPlywood", "" }
						,{"probOrderLimit_AB", "" },{"probOrderLimit_TR", "" }
						,{"OrderLimitRemarks_AB", "" },{"OrderLimitRemarks_TR", "" }
						/* End Code added by Billy Jay (04/23/2015)*/
						,{"propCredTerms", "" },{"propCredLimit", "" }
						,{"CredTermRemarks", "" },{"CredLimitRemarks", "" }
						,{"pl_priceListCode_mw", "" },{"pl_codeDesc_mw", "" },{"pl_CommDisc_mw", "" },{"pl_remarks_mw", "" }
						,{"pl_priceListCode_ww", "" },{"pl_codeDesc_ww", "" },{"pl_CommDisc_ww", "" },{"pl_remarks_ww", "" }
						,{"pl_priceListCode_pwf", "" },{"pl_codeDesc_pwf", "" },{"pl_CommDisc_pwf", "" },{"pl_remarks_pwf", "" }
						,{"pl_priceListCode_pwr", "" },{"pl_codeDesc_pwr", "" },{"pl_CommDisc_pwr", "" },{"pl_remarks_pwr", "" }
						,{"pl_priceListCode_gw", "" },{"pl_codeDesc_gw", "" },{"pl_CommDisc_gw", "" },{"pl_remarks_gw", "" }
						,{"pl_priceListCode_tw", "" },{"pl_codeDesc_tw", "" },{"pl_CommDisc_tw", "" },{"pl_remarks_tw", "" }

						,{"pl_priceListCode_mz", "" },{"pl_codeDesc_mz", "" },{"pl_CommDisc_mz", "" },{"pl_remarks_mz", "" }
						,{"pl_priceListCode_nw", "" },{"pl_codeDesc_nw", "" },{"pl_CommDisc_nw", "" },{"pl_remarks_nw", "" }
						,{"pl_priceListCode_ec", "" },{"pl_codeDesc_ec", "" },{"pl_CommDisc_ec", "" },{"pl_remarks_ec", "" }
						,{"pl_priceListCode_ecu", "" },{"pl_codeDesc_ecu", "" },{"pl_CommDisc_ecu", "" },{"pl_remarks_ecu", "" }
						,{"status", "1" }
						,{"changesType", "" },{"routeType", "" }
						,{"SkipASM", null },{"SkipCHM", null }
					}, new Dictionary<string, object>() { 
						{"ccanum", acct_ccanum }
					});

					mt_trans.UpdateTo("proposedChangesCA",
						new Dictionary<string, object>() { 
							{"is_sent_back", 0}
						}, new Dictionary<string, object>() { 
							{"ccaNum", acct_ccanum}
					});
					#endregion
				}


				if (new_docstatus == "1000" && action_type != CCA.EDocActionType.DISAPPROVE.ToString())
				{
					#region APPROVE
					// save the changes to customerHeader
					mt_trans.CommandText = "" +
						"UPDATE customerHeader " +
						"SET acctName = '" + StringHelper.ReCodeCharacters(proposedChangesCA_row["acctName"].ToString()) + "' " +
						",acctOffcr = '" + proposedChangesCA_row["acctOffcr"].ToString() + "' " +
						",territory = '" + proposedChangesCA_row["territory"].ToString() + "' " +
						",area = '" + proposedChangesCA_row["area"].ToString() + "' " +
						",region = '" + proposedChangesCA_row["region"].ToString() + "' " +
						",regBusName = '" + StringHelper.ReCodeCharacters(proposedChangesCA_row["regBusName"].ToString()) + "' " +
						",bussAdd = '" + StringHelper.ReCodeCharacters(proposedChangesCA_row["bussAdd"].ToString()) + "' " +
						",delAdd = '" + StringHelper.ReCodeCharacters(proposedChangesCA_row["delAdd"].ToString()) + "' " +
						"WHERE ccaNum = '" + acct_ccanum + "' " +
						"";

					// save to custBusHdr
					mt_trans.TryInsertUpdate("custBusHdr",
					new Dictionary<string, object>()
					{
						{"busTermsID", 0}
						,{"acctCode", acct_code}
						/* Code added by Billy Jay (04/23/2015) */
						,{"propCredTermsArchitecturalBrand", proposedChangesCA_row["propCredTermsArchitecturalBrand"].ToString()}
						,{"propCredTermsEcoforLumber", proposedChangesCA_row["propCredTermsEcoforLumber"].ToString()}
						,{"propCredTermsEcoforPlywood", proposedChangesCA_row["propCredTermsEcoforPlywood"].ToString()}
						,{"CredTermRemarksArchitecturalBrand", proposedChangesCA_row["CredTermRemarksArchitecturalBrand"].ToString() }
						,{"CredTermRemarksEcoforLumber", proposedChangesCA_row["CredTermRemarksEcoforLumber"].ToString() }
						,{"CredTermRemarksEcoforPlywood", proposedChangesCA_row["CredTermRemarksEcoforPlywood"].ToString() }

						,{"CredTermRemarks", proposedChangesCA_row["CredTermRemarks"].ToString()}
						,{"CredLimitRemarks", proposedChangesCA_row["CredLimitRemarks"].ToString()}
						
						,{"probOrderLimit_AB", proposedChangesCA_row["probOrderLimit_AB"].ToString()}
						,{"probOrderLimit_TR", proposedChangesCA_row["probOrderLimit_TR"].ToString()}
						
						,{"OrderLimitRemarks_AB", proposedChangesCA_row["OrderLimitRemarks_AB"].ToString()}
						,{"OrderLimitRemarks_TR", proposedChangesCA_row["OrderLimitRemarks_TR"].ToString()}


						/* End Code added by Billy Jay (04/23/2015) */
						,{"propCredTerms", proposedChangesCA_row["propCredTerms"].ToString()}
						,{"probCredLimit", proposedChangesCA_row["propCredLimit"].ToString()}
					}
					, new Dictionary<string, object>() { {"ccaNum", acct_ccanum }
					});

					// MW
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_mw"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_mw"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_mw"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_mw"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_mw"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "MW" }
						}
					);

					// WW
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_ww"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_ww"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_ww"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_ww"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_ww"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "WW" }
						}
					);

					// PWF
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_pwf"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_pwf"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_pwf"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_pwf"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_pwf"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "PWF" }
						}
					);

					// PWR
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_pwr"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_pwr"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_pwr"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_pwr"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_pwr"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "PWR" }
						}
					);

					// GW
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_gw"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_gw"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_gw"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_gw"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_gw"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "GW" }
						}
					);

					// TW
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_tw"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_tw"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_tw"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_tw"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_tw"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "TW" }
						}
					);

					// MZ
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_mz"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_mz"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_mz"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_mz"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_mz"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "MZ" }
						}
					);

					// NW
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_nw"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_nw"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_nw"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_nw"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_nw"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "NW" }
						}
					);

					// EC
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_ec"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_ec"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_ec"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_ec"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_ec"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "EC" }
						}
					);

					// ECU
					mt_trans.TryInsertUpdate("propsedPrice",
						new Dictionary<string, object>()
						{
							{"busTermsID", 0 }
							,{"priceListCode", proposedChangesCA_row["pl_priceListCode_ecu"].ToString() }
							,{"codeDesc", proposedChangesCA_row["pl_codeDesc_ecu"].ToString() }
							,{"remarks", proposedChangesCA_row["pl_remarks_ecu"].ToString() }
							,{"CommisionDiscounts", proposedChangesCA_row["pl_CommDisc_ecu"].ToString() != "" ? Convert.ToDecimal(proposedChangesCA_row["pl_CommDisc_ecu"].ToString()) : (decimal?)null }
						}
						, new Dictionary<string, object>()
						{
							{"ccaNum", acct_ccanum}
							,{"brandType", "ECU" }
						}
					);

					mt_trans.UpdateTo("proposedChangesCA", new Dictionary<string, object>() { 
						{"acctName", "" }
						,{"acctOffcr", "" }
						,{"territory", "" },{"area", "" },{"region", "" }
						,{"regBusName", "" }
						,{"bussAdd", "" },{"delAdd", "" }
						/* Code added by Billy Jay (04/23/2015) */

						,{"propCredTermsArchitecturalBrand", "" },{"propCredTermsEcoforLumber", "" },{"propCredTermsEcoforPlywood", "" }
						,{"CredTermRemarksArchitecturalBrand", "" },{"CredTermRemarksEcoforLumber", "" },{"CredTermRemarksEcoforPlywood", "" }
						,{"probOrderLimit_AB", "" },{"probOrderLimit_TR", "" }
						,{"OrderLimitRemarks_AB", "" },{"OrderLimitRemarks_TR", "" }
						/* End Code added by Billy Jay (04/23/2015) */
						,{"propCredTerms", "" },{"propCredLimit", "" }
						,{"CredTermRemarks", "" },{"CredLimitRemarks", "" }
						,{"pl_priceListCode_mw", "" },{"pl_codeDesc_mw", "" },{"pl_CommDisc_mw", "" },{"pl_remarks_mw", "" }
						,{"pl_priceListCode_ww", "" },{"pl_codeDesc_ww", "" },{"pl_CommDisc_ww", "" },{"pl_remarks_ww", "" }
						,{"pl_priceListCode_pwf", "" },{"pl_codeDesc_pwf", "" },{"pl_CommDisc_pwf", "" },{"pl_remarks_pwf", "" }
						,{"pl_priceListCode_pwr", "" },{"pl_codeDesc_pwr", "" },{"pl_CommDisc_pwr", "" },{"pl_remarks_pwr", "" }
						,{"pl_priceListCode_gw", "" },{"pl_codeDesc_gw", "" },{"pl_CommDisc_gw", "" },{"pl_remarks_gw", "" }
						,{"pl_priceListCode_tw", "" },{"pl_codeDesc_tw", "" },{"pl_CommDisc_tw", "" },{"pl_remarks_tw", "" }

						,{"pl_priceListCode_mz", "" },{"pl_codeDesc_mz", "" },{"pl_CommDisc_mz", "" },{"pl_remarks_mz", "" }
						,{"pl_priceListCode_nw", "" },{"pl_codeDesc_nw", "" },{"pl_CommDisc_nw", "" },{"pl_remarks_nw", "" }
						,{"pl_priceListCode_ec", "" },{"pl_codeDesc_ec", "" },{"pl_CommDisc_ec", "" },{"pl_remarks_ec", "" }
						,{"pl_priceListCode_ecu", "" },{"pl_codeDesc_ecu", "" },{"pl_CommDisc_ecu", "" },{"pl_remarks_ecu", "" }
						,{"status", "1" }
						,{"changesType", "" },{"routeType", "" }
						,{"SkipASM", null },{"SkipCHM", null }
					}, new Dictionary<string, object>() { 
						{"ccanum", acct_ccanum }
					});

					mt_trans.UpdateTo("proposedChangesCA", new Dictionary<string, object>() { 
						{"hasmodified", 1 }
					}, new Dictionary<string, object>() { 
						{"ccanum", acct_ccanum }
					});
					#endregion

					update_to_sap = true;
				}

				mt_trans.Committransaction();

				try
				{
					/*
						ALWAYS SEND THE CHANGES
						changes are stored in proposedChangesCA1
					*/
					DataTable otherChanges;
					string other_changes_from_db = "";

					if (other_changes == "")
					{
						otherChanges = SqlDbHelper.getDataDT("select OthrChanges from proposedChangesCA1 where ccanum='" + acct_ccanum + "'");
						foreach (DataRow itm in otherChanges.Rows)
						{
							other_changes_from_db = itm["OthrChanges"].ToString();

							if (itm["OthrChanges"].ToString().Contains("Type of Account") == true)
								update_to_sap = true;
						}

						other_changes = other_changes_from_db;

					}

					// SEND MAIL
					SendMail(branch, "1000", acct_ccanum, new_docstatus, CCA.EDocActionType.PROPOSE_CHANGES, other_changes);

					// SaveToLog
					AppHelper.SaveToLOg("CCA", acct_ccanum, action_type, remarks, Session["username"].ToString(), new_docstatus, cur_docstatus, "CUSTOMER_CHANGES");

					if (update_to_sap == true)
					{
						// UPDATE IN SAP
						UPDATE_IN_SAP(acct_ccanum);
					}
				}
				catch (Exception ex) { }

				return SActionResult.Success + acct_ccanum;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}

		}


		private void SendMail(string brnch, string status_1, string acct_ccanum, string status_2, CCA.EDocActionType DocActionType = CCA.EDocActionType.NONE, string other_data_to_embed = "")
		{
			DataTable emails_list, cca_doc_status;
			string mail_subject = "", mail_body = "", acctname = "", cca_status1 = "", cca_status2 = "", new_docstatus = "", is_returned_to_requester = "false", doc_channel = "", doc_area = "", is_doc_approved = "", is_sent_back = "", stateDesc = "";

			/* customerHeader.acctname, customerHeader.status, proposedChanges.status */
			cca_doc_status = SqlDbHelper.getDataDT(@"
				select a.acctname, a.status as 'status1', b.status as 'status2', b.is_sent_back, 
				case when b.area != '' and b.area is not null then b.area else a.area end as 'area',
				case when b.area != '' and b.area is not null then 
					(select (select g.descript from SAPSERVER.MATIMCO.dbo.oter g where g.territryid=h.parent) from SAPSERVER.MATIMCO.dbo.oter h where h.descript=b.area collate SQL_Latin1_General_CP850_CI_AS)
					else 
					(select (select g.descript from SAPSERVER.MATIMCO.dbo.oter g where g.territryid=h.parent) from SAPSERVER.MATIMCO.dbo.oter h where h.descript=a.area collate SQL_Latin1_General_CP850_CI_AS)
					end as 'channel'
				, a.approved 
				from customerHeader a, proposedChangesCA b where a.ccanum=b.ccanum and a.ccanum='" + acct_ccanum + "'"
			);

			foreach (DataRow item in cca_doc_status.Rows)
			{
				cca_status1 = item["status1"].ToString().Trim();
				cca_status2 = item["status2"].ToString().Trim();
				acctname = item["acctname"].ToString().Trim();
				doc_area = item["area"].ToString().Trim();
				doc_channel = item["channel"].ToString().Trim();
				is_doc_approved = item["approved"].ToString().Trim();
				is_sent_back = item["is_sent_back"].ToString().Trim();
			}

			if (DocActionType != CCA.EDocActionType.PROPOSE_CHANGES)
			{
				// document not yet created
				new_docstatus = cca_status1;

				if (new_docstatus == "1000")
				{
					mail_subject = acct_ccanum + "(" + acctname + ") has been Approved";
				}
				else
				{
					if (Convert.ToInt32(new_docstatus) < 0)
					{
						// if disapproved, notify the csr only
						// new_docstatus = AppHelper.GetUserPositionId("csr");
						mail_subject = acct_ccanum + "(" + acctname + ") has been Disapproved";
					}
					else
					{
						stateDesc = AppHelper.GetAccDocStatusMessage(cca_status1, cca_status2);
						mail_subject = acct_ccanum + "(" + acctname + ") is waiting for your approval (" + (stateDesc == "For Channel Manager Approval" && doc_channel.Contains("TRADE") ? "For Regional Sales Manager Approval" : stateDesc) + ")";
						//orignal code: mail_subject = acct_ccanum + "(" + acctname + ") is waiting for your approval (" +  AppHelper.GetAccDocStatusMessage(cca_status1, cca_status2) + ")";


					}
				}
			}
			else
			{
				// action_type == "PROPOSE_CHANGES"
				// document created, but some changes are to be made
				new_docstatus = cca_status2;

				// :TODO
				// QUICK FIX

				if (new_docstatus == "1")
				{
					mail_subject = "Changes in " + acct_ccanum + "(" + acctname + ") has been Approved";
				}
				else
				{
					if (Convert.ToInt32(new_docstatus) < 0)
					{
						// if disapproved, notify the csr only
						mail_subject = "Changes in " + acct_ccanum + "(" + acctname + ") has been Disapproved";
					}
					else
					{
						if (new_docstatus == AppHelper.GetUserPositionId("csr"))
						{
							mail_subject = "Changes in " + acct_ccanum + "(" + acctname + ") is waiting for your approval (" + AppHelper.GetAccDocStatusMessage("1", "1") + ")";
						}
						else
						{
							stateDesc = AppHelper.GetAccDocStatusMessage(cca_status1, cca_status2);
							mail_subject = "Changes in " + acct_ccanum + "(" + acctname + ") is waiting for your approval (" + (stateDesc == "For Channel Manager Approval" && doc_channel.Contains("TRADE") ? "For Regional Sales Manager Approval" : stateDesc) + ")";
							//mail_subject = "Changes in " + acct_ccanum + "(" + acctname + ") is waiting for your approval (" + AppHelper.GetAccDocStatusMessage(cca_status1, cca_status2) + ")";
						}
					}
				}

			}

			emails_list = GetDestEmails(new_docstatus, brnch, doc_channel, doc_area, acct_ccanum);

			mail_body = "To view the details, please click this link -->  " + AppHelper.Arms_Url + "?id=" + acct_ccanum + "&doctype=cca";

			try
			{
				foreach (System.Data.DataRow mrow in emails_list.Rows)
				{
					// place to arms.dbo.scheduler
					ARMS_W.SkelClass.cls_email c_e = new SkelClass.cls_email();

					c_e.email_from = "ARMS@matimco.com";
					c_e.email_to = mrow["email"].ToString();
					c_e.email_subject = mail_subject;
					c_e.email_content = mail_body + " - " + FormatEData(other_data_to_embed);

					AppHelper.InsertToTable("Scheduler", new Dictionary<string, object>() { 
						{"Type", "EMAIL"}
						,{"Data", Parser.toJson(c_e) }
					});

					#region TO_REMOVE
					// MailHelper.SendMail("ARMS@matimco.com", mrow["email"].ToString(), mail_subject, mail_body + " - " + FormatEData(other_data_to_embed));
					#endregion
				}
			}
			catch (Exception ex)
			{
				string err = ex.Message;
			}
		}

		private DataTable GetDestEmails(string new_docstatus, string region = "", string channel = "", string area = "", string ccanum = "")
		{
			DataTable em_list = null;
			string strQuery = "";
			string cca_classification = "", cca_account_type = "";

			DataTable customerHeader = SqlDbHelper.getDataDT("select * from customerHeader where ccanum='" + ccanum + "'");
			foreach (DataRow itm in customerHeader.Rows)
			{
				cca_classification = itm["acctClassfxn"].ToString();
				cca_account_type = itm["acctType"].ToString();
			}

			IList<string> RegionUsers = new List<string>();
			RegionUsers.Add("csr");
			RegionUsers.Add("CSR");
			RegionUsers.Add("fnm");
			RegionUsers.Add("FNM");
			RegionUsers.Add("cnc");
			RegionUsers.Add("CNC");
			RegionUsers.Add("csm");
			RegionUsers.Add("CSM");
			RegionUsers.Add("Finance Mgr.");

			IList<string> ChannelUsers = new List<string>();
			ChannelUsers.Add("chm");
			ChannelUsers.Add("CHM");
			ChannelUsers.Add("vpbsm");//Sales Director will be a Channel User
			ChannelUsers.Add("VPBSM");// Sales Director will be a Channel User

			IList<string> AreaUsers = new List<string>();
			AreaUsers.Add("asm");
			AreaUsers.Add("ASM");

			IList<string> NolFilterUsers = new List<string>();
			//NolFilterUsers.Add("vpbsm"); //Commented by billy jay. Sales Director will be a Channel User
			//NolFilterUsers.Add("VPBSM");//Commented by billy jay. Sales Director will be a Channel User
			NolFilterUsers.Add("vptfi");
			NolFilterUsers.Add("VPTFI");


			// REGION
			if (RegionUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
			{
				string poss = "";
				if (AppHelper.GetUserPosition(new_docstatus) == "csr") poss = "'csr','CSR'";

				if (AppHelper.GetUserPosition(new_docstatus) == "csm") poss = "'csm','CSM'";

				if (AppHelper.GetUserPosition(new_docstatus) == "cnc") poss = "'cnc','CNC'";

				if (AppHelper.GetUserPosition(new_docstatus) == "fnm") poss = "'fnm','FNM','Finance Mgr.'";

				strQuery = strQuery + "" +
					"select a.email from apprvrDesig a, userheader b , apprvrRole c " +
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
					"left(a.branch,1) = '" + region.Substring(0, 1) + "' and " +
					"b.status = 'ACTIVE' " +
					"group by a.email" +
					"";

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			// CHANNEL
			if (ChannelUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
			{
				string poss = "";

				//string filter_region = "";
				//if (channel.Contains("TRAD")) filter_region = " left(a.branch,1) = '" + region.Substring(0, 1) + "' and ";

				if (AppHelper.GetUserPosition(new_docstatus) == "chm") poss = "'chm','CHM'";
				if (AppHelper.GetUserPosition(new_docstatus) == "vpbsm") poss = "'vpbsm','VPBSM'"; //Add Sales Director as Channel Users for Arrienda MT RT channel

				strQuery = strQuery + "" +
					"select a.email from apprvrDesig a, userheader b , apprvrRole c " +
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
					"a.channel = '" + channel + "' and " +
					"b.status = 'ACTIVE' " +
					"group by a.email" +
					"";

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			// AREA
			if (AreaUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
			{
				string poss = "";
				if (AppHelper.GetUserPosition(new_docstatus) == "asm") poss = "'asm','ASM'";

				strQuery = strQuery + "" +
					"select a.email from apprvrDesig a, userheader b , apprvrRole c " +
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
					"a.area = '" + area + "' and " +
					"b.status = 'ACTIVE' " +
					"group by a.email" +
					"";

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			// FOR VPBSM, VPTFI
			if (NolFilterUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
			{
				string poss = "";
				if (AppHelper.GetUserPosition(new_docstatus) == "vpbsm") poss = "'vpbsm','VPBSM'";
				if (AppHelper.GetUserPosition(new_docstatus) == "vptfi") poss = "'vptfi','VPTFI'";

				strQuery = strQuery + "" +
					"select a.email from apprvrDesig a, userheader b , apprvrRole c " +
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
					"b.status = 'ACTIVE' " +
					"group by a.email" +
					"";

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			// IF DISAPPROVED
			if (Convert.ToInt32(new_docstatus) < 0)
			{
				strQuery = "";
				List<string> emails = new List<string>();
				int disapprover = Convert.ToInt32(new_docstatus) * -1;
				int index_of_disapprover = 0;

				if (cca_account_type.Trim().ToUpper() == "DIRECT" && cca_classification.Trim().ToUpper() == "REGULAR")
				{
					index_of_disapprover = AppHelper.RegCustomerDirect.IndexOf(Convert.ToString(disapprover));

					for (int i = 0; i < index_of_disapprover; i++)
					{
						string pos = AppHelper.GetUserPosition(AppHelper.RegCustomerDirect[i].ToString());

						if (strQuery != "") strQuery = strQuery + " union ";

						if (RegionUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where left(branch,1) = '" + region.Substring(0, 1) + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (ChannelUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where channel = '" + channel + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (AreaUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where area = '" + area + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (NolFilterUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

					}
				}

				if (cca_account_type.Trim().ToUpper() == "INDIRECT" && cca_classification.Trim().ToUpper() == "REGULAR")
				{
					index_of_disapprover = AppHelper.RegCustomerDirect.IndexOf(Convert.ToString(disapprover));

					for (int i = 0; i < index_of_disapprover; i++)
					{
						string pos = AppHelper.GetUserPosition(AppHelper.RegCustomerDirect[i].ToString());

						if (strQuery != "") strQuery = strQuery + " union ";

						if (RegionUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where left(branch,1) = '" + region.Substring(0, 1) + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (ChannelUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where channel = '" + channel + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (AreaUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where area = '" + area + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (NolFilterUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

					}
				}

				if (cca_classification.Trim().ToUpper() == "WALKIN")
				{
					index_of_disapprover = AppHelper.RegCustomerDirect.IndexOf(Convert.ToString(disapprover));

					for (int i = 0; i < index_of_disapprover; i++)
					{
						string pos = AppHelper.GetUserPosition(AppHelper.RegCustomerDirect[i].ToString());

						if (strQuery != "") strQuery = strQuery + " union ";

						if (RegionUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where left(branch,1) = '" + region.Substring(0, 1) + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (ChannelUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where channel = '" + channel + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (AreaUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where area = '" + area + "' and roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

						if (NolFilterUsers.IndexOf(pos) != -1)
						{
							strQuery = strQuery +
								"select email from apprvrDesig " +
								"where roleid in (select roleid from apprvrRole where rolecode in ('" + pos.ToUpper() + "')) " +
								" group by email";
						}

					}
				}

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			return em_list;
		}

		[AuthorizeUsr]
		[HttpPost]
		public string CheckAcctcode(string acct_code)
		{
			DataTable AcctCode = SqlDbHelper.getDataDT(@"
				select cardcode 
				from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = '" + acct_code + @"'
				union
				select cast(acctcode collate SQL_Latin1_General_CP850_CI_AS as varchar(15)) 
				from dbo.customerheader where acctcode = '" + acct_code + @"'
				union
				select cast(ProposedLeadCode collate SQL_Latin1_General_CP850_CI_AS as varchar(15)) 
				from dbo.customerLeadI where ProposedLeadCode = '" + acct_code + @"'
			"
			);
			bool is_exist = false;

			try
			{
				foreach (DataRow item in AcctCode.Rows)
				{
					is_exist = true;
					break;
				}
			}
			catch (Exception ex)
			{
				return SActionResult.Error + ex.Message;
			}

			if (is_exist == true)
			{
				return SActionResult.Success + "\"" + acct_code + "\" IS NOT AVAILABLE.";
			}
			else
			{
				return SActionResult.Success + "\"" + acct_code + "\" IS AVAILABLE.";
			}

		}

		public string CheckAcctcodeifFoundnSAP(string final_acct_code, string ccanum)
		{
			string strQuery = "";
			// get acctcode using ccanum


			if (ccanum != "" && ccanum != null)
			{
				// customer creation
				strQuery = SqlQueryHelper.check_customer_code(final_acct_code, ccanum);
			}
			else
			{
				// lead creation
				strQuery = @" 
				select cardcode 
				from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = '" + final_acct_code + @"'
				union 
				select cast(ProposedLeadCode collate SQL_Latin1_General_CP850_CI_AS as varchar(15)) 
				from dbo.customerLeadI where ProposedLeadCode = '" + final_acct_code + @"'
				union
				select cast(acctcode collate SQL_Latin1_General_CP850_CI_AS as varchar(15)) 
				from dbo.customerheader where acctcode = '" + final_acct_code + @"' and ccanum != '" + ccanum + @"'
				";
			}

			DataTable AcctCode = SqlDbHelper.getDataDT(strQuery);
			bool is_exist = false;

			try
			{
				foreach (DataRow item in AcctCode.Rows)
				{
					is_exist = true;
					break;
				}
			}
			catch (Exception ex)
			{
				return SActionResult.Error + ex.Message;
			}

			if (is_exist == true)
			{
				return SActionResult.Success + "\"" + final_acct_code + "\" IS NOT AVAILABLE.";
			}
			else
			{
				return SActionResult.Success + "\"" + final_acct_code + "\" IS AVAILABLE.";
			}

		}

		private string FormatEData(string data)
		{
			if (data == "") return "";

			string str_final_data = "";
			string[] tmp_rows = data.Split(new string[] { "#$" }, StringSplitOptions.None);

			str_final_data = "\n\nChanges:\n\n";

			foreach (string cols in tmp_rows)
			{
				string[] tmp_cols = cols.Split('|');

				if (tmp_cols[0] != "")
				{
					str_final_data = str_final_data + tmp_cols[0] + ": " + tmp_cols[1] + " -> " + tmp_cols[2] + "\n";
				}
			}

			return str_final_data;
		}

		[HttpPost]
		public string UpdateCustSapUpdate(string ccanum)
		{
			SQLTransaction mt_trans = new SQLTransaction();

			try
			{
				mt_trans.StartTransaction();

				if (ccanum == "") { throw new Exception("no cca"); }

				mt_trans.CommandText = "update custSapUpdate set isUpdated = 'Y', DateUpdated = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', UpdatedBy='" + Session["username"].ToString() + "' where ccanum = '" + ccanum + "'";

				mt_trans.Committransaction();

				return SActionResult.Success;
			}
			catch (Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}

		}

		private void UPDATE_IN_SAP(string ccanum)
		{
			ARMS_W.SkelClass.sap_businesspartner s_b = new SkelClass.sap_businesspartner();
			s_b.docid = ccanum.Trim().ToUpper();
			s_b.doctype = "CUSTOMER";
			// save to arms.dbo.scheduler

			AppHelper.InsertToTable("Scheduler", new Dictionary<string, object>() { 
				{"Type", "UPDATE_TO_SAP"}
				,{"Data", Parser.toJson(s_b)}
			});

		}

		private void InsertLogs(string ccanum, string message, string data)
		{
			SQLTransaction sql_transation = new SQLTransaction();
			string err = "";

			try
			{
				sql_transation.StartTransaction();

				sql_transation.InsertTo("sap_di_logs",
				new Dictionary<string, object>(){
					{"ccanum", ccanum}
					,{"message", message}
					,{"data", data}
					,{"timestmp", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}
				});

				sql_transation.Committransaction();
			}
			catch (Exception ex)
			{
				sql_transation.RollbackTransaction();
				err = ex.Message;
			}
		}

		[AuthorizeUsr]
		public string GetFilteredList2(string _str_data, string par1 = "", string par2 = "", string tArea = "", string array1 = "")
		{
			string strquery = "", str_region = "", str_channel = "", str_area = "";
			OleDbDataReader tmp_reader = null;

			_User oUsr = new _User(HttpContext.Session["username"].ToString());

			foreach (_Roles rls in oUsr.Roles)
			{
				foreach (string region_name in rls.Region)
				{
					if (str_region != "") str_region = str_region + ",";
					str_region = "'" + region_name + "'";
				}

				foreach (string channel_name in rls.Channel)
				{
					if (str_channel != "") str_channel = str_channel + ",";
					str_channel = "'" + channel_name + "'";
				}

				foreach (string area_name in rls.Area)
				{
					if (str_area != "") str_area = str_area + ",";
					str_area = "'" + area_name + "'";
				}
			}

			try
			{
				if (_str_data == "ListOfSo")
				{
					strquery = @"
						select a.slpcode, a.slpname from 
						SAPSERVER.MATIMCO.dbo.OSLP a inner join SAPSERVER.MATIMCO.dbo.oter z on a.u_area=left(z.descript,5) 
						inner join SAPSERVER.MATIMCO.dbo.oter x on z.parent=x.territryid left join ChannelGroup y on 
						y.area collate SQL_Latin1_General_CP850_CI_AS=z.descript 
						where left(z.descript,2) = 'AR' and a.u_active='Y' and (a.u_position='AO' or a.u_position='MDO' )
						and (case when charindex('LUZON',x.descript) > 0 then 'LUZON' when charindex('VISMIN',x.descript) > 0 then 'VISMIN' else '' end) in (" + str_region + @")

						UNION 
						
						SELECT slpcode,slpname from SAPSERVER.MATIMCO.dbo.OSLP where  slpcode in('57','151','55')                      

						";
				}

				if (_str_data == "ListOfChannel")
				{
					if (str_region.IndexOf("VISMIN") != -1)
					{
						strquery = @"
						select 'GTV', 'GTV' union 
						select 'ISV', 'ISV' union
						select 'RTV', 'RTV' ";
					}

					if (str_region.IndexOf("LUZON") != -1)
					{
						strquery = @"
						select 'GTL', 'GTL' union 
						select 'ISL', 'ISL' union
						select 'RTL', 'RTL' ";
					}

					if (str_region.IndexOf("LUZON") != -1 && str_region.IndexOf("VISMIN") != -1)
					{
						strquery = @"
						select 'GTL', 'GTL' union 
						select 'ISL', 'ISL' union 
						select 'RTL', 'RTL' union
						select 'GTV', 'GTV' union 
						select 'ISV', 'ISV' union
						select 'RTV', 'RTV' ";
					}
				}

				tmp_reader = SqlDbHelper.getData(strquery);
				return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}
		}

		[AuthorizeUsr]
		public string GetCCADocRemarks(string ccanum)
		{
			string tmp_str = "";
			string strQ = "";
			string mType = "";
			string doc_history_remarks = "";

			StringBuilder doc_history_remB = new StringBuilder(1000);
			StringBuilder tmp_strb = new StringBuilder(1000);

			tmp_strb.Append(@"
				<div id='div_rmrks' style='padding:11px;'>
			
				<table cellspacing='0' cellpadding='3' border='0' style=' border:1px solid #ededed; font-family:Arial; font-size:11px; '>
					<tr>
						<td colspan='5' style='font-size:12px; padding:2px; background:#ededed;'><b> Customer Creation </b></td>
					</tr>
					<tr style='background:#f7f7f7;'>
						<td align='center'><b>Approver/User</b></td>
						<td align='center'><b>Position</b></td>
						<td align='center'><b>Date</b></td>
						<td align='center'><b>Action</b></td>
						<td align='center'><b>Remarks</b></td>
					</tr>
			");

			strQ = @"
			select a.*, b.firstname + ' ' + b.lastname as 'name', convert(varchar(10),mdate,101) as 'formattedMdate' from document_history a left join userheader b 
			on b.username=a.creator_uname where a.docid='" + ccanum + "' and a.doctype='CCA' and TAG='CUSTOMER_CREATION' order by a.mdate asc ";
			DataTable document_history = SqlDbHelper.getDataDT(strQ);

			foreach (DataRow itm in document_history.Rows)
			{

				switch (itm["mType"].ToString())
				{
					case "APPROVE": mType = "Approved"; break;
					case "DISAPPROVE": mType = "Disapproved"; break;
					case "SEND_BACK_TO_REQUESTER": mType = "Sent Back to Requester"; break;
					case "NEW_CUSTOMER": mType = "Added New Customer"; break;
					case "UPDATE": mType = "Updated Customer Info"; break;
					case "SEND_BACK_TO_CNC": mType = "Sent back to CNC"; break; //inserted line code october 12 2013
					case "PROPOSE_CHANGES": mType = "Proposed Changes"; break;  //inserted line code october 12 2013
					default: mType = ""; break;
				}

				doc_history_remB.Append(
					"<tr>" +
					"<td valign=\"top\">" + itm["name"].ToString() + "</td>" +
					"<td valign=\"top\">" + AppHelper.GetUserPositionTitle(itm["PrevDocStatus"].ToString()).ToString() + "</td>" +
					"<td valign=\"top\">" + itm["mdate"].ToString() + "</td>" +
					"<td valign=\"top\">" + mType + "</td>" +
					"<td valign=\"top\">" + itm["Remarks"].ToString() + "</td>" +
					"</tr>" +
					""
				);

			}

			tmp_strb.Append(doc_history_remB.ToString());

			tmp_strb.Append(@"
				
			</table>
			<br />
			
			<table cellspacing='0' cellpadding='3' border='0' style=' border:1px solid #ededed; font-family:Arial; font-size:11px; '>
				<tr>
					<td colspan='5' style='font-size:12px; padding:2px; background:#ededed;'><b> Document Changes </b></td>
				</tr>
				<tr style='background:#f7f7f7;'>
					<td align='center'><b>Approver/User</b></td>
					<td align='center'><b>Position</b></td>
					<td align='center'><b>Date</b></td>
					<td align='center'><b>Action</b></td>
					<td align='center'><b>Remarks</b></td>
				</tr>
			");

			strQ = @"
			select a.*, b.firstname + ' ' + b.lastname as 'name', convert(varchar(10),mdate,101) as 'formattedMdate' from document_history a left join userheader b 
			on b.username=a.creator_uname where a.docid='" + ccanum + "' and a.doctype='CCA' and TAG='CUSTOMER_CHANGES' order by a.mdate asc ";
			DataTable document_history_changes = SqlDbHelper.getDataDT(strQ);

			doc_history_remB = new StringBuilder(1000);

			foreach (DataRow itm in document_history_changes.Rows)
			{
				switch (itm["mType"].ToString())
				{
					case "APPROVE": mType = "Approved"; break;
					case "DISAPPROVE": mType = "Disapproved"; break;
					case "SEND_BACK_TO_REQUESTER": mType = "Sent Back to Requester"; break;
					case "NEW_CUSTOMER": mType = "Added New Customer"; break;
					case "UPDATE": mType = "Updated Customer Info"; break;
					case "PROPOSE_CHANGES": mType = "Updated Changes"; break;
					case "SEND_BACK_TO_CNC": mType = "Sent back to CNC"; break; //inserted line code october 12 2013
				}

				doc_history_remB.Append(
					"<tr>" +
					"<td valign=\"top\">" + itm["name"].ToString() + "</td>" +
					"<td valign=\"top\">" + AppHelper.GetUserPositionTitle(itm["PrevDocStatus"].ToString()).ToString() + "</td>" +
					"<td valign=\"top\">" + itm["mdate"].ToString() + "</td>" +
					"<td valign=\"top\">" + mType + "</td>" +
					"<td valign=\"top\">" + itm["Remarks"].ToString() + "</td>" +
					"</tr>" +
					""
				);
			}

			tmp_strb.Append(doc_history_remB.ToString());
			tmp_strb.Append("</table></div>");

			return tmp_strb.ToString();
		}

		public string GetCCADocChanges(string ccanum)
		{
			DataTable query = SqlDbHelper.getDataDT("select ccanum,OthrChanges,timestmp,username from dbo.proposedChangesCA1 where ccanum = '" + ccanum + "' and docstatus = '1000' order by timestmp desc");

			StringBuilder str_tbl = new StringBuilder(1000);
			int row_counter = 0;

			str_tbl.Append("<div style=\"padding:5px; font-size:11px;\">");
			str_tbl.Append("<table cellspacing='0' cellpadding='2' border='0'>");
			foreach (DataRow itm in query.Rows)
			{
				row_counter++;
				if (row_counter > 1)
				{
					str_tbl.Append("<tr>");
					str_tbl.Append("<td colspan=\"3\">&nbsp;</td>");
					str_tbl.Append("</tr>");
				}

				str_tbl.Append("<tr style=\"background:#cdcdcd;\">");
				str_tbl.Append("<td colspan='3' style=\"font-weight:bold;\">" + itm["username"].ToString() + ": " + itm["timestmp"].ToString() + "</td>");
				str_tbl.Append("</tr>");
				str_tbl.Append("<tr style=\"background:#ededed;\">");
				str_tbl.Append("<td></td><td align=\"center\"><b>Old Value</b></td><td align=\"center\"><b>New Value</b></td>");
				str_tbl.Append("</tr>");

				string data = itm["OthrChanges"].ToString();
				string[] tmp_str = data.Split('^');
				foreach (string str_rows in tmp_str)
				{
					if (str_rows.Trim() != "")
					{
						str_tbl.Append("<tr>");
						string[] splitted_str = str_rows.Split('|');
						str_tbl.Append("<td>" + splitted_str[0] + "</td><td>" + splitted_str[1] + "</td><td>" + splitted_str[2] + "</td>");
						str_tbl.Append("</tr>");
					}
				}
			}

			str_tbl.Append("</table>");
			str_tbl.Append("</div>");

			return str_tbl.ToString();
		}

	}
}
