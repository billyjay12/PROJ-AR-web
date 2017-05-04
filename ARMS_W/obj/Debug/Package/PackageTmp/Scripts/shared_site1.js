function CheckLastLogOnDate() {
    var ccanum = "";
    ccanum = $("#txt_acct_ccanum").attr('value');

    $.ajax({
        type: "POST", url: baseUrl + "Auth/NumDaysBeforeExp",
        data: "",
        success: function (res) {
            if (SrvResultMsg.GetMsgType(res) != "error") {
                DisplayReminder(SrvResultMsg.GetMessage(res));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {  }
    });

}

function DisplayReminder(msg) {
    var w = "";

    w = w + "<div id=\"div_reminder\" style=\"display:none;\" >";
    w = w + "";
    w = w + "<div class=\"sub_div\" style=\" background:#2c72c0; color:white; padding:2px 5px 2px 5px;\">";
    w = w + "<img src=\"" + baseUrl + "Images/error.png\" style=\"border:0;\" /> <b>Reminder</b>";
    w = w + "</div>";
    w = w + "<div style=\"padding:4px 10px 4px 10px; color:#b30606;\">";
    w = w + msg;
    w = w + "<br /> Click <a href=\"" + baseUrl + "Config/ChangesPassword\">here</a> to change your password";
    w = w + "</div>";
    w = w + "";
    w = w + "</div>";

    $("body").append(w);
    $("#div_reminder").show("slow");
}

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
    window.parent.HideMenuFrame();

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

                // refresh main page
                window.parent.RefreshPage();

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

function ShowLogin1() {
    try {
        window.parent.HideMenuFrame();
    }
    catch (err) { }
    var w = "" +
		"<div id=\"id_bkg\" style=\"opacity: 0.40; filter:alpha(opacity=40); z-index:9000\" class=\"dlg_box_bkg\" onclick=\"javascript:;\"></div>" +
		"<div id=\"id_content\" style=\"border:3px solid #575757; z-index:9001\" class=\"dlg_box_content\">" +
		"<table id=\"tbl_reLogin\" cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"margin:2px;\" >";

    w = w + "" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" style=\"background:#256e9d; color:#ffffff; padding:5px;\" >" +
                "<b>Session Expired</b>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" style=\"width:115%\">" +
            " <div class=\"img_holder1\" style=\"width:101%; \" ></div>&nbsp;</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "Username" +
            "</td>" +
            "<td>" +
                "<input type=\"text\" id=\"txtUsername\" style=\"width:200px;\" />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "Password" +
            "</td>" +
            "<td>" +
                "<input type=\"password\" id=\"txtPassword\" />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td align=\"right\" colspan=\"2\" >" +
                "<span id=\"spn_fld\" style=\"font:italic red\"></span>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" >" +
                "<hr />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td align=\"center\" colspan=\"2\" >" +
                "<input type=\"button\" value=\"Login\" onclick=\"javascript:reLogInUser();\" />" +
            "</td>" +
        "</tr>" +
        "";

    w = w + "" +
		"</table></div>" +
		"";

    // append
    $("body").after(w);

    $("#txtPassword, #txtUsername").keypress(
        function (e) {
            if (e.which == 13) {
                e.preventDefault();
                reLogInUser($("#txtUsername").attr("value"), $("#txtPassword").attr("value"));
            }
        }
    );

    // set position
    var btnY = 40;
    var btnX = 35;

    $("#id_content").css('top', btnX + '' + '%');
    $("#id_content").css('left', btnY + '' + '%');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function reLogInUser() {
    if ($("#tbl_reLogin tr td input[type=text]:first").attr("value") == "" || $("#tbl_reLogin tr td input[type=password]:last").attr("value") == "") {
        alert("USERNAME and PASSWORD CANNOT BE EMPTY!");
        return;
    }

    var uname = $("#tbl_reLogin tr td input[type=text]:first").attr("value");

    var password;
    password = $("#tbl_reLogin tr td input[type=password]:first").attr("value");

    var new_obj = { usrname: uname, psswrd: password };

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        type: "POST", url: baseUrl + "Auth/IsValidUser",
        data: JSON.stringify(new_obj),
//        data:
//            "usrname=" + uname +
//            "&psswrd=" + password,
        success: function (res) {
            if (res == "True") {
                window.parent.location = baseUrl + "SecurePage/FramedSetted";
            } else if (res == "False") {
                // go to error page
                alert("INVALID USERNAME/PASSWORD!");

                $("#tbl_reLogin tr td input[type=text]:first").removeAttr("value");
                $("#tbl_reLogin tr td input[type=password]:first").removeAttr("value");

                $("#tbl_reLogin tr td input[type=text]:first").focus();

                $("#spn_fld").text("incorrect password").css("color", "red");
            } else if (res == "Lock") {
                alert("ACCOUNT IS LOCKED!");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}
