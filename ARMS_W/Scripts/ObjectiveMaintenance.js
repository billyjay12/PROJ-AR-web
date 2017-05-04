var txt_objCode = null;
var txt_objDesc = null;
var btn_save = null;
var btn_cancel = null;
var btn_searchObjCode = null;

var tbl_details = null;

$(function () {
    $("#tab_main").tabs();

    txt_objCode = $("#txt_objCode");
    txt_objDesc = $("#txt_objDesc");
    btn_save = $("#btn_save");
    btn_cancel = $("#btn_cancel");

    tbl_details = $("#tbl_details");
    getDetails();

    btn_save.click(function () {
        UpdateObjectives();
    });
});

function getDetails() {

    $.ajax({
        type: "POST", url: baseUrl + "Maintenance/listOfAllObjectives",
        success: function (res) {
            $("#div_main1").append(res);

            $('#tbl_list').checkboxTree({
                onCheck: {
                    node: 'expand'
                },
                onUncheck: {
                    node: 'collapse'
                },
                collapseImage: baseUrl + 'Images/downArrow.gif',
                expandImage: baseUrl + 'Images/rightArrow.gif'
            });


        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function UpdateObjectives() {
    var data = new Array();

    $('li[class="leaf"] :checkbox').each(function () {
        data.push({
            objectiveCode: $(this).attr("code"),
            FieldName: $(this).attr("value"),
            isUsed: $(this).attr("checked") == "checked" ? true : false
        });

    });

    var new_obj = { objective_list: data }

 //UPDATE 
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Maintenance/UpdateObjective",
        success: function (res) {
            if (!res.iserror) {
                alert("Success");
                location.reload();
            } else {
                /* alert(res.message); */if (res.message == "Session Expired!") window.parent.ShowLogin(); else alert(res.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}