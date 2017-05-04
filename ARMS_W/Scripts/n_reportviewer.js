
function ShowNReportViewer(rpt_name, acct_code1, final_acct_code1) {

    var w = "";
    var params = new Array();

    var acct_code = acct_code1;
    var final_acct_code = final_acct_code1;

    if (final_acct_code != "" && final_acct_code != undefined) {
        params["cardcode"] = final_acct_code;
    } else {
        params["cardcode"] = acct_code;
    }

    switch (rpt_name) {
        case "pending_orders":
            params["page"] = "pending_orders"; break;
        case "mtd_sales":
            break;
        case "deliveries":
            params["page"] = "deliveries"; break;
        case "balance_order":
            params["page"] = "balance_orders"; break;
        case "collections":
            params["page"] = "collections"; break;
        case "account_balance":
            params["page"] = "account_balance"; break;
        case "bounce_checks":
            params["page"] = "bounce_checks"; break;
        case "mtdytd_sales":
            params["page"] = "mtdytd_sales"; break;
        case "past_dues":
            params["page"] = "past_dues"; break;
        default:
    }

    CB_REPORT(params);

}

function CB_REPORT(parameters) {
    var processed_parameters = "";

    for (var arr_name in parameters) {
        if (processed_parameters != "") { processed_parameters = processed_parameters + "&"; }
        processed_parameters = processed_parameters + arr_name + "=" + parameters[arr_name];
    }

    var w = "";
    w = w + "<div id=\"html_report_maker_background\" style=\" opacity: 0.60; filter:alpha(opacity=60); background:#3a3a3a; position:fixed; top:0; left:0; height:100%; width:100%; \" >";
    w = w + "</div>";

    w = w + "<div id=\"html_report_maker_content\" style=\"padding:0px; background:white; position:fixed; top:50%; left:50%; margin-left:-400px; margin-top:-300px; width:800px; height:600px; \" >";
    w = w + "<div style=\"background:#3b6bac; padding:2px; \"><input type=\"button\" value=\"Close\" onclick=\"javascript:HideHTMLReport()\" ></div>";
    w = w + "<iframe frameborder=\"0\" style=\"width:796px; height:570px; \" src=\"" + baseUrl + "Reports/Page/" + parameters["page"] + ".aspx?" + processed_parameters + "\">";
    w = w + "</iframe>";

    w = w + "</div>";

    $("frameset[BORDER=0]").append(w);

    $("#html_report_maker_background").show();
    $("#html_report_maker_content").show("fast");
}

function HideHTMLReport() {
    $("#html_report_maker_content").hide("fast");
    $("#html_report_maker_background").hide();

    $("#html_report_maker_content").remove();
    $("#html_report_maker_background").remove();
}


function HideMenuFrame() {
    btm_frameset.attr("cols", "0px,*");
}

function ShowMenuFrame() {
    btm_frameset.attr("cols", "300px,*");
}

function HideTopFrame() {
    top_frameset.attr("rows", "0px,*");
}

function ShowTopFrame() {
    top_frameset.attr("rows", "135px,*");
}