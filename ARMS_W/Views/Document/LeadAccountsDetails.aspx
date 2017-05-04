<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        string DocRequestId = Request.QueryString["RequestId"].ToString();
        
        const int IS_NOT_FOUND = -1;
        // _User oUsr = new _User(Session["username"].ToString());
        _User oUsr = (_User)Session["Ousr"];

        _Document oDucumnt = new _Document("LDI", DocRequestId);
        
        DataTable customerLeadI;
        customerLeadI = SqlDbHelper.getDataDT("select *, convert(varchar(10), ExhibitDate, 101) as 'ExhibitDateFormatted', convert(varchar(10), DateEncoded, 101) as 'DateEncodedFormatted', convert(varchar(10), InqDate, 101) as 'InqDateFormatted', case when right(ProposedChannel,1) = 'L' then 'Luzon' else 'Vismin' end 'Region' from customerLeadI where RequestId='" + DocRequestId + "'");
        
        // get document's assigned user, and region
        DataRow DocAssignedUser;
        DocAssignedUser = SqlDbHelper.getDataDT("select AssignTo_empId, case when right(proposedchannel,1) = 'L' then (select 'Luzon') else (select 'Vismin') end as 'region' from dbo.customerLeadI where requestid='" + DocRequestId + "'").Rows[0];

        bool is_returned_back = Model.BkToSender == "false" ? false : true;
        
        string str_lead_channel = "";
        
    %>
    <link href="<%=ResolveUrl("~/") %>Content/LeadAccountDetails.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/LeadAccountDetails.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var baseUrl = "<%= ResolveUrl("~/") %>";

        var general_request_id = "";

        $(function () {
			$("#tabs").tabs();

			$("#tabs_assign").tabs();
            $("#txt_exhibit_date").datepicker();
            $("#txt_mark_doc_date").attr('value', '<%: System.DateTime.Now.ToString("MM/dd/yyyy") %>');

            DisableAll();

            $("#web_contact").change( function() {
                    SwitchEnabledFields();
                }
            );
            $("#exhibit_name").change(
                function() {
                    SwitchEnabledFields();
                }
            );

            $("#referred_by").change(
                function() {
                    SwitchEnabledFields();
                }
            );

            $("#coverage").change(
                function() {
                    SwitchEnabledFields();
                }
            );

            $("#other_sources").change(
                function() { 
                    SwitchEnabledFields();
                }
            );

            $("#btn_fnm_disapprove").click(
                function(){
                    FnmDisapproveLead(general_request_id);
                }
            );

            LoadData();

		});

        function LoadData(){
            var m_request_id = "";
            var m_lead_name = "";
            var m_lead_inqdate = "";
            var m_lead_address = "";
            var m_lead_contactno = "";
            var m_lead_email = "";

            var m_is_mw_selected = "";
            var m_is_ww_selected = "";
            var m_is_pw_selected = "";
            var m_is_tw_selected = "";
            var m_is_gw_selected = "";
            var m_is_nw_selected = "";
            var m_is_mu_selected = "";
            var m_is_ec_selected = "";
            var m_is_framing_selected = "";
            var m_is_moulding_selected = "";
            var m_is_flooring_selected = "";
            var m_is_doorjambs_selected = "";
            var m_is_panellings_selected = "";
            var m_is_engditm_selected = "";
            var m_is_decking_selected = "";
            var m_is_staircomp_selected = "";
            var m_is_others_selected = "";
            var m_other_selected_desc = "";

            var m_lead_encoded_date = "";
            var m_lead_encoded_by = "";
            var m_remarks = "";
            var m_lead_proposed_channel = "";
            var m_inquiry_source_web = "";
            var m_inquiry_source_exhibit_name = "";
            var m_inquiry_source_exhibit_date = "";
            var m_inquiry_source_exhibit_address = "";
            var m_inquiry_source_refered_by = "";
            var m_inquiry_source_sales_officer = "";
            var m_inquiry_source_other_source = "";
            var m_request_status = "";
            var m_proposed_lead_code = "";
            var m_sap_lead_code = "";
            var m_assigned_to = "";

            <%  foreach (DataRow row in customerLeadI.Rows) { %>
                m_proposed_lead_code = "<% Response.Write(row["ProposedLeadCode"].ToString().Trim()); %>";
                m_sap_lead_code = "<% Response.Write(row["SapLeadCode"].ToString().Trim()); %>";
                m_request_status = "<% Response.Write(row["Status"].ToString().Trim()); %>";
                m_request_id = "<% Response.Write(row["RequestId"].ToString().Trim()); %>";
                m_lead_name = "<% Response.Write(row["Name"].ToString().Trim()); %>";
                m_lead_inqdate = "<% Response.Write(row["InqDateFormatted"].ToString().Trim()); %>";
                m_lead_address = "<% Response.Write(row["Address"].ToString().Trim()); %>";
                m_lead_contactno = "<% Response.Write(row["ContactNo"].ToString().Trim()); %>";
                m_lead_email = "<% Response.Write(row["E_mail"].ToString().Trim()); %>";
                m_is_mw_selected = "<% Response.Write(row["IsInquired_MWBrand"].ToString().Trim()); %>";
                m_is_ww_selected = "<% Response.Write(row["IsInquired_WWBrand"].ToString().Trim()); %>";
                m_is_pw_selected = "<% Response.Write(row["IsInquired_PWBrand"].ToString().Trim()); %>";
                m_is_tw_selected = "<% Response.Write(row["IsInquired_TWBrand"].ToString().Trim()); %>";
                m_is_gw_selected = "<% Response.Write(row["IsInquired_GWBrand"].ToString().Trim()); %>";
                m_is_nw_selected = "<% Response.Write(row["IsInquired_NWBrand"].ToString().Trim()); %>";
                m_is_mu_selected = "<% Response.Write(row["IsInquired_MUBrand"].ToString().Trim()); %>";
                m_is_ec_selected = "<% Response.Write(row["IsInquired_ECBrand"].ToString().Trim()); %>";
                m_is_framing_selected = "<% Response.Write(row["IsInquired_Framing"].ToString().Trim()); %>";
                m_is_moulding_selected = "<% Response.Write(row["IsInquired_Mouldings"].ToString().Trim()); %>";
                m_is_flooring_selected = "<% Response.Write(row["IsInquired_Floorings"].ToString().Trim()); %>";
                m_is_doorjambs_selected = "<% Response.Write(row["IsInquired_DoorJambs"].ToString().Trim()); %>";
                m_is_panellings_selected = "<% Response.Write(row["IsInquired_Panellings"].ToString().Trim()); %>";
                m_is_engditm_selected = "<% Response.Write(row["IsInquired_EngdItm"].ToString().Trim()); %>";
                m_is_decking_selected = "<% Response.Write(row["IsInquired_Decking"].ToString().Trim()); %>";
                m_is_staircomp_selected = "<% Response.Write(row["IsInquired_StairComp"].ToString().Trim()); %>";
                m_is_others_selected = "<% Response.Write(row["IsInquired_Others"].ToString().Trim()); %>";
                m_other_selected_desc = "<% Response.Write(row["OtherInquiry_desc"].ToString().Trim()); %>";
                m_lead_encoded_date = "<% Response.Write(row["DateEncodedFormatted"].ToString().Trim()); %>";
                m_lead_encoded_by = "<% Response.Write(row["EnCodedby"].ToString().Trim()); %>";
                m_remarks = "<% Response.Write(row["Remarks"].ToString().Replace("\n","").Replace("\r","&#10;").Trim()); %>";
                m_lead_proposed_channel = "<% Response.Write(row["ProposedChannel"].ToString().Trim()); %>";
                <% str_lead_channel = row["ProposedChannel"].ToString().Trim(); %>
                m_inquiry_source_web = "<% Response.Write(row["WebContactFrm"].ToString().Trim()); %>";
                m_inquiry_source_exhibit_name = "<% Response.Write(row["ExhibitName"].ToString().Trim()); %>";
                m_inquiry_source_exhibit_date = "<% Response.Write(row["ExhibitDateFormatted"].ToString().Trim()); %>";
                m_inquiry_source_exhibit_address = "<% Response.Write(row["ExhibitAddress"].ToString().Trim()); %>";
                m_inquiry_source_refered_by = "<% Response.Write(row["ReferedBy"].ToString().Trim()); %>";
                m_inquiry_source_sales_officer = "<% Response.Write(row["SalesOfficer"].ToString().Trim()); %>";
                m_inquiry_source_other_source = "<% Response.Write(row["OtherSources"].ToString().Trim()); %>";
                m_assigned_to = "<% Response.Write(row["AssignTo_AsmSo"].ToString().Trim()); %>";
            <%  } %>

            general_request_id = m_request_id;

            if (m_inquiry_source_web != "") {
                // select option1
                $("#web_contact").attr('checked', 'checked');
            }

            if (m_inquiry_source_exhibit_name != "" || m_inquiry_source_exhibit_date != "" || m_inquiry_source_exhibit_address != "") {
                // select option2
                $("#exhibit_name").attr('checked', 'checked');
            }

            if (m_inquiry_source_refered_by != "") {
                // select option3
                $("#referred_by").attr('checked', 'checked');
            }

            if (m_inquiry_source_sales_officer != "") {
                // select option4
                $("#coverage").attr('checked', 'checked');
            }

            if (m_inquiry_source_other_source != "") {
                // select option5
                $("#other_sources").attr('checked', 'checked');
            }

            SwitchEnabledFields();

            $("#txt_encoded_by").attr("value", m_lead_encoded_by);
            $("#txt_request_id").attr("value", m_request_id);
            $("#txt_date_encoded").attr("value", m_lead_encoded_date);
            $("#txt_web_contact_form_no").attr("value", m_inquiry_source_web);
            $("#txt_exhibit_name").attr("value", m_inquiry_source_exhibit_name);
            $("#txt_exhibit_date").attr("value", m_inquiry_source_exhibit_date);
            $("#txt_exhibit_loc").attr("value", m_inquiry_source_exhibit_address);
            $("#txt_refered_by").attr("value", m_inquiry_source_refered_by);
            $("#txt_sales_officer").attr("value", m_inquiry_source_sales_officer);
            $("#txt_other_sources").attr("value", m_inquiry_source_other_source);
            $("#txt_inquiry_name").attr("value", m_lead_name);
            $("#txt_inquiry_date").attr("value", m_lead_inqdate);
            $("#txt_inquiry_address").attr("value", m_lead_address);
            $("#txt_inquiry_number").attr("value", m_lead_contactno);
            $("#txt_inquiry_email").attr("value", m_lead_email);
            $("#txt_propose_lead_code_data").attr("value", m_proposed_lead_code);
            $("#txt_sap_lead_code_data").attr("value", m_sap_lead_code);
            $("#txt_assigned_to").attr("value", m_assigned_to);

            $("#txt_other_prod").attr("value", m_other_selected_desc);

            if(m_is_mw_selected == "true"){
                $("#opt_inqured_mw").attr("checked", "checked");
            }

            if(m_is_ww_selected == "true"){
                $("#opt_inqured_ww").attr("checked", "checked");
            }

            if(m_is_pw_selected == "true"){
                $("#opt_inqured_pw").attr("checked", "checked");
            }

            if(m_is_tw_selected == "true"){
                $("#opt_inqured_tw").attr("checked", "checked");
            }

            if(m_is_gw_selected == "true"){
                $("#opt_inqured_gw").attr("checked", "checked");
            }

            if(m_is_nw_selected == "true"){
                $("#opt_inqured_nw").attr("checked", "checked");
            }

            if(m_is_mu_selected == "true"){
                $("#opt_inqured_mu").attr("checked", "checked");
            }

            if(m_is_ec_selected == "true"){
                $("#opt_inqured_ec").attr("checked", "checked");
            }

            // ------------------------
            if(m_is_framing_selected == "true"){
                $("#opt_inqured_framing").attr("checked", "checked");
            }

            if(m_is_moulding_selected == "true"){
                $("#opt_inqured_moulding").attr("checked", "checked");
            }

            if(m_is_flooring_selected == "true"){
                $("#opt_inqured_flooring").attr("checked", "checked");
            }

            if(m_is_doorjambs_selected == "true"){
                $("#opt_inqured_doorjamb").attr("checked", "checked");
            }

            if(m_is_panellings_selected == "true"){
                $("#opt_inqured_panellings").attr("checked", "checked");
            }

            if(m_is_engditm_selected == "true"){
                $("#opt_inqured_engitm").attr("checked", "checked");
            }

            if(m_is_decking_selected == "true"){
                $("#opt_inqured_decking").attr("checked", "checked");
            }

            if(m_is_staircomp_selected == "true"){
                $("#opt_inqured_staircomp").attr("checked", "checked");
            }

            if(m_is_others_selected == "true"){
                $("#opt_other_prod").attr("checked", "checked");
            }

            $("#txt_remarks").attr("value", m_remarks.replace(/&#10;/g, "\n"));

            $("#txt_proposed_lead_channel").attr("value", m_lead_proposed_channel);

            var doc_stat_msg = "<%: AppHelper.GetLdbIDocStatMsg(Model.Status).Trim() %>";
            $("#doc_stat_msg").html(doc_stat_msg);

            <% 
                if ((oUsr.HasPositionOf("csr") != IS_NOT_FOUND || oUsr.HasPositionOf("tmg") != IS_NOT_FOUND || oUsr.HasPositionOf("fsp") != IS_NOT_FOUND || oUsr.HasPositionOf("brm") != IS_NOT_FOUND  ) &&
                    is_returned_back == true && oUsr.UserName == Model.EncodedBy ) { } else { %> DisableEditing(); disabled_radio_buttons(); <% }
            %>

        }

    </script>

    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Account Details</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="simple_box" style="padding:15px; font-size:12px;">
		<table width="100%" cellpadding="1" cellspacing="0" border="0" >
            <tr>
                <td colspan="4" align="right">
                    <span id="doc_stat_msg"></span>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <h2>Info</h2><hr />
                    <table cellpadding="1" cellspacing="0" border="0">
                        <tr>
                            <td>Request Id/Doc no</td>
                            <td><input type="text" id="txt_request_id" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Encoded by</td>
                            <td><input type="text" id="txt_encoded_by" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Date Encoded</td>
                            <td><input type="text" id="txt_date_encoded" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Proposed Channel</td>
                            <td><input type="text" id="txt_proposed_lead_channel" onclick="javascript:LookUpData('txt_proposed_lead_channel', 'ListOfChannel');" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Proposed Lead Code</td>
                            <td><input type="text" id="txt_propose_lead_code_data" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>SAP Lead Code</td>
                            <td><input type="text" id="txt_sap_lead_code_data" readonly="readonly" /></td>
                        </tr>
                    </table>
                </td>
                <td colspan="2" valign="top">
                    <h2>Inquiry Source</h2><hr />
                    <table cellpadding="1" cellspacing="0" border="0" style="margin:3px;" >
                        <tr>
                            <td><input type="radio" id="web_contact" name="option1" /></td>
                            <td>Web Contact Form #:</td>
                            <td><input type="text" id="txt_web_contact_form_no" /></td>
                        </tr>
                        <tr>
                            <td colspan="3"><hr /></td>
                        </tr>
                        <tr>
                            <td><label><input type="radio" id="exhibit_name" name="option1" /></label></td>
                            <td>Exhibit Name:</td>
                            <td><input type="text" id="txt_exhibit_name" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Exhibit Date:</td>
                            <td>
                                <input type="text" id="txt_exhibit_date" readonly="readonly" />
                                <input type="text" id="txt_dummy_date" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Exhibit Address:</td>
                            <td><input type="text" id="txt_exhibit_loc" /></td>
                        </tr>
                        <tr>
                            <td colspan="3"><hr /></td>
                        </tr>
                        <tr>
                            <td><input type="radio" id="referred_by" name="option1" /></td>
                            <td>Referred By:</td>
                            <td><input type="text" id="txt_refered_by" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                        </tr>
                        <tr>
                            <td><input type="radio" id="coverage" name="option1" /></td>
                            <td>Coverage: (Sales Officer)</td>
                            <td><input type="text" id="txt_sales_officer" onclick="javascript:LookUpData('txt_sales_officer', 'ListOfSo');" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td colspan="3"><hr /></td>
                        </tr>
                        <tr>
                            <td><input type="radio" id="other_sources" name="option1" /></td>
                            <td>Other Sources:</td>
                            <td><input type="text" id="txt_other_sources" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    <h2>Inquiring Party</h2><hr />
                    <table cellpadding="1" cellspacing="0" border="0">
                        <tr>
                            <td>Name</td>
                            <td><input type="text" id="txt_inquiry_name" /></td>
                            <td>Date of Inquiry</td>
                            <td><input type="text" id="txt_inquiry_date" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td>Address</td>
                            <td><input type="text" id="txt_inquiry_address" /></td>
                            <td>Email Address</td>
                            <td><input type="text" id="txt_inquiry_email" /></td>
                        </tr>
                        <tr>
                            <td>Contact Number</td>
                            <td><input type="text" id="txt_inquiry_number" /></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    <h2>Brand/s Inquired</h2><hr />
                    <table width="100%" cellpadding="1" cellspacing="0" border="0">
						<tr>
							<td align="center">
								<input type="checkbox" id="opt_inqured_mw" /> MW
							</td>
							<td align="center">
								<input type="checkbox" id="opt_inqured_ww" /> WW
							</td>
							<td align="center">
								<input type="checkbox" id="opt_inqured_pw" /> PCW
							</td>
							<td align="center">
								<input type="checkbox" id="opt_inqured_tw" /> TW
							</td>
							<td align="center">
								<input type="checkbox" id="opt_inqured_gw" /> GW
							</td>
                            <td align="center">
								<input type="checkbox" id="opt_inqured_nw" /> NuWood
							</td>
                            <td align="center">
								<input type="checkbox" id="opt_inqured_mu" /> MuZu
							</td>
                            <td align="center">
								<input type="checkbox" id="opt_inqured_ec" /> Ecofor
							</td>
						</tr>
					</table>
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    <h2>Type of Item Inquired</h2><hr />
                    <table width="100%" cellpadding="1" cellspacing="0" border="0">
						<tr>
							<td><label><input type="checkbox" id="opt_inqured_framing" /> Framings & S4S</label></td>
							<td>&nbsp;</td>
							<td><label><input type="checkbox" id="opt_inqured_moulding" /> Mouldings</label></td>
						</tr>
						<tr>
							<td><label><input type="checkbox" id="opt_inqured_flooring" /> Floorings</label></td>
							<td>&nbsp;</td>
							<td><label><input type="checkbox" id="opt_inqured_doorjamb" /> Doors & Door Jambs</label></td>
						</tr>
						<tr>
							<td><label><input type="checkbox" id="opt_inqured_panellings" /> Panellings</label></td>
							<td>&nbsp;</td>
							<td><label><input type="checkbox" id="opt_inqured_engitm" /> Engineered Items</label></td>
						</tr>
						<tr>
							<td><label><input type="checkbox" id="opt_inqured_decking" /> Decking</label></td>
							<td>&nbsp;</td>
							<td><label><input type="checkbox" id="opt_inqured_staircomp" /> Stair Components</label></td>
						</tr>
						<tr>
							<td colspan="3">
								<label>
									<input type="checkbox" id="opt_other_prod" /> 
									Others: Please specify
								</label>
								<input type="text" id="txt_other_prod" /> 
							</td>
						</tr>
					</table>
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    <h2>Additional Information</h2><hr />
                    <textarea id="txt_remarks" style="width:99%; height:100px; border:1px solid #9a9a9a;">
                    </textarea>
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    Assigned To: <input type="text" id="txt_assigned_to" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" >
                    <!-- USER SPECIFIC BUTTONS -->

                    <div class="simple_box" style="background:#ededed;" >
                        
                        <% 
                            if (
                                (
                                    oUsr.HasPositionOf("csr") != IS_NOT_FOUND ||
                                    oUsr.HasPositionOf("tmg") != IS_NOT_FOUND ||
                                    oUsr.HasPositionOf("fsp") != IS_NOT_FOUND ||
                                    oUsr.HasPositionOf("brm") != IS_NOT_FOUND 
                                ) &&
                                is_returned_back == true &&
                                oUsr.UserName == Model.EncodedBy
                            ) { 
                        %>
                            <input type="button" value="Update" onclick="javascript:SaveLeadDoc('<%: DocRequestId %>');" />
                        <%  } %>

                        <% if (
                                oUsr.HasPositionOf("chm") != IS_NOT_FOUND &&
                                Model.Status == AppHelper.GetUserPositionId("chm") 
                           ) {
                               bool CanMarkDoc = false;
                               foreach (string user_channel_name in oUsr.Roles[oUsr.HasPositionOf("chm")].Channel) 
                               {
                                   if (oDucumnt.IsInDocGroupChannel(user_channel_name)) CanMarkDoc = true;
                               }

                               if (CanMarkDoc)
                               {
                        %>
							<!-- CHM -->
                            <h2>Assign to</h2><hr />
                            <table cellpadding="1" cellspacing="0" border="0">
                                <tr>
                                    <td align="left">
                                        <label onclick="javascript:SwitchTextBoxes('asm');">
                                            <input type="radio" name="group_sub_2" id="opt_assign_asm" /> assign to ASM 
                                        </label>
                                    </td>
                                    <td align="left">
                                        <input type="text" id="txt_to_asm" value_id="" onclick="javascript:LookUpData('txt_to_asm', 'ListOfSoAsm', '<%:str_lead_channel %>');" disabled="disabled" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td align="left">
                                        Date
                                    </td>
                                    <td align="left"><input type="text" id="txt_mark_doc_date" readonly="readonly" class="required_fields" /></td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <label onclick="javascript:SwitchTextBoxes('so');">
                                            <input type="radio" name="group_sub_2" id="opt_assign_so" /> assign to SO 
                                        </label>
                                    </td>
                                    <td align="left">
                                        <input type="text" id="txt_to_so" value_id="" onclick="javascript:LookUpData('txt_to_so', 'ListOfSo', '<%:str_lead_channel %>');" disabled="disabled" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td align="left">
                                        Remarks
                                    </td>
                                    <td align="left">
                                        <input type="text" id="txt_mark_doc_remarks" class="required_fields" />
                                    </td>
                                </tr>
                            </table>

                            <br />

                            <center>
                                <input type="button" value="Endorse" onclick="javascript:MarkLeadDocument('ENDORSE', '<%: DocRequestId %>');" /> 
                                &nbsp;/&nbsp;
                                <input type="button" value="Disapprove" onclick="javascript:MarkLeadDocument('DISAPPROVE', '<%: DocRequestId %>');" /> 
                                <!-- HIDE THE SEND_BACK BUTTON (NEEDS RECHECK)
                                &nbsp;/&nbsp;
                                <input type="button" value="Send back to Requester" onclick="javascript:MarkLeadDocument('SEND_BACK_TO_REQUESTER', '<%: DocRequestId %>');" /> 
                                -->
                            </center>
                            <!-- CHM -->
                        <% 
                               }
                            } 
                        %>

                        <% 
                            if(
                                oUsr.HasPositionOf("csr") != IS_NOT_FOUND &&
                                Model.BkToSender == "false" &&
                                oUsr.HasPositionOf(AppHelper.GetUserPosition(Model.Status)) != IS_NOT_FOUND &&
                                oUsr.HasRegionOf("csr", oDucumnt.Region) &&
                                Model.Status != "1000"
                             ) { 
                        %>
                            <!-- CSR -->
                            <center>
                            <table cellpadding="1" cellspacing="0" border="0" style="margin:5px;">
                                <tr>
                                    <td align="left">Proposed Lead Code:</td>
                                    <td align="left">
                                        <input type="text" id="txt_proposed_lead_code" />
                                        <a href="javascript:CheckAcctcode();"><img src="<%=ResolveUrl("~/") %>Images/check_icon.png" style="border:0;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <input type="button" value="Save" onclick="javascript:MarkLeadDocument('PROPOSE_LEAD_CODE', '<%: DocRequestId %>');" />
                                    </td>
                                </tr>
                            </table>
                            </center>
                            <!-- CSR -->
                        <% } %>

                        <% 
                            if(
                                Model.Status == AppHelper.GetUserPositionId("fnm") &&
                                oUsr.HasPositionOf("fnm") != IS_NOT_FOUND &&
                                oUsr.HasRegionOf("fnm", oDucumnt.Region)
                            ) 
                            { 
                        %>
                            <!-- FNM -->
                            <center>
                            <table cellpadding="1" cellspacing="0" border="0" style="margin:5px;">
                                <tr>
                                    <td align="left">SAP Lead Code:</td>
                                    <td align="left"><input type="text" id="txt_sap_proposed_lead_code" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <input type="button" value="Save" onclick="javascript:MarkLeadDocument('ASSIGN_SAP_LEAD_CODE', '<%: DocRequestId %>');" />
                                        &nbsp; / &nbsp;
                                        <input type="button" value="Disapprove" id="btn_fnm_disapprove" />
                                    </td>
                                </tr>
                            </table>
                            </center>
                            <!-- FNM -->
                        <% } %>

                        <% 
                            if (
                                oUsr.HasPositionOf("csr") != IS_NOT_FOUND &&
                                Model.Status == "1000"
                            )
                            {
                                // TODO : Put RECLASS BUTTON
                            }
                        %>

					</div>

                    <!-- USER SPECIFIC BUTTONS -->
                </td>
            </tr>
        </table>
	</div>
    </div>
</asp:Content>

