var div_uploader = null;
var div_downloader = null;
var div_message = null;

var btn_upload = null;
var txtUpload = null;
var formUpload = null;

var btn_addfile = null;

var tbl_uploadfiles = null;
var tbl_downloader = null;

var file_counter = 1;


$(function () {
    div_uploader = $("#div_uploader");
    div_downloader = $("#div_downloader");
    div_message = $("#div_message");

    txtUpload = $("#txtUpload");
    formUpload = $("#formUpload");

    btn_upload = $("#btn_upload");
    btn_addfile = $("#btn_addfile");

    tbl_uploadfiles = $("#tbl_uploadfiles");
    tbl_downloader = $("#tbl_downloader");

    AllowPermissions();

    InitializeFileManager();

    btn_addfile.click(function () {
        if ($('input[type=text][name=file]').attr("value") == "") {
            alert("fill up description"); return;
        }
        tbl_uploadfiles.find('tr:nth-child(' + file_counter + ')').after('<tr clone="true">' +
                                                            '<td>File ' + (file_counter + 1) + ':</td>' +
                                                            '<td><input type="file" name="file_' + file_counter + '"></td>' +
                                                            '<td>Description:</td>' +
                                                            '<td><input type="text" name="file_' + file_counter + '"></td>' +
                                                        '</tr>');
                                                        
        file_counter += 1;
    });

    btn_upload.click(function () {
        if ($("#txtUpload").attr("value") != "") {
            blockUI();
            $("#formUpload").submit();
        }
        else
            alert("Select a file.");
    });
});

function InitializeFileManager() {

    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8',
        type: "POST", url: baseUrl + "Utilities/getListOfFiles",
        success: function (res) {
            var counter = 0;
            $(res.data.file).each(function (i, e) {
                tbl_downloader.find(".last_row").before('<tr clone="true" class="' + (counter % 2 == 0 ? "even" : "odd") + '">' +
                                                '<td><img src="' + baseUrl + 'Images/delete.png" class="btn_delete" id="' + e.id + '" /></td>' +
                                                '<td>' + e.FileAttachment + '</td>' +
                                                '<td style="width:300px;">' + e.FileDescription + '</td>' +
                                                '<td style="width:150px;">' + e.UploadedBy + '</td>' +
                                                '<td>' + FormatDate(e.DateTimeStamp) + '</td>' +
                                                 '<td><a href="' + baseUrl + 'Utilities/DownloadFile?id=' + e.id + '" style="text-decoration:none;">Download</a></td>' +
                                            '</tr>');

                counter = counter + 1;

            });

            $(".btn_delete").click(function () {
                if (confirm('Delete this uploaded file?')) {
                    deleteFile($(this).attr('id'));
                }
            });

        },
        error: function (xhr, ajaxOption, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

}

function deleteFile(id) {
    var new_obj = { id: id };
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + "Utilities/DeleteFile",
        success: function (res) {
            if (res >= 0) {
                location.reload();
            }
            else
                alert("Error deleting the file");
        },
        error: function (xhr, ajaxOption, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });
}

function AllowPermissions() {
    if (isUploader == "True")
        div_uploader.show();

    if (dateuploaded != "")
        div_downloader.show();
    else
        div_message.show();

}

function blockUI() {
    $.blockUI({ css: {
        border: 'none',
        padding: '15px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .5,
        color: '#fff'
    }
    });
}