var ns4;

/* delete current row */
function DelCurrRow(tbl_id, r_id) {
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
}

function LookUpData(obj_id_to_store, str_data) {
    DisplayPreloader();
    $.ajax({
        type: "POST", url: "../SQL/GetList",
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
                w = w + "<option val_soname=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
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
    var btnY = getElLeft($("#" + obj_id_to_position)[0]);
    var btnX = getElTop($("#" + obj_id_to_position)[0]);
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function SaveToTextBox(txt_box) {
    // forupload
    $("#" + txt_box + "_forupload").attr("value", "true");

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
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "Uploading/MmaUploadDialogBox\" width=\"330px\" height=\"76px\">" +
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

function SetValueFromSelect(obj) {

    if (obj == "txt_acct_code") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
        $("#txt_acct_name").attr("value", $("#id_content select option:selected").text());
        // $("#txt_acct_officer").attr("value", $("#id_content select option:selected").attr('val_soname'));

        // get other data
        GetExtraDatas("ListOfAcctNameWSoName", $("#id_content select option:selected").attr('value'), obj, "txt_acct_officer");
    }
    
    if (obj == "txt_meeting_type") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function GetId(strVal) {
    return strVal.substring(0, strVal.indexOf('-') - 1);
    return strVal;
}

function GetValue(strVal) {
    return strVal.substring(strVal.indexOf('-') + 2, 200);
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function Save_Doc() {


    if (CheckRequiredFields() == true) {
        AddNewMeetings();
    }
}

function CheckRequiredFields() {

    if ($("#txt_acct_code").attr("value") == ""){
        alert("PLEASE FILL IN THE REQUIRED FIELDS.");
        return false;
    }

    if ($("#txt_meeting_date").attr("value") == ""){
        alert("PLEASE FILL IN THE REQUIRED FIELDS.");
        return false;
    }

    // REMOVED
    // to enhancement
    /*
    if ($("#txt_meeting_start").attr("value") == ""){
        alert("Please fill in the required fields");
        return false;
    }

    if ($("#txt_meeting_end").attr("value") == ""){
        alert("Please fill in the required fields");
        return false;
    }
    */

    if ($("#txt_prepared_by").attr("value") == ""){
        alert("PLEASE FILL IN THE REQUIRED FIELDS.");
        return false;
    }

    if ($("#tbl_list_of_attendee tr").length - 2 == 0) {
        alert("PLEASE ADD LIST OF ATTENDEES.");
        return false;
    }

    if ($("#tbl_list_of_actions tr").length - 2 == 0) {
        alert("PLEASE ADD LIST OF ACTIONS.");
        return false;
    }

    return true;
}

function AddNewMeetings() {
    var row_count;

    DisplayPreloader();

    var acct_code = "";
    acct_code = $("#txt_acct_code").attr("value");

    var acct_name = "";
    acct_name = $("#txt_acct_name").attr("value");

    var acct_officer = "";
    acct_officer = $("#txt_acct_officer").attr("value");

    var meeting_type = "";
    meeting_type = $("#txt_meeting_type").attr("value");

    var meeting_date = "";
    meeting_date = $("#txt_meeting_date").attr("value");

    var meeting_objective = "";
    meeting_objective = $("#txt_meeting_objective").attr("value");

    /*
    var list_of_attendees = "";
    row_count = $("#tbl_list_of_attendee tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_attendees != "") {
            list_of_attendees = list_of_attendees + "$";
        }
        // first column
        list_of_attendees = list_of_attendees + $("#tbl_list_of_attendee").find("tr:nth-child(" + i + ") td:nth-child(1) input").attr('value');
    }
    */
    var list_of_attendees = new Array();
    row_count = $("#tbl_list_of_attendee tr").length;
    var loop_count = 0;
    $("#tbl_list_of_attendee tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_of_attendees.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                );
            }
        }
    );


    /*
    var list_of_actions = "";
    row_count = $("#tbl_list_of_actions tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_actions != "") {
            list_of_actions = list_of_actions + "$";
        }
        // first column
        list_of_actions = list_of_actions + $("#tbl_list_of_actions").find("tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_actions = list_of_actions + $("#tbl_list_of_actions").find("tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_actions = list_of_actions + $("#tbl_list_of_actions").find("tr:nth-child(" + i + ") td:nth-child(3) input").attr('value');
    }
    */
    var list_of_actions = new Array();
    row_count = $("#tbl_list_of_actions tr").length;
    var loop_count = 0;
    $("#tbl_list_of_actions tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 1 && loop_count < row_count) {
                list_of_actions.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(3) input[type=text]").attr('value')
                );
            }
        }
    );

    var meeting_prepared_by = "";
    meeting_prepared_by = $("#txt_prepared_by").attr("value");

    // Saving attachment
    /*
    var meeting_signed_minutes = "";
    row_count = $("#tbl_MMA tr").length - 2;
    //alert(row_count);
    for (i = 3; i <= row_count; i++) {
        if (meeting_signed_minutes != "") {
            meeting_signed_minutes = meeting_signed_minutes + "$";
        }
        // first column
        meeting_signed_minutes = meeting_signed_minutes + $("#tbl_MMA tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        meeting_signed_minutes = meeting_signed_minutes + $("#tbl_MMA tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";

    }
    */
    var meeting_signed_minutes = new Array();
    row_count = $("#tbl_MMA tr").length - 1;
    var loop_count = 0;
    $("#tbl_MMA tr").each(
        function (index, element) {
            loop_count++;
            if (loop_count > 2 && loop_count < row_count) {
                meeting_signed_minutes.push(
                    $(element).find("td:nth-child(1) input[type=text]").attr('value')
                    + "|" + $(element).find("td:nth-child(2) input[type=text]").attr('value')
                );
            }
        }
    );



    var params = {
        acct_code:  acct_code,
        acct_name: acct_name,
        acct_officer: acct_officer,
        meeting_type: meeting_type,
        meeting_date: meeting_date,
        meeting_objective: meeting_objective,
        list_of_attendees: list_of_attendees,
        list_of_actions: list_of_actions,
        meeting_signed_minutes: meeting_signed_minutes,
        meeting_prepared_by: meeting_prepared_by
    }

    $.ajax({
        type: "POST", url: baseUrl + "MMAgreement/AddNewMeetingsAndAgreements",
        data: $.param(params, true),
        success: function (res) {

            if (res.substring(0, 3) == "00:") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // open the document?
                window.location = baseUrl + "MMAgreement/MMAgreementDetails?agreeno=" + res.substring(3, 200);

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

function AddEntryActions() {

    // check fields if empty
    /* first field */
    var val_one = $("#tbl_list_of_actions tr:last").find("td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_list_of_actions tr:last").find("td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_list_of_actions tr:last").find("td:nth-child(3) input[type=text]").attr("value");
    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var rowid = $("#tbl_list_of_actions tr").length - 2;
    rowid++;
    $("#tbl_list_of_actions tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_list_of_actions'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_list_of_actions tr:last").find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_list_of_actions tr:last").find("td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_list_of_actions tr:last").find("td:nth-child(3) input[type=text]").attr("value", "");
}

function AddEntryAttendees() {

    // check fields if empty
    /* first field */
    var val_one = $("#tbl_list_of_attendee tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var rowid = $("#tbl_list_of_attendee tr").length - 2;
    rowid++;
    $("#tbl_list_of_attendee tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:300px;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_list_of_attendee'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_list_of_attendee tr:last td:nth-child(1) input[type=text]").attr("value", "");
}

function ShowTimeSelector(obj_id_to_position) {
    var w = "";
    w = w + "" +
        "<div id=\"id_bkg_time_selector\"></div>" +
        "<div id=\"id_content_time_selector\">" +
        "<input type=\"text\" style=\"width:15px;\" class=\"txt_num_field_only_12\" />:" +
        "<input type=\"text\" style=\"width:15px;\" class=\"txt_num_field_only_59\" /> " +
        "<select style=\"width:50px;\" >" +
            "<option selected=\"selected\" >AM</option>" + "<option>PM</option>" +
        "</select>" +
        "<a href=\"javascript:;\" style=\"font-size:11px; font-family:arial; padding:0px 3px 0px 3px; \" >select</a>" +
        "</div>" +
        "";

    // append
    $("body").after(w);

    // start binding
    $(".txt_num_field_only_12").bind("keypress", function (e) {
        var prev_value = $(this).attr('value');
        var cur_value = String.fromCharCode(e.which)
        if ((e.which > 47 && e.which < 59) || e.which == 8) {
            // must not exceed to 12
            if (parseInt(prev_value + cur_value) > 12) { e.preventDefault(); }
        } else {
            e.preventDefault();
        }
    });

    $(".txt_num_field_only_59").bind("keypress", function (e) {
        var prev_value = $(this).attr('value');
        var cur_value = String.fromCharCode(e.which)
        if ((e.which > 47 && e.which < 59) || e.which == 8) {
            // must not exceed to 59
            if (parseInt(prev_value + cur_value) > 59) { e.preventDefault(); }
        } else {
            e.preventDefault();
        }
    });

    // for selecting a value
    $("#id_content_time_selector a").bind("click", function (e) {

        if (
             $("#id_content_time_selector input[type=text]:nth-child(1)").attr('value') == "" ||
             $("#id_content_time_selector input[type=text]:nth-child(2)").attr('value') == ""
            ) {
        } else {

            $("#" + obj_id_to_position).attr('value',
                $("#id_content_time_selector input[type=text]:nth-child(1)").attr('value') +
                ":" +
                $("#id_content_time_selector input[type=text]:nth-child(2)").attr('value') +
                ' ' +
                $("#id_content_time_selector select option:selected").text()
            );

            $("#id_bkg_time_selector").hide();
            $("#id_content_time_selector").hide("fast");   
        }

    });

    // if clicking the background
    $("#id_bkg_time_selector").bind("click", function (e) {
        $("#id_bkg_time_selector").hide();
        $("#id_content_time_selector").hide("fast");
    });

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content_time_selector").css('top', btnX + '' + 'px');
    $("#id_content_time_selector").css('left', btnY + '' + 'px');

    // show 
    $("#id_content_time_selector").show("fast");
    $("#id_bkg_time_selector").show();

    // focus
    $("#id_content_time_selector input[type=text]:nth-child(1)").focus();
}

function LookUpDocumentType(obj_id_to_store, str_data) {
    
    DisplayPreloader();
    $.ajax({
        type: "POST", url: "../SQL/GetList",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBoxDocType(obj_id_to_store, StrResultTags(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

}

function CreateDialogBoxDocType(obj_id_to_position, data_to_add) {

    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table id=\"tbl_doctype_lookup\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"right\" >" +
		"<select style=\"width:250px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<option val_soname=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
            }
        }
    }

    w = w + "" +
		"\n</select>" +
        "<br />" +
        "<div style=\"font-size:9px; font-name:arial; text-align:left; padding:3px; display:none;\">" +
        "Please Specify: <input type=\"text\" />" +
        "</div>" +
		" <input onclick=\"javascript:SetValueFromSelectDocType('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

    // append
    $("body").after(w);

    $("#tbl_doctype_lookup select").bind("change",
        function () {
            if ($(this).attr('value') == "others") {
                $("#tbl_doctype_lookup div").show();
                $("#tbl_doctype_lookup input[type=text]").attr('value', '');
            } else {
                $("#tbl_doctype_lookup div").hide();
            }
        }
    );

    // set position
    var btnY = getElLeft($("#" + obj_id_to_position)[0]);
    var btnX = getElTop($("#" + obj_id_to_position)[0]);
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function SetValueFromSelectDocType(obj) {
    
    if ($("#tbl_doctype_lookup select").attr('value') == "others") {
        $("#" + obj).attr("value", $("#tbl_doctype_lookup input[type=text]").attr('value'));
    } else {
        $("#" + obj).attr("value", $("#tbl_doctype_lookup select").attr('value'));
    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function GetExtraDatas(val_lookup_type, val_par1, val_main_obj, val_obj1, val_obj2, val_obj3, val_obj4) {
    var ajx_data = "";

    if (val_main_obj == "txt_acct_code") {
        ajx_data = "_str_data=" + val_lookup_type + "&par1=" + val_par1;
    } else {
        ajx_data = "_str_data=" + val_lookup_type;
    }

    $.ajax({
        type: "POST", url: "../SQL/GetList",
        data: ajx_data,
        success: function (res) {
            if (IsError(res)) {
                // StrResultTags(res);
                var data_to_add = StrResultTags(res);
                var res_rows = data_to_add.split("#$");
                for (i = 0; i < res_rows.length; i++) {
                    var res_cols = res_rows[i].split("|");
                    if (res_cols[0] != null) {
                        if (res_cols[0] != "") {
                            // 
                            $("#" + val_obj1).attr("value", res_cols[0]);
                        }
                    }
                }
            } else {

            }

        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}

function AddMmaAttachments() {

    var Attachment_one = $("#tbl_MMA tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value");

    if (Attachment_one == "") {
        alert("FILE FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var Attachment_two = $("#tbl_MMA tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value");

    if (Attachment_two == "") {
        alert("DESCRIPTION FIELD CANNOT BE EMPTY!");
        return;
    }

    var rowid = $("#tbl_MMA tr").length - 1;
    rowid++;
    $("#tbl_MMA tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + Attachment_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td><input type=\"text\" value=\"" + Attachment_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_MMA'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
    // clear values
    $("#tbl_MMA tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_MMA tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
}
