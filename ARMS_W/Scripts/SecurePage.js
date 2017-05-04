var ns4;

function CheckActive(val_username) {
    $.ajax({
        type: "POST", url: baseUrl + "SecurePage/IsActive",
        data: "username=" + val_username + "",
        success: function (res) {
            if (res == "true") {
                ShowDlg();
            } else if (res == "p_true") {
                PerstShowDlg();
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function PerstShowDlg() {
    var w = "" +
		"<div id=\"id_bkg\" style=\"opacity: 0.40; filter:alpha(opacity=40);\" class=\"dlg_box_bkg\" onclick=\"javascript:;\"></div>" +
		"<div id=\"id_content\" style=\"border:3px solid #575757;\" class=\"dlg_box_content\">" +
		"<table id=\"tbl_new_password\" cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"margin:2px;\" >";

    w = w + "" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" style=\"background:#256e9d; color:#ffffff; padding:5px;\" >" +
                "<b>Change Password</b>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" >" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "New Password" +
            "</td>" +
            "<td>" +
                "<input type=\"password\" />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "Confirm Password" +
            "</td>" +
            "<td>" +
                "<input type=\"password\" />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" >" +
                "<hr />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td align=\"center\" colspan=\"2\" >" +
                "<input type=\"button\" value=\"Save\" onclick=\"javascript:SaveNewPassword();\" />" +
            "</td>" +
        "</tr>" +
        "";

    w = w + "" +
		"</table></div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = 40;
    var btnX = 35;

    $("#id_content").css('top', btnX + '' + '%');
    $("#id_content").css('left', btnY + '' + '%');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function ShowDlg() {
    var w = "" +
		"<div id=\"id_bkg\" style=\"opacity: 0.40; filter:alpha(opacity=40);\" class=\"dlg_box_bkg\" onclick=\"javascript:;\"></div>" +
		"<div id=\"id_content\" style=\"border:3px solid #575757;\" class=\"dlg_box_content\">" +
		"<table id=\"tbl_new_password\" cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"margin:2px;\" >";

    w = w + "" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" style=\"background:#256e9d; color:#ffffff; padding:5px;\" >" +
                "<b>Change Password</b>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" >" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "New Password" +
            "</td>" +
            "<td>" +
                "<input type=\"password\" />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "Confirm Password" +
            "</td>" +
            "<td>" +
                "<input type=\"password\" />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" >" +
                "<hr />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td align=\"center\" colspan=\"2\" >" +
                "<input type=\"button\" value=\"Cancel\" onclick=\"javascript:hide_dialog_box();\" />" +
                " / " +
                "<input type=\"button\" value=\"Save\" onclick=\"javascript:SaveNewPassword();\" />" +
            "</td>" +
        "</tr>" +
        "";

    w = w + "" +
		"</table></div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = 40;
    var btnX = 35;

    $("#id_content").css('top', btnX + '' + '%');
    $("#id_content").css('left', btnY + '' + '%');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    UpdateUser();
}

function SaveNewPassword() {
    // CHECK IF PASSWORDS ARE THE SAME

    if ($("#tbl_new_password tr td input[type=password]:first").attr("value") == "" || $("#tbl_new_password tr td input[type=password]:last").attr("value") == "") {
        alert("PASSWORD CANNOT BE EMPTY!");
        return;
    } else {
        if ($("#tbl_new_password tr td input[type=password]:first").attr("value") != $("#tbl_new_password tr td input[type=password]:last").attr("value")) {
            alert("PASSWORD DOES NOT MATCH!");
            return;
        }
    }
    
    var new_password;
    new_password = $("#tbl_new_password tr td input[type=password]:first").attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "SecurePage/SaveNewPassword",
        data: "newPassword=" + new_password + "&" + "username=" + G_username,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                alert("SUCCESSFULLY SAVED!");
                hide_dialog_box();
            } else {
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function UpdateUser() {
    
    $.ajax({
        type: "POST", url: baseUrl + "SecurePage/UpdateUser",
        data: "username=" + G_username,
        success: function (res) { },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); 
        }
    });
}