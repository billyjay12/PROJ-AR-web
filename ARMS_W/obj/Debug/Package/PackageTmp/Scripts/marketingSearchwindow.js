
var ns4;

function SetValueFromSelect(obj) {

    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));


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

function hide_dialog_box() {
    $("#id_content").hide("fast");
    $("#id_bkg").hide();


}


function SaveToTextBox(txt_box) {
    $("#" + txt_box).attr('value', $("#uploadframe").contents().find('body #file_name').attr('value'))
    $("#id_content_upload").hide("fast");
    $("#id_bkg_upload").hide();
}




function LookUpData(obj_id_to_store, str_data) {
   // DisplayPreloader();
    $.ajax({
        type: "POST", url: baseUrl + "SQL/GetList",
        data: "_str_data=" + str_data,
        success: function (res) {
            if (IsError(res)) {
                CreateDialogBox(obj_id_to_store, StrResultTags(res));
            }

            //HidePreloader();
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
        if (res_cols[0] != null) {
            if (res_cols[0] != "") {
                w = w + "<option val_itemName=\"" + res_cols[0] + "\" value=\"" + res_cols[1] + "\">" + res_cols[0] + "</option>";
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



function DelCurrRow(tbl_id, r_id) {
    //$("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(2) input[type=text]").attr("value");
    var qty_total = $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value");
    var orig_qty = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(4) input[type=text]").attr("value");

    $("#table_details tr:last td:nth-child(2) input[type=text]").attr("value", parseInt(qty_total) - parseInt(orig_qty));

    var over_total = $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value");
    var partial_total = $("#" + tbl_id + " tr[RowId=" + r_id + "] td:nth-child(5) input[type=text]").attr("value");
    $("#table_details tr:last td:nth-child(3) input[type=text]").attr("value", parseInt(over_total) - parseInt(partial_total));
    $("#" + tbl_id + " tr[RowId=" + r_id + "]").remove();


}



function getSearch() {


//    var Bybrand = "";
//    Bybrand = $("#txt_searchByBrand").attr("value");

//    if (Bybrand == "") {
//        alert("Field cannot be empty!.");
//        return;
//    }


    window.location = baseUrl + "MarketingProgram/SearchList?brand=" + $("opt_selected_brand").attr("value");


}



function RedirectTosearch() {



    //By brand

    if ($("#byBrand").attr("checked") == true) {

        var searchBybrand = "";
        searchBybrand = $("#txt_brand").attr("value");

        if (searchBybrand == "") {
            alert("PLEASE CHOOSE VALUE FOR BRAND.");
            return;


        }

        // window.location = baseUrl + "MarketingProgram/SearchList?brandtosearch=" + searchBybrand;
        window.location = baseUrl + "MarketingProgram/SearchWindow?brandtosearch=" + searchBybrand;
    }

    // Date

    else if ($("#byDate").attr("checked") == true) {

        var searchByDate = "";
        searchByDate = $("#txt_date").attr("value")
        var endDates = "";
        endDates = $("#txt_end_date").attr("value");

        if (searchByDate == "" || searchByDate == "Start Date") {

            alert("PLEASE CHOOSE VALUE FOR START DATE.");
            return;


        }


        else if (endDates == "" || endDates == "End Date") {

            alert("PLEASE CHOOSE VALUE FOR END DATE");
            return;

        }

        else {


            //window.location = baseUrl + "MarketingProgram/SearchList?datetosearch=" + searchByDate;
            window.location = baseUrl + "MarketingProgram/SearchWindow?datetosearch=" + searchByDate + "&endDate=" + endDates;
            //window.location = baseUrl + "MarketingProgram/SearchWindow?endDate=" + endDates;

        }
    }

    else if ($("#byProjectType").attr("checked") == true) {

        var searchByProjectType = "";
        searchByProjectType = $("#txt_ProjectType").attr("value");

        if (searchByProjectType == "") {

            alert("PLEASE CHOOSE VALUE FOR PROJECT TYPE.");
            return;


        }

        //window.location = baseUrl + "MarketingProgram/SearchList?projecttypetosearch=" + searchByProjectType;
        window.location = baseUrl + "MarketingProgram/SearchWindow?projecttypetosearch=" + searchByProjectType;




    }


    else if ($("#byAmount").attr("checked") == true) {

        var searchByAmount = "";
        searchByAmount = $("#txt_amount").attr("value");
        var searchByAmount2 = "";
        searchByAmount2 = $("#txt_amount2").attr("value");
        if (searchByAmount == "" || searchByAmount == "Starting Amount") {

            alert("PLEASE INPUT STARTING AMOUNT");
            return;


        }

        else if (searchByAmount2 == "" || searchByAmount2 == "Ending Amount") {

            alert("PLEASE INPUT ENDING AMOUNT");
            return;

        }

        else {

            // check if integer
            if (isNaN(searchByAmount) == true) {
                alert("STARTING AMOUNT MUST BE A NUMBER");
                return;
            }

            // check if integer
            if (isNaN(searchByAmount2) == true) {
                alert("ENDING AMOUNT MUST BE A NUMBER");
                return;
            }

            // window.location = baseUrl + "MarketingProgram/SearchList?amounttosearch=" + searchByAmount;
            window.location = baseUrl + "MarketingProgram/SearchWindow?startingAmtRange=" + searchByAmount + "&UptoAmtRange=" + searchByAmount2;




        } 
    }

    else {

        alert("PLEASE SELECT VALUE TO BE SEARCHED.");
    }






    /*
    $.ajax({
        type: "POST", url: baseUrl + "MarketingProgram/SearchList",
        data:
			//"searchBybrand=" + searchBybrand + "&" +
           // "acct_type=" + acct_type + "&" +
			//"acct_key_account=" + acct_key_account + "&" +
            "searchBybrand=" + searchBybrand +
			""
			,
        ,
        success: function (res) {
            //alert("successfully save");

        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
    });
    */



}





function radioEvent () {


   // var acct_type = "";
    if ($("#byBrand").attr("checked") == true) {
        $("#txt_date").attr('disabled', 'disabled');
        $("#txt_amount").attr('disabled', 'disabled');
        $("#txt_ProjectType").attr('disabled', 'disabled');
       
  } else {
        acct_type = "indirect";
    }

}




//function getDataTobeSearch() {



//    //By brand
//    var searchBybrand = "";
//    searchBybrand = $("#txt_referred_to").attr("value");

//    if (searchBybrand == "") {
//        alert("Field cannot be empty!.");
//        return;
//    }



//    $.ajax({

//     type: "POST", url: baseUrl + "eMAT/ConfirmEmatTrans",
//        data: "" +

//        "searchBybrand"

//});


//}


function backtosearch() { 


 window.location = baseUrl + "MarketingProgram/Searchwindow"

}


function viewListdetail(){


    window.location = baseUrl + "MarketingProgram/MarketingProgramDetails"

}