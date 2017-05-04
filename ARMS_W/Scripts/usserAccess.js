var ns4;

function SetValueFromSelect(obj) {
    //    alert(obj);
        $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
        $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));
    if (obj == "txt_idNo") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
        $("#txt_lname").attr("value", $("#id_content select option:selected").text());
        $("#txt_fname").attr("value", $("#id_content select option:selected").attr('val_fname'));

    }
    if (obj == "txt_Username") {

        $("#txt_roleID").attr("value", $("#id_content select option:selected").attr('val_username'));

    }

//    if (obj == "txt_module") {

//        $("#txt_module").attr("value", $("#id_content select option:selected").attr('val_module'));

//       
//    }



    $("#id_content").hide("fast");
    $("#id_bkg").hide();

}
function CreateDialogBox(obj_id_to_position, data_to_add) {

    var w = "" +
		"<div id=\"id_bkg\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content\" class=\"dlg_box_content\">" +
		"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">" +
		"<tr><td align=\"right\" >" +
		"<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
   //alert(data_to_add);
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                //alert(res_cols);


                if (obj_id_to_position == "txt_Username") {
                    w = w + "<option val_username=\"" + res_cols[1] + "\" value=\"" + res_cols[0] + "\">" + res_cols[0] + "</option>";
                }

                if (obj_id_to_position == "txt_module") {
                    w = w + "<option val_module=\"" + res_cols[0] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
                }

            }
        }
    }

    w = w + "" +
		"\n</select>" +
		"<br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
		"</td></tr></table></div>" +
		"";

    // append
    $("body").after(w);

    // set position
    var btnY = getElLeft($("#" + obj_id_to_position)[0]);
    var btnX = getElTop($("#" + obj_id_to_position)[0]);
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');


    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

function GetId(strVal) {
    //    alert(strVal);
    //return strVal.substring(0, strVal.indexOf('-') - 1);
      return strVal;
}

function GetValue(strVal) {
    //return strVal.substring(strVal.indexOf('-') + 2, 200);
    return strVal;
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function IsError(strMsg) {
    if (strMsg.substr(0, 2) != "00:") {
        return "false";
    } else {
        return "true";
    }
}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function LookUpData(obj_id_to_store, str_data) {
    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {

                CreateDialogBox(obj_id_to_store, StrResultTags(res));

            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });

    // show dialog box/window
}


function AddUserAccess() {

    var roleID = "";
    roleID = $("#txt_roleID").attr("value");

    if (roleID == "") {
        alert("ROLE ID FIELD CANNOT BE EMPTY!");
        return;
    }

    var UserName = "";
    UserName = $("#txt_Username").attr("value");

    if (UserName == "") {
        alert("USERNAME FIELD CANNOT BE EMPTY!");
        return;
    }

    var docType = "";
    docType = $("#txt_module").attr("value");

    if (docType == "") {
        alert("MODULE FIELD CANNOT BE EMPTY!");
        return;
    }

    var accessRights = "";
    accessRights = $("#txt_Acess").attr("value");

    if (accessRights == "Select Access Rights") {
        alert("ACCESS FIELD CANNOT BE EMPTY!");
        return;
    }



    $.ajax({
        type: "POST", url: baseUrl + "UseAcess/USERacess",
        data: "" +

            "roleID=" + roleID + "&" +
           "docType=" + docType + "&" +
            "accessRights=" + accessRights +
            ""
        ,
        success: function (res) {

            alert("SUCCESSFULLY SAVED!");
            location.reload();

        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });


}



function DocSave() {

    AddUserAccess();


}


function cancel() {

    location.reload();

}