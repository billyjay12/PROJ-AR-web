$(document).ready(function () {

    if (sessionStorage.getItem("WebUsername") != null) {
        $('#login_container').css('display', 'none');
        $('#redirect_page').show();
        getWebUserpass(sessionStorage.getItem("WebUsername"));
    }

    $("#btnLogin").click(
		function (e) {
		    e.preventDefault();
		    auth_user($("#txtUsername").attr("value"), $("#txtPassword").attr("value"));
		}
	);

    $("#txtPassword, #txtUsername").keypress(
        function (e) {
            if (e.which == 13) {
                e.preventDefault();
                auth_user($("#txtUsername").attr("value"), $("#txtPassword").attr("value"));
            }
        }
    );

    $("#txtUsername").focus();



});

function getWebUserpass(username) {
    $.ajax({
        type: "GET",
        url: baseUrl + "Auth/getWebUserpass",
        data: "username=" + username,
        success: function (res) {
            auth_user(username, res.toString());
        }
    });
}


function auth_user(username, password) {

    var new_obj = { usrname: username, psswrd: password };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        type: "POST", url: baseUrl + "Auth/IsValidUser",
        data: JSON.stringify(new_obj),
            //"usrname=" + username +
            //"&psswrd=" + password,
        success: function (res) {
            if (res == "True") {

                var docid = $("#txt_docid").attr('value');
                var doctype = $("#txt_doctype").attr('value');
                var month = $("#txt_month").attr('value');
                var soId = $("#txt_soId").attr('value');
                var year = $("#txt_year").attr('value');


                if (docid != "" && doctype != "") {



                    window.location = baseUrl + "SecurePage/FramedSetted?id=" + docid + "&doctype=" + doctype + "&month=" + month + "&year=" + year + "&soId=" + soId;
                } else {
                    window.location = baseUrl + "SecurePage/FramedSetted";
                }

            } else if (res == "False") {
                // go to error page
                alert("INVALID USERNAME/PASSWORD!");
               // window.location = baseUrl + "Home";
            } else if (res == "Lock") {
                alert("ACCOUNT IS LOCKED!");
                $("#p_acctlocked").show();
                //window.location = baseUrl + "Home";
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}



function requestUnlockAccount() {

    var w = "" +
		"<div id=\"id_bkg\" style=\"opacity: 0.40; filter:alpha(opacity=40);\" class=\"dlg_box_bkg\" onclick=\"javascript:;\"></div>" +
		"<div id=\"id_content\" style=\"border:3px solid #575757;\" class=\"dlg_box_content\">" +
		"<table id=\"tbl_reLogin\" cellspacing=\"0\" cellpadding=\"3\" border=\"0\" style=\"margin:2px;\" >";

    w = w + "" +
        "<tr>" +
            "<td colspan=\"2\" align=\"left\" style=\"background:#256e9d; color:#ffffff; padding:5px;\" >" +
                "<b>Request Unlock Account</b>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "Username" +
            "</td>" +
            "<td>" +
                "<input type=\"text\" id=\"txtUsername\" />" +
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
            "<td>" +
                "Message to Admin" +
            "</td>" +
            "<td>" +
                "<textarea id=\"txt_dtls\" style=\" height:50px \"></textarea>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td colspan=\"2\" align=\"center\" >" +
                "<hr />" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td align=\"center\" colspan=\"2\" >" +
                "<input type=\"button\" value=\"Submit\" onclick=\"javascript:unlockAccount();\" />" +
            "</td>" +
        "</tr>" +
        "";

    w = w + "" +
		"</table></div>" +
		"";

    // append
    $("body").append(w);

    // set position
    var btnY = 40;
    var btnX = 35;

    $("#id_content").css('top', btnX + '' + '%');
    $("#id_content").css('left', btnY + '' + '%');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();

}

function unlockAccount() {
    if ($("#tbl_reLogin tr td input[type=text]:first").attr("value") == "" || $("#tbl_reLogin tr td input[type=password]:last").attr("value") == "") {
        alert("PASSWORD CANNOT BE EMPTY!");
        return;
    }

    var uname = $("#tbl_reLogin tr td input[type=text]:first").attr("value");
    var dtls = $("#txt_dtls").attr("value");
    var password;
    password = $("#tbl_reLogin tr td input[type=password]:first").attr("value");

    $.ajax({
        type: "POST", url: baseUrl + "Auth/sendMailToUnlockAccount",
        data:
            "usrname=" + uname +
            "&psswrd=" + password +
            "&dtls=" + dtls,
        success: function (res) {
            if (res == "False") {
                // go to error page
                alert("INVALID USERNAME/PASSWORD!");

                $("#tbl_reLogin tr td input[type=text]:first").removeAttr("value");
                $("#tbl_reLogin tr td input[type=password]:first").removeAttr("value");

                $("#tbl_reLogin tr td input[type=text]:first").focus();

                $("#spn_fld").text("incorrect password").css("color", "red");
            } else if (res == "Lock") {
                alert("Request Sent! We will notify through mail. ");
                location.reload();
            }
            else if (res == "Unlock") {
                alert("Account is not locked!");
            }
            else
                alert(res);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}
