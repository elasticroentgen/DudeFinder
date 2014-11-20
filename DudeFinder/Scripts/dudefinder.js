function locate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            var geopos = new L.LatLng(position.coords.latitude, position.coords.longitude);

            window.map.setView(geopos, 15);
            var marker = L.marker(geopos).addTo(map);
            marker.bindPopup("You're here!").openPopup();
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

