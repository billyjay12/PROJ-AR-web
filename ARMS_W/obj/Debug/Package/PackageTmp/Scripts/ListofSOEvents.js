var ns4;

$(function () {


    getListso();

   



});



//this function populates the list of so per asm
function getListso() {

    var new_obj = {

        ASMID: userId
    }


    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: "POST", url: baseUrl + "Calendar/GetListofSO",
        success: function (res) {
           
            if (res != undefined) {

                
                $(res).each(function (index, item) {

                    populateSoList(item[0], item[1], item[2], item[3], item[4], item[5]);



                });


            } else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
        }
    });

}

function populateSoList(EventId, soId, SoName, Month, Year, status) {

        var monthaname = "";
        switch (Month) {
            case "1":
                monthaname = "January";
                break;
            case "2":
                monthaname = "February";
                break;
            case "3":
                monthaname = "March";
                break;
            case "4":
                monthaname = "April";
                break;
            case "5":
                monthaname = "May";
                break;
            case "6":
                monthaname = "June";
                break;
            case "7":
                monthaname = "July";
                break;
            case "8":
                monthaname = "August";
                break;
            case "9":
                monthaname = "September";
                break;
            case "10":
                monthaname = "October";
                break;
            case "11":
                monthaname = "November";
                break;
                monthaname = "December";
            case "12":
                break;



        }

            $("#tbl_listofso .last_row").before('<tr clone="true" align="left"><td><a href=' + baseUrl + 'Calendar/CalendarView?soId=' + soId + '&year=' + Year + '&month=' + Month + '&EventId=' + EventId + '>' + EventId + '</a></td>' +
                          '<td>' + soId + '</td>' +
                          '<td>' + SoName + '</td>' +
                          '<td>' + monthaname + '</td>' +
                          '<td>' + Year + '</td>' +
                          '<td>' + status + '</td>' +
                          '</tr>');





}


