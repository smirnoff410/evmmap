var map;
var rects =[], lines = [];
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: -34.397, lng: 150.644},
        zoom: 8
    });
    var drawingManager = new google.maps.drawing.DrawingManager({
        drawingMode: google.maps.drawing.OverlayType.MARKER,
        drawingControl: true,
        drawingControlOptions: {
          position: google.maps.ControlPosition.TOP_CENTER,
          drawingModes: ['polyline', 'rectangle']
        },
        rectangleOptions: {
          fillOpacity: 0.2,
          strokeWeight: 2,
          clickable: false,
          editable: true,
          zIndex: 1
        },
        polylineOptions: {
          fillOpacity: 0.2,
          strokeWeight: 2,
          clickable: false,
          editable: true,
          zIndex: 1
        }
    });
    drawingManager.setMap(map);
    google.maps.event.addListener(drawingManager, 'polylinecomplete', function(line) {
        var coords = line.getPath().getArray().toString();
        console.log(coords);
        alert("New polyline on: "+coords);
        lines[coords] = line;
    });
    google.maps.event.addListener(drawingManager, 'rectanglecomplete', function(rect) {
        var coords = rect.bounds.toString();
        console.log(coords);
        alert("New rectangle on: " + coords);
        coords = coords.split(/[()]/);
        var f_point = coords[2].split(/,/);
        var t_point = coords[4].split(/,/);
        f_point[1] = f_point[1].replace(/\s/, "");
        t_point[1] = t_point[1].replace(/\s/, "");
        console.log(f_point, t_point);
        /*$.ajax({
            method: "GET",
            url: "/Api/GetWeatherByBox?lon_left=" + "&lon_right=" + "&lat_bottom=" + "&lat_top=" + "&zoom="
        })*/
        rects[coords] = rect;
    });
}