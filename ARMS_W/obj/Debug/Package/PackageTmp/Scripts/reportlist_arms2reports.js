
function ShowReportViewer(rpt_name) {

    var w = "";
    var params = new Array();

    switch (rpt_name) {
        case "SOMTDPerformance":
            params["page"] = "SOMTDPerformance";
            break;
        case "SalesTargetDistribution":
            params["page"] = "SalesTargetDistribution";
            break;
        case "SOMonthlyWorkPlan":
            params["page"] = "SOMonthlyWorkPlan";
            break;
        case "CustomerCallEfficiency":
            params["page"] = "CustomerCallEfficiency";
            break;
        case "DetailedCallReport":
            params["page"] = "DetailedCallReport";
            break;
        case "ActivityLogReport":
            params["page"] = "arms2_ActivityLogReport";
            break;
        case "CallReportCollection":
            params["page"] = "CallReportCollection";
            break;
        case "ARMSNewsFeedReport":
            params["page"] = "ARMSNewsFeedReport";
            break;
        case "CallReportLocations":
            params["page"] = "CallReportLocations";
            break;
        case "CoveragePlanNoOfAccounts":
            params["page"] = "CoveragePlanNoOfAccounts";
            break;
        case "CustomerCreditLine":
            params["page"] = "CustomerCreditLine";
            break;
        case "CustomerCreationLeadTime":
            params["page"] = "CustomerCreationLeadTime";
            break;
        case "SapApprovalMonitoring":
            params["page"] = "SapApprovalMonitoring";
            break;
        case "ArmsApprovalMonitoring":
            params["page"] = "ArmsApprovalMonitoring";
            break;
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
    w = w + "<div id=\"html_report_maker_background\" style=\"opacity: 0.60; filter:alpha(opacity=60); background:#3a3a3a; position:absolute; top:0; left:0; height:100%; width:100%; \" >";
    w = w + "</div>";

    w = w + "<div id=\"html_report_maker_content\" style=\"padding:0px; background:white; position:absolute; top:3px; left:3px; width:800px; height:600px;\" >";
   // w = w + "<div id=\"html_report_maker_content\" style=\"padding:0px; background:white; position:absolute; top:3px; left:3px; width:1200px; height:600px; \" >";

    w = w + "<div style=\"background:#3b6bac; padding:2px;\"><input type=\"button\" value=\"Close\" onclick=\"javascript:HideHTMLReport()\" ></div>";
    w = w + "<iframe frameborder=\"0\" scrolling=\"yes\" style=\"overflow:scroll; width:796px; height:570px; \" src=\"" + baseUrl + "Reports/Page/" + parameters["page"] + ".aspx\">";
    //w = w + "<iframe frameborder=\"0\" style=\"width:1200px; height:570px; \" src=\"" + baseUrl + "Reports/Page/" + parameters["page"] + ".aspx\">";


    w = w + "</iframe>";

    w = w + "</div>";

    $("body").after(w);

    $("#html_report_maker_background").show();
    $("#html_report_maker_content").show("fast");
}

function HideHTMLReport() {
    $("#html_report_maker_content").hide("fast");
    $("#html_report_maker_background").hide();

    $("#html_report_maker_content").remove();
    $("#html_report_maker_background").remove();
}