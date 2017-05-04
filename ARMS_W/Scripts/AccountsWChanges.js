﻿var ns4;

var sub_menu_link_4 = null;
var sub_menu_link_3 = null;
var sub_menu_link_1 = null;

$(function () {

    sub_menu_link_4 = $("#sub_menu_link_4");
    sub_menu_link_3 = $("#sub_menu_link_3");
    sub_menu_link_1 = $("#sub_menu_link_1");

    sub_menu_link_4.click(function () {
        ShowFilterBy('sub_menu_link_4');
    });

    sub_menu_link_3.click(function () {
        ShowSearchDlg('sub_menu_link_3');
    });

    sub_menu_link_1.click(function () {
        ShowSubMenu('sub_menu_link_1');
    });

});


function ShowSubMenu(obj_id_to_position) {
    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table cellspacing=\"0\" style=\"font-size:11px; font-family;arial;\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"left\" style=\"padding:3px;\" >" +
		"<a style=\"font-weight:bold; font-size:11px; color:#656565; text-decoration:none;\" href=\"" + baseUrl + "Customer/AcctCreateRegularForm\">New Regular Customer</a> <br />" +
        "</td></tr>" +
        "<tr><td align=\"left\" style=\"padding:3px;\" >" +
        "<a style=\"font-weight:bold; font-size:11px; color:#656565; text-decoration:none;\" href=\"" + baseUrl + "Customer/AcctCreateWalkInForm\">New Walk-In Customer</a> " +
		"</td></tr>" +
        "</table></div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    var btnHeight = document.getElementById(obj_id_to_position).offsetHeight;
    btnX = btnX + btnHeight;
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function hide_dialog_box() {
    var id_content = $("#id_content");
    var id_bkg = $("#id_bkg");

    id_bkg.hide();
    id_content.hide("fast", function () {
        id_bkg.remove();
        id_content.remove();
    });

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
            
            "</select>" +
        "</td><td>" +
        "<select id=\"search_type\" style=\"width:100px; font-size:11px; font-family;arial;\">" +
            "<option value=\"cardname\"> Name </option>" +
            "<option value=\"cardcode\"> Code </option>" +
            "<option value=\"area\"> Area </option>" +
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
    
    $("#tbl_search #search_type").bind("change",
        function (e) {
            if ($("#tbl_search #search_type option:selected").attr('value') == "datecreated") {
                // show the datetime picker
                $("#tbl_search tr td input[type=text]:first").hide();
                $("#tbl_search #stat_selector").hide();

                
            } else if ($("#tbl_search #search_type option:selected").attr('value') == "status") {
                // show combo box
                $("#tbl_search tr td input[type=text]:first").hide();
                

                // 
                $("#tbl_search #stat_selector").show();
            } else {
                // hide datetime picker
                // show the textbox
                $("#tbl_search tr td input[type=text]:first").show();


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

    if (data_to_filter == "area") {
        data_value_to_filter = $("#tbl_search tr td input[type=text]:first").attr('value');
        search_option = "?area=" + data_value_to_filter;
    }

    if (data_to_filter == "status") {
        data_value_to_filter = $("#tbl_search #stat_selector option:selected").attr('value');
        search_option = "?status=" + data_value_to_filter;
    }

    window.location = baseUrl + "Document/AccountsWChanges" + search_option;
}

function ShowFilterBy(obj_id_to_position) {
    var w = "";

    w = "" +
        "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
        "<div id=\"id_content\" class=\"dlg_box_content\">" +
        "<div style=\"padding:3px; background:#ededed;\">" +
        "<table id=\"tbl_search2\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\" >" +
        "<tr><td colspan=\"2\">" +
            "<select id=\"stat_selector\" style=\"font-size:11px; font-family;arial;\" >" +
                "<option value=\"1\">For CSR Update</option>" +
                "<option value=\"2\">For ASM Approval</option>" +
                "<option value=\"3\">For Channel Manager/RSM Approval</option>" +
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

    
    window.location = baseUrl + "Document/AccountsWChanges" + search_option;
}