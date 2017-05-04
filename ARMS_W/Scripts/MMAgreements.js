var ns4;

/* should only display two column */
function LookUpData(obj_id_to_store, str_data) {

    $.ajax({
        type: "POST", url: "../SQL/GetList",
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
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "Uploading/CcaUploadDialogBox\" width=\"330px\" height=\"76px\">" +
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


function ShowSearchDlg(obj_id_to_position) {
    /* pops-up from the side? */
    var w = "";

    w = "" +
        "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
        "<div id=\"id_content\" class=\"dlg_box_content\">" +
        "<div style=\"padding:3px; background:#ededed;\">" +
        "<table id=\"tbl_search\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" >" +
        "<tr><td>" +
            "<input type=\"text\" style=\"width:100px;\" />" +
            "<input type=\"text\" id=\"tdate\" style=\"width:100px; display:none;\" />" +
            "</select>" +
        "</td><td>" +
        "<select id=\"search_type\" style=\"width:100px;\">" +
            "<option value=\"cardname\"> Name </option>" +
            "<option value=\"cardcode\"> Code </option>" +
        "</select>" +
        "</td></tr><tr><td colspan=\"2\" align=\"right\" >" +
        "<input type=\"button\" value=\"Search\" onclick=\"javascript:;\" />" +
        "</td></tr></table>" +
        "</div>" +
        "</div>" +
        "";

    $("body").after(w);

    $("#tbl_search tr td input[type=button]").bind("click", function (e) {
        SearchDt();
    });

    $("#tbl_search tr td input[type=text]:first").bind("keypress", function (e) {
        if (e.which == 13) {
            SearchDt();
        }
    });

    $("#tdate").datepicker({
        onSelect: function (dateText, inst) {
            $("#tbl_search tr td input[type=text]:first").attr("value", $("#tdate").attr("value"));
        }
    });

    $("#tbl_search #search_type").bind("change",
        function (e) {
            if ($("#tbl_search #search_type option:selected").attr('value') == "datecreated") {
                // show the datetime picker
                $("#tbl_search tr td input[type=text]:first").hide();
                $("#tbl_search #stat_selector").hide();

                $("#tdate").show();
            } else if ($("#tbl_search #search_type option:selected").attr('value') == "status") {
                // show combo box
                $("#tbl_search tr td input[type=text]:first").hide();
                $("#tdate").hide();

                // 
                $("#tbl_search #stat_selector").show();
            } else {
                // hide datetime picker
                // show the textbox
                $("#tbl_search tr td input[type=text]:first").show();

                $("#tdate").hide();
                $("#tdate").attr('value', '');

                $("#tbl_search #stat_selector").hide();

                $("#tbl_search tr td input[type=text]:first").attr('value', '');
            }
        }
    );

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    var btnHeight = document.getElementById(obj_id_to_position).offsetHeight;

    if ((btnY + $("#id_content").width()) > $(window).width()) {
        btnY = $(window).width() - ($("#id_content").width() + 20);
    }

    btnX = btnX + btnHeight;
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();

    $("#tbl_search tr td input[type=text]:first").focus();
}

function SearchDt() {
    var data_to_filter = $("#tbl_search #search_type option:selected").attr('value');
    var data_value_to_filter = '';
    var search_option = "";

    if (data_to_filter == "cardcode") {
        data_value_to_filter = $("#tbl_search tr td input[type=text]:first").attr('value');
        search_option = "?cardcode=" + data_value_to_filter;
    }

    if (data_to_filter == "cardname") {
        data_value_to_filter = $("#tbl_search tr td input[type=text]:first").attr('value');
        search_option = "?cardname=" + data_value_to_filter;
    }

    if (data_to_filter == "datecreated") {
        data_value_to_filter = $("#tbl_search #tdate").attr('value');
        search_option = "?datecreated=" + data_value_to_filter;
    }

    if (data_to_filter == "status") {
        data_value_to_filter = $("#tbl_search #stat_selector option:selected").attr('value');
        search_option = "?status=" + data_value_to_filter;
    }

    window.location = baseUrl + "Document/MMAgreements" + search_option;
}

function ShowFilterBy(obj_id_to_position) {
    var w = "";

    w = "" +
        "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
        "<div id=\"id_content\" class=\"dlg_box_content\">" +
        "<div style=\"padding:3px; background:#ededed;\">" +
        "<table id=\"tbl_search2\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" >" +
        "<tr><td colspan=\"2\">" +
            "<select id=\"stat_selector\" >" +
                "<option value=\"1\">For CSR Update</option>" +
                "<option value=\"2\">For ASM Approval</option>" +
                "<option value=\"3\">For Channel Manager Approval</option>" +
                "<option value=\"7\">For C&C Approval</option>" +
                "<option value=\"8\">For Finance Mgr. Approval</option>" +
                "<option value=\"6\">For Sales Director Approval</option>" +
                "<option value=\"9\">For VPTFI Approval</option>" +
                "<option value=\"1008\">For Customer Code Creation</option>" +
                "<option value=\"\">All</option>" +
            "</select>" +
        "</td></tr><tr><td colspan=\"2\" align=\"right\" >" +
        "<input type=\"button\" value=\"Search\" onclick=\"javascript:SearchDt2();\" />" +
        "</td></tr></table>" +
        "</div>" +
        "</div>" +
        "";

    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    var btnHeight = document.getElementById(obj_id_to_position).offsetHeight;

    if ((btnY + $("#id_content").width()) > $(window).width()) {
        btnY = $(window).width() - ($("#id_content").width() + 20);
    }

    btnX = btnX + btnHeight;
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();

    $("#tbl_search tr td input[type=text]:first").focus();
}

function SearchDt2() {

    var data_value_to_filter = '';
    var search_option = "";

    data_value_to_filter = $("#tbl_search2 #stat_selector option:selected").attr('value');
    search_option = "?status=" + data_value_to_filter;

    window.location = baseUrl + "Document/MMAgreements" + search_option;
}