var ns4;

function LoadReport(val_base_url, val_url) {
    window.parent.HideMenuFrame();

    var w;
    w = "" +
        "<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\" style=\"width:95%; height:95%;\" >" +
        "<div style=\"padding:1px 5px 3px 0px;\"> <input type=\"button\" value=\"Close\" onclick=\"javascript:hide_dialog_box();\" /> </div>" +
        "<iframe frameBorder=\"0\" src=\"" + baseUrl + "" + val_url + "\" name=\"ReportContainer\" width=\"100%\" height=\"100%\">" +
        "</iframe>" +
        "</div>" +
        "";

    // $("body").after(w);
    $("body").append(w);

    $("#id_content").css('top', '2.3%');
    $("#id_content").css('left', '2.3%');

    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function hide_dialog_box() {
    window.parent.ShowMenuFrame();

    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}
