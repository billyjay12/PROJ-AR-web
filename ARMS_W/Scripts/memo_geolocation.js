var geocoder;
var isCheckIn = false;

$(document).ready(function () {
    initialize();
});

$(function () {

    if (geocoder == null) {
        alert("Please share location");
    }
    if (navigator.geolocation) {
        //navigator.geolocation.getCurrentPosition(success, error, { maximumAge: 600000, timeout: 5000, enableHighAccuracy: true });
        //        navigator.geolocation.getCurrentPosition(success, error, {
        //            timeout: 0,
        //            enableHighAccuracy: true,
        //            maximumAge: Infinity
        //        });
        navigator.geolocation.getCurrentPosition(success, error);
    } else {
        $(".btn_getLocation").click(function () {
            alert("please turn on the location services.");
        });
    }
});

function success(position) {
    /*  APPEND GOOGLE MAP WITH CURRENT LOCATION */

    var mapcanvas = document.createElement('div');
    mapcanvas.id = 'mapcanvas';
    mapcanvas.style.height = '150px';
    //mapcanvas.style.width = '260px';

    $(".mapcanvas11").append(mapcanvas);

    var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    var myOptions = {
        zoom: 15,
        center: latlng,
       // mapTypeControl: false,
       // navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL },
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("mapcanvas"), myOptions);

    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: "You are here! (at least within a " + position.coords.accuracy + " meter radius)"
    });

    $("#btn_checkin").click(function () {

        alert(navigator.geolocation);
        var c_date = new Date();
        // var p_date = new Date(Eventdate);

        var curr_date = c_date.getMonth() + 1 + "/" + c_date.getDate() + "/" + c_date.getFullYear();
        var pass_date = Eventmonth + "/" + Eventday + "/" + Eventyear;
        var p_date = new Date(pass_date);
        var c_date = new Date(curr_date);

        if (p_date.toString() != c_date.toString()) {
            alert("Check in is not yet applicable for this date");
            return;
        }

        if ($("#txt_cr_accountCode").attr("value") == "") {
            alert("Select account code before checking in");
            return;
        }

        if (navigator.geolocation) {
            isCheckIn = true;
            navigator.geolocation.getCurrentPosition(getLocation, error, { maximumAge: 600000, timeout: Infinity, enableHighAccuracy: true });
            //            navigator.geolocation.getCurrentPosition(getLocation, error, {
            //                timeout: 0,
            //                enableHighAccuracy: true,
            //                maximumAge: Infinity
            //            });
            //navigator.geolocation.getCurrentPosition(getLocation, error);
        } else {
            error('not supported');
        }
    });
    $("#btn_checkout").click(function () {

        var c_date = new Date();
        // var p_date = new Date(Eventdate);

        var curr_date = c_date.getMonth() + 1 + "/" + c_date.getDate() + "/" + c_date.getFullYear();
        var pass_date = Eventmonth + "/" + Eventday + "/" + Eventyear;
        var p_date = new Date(pass_date);
        var c_date = new Date(curr_date);

        if ($("#location_in").text() == "(error)" | $("#location_in").text() == "(error. something's wrong!)" | $("#location_in").text() == "" | $("#location_in").text() == "(For Check In)") {
            alert("Check in before checking out.");
            return;
        }

        if (p_date.toString() != c_date.toString()) {
            alert("Check out is not yet applicable for this date");
            return;
        }

        if ($("#txt_cr_accountCode").attr("value") == "") {
            alert("Select account code before checking out");
            return;
        }

        if (navigator.geolocation) {
            blockUI();
            isCheckIn = false;
            // navigator.geolocation.getCurrentPosition(getLocation, error);
            //            navigator.geolocation.getCurrentPosition(getLocation, error, {
            //                timeout: 0,
            //                enableHighAccuracy: true,
            //                maximumAge: Infinity
            //            });
            navigator.geolocation.getCurrentPosition(getLocation, error, { maximumAge: 600000, timeout: Infinity, enableHighAccuracy: true });
        } else {
            error('not supported');
        }
    });
}

function error() {
    HidePreloader();
    alert("Turn on location services to get current location.");
}

function initialize() {
    geocoder = new google.maps.Geocoder();
}


function getLocation(position) {
    var lat = position.coords.latitude;
    var lng = position.coords.longitude;
    codeLatLng(lat, lng)
}

function codeLatLng(lat, lng) {
    var currentdate = new Date();

    var displayTime = currentdate.getMonth() + 1 + "/" + currentdate.getDate() + "/" + currentdate.getFullYear() + " @ " + currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds();
    var dateTime = currentdate.getMonth() + 1 + "/" + currentdate.getDate() + "/" + currentdate.getFullYear() + " " + currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds();
    var latlng = new google.maps.LatLng(lat, lng);


    var CallReportLocation = new Object();

    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {

            if (results[1]) {


                if (isCheckIn) {
                    $("#location_in").text(results[0].formatted_address).css("color", "#000000"); //formatted address
                    $("#time_in").text(displayTime).css("color", "#000000");
                }
                else {
                    $("#location_out").text(results[0].formatted_address).css("color", "#000000"); //formatted address
                    $("#time_out").text(displayTime).css("color", "#000000");
                }

                //find country name
                for (var i = 0; i < results[0].address_components.length; i++) {
                    for (var b = 0; b < results[0].address_components[i].types.length; b++) {

                        //there are different types that might hold a city admin_area_lvl_1 usually does in come cases looking for sublocality type will be more appropriate
                        if (results[0].address_components[i].types[b] == "administrative_area_level_1") {
                            //this is the object you are looking for
                            city = results[0].address_components[i];
                            break;
                        }

                    }
                }

                CallReportLocation.eventMonth = Eventmonth;
                CallReportLocation.eventYear = Eventyear;
                CallReportLocation.eventDay = Eventday;
                CallReportLocation.empId = soId;
                CallReportLocation.acctCode = $("#txt_cr_accountCode").attr("value");
                CallReportLocation.contactPerson = $("#txt_cr_contactperson").attr("value");
                CallReportLocation.contactPersonNo = $("#txt_cr_contactpersonNo").attr("value");
                CallReportLocation.LineNum = $("#txt_hidden_cr_linenum").attr("value");
                CallReportLocation.Longitude = lng;
                CallReportLocation.Latitude = lat;
                CallReportLocation.Time = dateTime;
                CallReportLocation.Address = results[0].formatted_address;
                CallReportLocation.act_type = isCheckIn ? "CHECKIN" : "CHECKOUT";

                blockUI();
                saveLocation(CallReportLocation);
                //city data
                //alert(city.short_name + " " + city.long_name)


            } else {
                alert("No results found");
            }
        } else {
            alert("geo location failed due to: " + status);
        }
    });
}

function saveLocation(new_obj) {
    $.ajax({
        dataType: 'json', contentType: 'application/json; charset=utf-8', data: JSON.stringify(new_obj),
        type: 'POST', url: baseUrl + "Calendar/saveLocation",
        success: function (res) {
            if (!res.iserror) {
                alert("Your location has been saved.");
               // if (!res.isplanned)
               //     location.reload();
            }
            else {
                alert(res.message);
                if (isCheckIn) {
                    $("#location_in").text("(error)").css("color", "#ff0000"); 
                    $("#time_in").text("(error)").css("color", "#ff0000");
                }
                else {
                    $("#location_out").text("(error)").css("color", "#ff0000"); 
                    $("#time_out").text("(error)").css("color", "#ff0000");
                }

            }
            unblockUI();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status); alert(thrownError);
            
            if (isCheckIn) {
                $("#location_in").text("(error. something's wrong!)").css("color", "#ff0000"); 
                $("#time_in").text("(error. something's wrong!)").css("color", "#ff0000");
            }
            else {
                $("#location_out").text("(error. something's wrong!)").css("color", "#ff0000");
                $("#time_out").text("(error. something's wrong!)").css("color", "#ff0000");
            }
            unblockUI();
        }
    });
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

function unblockUI() {
    // $.HidePreloader();
    $.unblockUI();
}