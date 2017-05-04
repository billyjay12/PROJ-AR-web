var ns4;

function LookUpData(obj_id_to_store, str_data, par1) {
    DisplayPreloader();

    if (par1 == undefined) {
        par1 = "";
    }

    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data + "&par1=" + par1,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
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

function CreateDialogBox(obj_id_to_position, data_to_add) {

    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"right\" >" +
		"<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<option emailadd=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
            }
        }
    }

    w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

    // append
    // $("body").after(w);
    $("body").append(w);

    // set position
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function SetValueFromSelect(obj) {
    $("#" + obj).attr("value", $("#id_content select option:selected").text());
    $("#" + obj).attr("value_id", $("#id_content select option:selected").attr('value'));

    if (obj == "txt_username") {
        $("#txt_email").attr('value', $("#id_content select option:selected").attr('emailadd'));
    }

    if (obj == "txt_roleid") {
        PTextBoxes($("#" + obj).attr("value"));
    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function PTextBoxes(roleid) { 
    
    // REGION
    if( 
        roleid == "0" ||
        roleid == "1" ||
        roleid == "3" ||
        roleid == "4" 
    ){
        // disable
        $("#txt_channel").attr("disabled", "disabled");
        $("#txt_area").attr("disabled", "disabled");
        $("#txt_slpcode").attr("disabled", "disabled");

        $("#txt_channel").attr("value");
        $("#txt_area").attr("value");
        $("#txt_slpcode").attr("value");

        // enable
        $("#txt_region").removeAttr("disabled");
    }

    // CHANNEL
    if (roleid == "8" || roleid == "51" || roleid == "53") {
        
        // disable
        $("#txt_area").attr("disabled", "disabled");
        $("#txt_slpcode").attr("disabled", "disabled");

        $("#txt_area").attr("value");
        $("#txt_slpcode").attr("value");

        // enable
        $("#txt_region").removeAttr("disabled");
        $("#txt_channel").removeAttr("disabled");
    }

    // AREA
    if (roleid == "2") {

        // disable
        $("#txt_channel").attr("disabled", "disabled");
        $("#txt_slpcode").attr("disabled", "disabled");

        $("#txt_channel").attr("value");
        $("#txt_slpcode").attr("value");

        // enable
        $("#txt_region").removeAttr("disabled");
        $("#txt_area").removeAttr("disabled");
    }

    // AREA
    if (roleid == "17") {

        // disable
        $("#txt_channel").attr("disabled", "disabled");

        $("#txt_channel").attr("value");

        // enable
        $("#txt_region").removeAttr("disabled");
        $("#txt_area").removeAttr("disabled");
        $("#txt_slpcode").removeAttr("disabled");
    }

}

function DeleteUserRole(desigig, username) {
    

    $.ajax({
        type: "POST", url: baseUrl + "Config/DeleteUserRole",
        data:"desigid=" + desigig ,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // refresh
                location.reload();
            } else {
                // error
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader();
        }
    });
}

function AddNeWuserRole() {

    var roleid;
    roleid = $("#txt_roleid").attr("value_id");

    var username;
    username = $("#txt_username").attr("value");

    var email;
    email = $("#txt_email").attr("value");

    var region;
    region = $("#txt_region").attr("value");

    var brand;
    brand = $("#txt_brand").attr("value");

    var channel;
    channel = $("#txt_channel").attr("value");

    var area;
    area = $("#txt_area").attr("value");

    var slpcode;
    slpcode = $("#txt_slpcode").attr("value_id");
    if ($("#txt_slpcode").attr("value_id") == undefined) slpcode = "";

    $.ajax({
        type: "POST", url: baseUrl + "Config/AddUserRole",
        data:
			"roleid=" + roleid + "&" +
			"username=" + username + "&" +
            "email=" + email + "&" +
            "region=" + region + "&" +
            "brand=" + brand + "&" +
            "channel=" + channel + "&" +
            "area=" + area + "&" +
            "slpcode=" + slpcode + ""
			,
        success: function (res) {

            if (SrvResultMsg.GetMsgType(res) != "error") {
                // success
                alert("SUCCESSFULLY SAVED!");

                // refresh
                location.reload();
            } else {
                // error
                alert(SrvResultMsg.GetMessage(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError); HidePreloader(); 
        }
    });
}

function ShowAddDialog() {
    $("#div_add_role").show('fast');
}

function HideAddDialog() {
    $("#div_add_role").hide('fast');
}

function SearchUsername() {

    var keyword = $("#txt_search_uname").attr("value");

    window.location = baseUrl + "Config/UserRole?username=" + keyword;
}