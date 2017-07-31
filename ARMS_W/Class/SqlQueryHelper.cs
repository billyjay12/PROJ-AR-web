using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ARMS_W.Class
{
	public class SqlQueryHelper
	{

		public static string GenerateNewCCANumber()
		{
			return "" +
				"SELECT isnull(( SELECT 'CCAN' + replicate('0', 7 - len( cast( (max(right(ccanum,16)) + 1) as nvarchar )" +
				")) + cast( (max(right(ccanum,16)) + 1) as nvarchar ) from dbo.customerHeader) , 'CCAN0000001')";
		}

		public static string LifOfAccounts(string cardcode = "", string cardname = "", string datecreated = "", string doccreator = "", string status = "", string area = "", string forSapUpdate = "")
		{
			string search_option = "";
			if (cardcode != "")
			{
				cardcode = cardcode.Replace("'", "''");
				search_option = " and a.acctcode like'%" + cardcode + "%'";
				status = "";
			}

			if (cardname != "")
			{
				cardname = cardname.Replace("'", "''");
				search_option = " and a.acctname like '%" + cardname + "%'";
				status = "";
			}

			if (area != "")
			{
				search_option = " and c.area like'%" + area + "%'";
				status = "";
			}

			if (forSapUpdate != "") 
			{
				search_option = " and a.ccanum in (select ccanum from custSapUpdate where IsUpdated is null and tag = 'CUSTOMER_CHANGES')";
				status = "";
			}

			if (status != "") search_option = " and b.status = '" + status + "'";
			else status = "";
			
			return @"
				SELECT a.ccanum, case when a.sapacctcode is null then a.acctcode else a.sapacctcode end as 'acctcode', a.acctname, a.status as 'status1', b.status as 'status2', a.region, c.channel
				, case when b.area is not null and b.area != '' then b.area else c.area end as 'area'
				, b.hasModified, b.is_sent_back , 
				d.creator_uname, convert(varchar(10), d.mdate, 101) as 'mdate', a.acctOffcr 
				from dbo.customerHeader a inner join proposedChangesCA b on a.ccanum=b.ccanum left join ChannelGroup c on c.area=a.area 
				left join document_history d on d.docid=a.ccanum and d.mtype = 'NEW_CUSTOMER' where a.status='1000'" +
				search_option +
				" order by cast(right(a.ccanum,16) as int)";
		}

		public static string ListOfeMAT()
		{
			return "SELECT a.eMatno, a.encodedBy, a.buyer, b.stateDesc as status, a.ematDoc FROM eMAt a INNER JOIN approvalState b ON a.status = b.stateID AND b.docType=9";
		}

		public static string ListOfmarketingProgram()
		{
			return "SELECT a.programNo, a.progName,a.brand, b.stateDesc as status FROM mrktProgram a INNER JOIN approvalState b ON a.status=b.stateID AND b.docType=6";
		}

		public static string ListOfUserAcess() {
			return "SELECT a.roleID, c.roleName ,b.docDescription as docType, a.accessRights  from  roleAccess  a inner join documentTypes b on a.docType= b.docType inner join apprvrRole c on a.roleID=c.roleID order by roleID";
		}

		public static string ListOfRoles(){
			return "SELECT roleID, roleName FROM  apprvrRole ORDER BY roleID";
		}

		public static string ListOfDesigApprvr()
		{
			return "SELECT b.roleID,b.roleName,a.name,a.email,a.branch,a.brand FROM apprvrDesig a INNER JOIN apprvrRole b ON a.roleID=b.roleID ORDER BY a.roleID";
		}
		public static string ListOfMarketingProgram(string brand)
		{
			return "SELECT U_Brand, a.encodedBy, a.buyer, b.stateDesc as status FROM eMAt a INNER JOIN approvalState b ON a.status = b.stateID AND b.docType=9";
		}

		public static string listOfMarketingReq()
		{
			return "SELECT reqID,encodeBy,requestedBy,b.stateDesc FROM marktingRequest a INNER JOIN approvalState b ON a.status=b.stateID WHERE b.docType=7";
		}

		public static string LifOfLeadAccounts()
		{
			return "SELECT requestid, name, address, convert(varchar(10), inqdate, 101) from dbo.customerLeadI";
		}

		public static string tablecontain(string ccaNum)
		{
			return "" +
				"SELECT " +
				"isnull((SELECT top 1 'true ' from busTypeHdr where ccanum='" + ccaNum + "'), 'false') as 'busTypeHdr',  " +
				"isnull((SELECT top 1 'true ' from busTypeDtl where ccanum='" + ccaNum + "'), 'false') as 'busTypeDtl',  " +
				"isnull((SELECT top 1 'true ' from custAttachment where ccanum='" + ccaNum + "'), 'false') as 'custAttachment',  " +
				"isnull((SELECT top 1 'true ' from custAttachment where ccanum='" + ccaNum + "' and attachType='AOI'), 'false') as 'custAttachment_AOI',  " +
				"isnull((SELECT top 1 'true ' from custAttachment where ccanum='" + ccaNum + "' and attachType='FS'), 'false') as 'custAttachment_FS',  " +
				"isnull((SELECT top 1 'true ' from custAttachment where ccanum='" + ccaNum + "' and attachType='ITR'), 'false') as 'custAttachment_ITR',  " +
				"isnull((SELECT top 1 'true ' from custAttachment where ccanum='" + ccaNum + "' and attachType='BIR'), 'false') as 'custAttachment_BIR',  " +
				"isnull((SELECT top 1 'true ' from custAttachment where ccanum='" + ccaNum + "' and attachType='BP'), 'false') as 'custAttachment_BP',  " +
				"isnull((SELECT top 1 'true ' from custBusHdr where ccanum='" + ccaNum + "'), 'false') as 'custBusHdr',  " +
				"isnull((SELECT top 1 'true ' from customerHeader where ccanum='" + ccaNum + "'), 'false') as 'customerHeader',  " +
				"isnull((SELECT top 1 'true ' from custOutlets where ccanum='" + ccaNum + "'), 'false') as 'custOutlets',  " +
				"isnull((SELECT top 1 'true ' from customerEvents where ccanum='" + ccaNum + "'), 'false') as 'customerEvents',  " +
				"isnull((SELECT top 1 'true ' from empInventory where ccanum='" + ccaNum + "'), 'false') as 'empInventory',  " +
				"isnull((SELECT top 1 'true ' from majorCustomer where ccanum='" + ccaNum + "'), 'false') as 'majorCustomer',  " +
				"isnull((SELECT top 1 'true ' from otherBusiness where ccanum='" + ccaNum + "'), 'false') as 'otherBusiness',  " +
				"isnull((SELECT top 1 'true ' from otherProducts where ccanum='" + ccaNum + "'), 'false') as 'otherProducts',  " +
				"isnull((SELECT top 1 'true ' from otherWoodSupp where ccanum='" + ccaNum + "'), 'false') as 'otherWoodSupp',  " +
				"isnull((SELECT top 1 'true ' from products where ccanum='" + ccaNum + "'), 'false') as 'products',  " +
				"isnull((SELECT top 1 'true ' from depositoryBank where ccanum='" + ccaNum + "'), 'false') as 'depositoryBank',  " +
				"isnull((SELECT top 1 'true ' from otherBusiness where ccanum='" + ccaNum + "'), 'false') as 'otherBusiness'," +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "'), 'false') as 'propsedPrice', " +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "' and brandtype='MW'), 'false') as 'propsedPriceMW', " +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "' and brandtype='WW'), 'false') as 'propsedPriceWW', " +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "' and brandtype='PWR'), 'false') as 'propsedPricePWR', " +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "' and brandtype='GW'), 'false') as 'propsedPriceGW', " +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "' and brandtype='PWF'), 'false') as 'propsedPricePWF', " +
				"isnull((SELECT top 1 'true ' from propsedPrice where ccanum='" + ccaNum + "' and brandtype='TW'), 'false') as 'propsedPriceTW', " +
				"isnull((SELECT top 1 'true ' from custCredInves where ccanum='" + ccaNum + "'), 'false') as 'custCredInves' " +
				"";
		}

		public static string InsertNewMarktingReq(
			   string encodeBy ,
			   string acctCode ,
			   string acctName ,
			   string acctAdd ,
			   string acctArea,
			   string acctOfficer,
			   string requestedBy,
			   string brand ,
			   string category ,
			   string type,
			   string setUpDate ,
			   string size  ,
			   string value ,
			   string availDeploy ,
			   string actualDeploy
			) {
				return "EXEC MTC_sp_insertMrktngReqHeader " +
					"" + encodeBy + ","
					+ acctCode + ","
					+ acctName + ","
					+ acctAdd + ","
					+ acctArea + ","
					+ acctOfficer + ","
					+ requestedBy + ","
					+ brand + ","
					+ category + ","
					+ type + ","
					+ setUpDate + ","
					+ size + ","
					+ value + ","
					+ availDeploy + ","
					+ actualDeploy + ")";
		}

		public static string InsertMrktngReqDtls(string reqID, string stipulation)
		{
		  return "EXEC MTC_sp_insertMrktngReqOtherStip '"+ reqID + "','" + stipulation + "'";
		}

		public static string InsertRoleName(string roleName)
		{ 
		  return "EXEC MTC_sp_insertApprvrRole '"+ roleName +"'";
		}

		public static string InsertApprvrDesig(string roleID,
											   string branch,
											   string channel,
											   string area,
											   string name,
											   string eMail,
											   string brand,
											   string empID) 
		{
			return "EXEC MTC_sp_insertApprvrDesig '" + roleID + "," + branch + "," + name + "," + eMail + "," + brand + "," + channel + "," + area + "," + empID;
		}

		public static string InsertUsserAccess(Int64 roleID, Int64 docType, string accessRights)
		{
		   // return "MTC_sp_insertMrktngReqOtherStip '" + reqID + "','" + stipulation + "'";

			return "" + "INSERT INTO userAcces " +
						"(roleId " +
						", docType " +
						", accessRights)"+
						"VALUES" +
						"(" + roleID + " " +
						", " + docType + " " +
						",'" +  accessRights + "' )" +
						"";
		}

		public static string Insert_marketingprog(
				string programNo,
				string status,
				string progName,
				string progType,
				string brand,
				string targetChannel,
				string targetArea,
				string backGround,
				string objective,
				string measures,
				string preparedBy,
				double totalAmtResources,
				double totalAmt
			){
				return "" +
				   "INSERT INTO mrktProgram " +
				   "(programNo " +
				   ",progName " +
					",status " +
				   ",progType " +
				   ",brand " +
				   ",targetChannel " +
				   ",targetArea " +
				   ",backGround " +
				   ",objective " +
				   ",measures " +
				   ",preparedBy " +
				   ",totalAmtResources " +
				   ",totalAmt) " +
				   "VALUES " +
				   "('" + programNo + "' " +
				   ",'" + progName + "' " +
				   ",'" + status + "' " +
				   ",'" + progType + "' " +
				   ",'" + brand + "' " +
				   ",'" + targetChannel + "' " +
					",'" + targetArea + "' " +
				   ",'" + backGround + "' " +
				   ",'" + objective + "' " +
				   ",'" + measures + "' " +
				   ",'" + preparedBy + "' " +
					"," + totalAmtResources + " " +
				   "," + totalAmt + ") " +
				   "";
		}

		public static string Update_customerLeadI(
				string Name,
				string InqDate,
				string Address,
				string ContactNo,
				string E_mail,
				string IsInquired_MWBrand,
				string IsInquired_WWBrand,
				string IsInquired_PWBrand,
				string IsInquired_TWBrand,
				string IsInquired_GWBrand,
				string IsInquired_Framing,
				string IsInquired_Mouldings,
				string IsInquired_Floorings,
				string IsInquired_DoorJambs,
				string IsInquired_Panellings,
				string IsInquired_EngdItm,
				string IsInquired_Decking,
				string IsInquired_StairComp,
				string IsInquired_Others,
				string Remarks,
				string ProposedChannel,
				string Status,
				string EnCodedby,
				string WebContactFrm,
				string ExhibitName,
				string ExhibitDate,
				string ExhibitAddress,
				string ReferedBy,
				string SalesOfficer,
				string OtherSources,
				string BkToSender,
				string RequestId
			)
		{
			return "" +
				"UPDATE customerLeadI " +
				"SET Name = '" + Name + "' " +
				",InqDate = '" + InqDate + "' " +
				",Address = '" + Address + "' " +
				",ContactNo = '" + ContactNo + "' " +
				",E_mail = '" + E_mail + "' " +
				",IsInquired_MWBrand = '" + IsInquired_MWBrand + "' " +
				",IsInquired_WWBrand = '" + IsInquired_WWBrand + "' " +
				",IsInquired_PWBrand = '" + IsInquired_PWBrand + "' " +
				",IsInquired_TWBrand = '" + IsInquired_TWBrand + "' " +
				",IsInquired_GWBrand = '" + IsInquired_GWBrand + "' " +
				",IsInquired_Framing = '" + IsInquired_Framing + "' " +
				",IsInquired_Mouldings = '" + IsInquired_Mouldings + "' " +
				",IsInquired_Floorings = '" + IsInquired_Floorings + "' " +
				",IsInquired_DoorJambs = '" + IsInquired_DoorJambs + "' " +
				",IsInquired_Panellings = '" + IsInquired_Panellings + "' " +
				",IsInquired_EngdItm = '" + IsInquired_EngdItm + "' " +
				",IsInquired_Decking = '" + IsInquired_Decking + "' " +
				",IsInquired_StairComp = '" + IsInquired_StairComp + "' " +
				",IsInquired_Others = '" + IsInquired_Others + "' " +
				",Remarks = '" + Remarks + "' " +
				",ProposedChannel = '" + ProposedChannel + "' " +
				",Status='" + Status + "' " +
				",EnCodedby = '" + EnCodedby + "' " +
				",WebContactFrm = '" + WebContactFrm + "' " +
				",ExhibitName = '" + ExhibitName + "' " +
				",ExhibitDate = '" + ExhibitDate + "' " +
				",ExhibitAddress = '" + ExhibitAddress + "' " +
				",ReferedBy = '" + ReferedBy + "' " +
				",SalesOfficer = '" + SalesOfficer + "' " +
				",OtherSources = '" + OtherSources + "' " +
				",BkToSender = '" + BkToSender + "' " +
				"WHERE RequestId = '" + RequestId + "' " +
				"";
		}

		public static string Update_proposedChangesCA(
				string ccaNum,
				string acctName,
				string acctOffcr,
				string territory,
				string area,
				string region,
				string regBusName,
				string bussAdd,
				string delAdd,
				string propCredTerms,
				string propCredLimit,
				string CredTermRemarks,
				string CredLimitRemarks,
				string pl_priceListCode_mw,
				string pl_codeDesc_mw,
				string pl_CommDisc_mw,
				string pl_remarks_mw,
				string pl_priceListCode_ww,
				string pl_codeDesc_ww,
				string pl_CommDisc_ww,
				string pl_remarks_ww,
				string pl_priceListCode_pwf,
				string pl_codeDesc_pwf,
				string pl_CommDisc_pwf,
				string pl_remarks_pwf,
				string pl_priceListCode_pwr,
				string pl_codeDesc_pwr,
				string pl_CommDisc_pwr,
				string pl_remarks_pwr,
				string pl_priceListCode_gw,
				string pl_codeDesc_gw,
				string pl_CommDisc_gw,
				string pl_remarks_gw,
				string pl_priceListCode_tw,
				string pl_codeDesc_tw,
				string pl_CommDisc_tw,
				string pl_remarks_tw,
				string status,
				string changesType,
				string routeType
			)
		{
			return "" +
				"UPDATE proposedChangesCA " +
				"SET acctName = '" + acctName + "' " +
				",acctOffcr = '" + acctOffcr + "' " +
				",territory = '" + territory + "' " +
				",area = '" + area + "' " +
				",region = '" + region + "' " +
				",regBusName = '" + regBusName + "' " +
				",bussAdd = '" + bussAdd + "' " +
				",delAdd = '" + delAdd + "' " +
				",propCredTerms = '" + propCredTerms + "' " +
				",propCredLimit = '" + propCredLimit + "' " +
				",CredTermRemarks = '" + CredTermRemarks + "' " +
				",CredLimitRemarks = '" + CredLimitRemarks + "' " +
				",pl_priceListCode_mw = '" + pl_priceListCode_mw + "' " +
				",pl_codeDesc_mw = '" + pl_codeDesc_mw + "' " +
				",pl_CommDisc_mw = '" + pl_CommDisc_mw + "' " +
				",pl_remarks_mw = '" + pl_remarks_mw + "' " +
				",pl_priceListCode_ww = '" + pl_priceListCode_ww + "' " +
				",pl_codeDesc_ww = '" + pl_codeDesc_ww + "' " +
				",pl_CommDisc_ww = '" + pl_CommDisc_ww + "' " +
				",pl_remarks_ww = '" + pl_remarks_ww + "' " +
				",pl_priceListCode_pwf = '" + pl_priceListCode_pwf + "' " +
				",pl_codeDesc_pwf = '" + pl_codeDesc_pwf + "' " +
				",pl_CommDisc_pwf = '" + pl_CommDisc_pwf + "' " +
				",pl_remarks_pwf = '" + pl_remarks_pwf + "' " +
				",pl_priceListCode_pwr = '" + pl_priceListCode_pwr + "' " +
				",pl_codeDesc_pwr = '" + pl_codeDesc_pwr + "' " +
				",pl_CommDisc_pwr = '" + pl_CommDisc_pwr + "' " +
				",pl_remarks_pwr = '" + pl_remarks_pwr + "' " +
				",pl_priceListCode_gw = '" + pl_priceListCode_gw + "' " +
				",pl_codeDesc_gw = '" + pl_codeDesc_gw + "' " +
				",pl_CommDisc_gw = '" + pl_CommDisc_gw + "' " +
				",pl_remarks_gw = '" + pl_remarks_gw + "' " +
				",pl_priceListCode_tw = '" + pl_priceListCode_tw + "' " +
				",pl_codeDesc_tw = '" + pl_codeDesc_tw + "' " +
				",pl_CommDisc_tw = '" + pl_CommDisc_tw + "' " +
				",pl_remarks_tw = '" + pl_remarks_tw + "' " +
				",status = '" + status + "' " +
				",changesType = '" + changesType + "' " +
				",routeType = '" + routeType + "' " +
				"WHERE ccaNum = '" + ccaNum + "' " +
				"";
		}

		public static string GenerateEMATno()
		{
			return "" +
			   "SELECT isnull(max(eMATno) + 1, 1) as newEMATno from eMAT";
		}




		public static string GenerateMktReq()
		{
			return "" +
			   "SELECT isnull(( SELECT 'PROG' + replicate('0', 5 - len( cast( (max(right(programNo,5)) + 1) as nvarchar ))) + cast( (max(right(programNo,5)) + 1) as nvarchar ) from dbo.mrktProgram) , 'PROG00001')";
		}


		public static string InsertNew_mtgMinutesAgreement(
				Int64 agreeNo,
				string ccaNum,
				string acctCode,
				string acctName,
				string mtgType,
				string mtgDate,
				string mtgTimeStart,
				string mtgTimeEnd,
				string mtgObjective,
				string preparedBy,
				string status
			) {
			return "" +
				"INSERT INTO mtgMinutesAgreement " +
				"(agreeNo " +
				",ccaNum " +
				",acctCode " +
				",acctName " +
				",mtgType " +
				",mtgDate " +
				",mtgTimeStart " +
				",mtgTimeEnd " +
				",mtgObjective " +
				",preparedBy " +
				",status) " +
				"VALUES " +
				"(" + agreeNo + " " +
				",'" + ccaNum + "' " +
				",'" + acctCode + "' " +
				",'" + acctName + "' " +
				",'" + mtgType + "' " +
				",'" + mtgDate + "' " +
				"," + mtgTimeStart + " " +
				"," + mtgTimeEnd + " " +
				",'" + mtgObjective + "' " +
				",'" + preparedBy + "' " +
				",'" + status + "') " +
				"";
		}

		public static string InsertNew_mtgAttendees(
				Int64 agreeNo,
				string attendName
			){
			return "" +
				"INSERT INTO mtgAttendees " +
				"(agreeNo " +
				",attendName) " +
				"VALUES " +
				"(" + agreeNo + " " +
				",'" + attendName + "') " +
				"";
		}

		#region BusinessReview
		public static string InsertBusReview(
				string br_no, 
				string acctCode,
				string newccaNum, 
				DateTime date,
				string status,
				string txt_encoded_by
			){
			return "" +
				"INSERT INTO busReview " +
				"(busReviewNo " +
				",ccaNum " +
				",acctCode " +
				",busReviewDate" +
				",status" +
				",encodedBy) " +
				"VALUES " +
				"('" + br_no + "' " +
				",'" + newccaNum + "' " +
				",'" + acctCode + "' " +
				",'" + date +"'"+
				",'" + status + "'" + 
				",'" + txt_encoded_by + "') " +
				""; 
	   }
		
		public static string InsertBusRevStrategicPlan(
			   string br_no,
			   string STOrigAnn,
			   string STRevAnn,
			   string STReason,
			   string plan,
			   string support,
			   string other_info
		   ){
			return "" +
				"INSERT INTO busRevStrategicPlan " +
				"(busReviewNo " +
				",STorigAnnual " +
				",STrevAnnual " +
				",STreason " +
				",IncentivePlan " +
				",Support " +
				",OtherInfo) " +
				"VALUES " +
				"('" + br_no + "' " +
				",'" + STOrigAnn + "' " +
				",'" + STRevAnn + "' " +
				",'" + STReason + "' " +
				",'" + plan + "' " +
				",'" + support + "'" +
				",'" + other_info + "') " +
				"";
		}

		public static string InsertBusRevPropCreditChange(
			string br_no,
			string ExstcrdLimit,
			string ReccrdLimit,
			string ExstcrdTerm,
			string ReccrdTerm )
		{
			return "" +
			"INSERT INTO busRevPropCreditChange " +
			"(busReviewNo " +
			",credLimitExist " +
			",credLimitReco " +
			",creditTermExist " +
			",creditTermReco) " +
			"VALUES " +
			"('" + br_no + "' " +
			",'" + ExstcrdLimit + "' " +
			",'" + ReccrdLimit + "' " +
			",'" + ExstcrdTerm + "' " +
			",'" + ReccrdTerm + "') " +
			"";
		}


		public static string UpdateEncoded_by(string encoded_by, string val_brNo) {

			return "" +
					"UPDATE busReview SET encodedBy = '"+ encoded_by +"' "+
					"WHERE busReviewNo = '"+ val_brNo +"'";
		
		}

		public static string SaveRemarkFromSSM(string remarksFromSSM, string val_brNo)
		{

			return "" +
					"UPDATE busReview SET remarksFromSSM = '" + remarksFromSSM + "' " +
					"WHERE busReviewNo = '" + val_brNo + "'";

		}

		public static string SaveRemarkFromFNM(string remarksfromFNM, string val_brNo)
		{

			return "" +
					"UPDATE busReview SET remarksFromFNM = '" + remarksfromFNM + "' " +
					"WHERE busReviewNo = '" + val_brNo + "'";

		}

		public static string UpdateRemarks(string remarks, string val_brNo)
		{

			return "" +
					"UPDATE busReview SET areaRemarks = '" + remarks + "' " +
					"WHERE busReviewNo = '" + val_brNo + "'";

		}

		public static string UpdateRemarksForCEO(string buss_no, string CEO_Remarks) {

			return "" +
					   "UPDATE busReview SET comments = '" + CEO_Remarks + "' " +
					   "WHERE busReviewNo = '" + buss_no + "'";

		}

		public static string BusRevFinanceUse(
				string busReviewNo,
				string lenPayment,
				string existCreditTerm,
				string remarksCredTerm,
				string dishonCheck,
				string remarksDisCheck)
		{
				return "" +
			   "INSERT INTO busRevFinanceUse " +
			   "(busReviewNo " +
			   ",lenPayment " +
			   ",existCreditTerm " +
			   ",remarksCredTerm " +
			   ",dishonCheck " +
			   ",remarksDisCheck) " +
			   "VALUES " +
			   "('" + busReviewNo + "' " +
			   ",'" + lenPayment + "' " +
			   ",'" + existCreditTerm + "' " +
			   ",'" + remarksCredTerm + "' " +
			   ",'" + dishonCheck + "' " +
			   ",'" + remarksDisCheck + "') " +
			   "";
		}

		public static string UpdateBusReviewArea(
				string br_no,
				string comExAgr,
				string comAcctPer,
				string areasForImprovemnts)
		{
			return "" +
			  "UPDATE busReview SET " +
						" comExistingAgree='" + comExAgr + "'," +
						" comCurrentAcctPerf='" + comAcctPer + "',"+
						" areasForImprovement='" + areasForImprovemnts + "'" +
				 " WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateStatus(string val_brNo,string status)
		{
			return "" +
				"UPDATE busReview SET " +
				" status = '" + status + "'" +
				" WHERE busReviewNo='" + val_brNo + "'";
		}

		public static string UpdateBusReviewChanges(
			string br_No,
			string areasForImprovemnts
)
		{
			return "" +
				"UPDATE busReview SET " +
				"areasForImprovement = '" + areasForImprovemnts + "'" +
				" WHERE busReviewNo='" + br_No + "'";
		}

		public static string CloseBusRevDocument(string val_brNo,string status)
		{
			return "" +
				"UPDATE busReview SET " +
				"status = '" + status + "'" +
				" WHERE busReviewNo='" + val_brNo + "'";
		}

		public static string SendBackToVPTFI(
			 string val_brNo,
			 string status)
		{
			return "" +
				"UPDATE busReview SET " +
				"status = '" + status + "'" +
				" WHERE busReviewNo='" + val_brNo + "'";
		}

		public static string UpdateBusReview(
			   string br_no,
			   string comExAgr,
			   string comAcctPer,
			   string encoded_by)
		{
			return "" +
				"UPDATE busReview SET " +
					" comExistingAgree='" + comExAgr + "'," +
					" comCurrentAcctPerf='" + comAcctPer + "'," +
					" encodedby='" + encoded_by + "'" +
				" WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateBusReviewFinal(
				string br_no,
				string status,
				string comExAgr,
				string comAcctPer)
		{
			return "" +
				"UPDATE busReview SET " +
					" status = '" + status + "'," +
					" comExistingAgree='" + comExAgr + "'," +
					" comCurrentAcctPerf='" + comAcctPer + "'"+
				" WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateBusReviewNoField(
			  string br_no,
			  string comExAgr,
			  string comAcctPer)
		{
			return "" +
				"UPDATE busReview SET " +
					" comExistingAgree='" + comExAgr + "'," +
					" comCurrentAcctPerf='" + comAcctPer + "'" +
				" WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateBusRevStrategicPlan(
				string br_no,
				string STOrigAnn,
				string STRevAnn,
				string STReason,
				string plan,
				string support)
		{
			return "" +
				"UPDATE busRevStrategicPlan SET " +
						" STorigAnnual='" + STOrigAnn + "'," +
						" STrevAnnual='" + STRevAnn + "'," +
						" STreason='" + STReason + "'," +
						" IncentivePlan='" + plan + "'," +
						" Support='" + support + "'" +
				 " WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateBusRevChangeAcctMasterFile(
				string fieldName,
				string existingVal,
				string revisedVal,
				string br_no)
		{
			return "" +
				"UPDATE busRevChangesAcctMasterFile SET " +
						" fieldName='" + fieldName + "'," +
						" existingVal='" + existingVal + "'," +
						" revisedVal='" + revisedVal + "'"+
				 " WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateBusRevPropCreditChange(
				string br_no,
				string ExstcrdLimit,
				string ReccrdLimit,
				string ExstcrdTerm,
				string ReccrdTerm)
		{
			return "" +

				"UPDATE busRevPropCreditChange SET " +
						" credLimitExist='" + ExstcrdLimit + "'," +
						" credLimitReco='" + ReccrdLimit + "'," +
						" creditTermExist='" + ExstcrdTerm + "'," +
						" creditTermReco='" + ReccrdTerm + "'"+
				" WHERE busReviewNo='" + br_no + "'";
		}

		public static string UpdateBusRevFinanceUse(
			  string br_no,
			  string lenPayment,
			  string existCreditTerm,
			  string remarksCredTerm,
			  string dishonCheck,
			  string remarksDisCheck)
		{
			return "" +
				"UPDATE busRevFinanceUse SET " +
						" lenPayment='" + lenPayment + "'," +
						" existCreditTerm='" + existCreditTerm + "'," +
						" remarksCredTerm='" + remarksCredTerm + "'," +
						" dishonCheck='" + dishonCheck + "'," +
						" remarksDisCheck='" + remarksDisCheck + "'" +
				 " WHERE busReviewNo='" + br_no + "'";
		}

#endregion BusinessReview

		public static string ListOfRoutingPerModule()
		{
			return "" +
				"SELECT c.docDescription,b.roleName,a.stateId,a.stateDesc FROM approvalState a INNER JOIN apprvrRole b ON a.roleid=b.roleId INNER JOIN documentTypes c ON a.docType=c.docType WHERE a.docType NOT IN (1,2,3) ORDER BY a.docType" +
				"";
		}
				
		public static string Profiles()
		{
			return "" +
				"SELECT empIdNo, (lastName + ', ' + firstName) AS fullname, position, status FROM userHeader order by lastname" +
				"";
		}

		public static string InsertUserHeader(
			string idNo,
			string status,
			string lname,
			string fname,
			string position,
			string emailAdd,
			string userName,
			string password,
			string area,
			string territory,
			string region
		){
			return "" +
				"INSERT INTO userHeader " +
				"(empIdNo " +
				",lastName " +
				",firstName " +
				",position " +
				",emailAdd " +
				",status " +
				",area " +
				",territory " +
				",region" +
				",userName " +
				",userPass) " +
				"VALUES " +
				"('" + idNo + "' " +
				",'" + lname + "' " +
				",'" + fname + "' " +
				",'" + position + "' " +
				",'" + emailAdd + "' " +
				",'" + status + "' " +
				",'" + area + "' " +
				",'" + territory + "' " +
				",'" + region + "' " +
				",'" + userName + "' " +
				",'" + password + "') " +
				"";
		}

		public static string InsertNew_mktgResources(string programNo, string item, string dscription, string amount)
		{
			return "" +
				"INSERT INTO mrktResources" +
				"(programNo  " +
				",item  " +
				",dscription " +
				",amount) " +
				"VALUES " +
				"('" + programNo + "' " +
				", '" + item + "'" +
				", '" + dscription + "'" +
				"," + amount + ") " +
				"";
		}

		public static string InsertNew_mktgTimeline(string programNo, DateTime startFinish, DateTime endDate, string task, string responsiblePerson, string updates)
		{
			return "" +
				"INSERT INTO mrktTimeline" +
				"(programNo  " +
				",startFinish  " +
				",task " +
				",responsiblePerson " +
				",updates " +
				",endDate) " +
				"VALUES " +
				"('" + programNo + "' " +
				", '" + startFinish + "'" +
				", '" + task + "'" +
				", '" + responsiblePerson + "'" +
				", '" + updates + "'"+
				", '" + endDate + "') " +
				"";
		}

		public static string InsertNew_Attachment(string programNo, string filePath, string fileDesc)
		{
			return "" +
				"INSERT INTO mrktAttachments" +
				"(programNo  " +
				",filePath  " +
				", fileDesc) " +
				"VALUES " +
				"('" + programNo + "' " +
				", '" + filePath + "'" +
				", '" + fileDesc + "') " +
				"";
		}


		public static string InsertNew_AttachmentMRKTR(string mrktReqId, string Attachment)
		{
			return "" +
				 "Insert into marketingAttach" +
				 "(filePath" +
				 ", reqID)" +
				 "VALUES" +
				 "('" + Attachment + "'" +
				 ",'" + mrktReqId + "') " +
				 "";
		}


		public static string InsertNew_actActivities(string programNo, DateTime date, string activityUpdate)
		{
			return "" +
				"INSERT INTO mrktActualActivities" +
				"(programNo  " +
				",date " +
				",activityUpdate) " +
				"VALUES " +
				"('" + programNo + "' " +
				", '" + date + "' " +
				", '" + activityUpdate + "') " +
				"";
		}

		public static string InsertNew_TargetAcct (string programNo, string acctCode, string acctName, string area , decimal amountAlloc )
		{
			return "" +
				"INSERT INTO mrktTargetAccounts" +
				"(programNo  " +
				",acctCode " +
				",acctName " +
				",area " +
				",amountAlloc) " +
				"VALUES " +
				"('" + programNo + "' " +
				", '" + acctCode + "' " +
				", '" + acctName + "' " +
				", '" + area + "' " +
				", " + amountAlloc + ") " +
				"";
		}     

		public static string InsertNew_mtgActionItems(
				Int64 agreeNo,
				string actionItm,
				string proposedTime,
				string status
			) 
		{
			return "" +
				"INSERT INTO mtgActionItems " +
				"(agreeNo " +
				",actionItm " +
				",proposedTime " +
				",status) " +
				"VALUES " +
				"(" + agreeNo + " " +
				",'" + actionItm + "' " +
				",'" + proposedTime + "' " +
				",'" + status + "') " +
				"";
		}

		public static string InsertNew_mtgAttachment(
				Int64 fileID,
				Int64 agreeNo,
				string filePath,
				string dscription
			){
			return "" +
				"INSERT INTO mtgAttachment " +
				"(fileID " +
				",agreeNo " +
				",filePath " +
				",dscription) " +
				"VALUES " +
				"(" + fileID + " " +
				"," + agreeNo + " " +
				",'" + filePath + "' " +
				",'" + dscription + "') " +
				"";
		}

		internal static string InsertNewMarktingReq(
				string mrktReqId,
				string encodedBy,
				string acctCode, 
				string acctName, 
				string acctAdd, 
				string acctArea, 
				string acctOfficer, 
				string requestedBy, 
				string brand, 
				string category, 
				string type,
				string setUpDate, 
				string size, 
				string value,
				string availDeploy,
				string actualDeploy,
				string stateid ="1"
			){

			return 
			"EXEC MTC_sp_insertMrktngReqHeader " +
				"'" + encodedBy + "','"
				+ stateid + "','"    
				+ acctCode + "','"
				+ acctName + "','" 
				+ acctAdd + "','"
				+ acctArea + "','" 
				+ acctOfficer + "','"
				+ requestedBy + "','"
				+ brand + "','" 
				+ category + "','"
				+ type + "','"
				+ setUpDate + "','"
				+ size + "','"
				+ value + "','"
				+ availDeploy + "','" 
				+ actualDeploy +"'" ;
		}

		public static string InsertNewMarktingReqAttach2(string attchmnts1, string mrktReqId)
		{
			return "" + 
				"Insert into marketingAttach"+ 
				"(filePath"+ 
				", reqID)"+
				"VALUES" + 
				"('" + attchmnts1 + "'" +
				",'"+ mrktReqId +"') " +
				"";
		}

		//public static string InsertNewMarktingReqAttach2(string attchmnts2, string mrktReqId)
		//{

		//    return "" +
		//        "Insert into marketingAttach" + 
		//        "(filePath" + 
		//        ", reqID)" +
		//        "VALUES" + 
		//        "('" + attchmnts2 + "'" +
		//        ",'" + mrktReqId + "') " +
		//        "";
		//}

		public static string InsertNewMarktingReqAttach3(string attchmnts3, string mrktReqId)
		{
			return "" +
				"Insert into marketingAttach" + 
				"(filePath" + 
				", reqID)" +
				"VALUES" + 
				"('" + attchmnts3 + "'" +
				",'" + mrktReqId + "') " +
				"";
		}

		public static string LifOfPendingAccounts(string cardcode = "", string cardname = "", string datecreated = "", string doccreator = "", string status = "", string area = "")
		{

			string search_option = "";
			if (cardcode != "") 
			{ 
				search_option = " and a.acctcode like'%" + cardcode + "%'";
				status = "";
			}

			if (cardname != "")
			{ 
				search_option = " and a.acctname like '%" + cardname + "%'";
				status = "";
			}

			if (area != "")
			{
				search_option = " and c.area like '%" + area + "%'";
				status = "";
			}

			if (status != "") search_option = " and a.status = '" + status + "'";
			else status = "";

			return @"
				SELECT a.ccanum, case when a.sapacctcode is null then a.acctcode else a.sapacctcode end as 'acctcode', a.acctname, a.status as 'status1', b.status as 'status2', a.region, c.channel, c.area, b.hasModified, b.is_sent_back , 
				d.creator_uname, convert(varchar(10), d.mdate, 101) as 'mdate', a.acctOffcr 
				from dbo.customerHeader a inner join proposedChangesCA b on a.ccanum=b.ccanum left join ChannelGroup c on c.area=a.area 
				left join document_history d on d.docid=a.ccanum and d.mtype = 'NEW_CUSTOMER' 
				where a.status!='1000' and b.status='1' " +
				search_option +
				" order by cast(right(a.ccanum,16) as int)" +
				"";
		}

		public static string LifOfLeadAccounts(string name = "", string datecreated = "", string encodedby = "", string status = "")
		{
			string search_option = "";
			if (name != "")
			{
				search_option = " and name like'%" + name + "%'";
				status = "";
			}

			if (datecreated != "")
			{
				search_option = " and dateencoded='" + datecreated + "'";
				status = "";
			}

			if (encodedby != "")
			{
				search_option = " and EnCodedby like '%" + encodedby + "%'";
				status = "";
			}

			if (status != "") search_option = " and a.status = '" + status + "'"; 
			
			return @"
				select requestid, name, address, convert(varchar(10), inqdate, 101) as InqDate, c.area, c.channel, c.grp_name, a.ProposedChannel,
				case when right(a.ProposedChannel,1) = 'L' then 'LUZON' else 'VISMIN' end as 'region', sapleadcode as 'acctcode', a.status, a.sapleadcode, a.proposedleadcode 
				from customerleadi a left join SAPSERVER.MATIMCO.dbo.oslp b on a.AssignTo_empId=b.slpcode 
				left join ChannelGroup c on c.area collate SQL_Latin1_General_CP850_CI_AS = (b.u_area + ' - ' + b.u_area) 
				where requestid is not null and a.status = '1000' " +
				search_option + " ORDER BY cast(requestid as bigint)";
		}

		public static string LifOfAccountsWChanges(string cardcode = "", string cardname = "", string datecreated = "", string doccreator = "", string status = "", string area = "")
		{
			string search_option = "";

			if (cardcode != "")
			{
				search_option = " and a.acctcode like'%" + cardcode + "%'";
				status = "";
			}

			if (cardname != "")
			{
				search_option = " and a.acctname like '%" + cardname + "%'";
				status = "";
			}

			if (area != "")
			{
				search_option = " and c.area like '%" + area + "%'";
				status = "";
			}

			if (status != "") search_option = " and b.status = '" + status + "'"; 

//            return @"
//                SELECT a.ccanum, case when a.sapacctcode is null then a.acctcode else a.sapacctcode end as 'acctcode', a.acctname, a.status as 'status1', b.status as 'status2', a.region, c.channel
//                , case when b.area is not null and b.area != '' then b.area else c.area end as 'area'
//                , b.hasModified, b.is_sent_back , 
//                d.creator_uname, convert(varchar(10), d.mdate, 101) as 'mdate', a.acctOffcr 
//                from dbo.customerHeader a inner join proposedChangesCA b on a.ccanum=b.ccanum left join ChannelGroup c on c.area=a.area 
//                left join document_history d on d.docid=a.ccanum and d.mtype = 'NEW_CUSTOMER' 
//                where a.ccanum!='' and (b.status!='1' or (b.status = '1' and b.is_sent_back = 1)) " +
//                search_option + " order by cast(right(a.ccanum,16) as int)";
			return @"
				select 
					a.ccanum, a.acctname, a.bussadd, a.sapacctcode as 'acctcode',
					case when b.area is null or b.area = '' then a.area else b.area end as 'area',
					case when b.area is null or b.area = '' then 
						(select channel from channelgroup where area=a.area)
					else 
						(select channel from channelgroup where area=b.area)
					end as 'channel',
					a.region,
					case when b.area is null or b.area = '' then 
						(select grp_name from channelgroup where area=a.area)
					else 
						(select grp_name from channelgroup where area=b.area)
					end as 'grp_name',
					a.status as 'status1', b.status as 'status2', b.hasModified, b.is_sent_back , a.acctOffcr,
					d.creator_uname, convert(varchar(10), d.mdate, 101) as 'mdate'
				from 
				customerheader a inner join proposedchangesca b on a.ccanum=b.ccanum 
				left join document_history d on d.docid=a.ccanum and d.mtype = 'NEW_CUSTOMER'
				where a.status = '1000' and b.status != '1' "   
				+ search_option + "  order by cast(right(a.ccanum,16) as int)"
				;

		}

		public static string InsertNew_marktingRequest(
				string reqID,
				string ccaNum,
				string status,
				string encodeBy,
				string existingCust,
				string acctCode,
				string acctName,
				string acctAdd,
				string acctArea,
				string acctOfficer,
				string requestedBy,
				string brand,
				string category,
				string setUpDate,
				string size,
				string value,
				string availDeploy,
				string actualDeploy,
				string entryDate
			) {
			return "" +
				"INSERT INTO marktingRequest " +
				"(reqID " +
				",ccaNum " +
				",status " +
				",encodeBy " +
				",existingCust " +
				",acctCode " +
				",acctName " +
				",acctAdd " +
				",acctArea " +
				",acctOfficer " +
				",requestedBy " +
				",brand " +
				",category " +
				",setUpDate " +
				",size " +
				",value " +
				",availDeploy " +
				",actualDeploy " +
				",entryDate) " +
				"VALUES " +
				"('" + reqID + "' " +
				",'" + ccaNum + "' " +
				",'" + status + "' " +
				",'" + encodeBy + "' " +
				"," + existingCust + " " +
				",'" + acctCode + "' " +
				",'" + acctName + "' " +
				",'" + acctAdd + "' " +
				",'" + acctArea + "' " +
				",'" + acctOfficer + "' " +
				",'" + requestedBy + "' " +
				",'" + brand + "' " +
				",'" + category + "' " +
				"," + setUpDate + " " +
				",'" + size + "' " +
				",'" + value + "' " +
				"," + availDeploy + " " +
				"," + actualDeploy + " " +
				"," + entryDate + ") " +
				"";
		}

		public static string BusRevListFiltered(HttpContext currPage) 
		{
			/*return @"
				SELECT a.busReviewNo, convert(varchar(10), a.busReviewDate,101)as newBusRevDate, a.acctCode, b.acctName, c.stateDesc as status 
				from busReview a INNER JOIN approvalState c on a.status = c.stateID AND c.docType=5 inner join customerheader b on a.acctCode = b.acctCode ORDER BY CAST(SUBSTRING(busReviewNo,11,5) AS INT)";
			*/
			return @"
				SELECT 
					a.busReviewNo, convert(varchar(10), a.busReviewDate,101)as newBusRevDate, a.acctCode, b.acctName, c.stateDesc as status 
					,b.area, d.channel, b.region
				from busReview a INNER JOIN approvalState c on a.status = c.stateID AND c.docType=5 
				inner join customerheader b on a.acctCode = b.acctCode left join channelgroup d 
				on b.area=d.area
				ORDER BY CAST(SUBSTRING(busReviewNo,11,5) AS INT)
			";
		}

		public static string EMATListFiltered(string cardcode = "", string buyer_name = "", string status = "")
		{
			string search_options = "";

			if (cardcode != "") search_options = " and d.cardcode like '%" + cardcode + "%'";

			if (buyer_name != "") search_options = " and a.buyer like '%" + buyer_name + "%'";

			return @"
				SELECT a.ematno, a.buyer, a.acctcode, a.status, b.statedesc, d.area, c.channel, d.region 
				FROM emat a inner join customerheader d on a.acctcode = d.acctcode 
				inner join approvalState b on a.status = b.stateID left join dbo.ChannelGroup c on c.area=d.area 
				where b.docType='9' " + search_options + " order by cast(a.ematno as int)";
		}

		public static string ListOfMrktgRqstFiltered(HttpContext currPage)
		{
			return @"
				select b.reqid, b.encodeby, b.requestedby, c.stateDesc, a.area, a.region, isnull((select c.descript 
				from SAPSERVER.MATIMCO.dbo.oter b , SAPSERVER.MATIMCO.dbo.oter c 
				where b.parent=c.territryid and b.descript=a.area collate SQL_Latin1_General_CP850_CI_AS 
				), '') as 'channel', b.brand from customerheader a inner join marktingRequest b on 
				a.ccanum=b.ccanum left join approvalstate c on b.status=c.stateID and c.doctype='7' ";

		}

		public static string Update_eMATstatus( string approved, string val_emat )
		{
			return @"
				UPDATE eMAT 
				SET status = '" + approved + @"' 
				WHERE eMATno = '" + val_emat + "' ";
		}
		
		public static string ListOfActiveLeadsFiltered(string cardcode = "", string cardname = "", string datecreated = "", string doccreator = "", string status = "") 
		{
			string search_option = "";
			if (cardcode != "")
			{
				search_option = " and a.sapleadcode like'%" + cardcode + "%'";
				status = "";
			}

			if (cardname != "")
			{
				search_option = " and a.name like '%" + cardname + "%'";
				status = "";
			}

			if (datecreated != "")
			{
				search_option = " and convert(varchar(10), z.dateEncoded, 101) = '" + datecreated + "'";
				status = "";
			}

			if (doccreator != "")
			{
				search_option = " and z.encodedby = '" + doccreator + "'";
				status = "";
			}

			if (status != "") search_option = " and (z.status = '" + status + "' or z.status = '" + status + "')";

			return @"
				select a.requestid, a.name, z.status, c.area, c.channel, c.grp_name, 
				case when right(a.ProposedChannel,1) = 'L' then 'LUZON' else 'VISMIN' end as 'region', a.sapleadcode as 'acctcode' 
				,z.is_nego_contacted 
				,z.nego_qoute_submitted 
				,z.nego_followup 
				,z.is_lost_sales 
				,z.is_closed 
				,z.is_conf_encoded 
				,z.encodedby
				,convert( varchar(10), z.dateEncoded, 101) as 'dateEncoded'
				from customerleadi a inner join customerLeadDb z on a.requestid=z.requestid left join 
				SAPSERVER.MATIMCO.dbo.oslp b on a.AssignTo_empId=b.slpcode 
				left join ChannelGroup c on c.area collate SQL_Latin1_General_CP850_CI_AS = (b.u_area + ' - ' + b.u_area) 
				where a.requestid is not null "
				+ search_option + @"
				order by cast(a.requestid as int) asc ";
		}

		public static string LifOfLeadPendingAccounts(string name = "", string datecreated = "", string encodedby = "", string status = "")
		{
			string search_option = "";
			if (name != "")
			{
				search_option = " and name like'%" + name + "%'";
				status = "";
			}

			if (datecreated != "")
			{
				search_option = " and dateencoded='" + datecreated + "'";
				status = "";
			}

			if (encodedby != "")
			{
				search_option = " and EnCodedby like '%" + encodedby + "%'";
				status = "";
			}

			if (status != "") search_option = " and a.status = '" + status + "'";
			
			return @"
				select requestid, name, address, convert(varchar(10), inqdate, 101) as InqDate, c.area, c.channel, c.grp_name,a.ProposedChannel, 
				case when right(a.ProposedChannel,1) = 'L' then 'LUZON' else 'VISMIN' end as 'region', sapleadcode as 'acctcode', a.status, a.sapleadcode, a.proposedleadcode 
				from customerleadi a left join SAPSERVER.MATIMCO.dbo.oslp b on a.AssignTo_empId=b.slpcode 
				left join ChannelGroup c on c.area collate SQL_Latin1_General_CP850_CI_AS = (b.u_area + ' - ' + b.u_area) 
				where requestid is not null and a.status != '1000' " + search_option + " ORDER BY cast(requestid as bigint)";
		}

		public static string ListOfContractsMeetings(string acctCode = "", string acctName = "", string status = "") 
		{
			string search_option = "";

			if (acctCode != "")
			{
				search_option = " and a.acctCode like '%" + acctCode + "%'";
				status = "";
			}

			if (acctName != "")
			{
				search_option = " and a.acctName like '%" + acctName + "%'";
				status = "";
			}

			if (status != "") search_option = " and a.status = '" + status + "'";

			return "" +
				"select a.agreeno, a.acctcode, b.acctname, a.status, a.mtgType, " +
				"case when charindex('LUZON',c.channel) > 0 then 'LUZON' when charindex('VISMIN',c.channel) > 0 then 'VISMIN' else '' end as 'region',  " +
				"isnull(c.channel,'') as 'channel', isnull(c.area,'') as 'area'  " +
				"from mtgMinutesAgreement a inner join customerHeader b on a.acctcode=b.acctcode  " +
				"left join ChannelGroup c on c.area=b.area where a.agreeno is not null " +
				search_option +
				" order by cast(a.agreeno as int) asc" +
				"";

		}

		public static string CustomerInfo_SAP(string ccanum) 
		{
			return @"
				select 
				'CUSTOMER' as 'CardType', 
				a.SapAcctCode as 'CardCode', 
				a.acctName as 'CardName', 
				a.telNum as 'Phone', 
				a.telNum2 as 'Phone2', 
				a.MobileNum as 'Mobile', 
				a.faxNum as 'Fax', 
				a.emailAdd as 'Email', 
				a.bussAdd as 'Address', 
				a.delAdd as 'Address2',
				(select territryID from SAPSERVER.MATIMCO.dbo.oter where descript=a.territory collate SQL_Latin1_General_CP850_CI_AS) as 'Territory', 
				a.area, 
				(select channel from channelgroup where a.area=area) as 'Channel', 
				a.TIN as 'TaxId', 
				rtrim(ltrim(a.acctType)) as 'AccountType', 
				(select slpcode from SAPSERVER.MATIMCO.dbo.oslp where slpname=a.acctOffcr collate SQL_Latin1_General_CP850_CI_AS) as 'AccountOfficer', 
				a.VATregNum as 'VatType', 
				(select GroupNum from SAPSERVER.MATIMCO.dbo.octg where pymntGroup = b.propCredTerms collate SQL_Latin1_General_CP850_CI_AS) as 'CreditTerms', 
				replace(b.probCredLimit, ',', '') as 'CreditLimit', 
				(select priceListCode from propsedPrice where ccanum=a.ccanum and brandType='MW') as 'MW_PriceCode', 
				(select priceListCode from propsedPrice where ccanum=a.ccanum and brandType='WW') as 'WW_PriceCode', 
				(select priceListCode from propsedPrice where ccanum=a.ccanum and brandType='PWF') as 'PWF_PriceCode', 
				(select priceListCode from propsedPrice where ccanum=a.ccanum and brandType='PWR') as 'PWR_PriceCode', 
				(select priceListCode from propsedPrice where ccanum=a.ccanum and brandType='GW') as 'GW_PriceCode', 
				a.acctCategoryVal, 
				a.acctBusinessClass, 
				a.acctCategoryPrem 
				from customerheader a, custBusHdr b 
				where a.ccanum=b.ccanum and a.ccanum = '" + ccanum + "'" +
				"";
		}

		public static string LeadInfo_SAP(string requestid) 
		{
			return @"
				select 
				a.sapleadcode as 'CardCode', a.name as 'CardName', a.contactno as 'Phone', a.e_mail as 'Email', a.address as 'Address', 
				(select slpcode from SAPSERVER.MATIMCO.dbo.oslp where slpname=case when a.salesofficer is null or a.salesofficer = '' then a.assignto_asmso else a.salesofficer end 
				collate SQL_Latin1_General_CP850_CI_AS) as 'AccountOfficer' 
				from customerleadi a 
				where a.requestid = '" + requestid + @"' 
				";
		}

		public static string check_customer_code(string final_acct_code, string ccanum) 
		{
			return @"
			CREATE TABLE #cardcode_to_check (
				cardcode VARCHAR(15)
			)
			declare @customer_code varchar(15);
			set @customer_code = '" + final_acct_code + @"';

			/* check if exist and not yet approved, means customer creation */
			if exists( select * from dbo.customerheader where acctcode = @customer_code and status != '1000' )
			begin 
	
				/* check if it is lead convertion */
				/* check if in customerLeadI */
				if exists( select * from customerLeadI where sapLeadCode = @customer_code and ccanum = '" + ccanum + @"' )
				begin
					
					/* a lead conversion */
					/* check if the sapleadcode in OCRD.cardcode is CardType = 'L' */
					if exists(select * from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code and cardtype != 'L')
					begin
			
						/* already a OCRD.CardType = 'C' in sap, not safe to update */
						 insert into #cardcode_to_check select @customer_code
					end
				end
				else
				begin
					/* not a lead conversion, but customer creation */
					/* check if code in sap */
					if exists(
						select cardcode from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code union 
						select acctcode collate SQL_Latin1_General_CP850_CI_AS from customerheader where (acctcode = @customer_code or sapacctcode = @customer_code)
						and ccanum != '" + ccanum + @"'
					)
					begin
						/* already in sap, not safe to update */
						insert into #cardcode_to_check select @customer_code
			
					end
				end
	
			end
			/* if alrady approved or does not exist */
			else
			begin
				/* check if exist in customerheader, customerleadi, ocrd */
				if exists(select * from customerheader where acctcode = @customer_code or sapacctcode = @customer_code)
				or exists(select * from customerleadi where ProposedLeadCode = @customer_code or sapleadcode = @customer_code)
				or exists(select * from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code )
				begin
					/* not safe to update */
					insert into #cardcode_to_check select @customer_code
				end
	
			end
			select * from #cardcode_to_check
			drop table #cardcode_to_check
			";
		}

		public static string check_customer_code_on_create(string sapacctcode, string requestid) 
		{
			return @"
				CREATE TABLE #cardcode_to_check (
					cardcode VARCHAR(15)
				)
				declare @customer_code varchar(15);
				set @customer_code = '" + sapacctcode + @"';

				/* check if lead convertion */
				if exists(select * from customerleadi where sapleadcode = @customer_code and requestid = '" + requestid + @"' and ccanum is null)
				begin
	
					/* means lead conversion */
					/* check if acount code already used in customerheader.acctcode and ocrd.cardcode (cardtype != 'L') */
					if exists(select * from customerheader where acctcode=@customer_code or sapacctcode = @customer_code)
					or exists(select * from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code and cardtype != 'L')
					begin
		
						/* if exist, means not safe to update */
						insert into #cardcode_to_check select @customer_code
					end
	
				end
				else
				begin
	
					/* not lead conversion */
					/* check if already used in customerheader and OCRD */
					if exists(
						select cardcode from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code union 
						select acctcode collate SQL_Latin1_General_CP850_CI_AS from customerheader where acctcode = @customer_code or sapacctcode = @customer_code
					) or exists(
						select * from customerleadi where proposedleadcode = @customer_code or sapleadcode = @customer_code
					)
					begin
						/* already in sap, not safe to update */
						insert into #cardcode_to_check select @customer_code
		
					end
	
				end

				select * from #cardcode_to_check
				drop table #cardcode_to_check
			";
		}

		public static string check_leadcust_code(string proposedleadcode) 
		{
			return @"
				CREATE TABLE #cardcode_to_check (
					cardcode VARCHAR(15)
				)
				declare @customer_code varchar(15);
				set @customer_code = '" + proposedleadcode + @"';

				/* check if exist in sap */
				if exists(select * from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code)
				begin
					/* not safe to used */
					insert into #cardcode_to_check select @customer_code
	
				end
				else
				begin
	
					/* does not exist in sap */
					/* check if already used in customerleadi */
	
					if exists(select * from customerleadi where proposedleadcode = @customer_code or sapleadcode = @customer_code)
					begin
						/* not safe to use */
						insert into #cardcode_to_check select @customer_code
		
					end
	
					/* check if already used in customerheader */
					if exists(select * from customerheader where acctcode = @customer_code or sapacctcode = @customer_code)
					begin
		
						/* not safe to use */
						insert into #cardcode_to_check select @customer_code
		
					end
				end

				select * from #cardcode_to_check
				drop table #cardcode_to_check
			";
		}

		public static string check_leadcust_code_on_create(string sapleadcode, string requestid) 
		{
			return @"
				/*  */
				CREATE TABLE #cardcode_to_check (
					cardcode VARCHAR(15)
				)
				declare @customer_code varchar(15);
				set @customer_code = '" + sapleadcode + @"';

				/* check if exist in sap */
				if exists(select * from SAPSERVER.MATIMCO.dbo.ocrd where cardcode = @customer_code)
				begin
					/* not safe to used */
					insert into #cardcode_to_check select @customer_code
				end
				else
				begin
	
					/* does not exist in sap */
					/* check if already used in customerleadi */
	
					if exists(select * from customerleadi where (proposedleadcode = @customer_code or sapleadcode = @customer_code) and requestid != '" + requestid + @"')
					begin
						/* not safe to use */
						insert into #cardcode_to_check select @customer_code
		
					end
	
					/* check if already used in customerheader */
					if exists(select * from customerheader where acctcode = @customer_code or sapacctcode = @customer_code)
					begin
		
						/* not safe to use */
						insert into #cardcode_to_check select @customer_code
		
					end
				end

				select * from #cardcode_to_check
				drop table #cardcode_to_check
			";
		}

	}
}

