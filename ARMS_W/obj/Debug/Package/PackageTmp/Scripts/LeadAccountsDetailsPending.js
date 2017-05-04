var ns4;
function test() {
    // alert("asdas");
}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function LoadList() {

}


function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function ShowSearchDlg(obj_id_to_position) {
    /* pops-up from the side? */
    var w = "";

    w = "" +
        "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
        "<div id=\"id_content\" class=\"dlg_box_content\">" +
        "<div style=\"padding:3px; background:#ededed;\">" +
        "<table id=\"tbl_search\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" >" +
        "<tr><td><input type=\"text\" style=\"width:100px;\" /></td><td><select style=\"width:100px;\">" +
        "<option value=\"name\"> name </option>" +
    // "<option value=\"datecreated\"> Date Created </option>" +
        "<option value=\"encodedby\"> Encoded by </option>" +
        "</select></td></tr><tr><td colspan=\"2\" align=\"right\" >" +
        "<input type=\"button\" value=\"Search\" onclick=\"javascript:;\" />" +
        "</td></tr></table>" +
        "</div>" +
        "</div>" +
        "";

    $("body").after(w);

    $("#tbl_search tr td input[type=button]").bind("click", function (e) {
        SearchDt();
    });

    $("#tbl_search tr td input[type=text]").bind("keypress", function (e) {
        if (e.which == 13) {
            SearchDt();
        }
    });

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    var btnHeight = document.getElementById(obj_id_to_position).offsetHeight;

    if ((btnY + $("#id_content").width()) > $(window).width()) {
        btnY = $(window).width() - $("#id_content").width();
    }

    btnX = btnX + btnHeight;
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();

    $("#tbl_search tr td input[type=text]").focus();
}

function SearchDt() {
    var data_to_filter = $("#tbl_search tr td select option:selected").attr('value');
    var data_value_to_filter = $("#tbl_search tr td input[type=text]").attr('value');
    var search_option = "";

    if (data_to_filter == "name") {
        search_option = "?name=" + data_value_to_filter;
    }

    if (data_to_filter == "datecreated") {
        search_option = "?datecreated=" + data_value_to_filter;
    }

    if (data_to_filter == "encodedby") {
        search_option = "?encodedby=" + data_value_to_filter;
    }

    window.location = baseUrl + "Document/LeadAccountsDetailsPending" + search_option;
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


    window.location = baseUrl + "Document/LeadAccountsDetailsPending" + search_option;
}