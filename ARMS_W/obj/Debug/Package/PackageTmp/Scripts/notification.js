var counter = 1;

$(function () {
    var run = setInterval(getPending, 5000);
    //window.setInterval(getPending(), 1);
});

function getPending() {
  //  alert('test');
    $(".notificationicon").text(counter);
    counter = counter + 1;

//    $.ajax({
//        type: "POST",
//        url: baseurl + "Home/getPending",
//        data: { "myID": id },
//        dataType: "JSON",
//        success: function (data) {
//            $('.rowAdded1').remove();
//            counter = 0;
//            if (data.length > 0) {
//                var number = 0;
//               
//                $(".notificationicon").remove();
//                $(".btnNotification").append("<div class='notificationicon' style='font-size:9px'>" + counter + "</div>");

//            }

//            else {
//                $('#divContainer').hide();
//                $(".notificationicon").remove();
//                $(".btnNotification").append("<div class='notificationicon'>0</div>");
//            }
//        }
//    });
}

function showNotifications() {
    alert('test');
}