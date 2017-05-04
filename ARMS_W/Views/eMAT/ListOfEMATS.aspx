<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %><%@ Import Namespace="System.Data.OleDb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        DataTable ematTable;
        string usrname = Session["username"].ToString();
        string cardcode = "", buyer_name = "", status = "", pageno = "1";
        
        // _User Ousr = new _User(usrname);
        _User Ousr = (_User)Session["Ousr"];

        if (Request.QueryString["cardcode"] != "" && Request.QueryString["cardcode"] != null)
        {
            cardcode = Request.QueryString["cardcode"].ToString();
        }

        if (Request.QueryString["buyer_name"] != "" && Request.QueryString["buyer_name"] != null)
        {
            buyer_name = Request.QueryString["buyer_name"].ToString();
        }
        
        if (Request.QueryString["status"] != null)
        {
            status = Request.QueryString["status"].ToString();

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
        }
        
    %>

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>E-MAT List</b>
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
                <td colspan="2" align="right">
                <% 
                    if ( Ousr.HasPositionOf("csr") != -1 ) { 
                %>
                    <a id="sub_menu_link_1" href="<%=ResolveUrl("~/") %>eMAT/Create">>> Create New E-MAT</a>
                <% 
                    } 
                %> 
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:0;">
		<table id="tbl_lst_eMAT" width="100%" cellpadding="2" cellspacing="0" border="0" style="color:#000000;">
            <tr>
                <th align="left">E-MAT Document No.</th>
			    <th align="left">Buyer's Name</th>
			    <th align="left">Account Code</th>
                <th align="left">Status</th>
                <th align="left">&nbsp;</th>
            </tr>
            
            <% 
                const int IS_NOT_FOUND = -1;

                ListDocuments LDocs = new ListDocuments();
                Int32 TotalDocs = 0;
                Int32 TotalPages = 0;
                Int32 DocsPerPage = 30;
                Int32 StartingRecordNumber = 0;
                Int32 EndingRecordNumber = 0;
                Int32 PageNo = Convert.ToInt32(pageno);
                Random RandGen = new Random();
                
                ematTable = SqlDbHelper.getDataDT(SqlQueryHelper.EMATListFiltered(cardcode, buyer_name, status));

                string[] RegionUsers = { "csr", "csm", "cnc", "fnm", "vw1" };
                string[] AreaUsers = { "asm", "so" };
                string[] ChannelUsers = { "chm", "cmg", "ca", "cmm" };
                string[] OtherUsers = { "vpbsm", "vptfi", "ceo", "ssm", "ssgm", "brd", "admin" };
                
                foreach (DataRow item in ematTable.Rows)
                {
                    bool CanViewCurrentDoc = false;

                    if (Ousr.HasPosAndRegionsOf(RegionUsers, item["region"].ToString()))
                    {
                        // REGION
                        CanViewCurrentDoc = true;
                    }
                    else if (Ousr.HasPosAndAreasOf(AreaUsers, item["area"].ToString()))
                    {
                        // AREA
                        CanViewCurrentDoc = true;
                    }
                    else if (Ousr.HasPosAndChannelsOf(ChannelUsers, item["channel"].ToString()))
                    {
                        // CHANNEL
                        CanViewCurrentDoc = true;
                    }
                    else if (Ousr.HasPositionsOf(OtherUsers))
                    {
                        // VPs
                        CanViewCurrentDoc = true;
                    }


                    if (CanViewCurrentDoc == true)
                    {
                        LDocs.DocList.Add(
                                new string[] { 
                                    item["ematno"].ToString().Trim(), 
                                    item["buyer"].ToString(), 
                                    item["acctcode"].ToString(), 
                                    item["statedesc"].ToString().Trim()
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
                    Response.Write("<td align=\"left\"><a href=\"" + ResolveUrl("~/") + "eMAT/eMATDetails?eMATno=" + LDocs.DocList[i][0].ToString() + "\">" + LDocs.DocList[i][0].ToString() + "</a></td>");
                    Response.Write("<td align=\"left\">" + LDocs.DocList[i][1].ToString() + "</td>");
                    Response.Write("<td align=\"left\">" + LDocs.DocList[i][2].ToString() + "</td>");
                    Response.Write("<td align=\"left\">" + LDocs.DocList[i][3].ToString() + "</td>");
                    Response.Write("<td align=\"left\">&nbsp;</td>");
                    Response.Write("</tr>");
                }


                string str_first = "";
                string str_prev = "";
                string str_next = "";
                string str_last = "";
                string other_options = "";

                // cardcode = "", cardname = "", datecreated = "", doccreator = "", status = "", pageno = "1"
                other_options = ResolveUrl("~/") + "eMat/ListOfEMATS";
                other_options = other_options + "?did=" + RandGen.NextDouble().ToString();

                if (cardcode != "") other_options = other_options + "&cardcode=" + cardcode;
                if (buyer_name != "") other_options = other_options + "&buyer_name=" + buyer_name;
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