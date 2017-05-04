var ns4;

function SetValueFromSelect(obj) {
    if (obj == "txt_acctCode") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
        $("#txt_acctName").attr("value", $("#id_content select option:selected").text());
       
        GetExtraDatas("ListOfSOASMCHM", $("#id_content select option:selected").attr('value'), obj, "txt_acctOfficer", "txt_salesManager", "txt_channelManager");
    }


    if (obj == "txtchannel") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
        $("#txtchannel").attr("value", $("#id_content select option:selected").text());

    }


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
    var ar_length = res_rows.length;
    for (i = 0; i < ar_length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                w = w + "<option val_CHM=\"" + res_cols[4] + "\" val_ASM=\"" + res_cols[3] + "\" val_SOName=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
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
    return strVal.substring(0, strVal.indexOf('-') - 1);
//    return strVal
}

function GetValue(strVal) {
  return strVal.substring(strVal.indexOf('-') + 2, 200);
//    return strVal;
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function LookUpData(obj_id_to_store, str_data) {
    var channel = $("#txtchannel").attr("value");
  if (obj_id_to_store == "txt_acctCode" && channel == "") {
      alert("PLEASE SELECT CHANNEL FIRST!");
      return;
     }
    DisplayPreloader();
        $.ajax({
        type: "POST", url: baseUrl + "SQL/GetAcctDetail",
        data: "_str_data=" + str_data + "&" +
              "channel=" + channel,
        success: function (res) {
            if (IsError(res)) {
                
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader();}
    });

    // show dialog box/window
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

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();
}

function Cancel() {
    var isCancelled = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (isCancelled) {
        window.history.back()
    }
}

function SetUpBusRevDoc() {
  
    var ccaNum = $("ccaNum")
    var status = $("status")
    var encoded_by =  $("#txt_encoded_by").attr("value");

    var date = "";
    date = $("#txt_br_date").attr("value");

    if (date == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [BUSINESS REVIEW DATE] FIELD.");
        return;
    }

    var acctCode = "";
    acctCode = $("#txt_acctCode").attr("value");

    if (acctCode == "") {
        alert("PLEASE FILL IN ALL REQUIRED FIELDS: [ACCOUNT CODE] FIELD.");
        return;
    }

    DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "BusinessReview/SetUpBusRevSchedule",
        data: "" +
            "status=" + status + "&" +
            "ccaNum=" + ccaNum + "&" +
            "date=" + date + "&" +
            "txt_encoded_by=" + encoded_by + "&" +
            "acctCode=" + acctCode +
            "",
        success: function (res) {
            alert("SUCCESSFULLY SAVED!");
            window.location = baseUrl + "BusinessReview/BusinessReviewList";
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });
}

function LookUpLocalData(obj_id_to_store) {
    CreateDialogBox(obj_id_to_store, AccountCodes);
}

function CreateLocalDialogBox(obj_id_to_position, data_to_add) {
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
             
                w = w + "<option val_CHM=\"" + res_cols[4] + "\" val_ASM=\"" + res_cols[3] + "\" val_SOName=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
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

function GetExtraDatas(val_lookup_type, val_par1, val_main_obj, val_obj1, val_obj2, val_obj3, val_obj4) {
    var ajx_data = "";
   
    if (val_main_obj == "txt_acct_code" || val_main_obj == "txt_acctCode") {
        ajx_data = "_str_data=" + val_lookup_type + "&par1=" + val_par1;
    } else {
        ajx_data = "_str_data=" + val_lookup_type;
    }

    $.ajax({
        type: "POST", url: "../SQL/GetList",
        data: ajx_data,
        success: function (res) {
            if (IsError(res)) {
                // StrResultTags(res);
                var data_to_add = StrResultTags(res);
                var res_rows = data_to_add.split("#$");
                for (i = 0; i < res_rows.length; i++) {
                    var res_cols = res_rows[i].split("|");
                    if (res_cols[0] != null) {
                        if (res_cols[0] != "") {
                            // 
                           
                            if (val_obj1 != "" && val_obj1 != undefined) {
                                $("#" + val_obj1).attr("value", res_cols[0]);
                            }

                            if (val_obj2 != "" && val_obj2 != undefined) {
                                $("#" + val_obj2).attr("value", res_cols[1]);
                            }

                            if (val_obj3 != "" && val_obj3 != undefined) {
                                $("#" + val_obj3).attr("value", res_cols[2]);
                            }

                            if (val_obj4 != "" && val_obj4 != undefined) {
                                $("#" + val_obj4).attr("value", res_cols[3]);
                            }
                        }
                    }
                }
            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}
