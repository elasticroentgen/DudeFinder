function startLocateWatch() {
    window.map.on("locationfound", function (e) {

        console.log("Location updated!");

        var geopos = e.latlng;
        if (typeof window.ownMarker === 'undefined') {
            window.map.setView(geopod, 17);
        } else {
            window.map.removeLayer(window.ownMarker);
        }

        window.ownMarker = L.marker(geopos);
        window.map.addLayer(window.ownMarker);

        window.geoOwn = geopos;

    });

    window.map.locate({ watch: true, enableHighAccuracy: true });

}

function locate() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            var geopos = new L.LatLng(position.coords.latitude, position.coords.longitude);

            if (typeof window.ownMarker === 'undefined') {
                window.map.setView(geopos, 17);
            } else {
                window.map.removeLayer(window.ownMarker);
            }

            window.ownMarker = L.marker(geopos);
            window.map.addLayer(window.ownMarker);

            window.geoOwn = geopos;
        }, function () { $("#locateError").show(); });
    } else {
        $("#locateError").show();
    }
}

function showCreateParty() {
    $("#createParty").show();
    window.map.on("click", function (e) {

        if (window.testLayer) {
            window.testLayer.removeLayer(createCircle);
        }
        window.testLayer = new L.LayerGroup();

        createCircle = L.circle(e.latlng, 20, {
            color: 'red',
            fillColor: '#f03',
            fillOpacity: 0.5
        }).addTo(testLayer);

        window.map.addLayer(window.testLayer);



        $("#createParty").data("position", e.latlng);
        $("#createParty").hide();
        $("#confirmParty").show();
    });
}

function cancelParty() {
    $("#confirmParty").hide();
    if (window.testLayer) {
        window.testLayer.removeLayer(createCircle);
    }
}

