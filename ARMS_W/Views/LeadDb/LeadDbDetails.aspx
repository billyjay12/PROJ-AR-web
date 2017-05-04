<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        
        string DocId = Request.QueryString["requestid"].ToString();
        
        // _User oUsr = new _User(Session["username"].ToString());
        _User oUsr = (_User)Session["Ousr"];
        _Document oDocumnt = new _Document("LDB", DocId);
        
        DataTable mtable;
        mtable = SqlDbHelper.getDataDT("select *, " +
            "convert(varchar(10), nego_date, 101) as 'formatted_negodate', " +
            "isnull(convert(varchar(10), nego_qoute_date, 101),'') as 'formatted_nego_qoute_date', " +
            "isnull(convert(varchar(10), nego_followup_date, 101),'') as 'formatted_nego_followup_date', " +
            "isnull(convert(varchar(10), ls_date, 101),'') as 'formatted_ls_date', " +
            "isnull(convert(varchar(10), closed_date, 101),'') as 'formatted_closed_date', " +
            "isnull(convert(varchar(10), conf_est_delivery_date, 101),'') as 'formatted_conf_est_delivery_date', " +
            "isnull(convert(varchar(10), conf_date_confirmed, 101),'') as 'formatted_conf_date_confirmed' " +
            "from customerLeadDb where requestid='" + Request.QueryString["requestid"].ToString() + "'");

        DataTable mtable_reclassed;
        mtable_reclassed = SqlDbHelper.getDataDT("select case when ccanum is NULL or ccanum = '' then 'false' else 'true' end as 'is_reclassed' from customerLeadI where requestid='" + DocId + "'");
        
        bool is_confirmed = false;
        bool is_reclassed = false;

        foreach (DataRow item in mtable_reclassed.Rows)
        {
            if (item["is_reclassed"].ToString() == "true")
            {
                is_reclassed = true;
            }
        }
        
    %>
    <link href="<%=ResolveUrl("~/") %>Content/LeadDbDetails.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/LeadDbDetails.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";

         $(function () {
            
            $("#txt_nego_date").datepicker();
            $("#txt_qt_submit_date").datepicker();
            $("#txt_followup_date").datepicker();
            $("#txt_ls_date").datepicker();
            $("#txt_closed_date").datepicker();
            $("#txt_est_delivery_date").datepicker();
            $("#txt_date_confirmed").datepicker();

            $("#chk_nego").click(
                function(){
                    $("#chk_nego").attr('checked', 'checked');
                }
            );

            $("#is_contacted").click(
                function(){
                    $("#is_contacted").attr('checked', 'checked');
                }
            );

            $("#is_qt_submitted").click(
                function(){
                    $("#is_qt_submitted").attr('checked', 'checked');
                }
            );

            $("#is_for_followup").click(
                function(){
                    $("#is_for_followup").attr('checked', 'checked');
                }
            );

            $("#rdo_lost_sales").click(
                function (){
                    
                    if ( $("#rdo_lost_sales").attr('checked') == 'checked' ) {
                        enable_rdo_ls_group();
                        disable_rdo_close_group();

                        $("#txt_ls_date").addClass("required_fields");
                    }
                }
            );

            $("#rdo_Closed").click(
                function (){
                    if ( $("#rdo_Closed").attr('checked') == 'checked' ) {
                        disable_rdo_ls_group();
                        enable_rdo_close_group();

                        $("#txt_closed_amount").addClass("required_fields");
                        $("#txt_closed_date").addClass("required_fields");
                    }
                }
            );

            disable_group1();
            disable_group2();
            disable_group3();

            disable_rdo_ls_group();
            disable_rdo_close_group();

            Load_data();
        });

        function Load_data(){
            var m_ldb_request_id = "";
            var m_ldb_sapleadcode = "";
            var m_ldb_is_nego_contacted = "";
            var m_ldb_formatted_negodate = "";
            var m_ldb_nego_contact_person = "";
            var m_ldb_nego_contact_number = "";
            var m_ldb_nego_qoute_submitted = "";
            var m_ldb_formatted_nego_qoute_date = "";
            var m_ldb_nego_followup = "";
            var m_ldb_formatted_nego_followup_date = "";
            var m_ldb_is_lost_sales = "";
            var m_ldb_formatted_ls_date = "";
            var m_ldb_ls_reason1 = "";
            var m_ldb_ls_reason2 = "";
            var m_ldb_ls_reason3 = "";
            var m_ldb_ls_reason4 = "";
            var m_ldb_ls_reason5 = "";
            var m_ldb_ls_reason6 = "";
            var m_ldb_ls_reason6_desc = "";
            var m_ldb_is_closed = "";
            var m_ldb_closed_total_amount = "";
            var m_ldb_closed_date = "";
            var m_ldb_is_conf_encoded = "";
            var m_ldb_conf_po_no = "";
            var m_ldb_conf_so_no = "";
            var m_ldb_conf_est_delivery_date = "";
            var m_ldb_conf_attachments = "";
            var m_ldb_conf_confirmed_by = "";
            var m_ldb_conf_date_confirmed = "";
            var m_ldb_status = "";

            <% foreach(DataRow item in mtable.Rows){ %>
                m_ldb_request_id = "<%: Request.QueryString["requestid"].ToString() %>";
                m_ldb_sapleadcode = "<%: item["sapleadcode"].ToString() %>";

                m_ldb_is_nego_contacted = "<%: item["is_nego_contacted"].ToString() %>";
                m_ldb_formatted_negodate = "<%: item["formatted_negodate"].ToString() %>";
                m_ldb_nego_contact_person = "<%: item["nego_contact_person"].ToString() %>";
                m_ldb_nego_contact_number = "<%: item["nego_contact_number"].ToString() %>";

                m_ldb_nego_qoute_submitted = "<%: item["nego_qoute_submitted"].ToString() %>";
                m_ldb_formatted_nego_qoute_date = "<%: item["formatted_nego_qoute_date"].ToString() %>";
                m_ldb_nego_qoute_receivedby = "<%: item["nego_qoute_receivedby"].ToString() %>";

                m_ldb_nego_followup = "<%: item["nego_followup"].ToString() %>";
                m_ldb_formatted_nego_followup_date = "<%: item["formatted_nego_followup_date"].ToString() %>";

                m_ldb_is_lost_sales = "<%: item["is_lost_sales"].ToString() %>";
                m_ldb_formatted_ls_date = "<%: item["formatted_ls_date"].ToString() %>";
                m_ldb_ls_reason1 = "<%: item["ls_reason1"].ToString() %>";
                m_ldb_ls_reason2 = "<%: item["ls_reason2"].ToString() %>";
                m_ldb_ls_reason3 = "<%: item["ls_reason3"].ToString() %>";
                m_ldb_ls_reason4 = "<%: item["ls_reason4"].ToString() %>";
                m_ldb_ls_reason5 = "<%: item["ls_reason5"].ToString() %>";
                m_ldb_ls_reason6 = "<%: item["ls_reason6"].ToString() %>";
                m_ldb_ls_reason6_desc = "<%: item["ls_reason6_desc"].ToString() %>";
                m_ldb_is_closed = "<%: item["is_closed"].ToString() %>";
                m_ldb_closed_total_amount = "<%: item["closed_total_amount"].ToString() %>";
                m_ldb_closed_date = "<%: item["formatted_closed_date"].ToString() %>";
                m_ldb_is_conf_encoded = "<%: item["is_conf_encoded"].ToString() %>";
                m_ldb_conf_po_no = "<%: item["conf_po_no"].ToString() %>";
                m_ldb_conf_so_no = "<%: item["conf_so_no"].ToString() %>";
                m_ldb_conf_est_delivery_date = "<%: item["formatted_conf_est_delivery_date"].ToString() %>";
                m_ldb_conf_attachments = "<%: item["conf_attachments"].ToString() %>";
                m_ldb_conf_confirmed_by = "<%: item["conf_confirmed_by"].ToString() %>";
                m_ldb_conf_date_confirmed = "<%: item["formatted_conf_date_confirmed"].ToString() %>";
                m_ldb_status = "<%: item["status"].ToString() %>";

                <%

                    if (item["is_conf_encoded"].ToString() == "1"){
                        is_confirmed = true;
                        Response.Write("DisableConfirmGroup();");
                    }
                    
                %>

            <% } %>
            
            $("#txt_lead_req_id").attr('value', m_ldb_sapleadcode);
            $("#txt_lead_req_id").attr('value_id', m_ldb_request_id);

            /* -- */
            if (m_ldb_is_nego_contacted == "1"){ 
                $("#is_contacted").removeAttr('disabled'); 
            }
            $("#txt_nego_date").attr('value', m_ldb_formatted_negodate);
            $("#txt_nego_contact_person").attr('value', m_ldb_nego_contact_person);
            $("#txt_nego_contact_number").attr('value', m_ldb_nego_contact_number);

            /* -- */
            if (m_ldb_is_nego_contacted == "1"){ 
                $("#is_qt_submitted").removeAttr('disabled'); 
                enable_group2();
            }
            $("#txt_qt_submit_date").attr('value', m_ldb_formatted_nego_qoute_date);
            $("#txt_qoute_received_by").attr('value', m_ldb_nego_qoute_receivedby);
            
            /* -- */
            if (m_ldb_nego_qoute_submitted == "1"){ 
                $("#is_for_followup").removeAttr('disabled'); 
                disable_group2();
                enable_group3();
            }
            $("#txt_followup_date").attr('value', m_ldb_formatted_nego_followup_date);

            if (m_ldb_is_lost_sales == "1"){ 
                $("#rdo_lost_sales").attr('checked', 'checked');
            }
            
            $("#txt_ls_date").attr('value', m_ldb_formatted_ls_date);
            if (m_ldb_ls_reason1 == "1"){ $("#chk_reason1").attr('checked', 'checked'); }
            if (m_ldb_ls_reason2 == "1"){ $("#chk_reason2").attr('checked', 'checked'); }
            if (m_ldb_ls_reason3 == "1"){ $("#chk_reason3").attr('checked', 'checked'); }
            if (m_ldb_ls_reason4 == "1"){ $("#chk_reason4").attr('checked', 'checked'); }
            if (m_ldb_ls_reason5 == "1"){ $("#chk_reason5").attr('checked', 'checked'); }
            if (m_ldb_ls_reason6 == "1"){ $("#chk_reason6").attr('checked', 'checked'); }
            $("#txt_reason6_desc").attr('value', m_ldb_ls_reason6_desc);
            
            /* -- */
            if (m_ldb_is_closed == "1"){ 
                $("#rdo_Closed").attr('checked', 'checked');
            }

            $("#txt_closed_amount").attr('value', m_ldb_closed_total_amount);
            $("#txt_closed_date").attr('value', m_ldb_closed_date);

            if (m_ldb_is_closed  == "1" || m_ldb_is_lost_sales == "1"){
                disable_all();
                $("#rdo_lost_sales").unbind('click');
                $("#rdo_lost_sales").attr('disabled', 'disabled');
                $("#rdo_Closed").unbind('click');
                $("#rdo_Closed").attr('disabled', 'disabled');
            }

            // load other data
            $("#txt_po_number").attr('value', m_ldb_conf_po_no);
            $("#txt_so_number").attr('value', m_ldb_conf_so_no);
            $("#txt_est_delivery_date").attr('value', m_ldb_conf_est_delivery_date);
            $("#txt_attachment").attr('value', m_ldb_conf_attachments);
            $("#txt_confirmed_by").attr('value', m_ldb_conf_confirmed_by);
            $("#txt_date_confirmed").attr('value', m_ldb_conf_date_confirmed);

        }
        
    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>Active Lead Details</b>
                </td>
                <td align="right" valign="middle" >
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box">
		<table cellpadding="1" cellspacing="0" border="0" >
            <tr>
                <td>
                    <table>
                        <tr>
                            <td> SAP Lead Code </td>
                            <td><input type="text" id="txt_lead_req_id" onclick="javascript:;" readonly="readonly" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
			<tr>
                <td>
                    <div style="padding:2px;">
                        <label><input id="chk_nego" type="checkbox" checked="checked" /> Ongoing Negotiation </label> <br />

                        <div style="padding:0px 0px 0px 15px; margin:2px;">
                            <label><input id="is_contacted" type="checkbox" disabled="disabled" checked="checked" /> Contacted </label> <br />
                            <table>
                                <tr>
                                    <td>Date:</td>
                                    <td><input id="txt_nego_date" type="text" /></td>
                                </tr>
                                <tr>
                                    <td>Contact Person:</td>
                                    <td><input id="txt_nego_contact_person" type="text" /></td>
                                </tr>
                                <tr>
                                    <td>Contact Number:</td>
                                    <td><input id="txt_nego_contact_number" type="text" /></td>
                                </tr>
                            </table>
                        </div>

                        <div style="padding:0px 0px 0px 15px; margin:2px;">
                            <label><input id="is_qt_submitted" type="checkbox" disabled="disabled" checked="checked" /> Quote Submitted </label> <br />
                            <table>
                                <tr>
                                    <td>Date:</td>
                                    <td><input id="txt_qt_submit_date" type="text" /></td>
                                </tr>
                                <tr>
                                    <td>Quote Received by:</td>
                                    <td><input id="txt_qoute_received_by" type="text" /></td>
                                </tr>
                            </table>
                        </div>  

                        <div style="padding:0px 0px 0px 15px; margin:2px;">
                            <label><input id="is_for_followup" type="checkbox" disabled="disabled" checked="checked" /> for follow up </label> <br />
                            <table>
                                <tr>
                                    <td>Date:</td>
                                    <td><input id="txt_followup_date" type="text" /></td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <hr />
                    <div style="padding:2px;">
                        <label><input id="rdo_lost_sales" type="radio" name="main_group_2" /> Lost Sales </label> <br />

                        <div style="padding:2px;">
                            <table>
                                <tr>
                                    <td>Date:</td>
                                    <td><input type="text" id="txt_ls_date" /></td>
                                </tr>
                                <tr>
                                    <td valign="top">Reason:</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <label><input type="checkbox" id="chk_reason1" />Low pricing from competition</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><input type="checkbox" id="chk_reason2" />Better value proposition from competition/substitutes</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><input type="checkbox" id="chk_reason3" />Uneconomical to Deliver</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><input type="checkbox" id="chk_reason4" />Mismatch on inquired vs sold item/s</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><input type="checkbox" id="chk_reason5" />Very urgent / not enough lead time to produce inquired items</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><input type="checkbox" id="chk_reason6" />Others:</label> <input type="text" id="txt_reason6_desc" />
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>
                            </table>
                        </div>    

                    </div>
                    <hr />
                    <div style="padding:2px;">
                        <label><input id="rdo_Closed" type="radio" name="main_group_2" /> Closed </label> <br />

                        <div style="padding:2px;">
                            <table>
                                <tr>
                                    <td>Total Amount:</td>
                                    <td><input id="txt_closed_amount" type="text" /></td>
                                </tr>
                                <tr>
                                    <td>Date:</td>
                                    <td><input id="txt_closed_date" type="text" /></td>
                                </tr>
                            </table>
                        </div>    

                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                    <% 
                        string is_lost_sales = "";
                        string is_closed = "";
                        foreach (DataRow item in mtable.Rows) { 
                            is_lost_sales = item["is_lost_sales"].ToString();
                            is_closed = item["is_closed"].ToString();
                        }
                        
                    %>
                    
                    <% 
                        if (is_lost_sales != "1" && is_closed != "1")
                        {
                            %> <input type="button" value="Update" onclick="javascript:SaveDoc();" /> <%
                        }
                        else 
                        {
                            // if [closed] or [lost sales]
                            // RECLASS AND CONFIRM ARE FOR CSR ONLY
                            if (
                                (oUsr.HasPositionOf("csr") != -1 && oUsr.HasRegionOf("csr", oDocumnt.Region) == true) ||
                                oUsr.HasPositionOf("csm") != -1 && oUsr.HasRegionOf("csm", oDocumnt.Region) == true
                            ){
                                if (is_reclassed == false && is_lost_sales != "1")
                                {
                                    // if not yet reclassed
                                    // show RECLASS BUTTON
                                    %>
                                        <input type="button" value="Reclass to Customer" onclick="javascript:ReClassLeadDoc('<%:DocId %>');" />
                                    <%
                                }
                                else
                                {
                                    // if reclassed
                                    // show [Confirm Button and Fields]
                                    if (is_lost_sales != "1")
                                    {
                                    %>
                                        <table>
		                                    <tr>
			                                    <td>Purchase Order Number</td>
			                                    <td>
				                                    <input type="text" id="txt_po_number" class="required_fields" />
			                                    </td>
		                                    </tr>
		                                    <tr>
			                                    <td>Sales Order Number</td>
			                                    <td>
				                                    <input type="text" id="txt_so_number" class="required_fields" />
			                                    </td>
		                                    </tr>
		                                    <tr>
			                                    <td>Est. Delivery Date</td>
			                                    <td>
				                                    <input type="text" id="txt_est_delivery_date" class="required_fields" />
			                                    </td>
		                                    </tr>
		                                    <tr>
			                                    <td>Attachment</td>
			                                    <td>
				                                    <input type="text" id="txt_attachment" onclick="javascript:CreateUploadingBox('txt_attachment');" readonly="readonly" style="background-color:#fff7dd;" />
			                                    </td>
		                                    </tr>
		                                    <tr>
			                                    <td>Confirmed by:</td>
			                                    <td>
				                                    <input type="text" id="txt_confirmed_by" class="required_fields" />
			                                    </td>
		                                    </tr>
		                                    <tr>
			                                    <td>Date confirmed:</td>
			                                    <td>
				                                    <input type="text" id="txt_date_confirmed" class="required_fields" />
			                                    </td>
		                                    </tr>
	                                    </table>
                                        <% 
                                        // check if already confirmed
                                        if (is_confirmed != true)
                                        {
                                            %>
                                                <input type="button" value="Confirm Encoded" onclick="javascript:LeadDbConfirm();" />
                                            <%
                                        }
                                    }
                                    %>
                                    
                                    <%
                                }
                            }
                        }
                    %>

                </td>
            </tr>
		</table>

	</div>

    </div>
</asp:Content>
