function disable_group1() {
    $("#txt_nego_contact_person").attr('disabled', 'disabled');
    $("#txt_nego_contact_number").attr('disabled', 'disabled');
    $("#txt_nego_date").datepicker("disable");
    $("#is_contacted").attr('disabled', 'disabled');
}

function disable_group2() {
    $("#txt_qt_submit_date").datepicker("disable");
    $("#txt_qoute_received_by").attr('disabled', 'disabled');
}

function disable_group3() {
    $("#txt_followup_date").datepicker("disable");
}

function enable_group1() {
    $("#is_contacted").removeAttr('disabled');
    $("#txt_nego_date").datepicker("enable");
    $("#txt_nego_contact_person").removeAttr('disabled');
    $("#txt_nego_contact_number").removeAttr('disabled');
}

function enable_group2() {
    $("#txt_qt_submit_date").datepicker("enable");
    $("#txt_qoute_received_by").removeAttr('disabled');
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
    $("#txt_closed_amount").removeAttr('disabled'); $("#txt_closed_amount").removeAttr('readonly');
}

function disable_rdo_close_group() {
    $("#txt_closed_date").datepicker("disable");
    $("#txt_closed_amount").attr('disabled', 'disabled'); $("#txt_closed_amount").attr('readonly', 'readonly');
}

function check_requirefields() {

    var lacking_fields = "";

    // account code
    if ($("#txt_lead_req_id").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "SAP Lead Code"; } else { lacking_fields = "SAP Lead Code"; }
    }

    // nego date
    if ($("#txt_nego_date").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Negotiation Date"; } else { lacking_fields = "Negotiation Date"; }
    }

    // nego contact person
    if ($("#txt_nego_contact_person").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Person"; } else { lacking_fields = "Contact Person"; }
    }

    // nego contact number
    if ($("#txt_nego_contact_number").attr('value') == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Contact Number"; } else { lacking_fields = "Contact Number"; }
    }

    if ($("#rdo_lost_sales").attr("checked") == "checked") {

        // nego contact number
        if ($("#txt_ls_date").attr("value") == "") {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Date"; } else { lacking_fields = "Date"; }
        }

        if(
            $("#chk_reason1").attr("checked") != "checked" &&
            $("#chk_reason2").attr("checked") != "checked" &&
            $("#chk_reason3").attr("checked") != "checked" &&
            $("#chk_reason4").attr("checked") != "checked" &&
            $("#chk_reason5").attr("checked") != "checked" &&
            $("#chk_reason6").attr("checked") != "checked"
        ) {
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[At Least one reason is selected]"; } else { lacking_fields = "[At Least one reason is selected]"; }
        }

        if($("#chk_reason6").attr("checked") == "checked" && $("#txt_reason6_desc").attr("value") == ""){
            if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "[Reason must not be blank]"; } else { lacking_fields = "[Reason must not be blank]"; } 
        }

    }

    if ($("#rdo_Closed").attr("checked") == "checked") { 
        


    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;

}

function AddNewLeadDB() {
    

    if (check_requirefields() == false) {return; }
    
    DisplayPreloader();

    var ldb_requestid = "";
    ldb_requestid = $("#txt_lead_req_id").attr('value_id');

    var ldb_channel = "";
    ldb_channel = "";

    var ldb_sapleadcode = "";
    ldb_sapleadcode = $("#txt_lead_req_id").attr('value');

    var ldb_is_nego_contacted = "";
    // ldb_sapleadcode = ($("#is_contacted").attr('checked') == true) ? "1" : "0";
    ldb_is_nego_contacted = "1";

    var ldb_nego_date = "";
    ldb_nego_date = $("#txt_nego_date").attr('value');

    var ldb_nego_contact_person = "";
    ldb_nego_contact_person = $("#txt_nego_contact_person").attr('value');

    var ldb_nego_contact_number = "";
    ldb_nego_contact_number = $("#txt_nego_contact_number").attr('value');

    /* lost sales */
    var ldb_is_lost_sales = "";
    ldb_is_lost_sales = ($("#rdo_lost_sales").attr('checked') == 'checked') ? "1" : "0";

    var ldb_ls_date = "";
    ldb_ls_date = $("#txt_ls_date").attr('value');

    var ldb_reason1 = "";
    ldb_reason1 = ($("#chk_reason1").attr('checked') == 'checked') ? "1" : "0";

    var ldb_reason2 = "";
    ldb_reason2 = ($("#chk_reason2").attr('checked') == 'checked') ? "1" : "0";

    var ldb_reason3 = "";
    ldb_reason3 = ($("#chk_reason3").attr('checked') == 'checked') ? "1" : "0";

    var ldb_reason4 = "";
    ldb_reason4 = ($("#chk_reason4").attr('checked') == 'checked') ? "1" : "0";

    var ldb_reason5 = "";
    ldb_reason5 = ($("#chk_reason5").attr('checked') == 'checked') ? "1" : "0";

    var ldb_reason6 = "";
    ldb_reason6 = ($("#chk_reason6").attr('checked') == 'checked') ? "1" : "0";

    var ldb_reason6_desc = "";
    ldb_reason6_desc = $("#txt_reason6_desc").attr('value');


    /* closed */
    var ldb_is_closed = "";
    ldb_is_closed = ($("#rdo_Closed").attr('checked') == 'checked') ? "1" : "0";

    var ldb_closed_date = "";
    ldb_closed_date = $("#txt_closed_date").attr('value');

    var ldb_closed_amount = "";
    ldb_closed_amount = $("#txt_closed_amount").attr('value');

    var encodedby = "";
    encodedby = $("#txt_encodedby").attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "LeadDb/AddNewLead",
        data:
            "ldb_requestid=" + ldb_requestid + "&" +
            "ldb_channel=" + ldb_channel + "&" +
            "ldb_sapleadcode=" + ldb_sapleadcode + "&" +
            "ldb_is_nego_contacted=" + ldb_is_nego_contacted + "&" +
            "ldb_nego_date=" + ldb_nego_date + "&" +
            "ldb_nego_contact_person=" + ldb_nego_contact_person + "&" +
            "ldb_nego_contact_number=" + ldb_nego_contact_number + "&" +
            "ldb_is_lost_sales=" + ldb_is_lost_sales + "&" +
            "ldb_ls_date=" + ldb_ls_date + "&" +
            "ldb_reason1=" + ldb_reason1 + "&" +
            "ldb_reason2=" + ldb_reason2 + "&" +
            "ldb_reason3=" + ldb_reason3 + "&" +
            "ldb_reason4=" + ldb_reason4 + "&" +
            "ldb_reason5=" + ldb_reason5 + "&" +
            "ldb_reason6=" + ldb_reason6 + "&" +
            "ldb_reason6_desc=" + ldb_reason6_desc + "&" +
            "ldb_is_closed=" + ldb_is_closed + "&" +
            "ldb_closed_date=" + ldb_closed_date + "&" +
            "ldb_closed_amount=" + ldb_closed_amount + "&" +
			"encodedby=" + encodedby + ""
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


/* for browsing data for a certain field */
/* should only display two column */
function LookUpData(obj_id_to_store, str_data) {
    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "leadDb/GetLeadCodeList",
        data: "_str_data=" + str_data,
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

var ns4;

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", $("#id_content select option:selected").text());
    $("#" + obj).attr("value_id", $("#id_content select option:selected").attr('value'));
    
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function GetId(strVal) {
    return strVal.substring(0, strVal.indexOf('-') - 1);
}

function GetValue(strVal) {
    return strVal.substring(strVal.indexOf('-') + 2, 200);
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function lost_sales_required_fields() {
    $("#txt_ls_date").addClass("required_fields");
}

function closed_amt_required_fields() {
    $("#txt_closed_amount").addClass("required_fields");
    $("#txt_closed_date").addClass("required_fields");
}