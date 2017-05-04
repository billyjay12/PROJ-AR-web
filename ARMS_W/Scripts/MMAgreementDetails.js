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
    // var btnY = getElLeft(document.getElementById(obj_id_to_position));
    // var btnX = getElTop(document.getElementById(obj_id_to_position));
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
    var new_file_name = "";
    new_file_name = $("#uploadframe").contents().find('body #file_name').attr('value');

    if (typeof (new_file_name) !== "undefined" && new_file_name != "") {
        $("#" + txt_box + "_forupload").attr("value", "true");
    }

    $("#" + txt_box).attr('value', new_file_name);
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
        $("#txt_acct_officer").attr("value", $("#id_content select option:selected").attr('val_soname'));
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

function AddEntryActions() {

    // check fields if empty
    /* first field */
    var val_one = $("#tbl_list_of_actions tr:last td:nth-child(1) input[type=text]").attr("value");
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#tbl_list_of_actions tr:last td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* third field */
    var val_three = $("#tbl_list_of_actions tr:last td:nth-child(3) input[type=text]").attr("value");
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
    $("#tbl_list_of_actions tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_list_of_actions tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_list_of_actions tr:last td:nth-child(3) input[type=text]").attr("value", "");
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

function AddActionItems(val1, val2, val3) {
    // add rows
    var rowid = $("#tbl_list_of_actions tr").length - 2;
    rowid++;
    $("#tbl_list_of_actions tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + val1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val3 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_list_of_actions'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function AddAttendees(val1) {
    // add rows
    var rowid = $("#tbl_list_of_attendee tr").length - 2;
    rowid++;
    $("#tbl_list_of_attendee tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input style=\"width:300px;\" type=\"text\" value=\"" + val1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_list_of_attendee'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);
}

function SaveDoc() {
    var i = 0;
    var row_count;

    DisplayPreloader();

    if (CheckRequireFields() == false) {
        return;
    }

    var mma_num;
    // mma_num = $("#txt_acct_code").attr("value");

    var acct_code;
    acct_code = $("#txt_acct_code").attr("value");

    var acct_name;
    acct_name = $("#txt_acct_name").attr("value");

    var meeting_type;
    meeting_type = $("#txt_meeting_type").attr("value");

    var meeting_date;
    meeting_date = $("#txt_meeting_date").attr("value");

    var meeting_time_start;
    meeting_time_start = $("#txt_meeting_start").attr("value");

    var meeting_time_end;
    meeting_time_end = $("#txt_meeting_end").attr("value");

    var meeting_objective;
    meeting_objective = $("#txt_meeting_objective").html();

    var list_of_attendees;
    row_count = $("#tbl_list_of_attendee tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_attendees != "") {
            list_of_attendees = list_of_attendees + "$";
        }
        // first column
        list_of_attendees = list_of_attendees + $("#tbl_list_of_attendee tr:nth-child(" + i + ") td:nth-child(1) input").attr('value');
    }

    var list_of_actions;
    row_count = $("#tbl_list_of_actions tr").length - 1;
    for (i = 2; i <= row_count; i++) {
        if (list_of_actions != "") {
            list_of_actions = list_of_actions + "$";
        }
        // first column
        list_of_actions = list_of_actions + $("#tbl_list_of_actions tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        list_of_actions = list_of_actions + $("#tbl_list_of_actions tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        // third column
        list_of_actions = list_of_actions + $("#tbl_list_of_actions tr:nth-child(" + i + ") td:nth-child(3) input").attr('value');
    }

    var meeting_signed_file;
    meeting_signed_file = $("#txt_signed_file").attr("value");
    var meeting_signed_file_forupload;
    meeting_signed_file_forupload = $("#txt_signed_file_forupload").attr("value");

    var meeting_prepared_by;
    meeting_prepared_by = $("#txt_prepared_by").attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "MMAgreement/SaveMMAgreement",
        data:
            "mma_num=" + "" + "&" +
			"acct_code=" + acct_code + "&" +
            "acct_name=" + acct_name + "&" +
			"meeting_type=" + meeting_type + "&" +
			"meeting_date=" + meeting_date + "&" +
			"meeting_time_start=" + meeting_time_start + "&" +
			"meeting_time_end=" + meeting_time_end + "&" +
			"meeting_objective=" + meeting_objective + "&" +
			"list_of_attendees=" + list_of_attendees + "&" +
            "list_of_actions=" + list_of_actions + "&" +
            "meeting_signed_file=" + meeting_signed_file + "&" +
            "meeting_signed_file_forupload=" + meeting_signed_file_forupload + "&" +
            "meeting_prepared_by=" + meeting_prepared_by + 
			""
			,
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

function CheckRequireFields() {

    return true;
}

function MarkDocument(val_action, val_mma_num) {

    DisplayPreloader();

    $.ajax({
        type: "POST", url: baseUrl + "MMAgreement/MarkMMaDocument",
        data:
            "mma_num=" + val_mma_num + "&" +
            "action_type=" + val_action + 
			""
			,
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

function disable_controls() {

    $("#txt_acct_code").attr('readonly', 'readonly');
    $("#txt_signed_file").attr('onclick', '');

    $("#txt_acct_name").attr('readonly', 'readonly');
    $("#txt_acct_officer").attr('readonly', 'readonly');
    $("#txt_meeting_type").attr('readonly', 'readonly');
    $("#txt_meeting_date").attr('readonly', 'readonly');
    $("#txt_meeting_start").attr('readonly', 'readonly');
    $("#txt_meeting_end").attr('readonly', 'readonly');
    $("#txt_meeting_objective").attr('readonly', 'readonly');
    $("#txt_prepared_by").attr('readonly', 'readonly');

    $("#txt_signed_file_desc").attr('readonly', 'readonly');

    $("#tbl_list_of_attendee tr:last").hide();
    $("#tbl_list_of_attendee tr td img").hide();

    $("#tbl_list_of_actions tr:last").hide();
    $("#tbl_list_of_actions tr td img").hide();

    $("#txt_signed_file").attr('onclick', '');
    $("#txt_acct_code").attr('onclick', '');
}

// retrieving values from attachment data

function AttachmentData(Attachment_one, Attachment_two, url_link) {

    var rowid = $("#tbl_MMA tr").length - 1;
    rowid++;
    $("#tbl_MMA tr:last").after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + Attachment_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td><input type=\"text\" value=\"" + Attachment_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /> " + url_link + "</td>" +
        "</tr>"
	);

}