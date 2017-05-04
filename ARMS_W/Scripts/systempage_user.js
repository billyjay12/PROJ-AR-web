var tbl_users = null;

$(function () {
    tbl_users = $("#tbl_users");
    DisplayPreloader();
    LoadList();
});

function LoadList() {
    var new_obj = {};

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "SystemPage/GetListOfAppUsers",
        success: function (res) {

            if (!res.iserror) {
                DisplayDataF(tbl_users, res.data.sb_list);

                $('#tbl_users').dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers",
                    "bSortClasses": false
                });
            } else {
                /* alert(res.message); if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);*/
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            HidePreloader();
        }
    });
}


function DisplayDataF(tbl_obj, sb) {

    var w = new Array();


    tbl_obj.find("tr:gt(0)").remove();

    tbl_obj.append(sb);

    tbl_obj.find(".cls_reset").click(function () {
        ShowPasswordResetWindow($(this).attr("username"), $(this).attr("userpass"));
    });


    tbl_obj.find(".cls_lock").click(function () {
        accountlock($(this).attr("username"), $(this).attr("errAtmpt"));
    });

    // bind the clicking of links
    tbl_obj.find("a:first").click(function () {
        var username = $(this).attr("username");
        var empIdNo = $(this).attr("empIdNo");

        setTimeout(function () {
            window.location = baseUrl + "UserProfile/Profile?empIdNo=" + empIdNo;
        }, 0);

    });


}

function accountlock(usrname, errAtmpt) {

    var par = {
        username: usrname
            , errAtmpt: errAtmpt
    };

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        type: "POST", url: baseUrl + "SystemPage/UnlockAccount", data: JSON.stringify(par),
        success: function (res) {
            if (!res.iserror) {
                alert("Success!!");
                location.reload();
            } else {
                alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

}

function ShowPasswordResetWindow(username,password) {

    var w = new Array();

    w.push("<div name=\"password_reset_window_bkg\" style=\"position:fixed; top:0; left:0; height:100%; width:100%; background:#111111; opacity: 0.20; filter:alpha(opacity=20); \" ></div>");

    w.push("<div name=\"password_reset_window_content\" style=\"position:fixed; top:0; left:0; height:100%; width:100%; \" >");
    w.push("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" style=\"height:100%; width:100%; \" >");
    w.push("<tr>"); w.push("<td align=\"center\" valign=\"middle\" >");

    w.push("<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\" style=\"border:1px solid #ededed; background:#ffffff; font-family:arial; font-size:11px; \" >");
    w.push("<tr>");
    w.push("<td colspan=\"2\" align=\"center\" style=\"background:#ededed;\" ><b>Reset Password (" + username + ")</b></td>");
    w.push("</tr>");

    w.push("<tr>");
    w.push("<td>Current Password</td>");
    w.push("<td><input type=\"text\"/ id=\"txt_current_pass\" readonly=\"readonly\" style=\"background:#ededed;\" original_password=\"" + password + "\" value=\"" + password + "\" /></td>");
    w.push("</tr>");

    w.push("<tr>");
    w.push("<td>New Password</td>");
    w.push("<td><input type=\"hidden\" id=\"txt_username\" value=\"" + username + "\" > <input type=\"text\"/ id=\"txt_newpassword\" readonly=\"readonly\" style=\"background:#ededed;\" /></td>");
    w.push("</tr>");

    w.push("<tr>");
    w.push("<td align=\"left\"><input type=\"button\" id=\"btn_decrypt_pass\" value=\"Decrypt\" />&nbsp;<input type=\"button\" id=\"btn_generate_pass\" value=\"Generate Pass\" /></td>");
    w.push("<td align=\"right\" ><input type=\"button\"/ value=\"Save\" id=\"btn_save\" />&nbsp;<input type=\"button\"/ value=\"Cancel\"  id=\"btn_cancel\" /></td>");
    w.push("</tr>");

    w.push("</table>");

    w.push("</td>"); w.push("</tr>");
    w.push("</table>");
    w.push("</div>");

    $("body").append(w.join(""));

    $("div[name=password_reset_window_content]").find("table td input[id=\"btn_decrypt_pass\"]").click(function () {
        var txt_current_pass = $("#txt_current_pass");
        var new_obj = { password: txt_current_pass.attr("original_password") };
        $.ajax({
            //dataType: 'json', 
            contentType: 'application/json; charset=utf-8',
            type: "POST", url: baseUrl + "SystemPage/DecryptPassword", data:JSON.stringify(new_obj), //data: "password=" + txt_current_pass.attr("original_password"),
            success: function (res) {
                txt_current_pass.attr("value", res);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });

    });

    $("div[name=password_reset_window_content]").find("table td input[id=\"btn_generate_pass\"]").click(function () {
        var txt_newpassword = $("#txt_newpassword");
        //var txt_newpasswordconfirm = $("#txt_newpasswordconfirm");
        var txt_username = $("#txt_username");

        $.ajax({
            type: "POST", url: baseUrl + "SystemPage/GeneratePassword",
            success: function (res) {
                txt_newpassword.attr("value", res);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });

    });

    // bind
    $("div[name=password_reset_window_content]").find("table td input[id=\"btn_save\"]").click(function () {
        var txt_newpassword = $("#txt_newpassword");
     //   var txt_newpasswordconfirm = $("#txt_newpasswordconfirm");
        var txt_username = $("#txt_username");

        var n_pass = txt_newpassword.attr("value");
        //var n_pass_conf = txt_newpasswordconfirm.attr("value");

        if (n_pass.trim() == "") {
            alert("Please Generate a Password");
            return;
        }

       // if (n_pass != n_pass_conf) {
       //     alert("Password do not match.");
       //     return;
       // }

        var par = {
            username: txt_username.attr("value")
                , password: n_pass
        };

        $.ajax({
            dataType: 'json', contentType: 'application/json; charset=utf-8',
            type: "POST", url: baseUrl + "SystemPage/ResetUserPassword", data: JSON.stringify(par),
            success: function (res) {
                if (!res.iserror) {
                    alert("Successfully Saved!");

                    $("div[name=password_reset_window_content]").hide(function () {
                        $("div[name=password_reset_window_content]").remove();
                        $("div[name=password_reset_window_bkg]").remove();
                    });

                    $("div[name=password_reset_window_bkg]").hide();

                } else {
                    alert(res.message);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });

    });

    $("div[name=password_reset_window_content]").find("table td input[id=btn_cancel]").click(function () {
        $("div[name=password_reset_window_content]").hide("fast", function () {
            $("div[name=password_reset_window_bkg]").hide();

            $("div[name=password_reset_window_content]").remove();
            $("div[name=password_reset_window_bkg]").remove();
        });
    });

}
