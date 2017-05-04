<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <% 
          //_User Ousr = new _User(Session["username"].ToString());
          //const int IS_NOT_FOUND = -1;

          DataTable mrktReqTable;
          //string cardcode = "", cardname = "", datecreated = "", doccreator = "";

          //if (Request.QueryString["cardcode"] != "" && Request.QueryString["cardcode"] != null)
          //{
          //    cardcode = Request.QueryString["cardcode"].ToString();
          //}

          //if (Request.QueryString["cardname"] != "" && Request.QueryString["cardname"] != null)
          //{
          //    cardname = Request.QueryString["cardname"].ToString();
          //}

          //if (Request.QueryString["datecreated"] != "" && Request.QueryString["datecreated"] != null)
          //{
          //    datecreated = Request.QueryString["datecreated"].ToString();
          //}

          //if (Request.QueryString["doccreator"] != "" && Request.QueryString["doccreator"] != null)
          //{
          //    doccreator = Request.QueryString["doccreator"].ToString();
          //}
        
    %>

    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/list_accounts.js" type="text/javascript"></script>

    <div class="bl_box">
      
      <div class="page_header">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Marketing Request List</b>
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
                    <a href="<%=ResolveUrl("~/") %>MrktngRequest/mrktRequestCreate">>> New Marketing Request</a>
                 </td>
              </tr>             
          </table>
      </div>

      <div class="simple_box">
		<table id="tbl_lst_mrktngReqList" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
			<tr>
				<th align="left" style="width:150px;">Request No.</th>
				<th align="left" style="width:200px;">Encoded By</th>
				<th align="left" style="width:200px;">Requested By</th>
                <th align="left" style="width:200px;">Status</th>
			</tr>

            </table>
	 </div>

     <div class="simple_box" style="height:650px;overflow:scroll;">
		<table id="tbl_lst_mrktngReqList1" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
            <% 
                mrktReqTable = SqlDbHelper.getDataDT(SqlQueryHelper.ListOfMrktgRqstFiltered(HttpContext.Current));
                _User CurrentUser = new _User(Session["username"].ToString());
                const int IS_NOT_FOUND = -1;

                foreach (DataRow item in mrktReqTable.Rows)
                {
                    string DocBrand = "";
                    bool CanViewCurrentDoc = false;
                    _Document MKAccount = new _Document("MKR", item["reqID"].ToString());

                    switch (item["brand"].ToString())
                    {
                        case "GW": DocBrand = "GUDWOOD"; break;
                        case "MW": DocBrand = "MATWOOD"; break;
                        case "WW": DocBrand = "WEATHERWOOD"; break;
                        case "PW": DocBrand = "PCW"; break;
                        case "TW": DocBrand = "TRUSSWOOD"; break;
                        case "GM": DocBrand = "GMELINA"; break;
                        case "AD": DocBrand = "AIRDRIED"; break;
                        case "ALL BRANDS": DocBrand = "ALL"; break;
                    }
                    
                    // FOR REGION USERS
                    if (
                        (
                            CurrentUser.HasPositionOf("csr") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("cnc") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("csm") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw1") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw2") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw3") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw4") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw5") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw6") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("vw7") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("fnm") != IS_NOT_FOUND
                        ) &&
                        (
                            CurrentUser.HasRegionOf("csr", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("cnc", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("csm", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw1", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw2", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw3", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw4", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw5", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw6", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("vw7", item["region"].ToString()) == true ||
                            CurrentUser.HasRegionOf("fnm", item["region"].ToString()) == true
                        )
                    )
                    {
                        CanViewCurrentDoc = true;
                    }
                    // FOR CHANNEL USERS
                    if (
                         (
                            CurrentUser.HasPositionOf("chm") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("cmg") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("cha") != IS_NOT_FOUND ||
                            CurrentUser.HasPositionOf("cmm") != IS_NOT_FOUND

                         )
                    &&
                         (
                            CurrentUser.HasChannelOf("chm", item["channel"].ToString()) == true ||
                            CurrentUser.HasChannelOf("cmg", item["channel"].ToString()) == true ||
                            CurrentUser.HasChannelOf("cha", item["channel"].ToString()) == true ||
                            CurrentUser.HasChannelOf("cmm", item["channel"].ToString()) == true
                         )

                       )
                    {
                        CanViewCurrentDoc = true;

                    }

                    // FOR AREA USERS
                    if (CurrentUser.HasPositionOf("asm") != IS_NOT_FOUND && CurrentUser.HasAreaOf("asm", item["area"].ToString()) == true)
                    {
                        CanViewCurrentDoc = true;
                    }

                    // FOR SO

                    // VP'S
                    if (
                           CurrentUser.HasPositionOf("vpbsm") != IS_NOT_FOUND ||
                           CurrentUser.HasPositionOf("vptfi") != IS_NOT_FOUND ||
                           CurrentUser.HasPositionOf("ssm") != IS_NOT_FOUND ||
                           CurrentUser.HasPositionOf("ssgm") != IS_NOT_FOUND ||
                           CurrentUser.HasPositionOf("ceo") != IS_NOT_FOUND ||
                           CurrentUser.HasPositionOf("brd") != IS_NOT_FOUND ||
                           CurrentUser.HasPositionOf("admin") != IS_NOT_FOUND

                      )
                    {
                        CanViewCurrentDoc = true;
                    }

                    if (CurrentUser.HasBrandOf("brd", DocBrand) == true || DocBrand == "ALL") CanViewCurrentDoc = true;

                    if (CanViewCurrentDoc == true)
                    {
                            Response.Write("<tr>");
                            Response.Write("<td style=\"width:150px;\" align=\"left\"><a href=\"" + ResolveUrl("~/") + "mrktngRequest/marketingReqDetails?reqID=" + item["reqID"].ToString() + "\">" + item["reqID"].ToString() + "</a></td>");
                            Response.Write("<td style=\"width:200px;\" align=\"left\">" + item["encodeBy"].ToString() + "</td>");
                            Response.Write("<td style=\"width:200px;\" align=\"left\">" + item["requestedBy"].ToString() + "</td>");
                            Response.Write("<td style=\"width:200px;\" align=\"left\">" + item["stateDesc"].ToString() + "</td>");
                            Response.Write("</tr>");
                      
                    }
                }
            %>
		</table>
	 </div>

    </div>

</asp:Content>
