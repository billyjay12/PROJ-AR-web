var ns4;

// Buttons event-handlers //

 function addUserRole()
 {
     var roleName = "";
     roleName = $("#txt_UserRole_name").attr("value");

     if (roleName == "") {
         alert("ROLE NAME FIELD CANNOT BE EMPTY!");
         return;
     }

     $.ajax({
         type: "POST", url: baseUrl + "Routing/addRoleName",
         data: "" +

            "roleName=" + roleName +
            ""
        ,
         success: function (res) {

             alert("SUCCESSFULLY SAVED!");
             location.reload();

         },
         error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
     }); 

 

 function addDesigApprvr() {
     var roleId = "";
     var branch = "";
     var name = "";
     var email = "";
     var brand = "";
     var empID = "";
     var roleName = "";
     var channel = "";
     var area = "";
     

     roleId = $("#Txt_desig_roleID").attr("value");
     branch = $("#Txt_desig_branch").attr("value");
     channel = $("#Txt_channel").attr("value");
     area = $("#Txt_area").attr("value");
     name = $("#Txt_desig_Name").attr("value");
     email = $("#Txt_desig_eMail").attr("value");
     brand = $("#Txt_desig_brand").attr("value");
     empID = $("#Txt_desig_IdNo").attr("value");
     roleName = $("#Txt_desig_RoleName").attr("value");

     if (roleName == "") {
         alert("ROLE NAME FIELD CANNOT BE EMPTY!");
         return;
         }

         $.ajax({
             type: "POST", url: baseUrl + "Routing/addApprvrDesig",
             data: "" +
            "roleID=" + roleId +
            "branch=" + branch +
            "channel=" + channel +
            "area=" + area +
            "name=" + name +
            "email=" + email +
            "brand=" + brand +
            "empID=" + empID +
            "",
             success: function (res) {
                 alert("SUCCESSFULLY SAVED!");
                 location.reload();
             },
             error: function (xhr, ajaxOptions, thrownError) { alert(xhr.status); alert(thrownError); }
         });
      }
  }

    function cancel() {

         location.reload();

     }


     // Textbox Event-handlers 

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

             if (obj_id_to_position == "Txt_desig_brand") {
                 w = w + "<option val_brandName=\"" + res_cols[1] + "\" value=\"" + res_cols[1] + "\">" + res_cols[1] + "</option>";                 
             }
             if (obj_id_to_position == "Txt_desig_Name") {
                 w = w + "<option val_soName=\"" + res_cols[1] + "\" value=\"" + res_cols[1] + "\">" + res_cols[0] + "</option>";
                // alert(res_cols[1] + "-1-" + res_cols[0] + "-0-" + res_cols[2] + "-2-");
             }
             if (obj_id_to_position == "Txt_desig_RoleName") {
                 w = w + "<option val_soName=\"" + res_cols[2] + "\" value=\"" + res_cols[1] + "\">" + res_cols[1] + "</option>";
                 // alert(res_cols[1] + "-1-" + res_cols[0] + "-0-" + res_cols[2] + "-2-");
             }

             if (obj_id_to_position == "Txt_channel") {
                 w = w + "<option val_soName=\"" + res_cols[0] + "\" value=\"" + res_cols[0] + "\">" + res_cols[0] + "</option>";
                 // alert(res_cols[1] + "-1-" + res_cols[0] + "-0-" + res_cols[2] + "-2-");
             }

             if (obj_id_to_position == "Txt_area") {
                 w = w + "<option val_soName=\"" + res_cols[0] + "\" value=\"" + res_cols[0] + "\">" + res_cols[0] + "</option>";
                 // alert(res_cols[1] + "-1-" + res_cols[0] + "-0-" + res_cols[2] + "-2-");
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

         if (obj == "Txt_desig_RoleName") {
             $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
             $("#Txt_desig_roleID").attr("value", $("#id_content select option:selected").attr('val_soname'));
         }

         if (obj == "Txt_channel") {
             $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
             $("#Txt_channel").attr("value", $("#id_content select option:selected").attr('val_soname'));
         }

         if (obj == "Txt_area") {
             $("#" + obj).attr("value", $("#id_content select option:selected").attr('value'));
             $("#Txt_area").attr("value", $("#id_content select option:selected").attr('val_soname'));
         }

         if (obj == "Txt_desig_brand") {
             $("#" + obj).attr("value", $("#id_content select option:selected").text());
         }
         if (obj == "Txt_desig_Name") {
             $("#" + obj).attr("value", $("#id_content select option:selected").text());
             $("#Txt_desig_IdNo").attr("value", $("#id_content select option:selected").attr('val_soname'))
             $("#Txt_desig_eMail").attr("value", $("#id_content select option:selected").text('value'));
         }
         if (obj == "txt_category") {
             $("#" + obj).attr("value", $("#id_content select option:selected").text());

             var category = $("#txt_category").attr("value");
             if (category != "billboard & streamer") {
                 if (category != "racks") {
                     $("#txt_size").attr('disabled', 'disabled');
                     //            $("#size_field").attr('disabled', 'disabled');
                 }

                 else {
                     $("#txt_size").removeAttr('disabled');
                 }
             }
             else {
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

     function DocSave() {

         addDesigApprvr();
        // addUserRole();
     
     
     }
