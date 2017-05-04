var ns4;

//$(document).ready(function () {

//    $("#txt_Amount").keypress(
//        function (e) {
//            if (e.which == 13) {
//                var amount = $("#txt_Amount").attr("value");
//                amount = addNewComma(amount);
//                $("#txt_Amount").attr("value", amount);

//            }

//        }
//    );

//});



function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));


    if (obj == "txt_targetChan") {

        getListChannel();

    }

    if (obj == "txt_targetArea") {
        getListArea(); 

    }


    if (obj == "txt_targetAccounts") {
        getListAccount();

    }


    if (obj == "txt_brand") {
        getListBrand();

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







function LookUpData(obj_id_to_store, str_data) {
   // DisplayPreloader();


    //spliting marketing program for query use


    var par2 = $("#txt_targetChan").attr("value");
    var param = par2.split(",");

    $.each(param, function (index, value) {
        //alert(index + ': ' + value);

        param[index] = "'" + value + "'";

    });

    var array1 = param.join(",");




    //spilting target area for query use

    //    var values_targetArea = $("#txt_targetArea").attr("value");
    //    var splitval_targetArea = values_targetArea.split(",");

    //    $.each(splitval_targetArea, function (index, value) {
    //        //alert(index + ': ' + value);

    //        splitval_targetArea[index] = "'" + value + "'";

    //    });

    //    var tArea = splitval_targetArea.join(",");



    var par1 = "";



    //    //if (str_data == par2) {

    //    if (str_data == "TargetArea") {

    //        par1 = $("#txt_targetChan").attr("value");

    //    }


    //    if (str_data == "TargetAccount") {

    //        par1 = $("#txt_targetArea").attr("value");

    //    }



    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data + "&" +
            "par1=" + par1 + " &" +
            "array1=" + array1 //+ "&" +
        //"tArea=" + tArea
        ,
        success: function (res) {

            //alert(res);

            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }
           // HidePreloader();
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
		"<tr><td align=\"left\" ><div id=\"mydiv\" style=\"overflow:auto; height:100px; width: 400px;\">";
    //"<select style=\"width:200px; font-family:arial; font-size:11px;\">\n";

    var res_rows = data_to_add.split("#$");
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                //w = w + "<option val_area=\"" + res_cols[2] + "\" val_region=\"" + res_cols[3] + "\" value=\"" + res_cols[0] + "\"><input type=\"checkbox\" value=\"2\">" + res_cols[1] + "</option>";
                w = w + "<input type=\"checkbox\" value=\"" + res_cols[1] + "\"/> " + res_cols[1] + " <br />";

            }
        }
    }

    w = w + "" +
    //"\n</select>" +
		"</div><br /> <input onclick=\"javascript:SetValueFromSelect('" + obj_id_to_position + "');\" type=\"button\" value=\"Select\">" +
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

    if (obj_id_to_position == "txt_targetChan") {

        $("#mydiv input[type=checkbox]").change(
        function () {

            // check if any item is/are selected
            

            // if all channel is checked
            if ($("#mydiv input[type=checkbox][value='ALL CHANNELS']").attr('checked') == true) {
                $("#mydiv input[type=checkbox]").attr('disabled', 'disabled');
                $("#mydiv input[type=checkbox][value='ALL CHANNELS']").removeAttr('disabled');
            } else {
                $("#mydiv input[type=checkbox][value='ALL CHANNELS']").attr('disabled', 'disabled');
            }

            if ($("#mydiv input[type=checkbox]:checked").length == 0) {
                $("#mydiv input[type=checkbox]").removeAttr('disabled');
            }

        }
    );

    }


    if (obj_id_to_position == "txt_brand") {

        $("#mydiv input[type=checkbox]").change(
        function () {

            // check if any item is/are selected


            // if all channel is checked
            if ($("#mydiv input[type=checkbox][value='ALL BRANDS']").attr('checked') == true) {
                $("#mydiv input[type=checkbox]").attr('disabled', 'disabled');
                $("#mydiv input[type=checkbox][value='ALL BRANDS']").removeAttr('disabled');
            } else {
                $("#mydiv input[type=checkbox][value='ALL BRANDS']").attr('disabled', 'disabled');
            }

            if ($("#mydiv input[type=checkbox]:checked").length == 0) {
                $("#mydiv input[type=checkbox]").removeAttr('disabled');
            }

        }
    );

    }


    

    // show 
    $("#id_content").show("fast");
    $("#id_bkg").show();
}


//function addURL() {

//    $("#" + tbl_id + " tr[RowId=" + r_id + "]").append("<a href=\""+baseUrl+ SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_ITR + "&id=" + m_acct_ccanum + "\"><img src=\"<%=ResolveUrl("~/") %>Images/page__get.png\" style=\"border:0;\" /></a>");

//}



function DelCurrRow(tbl_id, r_id) {

    var over_total = $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value");
    var partial_total = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(3) input[type=text]").attr("value");
    //    $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", parseInt(over_total) - parseInt(partial_total));

    var newTotal = stripNonNumeric(over_total);
    var new_partial_total = stripNonNumeric(partial_total);
    var result = parseFloat(newTotal) - parseFloat(new_partial_total);
    result = addNewComma(result);
    $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", result);


//    var targetacct_total = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
//    var target_acctindex = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(4) input[type=text]").attr("value");
////    var newvalue = targetacct_total - target_acctindex;
////    var newcastedvalue = newvalue.toString();
//    
//   $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value",parseInt(targetacct_total) - parseInt(target_acctindex));
   // $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", newcastedvalue);




    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
}



function AcctDelCurrRows(tbl_id, r_id) {


    var targetacct_total = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
    var target_acctindex = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(4) input[type=text]").attr("value");
    //    var newvalue = targetacct_total - target_acctindex;
    //    var newcastedvalue = newvalue.toString();

    $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", ((parseInt(targetacct_total) - parseInt(target_acctindex)).toFixed(2)).toString());
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();
}

/*-------------------------ADDING RESOURCES--------------------------------**/


function AddResources() {

    var val_one = $("#Tbl_resources tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value");

    if (val_one == "") {
        alert("ITEM FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var val_two = $("#Tbl_resources tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value");
    if (val_two == "") {
        alert("DESCRIPTION FIELD CANNOT BE EMPTY!");
        return;
    }

    var val_three = $("#Tbl_resources tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value");

    if (val_three == "") {
        alert("AMOUNT FIELD CANNOT BE EMPTY!");
        return;
    }

    else {
        var val = stripNonNumeric(val_three);
        if (isNaN(val) == true) {
            $("#Tbl_resources tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");
            alert("AMOUNT FIELD MUST BE A NUMBER.");
            return;
        }
        val_three = addNewComma(val);
    }

    var rowid = $("#Tbl_resources tr").length - 1;
    rowid++;
    $("#Tbl_resources tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:100px;\"><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:200px;\"><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" style=\"width:97%\" /></td>" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + val_three + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('Tbl_resources'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"

	);
    //value = $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value");
    //$("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", "");
    //var total = value + newValue;
    //alert(total);
    ////newtotal = AddComma(total);


    //$("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", total);

    //$("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", parseInt($("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value")) + parseInt(val_three));

    var total = $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value");
    if (parseFloat(total) == 0.00) {
        var newValue = stripNonNumeric(val_three);
        total = parseFloat(total) + parseFloat(newValue);
        var finalValue = addNewComma(total);

    }
    else {

        //var amount = parseFloat(val_three);
        //var newTotal = parseFloat(total);
        var newValue = stripNonNumeric(val_three);
        var finalTotal = stripNonNumeric(total);
        //var newValue = stripNonNumeric(val_three);
        //var finalTotal = stripNonNumeric(total);
        var overallTotal = parseFloat(finalTotal) + parseFloat(newValue);
        var finalValue = addNewComma(overallTotal);

    }


    $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", finalValue);


    // clear values
    $("#Tbl_resources tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#Tbl_resources tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
    $("#Tbl_resources tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");
}


function AddComma() {
    num = $("#txt_Amount").attr("value");
    var val_three = "";

    if (parseFloat(num) >= 1000) {

        val_three = formatNumberDec(num, 2, 1);
        // return val_three;
    }
    else {
        val_three = formatNumberDec(num, 2, 0);
    }
    $("#txt_Amount").attr("value", "");
    $("#txt_Amount").attr("value", val_three);
}

// Function that adds comma and formats number into currency
function formatNumberDec(num, places, comma) {

    var isNeg = 0;
    if (num < 0) {
        num = num * -1;
        isNeg = 1;
    }
    var myDecFact = 1;
    var myPlaces = 0;
    var myZeros = "";
    while (myPlaces < places) {
        myDecFact = myDecFact * 10;
        myPlaces = eval(myPlaces) + eval(1);
        myZeros = myZeros + "0";
    }
    onum = Math.round(num * myDecFact) / myDecFact;
    integer = Math.floor(onum);
    if (Math.ceil(onum) == integer) {
        decimal = myZeros;
    } else {
        decimal = Math.round((onum - integer) * myDecFact)
    }
    decimal = decimal.toString();
    if (decimal.length < places) {
        fillZeroes = places - decimal.length;
        for (z = 0; z < fillZeroes; z++) {
            decimal = "0" + decimal;
        }
    }

    if (places > 0) {
        decimal = "." + decimal;
    }
    if (comma == 1) {
        integer = integer.toString();
        var tmpnum = "";
        var tmpinteger = "";
        var y = 0;
        for (x = integer.length; x > 0; x--) {
            tmpnum = tmpnum + integer.charAt(x - 1);
            y = y + 1;
            if (y == 3 & x > 1) {
                tmpnum = tmpnum + ",";
                y = 0;
            }
        }

        for (x = tmpnum.length; x > 0; x--) {
            tmpinteger = tmpinteger + tmpnum.charAt(x - 1);
        }


        finNum = tmpinteger + "" + decimal;
    } else {
        finNum = integer + "" + decimal;
    }

    if (isNeg == 1) {
        finNum = "-" + finNum;
    }

    return finNum;

}

function addNewComma(num) {
    if (parseFloat(num) >= 1000) {
        val = formatNumberDec(num, 2, 1);
        // return val_three;
    }
    else {
        val = formatNumberDec(num, 2, 0);
    }

    return val;

}

// This function removes non-numeric characters
function stripNonNumeric(str) {
    str += '';
    var rgx = /^\d|\.|-$/;
    var out = '';
    for (var i = 0; i < str.length; i++) {
        if (rgx.test(str.charAt(i))) {
            if (!((str.charAt(i) == '.' && out.indexOf('.') != -1) ||
	             (str.charAt(i) == '-' && out.length != 0))) {
                out += str.charAt(i);
            }
        }
    }
    return out;
}





/*---------------END OF FUNCTION ADDING RESOURCES------------------------**/


/*---------------START OF ADDING TIMELINE--------------------------------**/

function AddTimeline() {

    var AddTimeline_one = $("#tbl_timeline tr:last td:nth-child(1) input[type=text]").attr("value");

    if (AddTimeline_one == "") {
        alert("START DATE FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var AddTimeline_two = $("#tbl_timeline tr:last td:nth-child(2) input[type=text]").attr("value");
    if (AddTimeline_two == "") {
        alert("END DATE FIELD CANNOT BE EMPTY!");
        return;
    }

    var AddTimeline_three = $("#tbl_timeline tr:last td:nth-child(3) input[type=text]").attr("value");

    if (AddTimeline_three == "") {
        alert("TASK FIELD CANNOT BE EMPTY!");
        return;
    }


    var AddTimeline_four = $("#tbl_timeline tr:last td:nth-child(4) input[type=text]").attr("value");

    if (AddTimeline_four == "") {
        alert("RESPONSIBLE PERSON FIELD CANNOT BE EMPTY!");
        return;
    }

    var AddTimeline_five = $("#tbl_timeline tr:last td:nth-child(5) input[type=text]").attr("value");

    if (AddTimeline_five == "") {
        alert("REMARKS FIELD CANNOT BE EMPTY!");
        return;
    }




    var rowid = $("#tbl_timeline tr").length - 1;
    rowid++;
    $("#tbl_timeline tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_three + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_four + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_five + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_timeline'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"



	);






    // clear values

    $("#tbl_timeline tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(4) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(5) input[type=text]").attr("value", "");


}


/*-----------------------------END OF FUNCTION ADDTIMELINE---------------------------**/


//Function for creating uploading box

function CreateUploadingBox(obj_id_to_position) {
    var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<!-- <a href=\"\"><img src=\"" + baseUrl + "Images/cancel.png\" style=\"border:0;\" /></a><br /> -->" +
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "SQL/UploadDialogBoxMarketingProgram\" width=\"330px\" height=\"76px\">" +
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


function CreateUploadingBoxs(obj_id_to_position) {
    var w = "" +
		"<div id=\"id_bkg_upload\" class=\"dlg_box_bkg\" onclick=\"javascript:hide_dialog_box();\"></div>" +
		"<div id=\"id_content_upload\" class=\"dlg_box_content\">" +
		"<div style=\"padding:3px; text-align:right;\">" +
		"<!-- <a href=\"\"><img src=\"" + baseUrl + "Images/cancel.png\" style=\"border:0;\" /></a><br /> -->" +
		"<iframe id=\"uploadframe\" src=\"" + baseUrl + "SQL/UploadDialogBoxForexcel\" width=\"330px\" height=\"76px\">" +
		"<p>Your browser does not support iframes.</p>" +
		"</iframe>" +
		"<br /><input type=\"button\" value=\"Close\" onclick=\"SaveToTextBoxs('" + obj_id_to_position + "');\" />" +
    //"<input type=\"button\" value=\"Close\" onclick=\"SaveToTextBoxs('" + obj_id_to_position + "');\" />" +
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
}


function SaveToTextBoxs(txt_box) {
     //alert($("#uploadframe").contents().find('body #data').attr('value'));
    var data = $("#uploadframe").contents().find('body #data').attr('value')
  

    if (data!= null) {

        var res_rows = data.split("#$");
        for (i = 0; i < res_rows.length; i++) {
            var res_cols = res_rows[i].split("|");
            if (res_cols[1] != null) {
                if (res_cols[1] != "") {

                    TarAccount(res_cols[0], res_cols[1], res_cols[2], res_cols[3])

                }
            }
        }

    } else {

        $("#id_content_upload").hide("fast");
        $("#id_bkg_upload").hide();
    
    }


    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();
}

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();
}



//Adding Attachment

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
			"<td style=\"width:170px;\"><input type=\"text\" value=\"" + Attachment_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + Attachment_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_attachment'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"



	)






    // clear values

    $("#tbl_attachment tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_attachment tr:last td:nth-child(2) input[type=text]").attr("value", "");


}



//Adding Project Activities

function AddProjectActualActivities() {

    var PAA_one = $("#tbl_PAA tr:last td:nth-child(1) input[type=text]").attr("value");

    if (PAA_one == "") {
        alert("DATE FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var PAA_two = $("#tbl_PAA tr:last td:nth-child(2) input[type=text]").attr("value");
    if (PAA_two == "") {
        alert("ACTIVITY/UPDATE FIELD CANNOT BE EMPTY!");
        return;
    }



    var rowid = $("#tbl_PAA tr").length - 1;
    rowid++;
    $("#tbl_PAA tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:170px;\"><input type=\"text\" value=\"" + PAA_one + "\" readonly=readonly class=\"readonly_fields\" size=\"28\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + PAA_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td><a href=\"javascript:DelCurrRow('tbl_PAA'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"



	)






    // clear values

    $("#tbl_PAA tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_PAA tr:last td:nth-child(2) input[type=text]").attr("value", "");


}


//Add target Account



function AddTargetAccount() {

    var TarAccount1 = $("#Tbl_targetAccount tr:last td:nth-child(1) input[type=text]").attr("value");

    if (TarAccount1 == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var TarAccount2 = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
    if (TarAccount2 == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }

    /* Third field */
    var TarAccount3 = $("#Tbl_targetAccount tr:last td:nth-child(3) input[type=text]").attr("value");
    if (TarAccount3 == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }


    /* fourth field */
    var TarAccount4 = $("#Tbl_targetAccount tr:last td:nth-child(4) input[type=text]").attr("value");
    if (TarAccount4 == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;
    }




    var rowid = $("#Tbl_targetAccount tr").length - 1;
    rowid++;
    $("#Tbl_targetAccount tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:100px;\"><input type=\"text\" value=\"" + TarAccount1 + "\" readonly=readonly class=\"readonly_fields\" size=\"28\" /></td>" +
            "<td style=\"width:200px;\"><input type=\"text\" value=\"" + TarAccount2 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:97%\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount3 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount4 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
		"</tr>"


	)



  //  $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", parseInt($("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value")) + parseInt(TarAccount4));


    // clear values

    $("#Tbl_targetAccount tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", "");


}








function SavingMarketingProg() {


    // Marketing Program no. 
    //    var programNo = "";
    //    programNo = $("#txt_mktgPNo").attr("value");

    //    if (programNo == "") {
    //        alert("Field cannot be empty!.");
    //        return;
    //    }


    // Program Name
    var progName = "";
    progName = $("#txt_progName").attr("value");
    if (progName == "") {
        alert("PROGRAM NAME FIELD CANNOT BE EMPTY!");
        return;
    }


    // Running Actual Cost
    // var RAC = "";
    //RAC = $("#txt_RAC").attr("value");
    //if (RAC == "") {
    //   alert("Running Actual Cost Field cannot be empty!.");
    //return;
    //}


    // status
    //    var status = "";
    //    status = $("#txt_status").attr("value");
    //    if (status == "") {
    //        alert("Field cannot be empty!.");
    //        return;
    //    }

    var status = 1;

    // Project Type
    var progType = "";
    progType = $("#txt_projtType").attr("value");
    if (progType == "--Select Project Type--") {
        alert("PROJECT TYPE FIELD CANNOT BE EMPTY!");
        return;
    }

    // Brand
    var brand = "";
    brand = $("#txt_brand").attr("value");
    if (brand == "") {
        alert("BRAND FIELD CANNOT BE EMPTY!");
        return;
    }

    // Target channel
    var targetChannel = "";
    targetChannel = $("#txt_targetChan").attr("value");
    if (targetChannel == "") {
        alert("TARGET CHANNEL FIELD CANNOT BE EMPTY!");
        return;
    }

    //    //Target Area
    //    var targetArea = "";
    //    targetArea = $("#txt_targetArea").attr("value");
    //    if (targetArea == "") {
    //        alert("Field cannot be empty!.");
    //        return;
    //    }


    //    // Target Accounts 

    //    var targetAccounts = "";
    //    targetAccounts = $("#txt_targetAccounts").attr("value");
    //    if (targetAccounts == "") {
    //        alert("Field cannot be empty!.");
    //        return;
    //    }


    // Background

    var backGround = "";
    backGround = $("#txt_backgrnd").attr("value");
    if (backGround == "") {
        alert("BACKGROUND FIELD CANNOT BE EMPTY!");
        return;
    }

    // objective
    var objective = "";
    objective = $("#txt_objective").attr("value");
    if (objective == "") {
        alert("OBJECTIVE FIELD CANNOT BE EMPTY!");
        return;
    }


    // measures

    var measures = "";
    measures = $("#txt_measures").attr("value");
    if (measures == "") {
        alert("MEASURES FIELD CANNOT BE EMPTY!");
        return;
    }

    // prepared by
    var preparedBy = "";
    preparedBy = $("#txt_prepby").attr("value");
    if (preparedBy == "") {
        alert("PREPARED BY FIELD CANNOT BE EMPTY!");
        return;
    }

    //     Excel File
    //    var excel = "";
    //    excel = $("#file_txt").attr("value");
    //    if (excel == "") {
    //        alert(" Upload Excel File.");
    //        return;
    //    }






    // resources

    var resources = "";
    row_count = $("#Tbl_resources tr").length - 2;
    for (i = 3; i <= row_count; i++) {
        if (resources != "") {
            resources = resources + "$";
        }
        // first column
        resources = resources + $("#Tbl_resources tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        resources = resources + $("#Tbl_resources tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        //third column
        resources = resources + $("#Tbl_resources tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
    }


    //total resources

    var totalAmtResources = "";
    var totalAmtResources = $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value");
    if (totalAmtResources == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;

    }

    //total allocatiom per account

    var totalAmt = "";
    var totalAmt = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
    if (totalAmt == "") {
        alert("FIELD CANNOT BE EMPTY!");
        return;

    }





    // time line

    var timeline = "";
    row_count = $("#tbl_timeline tr").length - 1;
    //alert(row_count);
    for (i = 3; i <= row_count; i++) {
        if (timeline != "") {
            timeline = timeline + "$";
        }
        // first column
        timeline = timeline + $("#tbl_timeline tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        timeline = timeline + $("#tbl_timeline tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        //third column
        timeline = timeline + $("#tbl_timeline tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        //fourth column
        timeline = timeline + $("#tbl_timeline tr:nth-child(" + i + ") td:nth-child(4) input").attr('value') + "|";
        //fifth column
        timeline = timeline + $("#tbl_timeline tr:nth-child(" + i + ") td:nth-child(5) input").attr('value') + "|";


    }


    // Target Accounts

    var TargetAcct = "";
    row_count = $("#Tbl_targetAccount tr").length - 1;
    //alert(row_count);
    for (i = 4; i <= row_count; i++) {
        if (TargetAcct != "") {
            TargetAcct = TargetAcct + "$";
        }
        // first column
        TargetAcct = TargetAcct + $("#Tbl_targetAccount tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        TargetAcct = TargetAcct + $("#Tbl_targetAccount tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";
        //third column
        TargetAcct = TargetAcct + $("#Tbl_targetAccount tr:nth-child(" + i + ") td:nth-child(3) input").attr('value') + "|";
        //fourth column
        TargetAcct = TargetAcct + $("#Tbl_targetAccount tr:nth-child(" + i + ") td:nth-child(4) input").attr('value');


    }

    //alert(UrlEncode(TargetAcct));


    // Saving attachment

    var Attachment = "";
    row_count = $("#tbl_attachment tr").length - 1;
    //alert(row_count);
    for (i = 3; i <= row_count; i++) {
        if (Attachment != "") {
            Attachment = Attachment + "$";
        }
        // first column
        Attachment = Attachment + $("#tbl_attachment tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        Attachment = Attachment + $("#tbl_attachment tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";

    }


    var actActivities = "";
    row_count = $("#tbl_PAA tr").length - 1;
    //alert(row_count);
    for (i = 3; i <= row_count; i++) {
        if (actActivities != "") {
            actActivities = actActivities + "$";
        }
        // first column
        actActivities = actActivities + $("#tbl_PAA tr:nth-child(" + i + ") td:nth-child(1) input").attr('value') + "|";
        // second column
        actActivities = actActivities + $("#tbl_PAA tr:nth-child(" + i + ") td:nth-child(2) input").attr('value') + "|";

    }



    $.ajax({
        type: "POST", url: baseUrl + "MarketingProgram/ConfirMktgPrograms",
        data: "" +

        //"programNo=" + programNo + "&" +
        "status=" + status + "&" +
            "progName=" + progName + "&" +
            "progType=" + progType + "&" +
            "brand=" + brand + "&" +
            "targetChannel=" + targetChannel + "&" +
        //"targetArea=" + targetArea + "&" +
        // "targetAccounts=" + targetAccounts + "&" +
            "backGround=" + backGround + "&" +
            "objective=" + objective + "&" +
            "measures=" + measures + "&" +
            "preparedBy=" + preparedBy + "&" +
            "totalAmtResources=" + totalAmtResources + "&" +
            "resources=" + resources + "&" +
            "timeline=" + timeline + "&" +
            "Attachment=" + Attachment + "&" +
             "actActivities=" + actActivities + "&" +
             "totalAmt=" + totalAmt + "&" +
             "TargetAcct=" + UrlEncode(TargetAcct) +
            ""
        ,
        success: function (res) {
            alert("SUCCESSFULLY SAVED!");
            location.reload();
           // $("#btn_save").style.visbility = "hidden";
           // $("#btn_cancel").style.visbility = "hidden";
          //  location.reload();


        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });




}




/*----------------------------------------------------TEST FOR MULTIPLE-----------------------------------------------**/



/*----------------------------------------------------END TEST FOR MULTIPLE-----------------------------------------------**/




function SaveMarktingProg() {

    SavingMarketingProg();

}


function getListChannel() {


    /* radiobutton */
    strListOfItems = "";
    var tmpCounter = 0;
    $('#mydiv').find('input:checked').each(function (i, el) {
        var inputEl = $(el).attr("value");

        //alert(inputEl);

        if (strListOfItems != "") {

            strListOfItems = strListOfItems + ",";

        }

        strListOfItems = strListOfItems + inputEl;


        $("#txt_targetChan").attr("value", strListOfItems)




    });


}


function getListArea() {


    /* radiobutton */
    strListOfItems = "";
    var tmpCounter = 0;
    $('#mydiv').find('input:checked').each(function (i, el) {
        var inputEl = $(el).attr("value");

        //alert(inputEl);

        if (strListOfItems != "") {
            strListOfItems = strListOfItems + ",";
        }

        strListOfItems = strListOfItems + inputEl;
        $("#txt_targetArea").attr("value", strListOfItems)
    });

}



function getListAccount() {


    /* radiobutton */
    strListOfItems = "";
    var tmpCounter = 0;
    $('#mydiv').find('input:checked').each(function (i, el) {
        var inputEl = $(el).attr("value");

        //alert(inputEl);
        if (strListOfItems != "") {

            strListOfItems = strListOfItems + ",";


        }

        strListOfItems = strListOfItems + inputEl;



        $("#txt_targetAccounts").attr("value", strListOfItems)


    });






}




function getListBrand() {


    /* radiobutton */
    strListOfItems = "";
    var tmpCounter = 0;
    $('#mydiv').find('input:checked').each(function (i, el) {
        var inputEl = $(el).attr("value");

        //alert(inputEl);


        if (strListOfItems != "") {

            strListOfItems = strListOfItems + ",";


        }

        strListOfItems = strListOfItems + inputEl;



        $("#txt_brand").attr("value", strListOfItems)


    });

}




// Retrieving data from resources



function ResourcesData(val_one, val_two, val_three) {
    var val_three = parseFloat(val_three);
    var newVal_three = addNewComma(val_three);


    var rowid = $("#Tbl_resources tr").length - 1;
    rowid++;
    $("#Tbl_resources tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:100px;\"><input type=\"text\" value=\"" + val_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:200px;\"><input type=\"text\" value=\"" + val_two + "\" readonly=readonly class=\"readonly_fields\" style=\"width:97%\" /></td>" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + newVal_three + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +

		"</tr>"



	);

    var total = $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value");
    //    var newValue = stripNonNumeric(newVal_three);
    //    var finalTotal = stripNonNumeric(total);
    //    var overallTotal = parseFloat(finalTotal) - parseFloat(newValue);
    var finalValue = addNewComma(total);



    $("#Tbl_resources tr:last td:nth-child(2) input[type=text]").attr("value", finalValue);



    // clear values
    $("#Tbl_resources tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#Tbl_resources tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
    $("#Tbl_resources tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");


}

//function Hide() {
//    $("#Tbl_resources tr:nth-child(2)").hide();
//  
//}





// Retrieving data from timeline


function TimelineData(AddTimeline_one, AddTimeline_two, AddTimeline_three, AddTimeline_four, AddTimeline_five) {

    var rowid = $("#tbl_timeline tr").length - 1;

    rowid++;
    $("#tbl_timeline tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_three + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_four + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
             "<td style=\"width:150px;\"><input type=\"text\" value=\"" + AddTimeline_five + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
		"</tr>"

	);






    // clear values

    $("#tbl_timeline tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(3) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(4) input[type=text]").attr("value", "");
    $("#tbl_timeline tr:last td:nth-child(5) input[type=text]").attr("value", "");


}


// retrieving values from attachment data

function AttachmentData(Attachment_one, Attachment_two) {
    var preparedBy = "";
    preparedBy = $("#txt_prepby").attr("value");


    var rowid = $("#tbl_attachment tr").length - 1;
    rowid++;
    $("#tbl_attachment tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:170px;\"><input type=\"text\" value=\"" + Attachment_one + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + Attachment_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
			"<td> <a href=\"" + baseUrl + "SQL/DownloadFile?doctype=MKTP&fileName=" + Attachment_one + "&id=" + GPreparedBy + " \"><img src=\"" + baseUrl + "Images/page_white_get.png\" style=\"border:0;\" /></a> </td>" +
            "</tr>"

    //"<a href=\""+baseUrl+ SQL/DownloadFile?doctype=CCA&fileName=" + m_attch_ITR + "&id=" + m_acct_ccanum + "\">


    //SQL/DownloadFile?doctype=MKTP&fileName=" + m_attch_FS + "&id=" + m_acct_ccanum + "

	)






    // clear values

    $("#tbl_attachment tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_attachment tr:last td:nth-child(2) input[type=text]").attr("value", "");


}



function AddProjectActualActivitiesData(PAA_one, PAA_two) {




    var rowid = $("#tbl_PAA tr").length - 1;
    rowid++;
    $("#tbl_PAA tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:170px;\"><input type=\"text\" value=\"" + PAA_one + "\" readonly=readonly class=\"readonly_fields\" size=\"28\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + PAA_two + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
		"</tr>"



	)






    // clear values

    $("#tbl_PAA tr:last td:nth-child(1) input[type=text]").attr("value", "");
    $("#tbl_PAA tr:last td:nth-child(2) input[type=text]").attr("value", "");


}

function TarAccount(TarAccount1, TarAccount2, TarAccount3, TarAccount4) {
   

    var rowid = $("#Tbl_targetAccount tr").length - 1;
    rowid++;
    $("#Tbl_targetAccount tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:100px;\"><input type=\"text\" value=\"" + TarAccount1 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:98%\" /></td>" +
            "<td style=\"width:200px;\"><input type=\"text\" value=\"" + TarAccount2 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:98%\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount3 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount4 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td><a href=\"javascript:AcctDelCurrRows('Tbl_targetAccount'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"



	)

    var num = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
    var castedNum = parseFloat(num) + parseFloat(TarAccount4);
    var result = castedNum.toFixed(2);



    //$("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", parseInt($("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value")) + parseInt(TarAccount4));
    $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", result);

    // clear values


    // $("#Tbl_targetAccount tr:last td:nth-child(1) input[type=text]").attr("value", "");
    // $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", "");
   // $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
   // $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
    //$("#Tbl_targetAccount tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");
   // $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value", "");



}





//Routing


function CallRouting(val_action_type, val_mkt) {
    /*
    val_mark_type = {'disapprove', 'approve'}
    */
    // var remarks = "";

    $.ajax({
        type: "POST", url: baseUrl + "MarketingProgram/CallRouting",
        data:
			"val_action_type=" + val_action_type + "&" +
            "val_mkt=" + val_mkt + /*"&" +
            "val_accCode=" + val_accCode +**/
			""
			,
        success: function (res) {

            if (res.substring(0, 3) == "00:") {
                // success
                // alert("Success");
                location.reload();
            } else {
                // error
                alert(res.substring(3, res.length));
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}


function gotoSearch() {

    window.location = baseUrl + "MarketingProgram/SearchWindow"


}


function AddnewMarketingProgram() {

    window.location = baseUrl + "MarketingProgram/MarketingPog"


}


//function CancelMarketingProgram() {
//    alert("Are you sure you want to cancel?");

//    window.location = baseUrl + "MarketingProgram/MarketingProgramFrontpage"

////    var $submitButton = $("#btn_save").attr('value');
////    $submitButton.attr("disabled", "true");





//}

function CancelMarketingProgram() {
    var okToRefresh = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (okToRefresh) {
        //			setTimeout("location.reload(true);",1500);
        window.location = baseUrl + "MarketingProgram/MarketingProgramFrontpage"
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else {
        return true;
    }
}




function displayWindowInfo() {
    var winInfo = "";
    //    winInfo = "Number of frames: " + window.length + "\r"
    //    winInfo += "Window object name: " + window.window + "\r"
    //    winInfo += "Window parent name: " + window.parent + "\r"
    //    winInfo += "URL: " + window.location + "\r"

    //alert(winInfo)

    var ProjectNumber = "";
    ProjectNumber = $("#MPD_txt_mktgPNo").attr("value");

    window.open(baseUrl + "MarketingProgram/RunningCostDetails?programnumbertosearch=" + ProjectNumber, "Window1", "menubar=no,width=1000,height=500,toolbar=no,scrollbars=yes");

    // window.location = baseUrl + "MarketingProgram/MarketingProgramFrontpage"

}

function download(file) {
    //window.location = file;
    window.open(file, 'Download');

}

//remove specific column and row of a table

function Undo() {

    var rowid = $("#Tbl_targetAccount tr").length - 4;
    for (var i = 0; i < rowid; i++) {
        $("#Tbl_targetAccount tr:nth-child(4)").remove();
    }

    value = "0.00"

    $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", '0.00');
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value", "");

}



// FOR DISPLAYING THE STATUS

function TarAccountsForDisplay(TarAccount1, TarAccount2, TarAccount3, TarAccount4) {

    var rowid = $("#Tbl_targetAccount tr").length - 1;
    rowid++;
    $("#Tbl_targetAccount tr:last").prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:100px;\"><input type=\"text\" value=\"" + TarAccount1 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:98%\" /></td>" +
            "<td style=\"width:200px;\"><input type=\"text\" value=\"" + TarAccount2 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:98%\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount3 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount4 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
           
		"</tr>"



	)

    var num = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
    var castedNum = parseFloat(num) + parseFloat(TarAccount4);
    var result = castedNum.toFixed(2);



    //$("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", parseInt($("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value")) + parseInt(TarAccount4));
    $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", result);

    // clear values


    // $("#Tbl_targetAccount tr:last td:nth-child(1) input[type=text]").attr("value", "");
    // $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", "");
   // $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
  //  $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
   // $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");
   // $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value", "");



}


// FOR MANUALLY INPUT

function TarAccounts(TarAccount1, TarAccount2, TarAccount3, TarAccount4) {
    var TarAccount1 = $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value");

    if (TarAccount1 == "") {
        alert("ACCOUNT CODE FIELD CANNOT BE EMPTY!");
        return;
    }

    /* second field */
    var TarAccount2 = $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value");
    if (TarAccount2 == "") {
        alert("TARGET ACCOUNT FIELD CANNOT BE EMPTY!");
        return;
    }

    /* Third field */
    var TarAccount3 = $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value");
    if (TarAccount3 == "") {
        alert("ACCOUNT NAME FIELD CANNOT BE EMPTY!");
        return;
    }


    /* fourth field */
    var TarAccount4 = $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value");
    if (TarAccount4 == "") {
        alert("AREA FIELD CANNOT BE EMPTY!");
        return;
    } else {
            // check if integer
            if (isNaN(TarAccount4) == true) {
                alert("AMOUNT MUST BE A NUMBER.");
                return;
            }
        }


    var rowid = $("#Tbl_targetAccount tr").length - 2;
    rowid++;
    $("#Tbl_targetAccount tr:last").prev().prev().after(
		"<tr RowId=\"" + rowid + "\">" +
			"<td style=\"width:100px;\"><input type=\"text\" value=\"" + TarAccount1 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:98%\" /></td>" +
            "<td style=\"width:200px;\"><input type=\"text\" value=\"" + TarAccount2 + "\" readonly=readonly class=\"readonly_fields\" style=\"width:98%\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount3 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td style=\"width:150px;\"><input type=\"text\" value=\"" + TarAccount4 + "\" readonly=readonly class=\"readonly_fields\" size=\"40\" /></td>" +
            "<td><a href=\"javascript:AcctDelCurrRows('Tbl_targetAccount'," + rowid + " );\"><img src=\"" + baseUrl + "Images/delete.png\" style=\"border:0;\" /></a></td>" +
		"</tr>"



	)

    var num = $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value");
    var castedNum = parseFloat(num) + parseFloat(TarAccount4);
    var result = castedNum.toFixed(2);



    //$("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", parseInt($("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value")) + parseInt(TarAccount4));
    $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", result);

    // clear values


    // $("#Tbl_targetAccount tr:last td:nth-child(1) input[type=text]").attr("value", "");
    // $("#Tbl_targetAccount tr:last td:nth-child(2) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(1) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(2) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(3) input[type=text]").attr("value", "");
    $("#Tbl_targetAccount tr:last").prev().find("td:nth-child(4) input[type=text]").attr("value", "");



}
