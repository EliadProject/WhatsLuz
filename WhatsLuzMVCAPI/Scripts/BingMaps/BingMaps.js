//get all places info from the DB
var Places = [];

$.ajax({
    type: "GET",
    url: 'http://localhost:61733/Places/getAllPlaces',
    dataType: 'json',
    async: false,
    accepts: "application/json",
    success: function (data) {
        Places = data;
    }
});

function GetMap(divId  = "map")
{
    SelectVal = document.getElementById("location").value;
    cord = getPlaceCord(SelectVal);
    getTemperature(cord);
    map = new Microsoft.Maps.Map(document.getElementById(divId), {center: new Microsoft.Maps.Location(cord.lat, cord.lng), zoom: 15 });
    var center = map.getCenter();
    var pin = new Microsoft.Maps.Pushpin(center, {icon: 'https://www.bingmapsportal.com/Content/images/poi_custom.png' });
    map.entities.push(pin);
}

function getPlaceCord(PlaceName)
{   
    for (i = 0; i < Places.length; i++) { 
        if(Places[i].Name == PlaceName)
        return Places[i];
    }
}


