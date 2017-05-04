<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        
        DataTable mtable;
        const int IS_NOT_FOUND = -1;
        // _User CurrentUser = new _User(Session["username"].ToString());
        _User CurrentUser = (_User)Session["Ousr"];

        string name = "", datecreated = "", encodedby = "", pageno = "1", status = "";

        List<string> ActMsg = new List<string>();
        
        if (Request.QueryString["name"] != "" && Request.QueryString["name"] != null)
        {
            name = Request.QueryString["name"].ToString();
            ActMsg.Add("Searched " + name + " keyword on Name on the List of Pending Lead Customers");
        }

        if (Request.QueryString["datecreated"] != "" && Request.QueryString["datecreated"] != null)
        {
            datecreated = Request.QueryString["datecreated"].ToString();
        }

        if (Request.QueryString["encodedby"] != "" && Request.QueryString["encodedby"] != null)
        {
            encodedby = Request.QueryString["encodedby"].ToString();
            ActMsg.Add("Searched " + encodedby + " keyword on Encoded by on the List of Pending Lead Customers");
        }

        if (Request.QueryString["status"] != null)
        {
            status = Request.QueryString["status"].ToString();
            ActMsg.Add("Filter the '" + AppHelper.GetLdbIDocStatMsg(status) + "' status on the List of Lead Customers");
            
            // SAVE TO DATABASE
            SqlDbHelper.ExecNQuery("update userheader set LastFilter1='" + status + "' where username='" + CurrentUser.UserName + "'");

        }
        else
        {
            // RETRIEVE LASTFILTER1 in the userheader
            DataTable LastFilter1_tbl = SqlDbHelper.getDataDT("select LastFilter1 from userheader where username='" + CurrentUser.UserName + "' and LastFilter1 is not null");
            foreach (DataRow lf_row in LastFilter1_tbl.Rows)
            {
                status = lf_row["LastFilter1"].ToString();
            }
        }

        if (Request.QueryString["pageno"] != "" && Request.QueryString["pageno"] != null)
        {
            pageno = Request.QueryString["pageno"].ToString();
            ActMsg.Add("Moved to the page no." + pageno + " on the List of Lead Customers");
        }

        if (Session["isFSP"] == "TRUE")
        foreach (string itm in ActMsg)
        {
            AppHelper.InsertActivityLog(Session["username"].ToString(), itm);
        }
        
    %>
    <link href="<%=ResolveUrl("~/") %>Content/LeadAccounts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>

    <script src="<%=ResolveUrl("~/") %>Scripts/LeadAccountsDetailsPending.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                        <b>Pending Accounts</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>

        <div class="page_header_y">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td colspan="2" align="right">
                        <a id="sub_menu_link_4" href="javascript:ShowFilterBy('sub_menu_link_4');" ><img src="<%=ResolveUrl("~/") %>Images/filter.png" style="border:0;" /> Filter by Status</a>
                        &nbsp;&nbsp;&nbsp;
                        <a id="sub_menu_link_3" href="javascript:ShowSearchDlg('sub_menu_link_3');" ><img src="<%=ResolveUrl("~/") %>Images/magnifier.png" style="border:0;" /> Search</a>
                        &nbsp;&nbsp;&nbsp;
                        <% if(CurrentUser.HasPositionOf("csr") != IS_NOT_FOUND){ %>
                            <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>Customer/AcctCreateLeadForm">>> Create New Lead</a>
                        <% } %>
                    </td>
                </tr>
            </table>
        </div>

        <div class="simple_box" style="padding:0;">
		    <table id="tbl_lst_accounts" width="100%" cellpadding="2" cellspacing="0" border="0" >
                <tr>
				    <th align="left">Doc. Number</th>
				    <th align="left">Name</th>
				    <th align="left">Address</th>
                    <th align="left">Inq. Date</th>
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
                
                    bool CanViewCurrentDoc = false;
                    mtable = SqlDbHelper.getDataDT(SqlQueryHelper.LifOfLeadPendingAccounts(name, datecreated, encodedby, status));

                    string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
                    string[] AreaUsers = { "asm" };
                    string[] ChannelUsers = { "chm", "cmg", "ca", "cmm" };
                    string[] OtherUsers = { "vpbsm", "vptfi", "ceo", "ssm", "ssgm", "brd", "admin" };
                    
                    foreach (DataRow itm in mtable.Rows) 
                    {
                        // _Document LeadIDoc = new _Document("LDI", itm["requestid"].ToString());
                        CanViewCurrentDoc = false;

                        if (CurrentUser.HasPosAndRegionsOf(RegionUsers, itm["region"].ToString()))
                        {
                            // REGION
                            CanViewCurrentDoc = true;
                        }
                        else if (CurrentUser.HasPosAndAreasOf(AreaUsers, itm["area"].ToString()))
                        {
                            // AREA
                            CanViewCurrentDoc = true;
                        }
                        else if (
                            CurrentUser.HasPosAndChannelsOf(ChannelUsers, itm["channel"].ToString()) ||
                            CurrentUser.HasPosAndChannelGroupOf(new string[] { "chm" }, itm["ProposedChannel"].ToString())
                            )
                        {
                            // CHANNEL
                            CanViewCurrentDoc = true;
                        }
                        else if (CurrentUser.HasPositionsOf(OtherUsers))
                        {
                            // VPs
                            CanViewCurrentDoc = true;
                        }
                        else if (CurrentUser.HasAccountsOf("SO", itm["acctcode"].ToString()))
                        {
                            // SO
                            CanViewCurrentDoc = true;
                        }
                        
                        if (CanViewCurrentDoc == true) 
                        {
                            
                            LDocs.DocList.Add(
                                new string[] { 
                                    itm["requestid"].ToString().Trim(), 
                                    itm["name"].ToString(), 
                                    itm["address"].ToString(), 
                                    itm["inqdate"].ToString().Trim(),
                                    itm["status"].ToString().Trim()
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
                        Response.Write("<tr>");
                        Response.Write("<td align=\"left\"><a href=\"" + ResolveUrl("~/") + "Document/LeadAccountsDetails?RequestId=" + LDocs.DocList[i][0].ToString() + "\">" + LDocs.DocList[i][0].ToString() + "</a></td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][1].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][2].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][3].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + AppHelper.GetLdbIDocStatMsg(LDocs.DocList[i][4].ToString()) + "</td>");
                        Response.Write("</tr>");
                    }

                    string str_first = "";
                    string str_prev = "";
                    string str_next = "";
                    string str_last = "";
                    string other_options = "";

                    // cardcode = "", cardname = "", datecreated = "", doccreator = "", status = "", pageno = "1"
                    other_options = ResolveUrl("~/") + "Document/LeadAccountsDetailsPending";
                    other_options = other_options + "?did=" + RandGen.NextDouble().ToString();
                    /*
                        if (cardcode != "") other_options = other_options + "&cardcode=" + cardcode;
                    */
                    if (name != "") other_options = other_options + "&name=" + name;
                    /*
                        if (datecreated != "") other_options = other_options + "&datecreated=" + datecreated;
                        if (doccreator != "") other_options = other_options + "&doccreator=" + doccreator;
                    */
                    if (status != "") other_options = other_options + "&status=" + status;


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

	    </div>
    </div>
	
    <script type="text/javascript" language="javascript">
        $(function () {

        });
    </script>
</asp:Content>
