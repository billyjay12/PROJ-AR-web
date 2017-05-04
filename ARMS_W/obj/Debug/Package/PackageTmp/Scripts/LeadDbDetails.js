function disable_group1() {
    $("#txt_nego_contact_person").attr('readonly', 'readonly');
    $("#txt_nego_contact_number").attr('readonly', 'readonly');
    $("#txt_nego_date").datepicker("disable");
}

function disable_group2() {
    $("#txt_qt_submit_date").datepicker("disable");
    $("#txt_qoute_received_by").attr('readonly', 'readonly');
}

function disable_group3() {
    $("#txt_followup_date").datepicker("disable");
}

function enable_group1() {
    $("#is_contacted").removeAttr('disabled');
    $("#txt_nego_date").datepicker("enable");
    $("#txt_nego_contact_person").removeAttr('readonly');
    $("#txt_nego_contact_number").removeAttr('readonly');
}

function enable_group2() {
    $("#txt_qt_submit_date").datepicker("enable");
    $("#txt_qoute_received_by").removeAttr('readonly');
}

function enable_group3() {
    $("#txt_followup_date").datepicker("enable");
}

function enable_rdo_ls_group() {
    $("#txt_ls_date").datepicker("enable");
    $("#chk_reason1").removeAttr('disabled'); $("#chk_reason1").removeAttr('readonly');
    $("#chk_reason2").removeAttr('disabled'); $("#chk_reason2").removeAttr('readonly');
    $("#chk_reason3").removeAttr('disabled'); $("#chk_reason3").removeAttr('readonly');
    $("#chk_reason4").removeAttr('disabled'); $("#chk_reason4").removeAttr('readonly');
    $("#chk_reason5").removeAttr('disabled'); $("#chk_reason5").removeAttr('readonly');
    $("#chk_reason6").removeAttr('disabled'); $("#chk_reason6").removeAttr('readonly');
    $("#chk_reason6").parent().parent().find("input[type=text]").removeAttr('disabled');
    $("#chk_reason6").parent().parent().find("input[type=text]").removeAttr('readonly');
}

function disable_rdo_ls_group() {
    $("#txt_ls_date").datepicker("disable");
    $("#chk_reason1").attr('disabled', 'disabled'); $("#chk_reason1").attr('readonly', 'readonly');
    $("#chk_reason2").attr('disabled', 'disabled'); $("#chk_reason2").attr('readonly', 'readonly');
    $("#chk_reason3").attr('disabled', 'disabled'); $("#chk_reason3").attr('readonly', 'readonly');
    $("#chk_reason4").attr('disabled', 'disabled'); $("#chk_reason4").attr('readonly', 'readonly');
    $("#chk_reason5").attr('disabled', 'disabled'); $("#chk_reason5").attr('readonly', 'readonly');
    $("#chk_reason6").attr('disabled', 'disabled'); $("#chk_reason6").attr('readonly', 'readonly');
    $("#chk_reason6").parent().parent().find("input[type=text]").attr('disabled', 'disabled');
    $("#chk_reason6").parent().parent().find("input[type=text]").attr('readonly', 'readonly');
}

function enable_rdo_close_group() {
    $("#txt_closed_date").datepicker("enable");
    $("#txt_closed_amount").removeAttr('readonly');
}

function disable_rdo_close_group() {
    $("#txt_closed_date").datepicker("disable");
    $("#txt_closed_amount").attr('readonly', 'readonly');
}

function disable_all() {
    disable_group1();
    disable_group2();
    disable_group3();
    disable_rdo_ls_group();
    disable_rdo_close_group();
}

var ns4;

function SaveDoc() {
    DisplayPreloader();

    if (CheckRequiredfields() == false) {
        HidePreloader();
        return;
    }

    var m_request_id = "";
    m_request_id = $("#txt_lead_req_id").attr('value_id');

    // nego - group 2
    var m_is_qt_submitted = "";
    if ($("#is_qt_submitted").attr('checked') == 'checked') { m_is_qt_submitted = "1"; } else { m_is_qt_submitted = "0"; }

    var m_txt_qt_submit_date = "";
    m_txt_qt_submit_date = $("#txt_qt_submit_date").attr('value');

    var m_txt_qoute_received_by = "";
    m_txt_qoute_received_by = $("#txt_qoute_received_by").attr('value');

    // nego - group 3
    var m_is_for_followup = "";
    if ($("#is_for_followup").attr('checked') == 'checked') { m_is_for_followup = "1"; } else { m_is_for_followup = "0"; }

    var m_txt_followup_date = "";
    m_txt_followup_date = $("#txt_followup_date").attr('value');

    // lost sales
    var m_rdo_lost_sales = "";
    if ($("#rdo_lost_sales").attr('checked') == 'checked') { m_rdo_lost_sales = "1"; } else { m_rdo_lost_sales = "0"; }

    var m_txt_ls_date = "";
    m_txt_ls_date = $("#txt_ls_date").attr('value');

    var m_chk_reason1 = "";
    if ($("#chk_reason1").attr('checked') == 'checked') { m_chk_reason1 = "1"; } else { m_chk_reason1 = "0"; }

    var m_chk_reason2 = "";
    if ($("#chk_reason3").attr('checked') == 'checked') { m_chk_reason2 = "1"; } else { m_chk_reason2 = "0"; }

    var m_chk_reason3 = "";
    if ($("#chk_reason3").attr('checked') == 'checked') { m_chk_reason3 = "1"; } else { m_chk_reason3 = "0"; }

    var m_chk_reason4 = "";
    if ($("#chk_reason4").attr('checked') == 'checked') { m_chk_reason4 = "1"; } else { m_chk_reason4 = "0"; }

    var m_chk_reason5 = "";
    if ($("#chk_reason5").attr('checked') == 'checked') { m_chk_reason5 = "1"; } else { m_chk_reason5 = "0"; }

    var m_chk_reason6 = "";
    if ($("#chk_reason6").attr('checked') == 'checked') { m_chk_reason6 = "1"; } else { m_chk_reason6 = "0"; }

    var m_chk_reason6_desc = "";
    m_chk_reason6_desc = $("#txt_reason6_desc").attr('value');

    // closed
    var m_rdo_Closed = "";
    if ($("#rdo_Closed").attr('checked') == 'checked') { m_rdo_Closed = "1"; } else { m_rdo_Closed = "0"; }

    var m_txt_closed_amount = "";
    m_txt_closed_amount = $("#txt_closed_amount").attr('value');

    var m_txt_closed_date = "";
    m_txt_closed_date = $("#txt_closed_date").attr('value');



    $.ajax({
        type: "POST", url: baseUrl + "LeadDb/SaveLeadDb",
        data:
            "m_request_id=" + m_request_id + "&" +
            "m_is_qt_submitted=" + m_is_qt_submitted + "&" +
            "m_txt_qt_submit_date=" + m_txt_qt_submit_date + "&" +
            "m_txt_qoute_received_by=" + m_txt_qoute_received_by + "&" +
            "m_is_for_followup=" + m_is_for_followup + "&" +
            "m_txt_followup_date=" + m_txt_followup_date + "&" +
            "m_rdo_lost_sales=" + m_rdo_lost_sales + "&" +
            "m_txt_ls_date=" + m_txt_ls_date + "&" +
            "m_chk_reason1=" + m_chk_reason1 + "&" +
            "m_chk_reason2=" + m_chk_reason2 + "&" +
            "m_chk_reason3=" + m_chk_reason3 + "&" +
            "m_chk_reason4=" + m_chk_reason4 + "&" +
            "m_chk_reason5=" + m_chk_reason5 + "&" +
            "m_chk_reason6=" + m_chk_reason6 + "&" +
            "m_chk_reason6_desc=" + m_chk_reason6_desc + "&" +
            "m_rdo_Closed=" + m_rdo_Closed + "&" +
            "m_txt_closed_amount=" + m_txt_closed_amount + "&" +
            "m_txt_closed_date=" + m_txt_closed_date ,
        success: function (res) {

            if (res.substring(0, 3) == "00:") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // open the document?
                window.location = baseUrl + "LeadDb/LeadDbDetails?requestid=" + res.substring(3, 200);

            } else {
                // error
                alert(res.substring(3, 200));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });

}

function LeadDbConfirm() {

    DisplayPreloader();

    if (CheckConfRequiredFields() == false) { HidePreloader(); return; }

    var m_request_id = "";
    m_request_id = $("#txt_lead_req_id").attr('value_id');

    var m_txt_po_number = "";
    m_txt_po_number = $("#txt_po_number").attr('value');
    
    var m_txt_so_number = "";
    m_txt_so_number = $("#txt_so_number").attr('value');

    var m_txt_est_delivery_date = "";
    m_txt_est_delivery_date = $("#txt_est_delivery_date").attr('value');

    var m_txt_attachment = "";
    m_txt_attachment = $("#txt_attachment").attr('value');

    var m_txt_confirmed_by = "";
    m_txt_confirmed_by = $("#txt_confirmed_by").attr('value');

    var m_txt_date_confirmed = "";
    m_txt_date_confirmed = $("#txt_date_confirmed").attr('value');
    
    $.ajax({
        type: "POST", url: baseUrl + "LeadDb/ConfirmLeadDb",
        data:
            "requestid=" + m_request_id + "&" +
            "m_txt_po_number=" + m_txt_po_number + "&" +
            "m_txt_so_number=" + m_txt_so_number + "&" +
            "m_txt_est_delivery_date=" + m_txt_est_delivery_date + "&" +
            "m_txt_attachment=" + m_txt_attachment + "&" +
            "m_txt_confirmed_by=" + m_txt_confirmed_by + "&" +
            "m_txt_date_confirmed=" + m_txt_date_confirmed + 
			""
			,
        success: function (res) {

            if (res.substring(0, 3) == "00:") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // open the document?
                window.location = baseUrl + "LeadDb/LeadDbDetails?requestid=" + res.substring(3, 200);

            } else {
                // error
                alert(res.substring(3, 200));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });

}

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();
}

function CreateUploadingBox(obj_id_to_position) {
    var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<!-- <a href=\"\"><img src=\"" + baseUrl + "Images/cancel.png\" style=\"border:0;\" /></a><br /> -->" +
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "Uploading/LdbUploadDialogBox\" width=\"330px\" height=\"76px\">" +
		"<p>Your browser does not support iframes.</p>" +
		"</iframe>" +
		"<br /><input type=\"button\" value=\"Close\" onclick=\"SaveToTextBox('" + obj_id_to_position + "');\" />" +
		"</div>" +
		"</div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content_upload").css('top', btnX + '' + 'px');
    $("#id_content_upload").css('left', btnY + '' + 'px');

    // show 
    $("#id_content_upload").show("fast");
    $("#id_bkg_upload").show();
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function CheckConfRequiredFields() {
    var lacking_fields = "";

    if ($("#txt_po_number").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "PO Number"; } else { lacking_fields = "PO Number"; }
    }

    if ($("#txt_so_number").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "SO Number"; } else { lacking_fields = "SO Number"; }
    }

    if ($("#txt_est_delivery_date").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Delivery Date"; } else { lacking_fields = "Delivery Date"; }
    }
    
    if ($("#txt_attachment").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Attachment"; } else { lacking_fields = "Attachment"; }
    }
    
    if ($("#txt_confirmed_by").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Confirmed By"; } else { lacking_fields = "Confirmed By"; }
    }

    if ($("#txt_date_confirmed").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Date Confirmed"; } else { lacking_fields = "Date Confirmed"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}

function DisableConfirmGroup() {
    $("#txt_po_number").attr('readonly', 'readonly');
    $("#txt_so_number").attr('readonly', 'readonly');
    $("#txt_est_delivery_date").datepicker("disable");

    $("#txt_attachment").attr('onclick', '');
    $("#txt_attachment").attr('readonly', 'readonly');
    $("#txt_attachment").attr('style', 'background:#ededed;');

    $("#txt_confirmed_by").attr('readonly', 'readonly');
    $("#txt_date_confirmed").datepicker("disable");
}

function ReClassLeadDoc(val_req_id) {
    window.location = baseUrl + "Customer/AcctCreateLeadFinalForm?RequestId=" + val_req_id;
}

function CheckRequiredfields() {
    var lacking_fields = "";

    if ($("#rdo_Closed").attr("checked") == "checked") {
        if ($("#txt_closed_amount").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Amount"; } else { lacking_fields = "Amount"; }
        }

        if ($("#txt_closed_date").attr('value') == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Date"; } else { lacking_fields = "Date"; }
        }

        if (lacking_fields != "") {
            alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
            return false;
        }

        return true;
    }

    if ($("#rdo_lost_sales").attr("checked") == "checked") { 
        if ($("#txt_ls_date").attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Date"; } else { lacking_fields = "Date"; }
        }

        /* at least one reason must be selected */
        if (
            $("#chk_reason1").attr("checked") != "checked" &&
            $("#chk_reason2").attr("checked") != "checked" &&
            $("#chk_reason3").attr("checked") != "checked" &&
            $("#chk_reason4").attr("checked") != "checked" &&
            $("#chk_reason5").attr("checked") != "checked" &&
            $("#chk_reason6").attr("checked") != "checked"
        ) { 
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[At Least one reason is selected]"; } else { lacking_fields = "[At Least one reason is selected]"; }
        }

        if( $("#chk_reason6").attr("checked") == "checked" && $("#txt_reason6_desc").attr("value") == ""){
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Reason must not be blank]"; } else { lacking_fields = "[Reason must not be blank]"; } 
        }

        if (lacking_fields != "") {
            alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
            return false;
        }

        return true;
    }
}