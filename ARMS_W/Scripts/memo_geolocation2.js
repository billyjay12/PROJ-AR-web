/* NOT USED */

//var watchID;
//var geoLoc;

//function showLocation(position) {
//    var longitiude = position.coords.longitude;
//    var latitude = position.coords.latitude;

//    setInterval(function () {
//        alert("Longitude: " + longitiude + "\nLatitude: " + latitude);
//    }, 1000);

//    navigator.geolocation.clearWatch(watchID);
//}

//function errorHandler(err) {
//    if (err.code == 1)
//        alert("Error: Access is denied");
//    else if (err.code == 2)
//        alert("Error: Position is unavailable");
//}

//function geoLocationUpdate() {
//    if (navigator.geolocation) {
//        var options = { timeout: 6000 };
//        geoLoc = navigator.geolocation;
//        watchID = geoLoc.watchPosition(showLocation, errorHandler, options);
//    }
//    else {
//        alert("Sorry, browser does not support geolocation!");
//    }
//}



var map,
    currentPositionMarker,
    mapCenter = new google.maps.LatLng(40.700683, -73.925972);

$(document).ready(function () {
    initLocationProcedure();
});

function initializeMap() {

    var map_settings =
    {
        zoom: 16,
        center: mapCenter,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById('map_canvas'), map_settings);

}

function locationError(Err) {
    alert("The current position could not be found!");
}

function setCurrentPosition(position) {
    currentPositionMarker = new google.maps.Marker({
        map:map,
        position: new google.maps.LatLng(position.coords.latitude, position.coords.longitude),
        title:"Current Position"
    });

    map.panTo(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
}

function displayAndWatch(position) {
    // set current position
    setCurrentPosition(position);
    // watch position
    watchCurrentPosition();
}

function watchCurrentPosition() {
    var positionTimer = navigator.geolocation.watchPosition(
        function (position) {
            setMarkerPosition(
                currentPositionMarker,
                position
            );
        });
}

function setMarkerPosition(marker, position) {
    marker.setPosition(
        new google.maps.LatLng(position.coords.latitude, position.coords.longitude)
        );
}


function initLocationProcedure() {
    initializeMap();
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(displayAndWatch, locationError);
    } else {
        alert("Your browser does not support the Geolocation API");
    }
}

