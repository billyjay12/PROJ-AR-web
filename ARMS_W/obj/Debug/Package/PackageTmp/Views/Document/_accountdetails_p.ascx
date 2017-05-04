<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %><%@ Import Namespace="ARMS_W.Class" %>

<center tag="approvers_corner" >
    
    <% 
        int IS_NOT_FOUND = -1;
        
        if (
            (
                Model.oUsr.HasPositionOf("csr") != IS_NOT_FOUND
                // ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
                || Model.oUsr.HasPositionOf("csr") == IS_NOT_FOUND 
            ) 
                &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("csr") &&
            (
                Model.oUsr.HasRegionOf("csr", Model.oDocumnt.Region) == true
                // ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
                || Model.oUsr.HasRegionOf("csm", Model.oDocumnt.Region) == true
            )
            ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
            <tr>
                <td><input type="button" value="Save Changes" id="btn_save" /></td>
                <td>
                    <% 
                    if (Model.curr_doc_DocStatus != "1000" && Model.curr_doc_DocChangesStatus == "1")
                    {
                      %>
                        <input type="button" value="Close Request" onclick="javascript:MarkCustomerCreationDocument('DISAPPROVE', '<%: Model.DocCCaNum %>');" />
                      <%  
                    }
                    %>
                </td>
            </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("asm") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("asm") &&
            Model.oUsr.HasAreaOf("asm", Model.oDocumnt.Area) == true
           ) { 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2">&nbsp;</td>
				        </tr>
				        <tr>
					        <td colspan="2">
						        <input type="button" value="Approve" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" />
					        </td>
				        </tr>
			        </table>
			        </div>
                    <!-- -->
		        </td>
	        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("chm") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("chm") &&
            Model.oUsr.HasChannelOf("chm", Model.oDocumnt.Channel) == true
           ) { 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2">&nbsp;</td>
				        </tr>
				        <tr>
					        <td colspan="2">
						        <input type="button" value="Approve" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" />
					        </td>
				        </tr>
			        </table>
			        </div>
                    <!-- -->
		        </td>
	        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("vpbsm") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("vpbsm") &&
            Model.oUsr.HasChannelOf("vpbsm", Model.oDocumnt.Channel) == true
            ) { 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2">&nbsp;</td>
				        </tr>
				        <tr>
					        <td colspan="2">
						        <input type="button" value="Approve" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" />
					        </td>
				        </tr>
			        </table>
			        </div>
                    <!-- -->
		        </td>
	        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("fnm") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("fnm") &&
            Model.oUsr.HasRegionOf("fnm", Model.oDocumnt.Region) == true
            ) { 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2">&nbsp;</td>
				        </tr>
                        
				        <tr>
					        <td colspan="2">
						        <input type="button" value="Approve" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" /> &nbsp; / &nbsp;
                                <input type="button" value="Send Back to C & C" bname="cust_create_returncnc" />
					        </td>
				        </tr>
			        </table>
			        </div>
                    <!-- -->
		        </td>
	        </tr>
        </table>

    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("cnc") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("cnc") &&
            Model.oUsr.HasRegionOf("cnc", Model.oDocumnt.Region) == true
            ) { 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2">&nbsp;</td>
				        </tr>
				        <tr>
					        <td colspan="2"> 
						        <input type="button" value="Approve" pos_id="7" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" />
					        </td>
				        </tr>
			        </table>
			        </div>
                    <!-- -->
		        </td>
	        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("vptfi") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == AppHelper.GetUserPositionId("vptfi")
            ) { 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2">&nbsp;</td>
				        </tr>
				        <tr>
					        <td colspan="2">
						        <input type="button" value="Approve" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" /> &nbsp; / &nbsp;
                                <input type="button" value="Send Back to C & C" bname="cust_create_returncnc" />
					        </td>
				        </tr>
			        </table>
			        </div>
                    <!-- -->
		        </td>
	        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.oUsr.HasPositionOf("fnm") != IS_NOT_FOUND &&
            Model.curr_doc_DocStatus == "1008" &&
            Model.oUsr.HasRegionOf("fnm", Model.oDocumnt.Region) == true
            ){ 
    %>
        <table cellspacing="0" cellpadding="2" border="0">
	        <tr>
		        <td>
			        <!--  -->
			        <div class="div_blinker">
			        <table cellspacing="0" cellpadding="2" border="0">
				        <tr>
					        <td align="right">Final Account Code</td>
					        <td align="left"><input type="text" id="txt_final_account_code" maxlength="15" class="required_fields" /><a href="javascript:CheckAcctcode();"><img src="<%=ResolveUrl("~/") %>Images/check_icon.png" style="border:0;" /></a></td>
				        </tr>
				        <tr>
					        <td colspan="2" align="left">
                                Remarks: 
                            </td>
				        </tr>
                        <tr>
					        <td colspan="2" align="left">
                                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
                            </td>
				        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
				        <tr>
					        <td colspan="2">
						        <input type="button" value="Approve" for_cust_code_create="YES" bname="cust_create_approve" /> &nbsp; / &nbsp;
						        <input type="button" value="Disapprove" bname="cust_create_disapprove" /> &nbsp; / &nbsp;
						        <input type="button" value="Send Back to Requester" bname="cust_create_returncsr" />
					        </td>
				        </tr>
			        </table>
			        </div>
		        </td>
	        </tr>
        </table>
    <% 
        } 
    %>

    <!-- 2ND PART -->

    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("csr") &&
            (
                Model.oUsr.HasPositionOf("csr") != IS_NOT_FOUND
                // ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
                || Model.oUsr.HasPositionOf("csm") != IS_NOT_FOUND
                // ADDED TO ENABLE SIM ROLE, TO UPDATE PERSONAL INFO TAB ONLY
                || Model.oUsr.HasPositionOf("sim") != IS_NOT_FOUND
            )
            &&
            (
                Model.oUsr.HasRegionOf("csr", Model.oDocumnt.Region) == true
                // ADDED TO ENABLE CS MANAGER, TO CREATE AND UPDATE CUSTOMER/CUSTOMER INFO
                || Model.oUsr.HasRegionOf("csm", Model.oDocumnt.Region) == true
                // ADDED TO ENABLE SIM ROLE, TO UPDATE PERSONAL INFO TAB ONLY
                || Model.oUsr.HasRegionOf("sim", Model.oDocumnt.Region) == true
            )
            ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
			<td colspan="2" align="left">
                Remarks: 
            </td>
		</tr>
        <tr>
			<td colspan="2" align="left">
                <input type="text" obj="txt_doc_remarks" style="width:100%;" />
            </td>
		</tr>
        <tr>
			<td colspan="2">&nbsp;</td>
		</tr>
        <tr>
            <td><input type="button" value="Update Changes" onclick="javascript:Save_Doc();" /></td>
            <td>
                <!--
                <input type="button" value="Cancel" />
                -->
            </td>
        </tr>
        </table>
    <% 
        } 
    %>
    
    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("asm") &&
            Model.oUsr.HasPositionOf("asm") != IS_NOT_FOUND &&
            Model.oUsr.HasAreaOf("asm", Model.oDocumnt.Area) == true
            ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        
        <tr>
            <td>Remarks</td>
            <td>
                <input type="text" obj="txt_doc_remarks" class="required_fields" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <input type="button" value="Send Back to Requestor" bname="cust_change_returncsr" />
            </td>
        </tr>
        
        <tr>
            <td><input type="button" value="Approve Changes" bname="cust_change_approve" /></td>
            <td><input type="button" value="Disapprove Changes" bname="cust_change_disapprove" /></td>
        </tr>
        
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("chm") &&
            Model.oUsr.HasPositionOf("chm") != IS_NOT_FOUND &&
            Model.oUsr.HasChannelOf("chm", Model.oDocumnt.Channel) == true
            ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td>Remarks</td>
            <td>
                <input type="text" obj="txt_doc_remarks" class="required_fields" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="3" align="left">
                
            </td>
        </tr>
        <tr>
            <td><input type="button" value="Approve Changes" bname="cust_change_approve" /></td>
            <td><input type="button" value="Disapprove Changes" bname="cust_change_disapprove" />&nbsp;<input type="button" value="Send Back to Requestor" bname="cust_change_returncsr" /></td>
        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("vpbsm") &&
            Model.oUsr.HasPositionOf("vpbsm") != IS_NOT_FOUND &&
            Model.oUsr.HasChannelOf("vpbsm", Model.oDocumnt.Channel) == true
            ){ 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td align="left">Remarks</td>
        </tr>
        <tr>
            <td>
                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" value="Approve Changes" bname="cust_change_approve" /> &nbsp;&nbsp;/&nbsp;&nbsp;
                <input type="button" value="Disapprove Changes" bname="cust_change_disapprove" /> &nbsp;&nbsp;/&nbsp;&nbsp;
                <input type="button" value="Send Back to Requestor" bname="cust_change_returncsr" />
            </td>
        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("cnc") &&
            Model.oUsr.HasPositionOf("cnc") != IS_NOT_FOUND &&
            Model.oUsr.HasRegionOf("cnc", Model.oDocumnt.Region) == true
            ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td align="left">
                Remarks
            </td>
        </tr>
        <tr>
            <td align="center">
                <input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" value="Approve Changes" pos_id="7" bname="cust_change_approve" /> &nbsp;&nbsp; / &nbsp;&nbsp;
                <input type="button" value="Disapprove Changes" bname="cust_change_disapprove" /> &nbsp;&nbsp; / &nbsp;&nbsp;
                <input type="button" value="Send Back to Requestor" bname="cust_change_returncsr" />
            </td>
        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("fnm") &&
            Model.oUsr.HasPositionOf("fnm") != IS_NOT_FOUND &&
            Model.oUsr.HasRegionOf("fnm", Model.oDocumnt.Region) == true
        ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td align="left">Remarks</td>
        </tr>
        <tr>
			<td align="left" >
				<input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
			</td>
		</tr>
        <tr>
            <td >
                <input type="button" value="Approve Changes" bname="cust_change_approve" /> &nbsp; / &nbsp;
                <input type="button" value="Disapprove Changes" bname="cust_change_disapprove" /> &nbsp; / &nbsp;
                <input type="button" value="Send Back to Requestor" bname="cust_change_returncsr" /> &nbsp; / &nbsp;
                <input type="button" value="Send Back to C & C" bname="cust_change_returncnc" />
            </td>
        </tr>
        </table>
    <% 
        } 
    %>

    <% 
        if (
            Model.curr_doc_DocStatus == "1000" &&
            Model.curr_doc_DocChangesStatus == AppHelper.GetUserPositionId("vptfi") &&
            Model.oUsr.HasPositionOf("vptfi") != IS_NOT_FOUND
        ) { 
    %>
        <table cellpadding="2" cellspacing="0" border="0">
        <tr>
            <td align="left">Remarks</td>
        </tr>
        <tr>
			<td align="left" >
				<input type="text" obj="txt_doc_remarks" class="required_fields" style="width:100%;" />
			</td>
		</tr>
        <tr>
            <td>
                <input type="button" value="Approve Changes" bname="cust_change_approve" /> &nbsp; / &nbsp;
                <input type="button" value="Disapprove Changes" bname="cust_change_disapprove" /> &nbsp; / &nbsp;
                <input type="button" value="Send Back to Requestor" bname="cust_change_returncsr" /> &nbsp; / &nbsp;
                <input type="button" value="Send Back to C & C" bname="cust_change_returncnc" />
            </td>
        </tr>
        </table>
    <% 
        } 
    %>

</center>