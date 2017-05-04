

var ns4;

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function DisableEditing() {
    $("#table_details tr:last").prev().hide();
    $("#table_details tr td img").hide();
    $("#txt_acct_code").attr('onclick', '');
}

function CallRouting(val_action_type, val_reqid, val_accCode) {
    /*
    val_mark_type = {'disapprove', 'approve'}
    */
    // var remarks = "";
    var rm = "";
    rm = "";

    DisplayPreloader();

    $.ajax({
        type: "POST", url: baseUrl + "MrktngRequest/CallRouting",
        data:
			"action_type=" + val_action_type + "&" +
            "reqID=" + val_reqid + "&" +
            "val_accCode=" + val_accCode 
			,
        success: function (res) {

            if (res.substring(0, 3) == "00:") {
                // success
                // alert("Success");
                location.reload();
            } else {
                // error
                alert(res.substring(3, res.length));
                HidePreloader();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function AddStipulation(col1) {

    var rowid = $("#tbl_other_stipulation tr").length - 2;
    rowid++;
    $("#tbl_other_stipulation tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td></td>" +
		"</tr>"
	);
}



function Addattachments(col1) {

    var rowid = $("#tbl_attachment tr").length - 2;
    rowid++;
    $("#tbl_attachment tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td><input type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td> <a href=\"" + baseUrl + "SQL/DownloadFile?doctype=MKTR&fileName=" + col1 + "&id=" + GPreparedBy + " \"><img src=\"" + baseUrl + "Images/page_white_get.png\" style=\"border:0;\" /></a> </td>" +
			"<td></td>" +
		"</tr>"
	);
}