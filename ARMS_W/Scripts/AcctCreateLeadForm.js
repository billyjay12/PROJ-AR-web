var ns4;
function clear_values_tab() {
    $("#txt_web_contact_form_no").attr('value', '');
    $("#txt_exhibit_name").attr('value', '');
    $("#txt_exhibit_date").attr('value', '');
    $("#txt_exhibit_loc").attr('value', '');
    $("#txt_refered_by").attr('value', '');
    $("#txt_sales_officer").attr('value', '');
    $("#txt_other_sources").attr('value', '');
}

/* should only display two column */
function LookUpData(obj_id_to_store, str_data, par1) {
    // if obj is disabled
    if (obj_id_to_store == "txt_sales_officer") {
        if ($("#coverage").attr('checked') == false) {
            return;
        }
    }

    if (par1 == undefined) {
        par1 = "";
    }

    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "Customer/GetFilteredList2",
        data: "_str_data=" + str_data + "&par1=" + par1,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
            HidePreloader();
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
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function GetId(strVal) {
    //return strVal.substring(0, strVal.indexOf('-') - 1);
    return strVal;
}

function GetValue(strVal) {
    //return strVal.substring(strVal.indexOf('-') + 2, 200);
    return strVal;
}

function SaveLeadDoc() {

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
    if ($("#opt_inqured_mw").attr("checked") == "checked") {
        is_mw_selected = "true";
    }

    var is_ww_selected = "";
    if ($("#opt_inqured_ww").attr("checked") == "checked") {
        is_ww_selected = "true";
    }

    var is_pw_selected = "";
    if ($("#opt_inqured_pw").attr("checked") == "checked") {
        is_pw_selected = "true";
    }

    var is_tw_selected = "";
    if ($("#opt_inqured_tw").attr("checked") == "checked") {
        is_tw_selected = "true";
    }

    var is_gw_selected = "";
    if ($("#opt_inqured_gw").attr("checked") == "checked") {
        is_gw_selected = "true";
    }

    var is_nw_selected = "";
    if ($("#opt_inquired_nw").attr("checked") == "checked") {
        is_nw_selected = "true";
    }

    var is_mu_selected = "";
    if ($("#opt_inquired_mu").attr("checked") == "checked") {
        is_mu_selected = "true";
    }

    var is_ec_selected = "";
    if ($("#opt_inquired_ec").attr("checked") == "checked") {
        is_ec_selected = "true";
    }

   // var is_ecu_selected = "";
    //if ($("#opt_inquired_ecu").attr("checked") == "checked") {
    //    is_ecu_selected = "true";
   // }

    var is_framing_selected = "";
    if ($("#opt_inqured_framing").attr("checked") == "checked") {
        is_framing_selected = "true";
    }

    var is_moulding_selected = "";
    if ($("#opt_inqured_moulding").attr("checked") == "checked") {
        is_moulding_selected = "true";
    }

    var is_flooring_selected = "";
    if ($("#opt_inqured_flooring").attr("checked") == "checked") {
        is_flooring_selected = "true";
    }

    var is_doorjambs_selected = "";
    if ($("#opt_inqured_doorjamb").attr("checked") == "checked") {
        is_doorjambs_selected = "true";
    }

    var is_panellings_selected = "";
    if ($("#opt_inqured_panellings").attr("checked") == "checked") {
        is_panellings_selected = "true";
    }

    var is_engditm_selected = "";
    if ($("#opt_inqured_engitm").attr("checked") == "checked") {
        is_engditm_selected = "true";
    }

    var is_decking_selected = "";
    if ($("#opt_inqured_decking").attr("checked") == "checked") {
        is_decking_selected = "true";
    }

    var is_staircomp_selected = "";
    if ($("#opt_inqured_staircomp").attr("checked") == "checked") {
        is_staircomp_selected = "true";
    }

    var is_others_selected = "";
    if ($("#opt_other_prod").attr("checked") == "checked") {
        is_others_selected = "true";
    }

    var opt_other_desc = "";
    opt_other_desc = $("#txt_other_prod").attr("value");

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

    opt_other_desc = EncodeAmpersand(opt_other_desc);
    remarks = EncodeAmpersand(remarks);

    lead_name = EncodeAmpersand(lead_name);
    lead_address = EncodeAmpersand(lead_address);

    $.ajax({
        type: "POST", url: baseUrl + "SQL/AddLeadCustomer",
        data: "" +
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
            "is_nw_selected=" + is_nw_selected + "&" +
            "is_mu_selected=" + is_mu_selected + "&" +
            "is_ec_selected=" + is_ec_selected + "&" +
           // "is_ecu_selected=" + is_ecu_selected + "&" +
            "is_framing_selected=" + is_framing_selected + "&" +
            "is_moulding_selected=" + is_moulding_selected + "&" +
            "is_flooring_selected=" + is_flooring_selected + "&" +
            "is_doorjambs_selected=" + is_doorjambs_selected + "&" +
            "is_panellings_selected=" + is_panellings_selected + "&" +
            "is_engditm_selected=" + is_engditm_selected + "&" +
            "is_decking_selected=" + is_decking_selected + "&" +
            "is_staircomp_selected=" + is_staircomp_selected + "&" +
            "is_others_selected=" + is_others_selected + "&" +
            "opt_other_desc=" + opt_other_desc + "&" +
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
                window.location = baseUrl + "Document/LeadAccountsDetails?RequestId=" + SrvResultMsg.GetMessage(res);
            } else {
                // 
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

}

function DocSave() { 
    if ( CheckRequiredFields() == true ){
        SaveLeadDoc();
    }
}

function CheckRequiredFields() {
    var inqsource = "";
   

    if ($("#web_contact").attr('checked') == 'checked') {
        if ($("#txt_web_contact_form_no").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Web Contact Form Number]!");
            return false;
        }
    }

    if ($("#exhibit_name").attr('checked') == 'checked') {
        if ($("#txt_exhibit_name").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Exhibit Name]!");
            return false;
        }

        if ($("#txt_exhibit_date").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Exhibit Date]!");
            return false;
        }

        if ($("#txt_exhibit_loc").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Exhibit Location]!");
            return false;
        }
    }

    if ($("#referred_by").attr('checked') == 'checked') {
        if ($("#txt_refered_by").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Reffered By]!");
            return false;
        }
    }

    if ($("#coverage").attr('checked') == 'checked') {
        if ($("#txt_sales_officer").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Coverage]!");
            return false;
        }
    }

    if ($("#other_sources").attr('checked') == 'checked') {
        if ($("#txt_other_sources").attr('value') == "") {
            alert("PLEASE FILL IN THE REQUIRED FIELDS [Other Sources]!");
            return false;
        }
    }

    //
    if ($("#txt_inquiry_address").attr("value") == "") {
        alert("PLEASE FILL IN THE REQUIRED FIELDS [Inquiry Address]!");
        return false;
    }

    if ($("#txt_inquiry_number").attr("value") == "") {
        alert("PLEASE FILL IN THE REQUIRED FIELDS [Inquiry Number]!");
        return false;
    }

    if ($("#txt_inquiry_name").attr("value") == "") {
        alert("PLEASE FILL IN THE REQUIRED FIELDS [Inquiry Name]!");
        return false;
    }

    if ($("#txt_proposed_lead_channel").attr("value") == "") {
        alert("PLEASE FILL IN THE REQUIRED FIELDS[Proposed Lead Channel]!");
        return false;
    }

    return true;

}

function Cancel() {
    var isCancelled = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (isCancelled) {
        window.history.back()
    }
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