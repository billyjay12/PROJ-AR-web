using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARMS_W.Class;
using ARMS_W.GLOBALS;
using System.Data.OleDb;
using System.IO;
using System.Data;


namespace ARMS_W.Controllers
{
	public class SQLController : Controller
	{
		//
		// GET: /SQL/

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public string GetList (string _str_data, string par1="", string par2="",string tArea="", string array1="") {
			string strquery = "";


			if (_str_data == "roleName")
			{
				strquery = "SELECT roleId,roleName FROM apprvrRole";
			}
			if (_str_data == "routingEmp")
			{
				strquery = "SELECT lastName+','+firstname , empIdNo,emailadd FROM userHeader";
			}

			if ( _str_data == "ListOfSo" ) {
				string region = "";

				if (par1.IndexOf("L") > 0) region = "LUZON";
				if (par1.IndexOf("V") > 0) region = "VISMIN";

				if (par1 != "")
				{
					strquery = @"
						select a.slpcode, a.slpname from 
						SAPSERVER.MATIMCO.dbo.OSLP a inner join SAPSERVER.MATIMCO.dbo.oter z on a.u_area=left(z.descript,5) 
						inner join SAPSERVER.MATIMCO.dbo.oter x on z.parent=x.territryid left join ChannelGroup y on 
						y.area collate SQL_Latin1_General_CP850_CI_AS=z.descript 
						where left(z.descript,2) = 'AR' and a.u_active='Y' and a.u_position='AO' and y.grp_name='" + par1 + "' order by a.slpname";
				}
				else 
				{
					strquery = @"
						select a.slpcode, a.slpname from 
						SAPSERVER.MATIMCO.dbo.OSLP a inner join SAPSERVER.MATIMCO.dbo.oter z on a.u_area=left(z.descript,5) 
						inner join SAPSERVER.MATIMCO.dbo.oter x on z.parent=x.territryid left join ChannelGroup y on 
						y.area collate SQL_Latin1_General_CP850_CI_AS=z.descript 
						where left(z.descript,2) = 'AR' and a.u_active='Y' and a.u_position='AO' order by a.slpname";
				}
			}

			if (_str_data == "ListOfDirAcccod")
			{
				strquery = "select '', acctCode from dbo.customerHeader where acctType='direct' order by acctCode";
			}

			if (_str_data == "ListOfINDirAcccod")
			{
				strquery = "select '', acctName, bussAdd, telNum from dbo.customerHeader where acctType='indirect' order by acctName";
			}

			if(_str_data=="ListOfCodeAndName"){

				strquery = "SELECT acctcode,acctName,acctOffcr,area,bussAdd FROM dbo.customerHeader";
			}

			if (_str_data == "ListOfSoAsm")
			{
				string region = "";
				if (par1.IndexOf("V") > 0) { region = "VISMIN"; }
				else { region = "LUZON"; }
				string poss = "asm', 'ASM";
				
				if (region != "")
				{
					strquery = @"
						select a.slpcode, a.slpname, a.u_area + ' - ' + a.u_area as 'area', isnull(b.channel, '') as 'channel' 
						from SAPSERVER.MATIMCO.dbo.oslp a left join ChannelGroup b on (a.u_area + ' - ' + a.u_area) = b.area collate SQL_Latin1_General_CP850_CI_AS 
						where a.u_active = 'Y' and a.u_position = 'ASM' 
						and b.grp_name='" + par1 + "'";
				}
				else 
				{
					strquery = @"
						select a.slpcode, a.slpname, a.u_area + ' - ' + a.u_area as 'area', isnull(b.channel, '') as 'channel' 
						from SAPSERVER.MATIMCO.dbo.oslp a left join ChannelGroup b on (a.u_area + ' - ' + a.u_area) = b.area collate SQL_Latin1_General_CP850_CI_AS 
						where a.u_active = 'Y' and a.u_position = 'ASM'";
				}
				
			}
		   
			if (_str_data == "ListOfitem")
			{
			   strquery = "SELECT * from arms_vw_itemcode";
			}

			if (_str_data == "ListOfunit") {

				strquery = " SELECT '', uom FROM unitOfMeasure ";
			}

			if (_str_data == "ListofMarketingBrand")
			{
				strquery = "SELECT FldValue, UPPER(Descr) FROM SAPSERVER.MATIMCO.dbo.ufd1 WHERE TableId='OITM' AND FieldId='23' and FldValue NOT IN('','XX','AD','MH','GM') UNION ALL select 'ALL BRANDS', 'ALL BRANDS' ";
			}

			if (_str_data == "MktProgramDate")
			{
				strquery = "SELECT DISTINCT startFinish FROM dbo.mrktTimeline";
			}

			if (_str_data == "MktProgramAmount")
			{
				strquery = "SELECT totalAmtResources FROM dbo.mrktProgram";
			}

			if (_str_data == "MktProgtype")
			{
				strquery = "SELECT DISTINCT progType FROM dbo.mrktProgram";
			}

			if (_str_data == "UsserID")
			{
				strquery = "SELECT roleName,roleID  FROM dbo.apprvrRole";
			}

			if (_str_data == "docdesc")
			{
				strquery = "SELECT '',docDescription FROM dbo.documentTypes";
			}

			if (_str_data == "ListofBrand")
			{
				strquery = "SELECT '', 'ALL BRANDS' UNION ALL SELECT '', UPPER(Descr) FROM SAPSERVER.MATIMCO.dbo.ufd1 WHERE TableId='OITM' AND FieldId='23'";
			}

			if (_str_data == "ListOfRoutingBrand")
			{
				strquery = "SELECT '', '-N/A-' UNION ALL SELECT '', UPPER(Descr) FROM SAPSERVER.MATIMCO.dbo.ufd1 WHERE TableId='OITM' AND FieldId='23'";
			}

			if (_str_data == "ListOfCategoryType") {
				strquery = "SELECT categoryType FROM marketingReqCategory";
			}

			if (_str_data == "ListOfChannel")
			{

				if (par1 == "LUZON") 
				{
					strquery = @"
					select 'GTL', 'GTL' union 
					select 'ISL', 'ISL' ";
				}
				else if (par1 == "VISMIN")
				{
					strquery = @"
					select 'GTV', 'GTV' union 
					select 'ISV', 'ISV' ";
				}
				else 
				{
					strquery = @"
					select 'GTL', 'GTL' union 
					select 'ISL', 'ISL' union 
					select 'GTV', 'GTV' union 
					select 'ISV', 'ISV' ";
				}

			}

			if ( _str_data == "ListOfTerritory" ) {
				if (par1 != "")
				{
					strquery = @"
						select a.territryid, a.descript as 'territory', cast(b.territryid as nvarchar) + ' - ' + b.descript as 'area', 
						cast(c.territryid as nvarchar) + ' - ' + c.descript as 'channel' from 
						SAPSERVER.MATIMCO.dbo.oter a, SAPSERVER.MATIMCO.dbo.oter b, SAPSERVER.MATIMCO.dbo.oter c 
						where a.parent = b.territryid and b.parent = c.territryid and left(b.descript,2) = 'AR' 
						and charindex('" + par1 + @"', c.descript) >0 
						order by a.descript asc --c.descript, b.descript, a.descript ";
				}
				else 
				{
					strquery = @"
						select a.territryid, a.descript as 'territory', cast(b.territryid as nvarchar) + ' - ' + b.descript as 'area', 
						cast(c.territryid as nvarchar) + ' - ' + c.descript as 'channel' from 
						SAPSERVER.MATIMCO.dbo.oter a, SAPSERVER.MATIMCO.dbo.oter b, SAPSERVER.MATIMCO.dbo.oter c 
						where a.parent = b.territryid and b.parent = c.territryid and left(b.descript,2) = 'AR' 
						order by c.descript, b.descript, a.descript ";
				}
				
			}

			if ( _str_data == "ListOfBPClass" ) {
				strquery = @"
					select '1M', 'LOW TIER' union 
					select '2M', 'MID TIER' union 
					select '3M', 'TOP TIER' ";
			}

			if ( _str_data == "ListOfPriceCode" ) {
				strquery = "select '', FldValue, Descr from SAPSERVER.MATIMCO.dbo.ufd1 where tableID='OCRD'and FieldID='9'";        
			}

			if (_str_data == "ListOfRoleID"){
				strquery = "SELECT roleID,roleName FROM apprvrRole";
			}

			if (_str_data == "ListOfLandType") {
				strquery = @"
					select '', 'Agricultural' union 
					select '', 'Commercial' union 
					select '', 'Residential' ";
			}

			if (_str_data == "ListOfBuildingType")
			{
				strquery = @"
					select '', 'Commercial' union 
					select '', 'Residential' ";
			}

			if (_str_data == "TargetChannel")
			{
				strquery = "SELECT '', descript FROM SAPSERVER.MATIMCO.dbo.oter WHERE parent in (SELECT territryID FROM SAPSERVER.MATIMCO.dbo.oter WHERE territryID in ('1','86')) AND inactive='N'union select '', 'ALL CHANNELS'";
			}

			if (_str_data == "TargetArea")
			{
				strquery = "select  '',  areaname from SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd where channel in ("+ array1  +")";
			}

			if (_str_data == "TargetAccount")
			{
				strquery = "select  '',  cardcode from SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd where areaname in (" + tArea + ")";
			}

			if (_str_data == "ListOfAcctNameSo")
			{
				strquery = "select cardcode, cardname, soname from SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd where left(cardcode,2) in ('CV', 'CL') order by cardname";
			}

			if (_str_data == "ListOfPaymentGroup")
			{
				strquery = "SELECT '', pymntGroup FROM SAPSERVER.MATIMCO.dbo.octg group by pymntGroup";
			}

			if (_str_data == "ListOFCreditLine")
			{
				strquery = "SELECT DISTINCT '', CreditLine FROM SAPSERVER.MATIMCO.dbo.ocrd";
			}

			if (_str_data == "ListOfMmaDocType") 
			{
				strquery = @"
					SELECT 'Meeting Minutes', 'Meeting Minutes' union 
					SELECT 'Contract', 'Contract' union 
					SELECT 'Certificate', 'Certificate' union 
					SELECT 'Display Module Agreements', 'Display Module Agreements' union 
					SELECT 'Racks Agreements', 'Racks Agreements' union 
					SELECT 'Campaign Agreements', 'Campaign Agreements' union 
					SELECT 'Welcome Letters', 'Welcome Letters' union 
					SELECT 'MTO DP Exemption', 'MTO DP Exemption' union 
					SELECT 'Consignment Contracts', 'Consignment Contracts' union 
					SELECT 'Billboard Agreements', 'Billboard Agreements' union 
					SELECT 'Distribution Agreements', 'Distribution Agreements' union 
					SELECT 'others', 'Others' ";
			}

			if (_str_data == "ListOfMeetingStat")
			{
				strquery = @"
					SELECT 'Pending', 'Pending' union 
					SELECT 'Done', 'Done' ";
			}

			if (_str_data == "ListOfVat")
			{
				strquery = @"
					SELECT 'VAT', 'VAT' union 
					SELECT 'NON-VAT', 'NON-VAT' union 
					SELECT 'ZERO-RATED', 'ZERO-RATED' ";
			}

			if (_str_data == "ListOfAcctNameSoOnly") 
			{
				strquery = @"
					select 
						case when sapacctcode is not null and sapacctcode != '' then sapacctcode else acctcode end as 'acctcode', 
					acctname, acctoffcr from customerheader where status = '1000' ORDER BY acctname asc
					";
			}

			if (_str_data == "ListOfAcctNameWSoName")
			{
				strquery = "select soname from SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd where left(cardcode,2) in ('CV', 'CL') and cardcode='" + par1 + "' order by cardname";
			}

			if (_str_data == "ListOfitemNames")
			{
				strquery = "SELECT itemname from arms_vw_itemcode where itemcode='" + par1 + "'";
			}

			if (_str_data == "ListOfitemCode")
			{
				strquery = "SELECT '', itemcode from arms_vw_itemcode    ";
			}

			if (_str_data == "ListOfSOASMCHM") 
			{
				strquery = "SELECT CASE WHEN RTRIM(LTRIM(SOName))='NULL' THEN '' ELSE SOName END AS 'SOName', CASE WHEN RTRIM(LTRIM(U_ASM))='NULL' THEN '' ELSE U_ASM END AS 'U_ASM', CASE WHEN RTRIM(LTRIM(U_CHMGR))='NULL' THEN '' ELSE U_CHMGR END AS 'U_CHMGR' FROM mtc_vw_BRQD WHERE cardcode='" + par1 + "'";
			}

			if (_str_data == "ListOfChannels")
			{
				strquery = "SELECT * FROM arms_vw_ListOfChannel";
			}

			if (_str_data == "ListOfArea")
			{
				strquery = "SELECT * FROM arms_vw_ListOfArea";
			}

			if (_str_data == "ListOfChannels_MOD")
			{
				strquery = @"
					select c.territryid, c.descript 
					from SAPSERVER.MATIMCO.dbo.oter b, SAPSERVER.MATIMCO.dbo.oter c 
					where b.parent=c.territryid and left(b.descript,2) = 'AR' group by c.descript, c.territryid ";
			}

			if (_str_data == "ListOfArea_MOD")
			{
				strquery = @"
					select b.territryid, b.descript 
					from SAPSERVER.MATIMCO.dbo.oter b, SAPSERVER.MATIMCO.dbo.oter c 
					where b.parent=c.territryid and left(b.descript,2) = 'AR' group by b.descript, b.territryid ";
			}

			if (_str_data == "ListOfBrand_MOD")
			{
				strquery = "SELECT FldValue, UPPER(Descr) FROM SAPSERVER.MATIMCO.dbo.ufd1 WHERE TableId='OITM' AND FieldId='23'";
			}

			if (_str_data == "ListOfRegion_MOD")
			{
				strquery = @"
					SELECT 'LUZON', 'LUZON' union 
					SELECT 'VISMIN', 'VISMIN' ";
			}

			if (_str_data == "ListOfUserHeader_MOD")
			{
				strquery = "select empidno, username, emailadd from userheader order by username asc";
			}

			if (_str_data == "ListOfRoleId_MOD")
			{
				strquery = "select roleid, rolename from dbo.apprvrRole order by rolename asc";
			}

			if (_str_data == "ListOfSlpcode_MOD")
			{
				strquery = "select slpcode, slpname from SAPSERVER.MATIMCO.dbo.oslp";
			}

			try 
			{
				DataTable tmp_table = SqlDbHelper.getDataDT(strquery);
				return "00:" + StringHelper.ConvertDataTableToString(tmp_table);
			}
			catch(Exception ex)
			{
				return "01:" + ex.Message;
			}
		}

		[HttpPost]
		[ParseUrl]
		public string AddLeadCustomer (
				string lead_name,
				string lead_inqdate,
				string lead_address,
				string lead_contactno,
				string lead_email,
				string is_mw_selected,
				string is_ww_selected,
				string is_pw_selected,
				string is_tw_selected,
				string is_gw_selected,
				string is_nw_selected,
				string is_mu_selected,
				string is_ec_selected,
				string is_framing_selected,
				string is_moulding_selected,
				string is_flooring_selected,
				string is_doorjambs_selected,
				string is_panellings_selected,
				string is_engditm_selected,
				string is_decking_selected,
				string is_staircomp_selected,
				string is_others_selected,
				string opt_other_desc,
				string lead_encoded_date,
				string lead_encoded_by,
				string remarks,
				string lead_proposed_channel,
				string inquiry_source_web,
				string inquiry_source_exhibit_name,
				string inquiry_source_exhibit_date,
				string inquiry_source_exhibit_address,
				string inquiry_source_refered_by,
				string inquiry_source_sales_officer,
				string inquiry_source_other_source
			) {
			SQLTransaction mt_trans = new SQLTransaction();

			// generate new request ID
			System.Data.DataRow new_req_id_row = SqlDbHelper.getDataDT("select isnull((select max(cast(requestid as bigint)) + 1 from dbo.customerLeadI),1) as 'new_id'").Rows[0];
			string new_request_id = new_req_id_row["new_id"].ToString();

			string lead_doc_destination = "0";
			string sales_officer_id = "";
			// check if source from coverage
			if (inquiry_source_sales_officer != "")
			{
				lead_doc_destination = AppHelper.GetUserPositionId("csr");

				DataTable SO_ID = SqlDbHelper.getDataDT("select slpcode from SAPSERVER.MATIMCO.dbo.oslp where slpname = '" + inquiry_source_sales_officer + "'");
				foreach (DataRow itm in SO_ID.Rows) 
				{
					sales_officer_id = itm["slpcode"].ToString();
				}
			}
			else 
			{ 
				// send to channel manager
				lead_doc_destination = AppHelper.GetUserPositionId("chm");
			}

			string str_region = "";
			if (lead_proposed_channel.Substring((lead_proposed_channel.Length - 1) - 1, 1) == "L")
			{
				str_region = "Luzon";
			}
			else 
			{
				str_region = "Vismin";
			}

			try 
			{
				mt_trans.StartTransaction();
				
				mt_trans.InsertTo("customerLeadI", new Dictionary<string, object>() { 
					{"RequestId", new_request_id }
					,{"Name", lead_name }
					,{"InqDate", lead_inqdate != "" ? Convert.ToDateTime(lead_inqdate) : (DateTime?)null }
					,{"Address", lead_address }
					,{"ContactNo", lead_contactno }
					,{"E_mail", lead_email }
					,{"IsInquired_MWBrand", is_mw_selected }
					,{"IsInquired_WWBrand", is_ww_selected }
					,{"IsInquired_PWBrand", is_pw_selected }
					,{"IsInquired_TWBrand", is_tw_selected }
					,{"IsInquired_GWBrand", is_gw_selected }
					,{"IsInquired_NWBrand", is_nw_selected }
					,{"IsInquired_MUBrand", is_mu_selected }
					,{"IsInquired_ECBrand", is_ec_selected }
					,{"IsInquired_Framing", is_framing_selected }
					,{"IsInquired_Mouldings", is_moulding_selected }
					,{"IsInquired_Floorings", is_flooring_selected }
					,{"IsInquired_DoorJambs", is_doorjambs_selected }
					,{"IsInquired_Panellings", is_panellings_selected }
					,{"IsInquired_EngdItm", is_engditm_selected }
					,{"IsInquired_Decking", is_decking_selected }
					,{"IsInquired_StairComp", is_staircomp_selected }
					,{"IsInquired_Others", is_others_selected }
					,{"OtherInquiry_desc", opt_other_desc }
					,{"Remarks", remarks }
					,{"ProposedChannel", lead_proposed_channel }
					,{"EnCodedby", lead_encoded_by }
					,{"DateEncoded", DateTime.Now }
					,{"Status", lead_doc_destination }
					,{"InquirySource", "0" }
					,{"WebContactFrm", inquiry_source_web }
					,{"ExhibitName", inquiry_source_exhibit_name }
					,{"ExhibitDate", inquiry_source_exhibit_date != "" ? Convert.ToDateTime(inquiry_source_exhibit_date) : (DateTime?)null }
					,{"ExhibitAddress", inquiry_source_exhibit_address }
					,{"ReferedBy", inquiry_source_refered_by }
					,{"SalesOfficer", inquiry_source_sales_officer }
					,{"OtherSources", inquiry_source_other_source }
					,{"BkToSender", "false" }
					,{"AssignTo_empId", sales_officer_id != "" ? sales_officer_id : null }
				});

				mt_trans.Committransaction();

				// send mail - 
				SendMailLead(str_region, lead_doc_destination, new_request_id, "");

				// SaveToLog
				AppHelper.SaveToLOg("LCD", new_request_id, "ADD_LEAD_CUSTOMER", "", Session["username"].ToString());

				return SActionResult.Success + new_req_id_row["new_id"].ToString();
			}
			catch(Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
			
		}

		[HttpPost]
		public string UpdateLeadCustomer(
				string request_id,
				string lead_name,
				string lead_inqdate,
				string lead_address,
				string lead_contactno,
				string lead_email,
				string is_mw_selected,
				string is_ww_selected,
				string is_pw_selected,
				string is_tw_selected,
				string is_gw_selected,
				string is_framing_selected,
				string is_moulding_selected,
				string is_flooring_selected,
				string is_doorjambs_selected,
				string is_panellings_selected,
				string is_engditm_selected,
				string is_decking_selected,
				string is_staircomp_selected,
				string is_others_selected,
				string lead_encoded_date,
				string lead_encoded_by,
				string remarks,
				string lead_proposed_channel,
				string inquiry_source_web,
				string inquiry_source_exhibit_name,
				string inquiry_source_exhibit_date,
				string inquiry_source_exhibit_address,
				string inquiry_source_refered_by,
				string inquiry_source_sales_officer,
				string inquiry_source_other_source
			) {
			SQLTransaction mt_trans = new SQLTransaction();

			// lead_doc_destination - used for rerouting the lead request again
			string lead_doc_destination = "0";
			// check if sourced from coverage
			if (inquiry_source_sales_officer != "")
			{
				lead_doc_destination = AppHelper.GetUserPositionId("csr");
			}
			else
			{
				lead_doc_destination = AppHelper.GetUserPositionId("chm");
			}

			string str_region = "";
			if (lead_proposed_channel.Substring((lead_proposed_channel.Length - 1) - 1, 1) == "L")
			{
				str_region = "Luzon";
			}
			else
			{
				str_region = "Vismin";
			}

			try 
			{
				mt_trans.StartTransaction();
				
				mt_trans.CommandText = SqlQueryHelper.Update_customerLeadI(
						lead_name,
						lead_inqdate,
						lead_address,
						lead_contactno,
						lead_email,
						is_mw_selected,
						is_ww_selected,
						is_pw_selected,
						is_tw_selected,
						is_gw_selected,
						is_framing_selected,
						is_moulding_selected,
						is_flooring_selected,
						is_doorjambs_selected,
						is_panellings_selected,
						is_engditm_selected,
						is_decking_selected,
						is_staircomp_selected,
						is_others_selected,
						remarks,
						lead_proposed_channel,
						// AppHelper.GetUserPositionId("chm"),
						lead_doc_destination,
						lead_encoded_by,
						inquiry_source_web,
						inquiry_source_exhibit_name,
						inquiry_source_exhibit_date,
						inquiry_source_exhibit_address,
						inquiry_source_refered_by,
						inquiry_source_sales_officer,
						inquiry_source_other_source,
						"false",
						request_id
					);

				mt_trans.Committransaction();

				// send mail
				SendMailLead(str_region, lead_doc_destination, request_id, "");

				// SaveToLog
				AppHelper.SaveToLOg("LCD", request_id, "UPDATE_LEAD_CUSTOMER", "", Session["username"].ToString());

				return SActionResult.Success;
			}
			catch(Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult .Error + ex.Message;
			}
		}

		[HttpPost]
		public string DeleteFileAttachment(string attachment_type, string doc_id, string filename) {
			SQLTransaction mt_trans = new SQLTransaction();
			try 
			{
				if (attachment_type != "MKT") 
				{
					if (doc_id == "")
					{
						// delete the file under:
						// UploadHelper.UploadDirectory() + username + "\\"
						System.IO.File.Delete(UploadHelper.CcaUploadDirectory + Session["username"] + "\\" + filename);
					}
					else
					{
						// get the path and filename from the database and delete it
						System.Data.DataTable mtable = SqlDbHelper.getDataDT("select AttachPath from custAttachment where ccaNum='" + doc_id + "' and attachType='" + attachment_type + "'");

						// delete in the database
						mt_trans.StartTransaction();
						mt_trans.CommandText = "delete from custAttachment where ccaNum='" + doc_id + "' and attachType='" + attachment_type + "'";

						if (mtable.Rows.Count > 0)
						{
							System.Data.DataRow mrow = mtable.Rows[0];
							System.IO.File.Delete(mrow["AttachPath"].ToString());
						}

						mtable = null;
						mt_trans.Committransaction();

						// ALSO DELETE UNDER 
						// UploadHelper.UploadDirectory() + docid + "\\"  & Filename

					}
				}
				else
				{
					if (doc_id == "")
					{
						// delete the file under:
						// UploadHelper.UploadDirectory() + username + "\\"
						System.IO.File.Delete(UploadHelper.MrktngReqDirectory + Session["username"] + "\\" + filename);
					}
					else
					{
						// get the path and filename from the database and delete it
						System.Data.DataTable mtable = SqlDbHelper.getDataDT("select filePath from marketingAttach where reqID='" + doc_id + "'");

						// delete in the database
						mt_trans.StartTransaction();
						mt_trans.CommandText = "delete from  marketingAttach where reqID='" + doc_id + "' and attachType='" + attachment_type + "'";

						if (mtable.Rows.Count > 0)
						{
							System.Data.DataRow mrow = mtable.Rows[0];
							System.IO.File.Delete(mrow["AttachPath"].ToString());
						}

						mtable = null;
						mt_trans.Committransaction();
					}
				}

				return SActionResult.Success;
			}
			catch(Exception ex)
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
		}

		[HttpPost]
		public string MarkLeadCreationDocument(
				string action_type, 
				string request_id, 
				string forward_to_asm_id,
				string forward_to_asm_name,
				string forward_to_so_id,
				string forward_to_so_name,
				string date_endorsed,
				string remarks,
				string proposed_lead_code,
				string sap_lead_code
			) {
			SQLTransaction mt_trans = new SQLTransaction();

			string cur_docstatus = "", new_docstatus = "", doc_region = "", str_encodedby = "";
			string str_emp_id = forward_to_asm_id != "" ? forward_to_asm_id : forward_to_so_id;
			string str_emp_name = forward_to_asm_name != "" ? forward_to_asm_name : forward_to_so_name;

			bool update_in_sap = false;

			// check if proposedleadcode already exist
			if (proposed_lead_code.Trim() != "") 
			{
				string str_q = "";
				if (action_type == "PROPOSE_LEAD_CODE" || action_type == "ASSIGN_SAP_LEAD_CODE") 
				{
					if (action_type == "PROPOSE_LEAD_CODE") str_q = SqlQueryHelper.check_leadcust_code(proposed_lead_code.Trim());

					if (action_type == "ASSIGN_SAP_LEAD_CODE") str_q = SqlQueryHelper.check_leadcust_code_on_create(sap_lead_code, request_id);

					DataTable acct_exist = SqlDbHelper.getDataDT(str_q);
					foreach (DataRow itm in acct_exist.Rows)
					{
						return SActionResult.Error + "PROPOSED LEAD CODE ALREADY EXIST!";
					}
				}
			}

			DataTable mtgMinutesAgreement = SqlDbHelper.getDataDT("select *, case when right(proposedchannel,1)='L' then (select 'Luzon') else (select 'Vismin') end as 'region' from customerLeadI where requestid='" + request_id + "'");
			foreach (DataRow item in mtgMinutesAgreement.Rows) 
			{
				cur_docstatus = item["status"].ToString().Trim();
				doc_region = item["region"].ToString().Trim();
				str_encodedby = item["EnCodedBy"].ToString();
			}

			_User oEncoder = new _User(str_encodedby);

			try 
			{
				mt_trans.StartTransaction();

				// DISABLED
				if (action_type == "SEND_BACK_TO_REQUESTER")
				{
					new_docstatus = "1111";
					mt_trans.CommandText = "update customerLeadI set status='" + new_docstatus + "', BkToSender='true' where RequestId='" + request_id + "'";
				}

				if (action_type == "DISAPPROVE")
				{
					mt_trans.CommandText = "update customerLeadI set status='-" + AppHelper.GetUserPositionId("chm") + "' , BkToSender='true' where RequestId='" + request_id + "'";
				}

				if (action_type == "ENDORSE")
				{
					// forward to always CSR
					// new_docstatus = "1";
					new_docstatus = AppHelper.GetUserPositionId("csr");
					mt_trans.CommandText = "update customerLeadI set status='" + new_docstatus + "', AssignTo_empId='" + str_emp_id + "', AssignTo_AsmSo='" + str_emp_name + "' where RequestId='" + request_id + "'";
				}

				if (action_type == "PROPOSE_LEAD_CODE")
				{
					// forward to FNM
					/* status will be from ? to 8 */
					new_docstatus = AppHelper.GetUserPositionId("fnm");
					mt_trans.CommandText = "update customerLeadI set status='" + AppHelper.GetUserPositionId("fnm") + "', ProposedLeadCode='" + proposed_lead_code + "' where RequestId='" + request_id + "'";
				}

				if (action_type == "ASSIGN_SAP_LEAD_CODE")
				{
					new_docstatus = "1000";
					mt_trans.CommandText = "update customerLeadI set status='1000', approved='Y', SapLeadCode='" + sap_lead_code + "' where RequestId='" + request_id + "'";

					update_in_sap = true;
				}

				mt_trans.Committransaction();

				// send mail
				SendMailLead(doc_region, new_docstatus, request_id, str_emp_id);

				// SaveToLog
				AppHelper.SaveToLOg("LCD", request_id, action_type, remarks, Session["username"].ToString(), new_docstatus, cur_docstatus);

				// update to sap
				if (update_in_sap == true) 
				{
					UPDATE_IN_SAP(request_id);
				}

				return SActionResult.Success + request_id;

			}
			catch (Exception ex) 
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}

		}

		private void SendMailLead(string brnch, string newDocStatus, string requestid, string emp_id) {
			DataTable email_list = null, customerLeadI = null;
			string mail_body = "", mail_subject = "", str_proposedchannel = "", str_docencoder = "", tmp_new_docstatus = "";

			_Document oDucumnt = new _Document("LDI", requestid);

			DataTable dt_proposedchannel = SqlDbHelper.getDataDT("select ProposedChannel, EnCodedby from customerLeadi where requestid='" + requestid + "'");
			foreach (DataRow itm in dt_proposedchannel.Rows) 
			{
				str_proposedchannel = itm["ProposedChannel"].ToString().Trim();
				str_docencoder = itm["EnCodedby"].ToString().Trim();
			}

			if (newDocStatus == "1000")
			{
				mail_subject = "Lead Creation (" + requestid + ") has been Approved.";
			}
			else 
			{
				if (Convert.ToInt32(newDocStatus) < 0)
				{
					mail_subject = "Lead Creation (" + requestid + ") has been Disapproved.";
				}
				else
				{
					tmp_new_docstatus = newDocStatus;
					
					mail_subject = "Lead Creation (" + requestid + ") is waiting for your approval (" + AppHelper.GetLdbIDocStatMsg(tmp_new_docstatus) + ")";
				}
			}

			if (oDucumnt.Area == "" && oDucumnt.Channel == "")
			{
				// GET EMAIL USING PROPOSEDCHANNEL
				// ONLY FOR CHM
				email_list = GetDestEmail(newDocStatus, str_proposedchannel);
			}
			else
			{
				email_list = GetDestEmail(newDocStatus, oDucumnt.Region, oDucumnt.Area, oDucumnt.Channel);
			}

			mail_body = AppHelper.Arms_Url + "?id=" + requestid + "&doctype=lcd";
			try 
			{
				foreach (DataRow item in email_list.Rows) 
				{
					// place to arms.dbo.scheduler
					ARMS_W.SkelClass.cls_email c_e = new SkelClass.cls_email();

					c_e.email_from = "ARMS@matimco.com";
					c_e.email_to = item["email"].ToString();
					c_e.email_subject = mail_subject;
					c_e.email_content = mail_body;

					AppHelper.InsertToTable("Scheduler", new Dictionary<string, object>() { 
						{"Type", "EMAIL"}
						,{"Data", Parser.toJson(c_e) }
					});

					#region TO REMOVE
					//MailHelper.SendMail("ARMS@matimco.com", item["email"].ToString(), mail_subject, mail_body);
					#endregion
				}
			}
			catch (Exception ex) 
			{ 

			}

		}
		
		public ActionResult UploadDialogBoxMarketingProgram()
		{
			return View();
		}

		public ActionResult UploadDialogBoxForexcel()
		{
			return View();
		}

		// FOR VIEWWING/DOWNLOADING UPLOADED FILES
		[AuthorizeUsr]
		public ActionResult DownloadFile(string doctype, string fileName, string id) 
		{
			// fileName = StringHelper.ReCodeCharacters(fileName);

			string doc_path = "";

			if (doctype == "CCA") { doc_path = UploadHelper.CcaUploadDirectory + id + "\\"; }
			if (doctype == "MMA") { doc_path =UploadHelper.MmaUploadDirectory + id + "\\"; }
			if (doctype == "MKTP") { doc_path = UploadHelper.MktpUploadDirectory + id +"\\";}
			if (doctype == "LDB") { doc_path = UploadHelper.LdbUploadDirectory + id + "\\"; }
			if (doctype == "MKTR") { doc_path = UploadHelper.MrktngReqDirectory + id + "\\"; }

			if (System.IO.File.Exists(doc_path + fileName))
			{

				// get extension
				string ext_file = System.IO.Path.GetExtension(fileName).Replace(".", "");

				string MimeType = File(doc_path + StringHelper.UrlDecode(fileName), "application/" + ext_file, fileName).ContentType;
				return File(doc_path + StringHelper.UrlDecode(fileName), MimeType, fileName);
			}
			else
			{
				// TODO
				// ERROR: File not found.!
				return RedirectToAction("Index");
			}
		}

		public string GetAcctDetail(string _str_data, string channel) {
			string strquery = "";
			DataTable tmp_table;
			_User CurrentUser = new _User(Session["username"].ToString());

			if (_str_data == "ListOfDirAcccod")
			{
				strquery = @"
					SELECT a.CardCode, a.Cardname 
					FROM mtc_vw_BRQD a, customerheader b WHERE LEFT(a.cardcode,2) IN ('CV', 'CL') AND a.cardcode COLLATE SQL_Latin1_General_CP850_CI_AS = b.acctcode 
					AND RTRIM(LTRIM(b.acctType))='DIRECT' and b.status = '1000' and a.channel='" + channel + "' ORDER BY a.cardname";
			}

			if (_str_data == "ListOfChannel")
			{
				if (CurrentUser.HasPositionOf("chm") != -1 || CurrentUser.HasPositionOf("ca") != -1)
				{
					strquery = "SELECT '', a.channel FROM channelgroup a, apprvrdesig b, userheader c WHERE a.channel = b.channel AND b.counterid = c.counterid AND c.username='" + Session["username"].ToString() + "' group by a.channel order by a.channel ";
				}
				else 
				{
					strquery = "select '', channel from channelgroup group by channel order by channel ";
				}
			}

			try
			{
				tmp_table = SqlDbHelper.getDataDT(strquery);
				return "00:" + StringHelper.ConvertDataTableToString(tmp_table);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message; 
			}

		}

		// Test Uploada for marketing Program
		public ActionResult TestUploadMPK()
		{
			HttpFileCollectionBase File = Request.Files;
			string extension = "";

			extension = Path.GetFileName(File[0].FileName);

			string[] fileFormat = 
			{
				".pdf", ".rpt", ".tif", ".png", ".jpg", ".jpeg", ".gif",
				".docx", ".docm", ".dotx", ".dotm", ".xlsx", ".xlsm",
				".xltx", ".xltm", ".xlsb", ".xlam", ".pptx", ".pptm",
				".potx", ".potm", ".ppam", ".ppsx", ".ppsm", ".sldx",
				".sldm", ".thmx", ".doc", ".ppt", ".xls", ".xlsb", ".pst",
				".xml", ".vdx", ".vsx", ".vtx", ".xsn", ".zip", ".rar"
			};
	  
			try
			{
				// check extension
				if (Array.IndexOf(fileFormat, Path.GetExtension(extension)) > -1)
				{
					Directory.CreateDirectory(UploadHelper.MktpUploadDirectory + Session["username"]);
					File[0].SaveAs(UploadHelper.MktpUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));

					ViewData["fname"] = Path.GetFileName(File[0].FileName); 
					ViewData["ftype"] = File[0].InputStream.Length;
					ViewData["fsize"] = File[0].ContentType;
				}
				else
				{
					throw new System.OperationCanceledException(" The file can only be a PDF, RAR, ZIP, Image files(tif, png, jpg/jpeg, gif) and Microsoft Office Files.");
				}
			}
			catch (Exception ex)
			{
				ViewData["ferror"] = ex.Message;
				ViewData["fname"] = "";
				ViewData["ftype"] = "";
				ViewData["fsize"] = "";
			}
			
			return View("UploadResult");
		}

		public ActionResult TestUploadEXCEL()
		{
			string FileName = "";
			ExcelReader EXLREADER = new ExcelReader();
			HttpFileCollectionBase File = Request.Files;
			try
			{
				// check extension
				if (Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == "xlsx" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".rpt")
				{
					Directory.CreateDirectory(UploadHelper.MktpUploadDirectory + Session["username"]);
					FileName = UploadHelper.MktpUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName);
					File[0].SaveAs(FileName);

					OleDbDataReader excelreader;
					OleDbDataReader acct_name_area, final_reader;

					DataTable excel_datatable = null;
					DataTable acct_name_area_datatable = null, final_datatable = null;

					excel_datatable = SqlDbHelper.getExclDataDt("Select * from [sheet1$] ", UploadHelper.MktpUploadDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));
					
					string str_query = "", str_query1 = "";

					foreach (DataRow itm in excel_datatable.Rows) 
					{
						acct_name_area_datatable = SqlDbHelper.getDataDT("select cardname, areaname from SAPSERVER.MATIMCO.dbo.abmmw_vw_ocrd where cardcode='" + itm[0].ToString() + "'");
						string cardname = "", areaname = "", cardcode = "", amnt_alloc = "";

						cardcode = itm[0].ToString();
						amnt_alloc = itm[1].ToString();

						foreach(DataRow itm1 in acct_name_area_datatable.Rows)
						{
							cardname = itm1[0].ToString();
							areaname = itm1[1].ToString();
						}

						if (str_query != "") str_query = str_query + " union ";

						if (cardname == "")
						{
							str_query = str_query + "select '" + cardcode + "', '" + StringHelper.InsertQoutes(cardname) + "', '" + areaname + "', '" + amnt_alloc + "'";
							// ERROR CARDCODE NOT FOUND
						}
						else
						{
							str_query = str_query + "select '" + cardcode + "', '" + StringHelper.InsertQoutes(cardname) + "', '" + areaname + "', '" + amnt_alloc + "'";
						}
					}

					final_datatable = SqlDbHelper.getDataDT(str_query);        
					
					ViewData["listofAccounts"] = StringHelper.ConvertDataTableToString(final_datatable);
				}
				else
				{
					throw new System.OperationCanceledException("The file can only be a PDF file");
				}
			}
			catch (Exception ex)
			{
				ViewData["ferror"] = ex.Message;
				ViewData["fname"] = "";
				ViewData["ftype"] = "";
				ViewData["fsize"] = "";
			}

			return View("UploadResultForMarketing");
		}

		public ActionResult UploadResultForMarketing()
		{
			return View();
		}

		public string Details(string str_data, string acctCode)
		{
			string strquery = "";
			OleDbDataReader tmp_reader = null;

			if (str_data == "list_of_bounced_checks")
			{
				strquery = "" +
					"SELECT convert(varchar(10), CntctDate, 101), CardCode, CardName, Notes  FROM mtc_vw_DisChecks WHERE CardCode='" + acctCode + "'" + "";
			}
			try
			{

				tmp_reader = SqlDbHelper.getData(strquery);
				return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}
		
		}

		public string ListOfAccts(string str_data, string filter, string filter_by)
		{
			string strquery = "";
			OleDbDataReader tmp_reader = null;

			if (str_data == "ListOfAcctCode")
			{
				if (filter_by == "area"){
					strquery = "" +
					   "SELECT cardcode FROM arms_vw_individualftmsellin WHERE areaname='" + filter + "'" + "";
				
				}
				if (filter_by == "channel")
				{
					strquery = "" +
					   "SELECT cardcode FROM arms_vw_individualftmsellin WHERE channel='" + filter + "'" + "";

				}
				if (filter_by == "brand")
				{
					strquery = "" +
					   "SELECT cardcode FROM arms_vw_individualftmsellin WHERE u_brand='" + filter + "'" + "";

				}
				if (filter_by == "region")
				{
					strquery = "" +
					   "SELECT cardcode FROM arms_vw_individualftmsellin WHERE LEFT(cardcode,2)='" + filter + "'" + "";

				}
				if (filter_by == "NONE")
				{
					strquery = "" +
					   "SELECT cardcode FROM arms_vw_individualftmsellin";

				}

			}
			try
			{

				tmp_reader = SqlDbHelper.getData(strquery);
				return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}

		}

		public string GetEmployeeDetails(string _str_data)
		{
			string strquery = "";
			OleDbDataReader tmp_reader = null;

			if (_str_data == "ListOfEmployee")
			{
				strquery = "" +
					"SELECT in_emp_id, " +
						   " RTRIM(lastName), RTRIM(firstName), RTRIM(emailAdd)" +
				   " FROM MTC_vw_empListHRIS ORDER BY lastName";
				//strquery = ""+
				//    "SELECT emp_id, " +
				//        " RTRIM(last_name), RTRIM(first_name) "+
				//        " FROM MTC_vw_empListHRIS ORDER BY last_name";
			}

			if (_str_data == "ListRoleID")
			{

				strquery = "" + "SELECT '', roleName FROM dbo.apprvrRole order by roleName" + "";

			}

			try
			{

				tmp_reader = SqlDbHelper.getData(strquery);
				return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}

		}

		public string ListOfAcctDetails(string _str_data)
		{
			string strquery = "";
			OleDbDataReader tmp_reader = null;

			if (_str_data == "ListOfAcctDetails")
			{
				strquery = @"
					select '1M', 'LOW TIER' union 
					select '2M', 'MID TIER' union 
					select '3M', 'TOP TIER' ";
			}

			if (_str_data == "ListRoleID")
			{
				strquery = "" + "SELECT '', roleName FROM dbo.apprvrRole order by roleName" + "";
			}

			try
			{
				tmp_reader = SqlDbHelper.getData(strquery);
				return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}
		}

		public string ListOfAcctCode(string _str_data, string region)
		{
			string strquery = "";
			OleDbDataReader tmp_reader = null;

			if (_str_data == "ListRoleID")
			{
				strquery = "" + "SELECT '', acctcode from customerheader where region='" + region + "'" + "";
			}

			try
			{
				tmp_reader = SqlDbHelper.getData(strquery);
				return "00:" + StringHelper.ConvertReaderToString(tmp_reader);
			}
			catch (Exception ex)
			{
				return "01:" + ex.Message;
			}

		}

		public ActionResult marketingReq()
		{
			HttpFileCollectionBase File = Request.Files;
			try
			{
				// check extension
				if (Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".pdf" || Path.GetFileName(File[0].FileName).Substring(Path.GetFileName(File[0].FileName).Length - 4, 4) == ".rpt")
				{
					Directory.CreateDirectory(UploadHelper.MrktngReqDirectory + Session["username"]);
					File[0].SaveAs(UploadHelper.MrktngReqDirectory + Session["username"] + "\\" + Path.GetFileName(File[0].FileName));

					ViewData["fname"] = Path.GetFileName(File[0].FileName);
					ViewData["ftype"] = File[0].InputStream.Length;
					ViewData["fsize"] = File[0].ContentType;
				}
				else
				{
					throw new System.OperationCanceledException("The file can only be a PDF file");
				}
			}
			catch (Exception ex)
			{
				ViewData["ferror"] = ex.Message;
				ViewData["fname"] = "";
				ViewData["ftype"] = "";
				ViewData["fsize"] = "";
			}

			return View("UploadResult");
		}

		private DataTable GetDestEmail(string new_docstatus, string region, string area, string channel) 
		{
			DataTable em_list = null;

			string strQuery = "";

			IList<string> RegionUsers = new List<string>();
			RegionUsers.Add("csr");
			RegionUsers.Add("CSR");
			RegionUsers.Add("fnm");
			RegionUsers.Add("FNM");
			RegionUsers.Add("cnc");
			RegionUsers.Add("CNC");
			RegionUsers.Add("Finance Mgr.");

			IList<string> ChannelUsers = new List<string>();
			ChannelUsers.Add("chm");
			ChannelUsers.Add("CHM");
			ChannelUsers.Add("Channel Mgr.");

			IList<string> AreaUsers = new List<string>();
			AreaUsers.Add("asm");
			AreaUsers.Add("ASM");

			IList<string> NolFilterUsers = new List<string>();
			NolFilterUsers.Add("vpbsm");
			NolFilterUsers.Add("VPBSM");
			NolFilterUsers.Add("vptfi");
			NolFilterUsers.Add("VPTFI");

			// REGION
			if (RegionUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
			{
				string poss = "";
				if (AppHelper.GetUserPosition(new_docstatus) == "csr") poss = "'csr','CSR'";

				if (AppHelper.GetUserPosition(new_docstatus) == "cnc") poss = "'cnc','CNC'";

				if (AppHelper.GetUserPosition(new_docstatus) == "fnm") poss = "'fnm','FNM','Finance Mgr.'";

				strQuery = strQuery + "" +
					"select a.email from apprvrDesig a, userheader b , apprvrRole c " +
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
					"left(a.branch,1) = '" + region.Substring(0, 1) + "' " +
					"group by a.email" +
					"";

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			// CHANNEL
			if (ChannelUsers.IndexOf(AppHelper.GetUserPosition(new_docstatus)) != -1)
			{
				string poss = "";
				if (AppHelper.GetUserPosition(new_docstatus) == "chm") poss = "'chm','CHM'";

				strQuery = strQuery + "" +
					"select a.email from apprvrDesig a, userheader b , apprvrRole c " +
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") and " +
					"a.channel = '" + channel + "' " +
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
					"a.area = '" + area + "' " +
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
					"where a.counterid=b.counterid and c.roleid=a.roleid and c.rolecode in (" + poss + ") " +
					"group by a.email" +
					"";

				em_list = SqlDbHelper.getDataDT(strQuery);
			}

			// IF DESAPPROVED


			return em_list;
		}

		// FOR CUSTOMERLEADI USE ONYL
		// CHM 
		private DataTable GetDestEmail(string new_docstatus, string channel_group_name) 
		{
			DataTable _email_list = null;

			string poss = "'Channel Mgr.', 'chm', 'CHM'";
			string strQ = @"
				select a.email 
				from apprvrDesig a, apprvrRole b, userHeader c , ChannelGroup d 
				where a.roleid=b.roleid and c.counterid=a.counterid and d.channel=a.channel 
				and b.rolecode in (" + poss + ") and d.grp_name = '" + channel_group_name + "' group by email ";

			_email_list = SqlDbHelper.getDataDT(strQ);

			return _email_list;
		}

		public string GetFilteredList(string _str_data, string par1 = "", string par2 = "", string tArea = "", string array1 = "") 
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
					strquery = "" +
						"select a.slpcode, a.slpname from " +
						"SAPSERVER.MATIMCO.dbo.OSLP a inner join SAPSERVER.MATIMCO.dbo.oter z on a.u_area=left(z.descript,5) " +
						"inner join SAPSERVER.MATIMCO.dbo.oter x on z.parent=x.territryid left join ChannelGroup y on " +
						"y.area collate SQL_Latin1_General_CP850_CI_AS=z.descript " +
						"where left(z.descript,2) = 'AR' and a.u_active='Y' and a.u_position='AO' " +
						"and (case when charindex('LUZON',x.descript) > 0 then 'LUZON' when charindex('VISMIN',x.descript) > 0 then 'VISMIN' else '' end) in (" + str_region + ")" +
						"order by a.slpname asc";
				}

				if (_str_data == "ListOfChannel") 
				{
					if (str_region.IndexOf("VISMIN") != -1) 
					{
						strquery = "" +
						"select 'GTV', 'GTV' union " +
						"select 'ISV', 'ISV'  " +
						"";
					}

					if (str_region.IndexOf("LUZON") != -1)
					{
						strquery = "" +
						"select 'GTL', 'GTL' union " +
						"select 'ISL', 'ISL'  " +
						"";
					}

					if (str_region.IndexOf("LUZON") != -1 && str_region.IndexOf("VISMIN") != -1)
					{
						strquery = "" +
							"select 'GTL', 'GTL' union " +
							"select 'ISL', 'ISL' union " +
							"select 'GTV', 'GTV' union " +
							"select 'ISV', 'ISV'  " +
							"";
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

		private void UPDATE_IN_SAP(string docid)
		{
			ARMS_W.SkelClass.sap_businesspartner s_b = new SkelClass.sap_businesspartner();
			s_b.docid = docid.Trim().ToUpper();
			s_b.doctype = "LEAD";
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

		public string FnmDisapproveLead(string request_id) 
		{
			SQLTransaction mt_trans = new SQLTransaction();

			try 
			{
				if (request_id == "" || request_id == null) 
				{
					throw new Exception("Error updating record");
				}

				mt_trans.StartTransaction();
				mt_trans.UpdateTo("customerLeadI", 
					new Dictionary<string, object>() { 
						{"status", "-" + AppHelper.GetUserPositionId("fnm") }
					}, 
					new Dictionary<string, object>() { 
						{"Requestid", request_id}
					});
				
				mt_trans.Committransaction();
				return SActionResult.Success + request_id;
			}
			catch (Exception ex) 
			{
				mt_trans.RollbackTransaction();
				return SActionResult.Error + ex.Message;
			}
		}

	}
}
