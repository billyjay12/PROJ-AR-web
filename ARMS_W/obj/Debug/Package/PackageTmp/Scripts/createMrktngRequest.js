var ns4;

function DelCurrRow(tbl_id, r_id) {
    
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
}

function SaveDoc() {
    if (CheckRequireFields_two() == false) { return; }

    DisplayPreloader();

    var encodedBy = "";
        encodedBy = $("#txt_encoded_by").attr("value");

    var acctCode = "";
    acctCode = $("#txt_acct_code").attr("value");

    var acctName = "";
    acctName = $("#txt_acct_name").attr("value");

    var acctAddress = "";
    acctAddress = $("#txt_address").attr("value")

    var area = "";
    area = $("#txt_area").attr("value");

    var acctOffcr = "";
    acctOffcr = $("#txt_acct_officer").attr("value");

    var reqstdBy="";
    reqstdBy = $("#txt_request_by").attr("value");

    var brand="";
    brand = $("#txt_brand").attr("value");

    var category = "";
    category = $("#txt_category").attr("value");

    var type = "";
    type = $("#txt_type").attr("value");

    var setUpDate = "";
    setUpDate = $("#txt_setup_date").attr("value");

    var size = "";
    size = $("#txt_size").attr("value");

    var value = "";
    value = $("#txt_value").attr("value");

    var deploymentDate = "";
    deploymentDate = $("#txt_actual_deployDate").attr("value");

    var actualDeployDate = "";
    actualDeployDate = $("#txt_actual_deployDate").attr("value");

    // [other stipulation]
    var otherStipulation = "";
    row_count = $("#tbl_other_stipulation tr").length - 1;
    
    for (i = 2; i <= row_count; i++) {
        if (otherStipulation != "") {
            otherStipulation = otherStipulation + "$";
        }
        // stipulation
        otherStipulation = otherStipulation + $("#tbl_other_stipulation tr:nth-child(" + i + ") td:nth-child(1) input").attr('value');
    }

    //Attachments
    var attchmnts = "";
    var attchmnts1 = "";
    var attchmnts2 = "";
    var attchmnts3 = "";
   //attchmnts = $("#txt_attchmnt_1" + "$").attr("value") + $("#txt_attchmnt_2" + "$").attr("value") + $("#txt_attchmnt_3" + "$").attr("value");
    attchmnts1 = $("#txt_attchmnt1").attr("value");
    attchmnts2 = $("#txt_attchmnt2").attr("value");
    attchmnts3 = $("#txt_attchmnt3").attr("value");
    
    $.ajax({
        type: "POST", url: baseUrl + "MrktngRequest/insertMrktngRequest",
        data: "" +
            "encodedBy=" + encodedBy + "&" +
            "acctCode=" + acctCode + "&" +
            "acctName=" + acctName + "&" +
            "acctAdd=" + acctAddress + "&" +
            "acctArea=" + area + "&" +
            "acctOfficer=" + acctOffcr + "&" +
            "requestedBy=" + reqstdBy + "&" +
            "brand=" + brand + "&" +
            "category=" + category + "&" +
            "setUpDate=" + setUpDate + "&" +
            "size=" + size + "&" +
            "value=" + value + "&" +
            "availDeploy=" + deploymentDate + "&" +
            "actualDeploy=" + actualDeployDate + "&" +
            "otherStipulation=" + otherStipulation + "&" +
            "attchmnts1=" + attchmnts1 + "&" +
            "attchmnts2=" + attchmnts2 + "&" +
            "attchmnts3=" + attchmnts3 
        ,
        success: function (res) {
            if (SrvResultMsg.GetMsgType != "error") {
                alert("SUCCESSFULLY SAVED!");
            } else {
                alert(SrvResultMsg.GetMessage);
            }
            HidePreloader();
        },

        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError);  }
    });
}


function AddStipulation() {

    // check fields if empty  
    /* first field */
    var val_one = $("#tbl_other_stipulation tr:last td:nth-child(1) input[type=text]").attr("value");    
    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }
    
    // get last id
    var last_id = $("#tbl_other_stipulation tr:last").prev().attr("RowId");
    var new_id = 0;
    if (last_id != undefined) {
        new_id = last_id;
    }

    var rowid = $("#tbl_other_stipulation tr").length - 2;
    rowid++;
    $("#tbl_other_stipulation tr:last").prev().after(
		"<tr RowId=\"" + (parseInt(rowid) + parseInt(new_id)) + "\">" +
			"<td><input style=\"width:250px;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\"/></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_other_stipulation'," + (parseInt(rowid) + parseInt(new_id)) + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);

    // clear values
    $("#tbl_other_stipulation tr:last td:nth-child(1) input[type=text]").attr("value", "");
}

function cancel() { 

  var okToRefresh = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    
     if (okToRefresh) {
          //			setTimeout("location.reload(true);",1500);
        location.reload();
        }
}

function CreateUploadingBox(obj_id_to_position) {
	var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<!-- <a href=\"\"><img src=\"" + baseUrl + "Images/cancel.png\" style=\"border:0;\" /></a><br /> -->" +
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "Uploading/UploadFileMarketing\" width=\"330px\" height=\"76px\">" +
		"<p>Your browser does not support iframes.</p>" +
		"</iframe>" +
		"<br /><input type=\"button\" value=\"Close\" onclick=\"SaveToTextBox('" + obj_id_to_position + "');\" />" +
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

function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();

    /* for browsing data for a certain field */
    /* should only display two column */

}

function StrResultTags(str_res) {
    return str_res.substr(3, str_res.length - 3);
}

function LookUpData(obj_id_to_store, str_data) {
    DisplayPreloader();
    
    $.ajax({
        type: "POST", url: baseUrl + "MrktngRequest/GetFilteredList",
        data: "_str_data=" + str_data,
        success: function (res) {            
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }

            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
        // show dialog box/window
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
       
        if (obj_id_to_position == "txt_brand") { 
            w = w + "<option val_brandName=\"" + res_cols[1] + "\" value=\"" + res_cols[1] + "\">" + res_cols[1] + "</option>";
        }
        if (obj_id_to_position == "txt_request_by") {
            w = w + "<option val_soName=\"" + res_cols[1] + "\" value=\"" + res_cols[1] + "\">" + res_cols[1] + "</option>";
        }
        if (obj_id_to_position == "txt_acct_code") {
            // w = w + "<option val_soname=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
            w = w + "<option value1=\"" + res_cols[0] + "\" value2=\"" + res_cols[1] + "\" value3=\"" + res_cols[2] + "\" value4=\"" + res_cols[3] + "\" value5=\"" + res_cols[5] + "\" >" + res_cols[1] + "</option>";
        }
        if (obj_id_to_position == "txt_category") {
            w = w + "<option val_brandName=\"" + res_cols[0] + "\" value=\"" + res_cols[0] + "\">" + res_cols[0] + "</option>";
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
    // var btnY = getElLeft(document.getElementById(obj_id_to_position));
    // var btnX = getElTop(document.getElementById(obj_id_to_position));
    var btnY = getElLeft($("#" + obj_id_to_position)[0]);
    var btnX = getElTop($("#" + obj_id_to_position)[0]);
   
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');
  
    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();

}

function SetValueFromSelect(obj) {

    if (obj == "txt_acct_code") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value1'));
        $("#txt_acct_name").attr("value", $("#id_content select option:selected").attr('value2'));
        $("#txt_acct_officer").attr("value", $("#id_content select option:selected").attr('value3'));
        $("#txt_address").attr("value", $("#id_content select option:selected").attr('value5'));
        $("#txt_area").attr("value", $("#id_content select option:selected").attr('value4'));
    }

    if (obj == "txt_brand") {
       $("#" + obj).attr("value", $("#id_content select option:selected").text());
    }
    if (obj == "txt_request_by") {
       $("#" + obj).attr("value", $("#id_content select option:selected").text());
    }
    if (obj == "txt_category") {
        $("#" + obj).attr("value", $("#id_content select option:selected").text());

        var category = $("#txt_category").attr("value");

        if (category != "billboard & streamer") {
            if (category != "racks") {
                $("#txt_size").attr('disabled', 'disabled');
            } else {
                $("#txt_size").removeAttr('disabled');
            }
        } else {
            $("#txt_size").removeAttr('disabled');
        }
    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}


function DeleteFileAttachment(attch_type) {

    var mrktReqNo = "";
    var filename = "";
    var obj = "";

    if (attch_type == "A1") { obj = "txt_attchmnt1"; }
    if (attch_type == "A2") { obj = "txt_attchmnt2"; }
    if (attch_type == "A3") { obj = "txt_attchmnt3"; }   

    // get ccaNum
    if ($("#txt_mrktngReqNo").length > 0) {
        mrktReqNo = $("#txt_mrktngReqNo").attr('value');
    }

    // get filename
    filename = $("#" + obj).attr('value');

    if (filename != "") {
        $.ajax({
            type: "POST", url: baseUrl + "SQL/DeleteFileAttachment",
            data:
			"attachment_type=" + attch_type + "&" +
            "acct_ccanum=" + mrktReqNo + "&" +
            "filename=" + filename 
			,
            success: function (res) {

                if (SrvResultMsg.GetMsgType(res) != "error") {
                    // clear the value in textbox
                    $("#" + obj).attr('value', '');

                    // success
                    alert("ATTACHMENT IS REMOVED");
                } else {
                    // error
                    alert(SrvResultMsg.GetMessage(res));
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });
    }
}

function SaveMKRequestDoc() {
    if ($("#txt_size").attr("value") == "") {
        DocSave();
    }
    else {
        SaveDoc();
    }
}

function DocSave() {
    if (CheckRequireFields_one() == false) {
        return;
    }

    DisplayPreloader();

    var encodedBy = "";
    encodedBy = $("#txt_encoded_by").attr("value");
    
    var acctCode = "";
    acctCode = $("#txt_acct_code").attr("value");

    var acctName = "";
    acctName = $("#txt_acct_name").attr("value");

    var acctAddress = "";
    acctAddress = $("#txt_address").attr("value")

    var area = "";
    area = $("#txt_area").attr("value");

    var acctOffcr = "";
    acctOffcr = $("#txt_acct_officer").attr("value");

    var reqstdBy = "";
    reqstdBy = $("#txt_request_by").attr("value");

    var brand = "";
    brand = $("#txt_brand").attr("value");

    var category = "";
    category = $("#txt_category").attr("value");

    var type = "";
    type = $("#txt_type").attr("value");

    var setUpDate = "";
    setUpDate = $("#txt_setup_date").attr("value");

    var size = "";
    size = $("#txt_size").attr("value");

    var value = "";
    value = $("#txt_value").attr("value");

    if (value == "") {
        alert("'VALUE' FIELD CANNOT BE EMPTY!");
        return;
    }

    var deploymentDate = "";
    deploymentDate = $("#txt_avail_deployOn").attr("value");

    var actualDeployDate = "";
    actualDeployDate = $("#txt_actual_deployDate").attr("value");

    // [other stipulation]
    var otherStipulation = "";
    row_count = $("#tbl_other_stipulation tr").length - 1;
    
    for (i = 2; i <= row_count; i++) {
        if (otherStipulation != "") {
            otherStipulation = otherStipulation + "$";
        }
        // stipulation
        otherStipulation = otherStipulation + $("#tbl_other_stipulation tr:nth-child(" + i + ") td:nth-child(1) input").attr('value');
    }

    //SAVING ATTACHMENT....
    // Saving attachment

    var Attachment = "";
    row_count = $("#tbl_attachment tr").length - 1;
    //alert(row_count);
    for (i = 2; i <= row_count; i++) {
        if (Attachment != "") {
            Attachment = Attachment + "$";
        }
        // first column
        Attachment = Attachment + $("#tbl_attachment tr:nth-child(" + i + ") td:nth-child(1) input").attr('value');

    }





 /**   //Attachments
    var attchmnts = "";
    var attchmnts1 = "";
    var attchmnts2 = "";
    var attchmnts3 = "";
    
    attchmnts1 = $("#txt_attchmnt1").attr("value");
    attchmnts2 = $("#txt_attchmnt2").attr("value");
    attchmnts3 = $("#txt_attchmnt3").attr("value");**/
    
    $.ajax({
        type: "POST", url: baseUrl + "MrktngRequest/insertMrktngRequest",
        data: "" +
            "encodedBy=" + encodedBy + "&" +
            "acctCode=" + acctCode + "&" +
            "acctName=" + acctName + "&" +
            "acctAdd=" + acctAddress + "&" +
            "acctArea=" + area + "&" +
            "acctOfficer=" + acctOffcr + "&" +
            "requestedBy=" + reqstdBy + "&" +
            "brand=" + brand + "&" +
            "category=" + category + "&" +
            //"type=" + type + "&" +
            "setUpDate=" + setUpDate + "&" +
            "size=" + size + "&" +
            "value=" + value + "&" +
            "availDeploy=" + deploymentDate + "&" +
            "actualDeploy=" + actualDeployDate + "&" +
            "otherStipulation=" + otherStipulation + "&" +
            "Attachment=" + Attachment +
            ""
        ,

        success: function (res) {

            if (SrvResultMsg.GetMsgType != "error") {
                alert("SUCCESSFULLY SAVED!");
                location.reload();
            } else {
                alert(SrvResultMsg.GetMessage);
            }
            HidePreloader();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); HidePreloader(); }
    });
}


function CheckRequireFields_one() {
    var lacking_fields = "";

    if ($("#txt_encoded_by").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Encoded By"; } else { lacking_fields = "Encoded By"; }
    }

    if ($("#txt_acct_code").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    if ($("#txt_acct_name").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Name"; } else { lacking_fields = "Account Name"; }
    }

    if ($("#txt_address").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Address"; } else { lacking_fields = "Address"; }
    }

    if ($("#txt_area").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Area"; } else { lacking_fields = "Area"; }
    }

    if ($("#txt_acct_officer").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Officer"; } else { lacking_fields = "Account Officer"; }
    }

    if ($("#txt_request_by").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Requested By"; } else { lacking_fields = "Requested By"; }
    }

    if ($("#txt_brand").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Brand"; } else { lacking_fields = "Brand"; }
    }

    if ($("#txt_category").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Category"; } else { lacking_fields = "Category"; }
    }

    if ($("#txt_type").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Type"; } else { lacking_fields = "Type"; }
    }

    if ($("#txt_setup_date").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Set-up Date"; } else { lacking_fields = "Set-up Date"; }
    }

    // if ($("#txt_size").attr("value") == "") {
    //     if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Size"; } else { lacking_fields = "Size"; }
    // }

    if ($("#txt_value").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Value"; } else { lacking_fields = "Value"; }
    }

    if ($("#txt_avail_deployOn").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Available for Deployment"; } else { lacking_fields = "Available for Deployment"; }
    }

    if ($("#txt_actual_deployDate").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Deployment Date"; } else { lacking_fields = "Deployment Date"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}

function CheckRequireFields_two() {
    var lacking_fields = "";

    if ($("#txt_encoded_by").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Encoded By"; } else { lacking_fields = "Encoded By"; }
    }

    if ($("#txt_acct_code").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Code"; } else { lacking_fields = "Account Code"; }
    }

    if ($("#txt_acct_name").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Name"; } else { lacking_fields = "Account Name"; }
    }

    if ($("#txt_address").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Address"; } else { lacking_fields = "Address"; }
    }

    if ($("#txt_area").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Area"; } else { lacking_fields = "Area"; }
    }

    if ($("#txt_acct_officer").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Account Officer"; } else { lacking_fields = "Account Officer"; }
    }

    if ($("#txt_request_by").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Requested By"; } else { lacking_fields = "Requested By"; }
    }

    if ($("#txt_brand").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Brand"; } else { lacking_fields = "Brand"; }
    }

    if ($("#txt_category").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Category"; } else { lacking_fields = "Category"; }
    }

    if ($("#txt_type").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Type"; } else { lacking_fields = "Type"; }
    }

    if ($("#txt_setup_date").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Set-up Date"; } else { lacking_fields = "Set-up Date"; }
    }

    if ($("#txt_size").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Size"; } else { lacking_fields = "Size"; }
    }

    if ($("#txt_value").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Value"; } else { lacking_fields = "Value"; }
    }

    if ($("#txt_avail_deployOn").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Available for Deployment"; } else { lacking_fields = "Available for Deployment"; }
    }

    if ($("#txt_actual_deployDate").attr("value") == "") {
        if (lacking_fields != "") { lacking_fields = lacking_fields + "\n" + "Deployment Date"; } else { lacking_fields = "Deployment Date"; }
    }

    if (lacking_fields != "") {
        alert("PLEASE FILL IN THE FF. FIELDS: \n" + lacking_fields);
        return false;
    }

    return true;
}




function AddAttachment() {

    var Attachment_one = $("#tbl_attachment tr:last td:nth-child(1) input[type=text]").attr("value");

    if (Attachment_one == "") {
        alert("FILE FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var Attachment_two = $("#tbl_attachment tr:last td:nth-child(2) input[type=text]").attr("value");
    if (Attachment_two == "") {
        alert("BRIEF DESCRIPTION FIELD CANNOT BE EMPTY!");
        return;
    }



    var rowid = $("#tbl_attachment tr").length - 1;
    rowid++;
    $("#tbl_attachment tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + Attachment_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_attachment'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"



	)






    // clear values

    $("#tbl_attachment tr:last td:nth-child(1) input[type=text]").attr("value", "");
   


}