<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=ResolveUrl("~/") %>Content/NewLeadDb.css" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/") %>Css/Themes/base/jquery.ui.selectedcss.css" rel="stylesheet" type="text/css" />

    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/") %>Scripts/NewLeadDb.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
        var baseUrl = "<%= ResolveUrl("~/") %>";

        $(function () {
            
            $("#txt_nego_date").datepicker();
            $("#txt_qt_submit_date").datepicker();
            $("#txt_followup_date").datepicker();
            $("#txt_ls_date").datepicker();
            $("#txt_closed_date").datepicker();

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

            $("#rdo_lost_sales").click(
                function (){
                    
                    if ( $("#rdo_lost_sales").attr('checked') == 'checked' ) {
                        enable_rdo_ls_group();
                        disable_rdo_close_group();
                        lost_sales_required_fields();
                    }
                }
            );

            $("#rdo_Closed").click(
                function (){
                    if ( $("#rdo_Closed").attr('checked') == 'checked' ) {
                        disable_rdo_ls_group();
                        enable_rdo_close_group();
                        closed_amt_required_fields();
                    }
                }
            );

            disable_group1();
            disable_group2();
            disable_group3();

            enable_group1();

            disable_rdo_ls_group();
            disable_rdo_close_group();
        });

    </script>
    
    <div class="bl_box">
    <div class="page_header">

        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <b>New Active Leads</b>
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
                            <td><input type="text" id="txt_lead_req_id" onclick="javascript:LookUpData('txt_lead_req_id', '');" class="required_fields" readonly="readonly" /></td>
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
                                    <td><input id="txt_nego_date" type="text" class="required_fields" readonly="readonly" /></td>
                                </tr>
                                <tr>
                                    <td>Contact Person:</td>
                                    <td><input id="txt_nego_contact_person" type="text" class="required_fields" /></td>
                                </tr>
                                <tr>
                                    <td>Contact Number:</td>
                                    <td><input id="txt_nego_contact_number" type="text" class="required_fields" /></td>
                                </tr>
                            </table>
                        </div>

                        <div style="padding:0px 0px 0px 15px; margin:2px;">
                            <label><input id="is_qt_submitted" type="checkbox" disabled="disabled" /> Quote Submitted </label> <br />
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
                            <label><input id="is_for_followup" type="checkbox" disabled="disabled" /> for follow up </label> <br />
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
                <td>
                    <hr />
                    Encoded By : <input type="text" id="txt_encodedby" value="<%: Session["username"] %>" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                    <input type="button" value="Update" onclick="javascript:AddNewLeadDB();" />
                </td>
            </tr>
		</table>

	</div>
    </div>
</asp:Content>
