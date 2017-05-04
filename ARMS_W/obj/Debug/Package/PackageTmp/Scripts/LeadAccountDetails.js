var ns4;

/* should only display two column */
function LookUpData(obj_id_to_store, str_data, channel) {
    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data + "&" + "par1=" + channel,
        success: function (res) {
            HidePreloader();
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
            
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

    // show dialog box/window
}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function IsError(strMsg) {
    if (strMsg.substr(0, 2) != "00:") {
        return "false";
    } else {
        return "true";
    }
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function CreateDialogBox(obj_id_to_position, data_to_add) {

    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"right\" >" +
		"<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<option val_area=\"" + res_cols[2] + "\" val_region=\"" + res_cols[3] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
            }
        }
    }

    w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").attr('value')));

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function GetId(strVal) {
    // return strVal.substring(0, strVal.indexOf('-') - 1);
    return strVal;
}

function GetValue(strVal) {
    // return strVal.substring(strVal.indexOf('-') + 2, 200);
    return strVal;
}

function DisableEditing() {
    $("#txt_exhibit_date").datepicker("disable");

    $("#txt_encoded_by").attr("readonly", "readonly");
    $("#txt_date_encoded").attr("readonly", "readonly");
    $("#txt_proposed_lead_channel").attr('onclick', '');
    $("#txt_web_contact_form_no").attr("readonly", "readonly");
    $("#txt_exhibit_name").attr("readonly", "readonly");
    $("#txt_exhibit_date").attr("readonly", "readonly");
    $("#txt_exhibit_loc").attr("readonly", "readonly");
    $("#txt_refered_by").attr("readonly", "readonly");
    $("#txt_sales_officer").attr("onclick", "");
    $("#txt_other_sources").attr("readonly", "readonly");
    $("#txt_inquiry_name").attr("readonly", "readonly");
    $("#txt_inquiry_date").attr("readonly", "readonly");
    $("#txt_inquiry_address").attr("readonly", "readonly");
    $("#txt_inquiry_number").attr("readonly", "readonly");
    $("#txt_inquiry_email").attr("readonly", "readonly");

    $("#opt_inqured_mw").attr("disabled", "disabled");
    $("#opt_inqured_ww").attr("disabled", "disabled");
    $("#opt_inqured_pw").attr("disabled", "disabled");
    $("#opt_inqured_tw").attr("disabled", "disabled");
    $("#opt_inqured_gw").attr("disabled", "disabled");
    $("#opt_inqured_nw").attr("disabled", "disabled");
    $("#opt_inqured_mu").attr("disabled", "disabled");
    $("#opt_inqured_ec").attr("disabled", "disabled");
    $("#opt_inqured_framing").attr("disabled", "disabled");
    $("#opt_inqured_moulding").attr("disabled", "disabled");
    $("#opt_inqured_flooring").attr("disabled", "disabled");
    $("#opt_inqured_doorjamb").attr("disabled", "disabled");
    $("#opt_inqured_panellings").attr("disabled", "disabled");
    $("#opt_inqured_engitm").attr("disabled", "disabled");
    $("#opt_inqured_decking").attr("disabled", "disabled");
    $("#opt_inqured_staircomp").attr("disabled", "disabled");
    $("#opt_other_prod").attr("disabled", "disabled");

    $("#txt_other_prod").attr("readonly", "readonly");
    $("#txt_remarks").attr("readonly", "readonly");
    $("#txt_proposed_lead_channel").attr("readonly", "readonly");

}

function MarkLeadDocument(action_type, request_id) {
    
    DisplayPreloader();

    if (CkeckRequiredFields(action_type) == false) {
        HidePreloader();
        return;
    }

    var remarks = "";
    remarks = $("#txt_mark_doc_remarks").attr("value");

    if ($("#txt_mark_doc_remarks").attr("value") == undefined) {
        remarks = "";
    }

    var proposed_lead_code = "";
    proposed_lead_code = $("#txt_proposed_lead_code").attr("value");

    var sap_lead_code = "";
    sap_lead_code = $("#txt_sap_proposed_lead_code").attr("value");

    var date_endorsed = "";
    date_endorsed = $("#txt_mark_doc_date").attr("value");

    var forward_to_asm_id = "";
    forward_to_asm_id = $("#txt_to_asm").attr("value_id");

    var forward_to_asm_name = "";
    forward_to_asm_name = $("#txt_to_asm").attr("value");

    var forward_to_so_id = "";
    forward_to_so_id = $("#txt_to_so").attr("value_id");

    var forward_to_so_name = "";
    forward_to_so_name = $("#txt_to_so").attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "SQL/MarkLeadCreationDocument",
        data:
			"action_type=" + action_type + "&" +
            "request_id=" + request_id + "&" +
            "forward_to_asm_id=" + forward_to_asm_id + "&" +
            "forward_to_asm_name=" + forward_to_asm_name + "&" +
            "forward_to_so_id=" + forward_to_so_id + "&" +
            "forward_to_so_name=" + forward_to_so_name + "&" +
            "date_endorsed=" + date_endorsed + "&" +
            "remarks=" + remarks + "&" +
            "proposed_lead_code=" + proposed_lead_code + "&" +
            "sap_lead_code=" + sap_lead_code + 
			""
			,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // open the document?
                window.location = baseUrl + "Document/LeadAccountsDetails?RequestId=" + SrvResultMsg.GetMessage(res);
            } else {
                // error
                alert(SrvResultMsg.GetMessage(res));
                HidePreloader();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function ReClassLeadDoc(val_req_id) {
    window.location = baseUrl + "Customer/AcctCreateLeadFinalForm?RequestId=" + val_req_id;
}

function DisplayPreloader() {
    var w = "";

    w = "" +
        "<div id=\"PreLoaderBackground\" style=\"height:100%; width:100%; left:0; top:0; position:fixed; background:#ffffff; opacity: 0.0; filter:alpha(opacity=0);\"></div>" +
        "<table id=\"PreLoaderContent\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"left:0; top:0; width:100%; height:100%; position:fixed; \" >" +
            "<tr>" +
                "<td valign=\"bottom\" style=\"width:35px;\">" +
                    "<div style=\"border:1px solid #ededed; padding:8px 0px 0px 15px; background:#ffffff; border-top:2px solid #ededed; font-size:34px; font-family:arial; font-weight:bold; \">" +
                        "<img src=\"../Images/5.gif\" style=\"border:0;\" />" +
                        "&nbsp;" +
                        "<img src=\"../Images/loading.gif\" style=\"border:0;\" />" +
                    "</div>" +
                "</td>" +
            "</tr>" +
        "</table>";

    // append
    $("body").after(w);

    $("#PreLoaderContent").show("fast");
    $("#PreLoaderBackground").show();
}

function HidePreloader() {
    $('#PreLoaderContent').hide('fast');
    $('#PreLoaderBackground').hide();
}

function CkeckRequiredFields(val_action_type) {

    var lacking_fields = "";

    if (val_action_type == "ENDORSE") {

        if ($("#txt_mark_doc_date").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Date"; } else { lacking_fields = "Date"; }
        }

        if ($("#txt_to_asm").attr('value') == "" && $("#txt_to_so").attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Assign to ASM/SO"; } else { lacking_fields = "Assign to ASM/SO"; }
        }

    }

    if (val_action_type == "PROPOSE_LEAD_CODE") {
        // txt_proposed_lead_code
        if ($("#txt_proposed_lead_code").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Proposed Lead Code"; } else { lacking_fields = "Proposed Lead Code"; }
        }
    }

    if (val_action_type == "ASSIGN_SAP_LEAD_CODE") {
        // txt_sap_proposed_lead_code
        if ($("#txt_sap_proposed_lead_code").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "SAP Lead Code"; } else { lacking_fields = "SAP Lead Code"; }
        }
    }

    if (val_action_type == "send_back_to_requester") {
        // txt_mark_doc_remarks
        if ($("#txt_mark_doc_remarks").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Remarks"; } else { lacking_fields = "Remarks"; }
        }
    }

    if (val_action_type == "DISAPPROVE") {
        // txt_mark_doc_remarks
        if ($("#txt_mark_doc_remarks").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Remarks"; } else { lacking_fields = "Remarks"; }
        }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;

}

function SwitchTextBoxes(opt_clicked) {
    if (opt_clicked == "asm") {
        $("#txt_to_so").attr('disabled', 'disabled');
        $("#txt_to_so").attr('value', '');
        $("#txt_to_so").attr('value_id', '');
        $("#txt_to_so").attr('class', '');
        $("#txt_to_so").addClass('disabled');

        $("#txt_to_asm").removeAttr('disabled');
        $("#txt_to_asm").attr('class', '');
        $("#txt_to_asm").addClass('enabled');
    } else {
        $("#txt_to_asm").attr('disabled', 'disabled');
        $("#txt_to_asm").attr('value', '');
        $("#txt_to_asm").attr('value_id', '');
        $("#txt_to_asm").attr('class', '');
        $("#txt_to_asm").addClass('disabled');

        $("#txt_to_so").removeAttr('disabled');
        $("#txt_to_so").attr('class', '');
        $("#txt_to_so").addClass('enabled');
    }
}

function SaveLeadDoc(val_request_id) {

    DisplayPreloader();

    var lead_encoded_date = "";
    lead_encoded_date = $("#txt_date_encoded").attr("value");

    var lead_encoded_by = "";
    lead_encoded_by = $("#txt_encoded_by").attr("value");

    var lead_name = "";
    lead_name = $("#txt_inquiry_name").attr("value");

    var lead_inqdate = "";
    lead_inqdate = $("#txt_inquiry_date").attr("value");

    var lead_address = "";
    lead_address = $("#txt_inquiry_address").attr("value");

    var lead_contactno = "";
    lead_contactno = $("#txt_inquiry_number").attr("value");

    var lead_email = "";
    lead_email = $("#txt_inquiry_email").attr("value");

    var is_mw_selected = "";
    if ($("#opt_inqured_mw").attr("checked") == true) {
        is_mw_selected = "true";
    }

    var is_ww_selected = "";
    if ($("#opt_inqured_ww").attr("checked") == true) {
        is_ww_selected = "true";
    }

    var is_pw_selected = "";
    if ($("#opt_inqured_pw").attr("checked") == true) {
        is_pw_selected = "true";
    }

    var is_tw_selected = "";
    if ($("#opt_inqured_tw").attr("checked") == true) {
        is_tw_selected = "true";
    }

    var is_gw_selected = "";
    if ($("#opt_inqured_gw").attr("checked") == true) {
        is_gw_selected = "true";
    }

    var is_framing_selected = "";
    if ($("#opt_inqured_framing").attr("checked") == true) {
        is_framing_selected = "true";
    }

    var is_moulding_selected = "";
    if ($("#opt_inqured_moulding").attr("checked") == true) {
        is_moulding_selected = "true";
    }

    var is_flooring_selected = "";
    if ($("#opt_inqured_flooring").attr("checked") == true) {
        is_flooring_selected = "true";
    }

    var is_doorjambs_selected = "";
    if ($("#opt_inqured_doorjamb").attr("checked") == true) {
        is_doorjambs_selected = "true";
    }

    var is_panellings_selected = "";
    if ($("#opt_inqured_panellings").attr("checked") == true) {
        is_panellings_selected = "true";
    }

    var is_engditm_selected = "";
    if ($("#opt_inqured_engitm").attr("checked") == true) {
        is_engditm_selected = "true";
    }

    var is_decking_selected = "";
    if ($("#opt_inqured_decking").attr("checked") == true) {
        is_decking_selected = "true";
    }

    var is_staircomp_selected = "";
    if ($("#opt_inqured_staircomp").attr("checked") == true) {
        is_staircomp_selected = "true";
    }

    var is_others_selected = "";
    if ($("#opt_other_prod").attr("checked") == true) {
        is_others_selected = "true";
    }

    var lead_proposed_channel = "";
    lead_proposed_channel = $("#txt_proposed_lead_channel").attr("value");

    var remarks = "";
    remarks = $("#txt_remarks").attr("value");

    var inquiry_source_web = $("#txt_web_contact_form_no").attr("value");
    var inquiry_source_exhibit_name = $("#txt_exhibit_name").attr("value");
    var inquiry_source_exhibit_date = $("#txt_exhibit_date").attr("value");
    var inquiry_source_exhibit_address = $("#txt_exhibit_loc").attr("value");
    var inquiry_source_refered_by = $("#txt_refered_by").attr("value");
    var inquiry_source_sales_officer = $("#txt_sales_officer").attr("value");
    var inquiry_source_other_source = $("#txt_other_sources").attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "SQL/UpdateLeadCustomer",
        data: "" +
            "request_id=" + val_request_id + "&" +
            "lead_name=" + lead_name + "&" +
            "lead_inqdate=" + lead_inqdate + "&" +
            "lead_address=" + lead_address + "&" +
            "lead_contactno=" + lead_contactno + "&" +
            "lead_email=" + lead_email + "&" +
            "is_mw_selected=" + is_mw_selected + "&" +
            "is_ww_selected=" + is_ww_selected + "&" +
            "is_pw_selected=" + is_pw_selected + "&" +
            "is_tw_selected=" + is_tw_selected + "&" +
            "is_gw_selected=" + is_gw_selected + "&" +
            "is_framing_selected=" + is_framing_selected + "&" +
            "is_moulding_selected=" + is_moulding_selected + "&" +
            "is_flooring_selected=" + is_flooring_selected + "&" +
            "is_doorjambs_selected=" + is_doorjambs_selected + "&" +
            "is_panellings_selected=" + is_panellings_selected + "&" +
            "is_engditm_selected=" + is_engditm_selected + "&" +
            "is_decking_selected=" + is_decking_selected + "&" +
            "is_staircomp_selected=" + is_staircomp_selected + "&" +
            "is_others_selected=" + is_others_selected + "&" +
            "lead_encoded_date=" + lead_encoded_date + "&" +
            "lead_encoded_by=" + lead_encoded_by + "&" +
            "remarks=" + remarks + "&" +
            "lead_proposed_channel=" + lead_proposed_channel + "&" +
            "inquiry_source_web=" + inquiry_source_web + "&" +
            "inquiry_source_exhibit_name=" + inquiry_source_exhibit_name + "&" +
            "inquiry_source_exhibit_date=" + inquiry_source_exhibit_date + "&" +
            "inquiry_source_exhibit_address=" + inquiry_source_exhibit_address + "&" +
            "inquiry_source_refered_by=" + inquiry_source_refered_by + "&" +
            "inquiry_source_sales_officer=" + inquiry_source_sales_officer + "&" +
            "inquiry_source_other_source=" + inquiry_source_other_source +
            ""
        ,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                // refresh
                alert("SUCCESSFULLY SAVED!");

                // open the document?
                // REFRESH PAGE
                location.reload();
            } else {
                // 
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

}

function SwitchEnabledFields() {
    var curr_selected_radio_button = "";

    DisableAll();

    if ($("#web_contact").attr('checked') == 'checked') {
        // web contact
        // enable
        $("#txt_web_contact_form_no").removeAttr('disabled');
        $("#txt_web_contact_form_no").attr('class', 'required_fields');

    }

    if ($("#exhibit_name").attr('checked') == 'checked') {

        // exhibit
        // enable
        $("#txt_exhibit_name").removeAttr('disabled');
        $("#txt_exhibit_name").attr('class', 'required_fields');

        // $("#txt_exhibit_date").removeAttr('disabled');
        $("#txt_exhibit_date").attr('class', 'required_fields');
        $("#txt_dummy_date").hide();
        $("#txt_exhibit_date").show();

        $("#txt_exhibit_loc").removeAttr('disabled');
        $("#txt_exhibit_loc").attr('class', 'required_fields');
    }

    if ($("#referred_by").attr('checked') == 'checked') {

        // refered
        // enable
        $("#txt_refered_by").removeAttr('disabled');
        $("#txt_refered_by").attr('class', 'required_fields');

    }

    if ($("#coverage").attr('checked') == 'checked') {
        // coverage
        // enable
        $("#txt_sales_officer").attr('class', 'required_fields');
    }

    if ($("#other_sources").attr('checked') == 'checked') {
        // other
        // enable
        $("#txt_other_sources").removeAttr('disabled');
        $("#txt_other_sources").attr('class', 'required_fields');
    }
}

/* disable and clear data */
function DisableAll() {
    $("#txt_web_contact_form_no").attr('disabled', 'disabled');
    $("#txt_web_contact_form_no").attr('class', 'readonly_fields');
    $("#txt_web_contact_form_no").attr('value', '');
    // exhibit
    $("#txt_exhibit_name").attr('disabled', 'disabled');
    $("#txt_exhibit_name").attr('class', 'readonly_fields');
    $("#txt_exhibit_name").attr('value', '');

    $("#txt_exhibit_date").hide();
    $("#txt_dummy_date").show();
    $("#txt_dummy_date").attr('class', 'readonly_fields');
    $("#txt_exhibit_date").attr('class', 'readonly_fields');
    $("#txt_exhibit_date").attr('value', '');

    $("#txt_exhibit_loc").attr('disabled', 'disabled');
    $("#txt_exhibit_loc").attr('class', 'readonly_fields');
    $("#txt_exhibit_loc").attr('value', '');
    // refered
    $("#txt_refered_by").attr('disabled', 'disabled');
    $("#txt_refered_by").attr('class', 'readonly_fields');
    $("#txt_refered_by").attr('value', '');
    // coverage
    $("#txt_sales_officer").attr('class', 'readonly_fields');
    $("#txt_sales_officer").attr('value', '');
    // other
    $("#txt_other_sources").attr('disabled', 'disabled');
    $("#txt_other_sources").attr('class', 'readonly_fields');
    $("#txt_other_sources").attr('value', '');


}

function disabled_radio_buttons() {
    /* radio buttons */
    $("#web_contact").attr('disabled', 'disabled');
    $("#exhibit_name").attr('disabled', 'disabled');
    $("#referred_by").attr('disabled', 'disabled');
    $("#coverage").attr('disabled', 'disabled');
    $("#other_sources").attr('disabled', 'disabled');
}

function CheckAcctcode() {
    // account code
    var final_acct_code = "";
    final_acct_code = $("#txt_proposed_lead_code").attr('value');

    if (final_acct_code == "") { return; }

    $.ajax({
        type: "POST", url: baseUrl + "Customer/CheckAcctcodeifFoundnSAP",
        data:
			"final_acct_code=" + final_acct_code,
        success: function (res) {
            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert(SrvResultMsg.GetMessage(res));
            } else {
                // error
                alert(SrvResultMsg.GetMessage(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function FnmDisapproveLead(request_id) {
    $.ajax({
        type: "POST", url: baseUrl + "SQL/FnmDisapproveLead",
        data: "request_id=" + request_id,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                alert("SUCCESSFULLY SAVED!");
                location.reload();
            } else {
                // 
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });
}