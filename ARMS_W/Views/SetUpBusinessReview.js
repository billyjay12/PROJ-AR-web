var ns4;

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));


    if (obj == "txt_acct_territory") {
        $("#txt_area").attr("value", GetValue($("#id_content select option:selected").attr("val_area")));
        $("#txt_area").attr("value_id", GetId($("#id_content select option:selected").attr("val_area")));

        $("#txt_region").attr("value", GetValue($("#id_content select option:selected").attr("val_region")));
        $("#txt_region").attr("value_id", GetId($("#id_content select option:selected").attr("val_region")));
    }

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

function LookUpData(obj_id_to_store, str_data) {

    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
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

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();
}

function DelCurrRow(tbl_id, r_id) {
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();


}

function AddField() {
    // check fields if empty
    /* first field */
   var val_one = $("#tbl_br7 tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value");

    if (val_one == "") {
        alert("Field cannot be empty!.");
        return;
    }

    /* second field */
    var val_two = $("#tbl_br7 tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("Field cannot be empty!.");
        return;
    }
    else {
        

    var rowid = $("#tbl_br7 tr").length - 1;
    rowid++;
    $("#tbl_br7 tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:137px;\"><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:250px;\"><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" size=\"51\" /></td>" +
	        "<td><a href=\"javascript:DelCurrRow('tbl_br7'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
);


	

    // clear values
    $("#table_details tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#table_details tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
    }

    function cancel() {

    alert("Are you sure you want to cancel??")
    {

        var br_no = "";
        br_no = $("#txt_brn").attr("value", "");

        var date = "";
        date = $("#txt_br_date").attr("value", "");

        var acctCode = "";
        acctCode = $("#txt_acctCode").attr("value", "");

        var acctName = "";
        acctName = $("#txt_acctName").attr("value", "");

        var acctOfficer = "";
        acctOfficer = $("#txt_acctOfficer").attr("value", "");

        var salesManager = "";
        salesManager = $("#txt_salesManager").attr("value", "");

        var channelManager = "";
        channelManager = $("#txt_channelManager").attr("value", "");

        var comExAgr = "";
        comExAgr = $("#txt_comExAgr").attr("value", "");

        var comAcctPer = "";
        comAcctPer = $("#txt_comAcctPer").attr("value", "");

        var STOrigAnn = "";
        STOrigAnn = $("#txt_STOrigAnn").attr("value", "");

        var txt_STRevAnn = "";
        STRevAnn = $("#txt_STRevAnn").attr("value", "");

        var txt_STReason = "";
        STReason = $("#txt_STReason").attr("value", "");

        var plan = "";
        plan = $("#txt_plan").attr("value", "");

        var support = "";
        support = $("#txt_support").attr("value", "");

        var field = "";
        field = $("#txt_field").attr("value", "");

        var existing_val = "";
        existing_val = $("#txt_existing_val").attr("value", "");

        var revised_val = "";
        revised_val = $("#txt_revised_val").attr("value", "");

        var ExstcrdLimit = "";
        ExstcrdLimit = $("#txt_ExstcrdLimit").attr("value", "");

        var ReccrdLimit= "";
        ReccrdLimit = $("#txt_ReccrdLimit").attr("value", "");

        var ExstcrdTerm = "";
        ExstcrdTerm = $("#txt_ExstcrdTerm").attr("value", "");

        var ReccrdTerm = "";
        ReccrdTerm = $("#txt_ReccrdTerm").attr("value", "");



    }