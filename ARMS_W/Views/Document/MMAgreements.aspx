<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="ARMS_W.Class" %><%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <% 
        DataTable mtable;
        // _User oUsr = new _User(Session["username"].ToString());
        _User oUsr = (_User)Session["Ousr"];

        const int IS_NOT_FOUND = -1;

        string cardcode = "", cardname = "", status = "", pageno = "1";

        if (Request.QueryString["cardcode"] != "" && Request.QueryString["cardcode"] != null)
        {
            cardcode = Request.QueryString["cardcode"].ToString();
        }

        if (Request.QueryString["cardname"] != "" && Request.QueryString["cardname"] != null)
        {
            cardname = Request.QueryString["cardname"].ToString();
        }

        if (Request.QueryString["status"] != null)
        {
            status = Request.QueryString["status"].ToString();

            // SAVE TO DATABASE
            SqlDbHelper.ExecNQuery("update userheader set LastFilter1='" + status + "' where username='" + oUsr.UserName + "'");

        }
        else
        {
            // RETRIEVE LASTFILTER1 in the userheader
            DataTable LastFilter1_tbl = SqlDbHelper.getDataDT("select LastFilter1 from userheader where username='" + oUsr.UserName + "' and LastFilter1 is not null");
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

    <link href="<%=ResolveUrl("~/") %>Content/MMAgreements.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/MMAgreements.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";
    </script>

    <div class="bl_box">
        <div class="page_header">
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="left">
                        <b>Contracts and Agreements</b>
                    </td>
                    <td align="right">
                        <%: Session["InputedUname"] %>- <a href="javascript:window.parent.GoHome();">Logout</a>
                    </td>
                </tr>
            </table>
        </div>

        <div class="page_header_y">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right">
                        <a id="sub_menu_link_4" href="javascript:ShowFilterBy('sub_menu_link_4');" ><img src="<%=ResolveUrl("~/") %>Images/filter.png" style="border:0;" /> Filter by Status</a>
                        &nbsp;&nbsp;&nbsp;
                        <a id="sub_menu_link_3" href="javascript:ShowSearchDlg('sub_menu_link_3');" ><img src="<%=ResolveUrl("~/") %>Images/magnifier.png" style="border:0;" /> Search</a>
                        &nbsp;&nbsp;&nbsp;
                        <% 
                            if (
                                oUsr.HasPositionOf("so") != IS_NOT_FOUND ||
                                oUsr.HasPositionOf("csr") != IS_NOT_FOUND ||
                                oUsr.HasPositionOf("csm") != IS_NOT_FOUND ||
                                oUsr.HasPositionOf("ca") != IS_NOT_FOUND ||
                                oUsr.HasPositionOf("ssm") != IS_NOT_FOUND ||
                                oUsr.HasPositionOf("cmg") != IS_NOT_FOUND ||
                                oUsr.HasPositionOf("MMAE") != IS_NOT_FOUND
                            )
                            {
                        %>
                            <a href="<%=ResolveUrl("~/") %>MMAgreement/CreateMMAgreement">>> New Contracts and Agreements</a>
                        <% 
                            }
                        %>
                    </td>
                </tr>
            </table>
        </div>

        <div class="simple_box" style="padding:0;" >
            <table id="tbl_lst_mma" width="100%" cellpadding="2" cellspacing="0" border="0" >
			    <tr>
				    <th align="center">Document No.</th>
				    <th align="center">Account Code</th>
				    <th align="center">Account Name</th>
                    <th align="center">Document Type</th>
                    <th align="center">Status</th>
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
                
                    mtable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfContractsMeetings(cardcode, cardname, status));
                    bool CanViewDoc = false;

                    string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
                    string[] AreaUsers = { "asm" };
                    string[] ChannelUsers = { "chm", "cmg", "ca", "cmm" };
                    string[] OtherUsers = { "vpbsm", "vptfi", "ceo", "ssm", "ssgm", "brd", "admin" };
                    
                    foreach (DataRow item in mtable.Rows) 
                    {
                    
                        CanViewDoc = false;

                        // REGION
                        if (oUsr.HasPosAndRegionsOf(RegionUsers, item["region"].ToString())) CanViewDoc = true;

                        // AREA
                        if (oUsr.HasPosAndAreasOf(AreaUsers, item["area"].ToString())) CanViewDoc = true;

                        // CHANNEL
                        if (oUsr.HasPosAndChannelsOf(ChannelUsers, item["channel"].ToString())) CanViewDoc = true;

                        // VPS
                        if (oUsr.HasPositionsOf(OtherUsers)) CanViewDoc = true;

                        if (CanViewDoc) 
                        {
                            LDocs.DocList.Add(
                                    new string[] { 
                                        item["agreeno"].ToString().Trim(), 
                                        item["acctCode"].ToString(), 
                                        item["acctName"].ToString(), 
                                        item["status"].ToString().Trim(),
                                        item["mtgType"].ToString()
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
                        Response.Write("<td align=\"left\"><a href=\"" + ResolveUrl("~/") + "MMAgreement/MMAgreementDetails?agreeno=" + LDocs.DocList[i][0].ToString() + "\">" + LDocs.DocList[i][0].ToString() + "</a></td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][1].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + LDocs.DocList[i][2].ToString() + "</td>");
                        Response.Write("<td align=\"center\">" + LDocs.DocList[i][4].ToString() + "</td>");
                        Response.Write("<td align=\"left\">" + AppHelper.GetMMaDocStatusMessage(LDocs.DocList[i][3].ToString()) + "</td>");
                        Response.Write("</tr>");
                    }

                    string str_first = "";
                    string str_prev = "";
                    string str_next = "";
                    string str_last = "";
                    string other_options = "";

                    // cardcode = "", cardname = "", datecreated = "", doccreator = "", status = "", pageno = "1"
                    other_options = ResolveUrl("~/") + "Document/MMAgreements";
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
