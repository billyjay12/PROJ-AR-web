<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<% 
		// _User Ousr = new _User(Session["username"].ToString());
		_User Ousr = (_User)Session["Ousr"];
		const int IS_NOT_FOUND = -1;
		
		DataTable mtable;

		string cardcode = "", cardname = "", datecreated = "", doccreator = "", status = "", pageno = "1", area = "";

		List<string> ActMsg = new List<string>();
		
		if (Request.QueryString["cardcode"] != "" && Request.QueryString["cardcode"] != null)
		{
			cardcode = Request.QueryString["cardcode"].ToString();
			ActMsg.Add("Searched " + cardcode + " keyword on Customer Code on the List of Customer Creation Approval");
		}

		if (Request.QueryString["cardname"] != "" && Request.QueryString["cardname"] != null)
		{
			cardname = Request.QueryString["cardname"].ToString();
			ActMsg.Add("Searched " + cardname + " keyword on Customer Name on the List of Customer Creation Approval");
		}

		if (Request.QueryString["datecreated"] != "" && Request.QueryString["datecreated"] != null)
		{
			datecreated = Request.QueryString["datecreated"].ToString();
			ActMsg.Add("Searched " + datecreated + " keyword on Date Created on the List of Customer Creation Approval");
		}

		if (Request.QueryString["doccreator"] != "" && Request.QueryString["doccreator"] != null)
		{
			doccreator = Request.QueryString["doccreator"].ToString();
			ActMsg.Add("Searched " + doccreator + " keyword on Encoder on the List of Customer Creation Approval");
		}

		if (Request.QueryString["area"] != "" && Request.QueryString["area"] != null)
		{
			area = Request.QueryString["area"].ToString();
			ActMsg.Add("Searched " + area + " keyword on Area on the List of Customer Creation Approval");
		}

		if (Request.QueryString["status"] != null)
		{
			status = Request.QueryString["status"].ToString();
			ActMsg.Add("Filtered the '" + AppHelper.GetDocStateMsg(status) + "' status on the List of Customer Creation Approval");

			// SAVE TO DATABASE
			SqlDbHelper.ExecNQuery("update userheader set LastFilter1='" + status + "' where username='" + Ousr.UserName + "'");
		}
		else 
		{
			// RETRIEVE LASTFILTER1 in the userheader
			DataTable LastFilter1_tbl = SqlDbHelper.getDataDT("select LastFilter1 from userheader where username='" + Ousr.UserName + "' and LastFilter1 is not null");
			foreach (DataRow lf_row in LastFilter1_tbl.Rows)
			{
				status = lf_row["LastFilter1"].ToString();
			}
		}

		if (Request.QueryString["pageno"] != "" && Request.QueryString["pageno"] != null)
		{
			pageno = Request.QueryString["pageno"].ToString();
			ActMsg.Add("Moved to the page no." + pageno + " on the List of Customer Creation Approval");
		}
		
		// start logging
		if(Session["isFSP"] == "TRUE")
		foreach (string itm in ActMsg) 
		{
			AppHelper.InsertActivityLog(Session["username"].ToString(),itm);
		}
		
	%>

	<link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/AccountsDetailsPending.js" type="text/javascript"></script>
	<script type="text/javascript" language="javascript">    
		var baseUrl = "<%= ResolveUrl("~/") %>";
	</script>

	<div class="bl_box">
	<div class="page_header">
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="left" valign="middle">
					<b>Pending Accounts</b>
				</td>
				<td align="right" valign="middle" >
					<%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
				</td>
			</tr>
		</table>
	</div>

	<div class="page_header_y">
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="right">
					<a id="sub_menu_link_4" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/filter.png" style="border:0;" /> Filter by Status</a>
					&nbsp;&nbsp;&nbsp;
					<a id="sub_menu_link_3" href="javascript:;" ><img src="<%=ResolveUrl("~/") %>Images/magnifier.png" style="border:0;" /> Search</a>
					&nbsp;&nbsp;&nbsp;
					<% 
						if (
							Ousr.HasPositionOf("csr") != IS_NOT_FOUND ||
							Ousr.HasPositionOf("csm") != IS_NOT_FOUND
							)
						{ 
					%>
						<a id="sub_menu_link_1" href="javascript:;">>> Create New Account</a>
					<% 
						} 
					%>
				</td>
			</tr>
		</table>
	</div>

	<div class="simple_box" style="padding:0;" >
		<table id="tbl_lst_accounts" width="100%" cellpadding="3" cellspacing="0" border="0" >
			<tr>
				<th align="left">CCA Number</th>
				<th align="left">Account Code</th>
				<th align="left">Account Name</th>
				<th align="left">Area</th>
				<th align="left">Encoder</th>
				<th align="left">Date Encoded</th>
				<th align="left">Sales Officer</th>
				<th align="left">Status</th>
			</tr>
			<% 
				ListDocuments LDocs = new ListDocuments();
				Int32 TotalDocs = 0;
				Int32 TotalPages = 0;
				Int32 DocsPerPage = 30;
				Int32 StartingRecordNumber = 0;
				Int32 EndingRecordNumber = 0;
				Int32 PageNo = Convert.ToInt32(pageno);
				Random RandGen = new Random();
				
				mtable = SqlDbHelper.getDataDT(SqlQueryHelper.LifOfPendingAccounts(cardcode, cardname, datecreated, doccreator, status, area));

				string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
				string[] AreaUsers = { "asm" };
				string[] ChannelUsers = { "chm", "cmg", "ca", "cmm", "vpbsm" };
				string[] OtherUsers = {  "vptfi", "ceo", "ssm", "ssgm", "brd", "admin", "sim" };
				
				foreach (DataRow item in mtable.Rows)
				{
					bool viewable = false;

					if (Ousr.HasPosAndRegionsOf(RegionUsers, item["region"].ToString()))
					{
						// REGION
						viewable = true;
					}
					else if (Ousr.HasPosAndAreasOf(AreaUsers, item["area"].ToString()))
					{
						// AREA
						viewable = true;
					}
					else if (Ousr.HasPosAndChannelsOf(ChannelUsers, item["channel"].ToString()))
					{
						// CHANNEL
						viewable = true;
					}
					else if (Ousr.HasPositionsOf(OtherUsers))
					{
						// VPs
						viewable = true;
					}
					else if (Ousr.HasAccountsOf("SO", item["acctcode"].ToString()))
					{
						// SO
						viewable = true;
					}

					if (viewable == true)
					{
						
						LDocs.DocList.Add(
								new string[] { 
									item["ccanum"].ToString().Trim(), 
									item["acctcode"].ToString(), 
									item["acctname"].ToString(), 
									item["status1"].ToString().Trim(),
									item["status2"].ToString().Trim(),
									item["ccanum"].ToString(),
									item["hasModified"].ToString(),
									item["area"].ToString().Length > 0 ? item["area"].ToString().Substring(0,5).Trim() : "" ,
									item["creator_uname"].ToString().ToUpper(),
									item["mdate"].ToString(),
									item["acctOffcr"].ToString(),
									item["channel"].ToString()
								}
							);
					}
				}

				// GET TOTAL RECORDS
				TotalDocs = LDocs.DocList.Count;

				// GET TOTAL PAGES
				if (Convert.ToDouble(TotalDocs) % Convert.ToDouble(DocsPerPage) > 0)
				{
					// has remainder
					TotalPages = (TotalDocs / DocsPerPage) + 1;
				}
				else
				{
					// no remainder
					TotalPages = (TotalDocs / DocsPerPage);
				}

				// GET STARTING RECORD NUMBER
				StartingRecordNumber = (DocsPerPage * (PageNo - 1));

				// GET ENDING RECORD NUMBER
				EndingRecordNumber = StartingRecordNumber + DocsPerPage;

				for (Int32 i = StartingRecordNumber; i < EndingRecordNumber && i < TotalDocs; i++)
				{
					String Status = AppHelper.GetAccDocStatusMessage(LDocs.DocList[i][3].ToString(), LDocs.DocList[i][4].ToString(), LDocs.DocList[i][0].ToString(), LDocs.DocList[i][6].ToString());
					Response.Write("<tr>");
					Response.Write("<td align=\"left\"><a href=\"" + ResolveUrl("~/") + "Document/AccountsDetails?ccanum=" + LDocs.DocList[i][0].ToString() + "\">" + LDocs.DocList[i][0].ToString() + "</a></td>");
					Response.Write("<td align=\"left\">" + LDocs.DocList[i][1].ToString() + "</td>");
					Response.Write("<td align=\"left\">" + LDocs.DocList[i][2].ToString() + "</td>");
					Response.Write("<td align=\"left\">" + LDocs.DocList[i][7].ToString() + "</td>");
					Response.Write("<td align=\"left\">" + LDocs.DocList[i][8].ToString() + "</td>");
					Response.Write("<td align=\"left\">" + LDocs.DocList[i][9].ToString() + "</td>");
					Response.Write("<td align=\"left\">" + LDocs.DocList[i][10].ToString() + "</td>");
					Response.Write("<td align=\"left\">" + (Status == "For Channel Manager Approval" && (LDocs.DocList[i][11].ToString()).Contains("TRADE") ? "For RSM Approval" : Status)+ "</td>");
					Response.Write("</tr>");
				}


				string str_first = "";
				string str_prev = "";
				string str_next = "";
				string str_last = "";
				string other_options = "";

				// cardcode = "", cardname = "", datecreated = "", doccreator = "", status = "", pageno = "1"
				other_options = ResolveUrl("~/") + "Document/AccountsDetailsPending";
				other_options = other_options + "?did=" + RandGen.NextDouble().ToString();

				if (cardcode != "") other_options = other_options + "&cardcode=" + cardcode;
				if (cardname != "") other_options = other_options + "&cardname=" + cardname;
				if (datecreated != "") other_options = other_options + "&datecreated=" + datecreated;
				if (doccreator != "") other_options = other_options + "&doccreator=" + doccreator;
				if (status != "") other_options = other_options + "&status=" + status;
				if (area != "") other_options = other_options + "&area=" + area;

				Int32 prev_page = 0;
				Int32 next_page = 0;

				if (PageNo - 1 < 1) prev_page = 1;
				else prev_page = PageNo - 1;

				if (PageNo + 1 > TotalPages) next_page = TotalPages;
				else next_page = PageNo + 1;

				str_first = other_options + "&pageno=1";
				str_prev = other_options + "&pageno=" + Convert.ToString(prev_page);
				str_next = other_options + "&pageno=" + Convert.ToString(next_page);
				str_last = other_options + "&pageno=" + Convert.ToString(TotalPages);
				
			%>
		</table>

		<div class="div_page_navigator">
			<a href="<%: str_first %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_first.png" alt="First Page" /></a> &nbsp;
			<a href="<%: str_prev %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_previous.png" alt="Previous Page" /></a> &nbsp;
			<a href="<%: str_next %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_next.png" alt="Next Page" /></a> &nbsp;
			<a href="<%: str_last %>" ><img src="<%:ResolveUrl("~/") %>Images/resultset_last.png" alt="Last Page" /></a> &nbsp;

			/ &nbsp; Record/s Found: <%:TotalDocs %>
		</div>

		<script language="javascript" type="text/javascript" >
			/*
			$(function () {
				$("#tbl_lst_accounts tr:nth-child(1)").find("th:nth-child(1)").width(
					$("#tbl_lst_accounts1 tr:nth-child(1)").find("td:nth-child(1)").width()
				);
				$("#tbl_lst_accounts tr:nth-child(1)").find("th:nth-child(2)").width(
					$("#tbl_lst_accounts1 tr:nth-child(1)").find("td:nth-child(2)").width()
				);
				$("#tbl_lst_accounts tr:nth-child(1)").find("th:nth-child(3)").width(
					$("#tbl_lst_accounts1 tr:nth-child(1)").find("td:nth-child(3)").width()
				);
				$("#tbl_lst_accounts tr:nth-child(1)").find("th:nth-child(4)").width(
					$("#tbl_lst_accounts1 tr:nth-child(1)").find("td:nth-child(4)").width()
				);
			});
			*/
		</script>

	</div>
	</div>
	
</asp:Content>
