$(document).ready(function () {

    getLatestDate_salesorderstatus();
});

function ShowReportViewer(rpt_name) {

    var w = "";
    var params = new Array();

    switch (rpt_name) {
        case "salesorderstatus":
            params["page"] = "salesorderstatus";
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
    w = w + "<div id=\"html_report_maker_background\" style=\" opacity: 0.60; filter:alpha(opacity=60); background:#3a3a3a; position:fixed; top:0; left:0; height:100%; width:100%; \" >";
    w = w + "</div>";

    w = w + "<div id=\"html_report_maker_content\" style=\"padding:0px; background:white; position:absolute; top:3px; left:3px; width:800px; height:600px; \" >";
    //    w = w + "<div style=\"background:#3b6bac; padding:2px; \"><input type=\"button\" value=\"Close\" onclick=\"javascript:HideHTMLReport()\" ></div>";

    if (parameters["page"] != "salesorderstatus")
        w = w + "<div style=\"background:#3b6bac; padding:2px; \"><input type=\"button\" value=\"Close\" onclick=\"javascript:HideHTMLReport()\" ></div>";
    else
        w = w + "<div style=\"background:#3b6bac; padding:2px; \"><input type=\"button\" value=\"Close\" onclick=\"javascript:HideHTMLReport()\" ><span style=\"color:#ffffff; float:right;\">REPORT AS OF <input type=\"text\" style=\"background-color:#ededed; width:80px; font-family:italic;\" value=\"" + $("#uploadDateTime").attr("value") + "\" readonly=\"true\"></span></div>";
  
        w = w + "<iframe frameborder=\"0\" style=\"width:796px; height:570px; \" src=\"" + baseUrl + "Reports/Page/" + parameters["page"] + ".aspx?" + processed_parameters + "\">";

        
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

function getLatestDate_salesorderstatus() {
    var new_obj = { };

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Utilities/getLatestDateSalesOrderStatusReport",

        success: function (res) {
            if (res.iserror != true) {
                $("#uploadDateTime").attr("value", res.data.uploadDateTime);
            }
            else
                alert(res.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
        }
    });
}