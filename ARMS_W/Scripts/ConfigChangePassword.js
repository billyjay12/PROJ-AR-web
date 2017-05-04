

function SavePassword() {
    
    var password_1 = $("#txt_password_first").attr("value");
    var password_2 = $("#txt_password_second").attr("value");
    if (password_1 == "" || password_2 == "") {
        alert("PASSWORD CANNOT BE EMPTY/BLANK");
        return;
    }

    if (password_1 != password_2) {
        alert("PASSWORD DOES NOT MATCH");
        return;
    }

    DisplayPreloader();

    $.ajax({
        type: "POST", url: baseUrl + "Config/UpdateUserPassword",
        data: "NewPassword=" + password_1,
        success: function (res) {
            if (SrvResultMsg.GetMsgType(res) != "error") {
                alert("PASSWORD UPDATED SUCCESSFULLY!");
                location.reload();
            } else {
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });

}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function IsError(strMsg) {
    if (strMsg.substr(0, 2) != "00:") {
        return "false";
    } else {
        return "true";
    }
}