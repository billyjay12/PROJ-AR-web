var ns4;

function CreateUploadingBox(obj_id_to_position, obj_table) {
    var uploaddate = $("#txt_upload_date").attr("value");
    var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "Uploading/SalesOrderStatusUploading?uploaddate=" + uploaddate + "\" width=\"330px\" height=\"76px\">" +
		"<p>Your browser does not support iframes.</p>" +
		"</iframe>" +
		"<br /><input type=\"button\" value=\"Close\" onclick=\"SaveToTextBox('" + obj_table + "');\" />" +
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

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function SaveToTextBox(table) {
    // forupload
    var new_file_name = "";
    new_file_name = $("#uploadframe").contents().find('body #file_name').attr('value');
    $("#txt_filename").attr("value", new_file_name);
    if (typeof (new_file_name) !== "undefined" && new_file_name != "") {
        // $("#txt_filename").attr("value", "true");
    }

    //contents
    var excel_datas = new Array();
    $("#uploadframe").contents().find("body").find("table").find("tr").each(
        function () {

            var name = $(this).find("td:nth-child(1)").html();
            var address = $(this).find("td:nth-child(2)").html();
            var email = $(this).find("td:nth-child(3)").html();

            var row = "";
            row = row + "<tr>";

            row = row + "<td>";
            row = row + name;
            row = row + "</td>";

            row = row + "<td>";
            row = row + address;
            row = row + "</td>";

            row = row + "<td>";
            row = row + email;
            row = row + "</td>";

            row = row + "</tr>";

            $("#" + table).find("tr:last-child").after(row);
        }
    );

    // append the rows to the table

    // $("#" + txt_box).attr('value', new_file_name);
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();

    $("#id_content_upload").remove();
    $("#id_bkg_upload").remove();
}


$(function () {
    var d1 = new Date();
    var str_month = d1.getMonth() + 1;
    var str_day = d1.getDate();
    var str_year = d1.getFullYear();

    var str_date = str_month + "/" + str_day + "/" + str_year;
    $("#txt_upload_date").attr("value", str_date);
    $("#txt_upload_date").datepicker();

    $("#btn_upload").click(
        function () {
            CreateUploadingBox('btn_upload', 'tbl_excel_contents');
        }
    );

});