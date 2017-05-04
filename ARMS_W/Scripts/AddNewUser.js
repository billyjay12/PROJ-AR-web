var ns4;

function SetValueFromSelect(obj) {
    //    alert(obj);
    //    $("#" + obj).attr("value", GetValue($("#id_content select option:selected").text()));
    //    $("#" + obj).attr("value_id", GetId($("#id_content select option:selected").text()));
    if (obj == "txt_idNo") {
        $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
        $("#txt_lname").attr("value", $("#id_content select option:selected").text());
        $("#txt_fname").attr("value", $("#id_content select option:selected").attr('val_fname'));
        $("#txt_email").attr("value", $("#id_content select option:selected").attr('val_emailadd'));
    }
    if (obj == "txt_position") {

        $("#txt_position").attr("value", $("#id_content select option:selected").attr('val_Roleid'));

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
    //alert(data_to_add);
    for (i = 0; i < res_rows.length; i++) {
        var res_cols = res_rows[i].split("|");
        if (res_cols[1] != null) {
            if (res_cols[1] != "") {
                //alert(res_cols);
              
                if (obj_id_to_position == "txt_idNo") {
                    w = w + "<option val_fname=\"" + res_cols[2] + "\" val_emailadd=\"" + res_cols[3] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
                }
               

                if (obj_id_to_position == "txt_position") {
                    w = w + "<option val_Roleid=\"" + res_cols[1] + "\" value=\"" + res_cols[0] + "\">" + res_cols[1] + "</option>";
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
     return strVal.substring(0, strVal.indexOf('-') - 1);
//    return strVal;
}

function GetValue(strVal) {
     return strVal.substring(strVal.indexOf('-') + 2, 200);
//    return strVal;
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
        type: "POST", url: baseUrl + "SQL/GetEmployeeDetails",
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

function Cancel() {
    var okToRefresh = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (okToRefresh) {
        //			setTimeout("location.reload(true);",1500);
        window.location = baseUrl + "SystemPage/ListOfUser"
    }
}




function AddUser(){
  var status = "Active"; 

        var idNo = "";
        idNo = $("#txt_idNo").attr("value");

        if (idNo == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }

        else {
            CheckAvailability(idNo);
        }
        newIdNo = idNo;

        var lname = "";
        lname = $("#txt_lname").attr("value");

        if (lname == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }

        var fname = "";
        fname = $("#txt_fname").attr("value");

        if (fname == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }

        var position = "";
        position = $("#txt_position").attr("value");

        if (position == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }

        var emailAdd = "";
        emailAdd = $("#txt_email").attr("value");

        if (emailAdd == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }

        var userName = "";
        userName = $("#txt_username").attr("value");

         if (userName == "") {
             alert("FIELD CANNOT BE EMPTY!");
            return;
        }

        var password = "";
        password = $("#txt_password").attr("value");
        
        var confirmPassword = "";
        confirmPassword = $("#txt_confirm_password").attr("value");
        
        if (password == "" || confirmPassword == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }
        else {
            validatePassword(password, confirmPassword);
            
            }

        newPassword = password;
        passwordConfirm = confirmPassword;

        var area = "";
        area = $("#txt_area").attr("value");
        /*
        if (area == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }
        */

        var territory = "";
        territory = $("#txt_territory").attr("value");

        /*
        if (territory == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }
        */

        var region = "";
        region = $("#txt_region").attr("value");

        /*
        if (region == "") {
            alert("FIELD CANNOT BE EMPTY!");
            return;
        }
        */
        $.ajax({
            type: "POST", url: baseUrl + "UserProfile/AddUser",
            data: "" + "idNo=" + newIdNo + "&" +
                      "status=" + status + "&" +
                      "lname=" + lname + "&" +
                      "fname=" + fname + "&" +
                      "position=" + position + "&" +
                      "emailAdd=" + emailAdd + "&" +
                      "userName=" + userName + "&" +
                      "password=" + newPassword + "&" +
                      "area=" + area + "&" +
                      "territory=" + territory + "&" +
                      "region=" + region +
                      ""
        ,
            success: function (res) {
                alert("SUCCESSFULLY ADDED!");
                //  window.location = baseUrl + "UserProfile/ListOfUsers"
                window.location = baseUrl + "SystemPage/ListOfUsers";
            },
            error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
        });

    }

    function validatePassword(password, confirmPassword) 
   {
        var userName = "";
        userName = $("#txt_username").attr("value");

        if (password != "" && password == confirmPassword) 
		{

            if (password.length < 6) {
                alert("ERROR: PASSWORD MUST CONTAIN AT LEAST SIX CHARACTERS!");
                password.focus();
                return false;
                }

            else if (password == userName) {
                alert("ERROR: PASSWORD MUST BE DIFFERENT FROM USERNAME!");
                password.focus();
                return false;
                }
            
            else {
                    testPassword = /[0-9]/;
                    if (!testPassword.test(password)) {
                    alert("ERROR: PASSWORD MUST CONTAIN AT LEAST ONE NUMBER (0-9)!");
                    password.focus();
                    return false;
                    }
                 
                    testPassword = /[a-z]/;
                    if (!testPassword.test(password)) {
                    alert("ERROR: PASSWORD MUST CONTAIN AT LEAST ONE LOWERCASE LETTER (a-z)!");
                    password.focus();
                    return false;
                    }
                  testPassword = /[A-Z]/;
                  if (!testPassword.test(password)) {
                    alert("ERROR: PASSWORD MUST CONTAIN AT LEAST ONE UPPERCASE LETTER (A-Z)!");
                    password.focus();
                    return false;
                    }
                }
            }
           
        else if (password != confirmPassword) {
                alert("ERROR: YOUR PASSWORD DID NOT MATCH!");
                password.focus();
                return false;
            }

        else {
            return (password, confirmPassword);
        }
   
	}

	function CheckUser() {
	    var idNo = "";
	    idNo = $("#txt_idNo").attr("value");
	    if (idNo == "") {
	        alert("PLEASE CHOOSE AN EMPLOYEE FIRST!");
	        return;
	    }
	    else {

	        $.ajax({
	            type: "POST", url: baseUrl + "UserProfile/CheckUser",
	            data: "" + "idNo=" + idNo +
                     ""
        ,
	            success: function (res) {
	                if (res == "00:Already a user! Please choose another.") {
	                    alert("ALREADY A USER! PLEASE CHOOSE ANOTHER.");
	                    $("#txt_idNo").attr("value", "");
	                    $("#txt_lname").attr("value", "");
	                    $("#txt_fname").attr("value", "");
	                }
	                else {
	                    alert("NOT A USER.");
	                }


	            },
	            error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
	        });


	    }
	}

	function CheckAvailability(idNo) {
	    $.ajax({
	        type: "POST", url: baseUrl + "UserProfile/CheckUser",
	        data: "" + "idNo=" + idNo +
                     ""
        ,
	        success: function (res) {
	            if (res == "00:ALREADY A USER! PLEASE CHOOSE ANOTHER.") {
	                alert("ALREADY A USER! PLEASE CHOOSE ANOTHER.");
	                $("#txt_idNo").attr("value", "");
	                $("#txt_lname").attr("value", "");
	                $("#txt_fname").attr("value", "");
	                $("#txt_position").attr("value", "");
                    $("#txt_email").attr("value", "");
                    $("#txt_username").attr("value", "");
                    $("#txt_password").attr("value", "");
                    $("#txt_confirm_password").attr("value", "");
                    $("#txt_area").attr("value", "");
                    $("#txt_territory").attr("value", "");
                    $("#txt_region").attr("value", "");
    
	            }
	            else {
	                return idNo;
	            }


	        },
	        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
	    });


	}


