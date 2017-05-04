<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        System.Data.DataTable mtable;

        // _User CurrentUser = new _User(Session["username"].ToString());
        _User CurrentUser = (_User)Session["Ousr"];
        const int IS_NOT_FOUND = -1;

        string cardname = "", cardcode = "", pageno = "1", status = "", datecreated = "", doccreator = "";

        if (Request.QueryString["cardname"] != "" && Request.QueryString["cardname"] != null)
        {
            cardname = Request.QueryString["cardname"].ToString();
        }

        if (Request.QueryString["cardcode"] != "" && Request.QueryString["cardcode"] != null)
        {
            cardcode = Request.QueryString["cardcode"].ToString();
        }

        if (Request.QueryString["datecreated"] != "" && Request.QueryString["datecreated"] != null)
        {
            datecreated = Request.QueryString["datecreated"].ToString();
        }

        if (Request.QueryString["doccreator"] != "" && Request.QueryString["doccreator"] != null)
        {
            doccreator = Request.QueryString["doccreator"].ToString();
        }

        if (Request.QueryString["status"] != null)
        {
            status = Request.QueryString["status"].ToString();

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
        }
        
    %>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/LeadDbList.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/LeadDbList.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>
    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left" valign="middle">
                        <b>Active Leads</b>
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
                        <a id="sub_menu_link_3" href="javascript:ShowSearchDlg('sub_menu_link_3');" ><img src="<%=ResolveUrl("~/") %>Images/magnifier.png" style="border:0;" /> Search</a>
                        &nbsp;&nbsp;&nbsp;
                        <% if (CurrentUser.HasPositionOf("asm") != -1 || CurrentUser.HasPositionOf("so") != -1 || CurrentUser.HasPositionOf("csr") != -1 || CurrentUser.HasPositionOf("csm") != -1)
                           { %>
                        <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>LeadDb/NewLeadDb">>> New Active Lead</a>
                        <% } %>
                    </td>
                </tr>
            </table>
        </div>

	    <div class="simple_box" style="padding:0;">
		    <table id="tbl_lst_lead" width="100%" cellpadding="2" cellspacing="0" border="0" >
			    <tr>
				    <th align="left">Request Id</th>
				    <th align="left">Account Code</th>
				    <th align="left">Name</th>
                    <th align="left">Status</th>
                    <th align="left">Encoder</th>
                    <th align="left">Date Encoded</th>
                    <th align="left"></th>
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
                    
                    // THIS IS TEMPORARY
                    status = "";

                    mtable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfActiveLeadsFiltered(cardcode, cardname, datecreated, doccreator, status));

                    string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
                    string[] AreaUsers = { "asm" };
                    string[] ChannelUsers = { "chm", "cmg", "ca", "cmm" };
                    string[] OtherUsers = { "vpbsm", "vptfi", "ceo", "ssm", "ssgm", "brd", "admin" };

                    bool CanViewCurrentDoc = false;
                    foreach (DataRow itm in mtable.Rows) {
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
                        else if (CurrentUser.HasPosAndChannelsOf(ChannelUsers, itm["channel"].ToString()))
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

                        if (CanViewCurrentDoc) 
                        {
                            LDocs.DocList.Add(
                            new string[] { 
                                itm["requestid"].ToString().Trim(), 
                                itm["acctcode"].ToString(), 
                                itm["name"].ToString(), 
                                itm["status"].ToString().Trim(),
                                itm["is_nego_contacted"].ToString(),
                                itm["nego_qoute_submitted"].ToString(),
                                itm["nego_followup"].ToString(),
                                itm["is_lost_sales"].ToString(),
                                itm["is_closed"].ToString(),
                                itm["is_conf_encoded"].ToString(),
                                itm["encodedby"].ToString(),
                                itm["dateEncoded"].ToString()
                            });
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
                        Response.Write("<td align=\"left\"><a href=\"" + ResolveUrl("~/") + "LeadDb/LeadDbDetails?requestid=" + LDocs.DocList[i][0].ToString() + "\">" + LDocs.DocList[i][0].ToString() + "</a></td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][1].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][2].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + AppHelper.GetLdbDocStatMsg(LDocs.DocList[i][4].ToString(), LDocs.DocList[i][5].ToString(), LDocs.DocList[i][6].ToString(), LDocs.DocList[i][7].ToString(), LDocs.DocList[i][8].ToString(), LDocs.DocList[i][9].ToString()) + "</td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][10].ToString().ToUpper() + "</td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][11].ToString() + "</td>");
                        Response.Write("<td align=\"left\">&nbsp;</td>");
                        Response.Write("</tr>");
                    }


                    string str_first = "";
                    string str_prev = "";
                    string str_next = "";
                    string str_last = "";
                    string other_options = "";

                    // cardcode = "", cardname = "", datecreated = "", doccreator = "", status = "", pageno = "1"
                    other_options = ResolveUrl("~/") + "LeadDb/LeadDbList";
                    other_options = other_options + "?did=" + RandGen.NextDouble().ToString();

                    if (cardcode != "") other_options = other_options + "&cardcode=" + cardcode;
                    if (cardname != "") other_options = other_options + "&cardname=" + cardname;
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
</asp:Content>
