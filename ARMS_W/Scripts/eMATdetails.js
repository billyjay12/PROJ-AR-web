function AddProduct(col1, col2, col3, col4, col5, col6) {
    var rowid = $("#table_details tr").length - 1;
    rowid++;
    $("#table_details tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
		    "<td><input style=\"width:98%;\" type=\"text\" value=\"" + col1 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input style=\"width:99%;\" type=\"text\" value=\"" + col2 + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
		    "<td><input type=\"text\" value=\"" + col3 + "\" style=\"width:95%; text-align:right;\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + col4 + "\" style=\"width:95%; text-align:right;\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + col5 + "\" style=\"width:95%; text-align:right;\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + col6 + "\" style=\"width:99%; text-align:right;\" readonly=readonly class=\"readonly_fields\" /></td>" +
		"</tr>"	);

    var qty_total = $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value");
    var orig_qty = col5;

    $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value", parseInt(qty_total) + parseInt(orig_qty));

    var over_total = $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value");
    var partial_total = col6;
    $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value", parseFloat(over_total) + parseFloat(partial_total));

}

function CallRouting(val_action_type, val_emat, val_accCode) {
    /*
        val_mark_type = {'disapprove', 'approve'}
    */
    $.ajax({
        type: "POST", url: baseUrl + "eMAT/CallRouting",
        data:
			"val_action_type=" + val_action_type + "&" +
            "val_emat=" + val_emat + "&" +
            "val_accCode=" + val_accCode ,
        success: function (res) {

           // if (res.substring(0, 3) == "00:") {
                // success
                alert("SUCCESSFULLY SAVED!");
                location.reload();
            //} else {
                // error
              //  alert(res.substring(3, res.length));
          //  }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
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
                w = w + "<option val_itemName=\"" + res_cols[2] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
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
    var btnY = getElLeft(document.getElementById(obj_id_to_position));
    var btnX = getElTop(document.getElementById(obj_id_to_position));
    $("#id_content").css('top', btnX + '' + 'px');
    $("#id_content").css('left', btnY + '' + 'px');

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}

var ns4;

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();

    $("#id_content").remove();
    $("#id_bkg").remove();
}

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));
    if (obj == "txt_itemcode") {
        $("#" + "txt_product").attr("value", $("#id_content select option:selected").attr("val_itemName"));

    }

    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}

function GetId(strVal) {
    //return strVal.substring(0, strVal.indexOf('-') - 1);
    return strVal;
}

function GetValue(strVal) {
    // return strVal.substring(strVal.indexOf('-') + 2, 200);
    return strVal;
}

function AddProductE() {
    // check fields if empty
    /* first field */
    //generateId();
    var val_one = $("#table_details tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value");

    if (val_one == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#table_details tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    var val_three = $("#table_details tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value");

    if (val_three == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } else {
        // check if integer
        if (isNaN(val_three) == true) {
            alert("PRICE MUST BE A NUMBER.");
            return;
        }
    }

    var val_four = $("#table_details tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value");

    if (val_four == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    } else {
        // check if integer
        if (isNaN(val_four) == true) {
            alert("QUANTITY MUST BE A NUMBER.");
            return;
        }
    }

    var rowid = $("#table_details tr").length - 1;
    rowid++;
    $("#table_details tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td ><input style=\"width:120px;\" type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td ><input style=\"width:257px;\" type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
            "<td><input type=\"text\" value=\"" + val_three * val_four + "\" readonly=readonly class=\"readonly_fields\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('table_details'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"
	);


    $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value", parseInt($("#table_details tr:last td:nth-child(2) input[type=text]").attr("value")) + parseInt(val_four));
    $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value", parseInt($("#table_details tr:last td:nth-child(3) input[type=text]").attr("value")) + val_three * val_four);
    
    // clear values
    $("#table_details tr:last").prev().find("td input[type=text]").attr("value", "");
}

function DisableEditing() {
    $("#table_details tr:last").prev().hide();
    $("#table_details tr td img").hide();
    $("#txt_itemcode").attr('onclick', '');
}

function asApproved(val_emat) {
    var encodedBy = "";
    encodedBy = $("#txt_ED_encodedby").attr("value");

    DisplayPreloader();

    $.ajax({
        type: "POST", url: baseUrl + "eMAT/ApprovedEmat",
        data: "" +
        	"val_emat=" + val_emat + "&" +
            "encodedBy=" + encodedBy ,
        success: function (res) {
            
            alert("APPROVED!");
            HidePreloader();
            location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}


function asDispproved(val_emat) {

    var approved = 2;
    var encodedBy = "";
    encodedBy = $("#txt_ED_encodedby").attr("value");

    DisplayPreloader();

    $.ajax({

        type: "POST", url: baseUrl + "eMAT/disApprovedEmat",
        data: "" +
        	"val_emat=" + val_emat + "&" +
            "approved=" + approved + "&" +
            "encodedBy=" + encodedBy ,
        success: function (res) {

            alert("DISAPPROVED!");
            HidePreloader();
            location.reload();

        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
}


function LoadValues(ematno) {
    
    $.ajax({
        dataType: "json",
        type: "POST",
        url: baseUrl + "eMAT/GetDetails",
        data: "ematno=" + ematno,
        success: function (res) {

            $("#txt_ED_encodedby").attr('value', res.encodedBy);
            $("#txt_ED_buyer").attr('value', res.buyer);
            $("#txt_ematdocnum").attr('value', res.ematDoc);
            $("#txt_ED_address").attr('value', res.buyerAdd);
            $("#txt_ED_telno").attr('value', res.telNo);
            $("#txt_ED_contactperson").attr('value', res.contactPerson);
            $("#txt_ED_terms").attr('value', res.terms);
            $("#txt_ED_deliverydate").attr('value', res.deliveryDateFormatted);
            $("#txt_ED_deliveryinstruction").attr('value', res.deliveryInstrct);
            $("#txt_ED_referredto").attr('value', res.acctCode);
            $("#txt_ED_Submittedto").attr('value', res.submttdTo);
            $("#txt_ED_contactnumber").attr('value', res.submttdContactNum);
            $("#txt_ED_confirmeddelivery").attr('value', res.confirmedDelBy);
            $("#txt_ED_tradesales_rep").attr('value', res.tradeSalesRep);

            $("#doc_stat_msg").html(res.DocStatusMsg);

            for (var i = 0; i < res.list_of_items.length; i++) {
                var cols = res.list_of_items[i].split("|");

                AddProduct(cols[0], cols[1], cols[2], cols[3], cols[4], cols[5]);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });

}