function changeType() {
    document.getElementById('txt_password').type = 'password';

}

$(document).ready(function () {


    $("#encrypt_pass").hide();
    $("#edit_button").click(function () {
        $("#txt_status").hide();
        //$("#txt_name").css("background-color", "yellow");
        $("#td_status").html("<select id='select_status'><option>ACTIVE</option><option>INACTIVE</option></select>");
    });

    $("#cancel_button").click(function () {
        //$('#iframeid', window.parent.document).attr('src', $('#iframeid', window.parent.document).attr('src'));
        document.location.reload();
    });

    $("#refresh_button").click(function () {
        document.location.reload();
    });
    $("#decrypt_pass").click(function () {

        var txt_current_pass = $("#txt_password");
        var new_obj = { password: $("#txt_password").val() }; //txt_current_pass.attr("#txt_password") };
        $.ajax({
            //dataType: 'json', 
            contentType: 'application/json; charset=utf-8',
            type: "POST", url: baseUrl + "SystemPage/DecryptPassword", data: JSON.stringify(new_obj), //data: "password=" + txt_current_pass.attr("original_password"),
            success: function (res) {
                txt_current_pass.removeAttr("readonly");
                txt_current_pass.prop("type", "text");
                txt_current_pass.attr("value", res);
                $("#decrypt_pass").hide();
                $("#encrypt_pass").show();
                //alert(res);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });

    });

    $("#encrypt_pass").click(function () {
        var encrypt = $("#txt_password_encrypted").val();
        $("#txt_password").val(encrypt);
        $("#encrypt_pass").hide();
        $("#decrypt_pass").show();
        //$("#txt_password").attr('value', userPass);
    });

    $("#update_button").click(function () {
        var empidno = $("#txt_idNo").val();
        var status = $("#select_status option:selected").text();
        alert("hey");
        $.ajax({
            type: "POST",
            url: baseUrl + "UserProfile/UpdateStatus",
            data: { empidno: empidno, status: status },
            success: function (res) {
                alert("Successfully updated.");
            }
        });
    });

});

