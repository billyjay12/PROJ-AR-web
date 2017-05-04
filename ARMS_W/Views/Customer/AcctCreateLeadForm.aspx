<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %><%@ Import Namespace="System.Data" %><%@ Import Namespace="ARMS_W.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        _User oUsr = new _User(Session["username"].ToString());
    %>
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />
	<link href="<%=ResolveUrl("~/") %>Content/LeadForm.css" rel="stylesheet" type="text/css" />

	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	
    <script src="<%=ResolveUrl("~/") %>Scripts/AcctCreateLeadForm.js" type="text/javascript"></script>

	<script type="text/javascript" language="javascript">
        
        var baseUrl = "<%= ResolveUrl("~/") %>";

		$(function () {
            $("#txt_encoded_by").attr('value', '<%: Session["username"] %>');
            $("#txt_date_encoded").attr('value', '<%: System.DateTime.Now.ToString("MM/dd/yyyy") %>');

            $("#txt_exhibit_date").datepicker();
            $("#txt_inquiry_date").datepicker();

            DisableAll();

            $("#web_contact").change(
                function() {
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
			
		});

	</script>
	
    <div class="bl_box">
    <div class="page_header">
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="left">
                    <b>Create New Lead</b>
                </td>
                <td align="right">
                    <%: Session["InputedUname"] %> - <a href="javascript:window.parent.GoHome();">Logout</a>
                </td>
            </tr>
        </table>
    </div>

	<div class="simple_box" style="padding:15px; font-size:12px;">
		<table cellpadding="1" cellspacing="0" border="0" width="100%" >
			<tr>
                <td colspan="4">
                    <table cellpadding="1" cellspacing="0" border="0">
                        <tr>
				            <td>Encoded by</td>
				            <td>
					            <input type="text" id="txt_encoded_by" readonly="readonly" class="required_fields" />
				            </td>
				            <td></td>
				            <td></td>
			            </tr>
			            <tr>
				            <td>Date Encoded</td>
				            <td>
					            <input type="text" id="txt_date_encoded" readonly="readonly" class="required_fields" />
				            </td>
				            <td></td>
				            <td></td>
			            </tr>
                        <tr>
				            <td>
					            Proposed Channel</td>
				            <td>
					            <input type="text" id="txt_proposed_lead_channel" onclick="javascript:LookUpData('txt_proposed_lead_channel', 'ListOfChannel');" readonly="readonly" class="required_fields" />
				            </td>
				            <td></td>
				            <td></td>
			            </tr>
                    </table>
                    <br />
                </td>
            </tr>
			<tr>
				<td colspan="4">
					<div class="blinker" >
					    
                        <table>
                            <tr>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td colspan="2"><b>Inquiring Party</b></td>
                                        </tr>
                                        <tr>
		                                    <td>Name </td>
		                                    <td>
			                                    <input type="text" id="txt_inquiry_name" class="required_fields" />
		                                    </td>
	                                    </tr>
	                                    <tr>
		                                    <td>Date of Inquiry</td>
		                                    <td>
			                                    <input type="text" id="txt_inquiry_date" readonly="readonly" />
		                                    </td>
	                                    </tr>
	                                    <tr>
		                                    <td>Address </td>
		                                    <td>
			                                    <input type="text" id="txt_inquiry_address" class="required_fields" />
		                                    </td>
	                                    </tr>
	                                    <tr>
		                                    <td>Contact Number </td>
		                                    <td>
			                                    <input type="text" id="txt_inquiry_number" class="required_fields" maxlength="20" />
		                                    </td>
	                                    </tr>
	                                    <tr>
		                                    <td>Email Address</td>
		                                    <td>
			                                    <input type="text" id="txt_inquiry_email"  />
		                                    </td>
	                                    </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td colspan="3"><b>Inquiry Source</b></td>
                                        </tr>
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
                        </table>
						
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<br />
					<div class="blinker" >
                        <table cellpadding="5" cellspacing="0" border="0">
                            <tr>
                                <td colspan="2"><b>Brand Inquired:</b></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="4" cellspacing="0" border="0">
						                <tr>
							                <td valign="bottom"><input type="checkbox" id="opt_inqured_mw" /> MW</td>
                                            <td valign="bottom">MatWood</td>
						                </tr>
                                        <tr>
							                <td valign="bottom"><input type="checkbox" id="opt_inqured_ww" /> WW</td>
                                            <td valign="bottom">Weatherwood</td>
						                </tr>
                                        <tr>
							                <td valign="bottom"><input type="checkbox" id="opt_inqured_pw" /> PCW</td>
                                            <td valign="bottom">PCW</td>
						                </tr>
					                </table>
                                </td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="0" border="0">
                                        <tr>
								            <td valign="bottom"><input type="checkbox" id="opt_inqured_tw" /> TW</td>
                                            <td valign="bottom">TrussWood</td>
							            </tr>
                                        <tr>
								            <td valign="bottom"><input type="checkbox" id="opt_inqured_gw" /> GW</td>
                                            <td valign="bottom">GudWood</td>
							            </tr>
                                        <tr>
								            <td valign="bottom"><input type="checkbox" id="opt_inquired_nw" /> NW</td>
                                            <td valign="bottom">NuWood</td>
							            </tr>
						            </table>
                                </td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="0" border="0">
                                        <tr>
								            <td valign="bottom"><input type="checkbox" id="opt_inquired_mu" /> MU</td>
                                            <td valign="bottom">MUZU</td>
							            </tr>
                                        <tr>
								            <td valign="bottom"><input type="checkbox" id="opt_inquired_ec" /> EC</td>
                                            <td valign="bottom">ECOFOR</td>
							            </tr>
						            </table>
                                </td>
                            </tr>
                        </table>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<br />
					<div class="blinker">
						<table cellpadding="7" cellspacing="0" border="0">
                            <tr>
                                <td><b>Type of Item Inquired</b></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="1" cellspacing="0" border="0">
                                        <tr><td><input type="checkbox" id="opt_inqured_framing" /></td><td>Framings & S4S</td></tr>
                                        <tr><td><input type="checkbox" id="opt_inqured_moulding" /></td><td>Mouldings</td></tr>
                                        <tr><td><input type="checkbox" id="opt_inqured_flooring" /></td><td>Floorings</td></tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="1" cellspacing="0" border="0">
                                        <tr><td><input type="checkbox" id="opt_inqured_doorjamb" /></td><td>Doors & Door Jambs</td></tr>
                                        <tr><td><input type="checkbox" id="opt_inqured_panellings" /></td><td>Panellings</td></tr>
                                        <tr><td><input type="checkbox" id="opt_inqured_engitm" /></td><td>Engineered Items</td></tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="1" cellspacing="0" border="0">
                                        <tr><td><input type="checkbox" id="opt_inqured_decking" /></td><td>Decking</td></tr>
                                        <tr><td><input type="checkbox" id="opt_inqured_staircomp" /></td><td>Stair Components</td></tr>
                                        <tr><td><input type="checkbox" id="opt_other_prod" /></td><td>Others: Please specify <input type="text" id="txt_other_prod" /></td></tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<br />
					<div class="blinker">
						<b>Additional Information</b><br />
						<center>
							<textarea id="txt_remarks" style="width:97%; height:100px;"></textarea>
						</center>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="4">&nbsp;</td>
			</tr>
			<tr>
				<td colspan="4">
					<div class="blinker" >
						<center>
							<input type="button" value="Save" onclick="javascript:DocSave();" />
                            <input type="button" value="Cancel" onclick="javascript:Cancel();" />
						</center>
					</div>
				</td>
			</tr>
		</table>
	</div>
    </div>
</asp:Content>

