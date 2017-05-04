<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="System.Data.OleDb" %><%@ Import Namespace="ARMS_W.Class" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  

    <%
        string DocId = Request.QueryString["reqID"].ToString();
        
        _User oUsr = new _User(Session["username"].ToString());
        _Document oDocumnt = new _Document("MKR", DocId);

        string DocBrand = "";
        
        DataTable marktingReq;
        marktingReq = SqlDbHelper.getDataDT("SELECT a.*,b.stateDesc,convert(varchar(10),a.setUpDate,101) as 'setUpDateFmtd', convert(varchar(10),a.availDeploy,101) as 'availDeployFmtd', convert(varchar(10),a.actualDeploy,101) as 'actualDeployFmtd', convert(varchar(10),a.entryDate,101) as 'entryDateFmtd' FROM marktingRequest a INNER JOIN approvalState b ON a.status=b.stateID WHERE reqID = '" + DocId + "'" + " AND docType=7");

        foreach (DataRow itm in marktingReq.Rows) 
        {
            switch (itm["brand"].ToString()) 
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
        }
        
        DataTable reqStipulation;
        reqStipulation = SqlDbHelper.getDataDT("SELECT stipulations FROM marketingOtherStipu WHERE reqID='" + DocId + "'");
        
        DataTable reqAttach;
        reqAttach = SqlDbHelper.getDataDT("SELECT filePath FROM marketingAttach WHERE reqID='" + DocId + "'");

        // select the position of the user
        DataRow userInfo;
        userInfo = SqlDbHelper.getDataDT("select * from userHeader where userName='" + Session["username"] + "'").Rows[0];

        // get doc stateid
        DataRow docStatusId;
        docStatusId = SqlDbHelper.getDataDT("select rtrim(ltrim(status)) as 'status' from marktingRequest where reqID= '" + DocId + "'").Rows[0];
        
    %>

    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/Accounts.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Content/AccountDetails.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/mrktingReqDetails.js" type="text/javascript"></script> 

    <script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";
        var GPreparedBy = "";

        $(function () {
            $("#tabs").tabs();
            $("#sub_tab").tabs();

            LoadDataMarketReq();
           
        });

        var marktngReqAcctCode;

        function LoadDataMarketReq() {
            var reqID;
            var ccaNum;
            var status;
            var encodeBy;
            var existingCust;
            var acctCode;
            var acctName;
            var acctAdd;
            var acctArea;
            var acctOfficer;
            var requestedBy;
            var brand;
            var category;
            var setUpDate;
            var size;
            var value;
            var availDeploy;
            var actualDeploy;
            var entryDate;            
            var doc_stat_msg

            <%  foreach (DataRow row in marktingReq.Rows) { %>
                reqID = '<% Response.Write(row["reqID"].ToString().Trim()); %>';
                ccaNum = '<% Response.Write(row["ccaNum"].ToString().Trim()); %>';
                status = '<% Response.Write(row["status"].ToString().Trim()); %>';
                encodeBy = '<% Response.Write(row["encodeBy"].ToString().Trim()); %>';
                existingCust = '<% Response.Write(row["existingCust"].ToString().Trim()); %>';
                acctCode = '<% Response.Write(row["acctCode"].ToString().Trim()); %>';
                acctName = '<% Response.Write(row["acctName"].ToString().Trim()); %>';
                acctAdd = '<% Response.Write(row["acctAdd"].ToString().Trim()); %>';
                acctArea = '<% Response.Write(row["acctArea"].ToString().Trim()); %>';
                acctOfficer = '<% Response.Write(row["acctOfficer"].ToString().Trim()); %>';
                requestedBy = '<% Response.Write(row["requestedBy"].ToString().Trim()); %>';
                brand = '<% Response.Write(row["brand"].ToString().Trim()); %>';
                category = '<% Response.Write(row["category"].ToString().Trim()); %>';
                setUpDate = '<% Response.Write(row["setUpDateFmtd"].ToString().Trim()); %>';
                size = '<% Response.Write(row["size"].ToString().Trim()); %>';
                value = '<% Response.Write(row["value"].ToString().Trim()); %>';
                availDeploy = '<% Response.Write(row["availDeployFmtd"].ToString().Trim()); %>';
                actualDeploy = '<% Response.Write(row["actualDeployFmtd"].ToString().Trim()); %>';
                entryDate = '<% Response.Write(row["entryDateFmtd"].ToString().Trim()); %>';
                doc_stat_msg = '<% Response.Write(row["stateDesc"].ToString().Trim()); %>';
                marktngReqAcctCode = acctCode;
                GPreparedBy=encodeBy;
                
            <%  } %>

            $("#txt_mrktngReqNo").attr('value', reqID );
            $("#txt_encoded_by").attr('value', encodeBy);            
            $("#txt_acct_code").attr('value', acctCode);
            $("#txt_acct_name").attr('value', acctName);
            $("#txt_address").attr('value', acctAdd);
            $("#txt_area").attr('value', acctArea);
            $("#txt_acct_officer").attr('value', acctOfficer);
            $("#txt_request_by").attr('value', requestedBy);
            $("#txt_brand").attr('value', brand);
            $("#txt_category").attr('value', category);
            $("#txt_setup_date").attr('value', setUpDate);
            $("#txt_size").attr('value', size);
            $("#txt_value").attr('value', value);
            $("#txt_avail_deployOn").attr('value', availDeploy);
            $("#txt_actual_deployDate").attr('value', actualDeploy); 
            $("#doc_stat_msg").html(doc_stat_msg);          
            
            // list of stipulation no.
            <%  
                foreach (DataRow row in reqStipulation.Rows) { 
                    Response.Write("AddStipulation(");
                    Response.Write("'" + row["stipulations"].ToString().Trim() + "'");                    
                    Response.Write(");\n");
                } 
            %>

            //List of attachment

            <%  
                foreach (DataRow row in reqAttach.Rows) { 
                    Response.Write("Addattachments(");
                    Response.Write("'" + row["filePath"].ToString().Trim() + "'");                    
                    Response.Write(");\n");
                } 
            %>

            reqAttach

            // disable the brwsibg of file for attachments
            $("#txt_attchmnt1").attr('onclick', '');
            $("#txt_attchmnt2").attr('onclick', '');
            $("#txt_attchmnt3").attr('onclick', '');
            
   }
  
 </script>

   <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>mrktRequestCreate</b>
                </td>
                <td align="right" valign="middle">
                     <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

     
    <div class="simple_box" style="padding:15px; font-size:12px;">
    
    <table align="center" style="margin:5px;" >
        <tr>
            <td colspan="5" align="right" valign="top">
                <span id="doc_stat_msg"></span>
            </td>
        </tr>
    </table>

    <div style="padding:15px; font-size:12px;">
         <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;" >
            <tr>
                <td><b>MARKETING REQUEST No.</b></td>
                <td><input type="text" id="txt_mrktngReqNo" readonly="readonly" /></td>
            </tr>                        
            <tr>
                <td>Encoded By</td>
                <td><input type="text" id="txt_encoded_by" readonly="readonly" /></td>
            </tr>
            <tr>
                <td colspan="2"> &nbsp;</td>
            </tr>
            <tr>
                <td> Account Code </td>
                <td><input type="text" id="txt_acct_code" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Account Name </td>
                <td><input type="text" id="txt_acct_name" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Address</td>
                <td><input type="text" id="txt_address" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Area</td>
                <td><input type="text" id="txt_area" readonly="readonly"/></td>             
            </tr>
            <tr>
                <td> Account Officer</td>
                <td><input type="text" id="txt_acct_officer" readonly="readonly" /></td>             
            </tr>
            <tr>
                <td> Requested By</td>
                <td><input type="text" id="txt_request_by" readonly="readonly" /></td>             
            </tr>
            <tr>
                <td> Brand</td>
                <td><input type="text" id="txt_brand" readonly="readonly" /></td>             
            </tr>
            <tr>
                <td> Category</td>
                <td><input type="text" id="txt_category" readonly="readonly" /></td>             
            </tr>
            <tr>
                <td colspan="2"> &nbsp;</td>
            </tr>
            <tr>
                <td> Setup Date</td>
                <td><input type="text" id="txt_setup_date" readonly="readonly" /></td>             
            </tr>
            <tr>
                <td> Size</td>
                <td><input type="text" id="txt_size" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Value</td>
                <td><input type="text" id="txt_value" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Available for Deployment on</td>
                <td><input type="text" id="txt_avail_deployOn" readonly="readonly" /></td>
            </tr>
            <tr>
                <td> Actual Deployment Date</td>
                <td><input type="text" id="txt_actual_deployDate" readonly="readonly" /></td>
            </tr>
        </table>      
    </div>
        
        <div style="padding:15px; font-size:12px;">
            <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;" id="tbl_other_stipulation">
                <tr>
                    <td align="left" colspan="2"><b>Other Stipulations:</b></td>
                </tr>
                <tr>
                    <td align="left" colspan="2">&nbsp;</td>
                </tr>
            </table>
        </div>


        <!--attachment created by hervie--->

    <div style="padding:11px; font-size:12px;">
         <table id="tbl_attachment" cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
             <tr><b>Attachments:</b></tr>
             <tr>
                <td>
                  <!-- <input type="text" id="txt_attchmnt1"  style="width:150px;" readonly="readonly" />--> 
                </td>
                
             </tr>
         </table>
    </div>



     <!---   <div style="padding:15px; font-size:12px;">
            <table cellpadding="1" cellspacing="0" border="0" style="color:#000000;">
                <tr>
                    <td colspan="2"><b>Attachments:</b></td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="txt_attchmnt1" name="txt_attchmnt_1" size="50" readonly="readonly" onclick="javascript:CreateUploadingBox('txt_attchmnt1');" readonly="readonly" /> 
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    <input type="text" id="txt_attchmnt2" name="txt_attchmnt_2" size="50" onclick="javascript:CreateUploadingBox('txt_attchmnt2');" readonly="readonly" /> 
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="txt_attchmnt3" name="txt_attchmnt_3" size="50" onclick="javascript:CreateUploadingBox('txt_attchmnt3');" readonly="readonly" /> 
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>-->

        <!-- USER SPECIFIC BUTTONS - START -->
        <% //if (userInfo["position"].ToString() == "Channel Mgr." && docStatusId["status"].ToString() == AppHelper.GetUserPositionId("Channel Mgr."))
            if (oUsr.HasPositionOf("asm") != -1 && oUsr.HasAreaOf("asm", oDocumnt.Area) == true && oDocumnt.DocStatus == "1" )
            { 
        %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: DocId %>', '<%: oDocumnt.AccountCode %>');"  /></td>
                        <td><input type="button" value="Cancel" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: DocId %>', '<%: oDocumnt.AccountCode %>');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% } %>

        <% 
            if (oUsr.HasPositionOf("chm") != -1 && oUsr.HasChannelOf("chm", oDocumnt.Channel) == true && oDocumnt.DocStatus == "3")
            { 
        %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: DocId %>', '<%: oDocumnt.AccountCode %>');"  /></td>
                        <td><input type="button" value="Cancel" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: DocId %>', '<%: oDocumnt.AccountCode %>');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% } %>

        <!--
            BRAND MANAGER
        -->
        <% 
        // if (oUsr.HasPositionOf("brm") != -1 && (oUsr.HasBrandOf("brm", DocBrand) == true || DocBrand == "ALL") && oDocumnt.DocStatus == "7")

            if (oUsr.HasPositionOf("brd") != -1 && oDocumnt.DocStatus == "9")
          
            { 
        %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: DocId %>', '<%: oDocumnt.AccountCode %>');"  /></td>
                        <td><input type="button" value="Cancel" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: DocId %>', '<%: oDocumnt.AccountCode %>');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% } %>

        <!--
            BRAND DIRECTOR
        -->
        <% 
            if (oUsr.HasPositionOf("brm") != -1 && oDocumnt.DocStatus == "7")
            { 
        %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: DocId %>', '<%: oDocumnt.AccountCode %>');"  /></td>
                        <td><input type="button" value="Cancel" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: DocId %>', '<%: oDocumnt.AccountCode %>');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% } %>

        <% 
            if (oUsr.HasPositionOf("vpbsm") != -1 && oDocumnt.DocStatus == "5")
            { 
        %>
            <div  style="padding:15px; font-size:12px;">
                <table cellpadding="2" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><input type="button" value="Approve" style="color:#000000;" onclick="javascript:CallRouting('Approve','<%: DocId %>', '<%: oDocumnt.AccountCode %>');"  /></td>
                        <td><input type="button" value="Cancel" style="color:#000000;" onclick="javascript:CallRouting('Disapprove','<%: DocId %>', '<%: oDocumnt.AccountCode %>');" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </div>
        <% } %>
        <!-- USER SPECIFIC BUTTONS - END -->
    </div>
    </div>
 
</asp:Content>
